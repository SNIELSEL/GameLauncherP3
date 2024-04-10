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
    }
    public void OnToggle()
    {
        if (!gameObject.GetComponent<Toggle>().isOn)
        {
            password.color = normalcolor;
            hiddenPassword.color = invisable;
        }
        else
        {
            password.color = invisable;
            hiddenPassword.color = normalcolor;
        }
    }
}
