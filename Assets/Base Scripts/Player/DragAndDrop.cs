using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragAndDrop : MonoBehaviour
{
    Rigidbody rigidbody;
    Vector3 mousePosition;
    public Camera camera;

    public GameObject triggeredObj;

    public bool isMultiDropChecker;
    public bool hasNoFail = false;

    public UnityEvent dropEvent;

    public TestSceneManager sceneManager;

    private BoxCollider coloider;
    

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();  
        coloider = GetComponent<BoxCollider>();
    }

    private Vector3 GetMousePos()
    {
        return camera.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        coloider.enabled = false;
        rigidbody.useGravity = false;
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = camera.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void OnMouseUp()
    {
        coloider.enabled = true;
        rigidbody.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            
            if(isMultiDropChecker)
            {
                Debug.Log(other.transform.parent.gameObject.name);
                if (this.gameObject.name == other.transform.parent.gameObject.name)
                {
                    other.gameObject.tag = "Untagged";
                    triggeredObj = other.gameObject;
                    StartCoroutine(Triggered());
                }
                else
                {
                    InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
                }
            }
            else
            {
                if (dropEvent != null) dropEvent.Invoke();
            }
        }
    }

    IEnumerator Triggered()
    {
        yield return new WaitForSeconds(0.1f);
        {
            rigidbody.useGravity = false;
            if (dropEvent != null) dropEvent.Invoke();
            coloider.enabled = false;
        }
    }
}
