using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class AimShoot : MonoBehaviour
{

    public ThirdPersonController TPPplayerScript;
    public Animator animator;
    public GameObject ActiveCamera;

    [Header("Crosshair")]
    [SerializeField]
    private GameObject pfBulletprojectile;
    private Vector3 mouseWorldPosition;
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField]
    private Transform DebugTransform;
    public GameObject CrosshairHitObj;
    [SerializeField]
    private GameObject PlayerAimPoint;
    private bool Shoot = false;
    private bool aim = false;

    private bool hasClicked = false;
    private bool corouteneIsRunning;

    [SerializeField] 
    public float timer = 0; 
    private Vector3 AimZoom = new Vector3(0.8f, 0.1f, 3f);   //new Vector3(1.1f, -0.4f, 2.5f);


    // Start is called before the first frame update
    void Start()
    {
        corouteneIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    { 
        
        // right click to aim
        if (Input.GetMouseButtonDown(1) && !hasClicked)
        {
            aim = true;
            TPPplayerScript.isAiming = aim;
            hasClicked = aim;
            corouteneIsRunning = aim;
        }
        if (Input.GetMouseButtonUp(1) && hasClicked)
        {
            aim = false;
            TPPplayerScript.isAiming = aim;
            hasClicked = aim;
        }

        IsAiming();

         
    }

    public Vector3 MouseWorldPosition() {
        // Get the center of the screen
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f); 
        // Set the z-coordinate to the distance from the camera to the objects
        screenCenter.z = Camera.main.nearClipPlane; 
        // Convert the screen center to world space
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(screenCenter);
        return mouseWorldPosition;
    }


    void IsAiming()
    {
        if (aim)
        { 
            // Rotate player 
            // Ensure that the mouseWorldPosition has the same y-coordinate as the player
            Vector3 worldAimTarget = new Vector3(MouseWorldPosition().x, transform.position.y, MouseWorldPosition().z); 
            // Calculate the direction to the target
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized; 
            // Invert the direction to look 180 degrees opposite
            aimDirection *= -1; 
            // Check if the direction is not zero before applying rotation
            if (aimDirection != Vector3.zero)
            {
                // Smoothly rotate towards the target direction
                Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 20f);
            }

            // Aim camera + Hand anim layerIndex
            if (corouteneIsRunning) 
            {
                StopAllCoroutines();
                StartCoroutine(HandAimAnim("in"));
            } 
        }
        else
        { 
            if (!corouteneIsRunning)
            {
                StopAllCoroutines();
                StartCoroutine(HandAimAnim("out"));
            } 
            //animator.Play("Empty", 1); //layerIndex =1   
        }

    }


    IEnumerator HandAimAnim(string zoom) 
    {
        corouteneIsRunning = !corouteneIsRunning;
        float duration = 0.14f;
        float elapsed = 0f;
        float layerIndex;

        Vector3 startPosition, targetPosition;

        if (zoom == "in")
        {
            startPosition = new Vector3(0.5f, -0.4f, 0); targetPosition = AimZoom;
            layerIndex = 1f;
            animator.Play("PistolAim", 1, 0.0f);
        }
        else 
        {
            targetPosition  = new Vector3(0.5f, -0.4f, 0); startPosition = AimZoom;
            layerIndex = 0f;
        }
          
        

        // Hand+Camera anim ==========================================================================================
        while (elapsed < duration)
        {
            if (layerIndex == 1f) { 
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), layerIndex, elapsed / duration)); 
            }
            ActiveCamera.GetComponent<CinemachineCameraOffset>().m_Offset = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (layerIndex == 0f)
        {
            StartCoroutine(AnotherCoroutine());
        }
         
    }

    IEnumerator AnotherCoroutine() 
    {
        animator.Play("PistolKeepBack", 1); 
        yield return new WaitForSeconds(0.5f);

        float duration2 = 0.14f;
        float elapsed2 = 0f;
        // Hand+Camera anim  
        while (elapsed2 < duration2)
        { 
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, elapsed2 / duration2 )); 
            elapsed2 += Time.deltaTime;
            yield return null;
        }
    }

}
