using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private GameObject libraryScreen;
    [SerializeField] private GameObject adminScreen;

    [SerializeField] private Toggle homeToggle;
    [SerializeField] private Toggle libraryToggle;
    [SerializeField] private Toggle adminToggle;

    // Start is called before the first frame update
    void Start()
    {
        homeScreen.SetActive(true);
        libraryScreen.SetActive(false);
        adminScreen.SetActive(false);

        homeToggle.isOn = true;
        libraryToggle.isOn = false;
        adminToggle.isOn = false;
    }
}
