using UnityEditor;
using UnityEngine;
using WindowsInput.Native;
using WindowsInput;
using System.IO;
using Unity.VisualScripting;
using UnityRawInput;
using System.Collections;

public class InputEmulationScript : MonoBehaviour
{
    InputSimulator sim;
    [SerializeField] VirtualKeyCode[] ArcadeInputs;
    [SerializeField] string[] InputNames;
    [SerializeField] string filePath, fileName;
    private int currentLineIndex = 0;

    private bool test;

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
        sim = new InputSimulator();

        filePath = Application.dataPath + fileName;

        for (int i = 0; i < ArcadeInputs.Length; i++)
        {
            InputNames[i] = GetLineAtIndex(i);
            ArcadeInputs[i] = (VirtualKeyCode)System.Enum.Parse(typeof(VirtualKeyCode), GetLineAtIndex(i));
        }
    }

    private string GetLineAtIndex(int index)
    {
        string[] lines = File.ReadAllLines(filePath);

        if(index < lines.Length)
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
        if(test == true)
        {
            StartCoroutine(ResetTestBool());
        }

        if (RawInput.IsKeyDown(RawKey.MiddleButton) && test == false) 
        {
            test = true;
            sim.Keyboard.KeyPress(VirtualKeyCode.LWIN);
        }

        if (RawInput.IsKeyDown(RawKey.Escape) && test == false)
        {
            Application.Quit();
        }
    }

    IEnumerator ResetTestBool()
    {
        yield return new WaitForSeconds(0.15f);
        test = false;
    }
}
