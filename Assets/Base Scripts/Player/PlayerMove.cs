using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public PlayerPickup playerPickup;
    public TragetDetector shapeFillChecker;
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
    public Button sprintBtn;
    private bool sprintBtnState = false; 
    public Button jumpBtn;
    private bool jumpBtnState = false;
    public Button InteractBtn;
    public Sprite interactSprite;
    public Sprite throwSprite;
    public bool InteractIspressed = false;

    [Header("Crosshair")]
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField]
    private Transform DebugTransform; 
    public GameObject CrosshairHitObj;
    [SerializeField]
    private GameObject PlayerAimPoint;

    public Transform objectHolder;
    public bool hasObjectOnHand = false;
    public bool hasWeapomOnHand = false;

    public static int gamemode = 1;   //1=pc,2=mobile
    private bool Shoot = false;
    private bool aim = false;
    private Vector3 mouseWorldPosition;
    public PlayerControlManager playerControlManager;
    public Vector3 AimZoom;
    public float timer = 0;
    public Button AimBtn;
    private bool AimBtnState = false;
    public Button ShootBtn;
    private bool ShootState = false;
    public GameObject pfBulletprojectile;

    private void Start()
    {

        //Set Cursor to not be visible
        //Cursor.lockState = CursorLockMode.Locked;
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

        InteractBtn.onClick.AddListener(MobileInteract);
        interactSprite = InteractBtn.GetComponent<Image>().sprite;

        AimBtn.onClick.AddListener(MobileAim);

        ShootBtn.onClick.AddListener(MobileShoot);

    }

    void Update()
    {
         Debug.Log("Int - " + InteractIspressed);
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            // cooldown interval to allow reliable jumping even whem coming down ramps
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        if (groundedTimer > 0)
        {
            DebugGroundCheck = true; // player is on ground
        }
        else
        {
            DebugGroundCheck = false; // player is in air
        }

        // slam into the ground
        if (groundedPlayer && playerVelocity.y < 0)
        {
            // hit ground
            playerVelocity.y = 0f;
        }

        // apply gravity always, to let us track down ramps properly
        playerVelocity.y -= gravityValue * Time.deltaTime;


        // movement WASD or left_joystick
        float h = Input.GetAxis("Horizontal") + variableJoystick.Horizontal;
        float z = Input.GetAxis("Vertical") + variableJoystick.Vertical; 
        Vector3 move = new Vector3(h, 0f, z).normalized;
         
         

        // only align to motion if we are providing enough input
        if (move.magnitude >= 0.1f)
        {
            //--------------------Player Direction------------------------//
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothness, turnSmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //--------------------//------------------------// 
            controller.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        } 

        // allow jump as long as the player is on the ground
        if (Input.GetButtonDown("Jump") || jumpBtnState)
        {
            //mobile jumpUI
            jumpBtnState = false; 

            // must have been grounded recently to allow jump
            if (groundedTimer > 0)
            {
                // no more until we recontact ground
                groundedTimer = 0;

                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                playerVelocity.y += Mathf.Sqrt(jumpHeight * 2 * gravityValue);
            }
        }
         

        // call .Move() once only 
        controller.Move(playerVelocity * Time.deltaTime);

        if ((Input.GetKeyDown(KeyCode.E) || InteractIspressed) && hasObjectOnHand && !hasWeapomOnHand)
        {
            InteractIspressed = false;
            animator.SetTrigger("Throw");
        }

        //checks for weapon and do weapon functions
        if (hasWeapomOnHand) WeaponOnHand();
        if (shapeFillChecker.canShootObject) ShootHole();

        // ==================== WALK Animation ====================== //
        animator.SetFloat("speed", move.magnitude);
        // ====================// ====================//
        // ==================== FALL Animation ====================== //
        if (DebugGroundCheck)
        {
            animator.SetBool("fall", false);
        }
        else
        {
            animator.SetBool("fall", true); 
        }
        // ====================// ====================//
        // ==================== RUN Animation ====================// 
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("run", true);
            playerSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("run", false);
            playerSpeed = 2.0f;
        }

        


        // ====================// ====================// 

        //=============== crosshair hit ===============//
        mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = MyCamera.ScreenPointToRay(screenCenterPoint);

        /*   Vector3 direction = MyCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height, 999f)) - transform.position; // note that you compute a ray by defining an origin and a direction, not a target point!
           Ray ray = new Ray(transform.position, direction);*/

        int layerMask = 1 << LayerMask.NameToLayer("Radius");
        //layermask => place hit objects in this layer  
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, ~layerMask))
        {
            mouseWorldPosition = raycastHit.point;
            DebugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            
            if(raycastHit.collider.gameObject.tag == "Interactable")
            {
                CrosshairHitObj = raycastHit.collider.gameObject;
            } else
            {
                CrosshairHitObj = null;
            }

            //Draw Ray 
            Vector3 drawdirection =  mouseWorldPosition - PlayerAimPoint.transform.position ;  // Point A - Point B gives direction with distance   , Use (a-b).normalized to get only direction
             Debug.DrawRay(PlayerAimPoint.transform.position, drawdirection , Color.yellow);
        }
         
    }

    void ThrowObject()
    {
        playerPickup.ThrowEvent();
    }

    void ShootHole()
    {
        if (((Input.GetMouseButtonDown(0) && GetGamemode() == 1) || ShootState))
        {
            Shoot = true;
        }
        ShootMedicine();
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
            ShootMedicine();
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
        }
        else
        {
            sprintBtn.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255); //color to white
            animator.SetBool("run", false);
            playerSpeed = 2f;
            sprintBtnState = false;
        }

    }

    void ShootMedicine()
    {
        if (Shoot == true)
        {
            if(shapeFillChecker.canShootObject)
            {
                if(shapeFillChecker.target != null)
                {
                    shapeFillChecker.target.Output();
                }
            }
            Vector3 aimdirection = (mouseWorldPosition - PlayerAimPoint.transform.position).normalized;
            Instantiate(pfBulletprojectile, PlayerAimPoint.transform.position, Quaternion.LookRotation(aimdirection, Vector3.up));
            Shoot = false;
            ShootState = false;
        }
    }
    void MobileJump()
    {
        if (!jumpBtnState)  //not jumping so jump
        { 
            jumpBtnState = true;
        } 
    }

 
    void MobileInteract()
    {
        InteractIspressed = true;
        //StartCoroutine(InteractionBtn());
        //if(InteractIspressed){
        //    CancelInvoke("InteractFalse");
        //    }  
        //InteractIspressed = true;
        //Invoke("InteractFalse", 0.01f);
    }

    IEnumerator InteractionBtn()
    {
        InteractIspressed = true;
        //InteractBtn.interactable = false;
        yield return null;
        //InteractIspressed = false;
        //yield return new WaitForSeconds(1);
        //InteractBtn.interactable = true;
    }
    void InteractFalse()
    {
        InteractIspressed = false;
    }

    public void PickUpAnim()
    {
        animator.SetTrigger("PickUp");
    }

    public void ThrowAnim()
    {
        animator.SetTrigger("Throw");
    }

    void IsAiming()
    {
        if (aim)
        {
            //rotate player
            Vector3 WorldAimTarget = mouseWorldPosition;
            WorldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (WorldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            //hand anim
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            //aim camera 
            Vector3 startPosition = new Vector3(0.5f, -0.4f, 0); Vector3 targetPosition = AimZoom;
            GameObject ActiveCamera = playerControlManager.ActiveCamera;
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, timer);
            if (timer < 1f)
            {
                timer += Time.deltaTime * 5f;
            }

        }
        else
        {
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            //aim camera 
            Vector3 startPosition = new Vector3(0.5f, -0.4f, 0); Vector3 targetPosition = AimZoom;
            GameObject ActiveCamera = playerControlManager.ActiveCamera;
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, timer);
            if (timer > 0f)
            {
                timer -= Time.deltaTime * 5f;
            }
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


}
