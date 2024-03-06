using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RequirmentCheck : MonoBehaviour
{
    [SerializeField] private GameObject uiGameButtonPrefab;
    [SerializeField] private GameObject infoPrefab;
    private Transform buttonParantObject;
    private Transform infoParantObject;

    public RawImage gameLogo;
    public RawImage gameBanner;
    public string gameName;
    public string gameDescrioption;
    public string creationDate;
    public string executableFilePath;

    public int year;
    public int month;
    public int day;

    private int variablesChecked;

    public void CreateCreationDateWithData()
    {
        creationDate = day + "/" + month+ "/" + year;
    }

    private void Start()
    {
        buttonParantObject = GameObject.Find("Content").transform;
        infoParantObject = GameObject.Find("ImageBackground").transform;
        StartCoroutine(CheckVeriables());
    }



    private IEnumerator CheckVeriables()
    {
        yield return new WaitForSeconds(1.5f);
        
        if (year <= 1900)
        {
            variablesChecked = -1000;
        }

        if (gameLogo.texture != null)
        {
            variablesChecked ++;
        }

        if (gameBanner.texture != null)
        {
            variablesChecked ++;
        }

        if (gameName != "")
        {
            variablesChecked ++;
        }

        if (gameDescrioption != "")
        {
            variablesChecked ++;
        }

        if (creationDate != "")
        {
            variablesChecked ++;
        }

        if (executableFilePath != "")
        {
            variablesChecked++;
        }

        if (variablesChecked == 6)
        {
            // this part of the codeis responseble of instatiating a button and creating 
            GameObject gameLibraryButton = Instantiate(uiGameButtonPrefab, transform.position, transform.rotation, buttonParantObject);

            GameObject button = gameLibraryButton.transform.GetChild(0).gameObject;
            button.GetComponent<RawImage>().texture = gameLogo.texture;

            button = gameLibraryButton.transform.GetChild(0).gameObject;
            GameObject panelTitel = button.transform.GetChild(0).gameObject;
            GameObject uiGameName = panelTitel.transform.GetChild(0).gameObject;
            uiGameName.GetComponent<TextMeshProUGUI>().text = gameName;

            //this part of the code is responseble of instatiating and setting the right info to the right text andf images on the info tab
            GameObject InfoTab = Instantiate(infoPrefab, transform.position, transform.rotation, infoParantObject);

            GameObject bannerImage = InfoTab.transform.GetChild(0).gameObject;
            bannerImage.GetComponent<RawImage>().texture = gameBanner.texture;

            GameObject gameImage = bannerImage.transform.GetChild(0).gameObject;
            gameImage.GetComponent<RawImage>().texture = gameLogo.texture;

            GameObject textPanel = bannerImage.transform.GetChild(2).gameObject;
            GameObject gameInfoName = textPanel.transform.GetChild(0).gameObject;
            gameInfoName.GetComponent<TextMeshProUGUI>().text = gameName;

            textPanel = bannerImage.transform.GetChild(2).gameObject;
            GameObject creatorInfo = textPanel.transform.GetChild(1).gameObject;
            creatorInfo.GetComponent<TextMeshProUGUI>().text = gameDescrioption;

            textPanel = bannerImage.transform.GetChild(2).gameObject;
            GameObject infoTags = textPanel.transform.GetChild(2).gameObject;
            infoTags.GetComponent<TextMeshProUGUI>().text = gameDescrioption;

            textPanel = bannerImage.transform.GetChild(2).gameObject;
            GameObject descriptionInfo = textPanel.transform.GetChild(3).gameObject;
            descriptionInfo.GetComponent<TextMeshProUGUI>().text = gameDescrioption;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
