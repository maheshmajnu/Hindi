using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidLevelDown : MonoBehaviour
{

    public float min;
    public float max;
    public float speed;

    public bool leveldown;

    void Start()
    { 
    }

    void Update()
    {
        if (leveldown)
        {
            if (max > (min + 0.01f))
            {
                max = Mathf.Lerp(max, min, speed * Time.deltaTime);
                Debug.Log(max);
                // Animate the Shininess value 
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_fill", max);
            }

        }
        else {

            if (max < (min + 0.01f))
            {
                max = Mathf.Lerp(max, min, speed * Time.deltaTime);
                Debug.Log(max);
                // Animate the Shininess value 
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_fill", max);
            }
             
        }
        
        
    }
}
