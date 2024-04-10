using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RequirmentCheck : MonoBehaviour
{
    [Header ("Prefabs's")]

    [SerializeField] private GameObject uiGameButtonPrefab;
    [SerializeField] private GameObject infoPrefab;


    [SerializeField] private Transform buttonParantObject;
    [SerializeField] private Transform infoParantObject;


    [Header ("Images")]

    public RawImage gameLogo;
    public RawImage gameBanner;
    public RawImage qrCode;


    [Header ("Data")]

    public string gameName;
    public string gameDescrioption;
    public string creationDate;
    public string leerJaar;
    public string creators;
    public string tag1;
    public string tag2;
    public string tag3;
    public string docentenScore;
    public string executableFilePath;

    public int year;
    public int month;
    public int day;
    public int convertedDocentenScore;
    public int gameTime;


    private int variablesChecked;


    [Header ("Script References")]
    public DescriptionAutoFill descriptionAuto;
    public GameObject library;

    //Doutzen Filter
   // public ProjectHolder projectHolder;
    public Product product;


    //Makes it so that the creation date looks nice inside the UI.
    public void CreateCreationDateWithData()
    {
        creationDate = day + "/" + month+ "/" + year;
    }

    private void Awake()
    {
        gameTime = UnityEngine.Random.Range(0, 100);
        //Debug.Log("DSHKFSHDJFJGSDJFHDSGJJHDSFJSDF ");
    }

    private void Start()
    {

        descriptionAuto = GameObject.Find("ScriptHolder").GetComponent<DescriptionAutoFill>();
        //library = descriptionAuto.library;
        //library.SetActive(true);
        buttonParantObject = GameObject.Find("Content").transform;
        infoParantObject = GameObject.FindGameObjectWithTag("Library").transform;
        CheckVeriables();

        //library.SetActive(false);
    }


    private void CheckVeriables()
    {
        if(year >= 2020 && gameLogo.texture != null && gameBanner.texture != null && gameName != "" && gameDescrioption != "" && creationDate != "" && creators != "" && tag1 != "" && tag2 != "" && tag3 != "" && docentenScore != "" && executableFilePath != "")
        {
            //print(1234567890);

            // this part of the codeis responseble of instatiating a button and creating 
            GameObject button = transform.GetChild(0).gameObject;
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

            GameObject gameImage = bannerImage.transform.GetChild(1).gameObject;
            gameImage.GetComponent<RawImage>().texture = gameLogo.texture;
            
            if (qrCode != null)
            {
                GameObject gameQRCode = bannerImage.transform.GetChild(2).gameObject;
                //print(bannerImage.name);
                gameQRCode.GetComponent<RawImage>().texture = qrCode.texture;
                //print(gameQRCode.name);
            }

            GameObject textPanel = bannerImage.transform.GetChild(3).gameObject;
            GameObject penal1 = textPanel.transform.GetChild(0).gameObject;
            GameObject gameInfoName = penal1.transform.GetChild(0).gameObject;
            gameInfoName.GetComponent<TextMeshProUGUI>().text = gameName;

            textPanel = bannerImage.transform.GetChild(3).gameObject;
            GameObject penal2 = textPanel.transform.GetChild(1).gameObject;
            GameObject creatorInfo = penal2.transform.GetChild(0).gameObject;
            creatorInfo.GetComponent<TextMeshProUGUI>().text = creators;

            textPanel = bannerImage.transform.GetChild(3).gameObject;
            penal2 = textPanel.transform.GetChild(1).gameObject;
            GameObject CreationDateInfo = penal2.transform.GetChild(1).gameObject;
            CreationDateInfo.GetComponent<TextMeshProUGUI>().text = creationDate;

            textPanel = bannerImage.transform.GetChild(3).gameObject;
            penal2 = textPanel.transform.GetChild(1).gameObject;
            GameObject leerJaarInfo = penal2.transform.GetChild(2).gameObject;
            leerJaarInfo.GetComponent<TextMeshProUGUI>().text = leerJaar;

            textPanel = bannerImage.transform.GetChild(3).gameObject;
            GameObject penal3 = textPanel.transform.GetChild(2).gameObject;
            GameObject infoTag1 = penal3.transform.GetChild(0).gameObject;
            infoTag1.GetComponent<TextMeshProUGUI>().text = tag1;

            textPanel = bannerImage.transform.GetChild(3).gameObject;
            penal3 = textPanel.transform.GetChild(2).gameObject;
            GameObject infoTag2 = penal3.transform.GetChild(1).gameObject;
            infoTag2.GetComponent<TextMeshProUGUI>().text = tag2;

            textPanel = bannerImage.transform.GetChild(3).gameObject;
            penal3 = textPanel.transform.GetChild(2).gameObject;
            GameObject infoTag3 = penal3.transform.GetChild(2).gameObject;
            infoTag3.GetComponent<TextMeshProUGUI>().text = tag3;

            textPanel = bannerImage.transform.GetChild(3).gameObject;
            GameObject penal4 = textPanel.transform.GetChild(3).gameObject;
            GameObject descriptionInfo = penal4.transform.GetChild(0).gameObject;
            descriptionInfo.GetComponent<TextMeshProUGUI>().text = gameDescrioption;

            textPanel = bannerImage.transform.GetChild(3).gameObject;
            penal4 = textPanel.transform.GetChild(3).gameObject;
            GameObject score = penal4.transform.GetChild(1).gameObject;
            int.TryParse(docentenScore, out convertedDocentenScore);
            score.GetComponent<Slider>().value = convertedDocentenScore;

            TextMeshProUGUI filePathText = infoTab.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            filePathText.text = executableFilePath;


            //Niels
            string processName = executableFilePath;
            processName = processName.Replace(".exe", "");
            processName = processName.Substring(processName.IndexOf('/') + 1);

            gameTime = (PlayerPrefs.GetInt("PlayTimeMinutes" + processName) + (PlayerPrefs.GetInt("PlayTimeHours" + processName) * 60)); ;
            product.filter.SetFilter(convertedDocentenScore, year, leerJaar, tag1, tag2, tag3, gameTime);

            //filter
        }

        else
        {
            Destroy(gameObject);
            Debug.Log("destroy");
        }   

    }
}
