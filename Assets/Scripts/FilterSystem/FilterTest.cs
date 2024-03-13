using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilterTest : MonoBehaviour
{
    int[] primeNumers = { 10, 92, 02, 56, 48, 23 };
    [SerializeField]string[] names; //{"eeeeeh", "nessie", "haaaaaaaaai", "doeeeeei", "peer", "jezus", "naast" }
    [SerializeField] TMP_Text[] AAAAH;

    public List<RequirmentCheck> games;

    public FilterTags tags;

    //p//ublic List<List<int>> filters;

    // Alle categories
    public List<int>[] filters = new List<int>[4];
    // Volgorde   BuildYear teacherScore studentYear filterTags;

    bool sortAlphabet;
    // Execute the query to produce the results
    
    public void UpdateFilterBuildYear (int i)
    {
        // Remove if it already exists.
        if (filters[0].Contains(i))
        {
            filters[0].Remove(i);
        }
        else
        {
            filters[0].Add(i);
        }
    }

    public void UpdateFilterTeacherScore(int i)
    {
        // Remove if it already exists.
        if (filters[1].Contains(i))
        {
            filters[1].Remove(i);
        }
        else
        {
            filters[1].Add(i);
        }
    }

    public void UpdateFilterStudentYear(int i)
    {
        // Remove if it already exists.
        if (filters[2].Contains(i))
        {
            filters[2].Remove(i);
        }
        else
        {
            filters[2].Add(i);
        }
    }

    public void UpdateFilterTags(int i)
    {
        // Remove if it already exists.
        if (filters[3].Contains(i))
        {
            filters[3].Remove(i);
        }
        else
        {
            filters[3].Add(i);
        }
    }

    public void UpdateFilters ()
    {
        // Verwijder alle games in lijst
        List<RequirmentCheck> gamesFiltered = new List<RequirmentCheck>();

        foreach (RequirmentCheck game in games) {
            

            // BUildYear
            for (int i = 0; i < filters[0].Count; i++)
            {
                if (game.buildYear == (BuildYear)i)
                {
                    gamesFiltered.Add(game);
                }
            }

            for (int i = 0; i < filters[1].Count; i++)
            {
                if (game.teacherScore == (TeacherScore)i)
                {
                    gamesFiltered.Add(game);
                }
            }

            for (int i = 0; i < filters[2].Count; i++)
            {
                if (game.studentYear == (StudentYear)i)
                {
                    gamesFiltered.Add(game);
                }
            }

            for (int i = 0; i < filters[3].Count; i++)
            {
                if (game.filterTags == (FilterTags)i)
                {
                    gamesFiltered.Add(game);
                }
            }
        }
    }
    
    private void Start()
    {
       
        Array.Sort(names);
        SortNames();
        tags = FilterTags.Action | FilterTags.Arcade | FilterTags.Racing;
        while (enabled)
        {
            switch (tags)
            {
                case 0:
                    break;
            }
        }
    }
    //tim code
    private void DoSort()
    {
        Debug.Log("Before sorting:");
        for (int i = 0; i < games.Count; i++)
        {
            Debug.Log("Game: " + games[i].ToString());
        }
        //Gebruikt linq
        games = games.OrderBy(game => game).ToList();

        // if(Game(0).Category....

        Debug.Log("After sorting:");
        for (int i = 0; i < games.Count; i++)
        {
            Debug.Log("Game: " + games[i].ToString());
        }
    } 


    public void SortNames()
    {
        for (int i = 0; i < AAAAH.Length; i++)
        {
            AAAAH[i].text = names[i];
            sortAlphabet = false;
            
        }
        if (!sortAlphabet)
        {
            AAAAH = null;
            sortAlphabet = true;
        }
        
    }
}
