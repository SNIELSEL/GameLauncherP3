using System.Diagnostics;
using TMPro;
using UnityEngine;

public class StartUpTes : MonoBehaviour
{
    //Starts the selected game(.EXE file).
    public void OnButtonClick(TextMeshProUGUI exeFolderPath)
    {
        Process.Start(exeFolderPath.text);
    }
}
