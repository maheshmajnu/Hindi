using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textLookat : MonoBehaviour
{
    //public GameObject playercam;
    [SerializeField]
    private Camera my_camera;


    // Start is called before the first frame update
    void Awake()
    {
        if (my_camera == null)
        {
            my_camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
       // transform.LookAt(playercam.transform);
      //  transform.rotation  = playercam.transform.rotation;

        transform.LookAt(transform.position + my_camera.transform.rotation * Vector3.forward, my_camera.transform.rotation * Vector3.up);
    }

}
