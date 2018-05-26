using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    const float MAP_DIST_MOD = 0.05f;//0.05f = 1/20 (Dist being 20)

    //Sprite TownMap;
    Map[] Maps;

    public string StartingMap = "Sapwood Town";

    void Awake()
    {
        InitializeMaps();
    }

    void Start()
    {
        SelectMapConntions();
    }

    void InitializeMaps()
    {
        Map map;
        int AmountOfMaps = transform.childCount;
        Maps = new Map[AmountOfMaps];

        for (int i = 0; i < AmountOfMaps; i++)
        {
            map = transform.GetChild(i).GetComponent<Map>();

            if (map != null)
            {
                Vector2 pos = new Vector2(map.transform.position.x, map.transform.position.y);
                Maps[i] = map;
                //Debug.Log(Maps[i].name);
            }
            else
            {
                Debug.LogError("Map not found");
                //Application.Quit();
            }
        }

        /*for (int i = 0; i < GameObject.FindGameObjectsWithTag("Map").Length; i++)
        {
        OR
        }*/
    }

    public Map GetMap(Vector2 aPostion)
    {
        Vector3 pos = aPostion;

        for (int i = 0; i < Maps.Length; i++)
        {
            if (pos == Maps[i].transform.position)
            {
                return Maps[i];
            }
        }

        return null;
    }

    public Map GetMap(string aName)
    {
        for (int i = 0; i < Maps.Length; i++)
        {
            if (aName == Maps[i].name)
            {
                return Maps[i];
            }
        }

        return null;
    }

    public Map GetMap(int aIndex)
    {
        return Maps[aIndex];
    }

    //To be used for now
    public Map GetNextMap(Map aCurrentMap, int aDirection)
    {
        int index = aCurrentMap.GetAdjacentMapIndex(aDirection);
        Map map = GetMap(index);

        if (map != null)
        {
            return map;
        }

        return null;
    }

    private void SelectMapConntions()
    {
        //  byte[] maps = Nou,Eas,Sou,Wes /255->invalid map

        //Map 0-SapwoodTown
        {
            byte[] maps = { 1, 255, 255, 255};
            SetMapConnections(0, maps);
        }
        //Map 1-Route1
        {
            byte[] maps = { 2, 255, 0, 255 };
            SetMapConnections(1, maps);
        }

    }

    //To be moved to a map manager which will set all of this up
    private void SetMapConnections(int aMapIndex, byte[] aConnections)
    {
        Maps[aMapIndex].SetAdjacentMaps(aConnections);
    }
}
