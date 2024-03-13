using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAllInfoTabs : MonoBehaviour
{

    public GameObject parent;

    public List<GameObject> infoTabs;
    public void DisableAllTabs()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if(parent.transform.GetChild(i).name == "InfoTab(Clone)")
            {
                infoTabs.Add(parent.transform.GetChild(i).gameObject);
            }
        }

        infoTabs.RemoveAll(x => x == null);

        for (int i = 0; i < infoTabs.Count; i++)
        {
            infoTabs[i].SetActive(false);
        }
    }
}
