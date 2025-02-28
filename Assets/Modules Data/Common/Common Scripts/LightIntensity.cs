using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensity : MonoBehaviour
{
    public Light myLight;

    public float min;
    public float max;
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        if (max > (min + 0.01f))
        {
            max = Mathf.Lerp(max, min, speed * Time.deltaTime);
            myLight.intensity = max;

        }
         
    }
}
