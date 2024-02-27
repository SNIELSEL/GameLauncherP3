using UnityEngine;
using UnityEngine.UI;

public class Fullscreen : MonoBehaviour
{
    public void Awake()
    {
        if (gameObject.GetComponent<Toggle>().isOn == true && Screen.fullScreen != Screen.fullScreen)
        {
            Screen.fullScreen = Screen.fullScreen;
        }
    }
    public void FullscreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
        print("Changed Screen");
    }
}
