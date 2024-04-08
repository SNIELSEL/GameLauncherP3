using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToInfoTab : MonoBehaviour
{
    public GameObject buttonInfoTab;


    //Sets the Info Tab Active
    public void SetInfoTabActive()
    {
        buttonInfoTab.SetActive(true);
        Debug.Log("working");
    }
}
