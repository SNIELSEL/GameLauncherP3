using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Backspace : MonoBehaviour
{
    public TMP_InputField selectedInputField;

    public void OnButtonClick()
    {
        if (selectedInputField.text.Length > 0)
        {
            selectedInputField.text = selectedInputField.text.Remove(selectedInputField.text.Length - 1);
        }
    }
}
