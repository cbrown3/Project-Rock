using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Tile[] fullGrid;

    public static GridManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < fullGrid.Length; i++)
        {
            fullGrid[i].SetTileIndex(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tile GetTile(int column, int row)
    {
        int index = 0;

        index += 8 * (row - 1);

        index += column - 1;

        return fullGrid[index];
    }

    public Tile GetTile(int index)
    {
        return fullGrid[index];
    }
}
