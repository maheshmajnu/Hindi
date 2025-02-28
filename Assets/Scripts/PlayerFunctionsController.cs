using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerFunctionsController : MonoBehaviour
{
    public ThirdPersonController ThirdPersonController;
    public CharacterController controller;
    public PlayerPickup playerPickup;
    public TragetDetector targetDetector;
    public Animator animator;

    [Header("Ground Props")]
    private float groundedTimer;        // to allow jumping when going down ramps => greater than 0 means grounded
    private float jumpHeight = 1.0f;
    private float gravityValue = 9.81f;
    private bool groundedPlayer;
    public bool DebugGroundCheck;


    [Header("Player Props")]
    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;
    public float runSpeed = 5.0f;

    [Header("Camera Props")]
    public Camera MyCamera;
    public Transform Cam;
    float turnSmoothness;
    public float turnSmoothing = 0.1f;

    [Header("Touch Controls")]
    public VariableJoystick variableJoystick;
    public GameObject variableJoystickUI;
    public Button sprintBtn;
    private bool sprintBtnState = false;
    public Button jumpBtn;
    private bool jumpBtnState = false;
    public Button InteractBtn;
    public Sprite shootSprite;
    public Sprite throwSprite;
    public bool InteractIspressed = false;
    public bool isRotatePressed = false;

    [Header("Crosshair")]
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();
    public Transform DebugTransform;
    public GameObject CrosshairHitObj;
    [SerializeField]
    private GameObject PlayerAimPoint;

    public Transform objectHolder;
    public Transform bagHolder;
    public bool hasObjectOnHand = false;
    public bool hasWeapomOnHand = false;

    [SerializeField] public static int gamemode = 1;   //1=pc,2=mobile
    private bool Shoot = false;
    private bool aim = false;
    public Vector3 mouseWorldPosition;
    public PlayerControlManager playerControlManager;
    public Vector3 AimZoom;
    public float timer = 0;
    public Button AimBtn;
    private bool AimBtnState = false;
    public Button ShootBtn;
    public GameObject rotateBtn;
    public bool ShootState = false;
    public GameObject pfBulletprojectile;

    private bool isOnFPP = false;
    public CinemachineVirtualCamera inventoryCamera;
    public CinemachineVirtualCamera mainPCCamera;
    public Canvas inventoryCanvas;
    public Image craftFillBar;
    public Transform invItemSlotsHolder;

    public List<GameObject> craftSlots;
    public List<GameObject> craftItems;

    public float origionalCamZoomSize;
    public GameObject mask;
    public Camera uiCamera;

    public UnityEvent triggerEvent;
    public bool canTriggerEvent;

    public Button invCloseBtn;

    public Transform mobileLookAtPoint;
    public Transform mobileInventoryCanvas;
    private const string mobileCamXAxis = "Mouse X";
    private const string mobileCamYAxis = "Mouse Y";
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();


        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        

        sprintBtn.onClick.AddListener(MobileRun);

        jumpBtn.onClick.AddListener(MobileJump);

        Button rotBtn = rotateBtn.GetComponent<Button>();
        rotBtn.onClick.AddListener(MobileRotate);

        //InteractBtn.onClick.AddListener(MobileInteract);
        shootSprite = InteractBtn.GetComponent<Image>().sprite;

        AimBtn.onClick.AddListener(MobileAim);

        ShootBtn.onClick.AddListener(MobileShoot);

        corouteneIsRunning = true;

        inventoryCanvas.gameObject.SetActive(false);

        InventoryManager.Instance.invSlotHolder = invItemSlotsHolder;

        InventoryManager.Instance.invCloseBtn = invCloseBtn;

        //origionalCamZoomSize = inventoryCamera.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

        DetectObjects();
        //CheckForFPP();

        if (hasWeapomOnHand) WeaponOnHand();
        if (targetDetector != null && targetDetector.canShootObject) ShootTarget();

        if(StaticVariables.gamemode == 1 && Input.GetMouseButtonDown(0) && hasObjectOnHand && !hasWeapomOnHand)
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                InventoryManager.Instance.LookTowardsCamera();
                if (Vector3.Distance(transform.position, mouseWorldPosition) < 4)
                {
                    animator.SetTrigger("PickUp");
                    ThrowObject();
                }
                else
                {
                    animator.SetTrigger("Throw");
                }
            }
        }
        else if (StaticVariables.gamemode == 2 && ShootState && hasObjectOnHand && !hasWeapomOnHand)
        {
            ShootState = false;
            InventoryManager.Instance.LookTowardsCamera();
            if (Vector3.Distance(transform.position, mouseWorldPosition) < 4)
            {
                animator.SetTrigger("PickUp");
                DropObject();
            }
            else
            {
                animator.SetTrigger("Throw");
            }
        }

        if(StaticVariables.gamemode == 2 && hasObjectOnHand)
        {
            ShootBtn.gameObject.SetActive(true);
        }
    }

    public void OpenInventory()
    {
        if (!inventoryCanvas.gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            InventoryManager.Instance.LookTowardsCameraBack();
            InventoryManager.Instance.invCloseBtn.gameObject.SetActive(true);
            //uiCamera.gameObject.SetActive(true);
            inventoryCanvas.gameObject.SetActive(true);
            //inventoryCamera.Priority = 20;
            Vector3 startPosition = new Vector3(0.65f, 0, 0); Vector3 targetPosition = new Vector3(1.1f, -0.66f, 2.06f);
            GameObject ActiveCamera = playerControlManager.ActiveCamera;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_XAxis.Value = 0.33f;
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, timer);
            ActiveCamera.GetComponent<CinemachineFreeLook>().LookAt = mobileInventoryCanvas;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Height = 0.22f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Height = 0.22f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Height = 0.22f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = 4f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = 4f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = 4f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "";
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "";
            this.GetComponent<ThirdPersonController>().LockCameraPosition = true;
            InventoryManager.Instance.inventryStatic.SetActive(false);
            this.gameObject.GetComponent<ThirdPersonController>().canMove = false;
            InteractBtn.gameObject.SetActive(false);
            jumpBtn.gameObject.SetActive(false);
            sprintBtn.gameObject.SetActive(false);
            variableJoystickUI.gameObject.SetActive(false);
            

        }
        else
        {
            if (StaticVariables.gamemode == 1) //1=PC
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            InventoryManager.Instance.LookTowardsCamera();
            InventoryManager.Instance.invCloseBtn.gameObject.SetActive(false);
            //uiCamera.gameObject.SetActive(false);
            inventoryCanvas.gameObject.SetActive(false);
            //inventoryCamera.Priority = 0;
            Vector3 startPosition = new Vector3(0.65f, 0, 0); Vector3 targetPosition = new Vector3(1.2f, -0.4f, 3);
            GameObject ActiveCamera = playerControlManager.ActiveCamera;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_XAxis.Value = 1;
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(targetPosition, startPosition, timer);
            ActiveCamera.GetComponent<CinemachineFreeLook>().LookAt = mobileLookAtPoint;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Height = 1;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Height = 0;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Height = -1.6f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = 1.75f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = 3.5f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = 1.3f;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = mobileCamXAxis;
            ActiveCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = mobileCamYAxis;
            this.GetComponent<ThirdPersonController>().LockCameraPosition = false;
            InventoryManager.Instance.inventryStatic.SetActive(true);
            this.gameObject.GetComponent<ThirdPersonController>().canMove = true;
            InteractBtn.gameObject.SetActive(true);
            jumpBtn.gameObject.SetActive(true);
            sprintBtn.gameObject.SetActive(true);           
            variableJoystickUI.gameObject.SetActive(true);           
        }
    }

    public void WheatThreshing()
    {
        animator.SetTrigger("Wheat");
    }

    public void CloseInventory()
    {
        InventoryManager.Instance.InventoryToggle();
    }

    private void CheckForFPP()
    {
        if (isOnFPP)
        {
            Vector3 startPosition = new Vector3(0.65f, -0.4f, 0); Vector3 targetPosition = AimZoom;
            GameObject ActiveCamera = playerControlManager.ActiveCamera;
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, timer);
        }
        else
        {
            Vector3 startPosition = new Vector3(0.65f, -0.4f, 0); Vector3 targetPosition = new Vector3(0, 0, 0);
            GameObject ActiveCamera = playerControlManager.ActiveCamera;
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, timer);
        }
    }

    void DetectObjects()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Radius");
        mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = MyCamera.ScreenPointToRay(screenCenterPoint);
        //layermask => place hit objects in this layer  
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, ~layerMask))
        {
            mouseWorldPosition = raycastHit.point;
            if (DebugTransform != null && isOnFPP)
            {
                if (!DebugTransform.gameObject.activeInHierarchy) DebugTransform.gameObject.SetActive(true);
                DebugTransform.position = raycastHit.point;
            }
            mouseWorldPosition = raycastHit.point;

            if (raycastHit.collider.gameObject.tag == "Interactable")
            {
                CrosshairHitObj = raycastHit.collider.gameObject;
            }
            else
            {
                CrosshairHitObj = null;
            }

            //Draw Ray 
            Vector3 drawdirection = mouseWorldPosition - PlayerAimPoint.transform.position;  // Point A - Point B gives direction with distance   , Use (a-b).normalized to get only direction
            Debug.DrawRay(PlayerAimPoint.transform.position, drawdirection, Color.yellow);
        }
    }

    void ThrowObject()
    {
        playerPickup.ThrowEvent();
    }

    void DropObject()
    {
        playerPickup.DropObject();
    }

    void ShootTarget()
    {
        if (((Input.GetMouseButtonDown(0) && GetGamemode() == 1) || ShootState))
        {
            Shoot = true;
        }
        ShootTargetObject();
    }

    void WeaponOnHand()
    {
        if (hasWeapomOnHand)
        {
            if (Input.GetMouseButtonDown(1) && GetGamemode() == 1)
            {
                aim = true;
            }
            if (Input.GetMouseButtonUp(1) && GetGamemode() == 1)
            {
                aim = false;
            }
            // left click to shoot
            if (((Input.GetMouseButtonDown(0) && GetGamemode() == 1) || ShootState) && aim)
            {
                Shoot = true;
            }
            ShootTargetObject();
            IsAiming();
        }
    }

    public void ShootingModeUIOn()
    {
        AimBtn.gameObject.SetActive(true);
        ShootBtn.gameObject.SetActive(true);
    }

    public void ShootingModeUIOff()
    {
        AimBtn.gameObject.SetActive(false);
        ShootBtn.gameObject.SetActive(false);
    }


    public static int GetGamemode()
    {
        int Gamemode = StaticVariables.gamemode;
        return Gamemode;
    }


    void MobileRun()
    {
        if (!sprintBtnState)  //not running so run
        {
            
            sprintBtn.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 247, 153, 255); //color to yellow
            animator.SetBool("run", true);
            playerSpeed = runSpeed;
            sprintBtnState = true;
            StarterAssetsInputs input = GetComponent<StarterAssetsInputs>();
            input.sprint = sprintBtnState;
        }
        else
        {
            sprintBtn.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255); //color to white
            animator.SetBool("run", false);
            playerSpeed = 2f;
            sprintBtnState = false;
            StarterAssetsInputs input = GetComponent<StarterAssetsInputs>();
            input.sprint = sprintBtnState;
        }

    }

    void ShootTargetObject()
    {
        if (Shoot == true)
        {
            if (targetDetector.canShootObject)
            {
                if (targetDetector.target != null)
                {
                    targetDetector.target.Output();
                }
            }
            Vector3 aimdirection = (mouseWorldPosition - PlayerAimPoint.transform.position).normalized;
            Instantiate(pfBulletprojectile, PlayerAimPoint.transform.position, Quaternion.LookRotation(aimdirection, Vector3.up));
            Shoot = false;
            ShootState = false;
        }
    }
    public void MobileJump()
    {
        if (!jumpBtnState)
        {
            jumpBtnState = true;
            StarterAssetsInputs input = GetComponent<StarterAssetsInputs>();
            input.JumpInput(true);


            StartCoroutine(ResetJumpInput());
        }
    }

    private IEnumerator ResetJumpInput()
    {
        yield return null;
        StarterAssetsInputs input = GetComponent<StarterAssetsInputs>();
        input.JumpInput(false);
        jumpBtnState = false;
    }


    public void MobileInteract()
    {
        InteractIspressed = true;
    }

    void MobileRotate()
    {
        isRotatePressed = true;
    }

    IEnumerator InteractionBtn()
    {
        InteractIspressed = true;
        yield return null;

    }
    void InteractFalse()
    {
        InteractIspressed = false;
    }

    public void PickUpAnim()
    {
        InventoryManager.Instance.LookTowardsCamera();
        animator.SetTrigger("PickUp");
    }

    public void ThrowAnim()
    {
        animator.SetTrigger("Throw");
    }

    public GameObject ActiveCamera;
    private bool hasClicked = false;
    private bool corouteneIsRunning;

    public Vector3 MouseWorldPosition()
    {
        // Get the center of the screen
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        // Set the z-coordinate to the distance from the camera to the objects
        screenCenter.z = Camera.main.nearClipPlane;
        // Convert the screen center to world space
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(screenCenter);
        return mouseWorldPosition;
    }

    void IsAiming()
    {
        ActiveCamera = playerControlManager.ActiveCamera;

        if (aim)
        {
            // Rotate player 
            // Ensure that the mouseWorldPosition has the same y-coordinate as the player
            Vector3 worldAimTarget = new Vector3(MouseWorldPosition().x, transform.position.y, MouseWorldPosition().z);
            // Calculate the direction to the target
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            // Invert the direction to look 180 degrees opposite
            aimDirection *= -1;
            // Check if the direction is not zero before applying rotation
            if (aimDirection != Vector3.zero)
            {
                // Smoothly rotate towards the target direction
                Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 20f);
            }

            // Aim camera + Hand anim layerIndex
            if (corouteneIsRunning)
            {
                StopAllCoroutines();
                StartCoroutine(HandAimAnim("in"));
            }
        }
        else
        {
            if (!corouteneIsRunning)
            {
                StopAllCoroutines();
                StartCoroutine(HandAimAnim("out"));
            }
            //animator.Play("Empty", 1); //layerIndex =1   
        }

    }


    IEnumerator HandAimAnim(string zoom)
    {
        corouteneIsRunning = !corouteneIsRunning;
        float duration = 0.14f;
        float elapsed = 0f;
        float layerIndex;

        Vector3 startPosition, targetPosition;

        if (zoom == "in")
        {
            startPosition = new Vector3(0.5f, -0.4f, 0); targetPosition = AimZoom;
            layerIndex = 1f;
            animator.Play("PistolAim", 1, 0.0f);
        }
        else
        {
            targetPosition = new Vector3(0.5f, -0.4f, 0); startPosition = AimZoom;
            layerIndex = 0f;
        }



        // Hand+Camera anim ==========================================================================================
        while (elapsed < duration)
        {
            if (layerIndex == 1f)
            {
                animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), layerIndex, elapsed / duration));
            }
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (layerIndex == 0f)
        {
            StartCoroutine(AnotherCoroutine());
        }

    }

    IEnumerator AnotherCoroutine()
    {
        animator.Play("PistolKeepBack", 1);
        yield return new WaitForSeconds(0.5f);

        float duration2 = 0.14f;
        float elapsed2 = 0f;
        // Hand+Camera anim  
        while (elapsed2 < duration2)
        {
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, elapsed2 / duration2));
            elapsed2 += Time.deltaTime;
            yield return null;
        }
    }

    void MobileAim()
    {
        if (!AimBtnState)  //not running so run
        {
            AimBtn.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 247, 153, 255); //color to yellow
            AimBtnState = true;
            aim = true;
        }
        else
        {
            AimBtn.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255); //color to white
            AimBtnState = false;
            aim = false;
        }
    }
    void MobileShoot()
    {
        ShootState = true;
    }

    public void FPPSwitch(Transform position, GameObject objec, bool isSwitching)
    {
        if (isSwitching)
        {
            isOnFPP = true;
            transform.position = position.position;
            transform.rotation = Quaternion.identity;
            this.gameObject.GetComponent<ThirdPersonController>().canMove = false;
            this.gameObject.GetComponent<ThirdPersonController>().objectToRotate = objec;
            InventoryManager.Instance.inventryStatic.SetActive(false);
            DebugTransform.gameObject.SetActive(true);
        }
        else
        {
            isOnFPP = false;
            this.gameObject.GetComponent<ThirdPersonController>().canMove = true;
            this.gameObject.GetComponent<ThirdPersonController>().objectToRotate = null;
            InventoryManager.Instance.inventryStatic.SetActive(true);
            DebugTransform.gameObject.SetActive(false);
        }
    }

    public void UpdateCraftingUI(int index, Sprite craftedItemSprite)
    {
        craftSlots[index].gameObject.SetActive(false);
        if ((index + 1) < craftSlots.Count) craftSlots[index + 1].gameObject.SetActive(true);
        craftItems[index].gameObject.SetActive(true);
        Image img = craftItems[index].GetComponent<Image>();
        img.sprite = craftedItemSprite;
    }

    public void ChangePosition(Transform position)
    {
        transform.position = position.position;
        Debug.Log(transform.position + " - " + position.position);
        transform.rotation = Quaternion.identity;
        this.gameObject.GetComponent<ThirdPersonController>().canMove = false;
        StartCoroutine(ResetCoontroller());
    }

    IEnumerator ResetCoontroller()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<ThirdPersonController>().canMove = true;
    }
}
