using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

public class ItemObject : MonoBehaviour
{
    public enum ItemType { weapon, throwable , Static};

    public InventoryItem item;
    public TextMeshProUGUI text;
    public string message;
    [HideInInspector] public Vector3 originalSize;
    public Vector3 shrinkSize;

    private Vector3 initialVelocity; // Initial velocity of the projectile
    private float gravity; // Gravity value

    private Vector3 currentPosition;
    private float time = 0f;

    bool canthrow = false;

    public UnityEvent optionOneEvent;
    public UnityEvent optionTwoEvent;
    public UnityEvent optionThreeEvent;
    public UnityEvent optionFourEvent;

    public UnityEvent pickUpEvent;

    private void Awake()
    {
        originalSize = transform.localScale;
    }

    public void InitializeProjectile(Vector3 initialVelocity, float gravity)
    {
        this.initialVelocity = initialVelocity;
        this.gravity = gravity;
        canthrow = true;

    }

    void Update()
    {
        if (canthrow)
        {
            // Update time
            time += Time.deltaTime;

            // Calculate new position based on time, initial velocity, and gravity
            currentPosition = transform.position + initialVelocity * time + 0.5f * Vector3.down * gravity * time * time;

            // Update position
            transform.position = currentPosition;

            // Check for collision with ground (for simplicity, just check y position)
            if (transform.position.y <= (InventoryManager.Instance.player.transform.position.y + 0.3f))
            {
                canthrow = false;
                // Destroy the projectile if it hits the ground
                Debug.Log("REACHED GROUND");
                // Destroy(gameObject);
            }
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!InventoryManager.Instance.player.hasObjectOnHand)
        {
            if (other.gameObject != this.gameObject)
            {
                if (other.gameObject.name == "Option1")
                {
                    other.enabled = false;
                    this.gameObject.SetActive(false);
                    if (optionOneEvent != null) optionOneEvent.Invoke();
                }
                else if (other.gameObject.name == "Option2")
                {
                    this.gameObject.SetActive(false);
                    other.enabled = false;
                    if (optionTwoEvent != null) optionTwoEvent.Invoke();
                }
                else if (other.gameObject.name == "Option3")
                {
                    other.enabled = false;
                    this.gameObject.SetActive(false);
                    if (optionThreeEvent != null) optionThreeEvent.Invoke();
                }
                else if (other.gameObject.name == "Option4")
                {
                    other.enabled = false;
                    this.gameObject.SetActive(false);
                    if (optionFourEvent != null) optionFourEvent.Invoke();
                }
            }
        }
    }
}
