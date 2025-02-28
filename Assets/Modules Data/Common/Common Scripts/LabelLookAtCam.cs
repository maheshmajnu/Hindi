using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelLookAtCam : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera my_camera;
    void Start()
    {
        if (my_camera == null)
        {
            my_camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + my_camera.transform.rotation*Vector3.forward, my_camera.transform.rotation*Vector3.up);
    }
}
