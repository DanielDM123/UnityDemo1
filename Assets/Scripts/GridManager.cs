using UnityEngine;
using Project;
using System.Collections;
using System.Collections.Generic;
using System;
using Mono.Cecil;

public class GridManager : MonoBehaviour
{
    public int width = 8;
    public int height = 4;

    public GameObject gridCellPrefab; // Set a prefab for the grid outline in unity inspector
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[,] gridCells; // [,] is a 2D array

    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        gridCells = new GameObject[width, height];
        // FInd the center of the grid
        Vector2 centerOffset = new Vector2(width / 2f - 0.5f, height / 2f - 0.5f); // 0.5 is half the width of the grid box

        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y< height; y++)
            {
                Vector2 currentCell = new Vector2(x, y);
                Vector2 spawnPostion = currentCell - centerOffset; // insead of having the origin being in the bottom left corner, set it to the center

                GameObject gridCell = Instantiate(gridCellPrefab, spawnPostion, Quaternion.identity);
                gridCell.transform.SetParent(transform); // SEt this cell's parent to this grid manager

                gridCell.GetComponent<GridCell>().gridIndex = currentCell;
                gridCells[x,y] = gridCell; 
            }
        }
    }

    public bool AddObjectToGrid(GameObject obj, Vector2 gridPosition)
    {
      
        // Make sure we are in-bounds
        if (gridPosition.x >= width && gridPosition.x < 0 && gridPosition.y >= height && gridPosition.y < 0)
        {
            return false;
        }

        GridCell cell = gridCells[(int)gridPosition.x, (int)gridPosition.y].GetComponent<GridCell>();

        if (cell.cellFull)
        {
            return false;
        }
        else
        {
            GameObject newObj = Instantiate(obj, cell.GetComponent<Transform>().position, Quaternion.identity);
            newObj.transform.SetParent(transform);

            gridObjects.Add(newObj);
            cell.objectInCell = newObj;
            cell.cellFull = true;
            return true;
        }
    }

}
