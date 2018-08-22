using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    bool inputLock = false;
    int rows;
    public float moveSpeed = 0.5f;
    public float shuffleSpeed = 0.1f;
    public int shuffleCount = 20;
    

    public List<TileController> tiles;

    private void Start()
    {
        rows = (int)Mathf.Sqrt(tiles.Count);
        

        StartCoroutine(Shuffle());

    }


    IEnumerator Shuffle()
    {
        for(int i = 0; i < shuffleCount;)
        {
            if(TilePressed(tiles[Random.Range(0, tiles.Count)], true))
            {
                i++;
                yield return new WaitForSeconds(shuffleSpeed);
            }

        }
        Debug.Log("Shuffle done");
    }


    /*void ShuffleBoard()
    { 
        for(int i = 0; i < 500; i++)
        {
            TilePressed(tiles[Random.Range(0, tiles.Count)]);
        }
    }
    */


    public bool TilePressed(TileController tile, bool shuffle)
    {
        if (inputLock)
            return false;

        //Debug.Log("Clickyclick on " + tile.gameObject.name);

        //SwitchTiles(tile, tiles[8]);

        TileController emptyNeighbour = EmptyNeighbour(tile);

        if (emptyNeighbour == null)
            return false;

        float speed = 0f;
        if (shuffle)
            speed = shuffleSpeed;
        else
            speed = moveSpeed;

        SwitchTiles(tile, emptyNeighbour, speed);
        return true;
        
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

    void SwitchTiles(TileController tile1, TileController tile2, float speed)
    {
        inputLock = true;

        Vector3 pos = tile1.gameObject.transform.position;

        LeanTween.move(tile1.gameObject, tile2.gameObject.transform.position, speed)
            .setEase(LeanTweenType.easeInSine)
            .setOnComplete(() => {
                inputLock = false;
            });
        LeanTween.move(tile2.gameObject, pos, speed).setEase(LeanTweenType.easeInSine);

        //tile1.gameObject.transform.position = tile2.gameObject.transform.position;
        //tile2.gameObject.transform.position = pos;

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
