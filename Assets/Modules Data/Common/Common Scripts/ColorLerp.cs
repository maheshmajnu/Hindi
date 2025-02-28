using UnityEngine;
using System.Collections;

public class ColorLerp : MonoBehaviour
{
    public Color ChangeToColor;
    public float speed = 2f;
     
    private MeshRenderer ObjMeshRenderer; 

    private void Start()
    {
        ObjMeshRenderer = GetComponent<MeshRenderer>(); 
    }

    void Update()
    {
        ObjMeshRenderer.material.color = Color.Lerp(ObjMeshRenderer.material.color, ChangeToColor, Time.deltaTime * 0.1f *speed);    // duration =  Mathf.PingPong(Time.time, 1)
     
    }
}