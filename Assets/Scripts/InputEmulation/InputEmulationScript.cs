using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityRawInput;
using WindowsInput;
using WindowsInput.Native;
using Cursor = System.Windows.Forms.Cursor;

public class InputEmulationScript : MonoBehaviour
{
    private InputSimulator sim;
    private MouseSimulator mouseSim;

    //keys to output
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

    //keys to search for input
    private RawKey InputlJoystickUp;
    private RawKey InputlJoystickDown;
    private RawKey InputlJoystickLeft;
    private RawKey InputlJoystickRight;
    private RawKey InputlXButton;
    private RawKey InputlAButton;
    private RawKey InputlBlankRedButton;
    private RawKey InputlYButton;
    private RawKey InputlBlackButton;
    private RawKey InputlYellowButton;
    private RawKey InputrJoystickUp;
    private RawKey InputrJoystickDown;
    private RawKey InputrJoystickLeft;
    private RawKey InputrJoystickRight;
    private RawKey InputrXButton;
    private RawKey InputrAButton;
    private RawKey InputrBlankRedButton;
    private RawKey InputrYButton;
    private RawKey InputrBlackButton;
    private RawKey InputrYellowButton;
    private RawKey InputstartButton;
    private RawKey InputselectButton;
    private RawKey InputrSelectButton;
    private RawKey InputrStartButton;
    private RawKey InputlSideBlackButton;

    [SerializeField] string filePath, fileName;
    private string currentLine;
    private int currentLineIndex = 0;
    private int invisableI;

    //checks for if variables are set or if a event is triggerd
    private bool inputDelayIsActive;
    private bool useCustomInput;
    private bool usingPC;
    private bool usingPCInput;
    private bool usingRaspberryPiInput;
    private bool setInputKeys;
    private bool isHolding;

    private Cursor cursor;
    
    public int sens;

    public bool usingMouse;

    //Windows mouse event emulation

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

