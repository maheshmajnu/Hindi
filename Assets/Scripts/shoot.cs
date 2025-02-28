using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    float rayDistance = 1f;
    public LayerMask layerMask;
    public RectTransform crosshairPosition;
    public Camera Cam;

    public Transform muzzle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = Cam.ScreenPointToRay(new Vector2(crosshairPosition.x, crosshairPosition.y));
        RaycastHit hit;
        /*
        if (Physics.Raycast(ray, out hit, rayDistance, layerMask)) //layermask determindes what can you click on
        {
            // print(hit.transform.name);
            string hitObjectName = hit.transform.name;
            Debug.Log(hitObjectName);
        }*/
        if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit))
        {
            if (hit.collider)
            {
                crosshairPosition.position = Cam.WorldToScreenPoint(hit.point);
            }
        }
    }
}
