using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTab : MonoBehaviour
{
    public DisableAllInfoTabs disableAllInfoTabs;

    public void DisableTab()
    {
        disableAllInfoTabs = GameObject.Find("ScriptHolder").GetComponent<DisableAllInfoTabs>();

        disableAllInfoTabs.DisableAllTabs();
    }
}
