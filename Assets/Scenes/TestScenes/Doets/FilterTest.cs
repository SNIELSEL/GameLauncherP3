using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FilterTest : MonoBehaviour
{
     //int[] scores = { 10, 92, 02, 56, 48, 23 };
     List<int> primeNumers = new List<int>();

     List<string> names = new List<string>();

    // Execute the query to produce the results
    private void Start()
    {
        primeNumers.Add(10);
        primeNumers.Add(92);
        primeNumers.Add(02);
        primeNumers.Add(56);
        primeNumers.Add(48);
        primeNumers.Add(23);

        names.Add("ewuifeiuw");
        names.Add("alaneiono");
        names.Add("wiqwqbnoin");
        names.Add("opomjpnmmm");

        IEnumerable<int> scoreQuery = //query variable
        from score in primeNumers //required
        where score > 0// optional
        orderby score descending // optional
        select score; //must end with select or group


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
