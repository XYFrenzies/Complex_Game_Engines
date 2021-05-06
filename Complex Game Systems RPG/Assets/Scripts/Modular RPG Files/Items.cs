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
[RequireComponent(typeof(TypeChart))]
[Serializable]
public class ItemID
{
    public int itemIndex = 0;
    [HideInInspector] public bool showItem;
    public string name;
    public double valueOfItem;
    public bool isAPercentage;
    public bool isTypeChartNull;
    public bool isDurability;
    public double durability;
    [Tooltip("Max size is 4!")]
    public List<ItemProperties> properties;
    public List<TypeChart> variation;
    public ItemID()
    {
        durability = 1;
        isDurability = false;
        isTypeChartNull = false;
        variation = new List<TypeChart>();
        name = "Default";
        valueOfItem = 1;
        isAPercentage = false;
        properties = new List<ItemProperties>();
        properties.Add(ItemProperties.None);
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
}
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class Items : MonoBehaviour
{
    public List<ItemID> m_items;
    private TypeChart type;
    private void LateUpdate()
    {  
        if (type == null)
            type = GetComponent<TypeChart>();
        if (!Application.isPlaying)
        {
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
                }
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
        for (int j = 0; j < m_items[i].variation.Count; j++)
        {
            m_items[i].variation[j].m_types = new List<string>();
            foreach (var item in TypeChart.chart.m_types)
            {
                m_items[i].variation[j].m_types.Add(item);
            }
        }
    }
}
