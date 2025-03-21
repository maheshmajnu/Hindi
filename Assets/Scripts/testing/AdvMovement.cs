using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [Header("Movement")]
    public float speed = 2f;
    public float gravity = -9.18f;
    public float jumpHeight = 1.5f;

    public float rotateSpeed = 5f;

    [Header("Camera & Character Syncing")]
    public float lookDIstance = 5;
    public float lookSpeed = 5;
    public Transform camCenter;


    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    private Quaternion qTo;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        qTo = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);  // For Check Player is Grounded Or Not
         
        movement();

        Run();

        Jump();
         

        //覧蘭 Player Rotation with Camera 覧覧覧覧覧覧-
        RotateToCamView();
        //覧覧覧覧覧覧覧覧覧覧覧覧覧�
    }

    private void movement() {

        float x = Input.GetAxis("Horizontal");        //To Get Input from A and D Keys
        float z = Input.GetAxis("Vertical");         // To Get Input from W and S Keys
        // 覧�- Player Movement Start 覧覧覧覧覧�
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        // 覧覧�- Player Movement End覧覧覧覧蘭

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

    }

    

    private void Run()
    {
        // 覧�- Code for Running 覧�-
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
        {
            speed = 5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 2f;
        }
        // 覧覧� Running End 覧覧覧�
    }

    private void Jump()
    {

        // 覧�-----------Jump-------- 覧覧覧�
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        // 覧覧覧覧覧� Jump Ends 覧覧覧覧覧�
    }





    //覧覧覧�- Rotate with Camera Start 覧覧覧覧覧覧

    void RotateToCamView()
    {
        Vector3 camCenterPos = camCenter.position;
        Vector3 lookPoint = camCenterPos + (camCenter.forward * lookDIstance);
        Vector3 direction = lookPoint - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;
        Quaternion finalRotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
        transform.rotation = finalRotation;

        /*
        float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothness, turnSmoothing);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);*/

    }
    //覧覧覧蘭 Rotate With Camera End 覧覧覧覧覧覧�
}
