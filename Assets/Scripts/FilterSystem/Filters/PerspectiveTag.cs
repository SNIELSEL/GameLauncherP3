using System;

[Flags]
public enum PerspectiveTag
{
    FirstPerson = 1 << 0,
    ThirdPerson = 1 << 1,
    TopDown = 1 << 2,
}