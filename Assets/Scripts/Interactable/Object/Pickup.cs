using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteract
{
    public ItemData.ItemType type;
    public string name;
    public bool tm;

    //Will inherit from interactable

    public void Interact()
    {
        Debug.Log("YOU GOT HM01 YAY!");
        Destroy(this.gameObject);
    }

    public Pickup()
    {

    }
}
