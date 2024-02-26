using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fullscreen : MonoBehaviour
{
    string _url = "https://ca-times.brightspotcdn.com/dims4/default/54847e8/2147483647/strip/true/crop/2048x1280+0+43/resize/1200x750!/quality/75/?url=https%3A%2F%2Fcalifornia-times-brightspot.s3.amazonaws.com%2Fb2%2Fa5%2Fd673ffac73e3ff63f2f3c095fde9%2Fhomemade-american-cheese-recipes-db";

    public void Awake()
    {
        if(gameObject.GetComponent<Toggle>().isOn == true && Screen.fullScreen != Screen.fullScreen)
        {
            Screen.fullScreen = Screen.fullScreen;
        }
    }
    public void FullscreenToggle()
    {
        Application.OpenURL(_url);

        //Screen.fullScreen = !Screen.fullScreen;
        //print("Changed Screen");
    }
}
