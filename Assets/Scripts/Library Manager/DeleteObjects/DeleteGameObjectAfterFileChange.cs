using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGameObjectAfterFileChange : MonoBehaviour
{
    [SerializeField] public GameObject deleteGameObject;

    private void FixedUpdate()
    {
        //deleteGameObject = GameObject.Find("Canvas").GetComponentInChildren<DeleteGameObject>();
    }
    public void OnFIleChange()
    {
        //deleteGameObject.DestroyObject();

        Destroy(deleteGameObject.gameObject);
    }
}
