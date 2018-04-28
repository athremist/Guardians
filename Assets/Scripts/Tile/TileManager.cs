﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    Tile[] MapTiles;
    Vector3 shit;

    void Start()
    {
        InitializeTiles();
    }

    void Update()
    {
        Debug.DrawLine(shit, new Vector3(shit.x, shit.y + 27, 0));

    }

    void InitializeTiles()
    {
        //Used to extract data from tilemap to use for the tile data, due to it being a lot easier to build & design
        //the maps using the unity tilemaps as well as a lot faster.
        Tilemap map = gameObject.GetComponentInChildren<Tilemap>();

        Vector2Int mapSize = new Vector2Int(map.cellBounds.size.x, map.cellBounds.size.y);
        int numOfTiles = mapSize.x * mapSize.y;
        shit = map.origin;


        MapTiles = new Tile[numOfTiles];
        //Variables to store data from the tilemap to be used for initializing tiles.
        string name = "";
        Vector3Int pos = new Vector3Int(0, 0, 0);
        Tile tile;
        
        for (int i = 0; i < numOfTiles; i++)
        {
            ///<summary> Explanation of what's going on for x and y coords;
            ///interate through all the coordinates row by row, starting at the 1st row
            ///by using modulus % we are checking the remainder of tiles still in that row
            /// Ex: 5 % 20 = 5 meaning we are the 5th tile in the row 
            /// OR 25 % 20 = 5 also the 5th in THAT row which gives us 'x'
            /// for 'y' we are checking where we are in that column by diving by the amount
            /// of tiles in one row to determine if at the next row
            /// Ex: 5 / 20 = 0.25 which auto-rounds down to 0 since we're using 'int' (50/20=2.5=2)
            ///</ summary >
            pos.x = map.origin.x + Mathf.Abs(i % mapSize.x);
            pos.y = map.origin.y + Mathf.Abs(i / mapSize.x);

            if (map.GetSprite(pos) != null)
            {
                name = map.GetSprite(pos).name;
            }
            else
            {
                name = "";
                Debug.LogError("Sprite not loaded");
            }
            //TODO: Currently not setting all the tiles...
            if (name != "")
            {
                name = GetTileTypeNameFromSprite(name);

                tile = new Tile(new Vector2(pos.x, pos.y), (Tile.TileType)System.Enum.Parse(typeof(Tile.TileType), name));
                MapTiles[i] = tile;
                Debug.Log(tile.GetTileType());
            }
        }

        /*Tile tile;
        tile = new Tile();
        //example
        if ((Pickup)tile.Object() != null)
        {

        }*/
        //Debug.Log(name);
    }

    //Extract the type of tile from the name of the sprite
    string GetTileTypeNameFromSprite(string aSpriteName)
    {
        string name = aSpriteName;

        while (name.EndsWith("_") == false)//Trim until '_'
        {
            name = name.Substring(0, name.Length - 1);
        }

        name = name.Substring(0, name.Length - 1);//NOW trim the '_'

        return name;
    }
}