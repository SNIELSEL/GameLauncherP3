using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField username, password;
    [SerializeField] private string AdminUsername, AdminPassword;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Username"))
        {
            PlayerPrefs.SetString("Username", AdminUsername);
        }
        if (!PlayerPrefs.HasKey("Password"))
        {
            PlayerPrefs.SetString("Password", AdminPassword);
        }
    }

    public void AdminLogin()
    {
        if (username.text == PlayerPrefs.GetString("Username") && password.text == PlayerPrefs.GetString("Password"))
        {
            print("Login succeeded");
            // Set bool isAdmin to true
        }
        else
        {
            print("Login failed");
            // Set text on screen: Login failed.
        }
    }
}
