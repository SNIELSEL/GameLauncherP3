using System.Collections;
using System.IO;
using UnityEngine;
using UnityRawInput;
using WindowsInput;
using WindowsInput.Native;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
using Object = System.Object;
using Cursor = System.Windows.Forms.Cursor;
using System.Drawing;

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
    private int invisableI;
    public int sens;

    private bool test;
    private bool useCustomInput;
    private bool usingMouse;
    private bool usingPC;
    private bool isHolding;

    private Cursor cursor;

    //Emulation spull

    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
    private const uint MOUSEEVENTF_LEFTUP = 0x04;
    private const uint MOUSEEVENTF_MIDDLEDOWN = 0x20;
    private const uint MOUSEEVENTF_MIDDLEUP = 0x40;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const uint MOUSEEVENTF_RIGHTUP = 0x10;

    public enum MouseButtonConstants
    {
        vbLeftButton = 1,
        vbMiddleButton = 4,
        vbRightButton = 2
    }

    public static void ButtonDown(MouseButtonConstants Button = MouseButtonConstants.vbLeftButton)
    {
        uint flag = 0;
        if (Button == MouseButtonConstants.vbLeftButton)
        {
            flag = MOUSEEVENTF_LEFTDOWN;
        }
        else if (Button == MouseButtonConstants.vbMiddleButton)
        {
            flag = MOUSEEVENTF_MIDDLEDOWN;
        }
        else if (Button == MouseButtonConstants.vbRightButton)
        {
            flag = MOUSEEVENTF_RIGHTDOWN;
        }
        mouse_event(flag, 0, 0, 0, UIntPtr.Zero);
    }

    public static void ButtonUp(MouseButtonConstants Button = MouseButtonConstants.vbLeftButton)
    {
        uint flag = 0;
        if (Button == MouseButtonConstants.vbLeftButton)
        {
            flag = MOUSEEVENTF_LEFTUP;
        }
        else if (Button == MouseButtonConstants.vbMiddleButton)
        {
            flag = MOUSEEVENTF_MIDDLEUP;
        }
        else if (Button == MouseButtonConstants.vbRightButton)
        {
            flag = MOUSEEVENTF_RIGHTUP;
        }
        mouse_event(flag, 0, 0, 0, UIntPtr.Zero);
    }

    public static void ButtonClick(MouseButtonConstants Button = MouseButtonConstants.vbLeftButton)
    {
        ButtonDown(Button);
        ButtonUp(Button);
    }

    public static void ButtonDblClick(MouseButtonConstants Button = MouseButtonConstants.vbLeftButton)
    {
        ButtonClick(Button);
        ButtonClick(Button);
    }
    public static System.Drawing.Point Position { get; set; }

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(ref Point lpPoint);

    [DllImport("user32.dll")]

    static extern void mouse_event(int dwFlags, int dx, int dy,
                      int dwData, int dwExtraInfo);

    [Flags]
    public enum MouseEventFlags
    {
        LEFTDOWN = 0x00000002,
        LEFTUP = 0x00000004,
        MIDDLEDOWN = 0x00000020,
        MIDDLEUP = 0x00000040,
        MOVE = 0x00000001,
        ABSOLUTE = 0x00008000,
        RIGHTDOWN = 0x00000008,
        RIGHTUP = 0x00000010
    }

    public static void LeftClick(int x, int y)
    {
        Cursor.Position = new System.Drawing.Point(x, y);
        mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
        mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
    }

    //private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

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
        this.cursor = new Cursor(Cursor.Current.Handle);

        string[] lines = File.ReadAllLines(filePath);

        sim = new InputSimulator();

        //filePath = Application.dataPath + fileName;

        for (invisableI = 0; invisableI < lines.Length; invisableI++)
        {
            currentLine = lines[invisableI];

            if (currentLine == "UseInputTranslation:")
            {
                if(GetLineAtIndex(invisableI + 1) == "Yes")
                {
                    useCustomInput = true;
                }
                else if (GetLineAtIndex(invisableI + 1) == "No")
                {
                    useCustomInput = false;
                }
                else if (GetLineAtIndex(invisableI + 1) == "PC")
                {
                    useCustomInput = true;
                    usingPC = true;
                }
            }

            if(useCustomInput == true)
            {
                checkForinputNames();
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

    public void checkForinputNames()
    {
        if (currentLine == "Left joystick Up:")
        {
            lJoystickUp = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Left joystick Down:")
        {
            lJoystickDown = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Left joystick Left:")
        {
            lJoystickLeft = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Left joystick Right:")
        {
            lJoystickRight = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "X Button:")
        {
            lXButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "A Button:")
        {
            lAButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Blank Red Button:")
        {
            lBlankRedButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Y Button:")
        {
            lYButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Black Button:")
        {
            lBlackButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Yellow Button:")
        {
            lYellowButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right joystick Up:")
        {
            rJoystickUp = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right joystick Down:")
        {
            rJoystickDown = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right joystick Left:")
        {
            rJoystickLeft = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right joystick Right:")
        {
            rJoystickRight = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right X Button:")
        {
            rXButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right A Button:")
        {
            rAButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right Red Button(WithoutLetter):")
        {
            rBlankRedButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right Y Button:")
        {
            rYButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right Black Button:")
        {
            rBlackButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Right Yellow Button:")
        {
            rYellowButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }

        if (currentLine == "Start Button:")
        {
            startButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }
        if (currentLine == "Select button:")
        {
            selectButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }
        if (currentLine == "Right Side Start:")
        {
            rStartButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }
        if (currentLine == "Right Side Select:")
        {
            rSelectButton = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(invisableI + 1));
        }
    }

    void Update()
    {
        if (test == true)
        {
            StartCoroutine(ResetTestBool());
        }

        if (!usingPC)
        {
            InputListener();
        }
        else
        {
            InputListenerPC();
        }
    }
    private void MoveCursor(int xToMove, int yToMove)
    {
        // Set the Current cursor, move the cursor's Position,
        // and set its clipping rectangle to the form. 
        Cursor.Position = new Point(Cursor.Position.X + xToMove, Cursor.Position.Y + yToMove);
    }
    IEnumerator ResetTestBool()
    {
        yield return new WaitForSeconds(0.25f);
        test = false;
    }

    private void InputListener()
    {
        if (RawInput.IsKeyDown(RawKey.OEMComma) && !RawInput.IsKeyDown(RawKey.LeftShift) &&test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lJoystickUp);
            }
            else
            {
                MoveCursor(0, -sens);
            }
        }

        if (RawInput.IsKeyDown(RawKey.OEMComma) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lJoystickDown);
            }
            else
            {
                MoveCursor(0, sens);
            }
        }

        if (RawInput.IsKeyDown(RawKey.OEMPeriod) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lJoystickLeft);
            }
            else
            {
                MoveCursor(-sens, 0);
            }
        }

        if (RawInput.IsKeyDown(RawKey.OEMPeriod) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lJoystickRight);
            }
            else
            {
                MoveCursor(sens, 0);
            }
        }

        if (RawInput.IsKeyDown(RawKey.OEM2) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lXButton);
            }
            else
            {
                Point defPnt = new Point();
                GetCursorPos(ref defPnt);

                LeftClick(defPnt.X, defPnt.Y);
            }
        }


        if (RawInput.IsKeyDown(RawKey.OEM2) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lAButton);
        }


        if (RawInput.IsKeyDown(RawKey.OEM1) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lBlankRedButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEM1) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lYButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEM7) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lBlackButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEM7) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lYellowButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEM4) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rJoystickUp);
        }

        if (RawInput.IsKeyDown(RawKey.OEM4) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rJoystickDown);
        }

        if (RawInput.IsKeyDown(RawKey.OEM6) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rJoystickLeft);
        }

        if (RawInput.IsKeyDown(RawKey.OEM6) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rJoystickRight);
        }

        if (RawInput.IsKeyDown(RawKey.OEM5) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rXButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEM5) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rAButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEMPlus) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rBlankRedButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEMPlus) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rYButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEMMinus) && !RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rBlackButton);
        }

        if (RawInput.IsKeyDown(RawKey.OEMMinus) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rYellowButton);
        }

        if (RawInput.IsKeyDown(RawKey.N0) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(startButton);
        }

        if (RawInput.IsKeyDown(RawKey.N9) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(selectButton);
        }

        if (RawInput.IsKeyDown(RawKey.N8) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rStartButton);
        }

        if (RawInput.IsKeyDown(RawKey.N7) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rSelectButton);
        }

        if (RawInput.IsKeyDown(RawKey.N6) && RawInput.IsKeyDown(RawKey.LeftShift) && test == false)
        {
            test = true;

            if(usingMouse == false)
            {
                usingMouse = true;
            }
            else
            {
                usingMouse = false;
            }
        }
    }



    /// <summary>
    /// PC Version of The Input Listener for Debugging Without a RaspberryPi
    /// </summary>

    private void InputListenerPC()
    {
        if (RawInput.IsKeyDown(RawKey.Up) && test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lJoystickUp);
            }
            else
            {
                MoveCursor(0, -sens);
            }
        }

        if (RawInput.IsKeyDown(RawKey.Down) && test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lJoystickDown);
            }
            else
            {
                MoveCursor(0, sens);
            }
        }

        if (RawInput.IsKeyDown(RawKey.Left) && test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lJoystickLeft);
            }
            else
            {
                MoveCursor(-sens, 0);
            }
        }

        if (RawInput.IsKeyDown(RawKey.Right) && test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lJoystickRight);
            }
            else
            {
                MoveCursor(sens, 0);
            }
        }

        if (RawInput.IsKeyDown(RawKey.X) && test == false)
        {
            if (!usingMouse)
            {
                test = true;
                sim.Keyboard.KeyPress(lXButton);
            }
            else
            {
                Point defPnt = new Point();
                GetCursorPos(ref defPnt);

                //LeftClick(defPnt.X, defPnt.Y);

                if (!isHolding)
                {
                    test = true;
                    isHolding = true;
                    ButtonDown(MouseButtonConstants.vbLeftButton);
                }
                else
                {
                    test = true;
                    isHolding = false;
                    ButtonUp(MouseButtonConstants.vbLeftButton);
                }
            }
        }


        if (RawInput.IsKeyDown(RawKey.Z) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lAButton);
        }


        if (RawInput.IsKeyDown(RawKey.Shift) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lBlankRedButton);
        }

        if (RawInput.IsKeyDown(RawKey.Space) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lYButton);
        }

        if (RawInput.IsKeyDown(RawKey.LeftMenu) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lBlackButton);
        }

        if (RawInput.IsKeyDown(RawKey.LeftControl) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(lYellowButton);
        }

        if (RawInput.IsKeyDown(RawKey.R) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rJoystickUp);
        }

        if (RawInput.IsKeyDown(RawKey.F) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rJoystickDown);
        }

        if (RawInput.IsKeyDown(RawKey.D) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rJoystickLeft);
        }

        if (RawInput.IsKeyDown(RawKey.G) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rJoystickRight);
        }

        if (RawInput.IsKeyDown(RawKey.W) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rXButton);
        }

        if (RawInput.IsKeyDown(RawKey.I) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rAButton);
        }

        if (RawInput.IsKeyDown(RawKey.K) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rBlankRedButton);
        }

        if (RawInput.IsKeyDown(RawKey.A) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rYButton);
        }

        if (RawInput.IsKeyDown(RawKey.S) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rBlackButton);
        }

        if (RawInput.IsKeyDown(RawKey.Q) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rYellowButton);
        }

        if (RawInput.IsKeyDown(RawKey.N1) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(startButton);
        }

        if (RawInput.IsKeyDown(RawKey.Tab) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(selectButton);
        }

        if (RawInput.IsKeyDown(RawKey.V) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rStartButton);
        }

        if (RawInput.IsKeyDown(RawKey.L) && test == false)
        {
            test = true;
            sim.Keyboard.KeyPress(rSelectButton);
        }

        if (RawInput.IsKeyDown(RawKey.P) && test == false)
        {
            test = true;

            if (usingMouse == false)
            {
                usingMouse = true;
            }
            else
            {
                usingMouse = false;
            }
        }
    }
}
