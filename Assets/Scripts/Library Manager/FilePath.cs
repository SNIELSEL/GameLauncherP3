using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using SimpleFileBrowser;

public class FilePath : MonoBehaviour
{

    [Header ("File Select")]
    [SerializeField] private string fileName;
    [SerializeField] private string[] subFolders;
    [SerializeField] private Login acount;


    [Header ("Script Reverence")]
    public DescriptionAutoFill descriptionAutoFill;

    public ProjectHolder projectHolder;

    public DeleteGameObjectAfterFileChange delete;

    // Code from the folder asset From Asset Store.
    public void ChooseGamesFolder()
    {
        // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
        // It is sufficient to add a quick link just once
        // Name: Users
        // Path: C:\Users
        // Icon: default (folder icon)
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        // !!! Uncomment any of the examples below to show the file browser !!!

        // Example 1: Show a save file dialog using callback approach
        // onSuccess event: not registered (which means this dialog is pretty useless)
        // onCancel event: not registered
        // Save file/folder: file, Allow multiple selection: false
        // Initial path: "C:\", Initial filename: "Screenshot.png"
        // Title: "Save As", Submit button text: "Save"
        FileBrowser.ShowLoadDialog(OnFilesSelected, null, FileBrowser.PickMode.FilesAndFolders, false, "C:\\Users\\" + System.Environment.UserName + "\\Desktop", "", "Choose Games Folder", "Load");
    }

    
    void OnFilesSelected(string[] filePaths)
    {
        //delete.OnFIleChange();

        acount.SetLog("Changed the folder location");
        // Print paths of the selected files
        for (int i = 0; i < filePaths.Length; i++)
        {
            Debug.Log(filePaths[i]);
        }

        // Get the file path of the first selected file
        fileName = filePaths[0];

        PlayerPrefs.SetString("GameFolderPath", fileName);

        GameFolderCheck();
    }

    private void Awake()
    {
        fileName = PlayerPrefs.GetString("GameFolderPath");

        if(fileName == null || fileName == "")
        {
            GetFilePath();
        }
        else
        {
            GameFolderCheck();
        }
    }

    private void GameFolderCheck()
    {
        subFolders = Directory.GetDirectories(fileName);

        descriptionAutoFill.gameFolders = subFolders;
        descriptionAutoFill.GetAllFolderData();
        projectHolder.Init();
    }

    protected void GetFilePath()
    {
        ChooseGamesFolder();
    }
}
