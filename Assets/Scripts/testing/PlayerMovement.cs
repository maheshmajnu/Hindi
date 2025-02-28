using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;

    public bool chk_grounded;

    private Vector3 moveDirection;

    private CharacterController controller;



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
         

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(horizontal, 0.0f, vertical);
        //moveDirection = new Vector3(0,0,moveZ);

        chk_grounded = controller.isGrounded;
        if (chk_grounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                //Walk
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                //Run
                 
                Run(); 
            } 
            else if (moveDirection == Vector3.zero)
            {
                //Idle
                Idle();
            }



            
        }

        if (Input.GetButtonDown("Jump"))
        {
            //jump
            Debug.Log("jump pressed");
            chk_grounded = false;
            //Jump();

        }


        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y = -gravity * Time.deltaTime;

        // Move the controller
        //controller.Move(moveDirection * Time.deltaTime);
        // Move forward / backward
         Vector3 forward = transform.TransformDirection(Vector3.forward);
         float curSpeed = moveSpeed * Input.GetAxis("Vertical");
         controller.SimpleMove(forward * curSpeed);






        //moveDirection *= moveSpeed;
        //controller.SimpleMove(moveDirection * Time.deltaTime);



        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);


    }

    private void Idle()
    {

    }
    private void Walk()
    {
        moveSpeed = walkSpeed;

    }
    private void Run()
    {
        moveSpeed = runSpeed;
    }
    private void Jump()
    {
        moveDirection.y = jumpSpeed;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
