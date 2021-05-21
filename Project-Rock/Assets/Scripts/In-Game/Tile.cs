using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private bool isTraversable;
    [SerializeField]
    private bool isPlayer1;
    [SerializeField]
    private int tileIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetIsPlayer1()
    {
        return isPlayer1;
    }

    public bool GetIsTraversable()
    {
        return isTraversable;
    }

    public int GetTileIndex()
    {
        return tileIndex;
    }

    public void SetTileIndex(int index)
    {
        tileIndex = index;
    }

    public void SetIsTraversable(bool traversable)
    {
        isTraversable = traversable;
    }

    public bool HasTileUp()
    {
        int tempIndex = tileIndex - 8;

        if(tempIndex < 0)
        {
            return false;
        }

        return true;
    }

    public bool HasTileRight()
    {
        int tempIndex = tileIndex + 1;

        if (tempIndex % 8 == 0)
        {
            return false;
        }

        return true;
    }

    public bool HasTileDown()
    {
        int tempIndex = tileIndex + 8;

        if (tempIndex > 31)
        {
            return false;
        }

        return true;
    }

    public bool HasTileLeft()
    {
        int tempIndex = tileIndex - 1;

        if (tempIndex % 8 == 7)
        {
            return false;
        }

        return true;
    }
}
