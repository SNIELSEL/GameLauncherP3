using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
public class Login : MonoBehaviour
{
    [Tooltip("if this bool is true than the user is logged into an (admin) acount")]
    public bool isAdmin;

    [Header("Screens")]
    [SerializeField] private GameObject loginScreen;
    [SerializeField] private GameObject acountScreen;

    [Header("InputField Refrence")]
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;

    [Header("Standard values")]
    [Tooltip("On the first time starting the app it will set and save the username to this variable")]
    [SerializeField] private string AdminUsername;
    [Tooltip("On the first time starting the app it will set and save the password to this variable")]
    [SerializeField] private string AdminPassword;
    [Tooltip("Recovery key is made random when you first start the app, If you forgot your username or password you can fill the recovery key in the password input field")]
    private string recoveryKey;

    [Header("Recovery Key text")]
    [SerializeField] private TMP_Text recoveryKeyText;
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
        if (!PlayerPrefs.HasKey("Recoverykey"))
        {
            recoveryKey = GenerateRandomString(8);
            PlayerPrefs.SetString("Recoverykey", recoveryKey);
            recoveryKeyText.text = recoveryKey;
        }
    }

    public void AdminLogin()
    {
        if (username.text == PlayerPrefs.GetString("Username") && password.text == PlayerPrefs.GetString("Password") || password.text == PlayerPrefs.GetString("Recoverykey"))
        {
            print("Login succeeded");
            isAdmin = true;
            loginScreen.SetActive(false);
            acountScreen.SetActive(true);
            recoveryKeyText.text = "";
        }
        else
        {
            print("Login failed");
            // Set text on screen: Login failed.
        }
        username.text = "";
        password.text = "";
    }

    public void Logout()
    {
        isAdmin = false;
        loginScreen.SetActive(true);
        acountScreen.SetActive(false);
    }

    public string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder stringBuilder = new StringBuilder(length);
        System.Random random = new System.Random();

        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(chars[random.Next(chars.Length)]);
        }

        return stringBuilder.ToString();
    }

}
