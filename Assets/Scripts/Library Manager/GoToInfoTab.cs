using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToInfoTab : MonoBehaviour
{
    public GameObject buttonInfoTab;

    public void SetInfoTabActive()
    {
        buttonInfoTab.SetActive(true);
    }
}
