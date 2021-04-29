using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public enum ItemProperties
{
    None,
    IncreaseStats,
    DecreaseStats,
    IncludeStatus
}
[Serializable]
public class ItemID
{
    public int itemIndex = 0;
    [HideInInspector] public bool showItem;
    public string name;
    public int valueOfItem;
    public bool isAPercentage;
    [Tooltip("Max size is 4!")]
    public ItemProperties properties;
    public List<TypeVariation> variation;
    public ItemID()
    {
        variation = new List<TypeVariation>();
        name = "Default";
        valueOfItem = 1;
        isAPercentage = false;
        properties = new ItemProperties();
        properties = ItemProperties.None;
        showItem = false;
    }
    public ItemID(string a_name, int a_valueOfItem, bool a_isAPercentage, int a_propertiesSize,
        ItemProperties a_properties)
    {
        name = a_name;
        valueOfItem = a_valueOfItem;
        isAPercentage = a_isAPercentage;
        properties = a_properties;
        showItem = false;
        variation = new List<TypeVariation>();
    }
}
[RequireComponent(typeof(TypeChart))]
[ExecuteInEditMode]
public class Items : MonoBehaviour
{
    public List<ItemID> m_items;
    public int indexValue = 0;
    private TypeChart type;
    private void LateUpdate()
    {
        if (type == null)
            type = GetComponent<TypeChart>();
        if (!Application.isPlaying && m_items == null)
        {
            m_items = new List<ItemID>();
            m_items.Add(new ItemID());
        }
        for (int i = 0; i < m_items.Count; i++)
        {
            for (int j = 0; j < m_items[i].variation.Count; j++)
            {
                if (m_items[i].variation[j].m_typeVariation.Count != type.ValueOfArray().Count)
                {
                    NewList();
                    return;
                }
                if (m_items[i].variation[j].m_typeVariation != type.ValueOfArray())
                    NewList();
            }
        }
        if (m_items.Count < 1)
        {
            m_items = new List<ItemID>();
            m_items.Add(new ItemID());
            type = GetComponent<TypeChart>();
        }
    }
    public void AddToList()
    {
        m_items.Add(new ItemID());
    }
    public void NewList()
    {
        m_items = new List<ItemID>();
        m_items.Add(new ItemID());
        m_items[0].variation[0].m_typeVariation.CopyTo(TypeChart.chart.m_types.ToArray(), 0);
        m_items[0].variation[0].m_typeVariation = TypeChart.chart.m_types;
    }
}
