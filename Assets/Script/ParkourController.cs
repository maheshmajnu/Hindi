using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    public EnvironmentChecker EnvironmentChecker;
    public bool PlayerInAction;
    public bool PlayerInAir;
    public Animator animator;
    public ThirdPersonController TPPplayerScript;

    [Header("Parkour Actions Area")]
    public List<MatchTarget> parkourActions;

    [Header("Climbable Settings")]
    // Layer mask for objects that can be climbed
    public LayerMask climbableLayer;

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButton("Jump") && !PlayerInAction)
        {
            PlayerInAir = true;
        }

        if (PlayerInAir)
        {

            if (TPPplayerScript.Grounded)
            {
                PlayerInAir = false;
            }

            var hit_data = EnvironmentChecker.CheckObstacle();

            if (hit_data.hitFound && !hit_data.RoofHitFound &&
                ((climbableLayer.value & (1 << hit_data.hitInfo.transform.gameObject.layer)) != 0))
            {
                foreach (var action in parkourActions)
                {
                    if (action.CheckIfAvailable(hit_data, transform))
                    {
                        PlayerInAir = false;
                        Debug.Log("hit found" + hit_data.hitInfo.transform.name);
                        // Enable root motion
                        animator.applyRootMotion = true;

                        StartCoroutine(PerformParkourAction(action));
                        break;
                    }
                }

            }

        }

    }

    IEnumerator PerformParkourAction(MatchTarget action)
    {
        PlayerInAction = true;
        TPPplayerScript.SetControl(false);
        float rotSpeed = 60f;

        animator.Play(action.AnimationName, 0);
        //reset usual jump params
        animator.SetBool("Jump", false); animator.SetBool("FreeFall", false); animator.SetBool("Grounded", true); animator.SetFloat("Speed", 0f);
        TPPplayerScript._verticalVelocity = -2.1f; TPPplayerScript.landedfromparkour = true;
        yield return null;

        var animationState = animator.GetCurrentAnimatorStateInfo(0);  //GetNextAnimatorStateInfo //GetCurrentAnimatorStateInfo
        if (!animationState.IsName(action.AnimationName))
        { Debug.Log("Animation Name is Incorrect"); }
        Debug.Log("Animation length: " + animationState.length);

        //yield return new WaitForSeconds(animationState.length);

        float timeCounter = 0f;
        while (timeCounter <= animationState.length)
        {
            timeCounter += Time.deltaTime;
            //Make Player Look towards Obstacle
            if (action.LookAtObstacle)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, action.RequiredRotation, rotSpeed * Time.deltaTime);
            }
            if (action.AllowTargetMatching)
            {
                CompareTarget(action);
            }
            yield return null;
        }

        TPPplayerScript.SetControl(true);
        PlayerInAction = false;

        // Disable root motion
        animator.applyRootMotion = false;
    }

    void CompareTarget(MatchTarget action)
    {
        animator.MatchTarget(action.ComparePosition, transform.rotation, action.CompareBodyPart, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), action.CompareStartTime, action.CompareEndTime);

    }
}