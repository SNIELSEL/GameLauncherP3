using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Backspace : MonoBehaviour
{
    public TMP_InputField selectedInputField;

    public void OnButtonClick()
    {
        selectedInputField.text = selectedInputField.text.Remove(selectedInputField.text.Length -1);
    }
}
