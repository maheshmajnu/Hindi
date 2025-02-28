using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMeshText : MonoBehaviour
{
     [Tooltip("Distance of the MeshObject with no calculateScale")]
     public float distance = 7.0f;
     Vector3 startScale;
 
     void Start () {
         startScale = transform.localScale;
     }
     
     void Update () {
         scale();
     }
 
     void scale()
     {
         float dist = Vector3.Distance(Camera.main.transform.position, transform.position);
         Vector3 newScale = startScale * (dist / distance);
         transform.localScale = newScale;
     } 
}
