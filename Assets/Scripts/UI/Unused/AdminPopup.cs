using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AdminPopup : MonoBehaviour
{
    [SerializeField] private GameObject popup;

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
