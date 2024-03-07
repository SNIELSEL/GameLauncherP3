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

    public BuildYear buildYear;
    public StudentYear studentYear;
    //public FilterTags filterTags;
    public List<FilterTags> games;

    bool sortAlphabet;
    // Execute the query to produce the results
    private void Start()
    {

        IEnumerable<int> scoreQuery = //query variable
        from score in primeNumers //required
        where score > 0// optional
        orderby score descending // optional
        select score; //must end with select or group

        // Array.Sort(names, (x, y) => String.Compare(x, y));
        Array.Sort(names);
        SortNames();

        /* static void DisplayValues(String[] arr)
         {
             for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0);
                   i++)
             {
                 Debug.Log( arr[i]);

             }   
             Console.WriteLine();
         }*/



        // je begint een query expression met een from en hij eindigt bij de laatste select of group.
        foreach (var testScore in scoreQuery)
        {
            Debug.Log(testScore);
           
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

        games = games.OrderBy(game => game).ToList();

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
