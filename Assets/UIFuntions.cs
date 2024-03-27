using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFuntions : MonoBehaviour
{
    [SerializeField] private GameObject filterMenu;

    public void FilterButtonClick()
    {
        if (!filterMenu.activeSelf)
        {
            MenuOn();
        }
        else
        {
            MenuOff();
        }


    }

    public void MenuOff()
    {
        filterMenu.SetActive(false);
    }

    public void MenuOn()
    {
        filterMenu.SetActive(true);
    }
}
