using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HidePassword : MonoBehaviour
{
    [SerializeField] private Toggle hide;
    [SerializeField] private TMP_Text password, hiddenPassword;
    
    public void OnToggle()
    {
        if (hide.isOn)
        {
            //password.GetComponent<Image>().color = 
        }
        else
        {
            
        }
    }
}
