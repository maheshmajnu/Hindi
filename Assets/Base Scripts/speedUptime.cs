using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedUptime : MonoBehaviour
{
    public float timeSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeSpeed;
    }
}
