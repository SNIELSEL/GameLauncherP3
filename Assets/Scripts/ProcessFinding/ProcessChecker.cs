using System;
using System.Collections;
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

    [SerializeField] private ActiveGameDetection activeGameDetection;

    [NonSerialized] public bool scanningForProcesses;

    private bool processWasRunning;
    private bool isMinimized;
    private bool inputSetUpComplete;

    private float timer;

    private void Start()
    {
        window.processName = Process.GetCurrentProcess().ProcessName;
        StartCoroutine(SetupTime());
    }

    private void Update()
    {
        if (!isProccesRunning && gameObject.GetComponent<InputEmulationScript>().enabled == true)
        {
            gameObject.GetComponent<InputEmulationScript>().enabled = false;
        }
        else if(isProccesRunning && gameObject.GetComponent<InputEmulationScript>().enabled == false)
        {
            gameObject.GetComponent<InputEmulationScript>().enabled = true;
        }

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
                        activeGameDetection.runningProcessName = currentRunningProcess;

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
                        activeGameDetection.runningProcessName = "";

                        window.MaximizeLauncher();
                       
                        isMinimized = false;
                    }
                }
            }
        }
    }

    private IEnumerator SetupTime()
    {
        yield return new WaitForSeconds(1.5f);
        inputSetUpComplete = true;
    }
}
