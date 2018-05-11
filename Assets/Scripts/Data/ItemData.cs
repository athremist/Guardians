using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{

    public enum ItemType
    {
        Item,
        Medicine,
        Tm,
        Key//, Berry (if I add held items AKA gen2)
    }

    public enum BattleType
    {
        None,
        Restore,
        HealStatus,
        Pokeball,
        BattleItem
    }

    public enum ItemEffect
    {
        None,
        Unique,
        HP,
        PP,
        Status,
        EV,
        Evolve,
        Flee,
        Ball,
        StatBoost,
        TM
    }

    private string Name;

    private ItemType Item_Type;
    private BattleType Battle_Type;
    private ItemEffect Item_Effect;
    private string Description;
    private int Price;

    private int TmNo;

    private string StringParameter;
    private float FloatParameter;

    public ItemData(string aName, ItemType aItemType, BattleType aBattleType, string aDescription, int aPrice)
    {
        this.Name = aName;
    }

    /*public ItemData(string name, ItemType itemType, BattleType battleType, string description, int price,
        ItemEffect itemEffect)
    {
        this.name = name;
        this.itemType = itemType;
        this.battleType = battleType;
        this.description = description;
        this.price = price;
        this.itemEffect = itemEffect;
    }

    public ItemData(string name, ItemType itemType, BattleType battleType, string description, int price,
        ItemEffect itemEffect, string stringParameter)
    {
        this.name = name;
        this.itemType = itemType;
        this.battleType = battleType;
        this.description = description;
        this.price = price;
        this.itemEffect = itemEffect;
        this.stringParameter = stringParameter;
    }

    public ItemData(string name, ItemType itemType, BattleType battleType, string description, int price,
        ItemEffect itemEffect, float floatParameter)
    {
        this.name = name;
        this.itemType = itemType;
        this.battleType = battleType;
        this.description = description;
        this.price = price;
        this.itemEffect = itemEffect;
        this.floatParameter = floatParameter;
    }

    public ItemData(string name, ItemType itemType, BattleType battleType, string description, int price,
        ItemEffect itemEffect, string stringParameter, float floatParameter)
    {
        this.name = name;
        this.itemType = itemType;
        this.battleType = battleType;
        this.description = description;
        this.price = price;
        this.itemEffect = itemEffect;
        this.stringParameter = stringParameter;
        this.floatParameter = floatParameter;
    }

    //TMs
    public ItemData(int tmNo, string name, ItemType itemType, BattleType battleType, string description, int price)
    {
        this.tmNo = tmNo;
        this.name = name;
        this.itemType = itemType;
        this.battleType = battleType;
        this.description = description;
        this.price = price;
        this.itemEffect = ItemEffect.TM;
    }

    public string getName()
    {
        return name;
    }

    public ItemType getItemType()
    {
        return itemType;
    }

    public BattleType getBattleType()
    {
        return battleType;
    }

    public string getDescription()
    {
        return description;
    }

    public int getPrice()
    {
        return price;
    }

    public int getTMNo()
    {
        return tmNo;
    }

    public ItemEffect getItemEffect()
    {
        return itemEffect;
    }

    public string getStringParameter()
    {
        return stringParameter;
    }

    public float getFloatParameter()
    {
        return floatParameter;
    }*/
}
