using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : IInteract
{
    public GameObject Sprite;
    public string Name;
    public bool Tm;

    public Pickup(string aName, Vector2 aPos, GameObject aSprite)
    {
        //TODO:
        ItemData.ItemType type = ItemData.ItemType.Item;
        //type = ItemDatabase.GetItem(aName).ItemType
        Name = aName;
        if (type == ItemData.ItemType.Tm)
        {
            Tm = true;
        }

        Sprite = aSprite;
    }

    public void Interact(PlayerData aPlayer)
    {
        aPlayer.Bag.AddItem(Name);
    }
}
