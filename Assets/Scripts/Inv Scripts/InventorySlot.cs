using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    public TextMeshProUGUI countText;
    public InventoryItem item;
    public Image bgImg;
    public Image itemIcon;
    public int index;
    private RectTransform rectTransform;
    private PlayerFunctionsController playerFunctionsController;
    [HideInInspector] public Transform parentAfterDrag;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        playerFunctionsController = FindObjectOfType<PlayerFunctionsController>();
    }

    private void Start()
    {
        UpdateIndex();
    }

    public void UpdateIndex()
    {
        index = transform.GetSiblingIndex();
    }

    public void SelectInventoryObject()
    {
        InventoryManager.Instance.UpdateInventoryUI(index,true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        itemIcon.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("On End Drag");
        transform.SetParent(parentAfterDrag);
        itemIcon.raycastTarget = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("On Drag");
        // rectTransform.anchoredPosition += eventData.delta;
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(playerFunctionsController.inventoryCanvas.transform as RectTransform, eventData.position, playerFunctionsController.inventoryCanvas.worldCamera, out pos);
        transform.position = playerFunctionsController.inventoryCanvas.transform.TransformPoint(pos);
        // rectTransform.SetParent(null);
    }
}
