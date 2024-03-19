using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using TMPro;
using UnityEngine;

public class Search : MonoBehaviour
{

    public GameObject searchBar, contentHolder;
    public GameObject[] games;

    int totalGames;
    float startTime = 4;
    
   
    void Start()
    {
        Invoke("FindGames", startTime);
    }

    void FindGames()
    {
        totalGames = contentHolder.transform.childCount;
        games = new GameObject[totalGames];

        for (int i = 0; i < totalGames; i++)
        {
            games[i] = contentHolder.transform.GetChild(i).gameObject;
        }

    }

    public void SearchBar()
    {
       
        string searchText = searchBar.GetComponent<TMP_Text>().text;

        int searchLength = searchText.Length; 
        int searchedGames = 0;

        foreach (GameObject game in games)
        {
            searchedGames++;
            for (int i  = 0; i < contentHolder.transform.childCount; i++)
            {
                if (game.transform.GetComponentInChildren<TextMeshProUGUI>().text.Length >= searchLength)//error
                {
                  

                    if (searchText.ToLower() == (game.transform.GetComponentInChildren<TextMeshProUGUI>().text.Substring(0, searchLength).ToLower()))
                    {
                       
                        game.SetActive(true);

                    }
                    else
                    {
                       
                        game.SetActive(false);
                    }
                }

            }
         
        }
    }

    public void CloseSearchBar()
    {
        for (int i = 0; i < games.Length; i++)
        {
            games[i].SetActive(true);
        }
    }
}
