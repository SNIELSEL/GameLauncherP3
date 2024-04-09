using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProcessSender : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI processpath;
    private string processName;

    private void Start()
    {
        processName = processpath.text;
        processName = processName.Replace(".exe", "");
        processName = processName.Substring(processName.IndexOf('/') + 1);

        //GameObject.Find("ScriptHolder").GetComponent<ProjectHolder>().gameNames.Add(processName);
    }

    public void SendProcessNameToChecker()
    {
        GameObject.Find("ScriptHolder").GetComponent<ProcessChecker>().scanningForProcesses = true;
        GameObject.Find("ScriptHolder").GetComponent<ProcessChecker>().currentRunningProcess = processName;
    }
}
