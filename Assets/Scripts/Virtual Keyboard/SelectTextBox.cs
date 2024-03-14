using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectTextBox : MonoBehaviour
{
    public EventSystem eventSystem;
    public Typing[] typing;
    public Backspace backspace;
    public GameObject searchbarText;
    string clearText = "Search";

    private TMP_Text input;
    [SerializeField] private GameObject setSelectedGameobject;

    public void Awake()
    {
        typing= FindObjectsOfType<Typing>();
        backspace = FindObjectOfType<Backspace>();

        searchbarText.GetComponent<TextMeshProUGUI>();
         clearText = searchbarText.GetComponent<TextMeshProUGUI>().text;
    }
    public void OnButtonClick(TMP_Text input)
    {
        for (int i = 0; i < typing.Length; i++)
        {
            typing[i].selectedInput = input;
        }
        backspace.selectedInputField = input;
        input.GetComponentInParent<Image>().color = Color.white;
        this.input = input;
        eventSystem.SetSelectedGameObject(setSelectedGameobject);
    }

    public void CloseKeyBoard()
    {
        eventSystem.SetSelectedGameObject(backspace.selectedInputField.GetComponentInParent<Button>().gameObject);
        input.GetComponentInParent<Image>().color = Color.white;
        searchbarText.GetComponent<TextMeshProUGUI>().text = clearText;
    }


    public void ClearText()
    {
        searchbarText.GetComponent<TextMeshProUGUI>().text = null;
    }
}
