using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class StartUpTes : MonoBehaviour
{
    private bool onCoolDown;

    //Starts the selected game(.EXE file).
    public void OnButtonClick(TextMeshProUGUI exeFolderPath)
    {
        if (!onCoolDown)
        {
            onCoolDown = true;
            Process.Start(exeFolderPath.text);
            StartCoroutine(GameCoolDown());
        }
    }

    public IEnumerator GameCoolDown()
    {
        yield return new WaitForSeconds(5);
        onCoolDown = false;
    }
}
