using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FilterTest : MonoBehaviour
{
    //int[] scores = { 10, 92, 02, 56, 48, 23 };
    int[] primeNumers = { 10, 92, 02, 56, 48, 23 };

    //List<string> names = new List<string>();
     string[] names = {"eeeeeh", "nessie", "haaaaaaaaai", "doeeeeei", "peerv", "jezus", "naast" };

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
        Console.WriteLine("After sorting the entire array by using the default comparer:");
        DisplayValues(names);


        static void DisplayValues(String[] arr)
        {
            for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0);
                  i++)
            {
                Debug.Log( arr[i]);
            }
            Console.WriteLine();
        }

        /* IEnumerable<string> nameQuery = //query variable
         from name in names //required
         //where name// optional
         orderby name ascending // optional
         select name; //must end with select or group*/

        // je begint een query expression met een from en hij eindigt bij de laatste select of group.
        foreach (var testScore in scoreQuery)
        {
            Debug.Log(testScore);

        }
        foreach (var name in names)
        {
            Debug.Log(name);
        }
    }
}
