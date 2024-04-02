using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;


public class AdminPopup : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject selectedGameobject;


    public void AdminButton()
    {
        if (!popup.activeSelf)
        {
            OpenAdmin();
        }
        else
        {
            CloseAdmin();
        }

    }

    public void CloseAdmin()
    {
        popup.SetActive(false);
    }

    public void OpenAdmin()
    {
        popup.SetActive(true);
    }
    
}
