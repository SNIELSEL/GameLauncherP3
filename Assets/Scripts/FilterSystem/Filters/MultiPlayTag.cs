using System;

[Flags]
public enum MultiPlayTag
{
    SinglePlayer = 1 << 0,
    MultiPlayer = 1 << 1,

}