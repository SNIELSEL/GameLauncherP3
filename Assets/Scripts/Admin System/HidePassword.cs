using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HidePassword : MonoBehaviour
{
    //[SerializeField] private Toggle hide;
    [SerializeField] private TMP_Text password, hiddenPassword;
    [SerializeField] private Color invisable, normalcolor;

    public void Start()
    {
        password.color = invisable;
        hiddenPassword.color = normalcolor;
        gameObject.GetComponent<Toggle>().isOn = false;
    }
    public void OnToggle()
    {
        if (!gameObject.GetComponent<Toggle>().isOn)
        {
            password.color = invisable;
            hiddenPassword.color = normalcolor;
        }
        else
        {
            password.color = normalcolor;
            hiddenPassword.color = invisable;
        }
    }
}
