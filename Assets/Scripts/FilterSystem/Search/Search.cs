using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Search : MonoBehaviour
{

    public GameObject searchBar, contentHolder;
    public GameObject[] games;

    int totalGames;
    // Start is called before the first frame update
    void Start()
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
        string searchText = searchBar.GetComponent<TMP_InputField>().text;

        int searchLength = searchText.Length; 
        int searchedGames = 0;

        foreach (GameObject game in games)
        {
            searchedGames++;
            //goede
            /*if(game.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length >= searchLength)
            {
                if(searchText.ToLower() == (game.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text.Substring(0, searchLength).ToLower()))
                {
                    game.SetActive(true);
                   *//* NESSIEEEEEEEEEEEE WE LOVE NESSSIEEEEEEEEEEEEEEE*//*
                }
                else
                {
                    game.SetActive(false);
                }
            }*/


            //probeer
            if (game.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text.Length >= searchLength)
            {
                
                if (searchText.ToLower() == (game.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text.Substring(0, searchLength).ToLower()))
                {
                    game.SetActive(true);
                    /* NESSIEEEEEEEEEEEE WE LOVE NESSSIEEEEEEEEEEEEEEE*/
                }
                else
                {
                    game.SetActive(false);
                }
            }
        }
    }
}
