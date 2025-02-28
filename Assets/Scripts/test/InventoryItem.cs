using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemObject;

[System.Serializable]
public class CraftedItem
{
    public string name;
    public Sprite icon;
    public GameObject craftedItemPrefab;
    public InventoryItem craftedItemSO;
    public List<Ingredient> ingredients;
}

[System.Serializable]
public class Ingredient
{
    public InventoryItem Item;
}

[System.Serializable]
public class Item
{
    public int count;
    public InventoryItem item;
    public ItemType type;
}

[CreateAssetMenu(fileName = "item", menuName = "Inventory/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject
{
    public Sprite icon;
    public Sprite banner;
    public string itemName;   
    public ItemType type;
}
