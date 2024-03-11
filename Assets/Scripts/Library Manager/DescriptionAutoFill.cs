using UnityEngine;
using System.IO;
using TMPro;
using System.Reflection;
using UnityEngine.UI;

public class DescriptionAutoFill : MonoBehaviour
{
    [SerializeField] private GameObject gameButtonPrefab;

    [SerializeField] private string filePath;
    [SerializeField] private string imageFilePath;
    [SerializeField] private string exeFilePath;
    [SerializeField] private string fileName;
    [SerializeField] private string[] lineText;
    public string[] gameFolders;


    private int currentLineIndex = 0;
    [SerializeField] private int lineAmount;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI CreatorText;

    [SerializeField] RawImage logo;
    [SerializeField] RawImage banner;
    [SerializeField] RawImage qrCode;

    public void Start()
    {

    }

    public void GetAllFolderData()
    {

        for (int i = 0; i < gameFolders.Length; i++)
        {
            filePath = gameFolders[i] + "/Description.txt";

            GameObject requirementChecker = Instantiate(gameButtonPrefab, transform.position, transform.rotation);
    
            string[] lines = File.ReadAllLines(filePath);
            lineText = new string[lines.Length];

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

            //LoadImages
            imageFilePath = gameFolders[i] + "/Logo.PNG";
            requirementChecker.GetComponent<RequirmentCheck>().gameLogo.texture = LoadImage(imageFilePath);

            imageFilePath = gameFolders[i] + "/Banner.PNG";
            requirementChecker.GetComponent<RequirmentCheck>().gameBanner.texture = LoadImage(imageFilePath);
            
            /*imageFilePath = gameFolders[i] + "/QRCode.PNG";
            requirementChecker.GetComponent<RequirmentCheck>().gameBanner.texture = LoadImage(imageFilePath);
*/
            // find .exe path
            filePath = gameFolders[i];
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            FileInfo[] exeName = directoryInfo.GetFiles("*.exe");
            exeFilePath = filePath + "/" + exeName[0].Name;
            requirementChecker.GetComponent<RequirmentCheck>().executableFilePath = exeFilePath;
        }
    }

    public static Texture2D LoadImage(string filename)
    {
        byte[] bytes = File.ReadAllBytes(filename);

        Texture2D texture = new Texture2D(474,474);
        texture.LoadImage(bytes);

        return texture;
    }

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
            return "No Bitches?";
        }
    }
}