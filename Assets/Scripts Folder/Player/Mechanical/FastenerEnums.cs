// ─────────────────────────────────────────────────────────────────────────────
//  FastenerEnums.cs
//  Central enum definitions for the fastener / repair system.
//  Add new entries here as the game grows – all other scripts reference these.
// ─────────────────────────────────────────────────────────────────────────────

/// <summary>
/// The three job categories in the apartment building.
/// Every fastener belongs to exactly one JobType.
/// Every tool declares which JobTypes it is permitted to work on.
/// A tool is silently blocked from interacting with fasteners whose JobType
/// is not in the tool's allowedJobTypes list.
/// </summary>
public enum JobType
{
    Plumbing,   // Pipe repairs, water fixtures, valves
    Boiler,     // Boiler room maintenance, heating-unit fasteners
    Furnace     // Furnace panels, burner assembly, duct fasteners
}

/// <summary>
/// Physical fastener head types present in the game.
/// Each value maps to exactly one required tool (enforced in RepairTool).
///
///   HexBolt       → Wrench      (Plumbing jobs)
///   FlatHeadScrew → AllenKey    (Boiler jobs)
///   PhillipsScrew → Screwdriver (Furnace jobs; Screwdriver may also cover Boiler
///                                if configured with the overlap job type)
/// </summary>
public enum FastenerType
{
    HexBolt,        // Plumbing  – pipe fittings, valve flanges, bracket bolts
    FlatHeadScrew,  // Boiler    – panel screws, burner-mount flat-blade heads
    PhillipsScrew   // Furnace   – sheet-metal cross-head screws, duct fasteners
}

/// <summary>
/// The three tools the player can carry.
/// </summary>
public enum ToolType
{
    Wrench,         // Plumbing  – drives HexBolt fasteners
    AllenKey,       // Boiler    – drives FlatHeadScrew fasteners
    Screwdriver     // Furnace (primary) – drives PhillipsScrew fasteners
                    // May also be assigned to Boiler jobs via allowedJobTypes overlap
}
