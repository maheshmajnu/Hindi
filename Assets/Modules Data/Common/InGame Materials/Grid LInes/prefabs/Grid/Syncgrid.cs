using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syncgrid : MonoBehaviour
{
    private GameObject gridline;
    private float tim = 5f;
    private float step = 1f;
    private float TimeDuration;
    // Start is called before the first frame update
    void Start()
    {
        gridline = this.gameObject;
        TimeDuration = tim;
    }

    // Update is called once per frame
    void Update()
    {


        if(tim >= 0f)
        {
            tim -= 1f * Time.deltaTime;
            step = tim / TimeDuration; 
            gridline.transform.localScale = new Vector3(1f, step, 1f);
        }

    }
}
