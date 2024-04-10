using UnityEngine;
using System.IO;
using TMPro;
using System.Reflection;
using UnityEngine.UI;
using static System.Windows.Forms.LinkLabel;

public class DescriptionAutoFill : MonoBehaviour
{
    [Header ("Prefab's")]
    [SerializeField] private GameObject gameButtonPrefab;


    [Header ("Text Data")]

    [SerializeField] private string filePath;
    [SerializeField] private string imageFilePath;
    [SerializeField] private string exeFilePath;

    [SerializeField] private string[] lineText;
    [SerializeField] private int lineAmount;
    [SerializeField] private Transform parent;

    public string[] gameFolders;
    private int currentLineIndex = 0;


    [Header ("Text")]

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI CreatorText;


    [Header ("Images")]

    [SerializeField] RawImage logo;
    [SerializeField] RawImage banner;
    [SerializeField] RawImage qrCode;


    public GameObject library;


    //This void gets info out of the Selected File.
    public void GetAllFolderData()
    {
        //Cicles trough all of the sub folders of the selected library file.
        for (int i = 0; i < gameFolders.Length; i++)
        {
            filePath = gameFolders[i] + "/Description.txt";

            GameObject requirementChecker = Instantiate(gameButtonPrefab, new Vector3(transform.position.x,transform.position.y, 90), transform.rotation, parent);
            
            if(File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                lineText = new string[lines.Length];

                //Cicles Trhough all of the text files lines until it vfinds a key word and then assaings it to the right variable.
                for (int j = 0; j < lines.Length; j++)
                {
                    lineText[j] = GetLineAtIndex(j);

                    if (lineText[j] == "Name:")
                    {
                        requirementChecker.GetComponent<RequirmentCheck>().gameName = GetLineAtIndex(j + 1);
                    }

                    if (lineText[j] == "Description:")
                    {
                        requirementChecker.GetComponent<RequirmentCheck>().gameDescrioption = GetLineAtIndex(j + 1);
                    }

                    if (lineText[j] == "Creators:")
                    {
                        requirementChecker.GetComponent<RequirmentCheck>().creators = GetLineAtIndex(j + 1);
                    }

                    if (lineText[j] == "LeerJaar:")
                    {
                        requirementChecker.GetComponent<RequirmentCheck>().leerJaar = GetLineAtIndex(j + 1);
                    }

                    if (lineText[j] == "tag1:")
                    {
                        requirementChecker.GetComponent<RequirmentCheck>().tag1 = GetLineAtIndex(j + 1);
                    }

                    if (lineText[j] == "tag2:")
                    {
                        requirementChecker.GetComponent<RequirmentCheck>().tag2 = GetLineAtIndex(j + 1);
                    }

                    if (lineText[j] == "tag3:")
                    {
                        requirementChecker.GetComponent<RequirmentCheck>().tag3 = GetLineAtIndex(j + 1);
                    }

                    if (lineText[j] == "Score:")
                    {
                        requirementChecker.GetComponent<RequirmentCheck>().docentenScore = GetLineAtIndex(j + 1);
                    }

                    if (lineText[j] == "Day:")
                    {
                        int.TryParse(GetLineAtIndex(j + 1), out requirementChecker.GetComponent<RequirmentCheck>().day);
                    }

                    if (lineText[j] == "Month:")
                    {
                        int.TryParse(GetLineAtIndex(j + 1), out requirementChecker.GetComponent<RequirmentCheck>().month);
                    }

                    if (lineText[j] == "Year:")
                    {
                        int.TryParse(GetLineAtIndex(j + 1), out requirementChecker.GetComponent<RequirmentCheck>().year);

                        requirementChecker.GetComponent<RequirmentCheck>().CreateCreationDateWithData();
                    }
                }
            }


            

            //After finding the right .PNG file it willl conect it to the right RawImage.
            imageFilePath = gameFolders[i] + "/Logo.png";
            if (File.Exists(imageFilePath))
            {
                requirementChecker.GetComponent<RequirmentCheck>().gameLogo.texture = LoadImage(imageFilePath);
            }

            imageFilePath = gameFolders[i] + "/Banner.png";
            if (File.Exists(imageFilePath))
            {
                requirementChecker.GetComponent<RequirmentCheck>().gameBanner.texture = LoadImage(imageFilePath);
            }

            imageFilePath = gameFolders[i] + "/QRCode.png";
            if (File.Exists(imageFilePath))
            {
                print("QR code found");
                requirementChecker.GetComponent<RequirmentCheck>().qrCode.texture = LoadImage(imageFilePath);
            }

            // find .exe path
            filePath = gameFolders[i];
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            FileInfo[] exeName = directoryInfo.GetFiles("*.exe");
            if(exeName != null && exeName.Length != 0)
            {
                exeFilePath = filePath + "/" + exeName[0].Name;
            }
            requirementChecker.GetComponent<RequirmentCheck>().executableFilePath = exeFilePath;
            //Niels
            string processName = exeFilePath;
            processName = processName.Replace(".exe", "");
            processName = processName.Substring(processName.IndexOf('/') + 1);

            //GameObject.Find("ScriptHolder").GetComponent<ProjectHolder>().gameNames.Add(processName);
        }
    }


    // Sets The selected textures above to the right size.
    public static Texture2D LoadImage(string filename)
    {
        byte[] bytes = File.ReadAllBytes(filename);

        Texture2D texture = new Texture2D(474,474);
        texture.LoadImage(bytes);

        return texture;
    }

    // Checks all The Lines
    private string GetLineAtIndex(int index)
    {
        lineAmount = index;
        string[] lines = File.ReadAllLines(filePath);

        if (index < lines.Length)
        {
            return lines[index];
        }
        else
        {
            return "No Lines Found";
        }
    }
}