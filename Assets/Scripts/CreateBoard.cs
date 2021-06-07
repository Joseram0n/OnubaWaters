using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{

    [SerializeField] int height = 10;
    [SerializeField] int width = 10;
    [SerializeField] float gridSpaceSize = 1f;
    [SerializeField] GameObject gridCellPrefab;
    private GameObject[,] gameGrid;
    // Start is called before the first frame update
    void Start()
    {
        CreaeGrid();
    }

    // Update is called once per frame


    //Genera el grid segun la altura y la anchura
    private void CreaeGrid()

    {

        gameGrid = new GameObject[height, width];

        if(gridCellPrefab == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab is not assigned");
            return;
        }

        // crear grid
        for(int z = 0; z < height; z++)
        {
            for( int x = 0; x < width; x++)
            {
                gameGrid[x, z] = Instantiate(gridCellPrefab, new Vector3(x * gridSpaceSize, 0, z * gridSpaceSize), Quaternion.identity);
                gameGrid[x, z].transform.parent = transform;
                gameGrid[x, z].gameObject.name = "GridCell (X: " + x.ToString() + " , Z: " + z.ToString() + ")";
            }
        }
    }
}
