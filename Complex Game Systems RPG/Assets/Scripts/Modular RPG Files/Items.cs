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
    Armour,
    Weapon,
    Potion
}
[RequireComponent(typeof(TypeChart))]
[Serializable]
public class ItemID
{
    public bool customizedItem;
    public List<ItemFunction> itemFunctions;
    public int itemIndex = 0;
    [HideInInspector] public bool showItem;
    public string name;
    public double valueOfItem;
    public bool isAPercentage;
    public bool isTypeChartNull;
    public bool isDurability;
    public bool staticItem;
    public double durability;
    public string description;
    [Tooltip("Max size is 4!")]
    public List<ItemProperties> properties;
    public ItemType itemType;
    public List<List<string>> allStatseffected;
    public List<TypeChart> variation;
    public int m_amountOfItemsForPlayer;
    public List<int> m_statIndex;
    public List<StatusEffects> status;
    public List<string> statusNames;
    public ItemID()
    {
        customizedItem = false;
        itemFunctions = new List<ItemFunction>();
        allStatseffected = new List<List<string>>();
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
    public void UseItem()
    {
        foreach (var itemFunction in itemFunctions)
        {
            itemFunction.DoSomething();
        }
    }
}
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(StatusEffects))]
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class Items : MonoBehaviour
{
    public int itemIndex;
    public List<ItemID> m_items;
    private TypeChart type;
    public static Items item;
    private void OnValidate()
    {
        item = this;
        if (type == null)
            type = GetComponent<TypeChart>();
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
                itemIndex = 0;
                m_items = new List<ItemID>();
                m_items.Add(new ItemID());
            }
            for (int i = 0; i < m_items.Count; i++)
            {
                if (TypeChart.chart == null || m_items[i].isTypeChartNull)
                {
                    AddToTypes(i);
                }
                if (m_items[i].status.Count <= 0)
                    AddNewStatus(i);
                if (m_items[i].allStatseffected.Count <= 0)
                    AddNewStats(i);
            }
        }
        for (int i = 0; i < m_items.Count; i++)
        {
            for (int j = 0; j < m_items[i].variation.Count; j++)
            {
                if (m_items[i].variation[j] == null)
                {
                    AddToTypes(i);
                    return;
                }
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
                if(m_items[i].variation[j].m_types.Count < TypeChart.chart.m_types.Count)
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
        for (int j = 0; j < m_items[i].allStatseffected.Count; j++)
        {
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                m_items[i].allStatseffected[j].Add(item.name);
            }
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                m_items[i].allStatseffected[j].Add(item.name);
            }
        }
    }
    public void AddNewStats(int i) 
    {
        m_items[i].allStatseffected.Add(new List<string>());
        m_items[i].m_statIndex.Add(0);
        for (int j = 0; j < m_items[i].allStatseffected.Count; j++)
        {
            foreach (var item in Stats.statsForObjects.m_primaryStatistic)
            {
                m_items[i].allStatseffected[j].Add(item.name);

            }
            foreach (var item in Stats.statsForObjects.m_secondaryStatistic)
            {
                m_items[i].allStatseffected[j].Add(item.name);
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

