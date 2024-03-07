using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequirmentCheck : MonoBehaviour
{
    [SerializeField] private GameObject uiGameButtonPrefab;
    [SerializeField] private GameObject infoPrefab;
    private Transform buttonParantObject;
    private Transform infoParantObject;

    public RawImage gameLogo;
    public RawImage gameBanner;
    public RawImage qrCode;

    public string gameName;
    public string gameDescrioption;
    public string creationDate;
    public string creators;
    public string tags;
    public string docentenScore;
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

        if (creators != "")
        {
            variablesChecked++;
        }
        
        if (tags != "")
        {
            variablesChecked++;
        }
        
        if (docentenScore != "")
        {
            variablesChecked++;
        }
        
        if (executableFilePath != "")
        {
            variablesChecked++;
        }

        if (variablesChecked >= 9)
        {
            // this part of the codeis responseble of instatiating a button and creating 
            GameObject gameLibraryButton = Instantiate(uiGameButtonPrefab, transform.position, transform.rotation, buttonParantObject);

            GameObject button = gameLibraryButton.transform.GetChild(0).gameObject;
            button.GetComponent<RawImage>().texture = gameLogo.texture;

            GameObject panelTitel = button.transform.GetChild(0).gameObject;
            GameObject uiGameName = panelTitel.transform.GetChild(0).gameObject;
            uiGameName.GetComponent<TextMeshProUGUI>().text = gameName;

            //this part of the code is responseble of instatiating and setting the right info to the right text andf images on the info tab
            GameObject infoTab = Instantiate(infoPrefab, transform.position, transform.rotation, infoParantObject);
            infoTab.GetComponent<RectTransform>().offsetMax = new Vector2(0,0);
            infoTab.GetComponent<RectTransform>().offsetMin = new Vector2(0,0);
            button.GetComponent<GoToInfoTab>().buttonInfoTab = infoTab;
            infoTab.SetActive(false);

            GameObject bannerImage = infoTab.transform.GetChild(0).gameObject;
            bannerImage.GetComponent<RawImage>().texture = gameBanner.texture;

            GameObject gameImage = bannerImage.transform.GetChild(0).gameObject;
            gameImage.GetComponent<RawImage>().texture = gameLogo.texture;
            
/*            GameObject gameQRCode = infoTab.transform.GetChild(1).gameObject;
            gameQRCode.GetComponent<RawImage>().texture = gameLogo.texture;*/

            GameObject textPanel = bannerImage.transform.GetChild(2).gameObject;
            GameObject gameInfoName = textPanel.transform.GetChild(0).gameObject;
            gameInfoName.GetComponent<TextMeshProUGUI>().text = gameName;

            textPanel = bannerImage.transform.GetChild(2).gameObject;
            GameObject creatorInfo = textPanel.transform.GetChild(1).gameObject;
            creatorInfo.GetComponent<TextMeshProUGUI>().text = creators;

            textPanel = bannerImage.transform.GetChild(2).gameObject;
            GameObject infoTags = textPanel.transform.GetChild(2).gameObject;
            infoTags.GetComponent<TextMeshProUGUI>().text = tags;

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
