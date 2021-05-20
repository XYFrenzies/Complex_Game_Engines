using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Inventory : MonoBehaviour
{
    private GameObject player;
    private Inventory inventory;
    private Transform itemContainer;
    private Transform itemTemplate;
    [SerializeField] private float SpaceBetweenItemsX = 75f;
    [SerializeField] private float SpaceBetweenItemsY = 55f;
    private bool onEventChangeDropItem = false;
    private bool onEventChangeUseItem = false;
    private ItemID itemToBeUsed = null;
    private void Awake()
    {
        itemContainer = transform.Find("ItemSlotContainer");
        itemTemplate = itemContainer.Find("ItemSlotTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.onItemsListChange += Inventory_OnItemListChange;
        RefreshInventoryItems();
    }
    public void SetPlayer(GameObject player) 
    {
        this.player = player;
    }
    private void Inventory_OnItemListChange(object sender, System.EventArgs e) 
    {
        RefreshInventoryItems();
    }
    //This is for if the player wants to drop an item from the inventory.
    public void DropItem(ItemID item)
    {
        onEventChangeDropItem = true;
        itemToBeUsed = item;
    }
    //This is for if the player presses the item being used 
    //(the item will be returned as a result of the press of the action)
    public void UseItem(ItemID item) 
    {
        onEventChangeUseItem = true;
        itemToBeUsed = item;
    }
    private void RefreshInventoryItems() 
    {
        foreach (Transform child in itemContainer)
        {
            if (child == itemTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        foreach (var item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemTemplate, itemContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            if (itemToBeUsed != null)
            {
                if (onEventChangeDropItem && itemToBeUsed == item)
                {
                    //ItemID duplicatedItem = DuplicateItem(item);
                    //inventory.RemoveItem(item);
                    //ItemWorld.DropItem(player.transform.position, duplicatedItem);
                    //onEventChangeDropItem = false;
                    //itemToBeUsed = null;
                }
                if (onEventChangeUseItem && itemToBeUsed == item)
                {
                    inventory.UseItem(item);
                    onEventChangeUseItem = false;
                    itemToBeUsed = null;
                }
            }

            itemSlotRectTransform.anchoredPosition = new Vector2(x * SpaceBetweenItemsX, y * SpaceBetweenItemsY);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.sprite;
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("ItemAmount").GetComponent<TextMeshProUGUI>();
            if(item.m_amountOfItemsForPlayer > 1)
                uiText.SetText(item.m_amountOfItemsForPlayer.ToString());
            else
                uiText.SetText("");
            x++;
            if (x >= 4)
            {
                x = 0;
                y--;
            }
        }
    }
    //public ItemID DuplicateItem(ItemID item) 
    //{
    //    ItemID duplicateItem = new ItemID { allStatsEffected = item.allStatsEffected, customizedItem = item.customizedItem, description = item.description, durability = item.durability,
    //        isAPercentage = item.isAPercentage, isDurability = item.isDurability, isStackable = item.isStackable, m_amountOfItemsForPlayer = item.m_amountOfItemsForPlayer, properties = item.properties,
    //        name = item.name, itemType = item.itemType, sprite = item.sprite, valueOfItem = item.valueOfItem, statusNames = item.statusNames, m_statIndex = item.m_statIndex, variation = item.variation, status = item.status };
    //    return duplicateItem;
    //}
}
