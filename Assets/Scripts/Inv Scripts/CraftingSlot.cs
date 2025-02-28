using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlot : MonoBehaviour, IDropHandler
{

    private CraftingManager _craftingManager;

    private void Start()
    {
        _craftingManager = FindObjectOfType<CraftingManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
           GameObject droppedObject = (GameObject)eventData.pointerDrag;
            InventorySlot inventorySlot = droppedObject.GetComponent<InventorySlot>();
            inventorySlot.parentAfterDrag = transform;
            _craftingManager.slots.Add(inventorySlot);
            _craftingManager.CheckForCraft(inventorySlot.item);
            
        }
    }
}
