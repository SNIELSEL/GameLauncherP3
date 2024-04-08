using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSlider : MonoBehaviour
{
    public Slider ratingSlider;
    public GameObject greyOut;
    public Toggle ratingToggle;

    public void ToggleClick()
    {
        if (ratingToggle.isOn == true)
        {
            Debug.Log(ratingToggle.isOn);
            ratingSlider.interactable = true;
            greyOut.SetActive(false);
        }

        if (ratingToggle.isOn == false)
        {
            Debug.Log(ratingToggle.isOn);
            ratingSlider.interactable = false;
            ratingSlider.value = 0;
            greyOut.SetActive(true);

        }
            

 

    }

}
