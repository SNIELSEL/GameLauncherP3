using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Shift : MonoBehaviour
{
    public Typing[] typing;

    public void Awake()
    {
        typing = FindObjectsOfType<Typing>();
    }
    public void OnButtonClick()
    {
        // If Shift is on
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            for (int i = 0; i < typing.Length; i++)
            {
                typing[i].Shift();
                typing[i].GetComponent<TMP_Text>().text = typing[i].GetComponent<TMP_Text>().text.ToUpper();
            }
        }
        // If Shift is off
        else
        {
            for (int i = 0; i < typing.Length; i++)
            {
                typing[i].Shift();
                typing[i].GetComponent<TMP_Text>().text = typing[i].GetComponent<TMP_Text>().text.ToLower();
            }
        }
    }
}
