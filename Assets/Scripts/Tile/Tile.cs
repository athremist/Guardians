using UnityEngine;
using System.Collections;

public class Tile
{
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

    Map Map;
    Pickup Pickup; //What pickup is on the tile/non
    Vector2 Coords; //Position of tile on the map
    TileType Type; //What kind of tile
    IInteract Interactable;
    //int LedgeDirection;
    bool Walkable; //Is the tile walkable?

    public Tile()
    {

    }

    public Tile(Map aMap, Vector2 aCoodinates, TileType aTileType)//, Pickup aPickup)
    {
        Map = aMap;
        Coords = aCoodinates;
        Type = aTileType;
        SetWalkable(aTileType);
    }

    public Map GetMap()
    {
        return Map;
    }

    public Pickup GetPickup()
    {
        return Pickup;
    }

    public void AddPickup(string aName, GameObject aSprite)
    {
        Pickup = new Pickup(aName, Coords, aSprite);
        Interactable = Pickup;
    }

    public GameObject RemovePickup()
    {
        GameObject sprite = Pickup.Sprite;
        Pickup = null;
        Interactable = null;
        return sprite;
    }

    public Vector2 GetTileCoordinates()
    {
        return Coords;
    }

    public TileType GetTileType()
    {
        return Type;
    }

    public IInteract GetInteractable()
    {
        return Interactable;
    }

    public void SetInteractable(IInteract aInteract)
    {
        Interactable = aInteract;
    }

    public bool IsWalkable()
    {
        return Walkable;
    }

    public bool IsWalkable(int aDir)
    {
        if (Type != TileType.Ledge)
        {
            return Walkable;
        }
        else
        {
            return CheckLedgeWalkable(aDir);
        }
    }

    //Tile manager will set tile to walkable depending on the tiletype
    protected void SetWalkable(TileType aType)
    {
        if (aType == TileType.Wall || aType == TileType.Water)
        {
            Walkable = false;
        }
        else
        {
            Walkable = true;
        }
    }

    private bool CheckLedgeWalkable(int aDir)
    {
        //TODO: Make it work for other directions and not just down
        if (aDir == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
