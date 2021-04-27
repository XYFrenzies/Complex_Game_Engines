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
    public string name;
    public int valueOfItem;
    public bool isAPercentage;
    public ItemProperties[] properties;
    public List<string> m_typeVariation = new List<string>();
    public ItemID() 
    {
        name = "Default";
        valueOfItem = 1;
        isAPercentage = false;
    }
    public ItemID(string a_name, int a_valueOfItem, bool a_isAPercentage) 
    {
        name = a_name;
        valueOfItem = a_valueOfItem;
        isAPercentage = a_isAPercentage;
    }
}
public class Items : MonoBehaviour
{
    public ItemID[] m_items;

    private void Update()
    {
        if (!Application.isPlaying && m_items == null)
        {
            m_items = new ItemID[1];
            for (int i = 0; i < m_items.Length; i++)
            {
                m_items[i] = new ItemID();
                m_items[i].m_typeVariation = TypeChart.chart.m_nameOfType;
            }
        }
        for (int i = 0; i < m_items.Length; i++)
        {
            if (m_items[i].properties.Length > 4)
            {
                m_items[i].properties = new ItemProperties[1];
                m_items[i].properties[0] = new ItemProperties();
            }
        }
    }
}
