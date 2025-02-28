using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Npc : MonoBehaviour
{
    public Transform moveposition;
    private NavMeshAgent NavAgent;

    public Animator animator;
    private Vector3 previousPosition;
    public float curSpeed;
 


    // Start is called before the first frame update
    void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveposition != null)
        {
            NavAgent.destination = moveposition.position;

            //detect speed
            Vector3 curMove = transform.position - previousPosition;
            curSpeed = curMove.magnitude / Time.deltaTime;
            previousPosition = transform.position;
            Debug.Log(curSpeed);
            if (curSpeed > 0.1f)
            {
                animator.SetFloat("Speed", 1);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
        }        
    }

 
}
