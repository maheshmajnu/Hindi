using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainWayPointManager : MonoBehaviour
{
     

    [SerializeField]
    private Transform[] Route1;

    [SerializeField]
    private Transform TargetStop;

    public int i = 0;

     

    void Awake()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    { 

        if (other.tag == "newstationpoint")
        {
            UpdateNavTargetStation();
        }
    }



    public void UpdateNavTargetStation()
    {
        if (i == 3)
        {
            Debug.Log(i + " last station update");
            TargetStop.position = Route1[i].position;
            i = 0;
        }
        else
        {
            Debug.Log(i + " new station update");
            TargetStop.position = Route1[i].position;
            i++;

        }
    }

}
