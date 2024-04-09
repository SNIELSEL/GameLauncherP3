using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

public class ProjectHolder : MonoBehaviour
{

    /*NOTE TO SELF>>>>
     * Dylano even zeggen dat er een product op de games moeten anders ka ik niet filteren: DONE
     * Noets melden over dat ik een toggle nodig heb waarmee me de slider aan en uit kunmnen zetten, ik heb het script ervoor al maar de slider moet eigenlijk grijs worden ipv uitgaan zodat je hem altijd kunt zien: DONE maar nog een keer doen voor de zekerheid
     * alles in main scene gOED ASSIGNEN: wachten tot dylano klaar is in main scene
    */

    [SerializeField] private GameObject requireCheckHolder;
    public List<Product> products = new List<Product>();

    public Slider ratingSlider;

    //Niels MostPlayed
    public List<string> gameNames;
    public List<float> gameTimes;


    public List<GameObject> filterButtons = new List<GameObject>();
    int totalRequirements;


    public SetFilter setFilter = new SetFilter();
   
   //Void wordt aangesproken in FilePath van Dylano
    public void Init()
    {
        foreach (Product product in products)
        {
            product.Init();
        }

        GetListOfGames();
    }

    public void GetListOfGames()
    {
        totalRequirements = requireCheckHolder.transform.childCount;
        GameObject[] requirements = new GameObject[totalRequirements];

        for (int i = 0; i < totalRequirements; i++)
        {
            requirements[i] = requireCheckHolder.transform.GetChild(i).gameObject;

            products.Add(requirements[i].GetComponent<Product>());
        }
    }

    public void RatingFilter(GameObject rating)
    {
        //SLIDER AAAAAAAAAAAAAh
        //we houden vanb sliders echt mijhn fvqavoriet
        setFilter.ratingToFilterTo = rating.GetComponent<Slider>().value;
    }
    public void BuildYearFilter(GameObject buildYear)
    {

        if (buildYear.GetComponent<Toggle>().isOn)
        {
            setFilter.buildYearToFilterTo = buildYear.GetComponent<FilterButton>().buildYear;
        }
        else
        {
            setFilter.buildYearToFilterTo = -1;
        }

    }

    public void StudentYearFilter(GameObject studentYear)
    {
        if (studentYear.GetComponent<Toggle>().isOn)
        {
            setFilter.studentYearToFilterTo = studentYear.GetComponent<FilterButton>().studentYear;
        }
        else
        {
            setFilter.studentYearToFilterTo = "";
        }

    }

    public void MultiplayerFilter(GameObject multiPlayer)
    {
        if (multiPlayer.GetComponent<Toggle>().isOn)
        {
            setFilter.multiPlayToFilterTo = multiPlayer.GetComponent<FilterButton>().multiPlay;
        }
        else
        {
            setFilter.multiPlayToFilterTo = "";
        }
    }

    public void PerspectiveFilter(GameObject perspective)
    {
        if (perspective.GetComponent<Toggle>().isOn)
        {
            setFilter.perspectiveToFilterTo = perspective.GetComponent<FilterButton>().perspective;
        }
        else
        {
            setFilter.perspectiveToFilterTo = "";
        }

    }

    public void GenreFilter(GameObject genre)
    {
        if (genre.GetComponent<Toggle>().isOn)
        {
            setFilter.genreToFilterTo = genre.GetComponent<FilterButton>().genre;
        }
        else
        {
            setFilter.genreToFilterTo = "";
        }
    }

    public void ApplyFilter()
    {

        FilterOnThis(setFilter.ratingToFilterTo, setFilter.buildYearToFilterTo, setFilter.studentYearToFilterTo, setFilter.multiPlayToFilterTo, setFilter.perspectiveToFilterTo, setFilter.genreToFilterTo);
    }


    public void FilterOnThis(float ratingToFilterTo, int buildYearToFilterTo, string studentYearToFilterTo, string multiPlayToFilterTo, string perspectiveToFilterTo, string genreToFilterTo)
    {

        List<Product> filteredList = new List<Product>();

        foreach (Product product in products)
        {
            filteredList.Add(product);
        }


        foreach (Product product in products)
        {
            //rating moet even op een andere manier
            if (ratingToFilterTo > 0 && product.filter.rating != ratingToFilterTo)
            {
                filteredList.Remove(product);
            }
            if (buildYearToFilterTo > 0 && product.filter.buildYear != buildYearToFilterTo)
            {
                filteredList.Remove(product);
            }
            if (studentYearToFilterTo != "" && product.filter.studentYear != studentYearToFilterTo)
            {
                filteredList.Remove(product);
            }
            if (multiPlayToFilterTo != "" && product.filter.multiTag != multiPlayToFilterTo)
            {
                filteredList.Remove(product);
            }
            if (perspectiveToFilterTo != "" && product.filter.perspectiveTag != perspectiveToFilterTo)
            {
                filteredList.Remove(product);
            }
            if (genreToFilterTo != "" && product.filter.genreTag != genreToFilterTo)
            {
                filteredList.Remove(product);
            }

        }
        // Zet elk product uit.
        foreach (Product g in products)
        {
            g.gameObject.SetActive(false);
        }
        //Zet elk product in de filteredLIst weer aan.
        foreach (Product p in filteredList)
        {
            p.gameObject.SetActive(true);
            
        }
        filteredList.Clear();
    }


    public void PutGamesBack()
    {
        foreach (Product product in products)
        {
            product.gameObject.SetActive(true);
        }
    }

    //FUCK DIT HOE WERKT DIT MOET NOG AF WHY THIS DO THIS IK WIL NIET MEER........... deze hele functie werkt niet bruhhhh
    public void MostPlayed()
    {
        // nu de array sorteren en daarbij de goede games bovenaan zetten :')
        gameTimes.Clear();

        for (int i = 0; i < gameNames.Count; i++)
        {
            gameTimes.Add(PlayerPrefs.GetInt("PlayTimeMinutes" + gameNames[i]) + (PlayerPrefs.GetInt("PlayTimeHours" + gameNames[i]) * 60));
        }
        





        Debug.Log(gameTimes);

    }
}

[System.Serializable]
public class SetFilter
{
    public float ratingToFilterTo;
    public int buildYearToFilterTo;
    public string studentYearToFilterTo;
    public string multiPlayToFilterTo;
    public string perspectiveToFilterTo;
    public string genreToFilterTo;

}