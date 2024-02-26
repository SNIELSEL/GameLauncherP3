using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Rendering.LookDev;
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
       //Process.Start(Environment.CurrentDirectory + "/GameLauncherTest.4Musketiers/4Musketiers,exe");
    }

    public void StartGame2()
    {

    }
}
