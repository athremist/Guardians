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

    public ItemData(string name, ItemType itemType, BattleType battleType, string description, int price,
        ItemEffect itemEffect)
    {
        this.Name = name;
        this.Item_Type = itemType;
        this.Battle_Type = battleType;
        this.Description = description;
        this.Price = price;
        this.Item_Effect = itemEffect;
    }

    //Last arguement ex: Full heal, heals 'all' status
    public ItemData(string name, ItemType itemType, BattleType battleType, string description, int price,
        ItemEffect itemEffect, string stringParameter)
    {
        this.Name = name;
        this.Item_Type = itemType;
        this.Battle_Type = battleType;
        this.Description = description;
        this.Price = price;
        this.Item_Effect = itemEffect;
        this.StringParameter = stringParameter;
    }

    //Last arguement ex: Ether adds'10f' pp, or pokeball effect is 'x1'(1f).
    public ItemData(string name, ItemType itemType, BattleType battleType, string description, int price,
        ItemEffect itemEffect, float floatParameter)
    {
        this.Name = name;
        this.Item_Type = itemType;
        this.Battle_Type = battleType;
        this.Description = description;
        this.Price = price;
        this.Item_Effect = itemEffect;
        this.FloatParameter = floatParameter;
    }

    //Last 2 arguments ex: Elixer Restores 'all' moves pp, and by '10f' pp.
    public ItemData(string name, ItemType itemType, BattleType battleType, string description, int price,
        ItemEffect itemEffect, string stringParameter, float floatParameter)
    {
        this.Name = name;
        this.Item_Type = itemType;
        this.Battle_Type = battleType;
        this.Description = description;
        this.Price = price;
        this.Item_Effect = itemEffect;
        this.StringParameter = stringParameter;
        this.FloatParameter = floatParameter;
    }

    //TMs
    public ItemData(int tmNo, string name, ItemType itemType, BattleType battleType, string description, int price)
    {
        this.TmNo = tmNo;
        this.Name = name;
        this.Item_Type = itemType;
        this.Battle_Type = battleType;
        this.Description = description;
        this.Price = price;
        this.Item_Effect = ItemEffect.TM;
    }

    public string getName()
    {
        return Name;
    }

    public ItemType getItemType()
    {
        return Item_Type;
    }

    public BattleType getBattleType()
    {
        return Battle_Type;
    }

    public string getDescription()
    {
        return Description;
    }

    public int getPrice()
    {
        return Price;
    }

    public int getTMNo()
    {
        return TmNo;
    }

    public ItemEffect getItemEffect()
    {
        return Item_Effect;
    }

    public string getStringParameter()
    {
        return StringParameter;
    }

    public float getFloatParameter()
    {
        return FloatParameter;
    }
}
