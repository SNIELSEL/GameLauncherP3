using System;

[Flags]
public enum GanreTags
{
    Action = 1 << 0,
    Adventure = 1 << 1,
    Arcade = 1 << 2,
    Casual = 1 << 3,
    Racing = 1 << 4,
    Simulation = 1 << 5,
    Strategy = 1 << 6,
    Puzzle = 1 << 7,
    Party = 1 << 8,

}
