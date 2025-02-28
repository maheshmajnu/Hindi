using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public Transform rabbit;
    public float rabbitDistanceThreshold = 0.1f; // Adjust as per your needs
    private bool missionFailed = false;
    public Animator anim;
    public sfx_Our_environment sfx_Our_Environment;
    public float ChaseRange = 5f;
    public bool isChasingPlayer = false;
    public bool isChasingRabbit = false;

    void Start()
    {
       
    }

    void Update()
    {
        // Automatically find the player by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Transform[] allChildren = playerObject.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.name == "TPP_Player")
                {
                    player = child; // Assign the TPP_Player transform
                    break;
                }
                
            }
            
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found in the scene.");
        }
        if (missionFailed || player == null) return;

        // Calculate distances
        float playerDistance = Vector3.Distance(transform.position, player.position);
        float rabbitDistance = Vector3.Distance(transform.position, rabbit.position);

        if (rabbitDistance <= ChaseRange)
        {
            isChasingRabbit = true;
            isChasingPlayer = false;
        }
        else if (playerDistance <= ChaseRange)
        {
            isChasingPlayer = true;
            isChasingRabbit = false;
        }


        // Determine the target
        if (isChasingRabbit)
        {
            enemy.SetDestination(rabbit.position);
            anim.SetBool("Bool", true);
        }
        else if (isChasingPlayer)
        {
            enemy.SetDestination(player.position);
            anim.SetBool("Bool", true);
        }
    }

        private void OnTriggerEnter(Collider other)
        {
            // Mission failed if collides with player
            if (other.CompareTag("Player"))
            {
                sfx_Our_Environment.MissionFailed();
                missionFailed = true;
                enemy.isStopped = true;
                anim.SetBool("Bool", false);
                // Additional game over logic can go here
            }
            // Stop enemy if collides with rabbit
            else if (other.gameObject == rabbit.gameObject)
            {
                Debug.Log("Enemy reached the rabbit.");
                enemy.isStopped = true;
                anim.SetBool("Bool", false);
            }
        }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set Gizmo color
        Gizmos.DrawWireSphere(transform.position, ChaseRange); // Draw a wire sphere
    }
}   
