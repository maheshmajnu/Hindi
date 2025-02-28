using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class SPACE_PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    [Header("Ground Props")]
    public bool DebugGroundCheck;
    private float groundedTimer;        // to allow jumping when going down ramps => greater than 0 means grounded
    private float jumpHeight = 1.0f;
    private float gravityValue = 20f;
    private bool groundedPlayer; 
    


    [Header("Player Props")]
    public float runSpeed = 5.0f; 
    private float playerSpeed = 2.0f;
    private Vector3 playerVelocity;

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

    [Header("Crosshair")]
    [SerializeField]
    private GameObject pfBulletprojectile; 
    private Vector3 mouseWorldPosition; 
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField]
    private Transform DebugTransform; 
    public GameObject CrosshairHitObj;
    [SerializeField]
    private GameObject PlayerAimPoint;
    private bool Shoot = false;
    private bool aim = false;

    [SerializeField]
    private GameObject PlayerControlManager;
    public float timer = 0;
    [SerializeField]
    private Vector3 AimZoom = new Vector3( 1.1f, -0.4f, 2.5f);

    [SerializeField]
    private float sensitivity = 250f;
    [SerializeField]
    private GameObject SpeedLines;

    private void Start()
    {

        //Set Cursor to not be visible
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        //mobile UI sprint button
        Button btns = sprintBtn.GetComponent<Button>();
        btns.onClick.AddListener(MobileRun);
        //mobile UI Jump button
        /*Button btnj = jumpBtn.GetComponent<Button>();
        btnj.onClick.AddListener(MobileJump);*/
    }

    public void OrbitEnterSpeed(){
        playerSpeed = runSpeed;
    }

    void Update()
    {
     /*
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

     */

        // movement WASD or left_joystick
        float h = Input.GetAxis("Horizontal") + variableJoystick.Horizontal;
        float z = Input.GetAxis("Vertical") + variableJoystick.Vertical; 
        Vector3 move = new Vector3(h, 0f, z).normalized;

        Vector3 moveDir = transform.right * h *0f + transform.forward * z; 
        controller.Move(moveDir * playerSpeed * Time.deltaTime);

        float Xaxis = h * sensitivity * Time.deltaTime; 
        transform.Rotate(Vector3.up * Xaxis);

        /*
           // only align to motion if we are providing enough input
           if (move.magnitude >= 0.1f)
           {
               //--------------------Player Direction------------------------//
               float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;// + Cam.eulerAngles.y;
               float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothness, turnSmoothing);

               if (!aim)
               {
                   transform.rotation = Quaternion.Euler(0f, angle, 0f);
               }


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

        */



        // ==================== RUN Animation ====================// 
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //animator.SetBool("run", true);
            playerSpeed = runSpeed;
            SpeedLines.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
           // animator.SetBool("run", false);
            playerSpeed = 2.0f;
            SpeedLines.SetActive(false);
        }
        // ====================// ====================// 

/*

        //=============== crosshair hit ===============//
        mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = MyCamera.ScreenPointToRay(screenCenterPoint);

       //Vector3 direction = MyCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height, 999f)) - transform.position; // note that you compute a ray by defining an origin and a direction, not a target point!
       // Ray ray = new Ray(transform.position, direction);
         
        //layermask => place hit objects in this layer  
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
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
            Vector3 Drawdirection =  mouseWorldPosition - PlayerAimPoint.transform.position ;  // Point A - Point B gives direction with distance   , Use (a-b).normalized to get only direction
             Debug.DrawRay(PlayerAimPoint.transform.position, Drawdirection , Color.yellow);
        }


        // right click to aim
        if (Input.GetMouseButtonDown(1))
        {
            aim = true; 
        }
        if (Input.GetMouseButtonUp(1))
        {
            aim = false;
        }

        IsAiming();

        // left click to shoot
        if (Input.GetMouseButtonDown(0) && aim)
        {
            Shoot =true;
        }
        ShootMedicine();
         
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
            GameObject ActiveCamera = PlayerControlManager.GetComponent<PlayerControlManager>().ActiveCamera; 
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset =  Vector3.Lerp(startPosition, targetPosition, timer); 
            if (timer < 1f)
            {
                timer += Time.deltaTime*5f;
            }  

        }
        else
        {
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            //aim camera 
            Vector3 startPosition = new Vector3(0.5f, -0.4f, 0); Vector3 targetPosition = AimZoom;
            GameObject ActiveCamera = PlayerControlManager.GetComponent<PlayerControlManager>().ActiveCamera; 
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, timer);
            if (timer > 0f)
            {
                timer -= Time.deltaTime * 5f;
            }
        }
*/
    }

 

    void MobileRun()
    {
        if (!sprintBtnState)  //not running so run
        {
            sprintBtn.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 247, 153, 255); //color to yellow
            animator.SetBool("run", true);
            playerSpeed = runSpeed;
            sprintBtnState = true;
            SpeedLines.SetActive(true);
        }
        else
        {
            sprintBtn.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255); //color to white
            animator.SetBool("run", false);
            playerSpeed = 2f;
            sprintBtnState = false;
            SpeedLines.SetActive(false);
        }

    }

 /*
    void MobileJump()
    {
        if (!jumpBtnState)  //not jumping so jump
        { 
            jumpBtnState = true;
        } 
    }

    void ShootMedicine(){
        if(Shoot == true) {
            Vector3 aimdirection =  (mouseWorldPosition - PlayerAimPoint.transform.position).normalized;
            Instantiate(pfBulletprojectile,PlayerAimPoint.transform.position,Quaternion.LookRotation(aimdirection,Vector3.up));
            Shoot = false;
        } 
    }
 */


}
