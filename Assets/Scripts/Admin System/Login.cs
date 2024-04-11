using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Login : MonoBehaviour
{
    #region variables
    [Tooltip("if this bool is true than the user is logged into an (admin) acount")]
    public bool isAdmin;
    public bool isSuperAdmin;

    [Header("Eventsystem variables")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject nextSelectedButtonAcount;
    [SerializeField] private GameObject nextSelectedButtonSuperAcount;
    [Header("Screens")]
    [SerializeField] private GameObject loginScreen;
    [SerializeField] private GameObject acountScreen;
    [SerializeField] private GameObject superAcountScreen;

    [Header("InputField Refrence")]
    [SerializeField] private TMP_Text username;
    [SerializeField] private TMP_Text password;

    [Header("InputField Refrence for changing Username and Password")]
    [SerializeField] private TMP_Text newUsername;
    [SerializeField] private TMP_Text confirmNewUsername;
    [SerializeField] private TMP_Text newPassword;
    [SerializeField] private TMP_Text confirmNewPassword;

    [Header("Standard values")]
    [Tooltip("On the first time starting the app it will set and save the username to this variable")]
    [SerializeField] private string AdminUsername;
    [Tooltip("On the first time starting the app it will set and save the password to this variable")]
    [SerializeField] private string AdminPassword;
    [Tooltip("Recovery key is made random when you first start the app, If you forgot your username or password you can fill the recovery key in the password input field")]
    private string recoveryKey;

    [Header("Recovery Key text")]
    [SerializeField] private TMP_Text recoveryKeyText;

    [Header("Message/Error text")]
    [Tooltip("is used for login errors")]
    [SerializeField] private TMP_Text errorText;
    private int wrongTries;

    [Header("Create new admin acount page")]
    [SerializeField] private TMP_Text newAcountUsername;
    [SerializeField] private TMP_Text newAcountPassword;
    [SerializeField] private TMP_Text createAcountMessage;
    private bool usernameAvailable = true;

    [Header("Delete Admin acount page")]
    [SerializeField] private TMP_Text deleteUsername;
    [SerializeField] private TMP_Text deleteAcountMessage;

    [Header("Change screens messages")]
    [SerializeField] private TMP_Text changeUsernameTextMessage;
    [SerializeField] private TMP_Text changePasswordTextMessage;

    private int currentAdminIndex;
    [SerializeField] private TMP_Text acountSuper; //used for displaying username in top left
    [SerializeField] private TMP_Text acountAdmin;
    [SerializeField] private TMP_Text[] log;
    private int logint;

    [Header("AudioSources")]
    [SerializeField] private AudioSource errorSFX;
    [SerializeField] private AudioSource succesSFX;

    #endregion
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
            recoveryKeyText.text = "This is your recovery key save it before loging in! \n\n" +recoveryKey;
        }
    }

    public void AdminLogin()
    {
        // Login Admin
        if (PlayerPrefs.HasKey("AdminIndex"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("AdminIndex"); i++)
            {
                if (username.text == PlayerPrefs.GetString("Username" + i) && password.text == PlayerPrefs.GetString("Password" + i) && username.text != "")
                {
                    print("Login succeeded");
                    isAdmin = true;
                    loginScreen.SetActive(false);
                    acountScreen.SetActive(true);
                    recoveryKeyText.text = "";
                    wrongTries = 0;
                    eventSystem.SetSelectedGameObject(nextSelectedButtonAcount);
                    currentAdminIndex = i;
                    acountAdmin.text = PlayerPrefs.GetString("Username" + i);
                    succesSFX.Play();
                }
            }
        }
        // Login Super Admin
        if (username.text == PlayerPrefs.GetString("Username") && password.text == PlayerPrefs.GetString("Password") || password.text == PlayerPrefs.GetString("Recoverykey"))
        {
            print("Login succeeded");
            isSuperAdmin = true;
            loginScreen.SetActive(false);
            superAcountScreen.SetActive(true);
            recoveryKeyText.text = "";
            wrongTries = 0;
            eventSystem.SetSelectedGameObject(nextSelectedButtonSuperAcount);
            acountSuper.text = PlayerPrefs.GetString("Username");
            succesSFX.Play();
        }
        //Login Failed
        else if (!isAdmin)
        {
            print("Login failed");
            wrongTries += 1;
            if (wrongTries >= 3)
            {
                errorText.text = "You can use the recovery key in the password slot to login";
            }
            else
            {
                errorText.text = "Wrong username or password";
            }
            errorSFX.Play();
        }
        if (wrongTries == 0)
        {
            errorText.text = "";
        }
        username.text = "";
        password.text = "";
        password.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    }

    public void Logout()
    {
        isAdmin = false;
        isSuperAdmin = false;
        loginScreen.SetActive(true);
        acountScreen.SetActive(false);
        superAcountScreen.SetActive(false);
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

    #region Changing username and password
    public void ChangeUsername()
    {
        usernameAvailable = true;
        if (newUsername.text == PlayerPrefs.GetString("Username"))
        {
            usernameAvailable = false;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("AdminIndex"); i++)
        {
            if (newUsername.text == PlayerPrefs.GetString("Username" + i.ToString()))
            {
                usernameAvailable = false;
                break;
            }
        }

        if (usernameAvailable)
        {
            if (isAdmin) // Normal  admin
            {
                if (newUsername.text != "" && newUsername.text == confirmNewUsername.text)
                {
                    SetLog($"Changed username to {newUsername.text}");
                    PlayerPrefs.SetString("Username" + currentAdminIndex.ToString(), newUsername.text);
                }
            }
            else // Super Admin
            {
                if (newUsername.text != "" && newUsername.text == confirmNewUsername.text)
                {
                    SetLog($"Changed username to {newUsername.text}");
                    PlayerPrefs.SetString("Username", newUsername.text);
                }
            }
            changeUsernameTextMessage.color = Color.green;
            changeUsernameTextMessage.text = "Username Changed succesfully";

        }
        else
        {
            changeUsernameTextMessage.color = Color.red;
            changeUsernameTextMessage.text = "Both fields have to be the exact same and can't be empty \n\n Username might allready be in use ";
        }
        newUsername.text = "";
        confirmNewUsername.text = "";
        newUsername.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        confirmNewUsername.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    }

    public void ChangePassword()
    {
        if (newPassword.text != "" && newPassword.text == confirmNewPassword.text)
        {
            if (isAdmin) // Normal Admin
            {
                PlayerPrefs.SetString("Password" + currentAdminIndex.ToString(), newPassword.text);
            }
            else // Super Admin
            {
                PlayerPrefs.SetString("Password", newPassword.text);
            }
            changePasswordTextMessage.color = Color.green;
            changePasswordTextMessage.text = "Password changed succesfully";

        }
        else
        {
            changePasswordTextMessage.color = Color.red;
            changePasswordTextMessage.text = "Both fields have to be the exact same and can't be empty";
        }
        newPassword.text = "";
        confirmNewPassword.text = "";
        newPassword.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        confirmNewPassword.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    }

    #endregion
    public void ResetRecoveryKey()
    {
        PlayerPrefs.DeleteKey("Recoverykey");
    }


    public void CreateAdmin()
    {
        usernameAvailable = true;
        if (!PlayerPrefs.HasKey("AdminIndex"))
        {
            // AdminCount is for admin acounts NOT SUPERADMIN
            PlayerPrefs.SetInt("AdminIndex", 0);
        }
        if (newAcountUsername.text != "" && newAcountPassword.text != "")
        {
            for (int i = 0; i < PlayerPrefs.GetInt("AdminIndex"); i++)
            {
                if (newAcountUsername.text == PlayerPrefs.GetString("Username" + i.ToString()) || newAcountUsername.text == PlayerPrefs.GetString("Username"))
                {
                    usernameAvailable = false;
                    break;
                }
            }
            if (usernameAvailable)
            {
                succesSFX.Play();
                PlayerPrefs.SetString("Username" + PlayerPrefs.GetInt("AdminIndex").ToString(), newAcountUsername.text);
                PlayerPrefs.SetString("Password" + PlayerPrefs.GetInt("AdminIndex").ToString(), newAcountPassword.text);
                PlayerPrefs.SetInt("AdminIndex", PlayerPrefs.GetInt("AdminIndex") + 1);
                SetLog($"Created new admin acount with username: {newAcountUsername.text}");
                createAcountMessage.color = Color.green;
                createAcountMessage.text = "Succesfully created new admin acount";
            }
            else
            {
                createAcountMessage.color = Color.red;
                createAcountMessage.text = "failed \n name might allready be in use";
                errorSFX.Play();
                // Username isn't available
            }
        }
    }

    public void DeleteAdmin()
    {
        for (int i = 0; i <= PlayerPrefs.GetInt("AdminIndex"); i++)
        {
            if (deleteUsername.text == PlayerPrefs.GetString("Username" +i))
            {
                PlayerPrefs.DeleteKey("Username" + i);
                PlayerPrefs.DeleteKey("Password" + i);
                deleteAcountMessage.color = Color.green;
                deleteAcountMessage.text = "Succesfully deleted the acount";
                SetLog("Deleted admin acount with username: " + deleteUsername.text);
                succesSFX.Play();
                break;
            }
            if (i == PlayerPrefs.GetInt("AdminIndex"))
            {
                deleteAcountMessage.color = Color.red;
                deleteAcountMessage.text = "Couldn't find an acount with that name";
                errorSFX.Play();
            }
        }
    }
    #region log system
    public void LoadLog(int whatBatchToLoad) 
    {
        int logIndex = 0;
        if (PlayerPrefs.HasKey("LogIndex"))
        {
            logIndex = PlayerPrefs.GetInt("LogIndex") - 1;
        }
        int logNodeNumber = 0;
        for (int i = logIndex + whatBatchToLoad; i >= -5; i--)
        {
            if (PlayerPrefs.HasKey("Log" + i.ToString()))
            {
                log[logNodeNumber].text = PlayerPrefs.GetString("Log" + i.ToString());
            }
            else
            {
                log[logNodeNumber].text = "Empty Log";
            }
                logNodeNumber += 1;
            if (logNodeNumber == 5)
            {
                break;
            }
        }
    }

    public void LoadNextLogBatch()
    {
        if (logint <= -5)
        {
            logint += 5;
        }
        LoadLog(logint);
    }

    public void LoadPreviousLogBatch()
    {
        logint -= 5;
        LoadLog(logint);
    }
    public string SetLog(string action)
    {
        if (!PlayerPrefs.HasKey("LogIndex"))
        {
            PlayerPrefs.SetInt("LogIndex", 0);
        }
        int logIndex = PlayerPrefs.GetInt("LogIndex");
        string currentUser;
        if (isAdmin) // Normal Admin
        {
            currentUser = PlayerPrefs.GetString("Username" + currentAdminIndex);
        }
        else // Super Admin
        {
            currentUser = PlayerPrefs.GetString("Username");
        }
        PlayerPrefs.SetInt("LogIndex", PlayerPrefs.GetInt("LogIndex") +1 );
        string date = GetCurrentTime();
        string log = $"{currentUser} | {action} | {date} | {logIndex}";
        print(log);

        PlayerPrefs.SetString("Log" + logIndex.ToString(), log);

        return log;

    }
    public string GetCurrentTime()
    {
        int day = System.DateTime.Now.Day;
        int month = System.DateTime.Now.Month;
        int year = System.DateTime.Now.Year;
        int hour = System.DateTime.Now.Hour;
        int minute = System.DateTime.Now.Minute;

        string date = $"{day}-{month}-{year}/{hour}:{minute}";
        return date;
    }
    #endregion
}
