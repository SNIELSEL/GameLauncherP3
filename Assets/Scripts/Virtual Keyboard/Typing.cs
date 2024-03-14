using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    public TMP_Text selectedInput;
    [SerializeField] private string uncapped, capped;
    private TMP_Text text;
    [SerializeField] private bool symbol;

    public void Awake()
    {
        text = gameObject.GetComponent<TMP_Text>();
    }
    public void OnButtonClick()
    {
        selectedInput.text += text.text;
        if(selectedInput.transform.childCount > 0)
        {
            if (selectedInput.transform.GetChild(0).GetComponent<TMP_Text>() != null)
            {
                selectedInput.transform.GetChild(0).GetComponent<TMP_Text>().text += "*";
            }
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
