using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestSceneManager : MonoBehaviour
{
    public GameObject mainPlayer;
    [SerializeField] private PlayArea playarea;

    public GameObject playerObjPrefab;

    private Material defaultMaterial;
    public Material error_RedMaterial;
    public bool isMissionCompleted = false;
    public bool DoingColliderAct = false;
    public GameObject colliderObj = null;
    private string Interactable_ColliderName;
    public string trigname;
    [SerializeField] private string missionTargetObj;

    public List<ItemObject> inventoryCollectableObjects;
    public ItemObject nearestItem;
    public Transform table;

    public int sphereRadius;

    [SerializeField] private GameObject pickupPopUp;
    [SerializeField] private TextMeshProUGUI pickupPopUpText;
    [SerializeField] private GameObject rotatePopUp;
    [SerializeField] private GameObject mobileRotatePopUp;
    [SerializeField] private TextMeshProUGUI rotatePopUpText;

    public LayerMask layerMask;
    public bool canSwitchFPP = false;

    public string currentObject;

    [SerializeField] private Animator doorAnimator;
    [SerializeField] private List<TriggersManager> doorTriggers;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //InitializeTestScene();
        //mobileRotatePopUp = mainPlayer.GetComponent<PlayerFunctionsController>().rotateBtn;
    }

    void InitializeTestScene()
    {
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/PlayerPrefabs/Main/__Player", typeof(GameObject));  // Load Player
        Instantiate(SelectedPrefabObj, new Vector3(0, 2, 0), Quaternion.identity);  // Instantiate Player 
        mainPlayer = GameObject.Find("TPP_Player");
        GameObject spawnpoint = mainPlayer.GetComponent<Teleporting>().FindClosestSpawnPoint();  //find spawnpoint
        //teleport Player
        mainPlayer.GetComponent<CharacterController>().enabled = false;
        mainPlayer.GetComponent<CharacterController>().transform.position = spawnpoint.transform.position;
        mainPlayer.GetComponent<CharacterController>().enabled = true;
        //Diable Player Camera Auido Listerner
        //GameObject.Find("MainCamera").GetComponent<AudioListener>().enabled = false;
        //gameObject.GetComponent<Camera>().enabled = false;
    }

    private int maxTomatoes = 5;
    private int currTomatoes;
    private bool collectedTomatoes = false;
    private bool collectedWheat = false;
    private bool collectedWater = false;
    private void Update()
    {
        //EnteredTriggerName();
        //HoverdColliderName();
        //DoTriggerActions();
        //CheckForPickableObject();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!collectedTomatoes || !collectedWheat || !collectedWater)
            {
                StartCoroutine(CheckForItems());
            }
        }
    }

    IEnumerator CheckForItems()
    {
        yield return new WaitForSeconds(1);
        if (InventoryManager.Instance.items.Count != 0)
        {
            foreach (Item itm in InventoryManager.Instance.items)
            {
                if (itm.item.itemName == "Tomato" && !collectedTomatoes)
                {
                    InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                    collectedTomatoes = true;
                    break;
                }
                else if (itm.item.itemName == "Wheat" && !collectedWheat)
                {
                    InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                    collectedWheat = true;
                    break;
                }
                else if (itm.item.itemName == "Water" && !collectedWater)
                {
                    InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                    collectedWheat = true;
                    break;
                }
            }
        }
    }

    void CheckForPickableObject()
    {

        if (mainPlayer != null)
        {

            if (mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj != null && mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj.layer == LayerMask.NameToLayer("InventoryObjects"))
            {
                colliderObj = mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj;
                Interactable_ColliderName = mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj.name;
                PlayerPickup playerpickup = mainPlayer.GetComponent<PlayerFunctionsController>().playerPickup;

                if (mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj.tag == "Allignment")
                {
                    playerpickup.alligningParent = mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj.transform;
                }
                else
                {
                    playerpickup.alligningParent = null;
                }

                if (Vector3.Distance(mainPlayer.transform.position, colliderObj.transform.position) < 5)
                {
                    ItemObject itmObj = colliderObj.GetComponent<ItemObject>();
                    ShowPickUpPopUp(itmObj);

                    if (colliderObj.GetComponent<TargetController>() != null)
                    {
                        if (colliderObj.GetComponent<TargetController>().type == TargetController.TargetType.Rotatable)
                        {
                            if (PlayerFunctionsController.GetGamemode() == 1)
                                rotatePopUp.SetActive(true);

                            else
                                mobileRotatePopUp.SetActive(true);
                        }
                    }


                    if (Interactable_ColliderName != "")
                    {
                        Debug.Log("collider:" + Interactable_ColliderName);
                        if (mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed || Input.GetKeyDown(KeyCode.E))
                        {
                            mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                            StartCoroutine(DoColliderAction(colliderObj));
                        }
                        if (Input.GetKeyDown(KeyCode.R) || mainPlayer.GetComponent<PlayerFunctionsController>().isRotatePressed)
                        {
                            mainPlayer.GetComponent<PlayerFunctionsController>().isRotatePressed = false;
                            UseObject(colliderObj);
                        }
                    }
                }
                else
                {
                    HidePickUpPopUp();
                }
            }
            else
            {
                if (rotatePopUp.activeInHierarchy)
                    rotatePopUp.SetActive(false);
                else if (mobileRotatePopUp.activeInHierarchy)
                    mobileRotatePopUp.SetActive(false);

                PlayerFunctionsController playermove = mainPlayer.GetComponent<PlayerFunctionsController>();

                if (playermove.playerPickup.canPickup && playermove.playerPickup.nearbyPickupObjects.Count > 0)
                {

                    colliderObj = playermove.playerPickup.NearByObject();
                    if (colliderObj != null)
                    {

                        Interactable_ColliderName = colliderObj.name;
                        ItemObject itmObj = colliderObj.GetComponent<ItemObject>();
                        ShowPickUpPopUp(itmObj);
                        if (Interactable_ColliderName != "")
                        {
                            Debug.Log("collider:" + Interactable_ColliderName);
                            if (mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed || Input.GetKeyDown(KeyCode.E))
                            {
                                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                                StartCoroutine(DoColliderAction(colliderObj));
                            }
                        }
                    }
                }
                else if (!canSwitchFPP)
                {
                    HidePickUpPopUp();
                }
            }

        }
    }

    private void UseObject(GameObject hitObject)
    {
        TargetController target = hitObject.GetComponent<TargetController>();
        if (target != null)
        {
            Debug.Log("FOUND TARGET");
            hitObject.GetComponent<TargetController>().Output();
        }
    }

    IEnumerator DoColliderAction(GameObject objec)
    {
        GameObject hitObject = objec;
        mainPlayer.GetComponent<PlayerFunctionsController>().PickUpAnim();


        yield return new WaitForSeconds(0.5f);

        if (Interactable_ColliderName == missionTargetObj)
        {
            hitObject.tag = "Untagged";
            mainPlayer.GetComponent<PlayerFunctionsController>().playerPickup.nearbyPickupObjects.Remove(hitObject);
            hitObject.SetActive(false);
            currentObject = hitObject.name;
            if (playarea.currentStep == 0) playarea.NextObjective();


        }

        else
        {
            hitObject.tag = "Untagged";
            mainPlayer.GetComponent<PlayerFunctionsController>().playerPickup.nearbyPickupObjects.Remove(hitObject);
            hitObject.SetActive(false);
            currentObject = hitObject.name;
        }

        yield return null;
        AddItemsInInventory(hitObject);
    }

    void AddItemsInInventory(GameObject hitObject)
    {
        ItemObject itObj = hitObject.GetComponent<ItemObject>();

        foreach (TriggersManager triggersManager in doorTriggers)
        {
            if (triggersManager.items.Contains(itObj))
            {
                Debug.Log("IN TRIGGER AREA");
                triggersManager.items.Remove(itObj);
                triggersManager.CheckItems();
            }
        }


        if (itObj != null)
        {
            for (int i = 0; i < InventoryManager.Instance.items.Count; i++)
            {
                if (InventoryManager.Instance.items[i].item == itObj.item)
                {
                    InventoryManager.Instance.items[i].count++;
                    InventoryManager.Instance.UpdateInventoryUI(i, false);
                    return;
                }
            }
            hitObject.transform.localScale = hitObject.GetComponent<ItemObject>().shrinkSize;
            InventoryManager.Instance.AddItem(itObj.item, hitObject);
        }
    }

    void EnteredTriggerName()
    {
        if (mainPlayer != null)
        {
            trigname = mainPlayer.GetComponent<GameTriggersManager>()._triggername;
            Debug.Log("TRIGGER NAME - " + trigname);
        }
    }

    void DoTriggerActions()
    {
        if (trigname == "DropObj_Trigger" && mainPlayer.GetComponent<PlayerFunctionsController>().hasObjectOnHand)
        {
            Debug.Log("INTERACTED WITH TRIGGER");
            if (Input.GetMouseButtonDown(0) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                currentObject = InventoryManager.Instance.collectedItems[InventoryManager.Instance.currentSelectedObjIndex].name;
                // check if the dropped object is correct
                if (currentObject == missionTargetObj)
                {
                    isMissionCompleted = true;
                    playarea.NextObjective();
                    InventoryManager.Instance.DropObject();
                }
                else if (currentObject == null)
                {
                    isMissionCompleted = false;
                }
                else
                {
                    isMissionCompleted = false;
                }
            }
        }
    }

    IEnumerator RedDefaultMat()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        colliderObj.GetComponent<Renderer>().material = error_RedMaterial;
        StartCoroutine(DefaultMat());
    }

    IEnumerator DefaultMat()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        colliderObj.GetComponent<Renderer>().material = defaultMaterial;

        DoingColliderAct = true;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

    public void ShowPickUpPopUp(ItemObject obj)
    {
        if (PlayerFunctionsController.GetGamemode() == 1)
        {
            pickupPopUp.SetActive(true);
            if (obj != null)
            {
                pickupPopUpText.text = "E to Pickup " + Interactable_ColliderName;
            }
            else
            {
                pickupPopUpText.text = "Press E to use";
            }
        }
    }

    public void HidePickUpPopUp()
    {
        if (pickupPopUp.activeInHierarchy)
        {
            pickupPopUp.SetActive(false);
        }

        if (rotatePopUp.activeInHierarchy)
        {
            rotatePopUp.SetActive(false);
        }
    }

    public void CheckForDoorOpen()
    {
        foreach (TriggersManager triggersManager in doorTriggers)
        {
            if (!triggersManager.hasFilled) return;
        }

        OpenDoor();
    }

    public void OpenDoor()
    {
        doorAnimator.SetTrigger("Open");
    }

  

    //Physical and chemaical module
    public List<Button> questionBtn = new List<Button>();
    public GameObject questions;
    public int expCount = 0;
    private TargetController currentMiniGame;
    public void PlayAnim(TargetController obj)
    {

        int correctIndex = int.Parse(obj.checkName);

        currentMiniGame = obj;

        for (int i = 0; i < questionBtn.Count; i++)
        {
            if (i == correctIndex)
            {
                questionBtn[i].onClick.AddListener(CorrectAnswer);
            }
            else
            {
                questionBtn[i].onClick.AddListener(WrongAnswer);
            }
        }
        StartCoroutine(QuestionDelay());
        Animator anim = obj.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Trigger");
    }

    public void CorrectAnswer()
    {
        questions.SetActive(false);

        expCount++;
        currentMiniGame.EndMiniGame();

        if (expCount == 5)
        {
            //InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void WrongAnswer()
    {
        questions.SetActive(false);

        Debug.Log("Mission Failed");
    }

    public void MiniGameStart()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MiniGameEnd()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator QuestionDelay()
    {
        yield return new WaitForSeconds (3);
        questions.SetActive(true);
    }

    public void SetActiveObject(TargetController obj)
    {
        int correctIndex = int.Parse(obj.checkName);


        currentMiniGame = obj;

        for (int i = 0; i < questionBtn.Count; i++)
        {
            if (i == correctIndex)
            {
                questionBtn[i].onClick.AddListener(CorrectAnswer);
            }
            else
            {
                questionBtn[i].onClick.AddListener(WrongAnswer);
            }
        }
        StartCoroutine (QuestionDelay());
        //obj.subObject.gameObject.SetActive(true);
    }


    public Material paintMat;
    public MeshRenderer pipeMesh;
    public void PaintPipe()
    {
        pipeMesh.material = paintMat;
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void Expirement3(Animator animator)
    {
        animator.SetTrigger("Trigger");
        StartCoroutine(ExpirementDelay());
    }

    IEnumerator ExpirementDelay()
    {
        yield return new WaitForSeconds(2);
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    //chemical reactions

    public void MissionFailed()
    {
        Debug.Log("Mission Failed");
    }

    //Body Movements


}
