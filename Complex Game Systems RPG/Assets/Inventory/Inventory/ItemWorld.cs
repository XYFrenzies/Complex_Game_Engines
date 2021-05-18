using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemInWorld(Vector3 position, ItemID item) 
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }
    private ItemID item;
    private SpriteRenderer renderer;
    private TextMeshPro textMeshPro;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    public void SetItem(ItemID item)
    {
        this.item = item;
        renderer.sprite = item.sprite;
        if(item.m_amountOfItemsForPlayer > 1)
            textMeshPro.SetText(item.m_amountOfItemsForPlayer.ToString());
        else
            textMeshPro.SetText("");
    }
    public ItemID GetItem() 
    {
        return item;
    }
    public void DestroySelf() 
    {
        Destroy(gameObject);
    }
    public static ItemWorld DropItem(Vector3 dropPosition, ItemID item) 
    {
        Vector3 rand = new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 5));
        ItemWorld itemWorld = SpawnItemInWorld(dropPosition + rand * 5f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(rand * 5f, ForceMode2D.Impulse);
        return itemWorld;
    }
}
