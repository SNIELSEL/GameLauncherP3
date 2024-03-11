using System.Collections;
using System.IO;
using UnityEngine;
using UnityRawInput;
using WindowsInput;
using WindowsInput.Native;
using System.Runtime.InteropServices;
using System;

public class InputEmulationScript : MonoBehaviour
{
    InputSimulator sim;
    MouseSimulator mouseSim;

    [SerializeField] VirtualKeyCode lJoystickUp;
    [SerializeField] VirtualKeyCode lJoystickDown;
    [SerializeField] VirtualKeyCode lJoystickLeft;
    [SerializeField] VirtualKeyCode lJoystickRight;
    [SerializeField] VirtualKeyCode lXButton;
    [SerializeField] VirtualKeyCode lAButton;
    [SerializeField] VirtualKeyCode lBlankRedButton;
    [SerializeField] VirtualKeyCode lYButton;
    [SerializeField] VirtualKeyCode lBlackButton;
    [SerializeField] VirtualKeyCode lYellowButton;
    [SerializeField] VirtualKeyCode rJoystickUp;
    [SerializeField] VirtualKeyCode rJoystickDown;
    [SerializeField] VirtualKeyCode rJoystickLeft;
    [SerializeField] VirtualKeyCode rJoystickRight;
    [SerializeField] VirtualKeyCode rXButton;
    [SerializeField] VirtualKeyCode rAButton;
    [SerializeField] VirtualKeyCode rBlankRedButton;
    [SerializeField] VirtualKeyCode rYButton;
    [SerializeField] VirtualKeyCode rBlackButton;
    [SerializeField] VirtualKeyCode rYellowButton;
    [SerializeField] VirtualKeyCode startButton;
    [SerializeField] VirtualKeyCode selectButton;
    [SerializeField] VirtualKeyCode rSelectButton;
    [SerializeField] VirtualKeyCode rStartButton;
    [SerializeField] VirtualKeyCode lSideBlackButton;

    [SerializeField] string filePath, fileName;
    private string currentLine;
    private int currentLineIndex = 0;

    private bool test;

    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    private const uint MOUSEEVENTF_WHEEL = 0x0800;

    public static void SimulateScroll(int delta)
    {
        // delta is the amount of scrolling, positive for scrolling up, negative for scrolling down
        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (uint)delta, UIntPtr.Zero);
    }

    private void OnDisable()
    {
        RawInput.WorkInBackground = false;
        RawInput.Stop();
    }

    private void OnEnable()
    {
        RawInput.Start();
        RawInput.WorkInBackground = true;
    }

    void Start()
    {
        string[] lines = File.ReadAllLines(filePath);

        sim = new InputSimulator();

        //filePath = Application.dataPath + fileName;

        for (int i = 0; i < lines.Length; i++)
        {
            currentLine = lines[i];

            if(currentLine == "Left joystick Up:")
            {
                lJoystickUp = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Left joystick Down:")
            {
                lJoystickDown = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Left joystick Left:")
            {
                lJoystickLeft = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Left joystick Right:")
            {
                lJoystickRight = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "X Button:")
            {
                lXButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "A Button:")
            {
                lAButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Blank Red Button:")
            {
                lBlankRedButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Y Button:")
            {
                lYButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Black Button:")
            {
                lBlackButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Yellow Button:")
            {
                lYellowButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right joystick Up:")
            {
                rJoystickUp = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right joystick Down:")
            {
                rJoystickDown = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right joystick Left:")
            {
                rJoystickLeft = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right joystick Right:")
            {
                rJoystickRight = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right X Button:")
            {
                rXButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right A Button:")
            {
                rAButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right Red Button(WithoutLetter):")
            {
                rBlankRedButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right Y Button:")
            {
                rYButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right Black Button:")
            {
                rBlackButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Right Yellow Button:")
            {
                rYellowButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }

            if (currentLine == "Start Button:")
            {
                startButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }
            if (currentLine == "Select button:")
            {
                selectButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }
            if (currentLine == "Right Side Start:")
            {
                rStartButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }
            if (currentLine == "Right Side Select:")
            {
                rSelectButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }
            if (currentLine == "Left Side Black Button:")
            {
                lSideBlackButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i + 1));
            }
        }
    }

    private string GetLineAtIndex(int index)
    {
        string[] lines = File.ReadAllLines(filePath);

        if (index < lines.Length)
        {
            return lines[index];
        }
        else
        {
            return "No Lines";
        }
    }

    void Update()
    {
        if (test == true)
        {
            StartCoroutine(ResetTestBool());
        }

        InputListener();
    }

    IEnumerator ResetTestBool()
    {
        yield return new WaitForSeconds(0.15f);
        test = false;
    }

    private void InputListener()
    {
        if (RawInput.IsKeyDown(RawKey.OEMComma) && !RawInput.IsKeyDown(RawKey.LeftShift) &&test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lJoystickUp);
        }

        if (RawInput.IsKeyDown(RawKey.OEMComma) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lJoystickDown);
        }

        if (RawInput.IsKeyDown(RawKey.OEMPeriod) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lJoystickLeft);
        }

        if (RawInput.IsKeyDown(RawKey.OEMPeriod) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lJoystickRight);
        }

        if (RawInput.IsKeyDown(RawKey.OEM2) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lXButton);
        }


        if (RawInput.IsKeyDown(RawKey.OEM2) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lAButton);
        }


        if (RawInput.IsKeyDown(RawKey.OEM1) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lBlankRedButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEM1) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lYButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEM7) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lBlackButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEM7) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(lYellowButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEM4) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(rJoystickUp);
        }

        if (RawInput.IsKeyDown(RawKey.OEM4) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyDown(rJoystickDown);
        }

    }
}
