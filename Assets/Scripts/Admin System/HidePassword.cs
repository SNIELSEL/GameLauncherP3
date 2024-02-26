using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HidePassword : MonoBehaviour
{
    [SerializeField] private Toggle hide;
    
    public void OnToggle()
    {
        if (hide.isOn)
        {
            gameObject.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.Password;
        }
        else
        {
            gameObject.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.Standard;
        }
        gameObject.GetComponent<TMP_InputField>().ActivateInputField();
    }
}
