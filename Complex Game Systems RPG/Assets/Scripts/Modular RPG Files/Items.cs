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
    [HideInInspector] public bool showItem;
    public string name;
    public int valueOfItem;
    public bool isAPercentage;
    [Tooltip("Max size is 4!")]
    public ItemProperties properties;
    public List<string> m_typeVariation = new List<string>();
    public ItemID()
    {
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
    }
}
[ExecuteInEditMode]
public class Items : MonoBehaviour
{
    public List<ItemID> m_items;
    public int indexValue = 0;
    private TypeChart type;
    private void Awake()
    {
        type = GetComponent<TypeChart>();
    }
    private void LateUpdate()
    {
        if (!Application.isPlaying && m_items == null)
        {
            m_items = new List<ItemID>();
            m_items.Add(new ItemID());
            m_items[0].m_typeVariation.CopyTo(type.m_nameOfType.ToArray(), 0);
            m_items[0].m_typeVariation = type.m_nameOfType;
        }
        for (int i = 0; i < m_items.Count; i++)
        {
            if (m_items[i].m_typeVariation.Count != type.ValueOfArray().Count)
            {
                NewList();
                return;
            }

            for (int j = 0; j < m_items[i].m_typeVariation.Count; j++)
            {
                if (m_items[i].m_typeVariation[j] != type.ValueOfArray()[j])
                    NewList();
            }

        }   
        if (m_items.Count < 1)
        {
            m_items = new List<ItemID>();
            m_items.Add(new ItemID());
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
        m_items[0].m_typeVariation.CopyTo(type.m_nameOfType.ToArray(), 0);
        m_items[0].m_typeVariation = type.m_nameOfType;
    }
}
