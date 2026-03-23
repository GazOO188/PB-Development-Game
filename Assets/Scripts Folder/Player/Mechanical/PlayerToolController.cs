using UnityEngine;

/// <summary>
/// Attach to the Player's Camera GameObject alongside (NOT replacing) PlayerController.
///
/// Solely responsible for:
///   • Equipping and swapping the three repair tools
///   • Raycasting to find Fasteners on the Interactable layer
///   • Routing tighten / loosen / lubricant input to FastenerMechanic
///
/// Has no knowledge of movement, gravity, crouching, or any other
/// PlayerController concerns. The two scripts are fully independent.
///
/// TAG  required on tool GameObjects : "RepairTool"
/// LAYER required on tool GameObjects: "Tool"
/// TAG  required on fastener GameObjects : "Fastener"
/// LAYER required on fastener GameObjects: "Interactable"
/// </summary>
public class PlayerToolController : MonoBehaviour
{
    // ─────────────────────────────────────────────
    //  Inspector Config
    // ─────────────────────────────────────────────

    [Header("Camera")]
    [Tooltip("The main player camera used for the interaction raycast.\n" +
             "Leave blank to auto-fetch from this GameObject.")]
    public Camera playerCamera;

    [Header("Three Tools")]
    [Tooltip("Drag the Wrench RepairTool GameObject here.")]
    public RepairTool wrench;

    [Tooltip("Drag the Allen Key RepairTool GameObject here.")]
    public RepairTool allenKey;

    [Tooltip("Drag the Screwdriver RepairTool GameObject here.")]
    public RepairTool screwdriver;

    [Header("Tool Hotkeys")]
    [Tooltip("Equip the Wrench (Plumbing jobs).")]
    public KeyCode wrenchKey      = KeyCode.Alpha1;

    [Tooltip("Equip the Allen Key (Boiler jobs).")]
    public KeyCode allenKeyKey    = KeyCode.Alpha2;

    [Tooltip("Equip the Screwdriver (Furnace / optional Boiler overlap).")]
    public KeyCode screwdriverKey = KeyCode.Alpha3;

    [Header("Interaction Input")]
    [Tooltip("Hold to tighten / drive the fastener in.")]
    public KeyCode tightenKey   = KeyCode.Mouse0;

    [Tooltip("Hold to loosen / back the fastener out.")]
    public KeyCode loosenKey    = KeyCode.Mouse1;

    [Tooltip("Input axis name (Input Manager) for analogue scroll-wheel torque.")]
    public string scrollAxis    = "Mouse ScrollWheel";

    [Tooltip("Apply lubricant (WD-40) to the targeted rusted fastener.")]
    public KeyCode lubricantKey = KeyCode.L;

    [Header("Reach")]
    [Tooltip("Maximum distance in metres at which the player can interact with a fastener.\n" +
             "Independent of PlayerController's raycastDist.")]
    public float interactionRange = 2.5f;

    [Header("Torque")]
    [Tooltip("Base torque delta applied per frame while a tighten/loosen key is held.")]
    public float torquePerFrame = 0.05f;

    // ─────────────────────────────────────────────
    //  Constants
    // ─────────────────────────────────────────────

    private const string FASTENER_TAG       = "Fastener";
    private const string INTERACTABLE_LAYER = "Interactable";

    // ─────────────────────────────────────────────
    //  Runtime State
    // ─────────────────────────────────────────────

    private RepairTool       _equippedTool;
    private FastenerMechanic _targetFastener;
    private int              _interactableLayerMask;

    // ─────────────────────────────────────────────
    //  Unity Lifecycle
    // ─────────────────────────────────────────────

    private void Awake()
    {
        if (playerCamera == null)
            playerCamera = GetComponent<Camera>();

        _interactableLayerMask = 1 << LayerMask.NameToLayer(INTERACTABLE_LAYER);

        ValidateToolReferences();
    }

    private void Start()
    {
        // Default to Wrench on game start
        EquipTool(wrench);
    }

    private void Update()
    {
        HandleToolSwap();
        ScanForFastener();
        HandleFastenerInput();
    }

    // ─────────────────────────────────────────────
    //  Tool Swapping
    // ─────────────────────────────────────────────

