using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class DescriptionAutoFill : MonoBehaviour
{
    [SerializeField] private GameObject gameButtonPrefab;

    [SerializeField] private string filePath;
    [SerializeField] private string imageFilePath;
    [SerializeField] private string fileName;
    [SerializeField] private string[] lineText;
    public string[] gameFolders;


    private int currentLineIndex = 0;
    [SerializeField] private int lineAmount;
    
    [SerializeField] TextMeshProUGUI nameText, descriptionText;
    [SerializeField] RawImage logo;
    [SerializeField] RawImage banner;

    public void Start()
    {

    }

    public void GetAllFolderData()
    {

        for (int i = 0; i < gameFolders.Length; i++)
        {
            filePath = gameFolders[i] + "/Description.txt";

            GameObject game = Instantiate(gameButtonPrefab, transform.position, transform.rotation);
    
            string[] lines = File.ReadAllLines(filePath);
            lineText = new string[lines.Length];

            for (int j = 0; j < lines.Length; j++)
            {
                lineText[j] = GetLineAtIndex(j);

                if (lineText[j] == "Name:")
                {
                    game.GetComponent<RequirmentCheck>().gameName = GetLineAtIndex(j + 1);
                }

                if (lineText[j] == "Description:")
                {
                    game.GetComponent<RequirmentCheck>().gameDescrioption = GetLineAtIndex(j + 1);
                }

                if (lineText[j] == "Day:")
                {
                    int.TryParse(GetLineAtIndex(j + 1), out game.GetComponent<RequirmentCheck>().day);
                }
                
                if (lineText[j] == "Month:")
                {
                    int.TryParse(GetLineAtIndex(j + 1), out game.GetComponent<RequirmentCheck>().month);
                }
                
                if (lineText[j] == "Year:")
                {
                    int.TryParse(GetLineAtIndex(j + 1), out game.GetComponent<RequirmentCheck>().year);

                    game.GetComponent<RequirmentCheck>().CreateCreationDateWithData();
                }
            }

            //LoadImages
            imageFilePath = gameFolders[i] + "/Logo.PNG";
            game.GetComponent<RequirmentCheck>().gameLogo.texture = LoadImage(imageFilePath);

            imageFilePath = gameFolders[i] + "/Banner.PNG";
            game.GetComponent<RequirmentCheck>().gameBanner.texture = LoadImage(imageFilePath);

            filePath = gameFolders[i] + "/.exe";
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