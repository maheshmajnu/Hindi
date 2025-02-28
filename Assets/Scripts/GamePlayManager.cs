using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;


[System.Serializable]
public class Subtitle
{
    public string subTitleText;
    public float delay;
}

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private PlayArea playarea;

    [HideInInspector] public GameObject mainPlayer;
    [HideInInspector] public PlayerFunctionsController playerFunctionsController;

    [SerializeField] private GameObject colliderObj = null;
    private string Interactable_ColliderName;

    [SerializeField] private GameObject pickupPopUp;
    [SerializeField] private TextMeshProUGUI pickupPopUpText;
    public GameObject rotatePopUp;
    [SerializeField] private GameObject mobileRotatePopUp;
    [SerializeField] private TextMeshProUGUI rotatePopUpText;

    public bool canSwitchFPP = false;

    public string currentObject;
    private string missionTargetObj;

    [SerializeField] private List<TriggersManager> doorTriggers;

    [SerializeField] private CanvasGroup fadeImg;

    private void Start()
    {
        //StartCoroutine(InitializeDelay());
        
    }

    IEnumerator InitializeDelay()
    {
        yield return new WaitForSeconds(0.05f);
        
        InitializeScene();
        
    }

    private void Update()
    {
        CheckForPickableObject();
    }

    public void InitializeScene()
    {

        // Load Player
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/PlayerPrefabs/Main/__Player", typeof(GameObject));

        // Instantiate Player 
        Instantiate(SelectedPrefabObj, new Vector3(0, 2, 0), Quaternion.identity);
        mainPlayer = GameObject.Find("TPP_Player");
        playerFunctionsController = mainPlayer.GetComponent<PlayerFunctionsController>();
        InventoryManager.Instance.player = playerFunctionsController;

        //find spawnpoint
        GameObject spawnpoint = mainPlayer.GetComponent<Teleporting>().FindClosestSpawnPoint();

        //teleport Player
        mainPlayer.GetComponent<CharacterController>().enabled = false;
        mainPlayer.GetComponent<CharacterController>().transform.position = spawnpoint.transform.position;
        mainPlayer.GetComponent<CharacterController>().enabled = true;
        mobileRotatePopUp = playerFunctionsController.rotateBtn;
        

    }

    private void CheckForPickableObject()
    {
        if (playerFunctionsController != null)
        {
            //checking if any object hit on raycast 
            if (playerFunctionsController.CrosshairHitObj != null && playerFunctionsController.CrosshairHitObj.layer == LayerMask.NameToLayer("InventoryObjects"))
            {
                //Assigning the hit object
                colliderObj = playerFunctionsController.CrosshairHitObj;
                Interactable_ColliderName = playerFunctionsController.CrosshairHitObj.name;

                PlayerPickup playerpickup = playerFunctionsController.playerPickup;
                //Checking for the alligner
                if (playerFunctionsController.CrosshairHitObj.tag == "Allignment")
                {
                    playerpickup.alligningParent = playerFunctionsController.CrosshairHitObj.transform;
                }
                else
                {
                    playerpickup.alligningParent = null;
                }

                //checking for the distance between object and player
                if (Vector3.Distance(mainPlayer.transform.position, colliderObj.transform.position) < 5)
                {
                    ItemObject itmObj = colliderObj.GetComponent<ItemObject>();
                    if(itmObj != null) ShowPickUpPopUp(itmObj);

                    //checks if its also a target that can be controlled
                    if (colliderObj.GetComponent<TargetController>() != null && colliderObj.GetComponent<TargetController>().enabled)
                    {
                        if (colliderObj.GetComponent<TargetController>().type == TargetController.TargetType.Rotatable)
                        {
                            if (PlayerFunctionsController.GetGamemode() == 1)
                                rotatePopUp.SetActive(true);

                            else
                                ShowInteraction();
                        }
                        else
                        {
                            if(!colliderObj.GetComponent<TargetController>().miniGameStarted)
                            {
                                if (PlayerFunctionsController.GetGamemode() == 1)
                                {
                                    rotatePopUp.SetActive(true);
                                    rotatePopUpText.text = "Press E";
                                }
                                else
                                {
                                    ShowInteraction();
                                }
                            }
                        }
                    }
                    else
                    {
                        HidePickUpPopUp();
                    }


                    if (Interactable_ColliderName != "")
                    {
                        //Picks up if intraction or E is pressed
                        if ((playerFunctionsController.InteractIspressed || Input.GetKeyDown(KeyCode.E)) && itmObj != null)
                        {
                            playerFunctionsController.InteractIspressed = false;
                            StartCoroutine(DoColliderAction(colliderObj));
                        }
                        //Rotates if rotate or R is pressed
                        if (Input.GetKeyDown(KeyCode.E) || playerFunctionsController.InteractIspressed)
                        {
                            playerFunctionsController.InteractIspressed = false;
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
                //disables the rotate popup 
                if (rotatePopUp.activeInHierarchy)
                    rotatePopUp.SetActive(false);
                else if (mobileRotatePopUp.activeInHierarchy)
                    mobileRotatePopUp.SetActive(false);

                //checks if there are any nearby objects
                if (playerFunctionsController.playerPickup.canPickup && playerFunctionsController.playerPickup.nearbyPickupObjects.Count > 0)
                {
                    //gets the closest object
                    colliderObj = playerFunctionsController.playerPickup.NearByObject();
                    if (colliderObj != null)
                    {
                        Interactable_ColliderName = colliderObj.name;
                        ItemObject itmObj = colliderObj.GetComponent<ItemObject>();
                        if (itmObj != null) ShowPickUpPopUp(itmObj);
                        if (Interactable_ColliderName != "" && itmObj != null)
                        {
                            //picks up object
                            if (playerFunctionsController.InteractIspressed || Input.GetKeyDown(KeyCode.E))
                            {
                                playerFunctionsController.InteractIspressed = false;
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

    IEnumerator DoColliderAction(GameObject objec)
    {
        GameObject hitObject = objec;
        playerFunctionsController.PickUpAnim();


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
        itObj.pickUpEvent?.Invoke();

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
            

            

            hitObject.transform.localScale = itObj.shrinkSize;

            InventoryManager.Instance.AddItem(itObj.item, hitObject);
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

        else
        {
            ShowInteraction();
        }
    }

    public void ShowInteraction()
    {
        if(!playerFunctionsController.hasObjectOnHand)
        {
            playerFunctionsController.InteractBtn.gameObject.SetActive(true);
        }
        else
        {
            playerFunctionsController.InteractBtn.gameObject.SetActive(false);
        }
    }

    public void HidePickUpPopUp()
    { 
        if (pickupPopUp.activeInHierarchy) // bug fix
        {
            pickupPopUp.SetActive(false);
        }

        if (rotatePopUp.activeInHierarchy)
        {
            rotatePopUp.SetActive(false);
        }

        if(playerFunctionsController.InteractBtn.gameObject.activeInHierarchy)
        {
            playerFunctionsController.InteractBtn.gameObject.SetActive(false);
        }
    }

    public void FadeOut()
    {
        fadeImg.alpha = 1.0f;
        fadeImg.DOFade(0, 1f);
    }

    public void FadeInAndOut()
    {
        fadeImg.alpha = 0f;
        fadeImg.DOFade(1, 1f).OnComplete(() => 
            fadeImg.DOFade(0, 1f)
        );
    }
}
