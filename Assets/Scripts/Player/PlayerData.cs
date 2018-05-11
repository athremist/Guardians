using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    string Name = "RED";
    //money/name/badges/bag?/pokedex?/etc
    public World World { get; private set; }
    public Map CurrentMap { get; private set; }
    public Tile CurrentTile;

    //For camera, 10x9 tiles
    void Start()
    {
        World = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        CurrentMap = World.GetMap(World.StartingMap);
        CurrentTile = CurrentMap.GetTile(250);
        transform.position = new Vector3(CurrentTile.GetTileCoordinates().x + 0.5f, CurrentTile.GetTileCoordinates().y + 0.5f);
    }

    void Update()
    {

    }

    public void ChangeMap()
    {
        CurrentMap = CurrentTile.GetMap();
    }
}
