using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tpp_Movement : MonoBehaviour
{
    public CharacterController controller;
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

 

 


    private void Start()
    {

        //Set Cursor to not be visible
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


 
    }

    void Update()
    {

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
        float h = Input.GetAxis("Horizontal") ;
        float z = Input.GetAxis("Vertical") ;
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
        if (Input.GetButtonDown("Jump") )
        {

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

      

    }


}
