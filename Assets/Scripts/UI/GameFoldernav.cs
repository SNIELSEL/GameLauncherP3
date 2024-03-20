using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameFoldernav : MonoBehaviour
{
    public GameObject parent;
    public Button libraryButton;
    public EventSystem eventSystem;

    public InputEmulationScript emulationScript;

    public void SetNavigation()
    {
        if(parent.transform.GetChild(0) != null)
        {
            eventSystem.SetSelectedGameObject(parent.transform.GetChild(0).GetChild(0).gameObject);
        }
    }

    private void Update()
    {
        if(eventSystem.currentSelectedGameObject == null)
        {
            emulationScript.usingMouse = false;
            eventSystem.SetSelectedGameObject(libraryButton.gameObject);
        }
    }
}
