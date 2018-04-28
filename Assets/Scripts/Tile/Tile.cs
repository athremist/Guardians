using UnityEngine;
using System.Collections;

public class Tile
{
    Vector2 Coords; //Position of tile on the map
    TileType Type; //What kind of tile
    //Interactable InteractableObject;
    //Pickup Pickup; //What pickup is on the tile/non
    bool Walkable; //Is the tile walkable?

    public Tile()
    {

    }

    public Tile(Vector2 aCoodinates, TileType aTileType)//, Pickup aPickup)
    {
        Coords = aCoodinates;
        Type = aTileType;
    }

    public Vector2 GetTileCoordinates()
    {
        return Coords;
    }

    public TileType GetTileType()
    {
        return Type;
    }

    //public Interactable Object()
    //{
    //    return interactableObject;
    //}

    public bool IsWalkable()
    {
        return Walkable;
    }

    //Tile manager will set tile to walkable depending on the tiletype
    protected void SetWalkable(bool aIsWalkable)
    {
        Walkable = aIsWalkable;
    }

    public enum TileType
    {
        Ground,
        Wall,
        Ledge,
        Grass,
        Cave,
        Water,
        Door,
        Stairs
    }
}
