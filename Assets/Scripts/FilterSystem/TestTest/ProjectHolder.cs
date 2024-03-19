using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

public class ProjectHolder : MonoBehaviour
{
    public List<Product> products = new List<Product>();
    [SerializeField] private GameObject gameHolder;
   /* public GameObject productHolder;
    int totalProducts;*/

    public void Start()
    {
        foreach(Product product in products)
        {
            product.Init();
        }

        for (int i = 0; i < gameHolder.transform.childCount - 1; ++i)
        {
            //products.Add(gameHolder.transform.GetChild(i));
        }

        FilterOnThis(-1,2003,"","BLA BLA", "", "" );
    }



    public void FilterOnThis(int ratingToFilterTo,int buildYearToFilterTo, string studentYearToFilterTo, string multiPlayToFilterTo, string perspectiveToFilterTo, string genreToFilterTo)
    {
        List<Product> filteredList = new List<Product>();
        foreach(Product product in products)
        {
            filteredList.Add(product);
        }

        foreach (Product product in products)
        {
            
            if(buildYearToFilterTo > 0 && product.filter.buildYear != buildYearToFilterTo)
            {
                filteredList.Remove(product);
            }
            if(ratingToFilterTo > 0 && product.filter.buildYear != ratingToFilterTo)
            {
                filteredList.Remove(product);
            }
            if(studentYearToFilterTo != "" && product.filter.studentYear != studentYearToFilterTo)
            {
                filteredList.Remove(product);
            }
            if(multiPlayToFilterTo != "" && product.filter.multiTag != multiPlayToFilterTo)
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

        foreach (Product product in filteredList)
        {
            print(product.name);
        }
    }
}
