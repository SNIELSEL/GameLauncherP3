using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public Filter filter;

    public void Init()
    {
        filter = new Filter();
        //int randomYear = Random.Range(2000, 2005);
        //filter.SetFilter(rc.convertedDocentenScore, rc.year, rc.leerJaar, rc.tag1, rc.tag2, rc.tag3);
        //hier moeten dylano zijn variables in ^^^^
    }
}


[System.Serializable]
public class Filter
{
    public int rating;
    private int minRating = 0;
    private int maxRating = 5;

    public int buildYear;

    public string studentYear;

    public string multiTag;
    public string perspectiveTag;
    public string genreTag;


    public List<string> tags = new List<string>();

    public void SetFilter(int rating, int buildYear, string studentYear, string multiTag, string perspectiveTag, string genreTag)
    {
        GiveRating(rating);
        SetBuildYear(buildYear);
        SetStudentYear(studentYear);
        SetMultiTag(multiTag);
        SetPerspectiveTag(perspectiveTag);
        SetGenreTag(genreTag);
    }

    public void GiveRating(int ratingToGive)
    {
        if (ratingToGive < minRating) {
            ratingToGive = 0; 
        }else if (ratingToGive > maxRating)
        {
            ratingToGive = maxRating;
        }
        rating = ratingToGive;
    }

    public void SetStudentYear(string newSudentYear)
    {
        studentYear = newSudentYear;
    }

    public void SetBuildYear(int buildYearToSet)
    {
        buildYear = buildYearToSet;
    }

    public void SetMultiTag(string multiTagToSet)
    {
        multiTag = multiTagToSet;
    }

    public void SetPerspectiveTag(string perspectiveTagToSet)
    {
        perspectiveTag = perspectiveTagToSet;
    }

    public void SetGenreTag(string genreTagToSet)
    {
        genreTag = genreTagToSet;
    }
}
