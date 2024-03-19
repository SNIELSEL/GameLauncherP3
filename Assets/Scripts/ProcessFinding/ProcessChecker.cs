using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using UnityEngine;
using Application = UnityEngine.Application;
using SysApp = System.Windows.Forms.Application;
public class ProcessChecker : MonoBehaviour
{
    public string currentRunningProcess;

    [SerializeField] private GameObject parent;

    [SerializeField] private bool isProccesRunning;

    [SerializeField] private WindowMinimizer window;

    [NonSerialized] public bool scanningForProcesses;

    private bool processWasRunning;
    private bool isMinimized;

    private float timer;

    private void Update()
    {
        if (scanningForProcesses)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 1;

                isProccesRunning = Process.GetProcessesByName(currentRunningProcess).Any();

                if (isProccesRunning)
                {
                    processWasRunning = true;

                    if (!isMinimized)
                    {
                        window.MinimizeLauncher();

                        isMinimized = true;
                    }
                }

                if (!isProccesRunning && processWasRunning)
                {
                    scanningForProcesses = false;
                    timer = 1;
                    currentRunningProcess = null;

                    if (isMinimized)
                    {
                        window.MaximizeLauncher();
                       
                        isMinimized = false;
                    }
                }
            }
        }
    }
}
