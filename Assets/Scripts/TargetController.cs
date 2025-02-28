using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TargetController : MonoBehaviour
{
    public enum TargetType { Hole, Damagable, Rotatable, Wearable, None, MiniGame };
    public TargetType type;
    public int health;
    public MeshRenderer meshRenderer;
    bool hasCompleted = false;
    public Vector3 rotatePos;
    private Quaternion currentRotation;
    private int currentRotationCount;
    public int rotationCount;
    public bool hasRotated;

    public Transform cameraHolder;
    public Transform cameraTransform;
    public Transform subObject;
    public bool miniGameStarted;
    public bool miniGameClickable;

    public UnityEvent triggerEvent;
    public UnityEvent wearingEvent;
    public UnityEvent defaultEvent;
    public UnityEvent miniGameStartEvent;
    public UnityEvent miniGameEndEvent;
    public UnityEvent rotateEvent;

    public string checkName;

    private void Start()
    {
        if (type == TargetType.Damagable)
        {
            if (defaultEvent != null)
            {
                defaultEvent.Invoke();
            }
        }

        currentRotation = transform.localRotation;
    }
    [ContextMenu("OUTPUT")]
    public void Output()
    {
        if (hasCompleted) return;

        InventoryManager.Instance.GetComponent<GamePlayManager>().HidePickUpPopUp();

        switch (type)
        {
            case TargetType.Hole:
                FillHole();
                break;

            case TargetType.Rotatable:
                Debug.Log("ROTATING");
                RotateObject();
                rotateEvent.Invoke();
                break;

            case TargetType.Damagable:
                TakeDamage();
                break;

            case TargetType.Wearable:
                wearingEvent.Invoke();
                break;

            case TargetType.None:
                this.GetComponent<Collider>().enabled = false;
                defaultEvent.Invoke();
                break;

            case TargetType.MiniGame:
                InventoryManager.Instance.inventryStatic.SetActive(false);
                ChangeToMiniGame();
                miniGameStartEvent.Invoke();
                break;
        }

        InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().HidePickUpPopUp(); 
    }

    

    public void FillHole()
    {
        health--;

        if (health <= 0)
        {
            meshRenderer.enabled = true;
        }
    }

    public void RotateObject()
    {

        // Update the currentRotation by adding 90 degrees
        currentRotation *= Quaternion.Euler(rotatePos);

        // Tween to the new rotation over 1 second
        transform.DOLocalRotateQuaternion(currentRotation, 1f);


    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            if (triggerEvent != null) triggerEvent.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable" && other.gameObject.GetComponent<TargetController>()?.type != TargetType.Damagable)
        {
            this.GetComponent<BoxCollider>().isTrigger = false;
            hasCompleted = true;
            if (triggerEvent != null) triggerEvent.Invoke();
        }

        if (other.gameObject.tag == "Player" && type != TargetType.MiniGame && type != TargetType.None)
        {
            if (defaultEvent != null) defaultEvent.Invoke();
            Debug.Log("PUSH PLAYER UP");
        }
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || InventoryManager.Instance.player.InteractIspressed) && miniGameStarted && miniGameClickable && !EventSystem.current.IsPointerOverGameObject())
        {
            defaultEvent.Invoke();
        }

        if (type == TargetType.Wearable && miniGameClickable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                defaultEvent.Invoke();
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                defaultEvent.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                triggerEvent.Invoke();
            }
        }
    }

    public void DelayOutput()
    {
        Invoke("Output", 2);
    }

    public void EndMiniGame()
    {
        InventoryManager.Instance.inventryStatic.SetActive(true);
        miniGameStarted = false;
        miniGameEndEvent.Invoke();
    }

    public void DelayEndMiniGame()
    {
        Invoke("EndMiniGame", 2);
    }

    private void ChangeToMiniGame()
    {
        if (triggerEvent != null) triggerEvent.Invoke();

        this.GetComponent<BoxCollider>().enabled = false;
        cameraTransform.gameObject.SetActive(true);
        miniGameStarted = true;
        StartCoroutine(DelayChangeCH());
        cameraTransform.position = cameraHolder.position;
        cameraTransform.rotation = cameraHolder.rotation;
        InventoryManager.Instance.GetComponent<GamePlayManager>().HidePickUpPopUp();
        InventoryManager.Instance.GetComponent<GamePlayManager>().rotatePopUp.SetActive(false);
    }

    IEnumerator DelayChangeCH()
    {
        yield return new WaitForSeconds(0.5f);
        cameraTransform.position = cameraHolder.position;
        cameraTransform.rotation = cameraHolder.rotation;

    }

    private void OnMouseDown()
    {
        if (this.gameObject.tag == "Untagged")
        {
            if (type == TargetType.Damagable) this.gameObject.SetActive(false);
            else if (type == TargetType.None)
            {
                if (defaultEvent != null) defaultEvent.Invoke();
            }
        }
    }
}
