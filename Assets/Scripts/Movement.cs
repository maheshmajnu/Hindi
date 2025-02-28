using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Touch Controls")]
    public VariableJoystick variableJoystick;
    public Button sprintBtn;
    private bool sprintBtnState = false;


    public Animator animator;
    float smoothBlendTime = 0.1f;
    public CharacterController controller;

    public Transform Cam;
    private Vector3 playerVelocity; 
    public float playerSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpSpeed = 1.5f;
    public float gravityValue= -10f;
    float turnSmoothness;
    public float turnSmoothing = 0.1f;



    [Header("GroundCheck")]
    public Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.1f;
    public LayerMask groundMask; 
    public bool groundedPlayer;

    [Header("Raycast-GroundCheck")]
    //using raycast
    public GameObject rayCube;
    RaycastHit hit;
    public float rayDistance = 0.3f ;
    Vector3 rayDir = new Vector3(0, -1);

    //public bool debugGround;

    // Start is called before the first frame update
    void Start()
    {
        //Set Cursor to not be visible
        Cursor.visible = false;


        //mobile UI sprint button
        Button btnj = sprintBtn.GetComponent<Button>();
        btnj.onClick.AddListener(MobileRun);
         
    }

    
    // Update is called once per frame
    void Update()
    {

        //groundedPlayer = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);  // For Check Player is Grounded Or Not
        //groundedPlayer = controller.isGrounded;

         
        //---------Raycast Ground Check ---------//
        
         Debug.DrawRay(rayCube.transform.position, rayDir* rayDistance, Color.red );
         if (Physics.Raycast(rayCube.transform.position, rayDir, out hit, rayDistance))
         {
             groundedPlayer = true;
         }
         else
         {
             groundedPlayer = false;
         }
          
        //---------// ---------//


        /*
        if (controller.isGrounded)
        { 
            groundedPlayer = true;
        }
        else
        { 
            groundedPlayer = false;
        }  
        */




        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

       
         
        //movement WASD or left_joystick
        float h = Input.GetAxis("Horizontal") + variableJoystick.Horizontal;
        float z = Input.GetAxis("Vertical") + variableJoystick.Vertical;

        Vector3 move = new Vector3(h,0f,z).normalized;

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


        //animator.SetFloat("speed", move.magnitude, smoothBlendTime, Time.deltaTime);  //use this for smooth blend
        animator.SetFloat("speed", move.magnitude);

        //--------------------Jump------------------------//

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpSpeed * -3.0f * gravityValue);
        }

        //gravity
        playerVelocity.y += gravityValue * Time.deltaTime ;
        controller.Move(playerVelocity  * Time.deltaTime);

        //--------------------//------------------------//
        //--------------------Fall------------------------//
        if(groundedPlayer)
        {
            animator.SetBool("fall",false);
        }
        else 
        {
            animator.SetBool("fall",true);
            animator.SetBool("run", false);
        }

        //--------------------//------------------------//
        //--------------------RUN------------------------//
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("run",true);
            playerSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("run",false); 
            playerSpeed = 2f;
        }

    }



    void MobileRun()
    {
        if (!sprintBtnState)  //not running so run
        {
            sprintBtn.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 247, 153, 255); //color to yellow
            animator.SetBool("run", true);
            playerSpeed = runSpeed;
            sprintBtnState = true;
        } else
        {
            sprintBtn.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 255, 255, 255); //color to white
            animator.SetBool("run", false);
            playerSpeed = 2f;
            sprintBtnState = false;
        }
        
    }

}
