using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.ComponentModel;
using System;

public class ActiveGameDetection : MonoBehaviour
{
    private Process currentProcess;
    private Process[] allProcesses;
    private Process[] processesByName;

    [Header("Processes")]
    public string launcherProccesName;
    public string runningProcessName;
    [SerializeField] string[] allProcessWithGameName;
    //[SerializeField] string[] allProccesName;

    [Header ("PlayTime")]
    [SerializeField] bool countingPlayTime;
    [SerializeField] bool FirstCheck;


    [SerializeField] int checkForProccesInterval;
    [SerializeField] int currentGamePlaytimeMinutes;
    [SerializeField] int currentGamePlaytimeHours;

    private int gameTime;

    [SerializeField] float timeTillSavingPlaytime;
    [SerializeField] string playtime;

    [SerializeField] GameObject gameButtonparent;

    private void Start()
    {
        StartCoroutine(BindToRunningProcesses());
        StartCoroutine(IsProcessRunning());
    }

    private void Update()
    {
        if (timeTillSavingPlaytime <= 0)
        {
            timeTillSavingPlaytime = 60;
            currentGamePlaytimeMinutes++;
        }

        if (currentGamePlaytimeMinutes >= 60)
        {
            currentGamePlaytimeMinutes = 0;
            currentGamePlaytimeHours++;
        }

        if (countingPlayTime)
        {
            timeTillSavingPlaytime -= Time.deltaTime;
        }
    }

    IEnumerator IsProcessRunning()
    {
        if (allProcessWithGameName.Length == 0)
        {
            countingPlayTime = false;
            timeTillSavingPlaytime = 60;

            playtime = null;
            FirstCheck = false;
        }
        else
        {
            if(FirstCheck == false)
            {
                FirstCheck = true;

                currentGamePlaytimeMinutes = PlayerPrefs.GetInt("PlayTimeMinutes" + runningProcessName);
                currentGamePlaytimeHours = PlayerPrefs.GetInt("PlayTimeHours" + runningProcessName);
            }

            countingPlayTime = true;

            playtime = currentGamePlaytimeHours + "." + Mathf.RoundToInt(currentGamePlaytimeMinutes / 6f);

            PlayerPrefs.SetInt("PlayTimeMinutes" + runningProcessName, currentGamePlaytimeMinutes);
            PlayerPrefs.SetInt("PlayTimeHours" + runningProcessName, currentGamePlaytimeHours);

            gameTime = (PlayerPrefs.GetInt("PlayTimeMinutes" + runningProcessName) + (PlayerPrefs.GetInt("PlayTimeHours" + runningProcessName) * 60)); ;

            for (int i = 0; i < gameButtonparent.transform.childCount; i++)
            {
                if (gameButtonparent.transform.GetChild(i).GetComponent<RequirmentCheck>().gameName == runningProcessName)
                {
                    gameButtonparent.transform.GetChild(i).GetComponent<Product>().filter.SetGameTime(gameTime);
                }
            }
        }

        yield return new WaitForSeconds(checkForProccesInterval);


        StartCoroutine(IsProcessRunning());
    }

    IEnumerator BindToRunningProcesses()
    {
        // pak de procces van de game
        currentProcess = Process.GetCurrentProcess();

        launcherProccesName = currentProcess.ProcessName;


        // pak alle dingen die runnen op je pc
        //allProcesses = Process.GetProcesses();
        //allProccesName = new string[allProcesses.Length];
        /* for (int i = 0; i < allProcesses.Length; i++)
        {
            allProccesName[i] = allProcesses[i].ProcessName;
        }*/


        //pak alle processes met een bepaalde naam
        processesByName = Process.GetProcessesByName(runningProcessName);

        allProcessWithGameName = new string[processesByName.Length];

        for (int i = 0; i < processesByName.Length; i++)
        {
            allProcessWithGameName[i] = processesByName[i].ProcessName;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(BindToRunningProcesses());
    }
}
