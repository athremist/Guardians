using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class Map : MonoBehaviour
{
    private Tile[] MapTiles;
    private byte[] AdjacentMapIndexes;//0-North,1-East,2-South,3-West
    private int NumberHorizontalTiles;
    //Using a byte as we only need 4 numbers
    [HideInInspector]
    public int NumOfTiles;
    public bool TileDebugHelper = false;//For editor use only
    public PickupList[] Pickups;
    public DoorConnections[] ConnectedDoors;

    void Awake()
    {
        InitializeTiles();
        AdjacentMapIndexes = new byte[4];//One for each direction
    }

    void Start()
    {
        AddPickupToMap();

        if (TileDebugHelper)
        {
            gameObject.AddComponent<DebugDrawTileIndex>();
        }
    }

    void InitializeTiles()
    {
        //Used to extract data from tilemap to use for the tile data, due to it being a lot easier to build & design
        //the maps using the unity tilemaps as well as a lot faster.
        Tilemap map = gameObject.GetComponentInChildren<Tilemap>();

        Vector2Int mapSize = new Vector2Int(map.cellBounds.size.x, map.cellBounds.size.y);
        NumOfTiles = mapSize.x * mapSize.y;
        NumberHorizontalTiles = mapSize.y;

        MapTiles = new Tile[NumOfTiles];
        //Variables to store data from the tilemap to be used for initializing tiles.
        string name = "";
        Vector3Int pos = new Vector3Int(0, 0, 0);
        Vector3Int mapOriginWorld = Vector3Int.FloorToInt(map.CellToWorld(map.origin));//Worldpos origin
        float tileMapOffset = 0.5f;//Cuz tiles' anchor from a tilemap is the bottom left corner
        Tile tile;
        
        for (int i = 0; i < NumOfTiles; i++)
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
            //Get sprite name from tile from tilemap
            if (map.GetSprite(pos) != null)
            {
                name = map.GetSprite(pos).name;
            }
            else
            {
                name = "";
                Debug.LogError("Sprite not loaded");
            }
            //Now that we got the sprite name we will set tile position according to origin's world pos
            pos.x = mapOriginWorld.x + Mathf.Abs(i % mapSize.x);
            pos.y = mapOriginWorld.y + Mathf.Abs(i / mapSize.x);

            if (name != "")
            {
                name = GetTileTypeNameFromSprite(name);
                //Add offset for MY tile data ^_^
                tile = new Tile(this, new Vector2(pos.x + tileMapOffset, pos.y + tileMapOffset), 
                                (Tile.TileType)System.Enum.Parse(typeof(Tile.TileType), name));
                MapTiles[i] = tile;
            }
        }
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

    //Get the tile in front of the character(using coords)
    public Tile GetTile(Vector2 aCoords)
    {
        for (int i = 0; i < NumOfTiles; i++)
        {
            if (aCoords == MapTiles[i].GetTileCoordinates())
            {
                return MapTiles[i];
            }
        }

        //Debug.LogError("No 'next' tile found");
        return null;
    }

    public Tile GetTile(int aIndex)
    {
        return MapTiles[aIndex];
    }

    //Get the tile in front of the character(using current tile)
    public Tile GetNextTile(Tile aCurrentTile, Vector2 aDirection)
    {
        Vector2 tilePos = aCurrentTile.GetTileCoordinates() + aDirection;

        return GetTile(tilePos);
    }

    //Get the tile in front of the character(using coords)
    public Tile GetNextTile(Vector2 aCoords, Vector2 aDirection)
    {
        Vector2 tilePos = aCoords + aDirection;

        return GetTile(tilePos);
    }

    public int GetTileIndex(Vector2 aCoords)
    {
        for (int i = 0; i < NumOfTiles; i++)
        {
            if (aCoords == MapTiles[i].GetTileCoordinates())
            {
                return i;
            }
        }

        //No tile found
        return -1;
    }

    public Tile[] GetPath(Vector2 aCoords, Vector2Int aDirection, short aRange)
    {
        Tile[] path = new Tile[aRange];
        int index = GetTileIndex(aCoords);
        int dir;

        //dir = (aDirection.x != 0) ? aDirection.x : aDirection.y * NumOfTiles;
        if (aDirection.x != 0)
        {
            dir = aDirection.x;
        }
        else
        {
            dir = aDirection.y * NumberHorizontalTiles; //index of next tile going up or down
        }

        for (int i = 0; i < aRange; i++)
        {
            if (index >= 0)
            {
                path[i] = MapTiles[index];
                index += dir;
            }
            else
            {
                Debug.LogError("Can't add null tile to path");
            }
        }

        return path;
    }

    public void AddPickupToMap()
    {
        Tile tile;

        for (int i = 0; i < Pickups.Length; i++)
        {
            tile = MapTiles[Pickups[i].TileIndex];

            GameObject temp = new GameObject(Pickups[i].PickupName);
            GameObject sprite = Instantiate(temp, tile.GetTileCoordinates(), Quaternion.identity);
            sprite.transform.SetParent(this.transform);
            sprite.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Item_Sprite");
            if (Pickups[i].Invisible) sprite.GetComponent<SpriteRenderer>().sortingOrder = -7;
            Destroy(temp);

            MapTiles[Pickups[i].TileIndex].AddPickup(Pickups[i].PickupName, sprite);
        }
    }

    public int GetConnectedMapIndex(int aDoorIndex)
    {
        for (int i = 0; i < ConnectedDoors.Length; i++)
        {
            if (aDoorIndex == ConnectedDoors[i].DoorIndex)
            {
                return ConnectedDoors[i].MapConnectionIndex;
            }
        }

        return 0;
    }

    public int GetConnectedDoorIndex(int aDoorIndex)
    {
        for (int i = 0; i < ConnectedDoors.Length; i++)
        {
            if (aDoorIndex == ConnectedDoors[i].DoorIndex)
            {
                return ConnectedDoors[i].DoorConnectionIndex;
            }
        }

        return 0;
    }

    public void SetAdjacentMaps(byte[] aIndexes)
    {
        for (int i = 0; i < AdjacentMapIndexes.Length; i++)
        {
            AdjacentMapIndexes[i] = aIndexes[i];
        }
    }

    public byte GetAdjacentMapIndex(int aDir)
    {
        return AdjacentMapIndexes[aDir];
    }
}

[System.Serializable]
public class PickupList
{
    public string PickupName;
    public int TileIndex;
    public bool Invisible;
}

[System.Serializable]
public class DoorConnections
{
    public int DoorIndex;
    public int MapConnectionIndex;
    public int DoorConnectionIndex;
}
