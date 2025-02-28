using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public List<GameObject> nearbyPickupObjects = new List<GameObject>();
    public GameObject pickupObject;
    public PlayerFunctionsController playerMove;

    public bool canPickup;

    public float throwSpeed = 10f; // Speed of the throw
    public float throwHeight = 5f; // Height of the throw

    public Transform alligningParent;


    //Find objects
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable" && other.gameObject.layer == LayerMask.NameToLayer("InventoryObjects"))
        {
            nearbyPickupObjects.Add(other.gameObject);
        }

        if(other.gameObject.tag == "Allignment")
        {
            alligningParent = other.transform;
        }
    }

    //clear objects
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable" && other.gameObject.layer == LayerMask.NameToLayer("InventoryObjects"))

        {
            nearbyPickupObjects.Remove(other.gameObject);
        }

        if (other.gameObject.tag == "Allignment")
        {
            alligningParent = null;
        }
    }

    private void Update()
    {
        canPickup = nearbyPickupObjects.Count > 0;
    }


    //drop object
    public void DropObject()
    {
        InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().HidePickUpPopUp();

        GameObject thrownObject = InventoryManager.Instance.collectedItems[InventoryManager.Instance.currentSelectedObjIndex];
        ItemObject itmObj = thrownObject.GetComponent<ItemObject>();

        if (InventoryManager.Instance.items[InventoryManager.Instance.currentSelectedObjIndex].count > 1)
        {
            GameObject dupThrowObj = Instantiate(thrownObject, playerMove.objectHolder);
            thrownObject = dupThrowObj;
        }

        InventoryManager.Instance.ThrowObject(thrownObject);

        if(alligningParent != null)
        {
            thrownObject.transform.position = alligningParent.position;
            thrownObject.transform.rotation = alligningParent.rotation;
        }
        else
        {
            Vector3 targetPoint = playerMove.mouseWorldPosition;
            //targetPoint.y += itmObj.originalSize.y / 2;
            thrownObject.transform.position = targetPoint;
            thrownObject.transform.rotation = Quaternion.identity;
        }


        
        
        thrownObject.transform.localScale = itmObj.originalSize;
    }

    //Throwing curve
    IEnumerator MoveObjectSmoothly(GameObject thrownObject, Vector3 targetPoint, float totalTime, Vector3 origionalSize)
    {
        float startTime = Time.time;
        Vector3 centre = (thrownObject.transform.position + targetPoint) * 0.5f;
        centre -= new Vector3(0, 0.3f, 0);

        Vector3 startRelCentre = (thrownObject.transform.position - centre);
        Vector3 endRelCentre = targetPoint - centre;

        while (Time.time - startTime < totalTime)
        {
            float fracComplete = (Time.time - startTime) / totalTime;
            thrownObject.transform.position = Vector3.Slerp(startRelCentre, endRelCentre, fracComplete) + centre;
            yield return null;
        }

        thrownObject.transform.position = targetPoint; // Ensure final position matches target

        //// Smoothly move in Y-axis to player's Y position
        //float startY = thrownObject.transform.position.y;
        //float playerY = (playerMove.transform.position.y + 0.5f); // Assuming playerMove is the reference to the player's movement script

        //startTime = Time.time;
        //while (Time.time - startTime < totalTime)
        //{
        //    float fracComplete = (Time.time - startTime) / totalTime;
        //    float newY = Mathf.Lerp(startY, playerY, fracComplete);
        //    thrownObject.transform.position = new Vector3(thrownObject.transform.position.x, newY, thrownObject.transform.position.z);
        //    yield return null;
        //}

        //// Ensure final Y position matches player's Y position
        //thrownObject.transform.position = new Vector3(thrownObject.transform.position.x, playerY, thrownObject.transform.position.z);


        thrownObject.transform.localScale = origionalSize;
        thrownObject.transform.rotation = Quaternion.identity;

    }

    // Throw method
    public void ThrowEvent()
    {
        InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().HidePickUpPopUp();

        GameObject thrownObject = InventoryManager.Instance.collectedItems[InventoryManager.Instance.currentSelectedObjIndex];
        ItemObject itmObj = thrownObject.GetComponent<ItemObject>();

        if (InventoryManager.Instance.items[InventoryManager.Instance.currentSelectedObjIndex].count > 1)
        {
            GameObject dupThrowObj = Instantiate(thrownObject, playerMove.objectHolder);
            thrownObject = dupThrowObj;
        }

        Vector3 targetPoint = playerMove.mouseWorldPosition;
        float totalTime = 1f; // Adjust as needed

        if (alligningParent != null)
        {
            StartCoroutine(MoveObjectSmoothly(thrownObject, alligningParent.transform.position, totalTime, itmObj.originalSize));
        }
        else
        {
            StartCoroutine(MoveObjectSmoothly(thrownObject, targetPoint, totalTime, itmObj.originalSize));
        }

        //StartCoroutine(MoveObjectSmoothly(thrownObject, targetPoint, totalTime,itmObj.originalSize));

        InventoryManager.Instance.ThrowObject(thrownObject);
    }


    //public void ThrowEvent()
    //{
    //    float startTime = 0;
    //    GameObject thrownObject = InventoryManager.Instance.collectedItems[InventoryManager.Instance.currentSelectedObjIndex];
    //    Vector3 targetPoint = playerMove.mouseWorldPosition;

    //    Vector3 centre = (thrownObject.transform.position + targetPoint) * 0.5f;
    //    centre -= new Vector3(0, 1, 0);

    //    Vector3 startRelCentre = (thrownObject.transform.position - centre);
    //    Vector3 endRelCentre = targetPoint - centre;


    //    float fracComplete = (Time.time - startTime) / 1;

    //    transform.position = Vector3.Slerp(startRelCentre,endRelCentre,fracComplete);
    //    transform.position += centre;

    //    if(fracComplete >= 1)
    //    {
    //        startTime = Time.time;
    //    }

    //    Vector3.Slerp(thrownObject.transform.position, targetPoint, 1f);
    //    //Vector3 direction = (targetPoint - transform.position).normalized;
    //    //direction.y = 0f; // Ignore Y component for horizontal movement
    //    //float distance = Vector3.Distance(transform.position, targetPoint);
    //    //float timeToReachTarget = distance / 5;
    //    //Vector3 initialVelocity = direction * 5;

    //    //// Simulate object motion without Rigidbody
    //    //StartCoroutine(SimulateThrow(thrownObject, initialVelocity, targetPoint, timeToReachTarget));

    //    InventoryManager.Instance.ThrowObject();
    //}

    private Vector3 CalculateInitialVelocity(Vector3 startPosition, Vector3 targetPosition, float height, float speed)
    {
        // Calculate the displacement
        Vector3 displacement = targetPosition - startPosition;
        Vector3 horizontalDisplacement = new Vector3(displacement.x, 0f, displacement.z);
        float distance = horizontalDisplacement.magnitude;

        // Calculate the time to reach the peak height
        float timeToPeak = Mathf.Sqrt(2 * height / Physics.gravity.magnitude);

        // Calculate the initial vertical velocity to reach the peak
        float verticalVelocity = Physics.gravity.magnitude * timeToPeak;

        // Calculate the initial horizontal velocity to cover the distance
        float horizontalVelocity = distance / (timeToPeak);

        // Calculate the initial velocity vector
        Vector3 initialVelocity = horizontalDisplacement.normalized * horizontalVelocity + Vector3.up * verticalVelocity;

        return initialVelocity;
    }

    //IEnumerator SimulateThrow(GameObject thrownObject, Vector3 initialVelocity, Vector3 targetPoint, float timeToReachTarget)
    //{
    //    ItemObject itmObj = thrownObject.GetComponent<ItemObject>();
    //    Debug.Log("INITIAL VELOCITY - " + initialVelocity);
    //    float elapsedTime = 0f;
    //    Vector3 startPosition = thrownObject.transform.localPosition;
    //    while (elapsedTime < timeToReachTarget)
    //    {
    //        // Calculate horizontal movement
    //        Vector3 horizontalMovement = initialVelocity * elapsedTime;

    //        // Calculate vertical movement (linear interpolation)
    //        float height = Mathf.Lerp(startPosition.y, (targetPoint.y + (playerMove.transform.position.y + 0.5f)), elapsedTime / timeToReachTarget);
    //        Vector3 verticalMovement = new Vector3(0, height, 0);

    //        Vector3 finalTargetPos = new Vector3(horizontalMovement.x, -verticalMovement.y, horizontalMovement.z);

    //        // Set position
    //        thrownObject.transform.position += finalTargetPos * Time.deltaTime;

    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    thrownObject.transform.position = targetPoint; // Ensure final position matches target\

    //    if (itmObj != null)
    //    {
    //        thrownObject.transform.localScale = itmObj.originalSize;
    //        thrownObject.transform.rotation = Quaternion.identity;
    //    }
    //}

    public GameObject NearByObject()
    {
        float minDistance = float.MaxValue; // Initialize the minimum distance with a large value
        GameObject nearbyObj = null;

        if (nearbyPickupObjects.Count > 0)
        {
            // Iterate through each object in the list
            foreach (GameObject obj in nearbyPickupObjects)
            {
                // Calculate the distance between the player and the current object
                if(obj != null)
                {
                    float distance = Vector3.Distance(transform.position, obj.transform.position);

                    // Check if the current object is closer than the previous nearest object
                    if (distance < minDistance)
                    {
                        minDistance = distance; // Update the minimum distance
                        nearbyObj = obj;
                    }
                }             
            }
        }
        return nearbyObj;

    }
}
