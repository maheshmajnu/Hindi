using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrainRespawn : MonoBehaviour
{
    [SerializeField]
    private Transform TrainStartPoint;

    private bool AtEnd = false;
    private bool AtStart = false;  
 

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
        {
            Debug.Log("respawn");
            AtEnd = true;
            StartCoroutine(ResetTrain()); 
        }
    }


    IEnumerator ResetTrain() {

        while (AtEnd)
        { 
            gameObject.GetComponent<NavAgentMove>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.transform.position = TrainStartPoint.position;

            AtEnd = false; AtStart = true;
             yield return null;

        }

        while (AtStart)
        {
            gameObject.GetComponent<NavAgentMove>().enabled = true;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            AtStart = false;
            yield return null;
        }

        yield return new WaitForSeconds(1);
    }



}
