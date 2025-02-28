using Cinemachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public GamePlayManager manager;
    public PlayerFunctionsController player;

    public List<Item> items;
    public List<Image> invSlots;
    public List<InventorySlot> invSlotsPrefabs;
    public List<GameObject> collectedItems;
    public int currentSelectedObjIndex;

    public GameObject inventry;
    public GameObject inventryStatic;
    public GameObject invSlotPrefab;
    public Transform invSlotHolder;

    public Button invButton;
    public Button invCloseBtn;

    bool isDroping = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        invButton?.onClick.AddListener(InventoryToggle);
    }

    private void Update()
    {
        // Check for key press to open inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryToggle();
        }
    }

    public void InventoryToggle()
    {
        LookTowardsCamera();
        // Open the inventory
        OpenInventory();
    }



    public void LookTowardsCamera()
    {
        Vector3 cameraDirection = GetCinemachineCameraForward();
        player.transform.LookAt(player.mouseWorldPosition);
        Quaternion lookAxis = player.transform.localRotation;
        lookAxis.x = 0f;
        lookAxis.z = 0f;
        player.transform.localRotation = lookAxis;
    }
    public void LookTowardsCameraBack()
    {
        Vector3 cameraDirection = GetCinemachineCameraForward();
        Vector3 oppositeDirection = player.transform.position * 2 - player.mouseWorldPosition;

        player.transform.LookAt(oppositeDirection);
        Quaternion lookAxis = player.transform.localRotation;
        lookAxis.x = 0f;
        lookAxis.z = 0f;
        player.transform.localRotation = lookAxis;
    }

    Vector3 GetCinemachineCameraForward()
    {
        // Get the Cinemachine Virtual Camera component
        CinemachineVirtualCamera virtualCamera = player.playerControlManager.ActiveCamera.GetComponent<CinemachineVirtualCamera>();

        if(virtualCamera == null)
        {
            cinemachineFreelook cinemachineFreelook = player.playerControlManager.ActiveCamera.GetComponent<cinemachineFreelook>();
            return player.playerControlManager.ActiveCamera.transform.forward;
        }

        // Get the forward axis of the virtual camera's transform
        return virtualCamera.transform.forward;
    }

    public void OpenInventory()
    {
        player.OpenInventory();
        //if (!inventry.activeInHierarchy)
        //{
        //    inventry.SetActive(true);
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //    player.OpenInventory();
        //}
        //else if (inventry.activeInHierarchy)
        //{
        //    inventry.SetActive(false);
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
    }

    public void AddItem(InventoryItem item, GameObject objec)
    {
        Item newItem = new Item();
        newItem.count = 1;
        newItem.item = item;
        //newItem.type = item.type;
        items.Add(newItem);


        GameObject invSlot = Instantiate(invSlotPrefab, invSlotHolder);
        InventorySlot slot = invSlot.GetComponent<InventorySlot>();
        slot.item = item;
        slot.index = items.Count - 1;
        slot.countText.text = items[items.Count - 1].count.ToString();
        slot.bgImg.color = Color.yellow;
        slot.itemIcon.sprite = items[items.Count - 1].item.icon;
        invSlotsPrefabs.Add(slot);
        collectedItems.Add(objec);

        

        UpdateInventoryUI(items.Count - 1, false);
    }



    public void UpdateInventoryUI(int index, bool isSelecting)
    {
        foreach (var item in invSlots)
        {
            item.color = Color.white;
        }
        foreach (var item in invSlotsPrefabs)
        {
            item.bgImg.color = Color.black;
        }
        foreach (var item in collectedItems)
        {
            item.SetActive(false);
        }

        //if(player.objectHolder.childCount > 0) Destroy(player.objectHolder.GetChild(0).gameObject);

        if (isSelecting)
        {
            if(StaticVariables.gamemode == 2)
            {
                player.ShootBtn.gameObject.SetActive(true);
            }

            invSlots[index].color = Color.yellow;
            invSlotsPrefabs[index].bgImg.color = Color.yellow;
            collectedItems[index].SetActive(true);
            manager.mainPlayer.GetComponent<PlayerFunctionsController>().InteractBtn.gameObject.SetActive(false);


            if (items[index].item.type == ItemObject.ItemType.weapon)
            {
                manager.mainPlayer.GetComponent<PlayerFunctionsController>().hasWeapomOnHand = true;
                manager.mainPlayer.GetComponent<PlayerFunctionsController>().hasObjectOnHand = false;
                manager.mainPlayer.GetComponent<PlayerFunctionsController>().ShootBtn.GetComponent<Image>().sprite = manager.mainPlayer.GetComponent<PlayerFunctionsController>().shootSprite;
                manager.mainPlayer.GetComponent<PlayerFunctionsController>().ShootingModeUIOn();
            }
            else
            {
                manager.mainPlayer.GetComponent<PlayerFunctionsController>().hasObjectOnHand = true;
                manager.mainPlayer.GetComponent<PlayerFunctionsController>().hasWeapomOnHand = false;
                manager.mainPlayer.GetComponent<PlayerFunctionsController>().ShootBtn.GetComponent<Image>().sprite = manager.mainPlayer.GetComponent<PlayerFunctionsController>().throwSprite;
                manager.mainPlayer.GetComponent<PlayerFunctionsController>().ShootingModeUIOff();
            }           
        }

        if(index < invSlots.Count)
        {
            invSlots[index].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            invSlots[index].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = items[index].item.icon;

            TextMeshProUGUI countText = invSlots[index].gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            countText.text = items[index].count.ToString();

            invSlotsPrefabs[index].countText.text = items[index].count.ToString();
        }

        GameObject obj = GameObject.Find(collectedItems[index].name);

        if (obj == null)
        {
            // Instantiate the Prefab
            obj = Instantiate(collectedItems[index]);

            // Set parent to the player's object holder
            obj.transform.SetParent(player.objectHolder.transform);
        }
        else
        {
            collectedItems[index].transform.SetParent(player.objectHolder.transform);
            collectedItems[index].transform.localPosition = Vector3.zero;
            collectedItems[index].transform.localRotation = Quaternion.identity;
        }


        manager.currentObject = collectedItems[index].name;

        currentSelectedObjIndex = index;

        InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().HidePickUpPopUp();
    }

    public void DropObject()
    {
        //testSceneManager.mainPlayer.GetComponent<PlayerFunctionsController>().InteractBtn.GetComponent<Image>().sprite = testSceneManager.mainPlayer.GetComponent<PlayerFunctionsController>().interactSprite;
        collectedItems[currentSelectedObjIndex].transform.SetParent(null);
        isDroping = true;
        //collectedItems[currentSelectedObjIndex].GetComponent<Rigidbody>().useGravity = true;
        //collectedItems[currentSelectedObjIndex].transform.localScale = collectedItems[currentSelectedObjIndex].GetComponent<ItemObject>().originalSize;
        //collectedItems[currentSelectedObjIndex].transform.position = new Vector3(testSceneManager.mainPlayer.transform.localPosition.x, testSceneManager.mainPlayer.transform.localPosition.y + 0.5f, testSceneManager.mainPlayer.transform.localPosition.z + 0.7f);
        //collectedItems[currentSelectedObjIndex].transform.rotation = Quaternion.identity;
        Destroy(collectedItems[currentSelectedObjIndex],2f);
        collectedItems.Remove(collectedItems[currentSelectedObjIndex]);
        items.Remove(items[currentSelectedObjIndex]);
        //Destroy(invSlotsPrefabs[currentSelectedObjIndex].gameObject);
        invSlotsPrefabs.Remove(invSlotsPrefabs[currentSelectedObjIndex]);
        manager.mainPlayer.GetComponent<PlayerFunctionsController>().hasObjectOnHand = false;
        UpdateBottomBar();
    }

    public void ThrowObject(GameObject thrownObject)
    {
        if (items[currentSelectedObjIndex].count > 1)
        {
            items[currentSelectedObjIndex].count--;
            thrownObject.tag = "Interactable";
            thrownObject.transform.SetParent(null);
        }
        else
        {
            if (StaticVariables.gamemode == 2)
            {
                player.ShootBtn.gameObject.SetActive(false);
            }
            manager.mainPlayer.GetComponent<PlayerFunctionsController>().InteractBtn.GetComponent<Image>().sprite = manager.mainPlayer.GetComponent<PlayerFunctionsController>().shootSprite;
            collectedItems[currentSelectedObjIndex].transform.SetParent(null);
            isDroping = true;
            collectedItems[currentSelectedObjIndex].tag = "Interactable";
            collectedItems.Remove(collectedItems[currentSelectedObjIndex]);
            items.Remove(items[currentSelectedObjIndex]);
            Destroy(invSlotsPrefabs[currentSelectedObjIndex].gameObject);
            invSlotsPrefabs.Remove(invSlotsPrefabs[currentSelectedObjIndex]);
            manager.mainPlayer.GetComponent<PlayerFunctionsController>().hasObjectOnHand = false;
            manager.mainPlayer.GetComponent<PlayerFunctionsController>().ShootBtn.gameObject.SetActive(false);
            UpdateBottomBar();
        }

    }

    void UpdateBottomBar()
    {
        for (int i = 0; i < invSlotsPrefabs.Count; i++)
        {
            invSlotsPrefabs[i].index = i;
        }

        foreach (var item in invSlots)
        {
            item.GetComponent<Image>().color = Color.black;
            item.transform.GetChild(0).gameObject.SetActive(false);
            TextMeshProUGUI countText = item.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            countText.text = "";
        }

        for (int i = 0; i < items.Count; i++)
        {
            invSlots[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            invSlots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = items[i].item.icon;
            TextMeshProUGUI countText = invSlots[i].gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            countText.text = items[i].count.ToString();
        }
    }
}
