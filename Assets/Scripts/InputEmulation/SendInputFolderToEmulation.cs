using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SendInputFolderToEmulation : MonoBehaviour
{
    public void SendFolderToEmulator(TextMeshProUGUI folderpath)
    {
        StartCoroutine(WaitForSettingInput());

        GameObject.Find("ScriptHolder").GetComponent<InputEmulationScript>().enabled = true;

        GameObject.Find("ScriptHolder").GetComponent<InputEmulationScript>().filePath = folderpath.text;
    }

    public IEnumerator WaitForSettingInput()
    {
        yield return new WaitForSeconds(2);

        GameObject.Find("ScriptHolder").GetComponent<InputEmulationScript>().SetKeyBinds();
    }
}
