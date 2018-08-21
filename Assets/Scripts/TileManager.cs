using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    int rows;

    public List<TileController> tiles;

    private void Start()
    {
        rows = (int)Mathf.Sqrt(tiles.Count);
    }

    public void TilePressed(TileController tile)
    {
        //Debug.Log("Clickyclick on " + tile.gameObject.name);

        //SwitchTiles(tile, tiles[8]);

        TileController emptyNeighbour = EmptyNeighbour(tile);

        if(emptyNeighbour != null)
        {
            SwitchTiles(tile, emptyNeighbour);
        }
    }
	
    TileController EmptyNeighbour(TileController tile)
    {
        foreach(TileController t in Neighbours(tile))
        {
            if (t.empty)
                return t;
        }

        return null;
    }


    List<TileController> Neighbours(TileController tile)
    {
        int index = TileIndex(tile);
        List<TileController> neighbours = new List<TileController>();


        int over = index - rows;
        if(InRange(over))
        { 
            neighbours.Add(tiles[over]);
        }

        int under = index + rows;
        if(InRange(under))
        {
            neighbours.Add(tiles[under]);
        }

        int right = index + 1;
        if(InRange(right) && (right%rows != 0))
        {
            neighbours.Add(tiles[right]);
        }

        int left = index - 1;
        if(InRange(left) && (index%rows != 0))
        {
            neighbours.Add(tiles[left]);
        }


        return neighbours;
    }

    bool InRange(int index)
    {
        return (index >= 0 && index < tiles.Count);
    }

    void SwitchTiles(TileController tile1, TileController tile2)
    {
        Vector3 pos = tile1.gameObject.transform.position;
        tile1.gameObject.transform.position = tile2.gameObject.transform.position;
        tile2.gameObject.transform.position = pos;

        int index1 = TileIndex(tile1);
        int index2 = TileIndex(tile2);

        tiles[index1] = tile2;
        tiles[index2] = tile1;

    }

    int TileIndex(TileController tile)
    {
        return tiles.IndexOf(tile);
    }




}
