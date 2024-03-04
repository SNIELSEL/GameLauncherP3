using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class GameProfile : MonoBehaviour
{
   /* public BuildYear buildYear;
    public StudentYear studentYear;*/


    // Start is called before the first frame update
    void Start()
    {
        // In start, kijk naar het tekstbestand van de game. En vul de data in.
        string text = File.ReadAllText("YourTextFile");

        char[] separators = { ',', ';', '|' };
        string[] strValues = text.Split(separators);

        List<int> intValues = new List<int>();
        foreach (string str in strValues)
        {
            int val = 0;
            if (int.TryParse(str, out val))
                intValues.Add(val);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // checks if bools are turned on to filter

    }
}
