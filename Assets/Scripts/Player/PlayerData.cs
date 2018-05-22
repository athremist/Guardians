using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    const int STARTING_TILE = 238;//Index

    string Name = "RED";
    //money/name/badges/bag?/pokedex?/etc
    public World World { get; private set; }
    public Map CurrentMap { get; private set; }
    public Tile CurrentTile;

    public Bag Bag;

    //For camera, 10x9 tiles
    void Start()
    {
        World = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        CurrentMap = World.GetMap(World.StartingMap);
        CurrentTile = CurrentMap.GetTile(STARTING_TILE);
        transform.position = new Vector3(CurrentTile.GetTileCoordinates().x, CurrentTile.GetTileCoordinates().y);

        Bag = new Bag();
    }

    public void ChangeMap()
    {
        CurrentMap = CurrentTile.GetMap();
    }
}
