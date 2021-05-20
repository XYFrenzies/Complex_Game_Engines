using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory
{
    public event EventHandler onItemsListChange;
    private List<ItemID> items;
    private Action<ItemID> useItemAction;
    public Inventory(Action<ItemID> useItemAction)
    {
        this.useItemAction = useItemAction;
        items = new List<ItemID>();
        foreach (var item in BattleCalc.battleCalc.m_mainPlayerEntity.GetAllItemsOnPLayer())
        {
            AddItem(item);
        }
        Debug.Log(items.Count);
    }
    public void AddItem(ItemID item)
    {
        if (item.isStackable)
        {
            bool itemAlreadyInInventory = false;
            foreach (ItemID inventoryItem in items)
            {
                if (inventoryItem.name == item.name)
                {
                    inventoryItem.m_amountOfItemsForPlayer += item.m_amountOfItemsForPlayer;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
                items.Add(item);
        }
        else
        {
            items.Add(item);
        }
        onItemsListChange?.Invoke(this, EventArgs.Empty);
    }
    public List<ItemID> GetItemList()
    {
        return items;
    }
    public void RemoveItem(ItemID item) 
    {
        if (item.isStackable)
        {
            ItemID itemInInventory = null;
            foreach (ItemID inventoryItem in items)
            {
                if (inventoryItem.name == item.name)
                {
                    inventoryItem.m_amountOfItemsForPlayer -= item.m_amountOfItemsForPlayer;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.m_amountOfItemsForPlayer <= 0)
                items.Remove(itemInInventory);
        }
        else
        {
            items.Remove(item);
        }
        onItemsListChange?.Invoke(this, EventArgs.Empty);
    }
    public void UseItem(ItemID item) 
    {
        useItemAction(item);
    }
}
