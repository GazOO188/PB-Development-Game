using UnityEngine;

/// <summary>
/// Attach to any screw / bolt / fastener GameObject in the scene.
///
/// TAG  : "Fastener"      (must be set in the Inspector / Tag Manager)
/// LAYER: "Interactable"  (must be set in the Inspector / Layer Manager)
///
/// Each fastener declares:
///   • fastenerType  – the physical head shape (drives tool matching)
///   • jobType       – the repair category (drives job-type restriction)
///
/// The Interact() method silently returns without effect if the supplied tool
/// does not satisfy BOTH the fastener type AND the job type check.
/// </summary>
public class FastenerMechanic : MonoBehaviour
{
    // ─────────────────────────────────────────────
    //  Inspector Config
    // ─────────────────────────────────────────────

    [Header("Fastener Classification")]
    [Tooltip("Physical head type – determines which tool can drive this fastener.")]
    public FastenerType fastenerType = FastenerType.PhillipsScrew;

    [Tooltip("Job category this fastener belongs to.\n" +
             "A tool will be silently blocked if its allowedJobTypes does not include this value.\n\n" +
             "  HexBolt       → Plumbing\n" +
             "  FlatHeadScrew → Boiler\n" +
             "  PhillipsScrew → Furnace")]
    public JobType jobType = JobType.Furnace;

    [Header("Tightness")]
    [Tooltip("Current tightness: 0 = fully loose, 1 = fully tightened.")]
    [Range(0f, 1f)]
    public float tightnessLevel = 0f;

    [Tooltip("How many full rotations are needed to go from fully loose to fully tight.")]
    public float totalTurnsRequired = 5f;

    [Tooltip("Target torque in N·m. Reaching this value snaps the fastener to 'tight'.\n" +
             "Exceeding it by 20 % strips the fastener.")]
    public float targetTorque = 25f;

    [Header("State Flags")]
    public bool isStripped      = false;
    public bool isRusted        = false;
    [Tooltip("Set true (alongside isRusted) to require lubricant before the fastener can turn.")]
    public bool requiresWD40    = false;

    [Header("Visual")]
    [Tooltip("Local axis around which the fastener mesh rotates when driven.")]
    public Vector3 rotationAxis        = Vector3.up;
    [Tooltip("Degrees of visual rotation applied per torque unit each call.")]
    public float   rotationSpeedDegrees = 90f;

    [Header("Audio")]
    public AudioClip screwingSound;
    public AudioClip tightSound;
    public AudioClip strippedSound;
    public AudioClip rustySound;

    // ─────────────────────────────────────────────
    //  Constants
    // ─────────────────────────────────────────────

    private const string FASTENER_TAG       = "Fastener";
    private const string INTERACTABLE_LAYER = "Interactable";
    private const string TOOL_TAG           = "RepairTool";

    // ─────────────────────────────────────────────
    //  Runtime State
    // ─────────────────────────────────────────────

    private float       _currentTorque     = 0f;
    private float       _turnsCompleted    = 0f;
    private bool        _isFullyTightened  = false;
    private bool        _isFullyLoosened   = true;
    private bool        _isBeingInteracted = false;
    private AudioSource _audioSource;

    // ─────────────────────────────────────────────
    //  Unity Lifecycle
    // ─────────────────────────────────────────────

    private void Awake()
    {
        ValidateSetup();

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();

        // Sync runtime state to the serialised starting tightness
        _turnsCompleted   = tightnessLevel * totalTurnsRequired;
        _currentTorque    = tightnessLevel * targetTorque;
        _isFullyTightened = Mathf.Approximately(tightnessLevel, 1f);
        _isFullyLoosened  = Mathf.Approximately(tightnessLevel, 0f);
    }

    private void OnValidate()
    {
        _turnsCompleted = tightnessLevel * totalTurnsRequired;
        _currentTorque  = tightnessLevel * targetTorque;
    }

    // ─────────────────────────────────────────────
    //  Public API  (called by PlayerToolController)
    // ─────────────────────────────────────────────

    /// <summary>
    /// Attempt to interact with this fastener using <paramref name="tool"/>.
    /// Silently returns without effect if:
    ///   • The tool's tag is not "RepairTool"
    ///   • The tool's allowedJobTypes does not contain this fastener's jobType
    ///   • The tool's compatibleFasteners does not contain this fastener's fastenerType
    /// </summary>
    /// <param name="tool">The equipped tool's GameObject.</param>
    /// <param name="directionMultiplier">+1 = tighten, -1 = loosen.</param>
    public void Interact(GameObject tool, float directionMultiplier)
    {
        if (!ValidateTool(tool)) return;   // silent block

        _isBeingInteracted = true;

        if (directionMultiplier > 0f)
            TryTighten(directionMultiplier);
        else
            TryLoosen(Mathf.Abs(directionMultiplier));
    }

    /// <summary>Call when the player releases the interact button.</summary>
    public void EndInteraction()
    {
        _isBeingInteracted = false;
    }

    /// <summary>
    /// Apply WD-40 to a rusted fastener. Must be called before Interact()
    /// will have any effect on rusted fasteners.
    /// </summary>
    public void ApplyLubricant()
    {
        if (!isRusted) return;
        requiresWD40 = false;
        Debug.Log($"[Fastener] '{name}' ({jobType} / {fastenerType}): Lubricant applied – ready to turn.");
    }

    // ─────────────────────────────────────────────
    //  Core Tighten / Loosen Logic
    // ─────────────────────────────────────────────

