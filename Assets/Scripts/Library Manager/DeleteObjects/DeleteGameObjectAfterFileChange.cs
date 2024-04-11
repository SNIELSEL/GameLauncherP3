using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGameObjectAfterFileChange : MonoBehaviour
{
    public GameObject parent;
    public void OnFIleChange()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }
}