    private void HandleToolSwap()
    {
        if (Input.GetKeyDown(wrenchKey))      EquipTool(wrench); //needs set up
        if (Input.GetKeyDown(allenKeyKey))    EquipTool(allenKey);
        if (Input.GetKeyDown(screwdriverKey)) EquipTool(screwdriver);
    }

    /// <summary>
    /// Equip a tool by reference. Unequips the previous tool first.
    /// Can also be called externally from an inventory or UI system.
    /// </summary>
    public void EquipTool(RepairTool newTool)
    {
        if (newTool == null || newTool == _equippedTool) return;

        _equippedTool?.Unequip();
        _equippedTool = newTool;
        _equippedTool.Equip();

        Debug.Log($"[PlayerToolController] Equipped: {_equippedTool.toolName}" +
                  $" | Jobs: {string.Join(", ", _equippedTool.allowedJobTypes)}");
    }

    // ─────────────────────────────────────────────
    //  Fastener Targeting
    // ─────────────────────────────────────────────

    private void ScanForFastener()
    {
        Vector3 screenCentre = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        Ray     ray          = playerCamera.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, _interactableLayerMask))
        {
            if (hit.collider.CompareTag(FASTENER_TAG))
            {
                FastenerMechanic found = hit.collider.GetComponent<FastenerMechanic>();

                if (found != _targetFastener)
                {
                    _targetFastener?.EndInteraction();
                    _targetFastener = found;
                    Debug.Log($"[PlayerToolController] Targeting: '{_targetFastener.name}'" +
                              $" ({_targetFastener.jobType} / {_targetFastener.fastenerType})");
                }
                return;
            }
        }

        // Nothing valid in range – clear target
        if (_targetFastener != null)
        {
            _targetFastener.EndInteraction();
            _targetFastener = null;
        }
    }

    // ─────────────────────────────────────────────
    //  Fastener Interaction Input
    // ─────────────────────────────────────────────

    private void HandleFastenerInput()
    {
        if (_targetFastener == null || _equippedTool == null) return;

        // ── Lubricant ──────────────────────────────────────────────────
        if (Input.GetKeyDown(lubricantKey))
            _targetFastener.ApplyLubricant();

        // ── Tighten (hold) ─────────────────────────────────────────────
        if (Input.GetKey(tightenKey))
        {
            float delta = _equippedTool.GetTorqueDelta(torquePerFrame);
            _targetFastener.Interact(_equippedTool.gameObject, +delta);
        }
        // ── Loosen (hold) ──────────────────────────────────────────────
        else if (Input.GetKey(loosenKey))
        {
            float delta = _equippedTool.GetTorqueDelta(torquePerFrame);
            _targetFastener.Interact(_equippedTool.gameObject, -delta);
        }

        // ── Scroll-wheel analogue torque ───────────────────────────────
        float scroll = Input.GetAxis(scrollAxis);
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float delta = _equippedTool.GetTorqueDelta(scroll);
            _targetFastener.Interact(_equippedTool.gameObject, delta);
        }

        // ── Release ────────────────────────────────────────────────────
        if (Input.GetKeyUp(tightenKey) || Input.GetKeyUp(loosenKey))
            _targetFastener.EndInteraction();
    }

    // ─────────────────────────────────────────────
    //  Validation
    // ─────────────────────────────────────────────

    private void ValidateToolReferences()
    {
        if (wrench      == null) Debug.LogWarning("[PlayerToolController] Wrench reference is not assigned.");
        if (allenKey    == null) Debug.LogWarning("[PlayerToolController] Allen Key reference is not assigned.");
        if (screwdriver == null) Debug.LogWarning("[PlayerToolController] Screwdriver reference is not assigned.");
    }

    // ─────────────────────────────────────────────
    //  Public Accessors
    // ─────────────────────────────────────────────

    public RepairTool       EquippedTool   => _equippedTool;
    public FastenerMechanic TargetFastener => _targetFastener;

    // ─────────────────────────────────────────────
    //  Editor Gizmos
    // ─────────────────────────────────────────────

    private void OnDrawGizmos()
    {
        if (playerCamera == null) return;

        Gizmos.color = _targetFastener != null ? Color.green : Color.cyan;
        Gizmos.DrawRay(playerCamera.transform.position,
                       playerCamera.transform.forward * interactionRange);
    }
}
