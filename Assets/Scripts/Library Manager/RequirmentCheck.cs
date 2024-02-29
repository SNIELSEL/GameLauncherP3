using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RequirmentCheck : MonoBehaviour
{
    [SerializeField] private GameObject uiGameButtonPrefab;
    private Transform parantObject;

    public RawImage gameLogo;
    public RawImage gameBanner;
    public string gameName;
    public string gameDescrioption;
    public string creationDate;

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
        parantObject = GameObject.Find("Games").transform;
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
            variablesChecked++;
        }

        if (gameBanner.texture != null)
        {
            variablesChecked++;
        }

        if (gameName != "")
        {
            variablesChecked++;
        }

        if (gameDescrioption != "")
        {
            variablesChecked++;
        }

        if (creationDate != "")
        {
            variablesChecked++;
        }

        if (variablesChecked == 5)
        {
            GameObject game = Instantiate(uiGameButtonPrefab, transform.position, transform.rotation, parantObject);

            GameObject logoChild = game.transform.GetChild(1).gameObject;
            logoChild.GetComponent<RawImage>().texture = gameLogo.texture;

            GameObject NameChild = game.transform.GetChild(2).gameObject;
            NameChild.GetComponent<TextMeshProUGUI>().text = gameName;

            GameObject fullInfo = game.transform.GetChild(3).gameObject;
            GameObject descriptionChild = fullInfo.transform.GetChild(2).gameObject;
            descriptionChild.GetComponent<TextMeshProUGUI>().text = gameDescrioption;

            GameObject fullInfoNameChild = fullInfo.transform.GetChild(3).gameObject;
            fullInfoNameChild.GetComponent<TextMeshProUGUI>().text = gameName;

            GameObject bannerChild = game.transform.GetChild(4).gameObject;
            bannerChild.GetComponent<RawImage>().texture = gameBanner.texture;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
