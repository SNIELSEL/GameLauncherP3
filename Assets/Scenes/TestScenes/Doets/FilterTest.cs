using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FilterTest : MonoBehaviour
{
     //int[] scores = { 10, 92, 02, 56, 48, 23 };
     List<int> primeNumers = new List<int>();

    // Execute the query to produce the results
    private void Start()
    {
        primeNumers.Add(10);
        primeNumers.Add(92);
        primeNumers.Add(02);
        primeNumers.Add(56);
        primeNumers.Add(48);
        primeNumers.Add(23);

        IEnumerable<int> scoreQuery = //query variable
        from score in primeNumers //required
        where score > 0// optional
        orderby score descending // optional
        select score; //must end with select or group

        foreach (var testScore in scoreQuery)
        {
            Debug.Log(testScore);
        }
    }
}
