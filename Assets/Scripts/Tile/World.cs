using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    //Sprite TownMap;
    Map[] Maps;
    public string StartingMap = "Sapwood Town";

    void Awake()
    {
        InitializeMaps();
    }

    void Start()
    {

    }

    void Update()
    {

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
                Maps[i] = map;
                Debug.Log(Maps[i].name);
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
}
