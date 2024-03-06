using System.Collections;
using System.Collections.Generic;
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
