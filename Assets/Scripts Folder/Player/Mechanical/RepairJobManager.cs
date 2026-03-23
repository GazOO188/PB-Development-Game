using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Groups a set of FastenerMechanics into one named repair task and tracks
/// completion. Each job now declares a required JobType, which is enforced
/// against every child fastener's jobType at startup.
///
/// TAG: "RepairJob" (must be created in the Tag Manager)
///
/// Typical scene hierarchy:
///
///   [RepairJob] Job_FixLeakyPipe  ← RepairJobManager (jobType = Plumbing)
///       ├── HexBolt_A              ← FastenerMechanic (jobType = Plumbing)
///       ├── HexBolt_B              ← FastenerMechanic (jobType = Plumbing)
///       └── HexBolt_C              ← FastenerMechanic (jobType = Plumbing)
/// </summary>
public class RepairJobManager : MonoBehaviour
{
    // ─────────────────────────────────────────────
    //  Inspector Config
    // ─────────────────────────────────────────────

    [Header("Job Info")]
    [Tooltip("Display name shown to the player (e.g. 'Fix Leaky Pipe').")]
    public string jobName = "Unnamed Repair Job";

    [TextArea]
    public string jobDescription = "";

    [Tooltip("XP awarded on completion.")]
    public int xpReward = 50;

    [Header("Job Classification")]
    [Tooltip("The job category this task belongs to.\n" +
             "All fasteners added to this job must share this JobType.\n\n" +
             "  Plumbing → uses Wrench  / HexBolt fasteners\n" +
             "  Boiler   → uses AllenKey / FlatHeadScrew fasteners\n" +
             "  Furnace  → uses Screwdriver / PhillipsScrew fasteners")]
    public JobType requiredJobType = JobType.Plumbing;

    [Header("Fasteners")]
    [Tooltip("Drag FastenerMechanic GameObjects here, OR leave empty to auto-collect children.")]
    public List<FastenerMechanic> fasteners = new List<FastenerMechanic>();

    [Header("Completion Mode")]
    [Tooltip("True  = all fasteners must be fully tightened to complete the job.\n" +
             "False = all fasteners must be fully loosened (e.g. disassembly task).")]
    public bool requireTightened = true;

    [Header("Events")]
    [Tooltip("Fired when every fastener reaches the required state.")]
    public UnityEvent onJobComplete;

    [Tooltip("Fired the first time any fastener in this job is interacted with.")]
    public UnityEvent onJobStarted;

    // ─────────────────────────────────────────────
    //  Constants
    // ─────────────────────────────────────────────

    private const string REPAIR_JOB_TAG = "RepairJob";

    // ─────────────────────────────────────────────
    //  Runtime State
    // ─────────────────────────────────────────────

    private bool _isComplete     = false;
    private bool _hasStarted     = false;
    private int  _completedCount = 0;

    // ─────────────────────────────────────────────
    //  Unity Lifecycle
    // ─────────────────────────────────────────────

    private void Awake()
    {
        if (!CompareTag(REPAIR_JOB_TAG))
            Debug.LogWarning($"[RepairJobManager] '{name}': Tag should be '{REPAIR_JOB_TAG}'.");
    }

    private void Start()
    {
        // Auto-collect child fasteners if none were manually assigned
        if (fasteners.Count == 0)
        {
            fasteners.AddRange(GetComponentsInChildren<FastenerMechanic>());
            Debug.Log($"[RepairJobManager] '{jobName}': Auto-collected {fasteners.Count} fasteners.");
        }

        ValidateFastenerJobTypes();
    }

    // ─────────────────────────────────────────────
    //  Messages from FastenerMechanic (via SendMessage)
    // ─────────────────────────────────────────────

    private void OnFastenerTightened(FastenerMechanic fastener)
    {
        if (!fasteners.Contains(fastener)) return;
        TryMarkStarted();
        if (requireTightened) CheckCompletion();
    }

    private void OnFastenerLoosened(FastenerMechanic fastener)
    {
        if (!fasteners.Contains(fastener)) return;
        TryMarkStarted();
        if (!requireTightened) CheckCompletion();
    }

    // ─────────────────────────────────────────────
    //  Completion Logic
    // ─────────────────────────────────────────────

    private void CheckCompletion()
    {
        if (_isComplete) return;

        int done = 0;
        foreach (FastenerMechanic f in fasteners)
        {
            if (requireTightened  && f.IsFullyTightened) done++;
            if (!requireTightened && f.IsFullyLoosened)  done++;
        }

        _completedCount = done;
        Debug.Log($"[RepairJobManager] '{jobName}' ({requiredJobType}): {done}/{fasteners.Count} done.");

        if (done >= fasteners.Count)
            CompleteJob();
    }

    private void CompleteJob()
    {
        _isComplete = true;
        Debug.Log($"[RepairJobManager] '{jobName}': COMPLETE! +{xpReward} XP");
        onJobComplete?.Invoke();
    }

    private void TryMarkStarted()
    {
        if (_hasStarted) return;
        _hasStarted = true;
        Debug.Log($"[RepairJobManager] '{jobName}': Started.");
        onJobStarted?.Invoke();
    }

    // ─────────────────────────────────────────────
    //  Validation
    // ─────────────────────────────────────────────

    /// <summary>
    /// Warns in the Editor if any child fastener's jobType does not match
    /// this job's requiredJobType. Mismatched fasteners will be silently
    /// blocked by the tool restriction system at runtime.
    /// </summary>
    private void ValidateFastenerJobTypes()
    {
        foreach (FastenerMechanic f in fasteners)
        {
            if (f.jobType != requiredJobType)
            {
                Debug.LogWarning(
                    $"[RepairJobManager] '{jobName}': Fastener '{f.name}' has jobType " +
                    $"'{f.jobType}' but this job requires '{requiredJobType}'. " +
                    $"It may never be completable with the correct tool.");
            }
        }
    }

    // ─────────────────────────────────────────────
    //  Public Helpers
    // ─────────────────────────────────────────────

    /// <summary>Returns completion progress as 0–1 (for HUD progress bars).</summary>
    public float GetProgressPercent()
    {
        if (fasteners.Count == 0) return 0f;
        int done = 0;
        foreach (FastenerMechanic f in fasteners)
        {
            if (requireTightened  && f.IsFullyTightened) done++;
            if (!requireTightened && f.IsFullyLoosened)  done++;
        }
        return (float)done / fasteners.Count;
    }

    public bool IsComplete     => _isComplete;
    public bool HasStarted     => _hasStarted;
    public int  CompletedCount => _completedCount;
    public int  TotalCount     => fasteners.Count;
}
