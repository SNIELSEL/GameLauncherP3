using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LibraryFix : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject library;
    private bool secondAction;
    public void SetLibraryButton()
    {
        if (!secondAction)
        {
            secondAction = true;
            StartCoroutine(LibraySetSelected());
        }
    }

    public IEnumerator LibraySetSelected()
    {
        yield return new WaitForSeconds(0.1f);

        eventSystem.SetSelectedGameObject(library);
    }
}