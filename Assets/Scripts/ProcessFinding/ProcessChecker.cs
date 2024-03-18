using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using System.Diagnostics;
using System.Linq;

public class ProcessChecker : MonoBehaviour
{
    [SerializeField] private string[] processNames;

    [SerializeField] private int[] processIDs;

    [SerializeField] private Process[] processes;

    [SerializeField] private List<GameObject> gameFolders;

    [SerializeField] private GameObject parent;

    [SerializeField] private bool isProccesRunning;

    private float timer;
    private void Start()
    {
        StartCoroutine(WaitForGames());
    }

    private IEnumerator WaitForGames()
    {
        yield return new WaitForSeconds(4);
        GetAllExePaths();
    }

    public void GetAllExePaths()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).name == "InfoTab(Clone)")
            {
                gameFolders.Add(parent.transform.GetChild(i).gameObject);
            }
        }

        gameFolders.RemoveAll(x => x == null);

        processNames = new string[gameFolders.Count];

        for (int i = 0; i < gameFolders.Count; i++)
        {
            processNames[i] = gameFolders[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;

            processNames[i] = processNames[i].Replace(".exe", "");
            processNames[i] = processNames[i].Substring(processNames[i].IndexOf('/') + 1);
        }

        processes = new Process[processNames.Length];

        for (int i = 0; i < processNames.Length; i++)
        {
            processes[i] = Process.GetProcessesByName(processNames[i]).FirstOrDefault<Process>();
        }

        processIDs = new int[processNames.Length];

        for (int i = 0; i < processNames.Length; i++)
        {
            //processIDs[i] = processes[i].Id;
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 1;

            for (int i = 0; i < processNames.Length; i++)
            {
                isProccesRunning = Process.GetProcessesByName(processNames[i]).Any();
            }
        }
    }
}
