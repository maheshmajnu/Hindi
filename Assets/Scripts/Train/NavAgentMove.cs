using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMove : MonoBehaviour
{
    [SerializeField]
    private Transform movepostransform;
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private GameObject TrainCam;

    [SerializeField]
    private GameObject TrainExitTrigger;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = movepostransform.position;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TrainStop")
        {
            Debug.Log("stop arrived");
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.speed = 0f;
            TrainExitTrigger.SetActive(true);
            TrainCam.SetActive(false);
            //Get TPP_Player and enable movement   
            GameObject.Find("TPP_Player").GetComponent<PlayerMove>().enabled = true;
        }
    }

}
