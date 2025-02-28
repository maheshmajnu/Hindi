using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class VehicleCam : MonoBehaviour
{
 
    public Transform body; // set here the player transform 
    float xRotation = 0f;
    public Vector2 turn;
    public float sensitivity = 300;
    public Vector3 deltaMove;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    { 

    }
   
    void Update()
    { 
        LookCam();
    }

    void LookCam()
    { 
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            turn.y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;



            xRotation -= turn.y;
            xRotation = Mathf.Clamp(xRotation, -30f, 30f);


            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            body.Rotate(Vector3.up * mouseX); 
    }
     
}
