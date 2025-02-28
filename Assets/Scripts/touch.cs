using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
  
   float touchSpeed = 0.005f; 
   public float RotationSpeed = 1;

    GameObject someGameObject; 
    Quaternion  z_scale; 
   int check; 

  // Start is called before the first frame update
    void Start()
    { 
     someGameObject = GameObject.Find ("Cylinder");
    }

    // Update is called once per frame
    void Update()
    {
  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
         
         //on mouse click 
            if (Input.GetMouseButtonDown(0))
            {

                //check if mouse click is on BOB
                if(Physics.Raycast (ray, out hit))
                { 
                    if (hit.collider != null && hit.collider.CompareTag ("Player"))
                {

                    someGameObject.GetComponent<Rigidbody>().isKinematic = true;
                    someGameObject.GetComponent<Rigidbody>().useGravity = false;
                    someGameObject.GetComponent<Rigidbody>().isKinematic = false;

                    // Debug.Log(hit.collider.gameObject.name); 
                    check = 1; 
                }

                }
            }

            //Mouse click release
            if (Input.GetMouseButtonUp(0))
            {
                // Debug.Log("Pressed left click.");
                    check = 0; 
                    someGameObject.GetComponent<Rigidbody>().useGravity = true;
            }



            //Mouse moving left
                 if(Input.GetAxis("Mouse X")<0  && check == 1) {
                    //Code for action on mouse moving left
                    //print("Mouse moved left");

                Debug.Log( Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime );
                touchSpeed = Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;

                checkHit();
                z_scale =  someGameObject.transform.rotation; 
                z_scale.z = z_scale.z + touchSpeed;

                if(z_scale.z < -0.50f) {
                    z_scale.z = -0.50f;
                }

                someGameObject.transform.rotation = z_scale;    
                }

            //Mouse moving right
                if(Input.GetAxis("Mouse X")>0 && check == 1){
                    //Code for action on mouse moving right
                    // print("Mouse moved right");

                Debug.Log( Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime );
                touchSpeed = Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
 
                checkHit();
                z_scale =  someGameObject.transform.rotation; 
                z_scale.z = z_scale.z + touchSpeed;

                if(z_scale.z > 0.50f) {
                    z_scale.z = 0.50f;
                }

                someGameObject.transform.rotation = z_scale;    
                }

// check if user holding BOB continously
void checkHit() {

     if(Physics.Raycast (ray, out hit)) { 

            if (hit.collider != null && hit.collider.CompareTag ("Player")) { 
                        check = 1; 
            } else {
                        check = 0;  
                    someGameObject.GetComponent<Rigidbody>().useGravity = true;
            }

        }
}









    } 
}
