using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Backspace : MonoBehaviour
{
    public TMP_Text selectedInputField;

    public void OnButtonClick()
    {
        if (selectedInputField.text.Length > 0)
        {
            selectedInputField.text = selectedInputField.text.Remove(selectedInputField.text.Length - 1);
            if(selectedInputField.transform.GetChild(0).GetComponent<TMP_Text>() != null)
            {
                selectedInputField.transform.GetChild(0).GetComponent<TMP_Text>().text = selectedInputField.transform.GetChild(0).GetComponent<TMP_Text>().text.Remove(selectedInputField.transform.GetChild(0).GetComponent<TMP_Text>().text.Length - 1);
            }
        }
    }
}
