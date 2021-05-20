using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
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
    public List<int> m_statIndex;
    public List<int> typesindex;
    public List<int> statusIndex;
    public List<List<string>> allStatsEffected;
    public List<List<string>> typesNames;
    public List<List<string>> statusNames;
    public int m_amountOfItemsForPlayer;
    public Sprite sprite;
    public bool itemIsSprite = true;
    public bool isStackable;
    public bool isStatusNull = false;
    public ItemID()
    {
        typesindex = new List<int>();
        typesNames = new List<List<string>>();
        statusIndex = new List<int>();
        allStatsEffected = new List<List<string>>();
        statusNames = new List<List<string>>();
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
        isStackable = true;
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
    public static Items item;
    public void SaveValues()
    {
        foreach (var item in m_items)
        {
            PlayerPrefs.SetInt("NumberOfItems", item.m_amountOfItemsForPlayer);
            PlayerPrefs.SetFloat("Durability", (float)item.durability);
            PlayerPrefs.SetFloat("ValueofItem", (float)item.valueOfItem);
            PlayerPrefs.SetString("ItemName " + item.name, item.name);
            PlayerPrefs.SetString("ItemDescription", item.description);
            PlayerPrefsX.SetBool("IsDurability", item.isDurability);
            PlayerPrefsX.SetBool("IsItemStatic", item.staticItem);
            PlayerPrefsX.SetBool("ValueIsPercentage", item.isAPercentage);
            PlayerPrefsX.SetBool("IsStackable", item.isStackable);
            //Saving only one of the arrays as they are all the same but 
            //will be converted to the same number after it is loaded
            PlayerPrefsX.SetStringArray("StatusNames", item.statusNames[0].ToArray());
            PlayerPrefsX.SetStringArray("TypesNames", item.typesNames[0].ToArray());
            PlayerPrefsX.SetStringArray("StatsNames", item.allStatsEffected[0].ToArray());
            //_______________________________________________________________________________
            PlayerPrefsX.SetIntArray("StatsIndex", item.m_statIndex.ToArray());
            PlayerPrefsX.SetIntArray("TypeIndex", item.typesindex.ToArray());
            PlayerPrefsX.SetIntArray("StatusIndex", item.statusIndex.ToArray());
            PlayerPrefs.Save();
        }
    }
    public void LoadValues() 
    {
        foreach (var item in m_items)
        {
            item.m_amountOfItemsForPlayer = PlayerPrefs.GetInt("NumberOfItems");
            item.durability = PlayerPrefs.GetFloat("Durability");
            item.valueOfItem = PlayerPrefs.GetFloat("ValueofItem");
            item.name = PlayerPrefs.GetString("ItemName " + item.name);
            item.description = PlayerPrefs.GetString("ItemDescription");
            item.isDurability = PlayerPrefsX.GetBool("IsDurability");
            item.staticItem = PlayerPrefsX.GetBool("IsItemStatic");
            item.isAPercentage = PlayerPrefsX.GetBool("ValueIsPercentage");
            item.isStackable = PlayerPrefsX.GetBool("IsStackable");
            item.statusNames[0] = PlayerPrefsX.GetStringArray("StatusNames").ToList();
            //Saving only one of the arrays as they are all the same but 
            //will be converted to the same number after it is loaded
            if (item.allStatsEffected == null)
            {
                item.allStatsEffected = new List<List<string>>();
                item.allStatsEffected.Add(new List<string>());
            }
            item.allStatsEffected[0] = PlayerPrefsX.GetStringArray("Stats Names").ToList();
            item.m_statIndex = PlayerPrefsX.GetIntArray("StatsIndex").ToList();
            item.typesindex = PlayerPrefsX.GetIntArray("TypeIndex").ToList();
            item.statusIndex = PlayerPrefsX.GetIntArray("StatusIndex").ToList();
           
            if (item.m_statIndex.Count != item.allStatsEffected.Count)
            {
                for (int i = 1; i < item.m_statIndex.Count; i++)
                {
                    item.allStatsEffected.Add(PlayerPrefsX.GetStringArray("StatsNames").ToList());
                }
            }
            if (item.typesindex.Count != item.typesNames.Count)
            {
                for (int i = 1; i < item.typesindex.Count; i++)
                {
                    item.typesNames.Add(PlayerPrefsX.GetStringArray("TypeIndex").ToList());
                }
            }
            if (item.statusIndex.Count != item.statusNames.Count)
            {
                for (int i = 1; i < item.statusIndex.Count; i++)
                {
                    item.statusNames.Add(PlayerPrefsX.GetStringArray("StatusIndex").ToList());
                }

            }
        }
    }
    private void OnEnable()
    {
        item = this;
    }
    private void LateUpdate()
    {
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
                if (m_items[i].statusNames.Count <= 0)
                    AddNewStatus(i);
                if (m_items[i].allStatsEffected.Count <= 0)
                    AddNewStats(i);
                if (m_items[i].typesNames.Count <= 0)
                    AddNewTyping(i);
            }
        }
        if (m_items.Count < 1)
        {
            m_items = new List<ItemID>();
            m_items.Add(new ItemID());
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
        m_items[i].typesNames.Add(new List<string>());
        m_items[i].typesindex.Add(0);
        for (int j = 0; j < m_items[i].typesNames.Count; j++)
        {
            foreach (var item in TypeChart.chart.m_types)
            {
                m_items[i].typesNames[j].Add(item);
            }
        }
    }
    //private void AddToTypes(int i)
    //{
    //    for (int j = 0; j < m_items[i].typesNames.Count; j++)
    //    {
    //        foreach (var item in TypeChart.chart.m_types)
    //        {
    //            m_items[i].typesNames[j].Add(item);
    //        }
    //    }

    //}
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
        for (int j = 0; j < m_items[i].statusNames.Count; j++)
        {
            foreach (var item in StatusEffects.status.m_statusEffects)
            {
                m_items[i].statusNames[j].Add(item.name);
            }
        }
    }
    public void AddNewStatus(int i)
    {
        m_items[i].statusNames.Add(new List<string>());
        m_items[i].statusIndex.Add(0);
        for (int j = 0; j < m_items[i].statusNames.Count; j++)
        {
            foreach (var item in StatusEffects.status.m_statusEffects)
            {
                m_items[i].statusNames[j].Add(item.name);
            }
        }
    }

}

