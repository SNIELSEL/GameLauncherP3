using System;

[Flags]
public enum StudentYear
{
    Year1 = 1 << 0,
    Year2 = 1 << 1, 
    Year3 = 1 << 2, 
    Year4 = 1 << 3,
    Teacher =1 << 4,
}
