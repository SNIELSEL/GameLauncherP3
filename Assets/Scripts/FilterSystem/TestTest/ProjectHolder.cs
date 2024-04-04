using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectHolder : MonoBehaviour
{
    public List<Product> products = new List<Product>();
    [SerializeField] private GameObject requireCheckHolder;


    public List<GameObject> filterButtons = new List<GameObject>();
    int totalRequirements, totalGames;


    public SetFilter setFilter = new SetFilter();
    //deze voif aanspreken in dylano zijn start
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

    public void RatingFilter(int rating)
    {
        //SLIDER AAAAAAAAAAAAAh
        //setFilter.ratingToFilterTo = rating.GetComponent<FilterButton>().rating;

       /* else
        {
            setFilter.ratingToFilterTo = 0;
        }*/
    }
    public void BuildYearFilter(GameObject buildYear)
    {

        if (buildYear.GetComponent<Toggle>().isOn)
        {
            setFilter.buildYearToFilterTo = buildYear.GetComponent<FilterButton>().buildYear;
        }
        else
        {
            setFilter.buildYearToFilterTo = 0;
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
            setFilter.studentYearToFilterTo = null;
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
            setFilter.multiPlayToFilterTo = null;
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
            setFilter.perspectiveToFilterTo = null;
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
            setFilter.genreToFilterTo = null;
        }
    }

    public void ApplyFilter()
    {
       
        FilterOnThis(setFilter.ratingToFilterTo, setFilter.buildYearToFilterTo, setFilter.studentYearToFilterTo, setFilter.multiPlayToFilterTo, setFilter.perspectiveToFilterTo, setFilter.genreToFilterTo);
    }

  
    public void FilterOnThis(int ratingToFilterTo, int buildYearToFilterTo, string studentYearToFilterTo, string multiPlayToFilterTo, string perspectiveToFilterTo, string genreToFilterTo)
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
        foreach(Product g in products)
        {
            g.gameObject.SetActive(false);
        }
        //Zet elk product in de filteredLIst weer aan.
        foreach(Product p in filteredList)
        {
            p.gameObject.SetActive(true);
        }
    }

   
    public void PutGamesBack()
    {
        foreach(Product product in products)
        {
            //product.GetComponent<RequirmentCheck>().gfuckingKnop.SetActive(true);
        }
    }
}

[System.Serializable]
public class SetFilter
{
    public int ratingToFilterTo;
    public int buildYearToFilterTo;
    public string studentYearToFilterTo;
    public string multiPlayToFilterTo;
    public string perspectiveToFilterTo;
    public string genreToFilterTo;

}