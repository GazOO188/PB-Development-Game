using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to one of the three tool GameObjects (Wrench, Allen Key, Screwdriver).
///
/// TAG  : "RepairTool"   (must be set in the Inspector / Tag Manager)
/// LAYER: "Tool"         (must be set in the Inspector / Layer Manager)
///
/// Tool → Job Type → Fastener Type mapping (hard design rules):
/// ┌─────────────────┬──────────────────────┬─────────────────────┐
/// │ Tool            │ Allowed Job Types    │ Compatible Fasteners │
/// ├─────────────────┼──────────────────────┼─────────────────────┤
/// │ Wrench          │ Plumbing             │ HexBolt             │
/// │ Allen Key       │ Boiler               │ FlatHeadScrew       │
/// │ Screwdriver     │ Furnace (+ optional  │ PhillipsScrew       │
/// │                 │  Boiler overlap)     │                     │
/// └─────────────────┴──────────────────────┴─────────────────────┘
///
/// Overlap rule: The Screwdriver may be assigned to Boiler jobs in the Inspector
/// by adding JobType.Boiler to its allowedJobTypes list. It still only drives
/// PhillipsScrew fasteners regardless of job type.
/// </summary>
public class RepairTool : MonoBehaviour
{
    // ─────────────────────────────────────────────
    //  Inspector Config
    // ─────────────────────────────────────────────

    [Header("Tool Identity")]
    [Tooltip("Human-readable display name shown in the UI.")]
    public string toolName = "Wrench";

    [Tooltip("Which of the three physical tools this is.")]
    public ToolType toolType = ToolType.Wrench;

    [Header("Job Type Restrictions")]
    [Tooltip("Which job categories this tool is allowed to work on.\n" +
             "Attempting to use it on a fastener belonging to any other job type\n" +
             "is silently blocked (no interaction occurs).")]
    public List<JobType> allowedJobTypes = new List<JobType> { JobType.Plumbing };

    [Header("Compatible Fastener Types")]
    [Tooltip("Which fastener head types this tool can physically drive.\n" +
             "A tool is blocked if EITHER the job type OR the fastener type does not match.")]
    public List<FastenerType> compatibleFasteners = new List<FastenerType> { FastenerType.HexBolt };

    [Header("Tool Stats")]
    [Tooltip("Torque multiplier applied per input unit.\n" +
             "  Wrench      → 1.2  (leverage advantage)\n" +
             "  Allen Key   → 0.8  (short handle, precision tool)\n" +
             "  Screwdriver → 1.0  (baseline)")]
    [Range(0.1f, 5f)]
    public float torqueMultiplier = 1f;

    [Tooltip("Maximum safe torque (N·m) before risking fastener damage.\n" +
             "The FastenerMechanic strips a fastener if torque exceeds targetTorque × 1.2.")]
    public float maxSafeTorque = 30f;

    [Header("Durability")]
    [Tooltip("Current condition: 1 = perfect, 0 = broken.")]
    [Range(0f, 1f)]
    public float condition = 1f;

    [Tooltip("Condition lost per use call (set to 0 for an indestructible tool).")]
    public float wearPerUse = 0.001f;

    // ─────────────────────────────────────────────
    //  Constants
    // ─────────────────────────────────────────────

    private const string TOOL_TAG   = "RepairTool";
    private const string TOOL_LAYER = "Tool";

    // ─────────────────────────────────────────────
    //  Runtime State
    // ─────────────────────────────────────────────

    private bool _isEquipped = false;
    private bool _isBroken   = false;

    // ─────────────────────────────────────────────
    //  Unity Lifecycle
    // ─────────────────────────────────────────────

    private void Awake()
    {
        ValidateSetup();
        _isBroken = condition <= 0f;
    }

    // ─────────────────────────────────────────────
    //  Public API
    // ─────────────────────────────────────────────

    /// <summary>
    /// Primary compatibility check called by FastenerMechanic.
    /// Returns true only when ALL three conditions are met:
    ///   1. The tool is not broken.
    ///   2. The fastener's JobType is in this tool's allowedJobTypes list.
    ///   3. The fastener's FastenerType is in this tool's compatibleFasteners list.
    /// Failures are silent (no Debug output) – the interaction is simply blocked.
    /// </summary>
    public bool CanWorkWith(FastenerType requiredFastener, JobType requiredJob)
    {
        if (_isBroken)                               return false;
        if (!allowedJobTypes.Contains(requiredJob))  return false;
        if (!compatibleFasteners.Contains(requiredFastener)) return false;
        return true;
    }

    /// <summary>
    /// Returns the scaled torque delta for this frame.
    /// Applies wear automatically. Returns 0 if the tool is broken.
    /// </summary>
    public float GetTorqueDelta(float rawInput)
    {
        if (_isBroken) return 0f;
        ApplyWear();
        return rawInput * torqueMultiplier;
    }

    public void Equip()
    {
        _isEquipped = true;
        Debug.Log($"[RepairTool] '{toolName}' equipped.");
    }

    public void Unequip()
    {
        _isEquipped = false;
        Debug.Log($"[RepairTool] '{toolName}' unequipped.");
    }

    /// <summary>Restore condition by <paramref name="amount"/> (0–1 range).</summary>
    public void RestoreCondition(float amount = 1f)
    {
        condition = Mathf.Clamp01(condition + amount);
        _isBroken = condition <= 0f;
        Debug.Log($"[RepairTool] '{toolName}' restored to {condition * 100f:F0}% condition.");
    }

    // ─────────────────────────────────────────────
    //  Private Helpers
    // ─────────────────────────────────────────────

    private void ApplyWear()
    {
        condition = Mathf.Max(0f, condition - wearPerUse);
        if (condition <= 0f && !_isBroken)
        {
            _isBroken = true;
            Debug.LogWarning($"[RepairTool] '{toolName}' has broken from wear!");
        }
    }

    private void ValidateSetup()
    {
        if (!CompareTag(TOOL_TAG))
            Debug.LogWarning($"[RepairTool] '{name}': Tag should be '{TOOL_TAG}'.");

        if (LayerMask.LayerToName(gameObject.layer) != TOOL_LAYER)
            Debug.LogWarning($"[RepairTool] '{name}': Layer should be '{TOOL_LAYER}'.");

        if (allowedJobTypes.Count == 0)
            Debug.LogWarning($"[RepairTool] '{name}': allowedJobTypes is empty – tool will never work.");

        if (compatibleFasteners.Count == 0)
            Debug.LogWarning($"[RepairTool] '{name}': compatibleFasteners is empty – tool will never work.");
    }

    // ─────────────────────────────────────────────
    //  Properties
    // ─────────────────────────────────────────────

    public bool IsEquipped => _isEquipped;
    public bool IsBroken   => _isBroken;

    // ─────────────────────────────────────────────
    //  Editor Gizmos
    // ─────────────────────────────────────────────

    private void OnDrawGizmos()
    {
        // Yellow = ready, red = broken
        Gizmos.color = _isBroken ? Color.red : Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.08f);
    }
}
