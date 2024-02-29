using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    [SerializeField] private enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    [SerializeField] private FitType fitType;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private Vector2 cellSize;

    [SerializeField] private int columnCount;
    [SerializeField] private int rowCount;
    [SerializeField] private Vector2 spacing;
    [SerializeField] private bool fitX;
    [SerializeField] private bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        //basis method die word gecalled in een layoutgroep
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;
            //berekening hoeveelheid rows en columns
            float sqrRT = Mathf.Sqrt(transform.childCount);
            columns = Mathf.CeilToInt(sqrRT);
            rows = Mathf.CeilToInt(sqrRT);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }

        if(fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }
        
        //lengte en breedte berekening
        float  parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        //grootte van de child objects
        float cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.left / (float)columns);
        float cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        //counts for columns en rows
        columnCount = 0;
        rowCount = 0;

        //forloop die telt hoeveel children er zijn en posisioneert ze met de info die hierboven word berekend
        for (int i = 0; i < rectChildren.Count; i++) 
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x = columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y = rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 0, yPos, cellSize.y);
        }
     }
    public override void CalculateLayoutInputVertical()
    {

    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }
}
