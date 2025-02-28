using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftableList", menuName = "Crafting/CraftingList", order = 2)]

public class CraftableItemsListSO : ScriptableObject
{
   public List<CraftedItem> items = new List<CraftedItem>();
}
