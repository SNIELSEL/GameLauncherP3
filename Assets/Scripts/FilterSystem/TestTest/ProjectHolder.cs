using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

public class ProjectHolder : MonoBehaviour
{
    public List<Product> products = new List<Product>();
    [SerializeField] private GameObject requireCheckHolder;
    [SerializeField] private GameObject[] requirements;
    [SerializeField] private GameObject gameHolder;
    [SerializeField] private GameObject[] games;
    

    public List<FilterButton>  filterButtons = new List<FilterButton>();
    int totalRequirements, totalGames;
    float waitTime = 5;
    public Filter filter;

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
        }
        
    }

    public void ApplyFilter()
    {
        for (int i = 0; i <= filterButtons.Count; i++)
        {
            if (filterButtons[i] == true)
            {
                FilterOnThis(filterButtons[i].rating, filterButtons[i].buildYear, filterButtons[i].studentYear, filterButtons[i].multiPlay, filterButtons[i].perspective, filterButtons[i].genre);
            }
        }
    }


    public void FilterOnThis(int ratingToFilterTo,int buildYearToFilterTo, string studentYearToFilterTo, string multiPlayToFilterTo, string perspectiveToFilterTo, string genreToFilterTo)
    {

        List<Product> filteredList = new List<Product>();
        foreach(Product product in products)
        {
            filteredList.Add(product);
        }

        foreach(GameObject game in games)
        {
            foreach (Product product in products)
            {
                //filtert door alles en als hij lager is dan 0 gooit hij hem uit de lijst
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
                    print("ehm");
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
                game.SetActive(true);
            }
        }



    }
}
