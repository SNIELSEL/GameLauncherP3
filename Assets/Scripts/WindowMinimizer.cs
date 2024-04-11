using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowMinimizer : MonoBehaviour
{
    public string processName;
    public string gameProcces;

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    public void MinimizeLauncher()
    {
        ShowWindow(GetActiveWindow(), 2);

        Process[] processes = Process.GetProcessesByName(gameProcces);

        IntPtr gameWindowHandle = processes[0].MainWindowHandle;

        SetForegroundWindow(gameWindowHandle);

        GameObject.Find("Music").GetComponent<AudioSource>().enabled = false;
    }

    public void MaximizeLauncher()
    {
        Process[] processes = Process.GetProcessesByName(processName);

        IntPtr mainWindowHandle = processes[0].MainWindowHandle;

        ShowWindow(mainWindowHandle, 3);

        SetForegroundWindow(mainWindowHandle);

        GameObject.Find("Music").GetComponent<AudioSource>().enabled = true;
    }
}
