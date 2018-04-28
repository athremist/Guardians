using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    Vector2 StartPostition;
    Vector2 EndPostition;
    Vector2 Dirction;

    void Start()
    {
        StartPostition = gameObject.transform.position;
        EndPostition = Vector2.zero;

    }

    void Update()
    {
        //TODO: If input tile.nextTile
        //TODO: implement turning AS WELL AS moving. Maybe?(Turning will set nextTile and moving will move to it.)
    }


}
