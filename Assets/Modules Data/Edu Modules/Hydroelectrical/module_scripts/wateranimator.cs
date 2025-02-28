using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wateranimator : MonoBehaviour
{

    public float speedX = 0.1f;
    public float speedY = 0.1f;
    private float curX;
    private float curY;

    // Use this for initialization
    void Start()
    {
        curX = GetComponent<Renderer>().material.mainTextureOffset.x;
        curY = GetComponent<Renderer>().material.mainTextureOffset.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curX += Time.deltaTime * speedX;
        curY += Time.deltaTime * speedY;
        float offset = Time.deltaTime*0.5f;
        GetComponent<Renderer>().material.SetTextureOffset("_BaseMap", new Vector2(curX, curY));
    }
}
