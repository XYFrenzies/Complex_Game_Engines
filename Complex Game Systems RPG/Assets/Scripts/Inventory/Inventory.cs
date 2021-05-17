using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<ItemID> items;

    public Inventory() 
    {
        items = new List<ItemID>();
        foreach (var item in BattleCalc.battleCalc.m_mainEntity.GetAllItemsOnPLayer())
        {
            AddItem(item);
        }
        Debug.Log(items.Count);
    }
    public void AddItem(ItemID item)
    {
        items.Add(item);
    }
}
