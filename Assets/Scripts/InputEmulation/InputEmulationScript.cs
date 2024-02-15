using UnityEditor;
using UnityEngine;
using WindowsInput.Native;
using WindowsInput;
using System.IO;
using Unity.VisualScripting;

public class InputEmulationScript : MonoBehaviour
{
    InputSimulator sim;
    [SerializeField] VirtualKeyCode[] ArcadeInputs;
    [SerializeField] string[] InputNames;
    [SerializeField] string filePath, fileName;
    private int currentLineIndex = 0;

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
        if (Input.GetMouseButton(0))
        {
            sim.Keyboard.KeyPress(VirtualKeyCode.LWIN);
        }
    }
}
