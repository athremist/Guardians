using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag
{
    private string[] Order;
    private int[] Quantity;
    private int NumItems;

    public Bag()
    {
        Order = new string[100];
        Quantity = new int[100];
    }

    public int GetIndexOf(string aName)
    {
        for (int i = 0; i < NumItems; i++)
        {
            if (Order[i] == aName)
            {
                return i;
            }
        }

        return -1;
    }

    public void Move(int targetIndex, int destinationIndex)
    {
        string temp = Order[targetIndex];
        //TODO
        //TODO
        //TODO
        //TODO
    }

    public int GetLength()
    {
        return NumItems;
    }

    /*public int GetLength()
    {

        int length = 0;
        for (int i = 0; i < Order.Length; i++)
        {
            //if(ItemDatabase.GetItem(order[i]) != null)
            {
                length += 1;
            }
        }
        return length;
    }*/

    public void PackOrder()
    {
        string[] packedOrder = new string[Order.Length];
        int packedIndex = 0;
        for (int i = 0; i < GetLength() + 1; i++)//Check an extra(+1) item spot for if an item was removed
        {
            if (Order[i] != null)
            {
                packedOrder[packedIndex] = Order[i];
                packedIndex += 1;
            }
        }
        Order = packedOrder;
    }

    public int GetQuantity(string aItemName)
    {
        int index = 0;//ItemDatabase.GetIndexOf(aItemName);
        if (index != -1)
        {
            return Quantity[index];
        }
        return 0;
    }

    public bool AddItem(string aItemName, int aAmount = 1)
    {
        //PackOrder();
        string name = "HM01";//TODO ItemDatabase aItemName
        int index = GetIndexOf(name);
        if (index == -1)
        {
            NumItems += 1;
            index = GetLength();
            Order[index] = name;
        }
        //index = ItemDatabase.GetIndexOf(Order[index]);
        if (Quantity[index] + aAmount > 999)
        {
            return false;
        }
        Quantity[index] += aAmount;
        Debug.Log(aItemName + " acquired");
        return true;
    }

    public bool RemoveItem(string aItemName, int aAmount)
    {
        //PackOrder
        string name = "HM01";//TODO ItemDatabase aItemName
        int index = GetIndexOf(name);
        if (index == -1)
        {
            return false;
        }
        //index = ItemDatabase.GetIndexOf(Order[index]);
        if (Quantity[index] - aAmount < 0)
        {
            return false;
        }
        Quantity[index] -= aAmount;
        if (Quantity[index] == 0)
        {
            Order[index] = null;
            NumItems -= 1;
            //PackOrder();
        }
        return true;
    }
}
