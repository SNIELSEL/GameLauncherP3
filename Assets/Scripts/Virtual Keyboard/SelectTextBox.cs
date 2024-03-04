using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectTextBox : MonoBehaviour
{
    public Typing[] typing;
    public Backspace backspace;

    public void Awake()
    {
        typing= FindObjectsOfType<Typing>();
        backspace = FindObjectOfType<Backspace>();
    }
    public void OnButtonClick(TMP_InputField input)
    {
        for (int i = 0; i < typing.Length; i++)
        {
            typing[i].selectedInput = input;
        }
        backspace.selectedInputField = input;

        input.GetComponent<Image>().color = Color.yellow;
    }
    public void DeselectColor(TMP_InputField input)
    {
        input.GetComponent <Image>().color = Color.white;
    }
}
