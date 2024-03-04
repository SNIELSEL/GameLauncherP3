using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    public TMP_InputField selectedInput;

    public void OnButtonClick()
    {
        if (selectedInput != null)
        {
            selectedInput.text += gameObject.GetComponent<TMP_Text>().text;
        }
    }
}
