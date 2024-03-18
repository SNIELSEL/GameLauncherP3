using UnityEngine;
using System.IO;
using UnityEngine.UI;
using SimpleFileBrowser;
using System.Collections;

public class OnUninstall : MonoBehaviour
{
    [SerializeField] private string selectedFile;
    [SerializeField] private GameObject folderPanel;

    //Code from the asset from the asset store.
    public void ChooseDeletionFolder()
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
        FileBrowser.ShowLoadDialog(OnFilesSelected, null, FileBrowser.PickMode.FilesAndFolders, false, "C:\\Users\\" + System.Environment.UserName + "\\Desktop", "", "deleteFile/Folder", "Delete");

    }

    void OnFilesSelected(string[] filePaths)
    {
        // Print paths of the selected files
        for (int i = 0; i < filePaths.Length; i++)
        {
            Debug.Log(filePaths[i]);
        }

        // Get the file path of the first selected file
        selectedFile = filePaths[0];
        DeleteGame();

        // Or, copy the first file to persistentDataPath
        FileBrowserHelpers.DeleteFile(selectedFile);

    }

    //Deletes the selected file after conferming.
    private void DeleteGame()
    {
        //Checks if the file is selected.
        if (selectedFile != "")
        {
            try
            {
                if (Directory.Exists(selectedFile))
                {
                    Directory.Delete(selectedFile, true);
                    Debug.Log("Directory deleted: " + selectedFile);
                }
                else
                {
                    Debug.Log("Directory does not exist: " + selectedFile);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error deleting directory: " + e.Message);
            }
        }
        #if !UNITY_EDITOR
            folderPanel.SetActive(false); // Deactivate the folder selection panel after folder selection
        #endif
    }
}
