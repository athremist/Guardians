using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

//Put this script on a map to be able to see tile indexes
public class DebugDrawTileIndex : MonoBehaviour
{
    Map Map;
    List<GameObject> listOfIndexes;
    GameObject DebugCanvas;

    public Color IndexColor = Color.black;

    void Start()
    {
        Map = GetComponent<Map>();
        listOfIndexes = new List<GameObject>();
        DebugCanvas = (GameObject)Instantiate(Resources.Load("Helpers/Canvas_Helper"));
    }

    void CreateTextBoxes()
    {
        Font font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        Color color = Color.black;
        string name = "Tile";
        float scaleMod = (DebugCanvas.transform as RectTransform).sizeDelta.x;

        for (int i = 0; i < Map.NumOfTiles; i++)
        {
            GameObject newText = new GameObject(name + i.ToString(), typeof(RectTransform));
            Text index = newText.AddComponent<Text>();

            index.text = i.ToString();
            index.color = IndexColor;
            index.fontSize = ScaleFontForDigitAmount(i.ToString().Length);
            index.font = font;
            index.alignment = TextAnchor.MiddleCenter;

            //Multiplying the reverse scale of what the canvas will to have proper positions
            newText.transform.position = (Map.GetTile(i).GetTileCoordinates()) * 20;//20 = scale
            newText.transform.SetParent(DebugCanvas.transform);
            listOfIndexes.Add(newText);

            listOfIndexes[i].SetActive(false);
        }
        //Setting the scale after or else unity does some wierd stuff where the UI gets wayy bigger 
        DebugCanvas.transform.localScale = Vector3.one * 0.05f; //0.05f = 1/20
    }

    public void ShowTileIndexes()
    {
        if (listOfIndexes.Count <= 0)
        {
            CreateTextBoxes();
            for (int i = 0; i < listOfIndexes.Count; i++)
            {
                listOfIndexes[i].SetActive(true);
            }
        }
    }

    public void HideTileIndexes()
    {
        for (int i = 0; i < listOfIndexes.Count; i++)
        {
            listOfIndexes[i].SetActive(true);
        }
    }

    //Just a fancy formula for making the font smaller by 5 for every extra digit for a resonable size
    private int ScaleFontForDigitAmount(int aDigits)
    {
        return 20 + (1 - aDigits) * 5;
    }
}

[CustomEditor(typeof(DebugDrawTileIndex))]
public class ObjectBuilderEditor : Editor
{
    bool On = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DebugDrawTileIndex myScript = (DebugDrawTileIndex)target;
        if (GUILayout.Button("Debug Draw"))
        {
            if (!On)
            {
                myScript.ShowTileIndexes();
            }
            else
            {
                myScript.HideTileIndexes();
            }
        }
    }
}
