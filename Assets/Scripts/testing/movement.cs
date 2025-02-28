using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] public Transform Cam;
    [SerializeField] private Vector3 playerVelocity;
    private bool grounded;
    [SerializeField] private float PlayerSpeed = 2f;
    [SerializeField] private float JumpForce = 2f;
    [SerializeField] private float gravityValue = -10f;
    float turnSmoothness;
    [SerializeField] private float turnSmoothing = 0.1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;

        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0f, z);


        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothness, turnSmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * PlayerSpeed * Time.deltaTime);
        }


        if (Input.GetButtonDown("Jump") && grounded)
        {
            Debug.Log("jump pressed");
            playerVelocity.y += Mathf.Sqrt(JumpForce * -3f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
