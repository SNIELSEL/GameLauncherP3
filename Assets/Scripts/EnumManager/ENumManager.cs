using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENumManager : MonoBehaviour
{
    public RequirmentCheck requirmentCheck;
    // Volgorde   BuildYear teacherScore studentYear filterTags, multitag, perspectivetag,;
    
    public void ExecuteAllVoids()
    {
        BuildYearENumManager();
        ScoreENumManager();
        StudentYearENumManager();
        GanreTagENumManager();
        MultiPlayENumManager();
        PerspectiveTagENumManeger();
    }
    
    private void BuildYearENumManager()
    {
        if(requirmentCheck.year == 2020)
        {
            requirmentCheck.buildYear = BuildYear.Year2020;
        }

        if (requirmentCheck.year == 2021)
        {
            requirmentCheck.buildYear = BuildYear.Year2021;
        }

        if (requirmentCheck.year == 2022)
        {
            requirmentCheck.buildYear = BuildYear.Year2022;
        }

        if (requirmentCheck.year == 2023)
        {
            requirmentCheck.buildYear = BuildYear.Year2023;
        }

        if (requirmentCheck.year == 2024)
        {
            requirmentCheck.buildYear = BuildYear.Year2024;
        }

        if (requirmentCheck.year == 2025)
        {
            requirmentCheck.buildYear = BuildYear.Year2025;
        }

        if (requirmentCheck.year == 2026)
        {
            requirmentCheck.buildYear = BuildYear.Year2026;
        }

        if (requirmentCheck.year == 2027)
        {
            requirmentCheck.buildYear = BuildYear.Year2027;
        }

        if (requirmentCheck.year == 2028)
        {
            requirmentCheck.buildYear = BuildYear.Year2028;
        }

        if (requirmentCheck.year == 2029)
        {
            requirmentCheck.buildYear = BuildYear.Year2029;
        }

        if (requirmentCheck.year == 2030)
        {
            requirmentCheck.buildYear = BuildYear.Year2030;
        }
    }

    private void ScoreENumManager()
    {
        if (requirmentCheck.docentenScore == "2")
        {
            requirmentCheck.teacherScore = TeacherScore.stars2;
        }

        if (requirmentCheck.docentenScore == "3")
        {
            requirmentCheck.teacherScore = TeacherScore.stars3;
        }

        if (requirmentCheck.docentenScore == "4")
        {
            requirmentCheck.teacherScore = TeacherScore.stars4;
        }

        if (requirmentCheck.docentenScore == "5")
        {
            requirmentCheck.teacherScore = TeacherScore.stars5;
        }

        if (requirmentCheck.docentenScore == "6")
        {
            requirmentCheck.teacherScore = TeacherScore.stars6;
        }

        if (requirmentCheck.docentenScore == "7")
        {
            requirmentCheck.teacherScore = TeacherScore.stars7;
        }

        if (requirmentCheck.docentenScore == "8")
        {
            requirmentCheck.teacherScore = TeacherScore.stars8;
        }

        if (requirmentCheck.docentenScore == "9")
        {
            requirmentCheck.teacherScore = TeacherScore.stars9;
        }

        if (requirmentCheck.docentenScore == "10")
        {
            requirmentCheck.teacherScore = TeacherScore.stars10;
        }
    }

    private void StudentYearENumManager()
    {
        if (requirmentCheck.leerJaar == "Staat 1")
        {
            requirmentCheck.studentYear = StudentYear.Year1;
        }

        if (requirmentCheck.leerJaar == "Staat 2")
        {
            requirmentCheck.studentYear = StudentYear.Year2;
        }

        if (requirmentCheck.leerJaar == "Staat 3")
        {
            requirmentCheck.studentYear = StudentYear.Year3;
        }

        if (requirmentCheck.leerJaar == "Staat 4")
        {
            requirmentCheck.studentYear = StudentYear.Year4;
        }

        if (requirmentCheck.leerJaar == "Docent")
        {
            requirmentCheck.studentYear = StudentYear.Teacher;
        }
    }

    private void GanreTagENumManager()
    {
        if (requirmentCheck.tag3 == "Action")
        {
            requirmentCheck.ganreTag = GanreTags.Action;
        }

        if (requirmentCheck.tag3 == "Adventure")
        {
            requirmentCheck.ganreTag = GanreTags.Adventure;
        }

        if (requirmentCheck.tag3 == "Arcade")
        {
            requirmentCheck.ganreTag = GanreTags.Arcade;
        }

        if (requirmentCheck.tag3 == "Casual")
        {
            requirmentCheck.ganreTag = GanreTags.Casual;
        }

        if (requirmentCheck.tag3 == "Racing")
        {
            requirmentCheck.ganreTag = GanreTags.Racing;
        }

        if (requirmentCheck.tag3 == "Simulation")
        {
            requirmentCheck.ganreTag = GanreTags.Simulation;
        }

        if (requirmentCheck.tag3 == "Strategy")
        {
            requirmentCheck.ganreTag = GanreTags.Strategy;
        }

        if (requirmentCheck.tag3 == "Puzzle")
        {
            requirmentCheck.ganreTag = GanreTags.Puzzle;
        }

        if (requirmentCheck.tag3 == "Party")
        {
            requirmentCheck.ganreTag = GanreTags.Party;
        }
    }

    private void MultiPlayENumManager()
    {
        if (requirmentCheck.tag1 == "SingelPlayer")
        {
            requirmentCheck.multiPlayTag = MultiPlayTag.SinglePlayer;
        }

        if (requirmentCheck.tag1 == "MultiPlayer")
        {
            requirmentCheck.multiPlayTag = MultiPlayTag.MultiPlayer;
        }
    }

    private void PerspectiveTagENumManeger()
    {
        if (requirmentCheck.tag2 == "FirstPerson")
        {
            requirmentCheck.perspectiveTag = PerspectiveTag.FirstPerson;
        }

        if (requirmentCheck.tag2 == "ThirdPerson")
        {
            requirmentCheck.perspectiveTag = PerspectiveTag.ThirdPerson;
        }

        if (requirmentCheck.tag2 == "TopDown")
        {
            requirmentCheck.perspectiveTag = PerspectiveTag.TopDown;
        }
    }

}