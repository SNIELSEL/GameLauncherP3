using System.Diagnostics;
using TMPro;
using UnityEngine;

public class StartUpTes : MonoBehaviour
{
    public void OnButtonClick(TextMeshProUGUI exeFolderPath)
    {
        Process.Start(exeFolderPath.text);
    }
}
