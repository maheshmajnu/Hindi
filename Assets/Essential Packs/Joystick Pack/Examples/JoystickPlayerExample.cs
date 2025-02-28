using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{

    public CharacterController controller;
    public Transform Cam;

    public float playerSpeed;
    public VariableJoystick variableJoystick;

    // public Rigidbody rb;

    float turnSmoothness;
    public float turnSmoothing = 0.1f;

    public void FixedUpdate()
    {

        float h = variableJoystick.Horizontal;
        float z = variableJoystick.Vertical;

        //Vector3 moveDirection = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        // rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        Vector3 move = new Vector3(h, 0f, z).normalized;

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
    }
}