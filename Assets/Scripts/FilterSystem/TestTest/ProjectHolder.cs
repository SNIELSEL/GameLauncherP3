using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using UnityEngine.XR;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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
    //public Filter filter;

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

    public void ApplyFilter()
    {
        foreach(GameObject button in filterButtons)
        {
            if (button.GetComponent<Toggle>().isOn)
            {
                toggleOn = true;

                if (toggleOn)
                {
                    FilterOnThis(button.GetComponent<FilterButton>().rating, button.GetComponent<FilterButton>().buildYear, button.GetComponent<FilterButton>().studentYear, button.GetComponent<FilterButton>().multiPlay, button.GetComponent<FilterButton>().perspective, button.GetComponent<FilterButton>().genre);
                    Debug.Log(button.GetComponent<FilterButton>().rating);
                    Debug.Log(button.GetComponent<FilterButton>().buildYear);
                    Debug.Log(button.GetComponent<FilterButton>().studentYear);
                    Debug.Log(button.GetComponent<FilterButton>().multiPlay);
                    Debug.Log(button.GetComponent<FilterButton>().perspective);
                    Debug.Log(button.GetComponent<FilterButton>().genre);
                }
                
                toggleOn = false;
            }
        }
    }

    public void FilterOnThis(int ratingToFilterTo,int buildYearToFilterTo, string studentYearToFilterTo, string multiPlayToFilterTo, string perspectiveToFilterTo, string genreToFilterTo)
    {

        List<Product> filteredList = new List<Product>();
       // List<GameObject> gameList = new List<GameObject>();
        foreach(Product product in products)
        {
            filteredList.Add(product);
        }

        foreach(GameObject game in games)
        {
            foreach (Product product in products)
            {
                if(ratingToFilterTo < 0 && product.filter.rating != ratingToFilterTo)
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
               game.SetActive(true);
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
