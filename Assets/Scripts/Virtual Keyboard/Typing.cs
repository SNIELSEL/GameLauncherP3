using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    public TMP_InputField selectedInput;
    [SerializeField] private string uncapped, capped;
    private TMP_Text text;
    [SerializeField] private bool symbol;

    public void Awake()
    {
        text = gameObject.GetComponent<TMP_Text>();
    }
    public void OnButtonClick()
    {
        if (selectedInput != null)
        {
            selectedInput.text += text.text;
        }
    }
    public void Shift()
    {
        if (symbol == true)
        {
            if (text.text == capped)
            {
                text.text = uncapped;
            }
            else
            {
                text.text = capped;
            }
        }
    }
}