    private void OnApplicationQuit()
    {
        RawInput.WorkInBackground = false;
        RawInput.Stop();
        usingMouse = false;
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

    void Update()
    {
        if (inputDelayIsActive == true)
        {
            StartCoroutine(DelayTheInput());
        }

        if (isHolding)
        {
            if (!RawInput.IsKeyDown(InputlXButton))
            {
                isHolding = false;
                ButtonUp(MouseButtonConstants.vbLeftButton);
            }
            else if (RawInput.IsKeyDown(InputlXButton))
            {
                inputDelayIsActive = true;
                isHolding = true;
                ButtonDown(MouseButtonConstants.vbLeftButton);
            }
        }

        if (usingPC)
        {
            usingPCInput = true;
            usingRaspberryPiInput = false;
            InputListener();
        }
        else
        {
            usingPCInput = false;
            usingRaspberryPiInput = true;
            InputListener();
        }
    }
    private void MoveCursor(int xToMove, int yToMove)
    {
        // Set the Current cursor, move the cursor's Position,
        // and set its clipping rectangle to the form. 
        Cursor.Position = new Point(Cursor.Position.X + xToMove, Cursor.Position.Y + yToMove);
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
    IEnumerator DelayTheInput()
    {
        yield return new WaitForSeconds(0.25f);
        inputDelayIsActive = false;
    }

    /// <summary>
    /// PC Version of The Input Listener for Debugging Without a RaspberryPi
    /// </summary>

    private void InputListener()
    {


        //set the input that the script is searching for to either the windows commands or for RaspberryPi commands this ensures that the code is usable without RaspberryPi
        if (usingPCInput && !setInputKeys)
        {
            InputlJoystickUp = RawKey.Up;
            InputlJoystickDown = RawKey.Down;
            InputlJoystickLeft = RawKey.Left;
            InputlJoystickRight = RawKey.Right;
            InputlXButton = RawKey.X;
            InputlAButton = RawKey.Z;
            InputlBlankRedButton = RawKey.Shift;
            InputlYButton = RawKey.Space;
            InputlBlackButton = RawKey.LeftMenu;
            InputlYellowButton = RawKey.LeftControl;
            InputrJoystickUp = RawKey.R;
            InputrJoystickDown = RawKey.F;
            InputrJoystickLeft = RawKey.D;
            InputrJoystickRight = RawKey.G;
            InputrXButton = RawKey.W;
            InputrAButton = RawKey.I;
            InputrBlankRedButton = RawKey.K;
            InputrYButton = RawKey.A;
            InputrBlackButton = RawKey.S;
            InputrYellowButton = RawKey.Q;
            InputstartButton = RawKey.N1;
            InputselectButton = RawKey.Tab;
            InputrSelectButton = RawKey.L;
            InputrStartButton = RawKey.V;
            InputlSideBlackButton = RawKey.P;

            setInputKeys = true;
            usingPCInput = false;
        }
        else if (usingRaspberryPiInput && !setInputKeys)
        {
            InputlJoystickUp = RawKey.F13;
            InputlJoystickDown = RawKey.F14;
            InputlJoystickLeft = RawKey.F15;
            InputlJoystickRight = RawKey.F16;
            InputlXButton = RawKey.F17;
            InputlAButton = RawKey.F18;
            InputlBlankRedButton = RawKey.F19;
            InputlYButton = RawKey.F20;
            InputlBlackButton = RawKey.F21;
            InputlYellowButton = RawKey.F22;
            InputrJoystickUp = RawKey.F23;
            InputrJoystickDown = RawKey.F24;
            InputrJoystickLeft = RawKey.OEMComma;
            InputrJoystickRight = RawKey.OEMPeriod;
            InputrXButton = RawKey.OEM2;
            InputrAButton = RawKey.OEM1;
            InputrBlankRedButton = RawKey.OEM7;
            InputrYButton = RawKey.OEM4;
            InputrBlackButton = RawKey.OEM6;
            InputrYellowButton = RawKey.OEM5;
            InputstartButton = RawKey.OEMPlus;
            InputselectButton = RawKey.OEMMinus;
            InputrSelectButton = RawKey.OEM3;
            InputrStartButton = RawKey.Noname;
            InputlSideBlackButton = RawKey.Kana;

            setInputKeys = true;
            usingRaspberryPiInput = false;
        }

        if (RawInput.IsKeyDown(InputlJoystickUp) && inputDelayIsActive == false)
        {
            if (!usingMouse)
            {
                inputDelayIsActive = true;
                sim.Keyboard.KeyPress(lJoystickUp);
            }
            else
            {
                MoveCursor(0, -sens);
            }
        }

        if (RawInput.IsKeyDown(InputlJoystickDown) && inputDelayIsActive == false)
        {
            if (!usingMouse)
            {
                inputDelayIsActive = true;
                sim.Keyboard.KeyPress(lJoystickDown);
            }
            else
            {
                MoveCursor(0, sens);
            }
        }

        if (RawInput.IsKeyDown(InputlJoystickLeft) && inputDelayIsActive == false)
        {
            if (!usingMouse)
            {
                inputDelayIsActive = true;
                sim.Keyboard.KeyPress(lJoystickLeft);
            }
            else
            {
                MoveCursor(-sens, 0);
            }
        }

        if (RawInput.IsKeyDown(InputlJoystickRight) && inputDelayIsActive == false)
        {
            if (!usingMouse)
            {
                inputDelayIsActive = true;
                sim.Keyboard.KeyPress(lJoystickRight);
            }
            else
            {
                MoveCursor(sens, 0);
            }
        }

        if (RawInput.IsKeyDown(InputlXButton) && inputDelayIsActive == false)
        {
            if (!usingMouse)
            {
                inputDelayIsActive = true;
                sim.Keyboard.KeyPress(lXButton);
            }
            else
            {
                if (!RawInput.IsKeyDown(InputlXButton))
                {
                    Point defPnt = new Point();
                    GetCursorPos(ref defPnt);

                    LeftClick(defPnt.X, defPnt.Y);
                }
                else
                {
                    isHolding = true;
                }
            }
        }


        if (RawInput.IsKeyDown(InputlAButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(lAButton);
        }


        if (RawInput.IsKeyDown(InputlBlankRedButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(lBlankRedButton);
        }

        if (RawInput.IsKeyDown(InputlYButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(lYButton);
        }

        if (RawInput.IsKeyDown(InputlBlackButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(lBlackButton);
        }

        if (RawInput.IsKeyDown(InputlYellowButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(lYellowButton);
        }

        if (RawInput.IsKeyDown(InputrJoystickUp) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rJoystickUp);
        }

        if (RawInput.IsKeyDown(InputrJoystickDown) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rJoystickDown);
        }

        if (RawInput.IsKeyDown(InputrJoystickLeft) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rJoystickLeft);
        }

        if (RawInput.IsKeyDown(InputrJoystickRight) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rJoystickRight);
        }

        if (RawInput.IsKeyDown(InputrXButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rXButton);
        }

        if (RawInput.IsKeyDown(InputrAButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rAButton);
        }

        if (RawInput.IsKeyDown(InputrBlankRedButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rBlankRedButton);
        }

        if (RawInput.IsKeyDown(InputrYButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rYButton);
        }

        if (RawInput.IsKeyDown(InputrBlackButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rBlackButton);
        }

        if (RawInput.IsKeyDown(InputrYellowButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rYellowButton);
        }

        if (RawInput.IsKeyDown(InputstartButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(startButton);
        }

        if (RawInput.IsKeyDown(InputselectButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(selectButton);
        }

        if (RawInput.IsKeyDown(InputrStartButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rStartButton);
        }

        if (RawInput.IsKeyDown(InputrSelectButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;
            sim.Keyboard.KeyPress(rSelectButton);
        }

        if (RawInput.IsKeyDown(InputlSideBlackButton) && inputDelayIsActive == false)
        {
            inputDelayIsActive = true;

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
