using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput.Native;
using WindowsInput;

public class InputEmulationScript : MonoBehaviour
{
    InputSimulator sim;
    public void Start()
    {
        sim = new InputSimulator();
    }

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.LWIN);
        }
    }
}
