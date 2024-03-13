using System;

[Flags]
public enum GanreTags
{
    Action = 1 << 5,
    Adventure = 1 << 6,
    Arcade = 1 << 7,
    Casual = 1 << 8,
    Racing = 1 << 9,
    Simulation = 1 << 10,
    Strategy = 1 << 11,
    Puzzle = 1 << 12,
    Party = 1 << 13,

}
