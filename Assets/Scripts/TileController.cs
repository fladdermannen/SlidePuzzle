using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    public bool empty = false;
    public TileManager tileManager;


    private void OnMouseDown()
    {
        //Debug.Log("Clickyclick on " + gameObject.name);
        tileManager.TilePressed(this);
    }











}
