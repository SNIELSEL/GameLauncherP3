using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecentlyPlayedDataTransfer : MonoBehaviour
{
    public void SendButtonToSaver()
    {
        GameObject.Find("ScriptHolder").GetComponent<RecentlyPlayedSaver>().recentlyPlayedButton3 = GameObject.Find("ScriptHolder").GetComponent<RecentlyPlayedSaver>().recentlyPlayedButton2;

        GameObject.Find("ScriptHolder").GetComponent<RecentlyPlayedSaver>().recentlyPlayedButton2 = GameObject.Find("ScriptHolder").GetComponent<RecentlyPlayedSaver>().recentlyPlayedButton1;

        GameObject.Find("ScriptHolder").GetComponent<RecentlyPlayedSaver>().recentlyPlayedButton1 = GetComponent<RawImage>();

        GameObject.Find("ScriptHolder").GetComponent<RecentlyPlayedSaver>().SetImages();
    }
}
