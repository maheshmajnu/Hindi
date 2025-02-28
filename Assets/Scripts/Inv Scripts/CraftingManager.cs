using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    public CraftableItemsListSO craftableItemList;
    [SerializeField] private List<CraftedItem> items = new();
    [SerializeField] private List<Ingredient> ingredients = new();
    public List<InventorySlot> slots = new();
    private PlayerFunctionsController playerFunctionsController;
    private int numberOfItemsCrafted;
    private Image craftFillBar;

    private void Awake()
    {
    }

    private void Start()
    {   
        playerFunctionsController = FindObjectOfType<PlayerFunctionsController>();
        craftFillBar = playerFunctionsController.craftFillBar;
    }

    public void CheckForCraft(InventoryItem slot)
    {
        Debug.Log("Item - " + slot);
        Ingredient ingredient = new()
        {
            Item = slot
        };
        ingredients.Add(ingredient);
        Debug.Log("CheckForCraft: Added ingredient. Total ingredients: " + items.Count);

        //InventoryManager.Instance.DropObject();
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i].ingredients);
            if (AreListsEqual(items[i].ingredients,ingredients))
            {
                Debug.Log(ingredients.Count);
                Debug.Log("CRAFTED " + items[i].name);
                StartCoroutine(CraftingItemTransition(items[i].craftedItemSO, items[i].craftedItemPrefab));
                return;
            }
            else
            {
                Debug.Log("Not Matching Ingredients");
            }
        }
    }

    [ContextMenu("ZOOM IN")]
    public void ZoomIn()
    {
        Vector3 startPosition = new Vector3(0, 0, 3); Vector3 targetPosition = new Vector3(0, 0, 2);
        GameObject ActiveCamera = playerFunctionsController.playerControlManager.ActiveCamera;
        ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, 2);
    }

    [ContextMenu("ZOOM OUT")]
    public void ZoomOut()
    {
        Vector3 startPosition = new Vector3(0, 0, 2); Vector3 targetPosition = new Vector3(0, 0, 3);
        GameObject ActiveCamera = playerFunctionsController.playerControlManager.ActiveCamera;
        ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, 2);
    }

    IEnumerator CraftingItemTransition(InventoryItem item, GameObject itemObj)
    {
        float duration = 2f;
        float elapsedTime = 0f;
        float progress = 0f; // Progress of the zooming

        Vector3 startPosition = new Vector3(0, 0, 3);
        Vector3 targetPosition = new Vector3(0, 0, 2);

        GameObject ActiveCamera = playerFunctionsController.playerControlManager.ActiveCamera;

        playerFunctionsController.animator.SetBool("Crafting", true);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, t);
            UpdateProgressBar(progress); // Update progress bar
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / duration;
            yield return null;
        }

        yield return new WaitForSeconds(2);

        UpdateProgressBar(1);

        elapsedTime = 0f;
        startPosition = new Vector3(0, 0, 2);
        targetPosition = new Vector3(0, 0, 3);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerFunctionsController.animator.SetBool("Crafting", false);

        CraftItem(item, itemObj);
        UpdateProgressBar(0);
    }


    // Method to update the zoom progress bar
    void UpdateProgressBar(float progress)
    {
        if (craftFillBar != null)
        {
            craftFillBar.fillAmount = progress;
        }
    }

    void CraftItem(InventoryItem item, GameObject itemObj)
    {
        InventoryManager.Instance.AddItem(item,itemObj);
        Debug.Log(slots.Count);

        for(int i = 0;i < slots.Count;i++)
        {
            int index = InventoryManager.Instance.invSlotsPrefabs.IndexOf(slots[i]);
            Debug.Log(index);
            InventoryManager.Instance.currentSelectedObjIndex = index;
            InventoryManager.Instance.DropObject();
        }
        
        playerFunctionsController.UpdateCraftingUI(numberOfItemsCrafted, item.banner);
        numberOfItemsCrafted++;
    }

    bool AreListsEqual(List<Ingredient> listA, List<Ingredient> listB)
    {
        if (listA.Count != listB.Count)
            return false;

        // Create dictionaries to count occurrences of each item in both lists
        Dictionary<InventoryItem, int> countA = new Dictionary<InventoryItem, int>();
        Dictionary<InventoryItem, int> countB = new Dictionary<InventoryItem, int>();

        // Count occurrences in list A
        foreach (Ingredient item in listA)
        {
            if (countA.ContainsKey(item.Item))
                countA[item.Item]++;
            else
                countA[item.Item] = 1;
        }

        // Count occurrences in list B
        foreach (Ingredient item in listB)
        {
            if (countB.ContainsKey(item.Item))
                countB[item.Item]++;
            else
                countB[item.Item] = 1;
        }

        // Compare the counts of each item in both dictionaries
        foreach (var kvp in countA)
        {
            if (!countB.ContainsKey(kvp.Key) || countB[kvp.Key] != kvp.Value)
                return false;
        }

        return true;
    }


}
