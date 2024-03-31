using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectHolder : MonoBehaviour
{
    public List<Product> products = new List<Product>();
    [SerializeField] private GameObject requireCheckHolder;
    [SerializeField] private GameObject[] requirements;
    [SerializeField] private GameObject gameHolder;
    [SerializeField] private GameObject[] games;
    public List<GameObject> gameButtons = new List<GameObject>();
    
    public List<GameObject>  filterButtons = new List<GameObject>();
    bool toggleOn;
    int totalRequirements, totalGames;
    float waitTime = 3;

    public SetFilter setFilter = new SetFilter();

    public void Start()
    {
        foreach(Product product in products)
        {
            product.Init();
        }

        Invoke("GetListOfGames", waitTime);
    }

    public void GetListOfGames()
    {
        totalRequirements = requireCheckHolder.transform.childCount;
        requirements = new GameObject[totalRequirements];

        for (int i = 0; i < totalRequirements; i++)
        {
            requirements[i] = requireCheckHolder.transform.GetChild(i).gameObject;

            products.Add(requirements[i].GetComponent<Product>());
        }

        totalGames = gameHolder.transform.childCount;
        games = new GameObject[totalGames];
        for (int i = 0;i < totalGames; i++)
        {
            games[i] = gameHolder.transform.GetChild(i).gameObject;

           gameButtons.Add(games[i]);
        }
        
    }
    public void RatingFilter(GameObject rating)
    {
        setFilter.ratingToFilterTo = rating.GetComponent<FilterButton>().rating;
    }
    public void BuildYearFilter(GameObject buildYear)
    {
        setFilter.buildYearToFilterTo = buildYear.GetComponent<FilterButton>().buildYear;
    }

    public void StudentYearFilter(GameObject studentYear)
    {
        setFilter.studentYearToFilterTo = studentYear.GetComponent<FilterButton>().studentYear;
    }

    public void MultiplayerFilter(GameObject multiPlayer)
    {
        setFilter.multiPlayToFilterTo = multiPlayer.GetComponent<FilterButton>().multiPlay;
    }

    public void PerspectiveFilter(GameObject perspective)
    {
        setFilter.perspectiveToFilterTo = perspective.GetComponent<FilterButton>().perspective;
    }

    public void GenreFilter(GameObject genre)
    {
        setFilter.genreToFilterTo = genre.GetComponent<FilterButton>().genre;

    }

    public void ApplyFilter()
    {
        FilterOnThis(setFilter.ratingToFilterTo, setFilter.buildYearToFilterTo, setFilter.studentYearToFilterTo, setFilter.multiPlayToFilterTo, setFilter.perspectiveToFilterTo, setFilter.genreToFilterTo);
    }

    public void FilterOnThis(int ratingToFilterTo,int buildYearToFilterTo, string studentYearToFilterTo, string multiPlayToFilterTo, string perspectiveToFilterTo, string genreToFilterTo)
    {

        List<Product> filteredList = new List<Product>();
        List<GameObject> games = new List<GameObject>();
        // List<GameObject> gameList = new List<GameObject>();
        foreach (Product product in products)
        {
            filteredList.Add(product);
        }

        foreach(GameObject game in games)
        {
            foreach (Product product in products)
            {
                if(ratingToFilterTo > 0 && product.filter.rating != ratingToFilterTo)
                {
                    filteredList.Remove(product);
                    game.SetActive(false);
                }
                if(buildYearToFilterTo > 0 && product.filter.buildYear != buildYearToFilterTo)
                {
                    filteredList.Remove(product);
                    game.SetActive(false);
                }
                if(studentYearToFilterTo != "" && product.filter.studentYear != studentYearToFilterTo)
                {
                    filteredList.Remove(product);
                    game.SetActive(false);
                }
                if(multiPlayToFilterTo != "" && product.filter.multiTag != multiPlayToFilterTo)
                {
                    filteredList.Remove(product);
                    game.SetActive(false);
                }
                if (perspectiveToFilterTo != "" && product.filter.perspectiveTag != perspectiveToFilterTo)
                {
                    filteredList.Remove(product);
                    game.SetActive(false);
                }
                if (genreToFilterTo != "" && product.filter.genreTag != genreToFilterTo)
                {
                    filteredList.Remove(product);
                    game.SetActive(false);
                }
            }
            
            foreach (Product product in filteredList)
            {
                //hier moeten de laatste games overblijven
                print(product.name);
                foreach (GameObject gaaaame in games)
                {
                    game.SetActive(true);
                }
            }
        }

    }

    public void PutGamesBack()
    {
        foreach(GameObject game in games)
        {
            game.SetActive(true);
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