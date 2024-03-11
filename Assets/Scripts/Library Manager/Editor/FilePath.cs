using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class FilePath : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private string[] subFolders;

    public DescriptionAutoFill descriptionAutoFill;

    private void Awake()
    {
        fileName = PlayerPrefs.GetString("GameFolderPath");

        if(fileName != null)
        {
            GameFolderCheck();
        }
    }

    private void GameFolderCheck()
    {
       subFolders = Directory.GetDirectories(fileName);

        descriptionAutoFill.gameFolders = subFolders;
        descriptionAutoFill.GetAllFolderData();
    }

    protected void GetFilePath()
    {
        fileName = EditorUtility.OpenFolderPanel("Select Games Folder", "", "");

        PlayerPrefs.SetString("GameFolderPath", fileName);

        GameFolderCheck();
    }
}
