using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LibraryFix : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject library;

    public void SetLibraryButton()
    {
        StartCoroutine(LibraySetSelected());
    }

    public IEnumerator LibraySetSelected()
    {
        yield return new WaitForSeconds(0.1f);

        eventSystem.SetSelectedGameObject(library);
    }
}
