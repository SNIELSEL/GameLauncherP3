using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFullScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
