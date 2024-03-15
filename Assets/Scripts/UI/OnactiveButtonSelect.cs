using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnactiveButtonSelect : MonoBehaviour
{
    public EventSystem eventSystem;
    private bool active;
    public GameObject button;

    public GameObject libButton;

    private void OnEnable()
    {
        active = true;
        if (active)
        {
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            eventSystem.SetSelectedGameObject(button);
        }
    }

    public void backtoLib()
    {
        libButton = GameObject.Find("ToGamesButton");
        eventSystem.SetSelectedGameObject(libButton);
    }

    IEnumerator TimerCountDown()
    {

        yield return new WaitForSeconds(0);
        active = true;
    }
}