    private void TryTighten(float amount)
    {
        if (_isFullyTightened)
        {
            PlayAudio(tightSound);
            return;
        }

        if (isStripped)
        {
            PlayAudio(strippedSound);
            Debug.LogWarning($"[Fastener] '{name}': Stripped – needs replacement.");
            return;
        }

        if (isRusted && requiresWD40)
        {
            PlayAudio(rustySound);
            Debug.Log($"[Fastener] '{name}': Rusted! Apply lubricant first.");
            return;
        }

        // Accumulate torque and turns
        float torqueApplied = amount * (targetTorque / totalTurnsRequired);
        _currentTorque     += torqueApplied;
        _turnsCompleted    += amount;
        tightnessLevel      = Mathf.Clamp01(_turnsCompleted / totalTurnsRequired);

        // Rotate the mesh
        transform.Rotate(rotationAxis, rotationSpeedDegrees * amount * Time.deltaTime, Space.Self);
        PlayAudio(screwingSound);

        // Over-torque check → strip fastener
        if (_currentTorque > targetTorque * 1.2f)
        {
            isStripped = true;
            PlayAudio(strippedSound);
            Debug.LogWarning($"[Fastener] '{name}': Over-torqued and stripped!");
            return;
        }

        if (_currentTorque >= targetTorque)
            SnapToTight();
    }

    private void TryLoosen(float amount)
    {
        if (_isFullyLoosened) return;

        if (isRusted && requiresWD40)
        {
            PlayAudio(rustySound);
            Debug.Log($"[Fastener] '{name}': Rusted! Apply lubricant first.");
            return;
        }

        _currentTorque  -= amount * (targetTorque / totalTurnsRequired);
        _turnsCompleted -= amount;
        tightnessLevel   = Mathf.Clamp01(_turnsCompleted / totalTurnsRequired);

        transform.Rotate(rotationAxis, -rotationSpeedDegrees * amount * Time.deltaTime, Space.Self);
        PlayAudio(screwingSound);

        if (_currentTorque <= 0f)
            SnapToLoose();
    }

    // ─────────────────────────────────────────────
    //  State Snapping
    // ─────────────────────────────────────────────

    private void SnapToTight()
    {
        _currentTorque    = targetTorque;
        _turnsCompleted   = totalTurnsRequired;
        tightnessLevel    = 1f;
        _isFullyTightened = true;
        _isFullyLoosened  = false;

        PlayAudio(tightSound);
        Debug.Log($"[Fastener] '{name}' ({jobType}): Fully tightened at {targetTorque} N·m.");

        // Bubble up to RepairJobManager (if this fastener is a child of one)
        SendMessage("OnFastenerTightened", this, SendMessageOptions.DontRequireReceiver);
    }

    private void SnapToLoose()
    {
        _currentTorque    = 0f;
        _turnsCompleted   = 0f;
        tightnessLevel    = 0f;
        _isFullyLoosened  = true;
        _isFullyTightened = false;

        Debug.Log($"[Fastener] '{name}' ({jobType}): Fully loosened.");
        SendMessage("OnFastenerLoosened", this, SendMessageOptions.DontRequireReceiver);
    }

    // ─────────────────────────────────────────────
    //  Validation
    // ─────────────────────────────────────────────

    /// <summary>
    /// Full tool validation: tag → RepairTool component → job type → fastener type.
    /// All failures are silent (returns false with no output).
    /// </summary>
    private bool ValidateTool(GameObject tool)
    {
        if (tool == null)                    return false;
        if (!tool.CompareTag(TOOL_TAG))      return false;

        RepairTool repairTool = tool.GetComponent<RepairTool>();
        if (repairTool == null)              return false;

        // Passes both job-type AND fastener-type to the single CanWorkWith call
        return repairTool.CanWorkWith(fastenerType, jobType);
    }

    private void ValidateSetup()
    {
        if (!CompareTag(FASTENER_TAG))
            Debug.LogWarning($"[Fastener] '{name}': Tag should be '{FASTENER_TAG}'.");

        if (LayerMask.LayerToName(gameObject.layer) != INTERACTABLE_LAYER)
            Debug.LogWarning($"[Fastener] '{name}': Layer should be '{INTERACTABLE_LAYER}'.");
    }

    // ─────────────────────────────────────────────
    //  Audio Helper
    // ─────────────────────────────────────────────

    private void PlayAudio(AudioClip clip)
    {
        if (clip == null || _audioSource == null) return;
        if (!_audioSource.isPlaying)
            _audioSource.PlayOneShot(clip);
    }

    // ─────────────────────────────────────────────
    //  Public Read-Only Properties
    // ─────────────────────────────────────────────

    public bool  IsFullyTightened  => _isFullyTightened;
    public bool  IsFullyLoosened   => _isFullyLoosened;
    public float CurrentTorque     => _currentTorque;
    public float TurnsCompleted    => _turnsCompleted;
    public bool  IsBeingInteracted => _isBeingInteracted;

    // ─────────────────────────────────────────────
    //  Editor Gizmos
    // ─────────────────────────────────────────────

    private void OnDrawGizmos()
    {
        // Sphere colour: red = loose → green = tight
        Gizmos.color = Color.Lerp(Color.red, Color.green, tightnessLevel);
        Gizmos.DrawWireSphere(transform.position, 0.05f);

        // Rotation axis indicator
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(rotationAxis) * 0.1f);

#if UNITY_EDITOR
        // Job type label visible in Scene view
        UnityEditor.Handles.Label(
            transform.position + Vector3.up * 0.12f,
            $"{jobType} / {fastenerType}");
#endif
    }
}
