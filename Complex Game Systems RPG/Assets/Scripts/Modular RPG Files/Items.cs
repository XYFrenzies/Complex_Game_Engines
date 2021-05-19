using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public enum ItemProperties
{
    None,
    IncreaseStats,
    DecreaseStats,
    IncludeStatus
}
[Serializable]
public enum ItemType
{
    None,
    Armour,
    Weapon,
    Potion,
    Other
}
[RequireComponent(typeof(TypeChart))]
[Serializable]
public class ItemID
{

    public bool customizedItem;
    public List<ItemFunction> itemFunctions;
    [HideInInspector] public bool showItem;
    public string name;
    public double valueOfItem;
    public bool isAPercentage;
    public bool isTypeChartNull;
    public bool isDurability;
    public bool staticItem;
    public double durability;
    public string description;
    public List<ItemProperties> properties;
    public ItemType itemType;
    public List<List<string>> allStatsEffected;
    public List<TypeChart> variation;
    public int m_amountOfItemsForPlayer;
    public List<int> m_statIndex;
    public List<StatusEffects> status;
    public List<string> statusNames;
    public Sprite sprite;
    public GameObject obj;
    public bool itemIsSprite = true;
    public bool isStackable;
    public bool isStatusNull = false;
    public ItemID()
    {
        isStackable = true;
        customizedItem = false;
        itemFunctions = new List<ItemFunction>();
        allStatsEffected = new List<List<string>>();
        variation = new List<TypeChart>();
        status = new List<StatusEffects>();
        statusNames = new List<string>();
        m_statIndex = new List<int>();
        itemType = ItemType.Potion;
        properties = new List<ItemProperties>();
        properties.Add(ItemProperties.None);
        name = "Default";
        description = "This is an Item.";
        m_amountOfItemsForPlayer = 1;
        valueOfItem = 1;
        durability = 1;
        isDurability = false;
        isTypeChartNull = false;
        isAPercentage = false;
        staticItem = false;
        showItem = false;
        variation.Add(new TypeChart());
        status.Add(new StatusEffects());
        if (TypeChart.chart == null)
            isTypeChartNull = true;
        else
            for (int j = 0; j < variation.Count; j++)
            {
                variation[j].m_types = new List<string>();
                foreach (var item in TypeChart.chart.m_types)
                {
                    variation[j].m_types.Add(item);
                }
            }
        if (StatusEffects.status == null)
        {
            isStatusNull = true;
            return;
        }
        else
        {
            for (int j = 0; j < status.Count; j++)
            {
                status[j].m_statusEffects = new List<Status>();
                foreach (var item in StatusEffects.status.m_statusEffects)
                {
                    status[j].m_statusEffects.Add(item);
                    statusNames.Add(item.name);
                }
            }
        }
    }
    public ItemID(string a_name, int a_valueOfItem, bool a_isAPercentage, int a_propertiesSize)
    {
        name = a_name;
        valueOfItem = a_valueOfItem;
        isAPercentage = a_isAPercentage;
        showItem = false;
        variation = new List<TypeChart>();
        for (int i = 0; i < variation.Count; i++)
        {
            variation[0].m_types = new List<string>();
            foreach (var item in TypeChart.chart.m_types)
            {
                variation[0].m_types.Add(item);
            }
        }
    }
    //This returns the amount of durability on the item and returns true if its below 0 or false if it isnt.
    public bool DamageDurability(double a_durability)
    {
        durability -= a_durability;
        if (durability <= 0)
            return true;
        return false;
    }
    //This returns the amount of durability on the item
    public double DoubleReDamageDurability(double a_durability)
    {
        durability -= a_durability;
        return durability;
    }
    public string GetDescription() 
    {
        return description;
    }
    public string GetName() 
    {
        return name;
    }
    public double GetItemValue() 
    {
        return valueOfItem;
    }
    public double GetDurability() 
    {
        return durability;
    }
    public void SetItemToStatic()
    {
        staticItem = true;
    }
    public void SetStaticItemToNonStatic()
    {
        staticItem = false;
    }
}
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(StatusEffects))]
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class Items : MonoBehaviour
{
    public List<ItemID> m_items;
    private TypeChart type;
    public static Items item;
    private bool scriptHasBeenChangedType = false;
    private bool scriptHasBeenChangedStatus = false;

    private void OnEnable()
    {
        item = this;
    }
    private void OnValidate()
    {
        item = this;
        if (type == null)
            type = GetComponent<TypeChart>();
        scriptHasBeenChangedType = true;
        scriptHasBeenChangedStatus = true;

    }
    private void LateUpdate()
    {
        if (type == null)
            type = GetComponent<TypeChart>();
        if (!Application.isPlaying)
        {
            item = this;
            if (m_items == null)
            {
                m_items = new List<ItemID>();
                m_items.Add(new ItemID());
            }
            for (int i = 0; i < m_items.Count; i++)
            {
                if (TypeChart.chart == null || m_items[i].isTypeChartNull)
                {
                    AddToTypes(i);
                    m_items[i].isTypeChartNull = true;
                }

                if (StatusEffects.status == null || m_items[i].isStatusNull)
                {
                    AddToStatus(i);
                    m_items[i].isStatusNull = true;
                }
                if (m_items[i].status.Count <= 0)
                    AddNewStatus(i);
                if (m_items[i].allStatsEffected.Count <= 0)
                    AddNewStats(i);
            }
        }
        for (int i = 0; i < m_items.Count; i++)
        {
            if (scriptHasBeenChangedType)
            {
                AddToTypes(i);
                scriptHasBeenChangedType = false;
                return;
            }
            if (scriptHasBeenChangedStatus)
            {
                AddToStatus(i);
                scriptHasBeenChangedStatus = false;
                return;
            }
            for (int j = 0; j < m_items[i].variation.Count; j++)
            {
                if (m_items[i].variation[j].m_types.Count != type.ValueOfArray().Count)
                {
                    NewList(i, j);
                }
                if (!m_items[i].variation[j].m_types.SequenceEqual(type.m_types))
                    NewList(i, j);
            }
        }
        if (m_items.Count < 1)
        {
            m_items = new List<ItemID>();
            m_items.Add(new ItemID());
            type = GetComponent<TypeChart>();
        }
    }
    public ItemID GetItems(string m_nameOfItem)
    {
        foreach (var item in m_items)
        {
            if (item.name == m_nameOfItem)
                return item;
        }
        return null;
    }
    public void AddNewItem()
    {
        m_items.Add(new ItemID());
    }
    public void AddNewTyping(int i)
    {
        m_items[i].variation.Add(new TypeChart());

        foreach (var item in TypeChart.chart.m_types)
        {
            for (int j = 0; j < m_items[i].variation.Count; j++)
            {
                if (m_items[i].variation[j].m_types == null)
                    m_items[i].variation[j].m_types = new List<string>();
                if (m_items[i].variation[j].m_types.Count < TypeChart.chart.m_types.Count)
                    m_items[i].variation[j].m_types.Add(item);
            }
        }
    }
    private void NewList(int itemPara, int variationPara)
    {
        foreach (var item in TypeChart.chart.m_types)
        {
            m_items[itemPara].variation[variationPara].m_types.Add(item);
        }
    }
    private void AddToTypes(int i)
    {
        TypeChart.chart = GetComponent<TypeChart>();
        for (int j = 0; j < m_items[i].variation.Count; j++)
        {
            if (m_items[i].variation[j] == null)
                m_items[i].variation[j] = new TypeChart();
            m_items[i].variation[j].m_types = new List<string>();
            foreach (var item in TypeChart.chart.m_types)
            {
                m_items[i].variation[j].m_types.Add(item);
            }
        }
    }
    public void AddStats(int i)
    {
        for (int j = 0; j < m_items[i].allStatsEffected.Count; j++)
        {
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                m_items[i].allStatsEffected[j].Add(item.name);
            }
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                m_items[i].allStatsEffected[j].Add(item.name);
            }
        }
    }
    public void AddNewStats(int i)
    {
        m_items[i].allStatsEffected.Add(new List<string>());
        m_items[i].m_statIndex.Add(0);
        for (int j = 0; j < m_items[i].allStatsEffected.Count; j++)
        {
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                m_items[i].allStatsEffected[j].Add(item.name);

            }
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                m_items[i].allStatsEffected[j].Add(item.name);
            }
        }
    }
    public void AddToStatus(int i)
    {
        StatusEffects.status = GetComponent<StatusEffects>();
        for (int j = 0; j < m_items[i].status.Count; j++)
        {
            if (m_items[i].status[j] == null)
                m_items[i].status[j] = new StatusEffects();
            m_items[i].status[j].m_statusEffects = new List<Status>();
            foreach (var item in StatusEffects.status.m_statusEffects)
            {
                m_items[i].status[j].m_statusEffects.Add(item);
                m_items[i].statusNames.Add(item.name);
            }

        }
    }
    public void AddNewStatus(int i)
    {
        m_items[i].status.Add(new StatusEffects());
        foreach (var item in StatusEffects.status.m_statusEffects)
        {
            for (int j = 0; j < m_items[i].status.Count; j++)
            {
                m_items[i].status[j].m_statusEffects = new List<Status>();
                m_items[i].status[j].m_statusEffects.Add(item);
                m_items[i].statusNames.Add(item.name);
            }
        }
    }
}

