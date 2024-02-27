using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;

public class AppStart : MonoBehaviour
{
    [SerializeField] string link;

    //opens the link
    private void Start()
    {
        //rootPath = Directory.GetCurrentDirectory();
    }
    public void ClickLink()
    {
        Application.OpenURL(link);
    }

    public void StartGame1()
    {
        string path = Application.dataPath + "/../Games/4 musketiers/4MusketiersS2P4";
        Process.Start(path);
    }

    public void StartGame2()
    {
        string path = Application.dataPath + "/../Games/TheCleanQueen Build 5/TheCleanQueen";
        Process.Start(path);
    }
}
