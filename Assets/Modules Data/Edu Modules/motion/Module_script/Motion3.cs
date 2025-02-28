using TMPro;
using UnityEngine;

public class Motion3 : MonoBehaviour
{
    public Sfx_motion sfx_Motion;
    public float initialVelocity = 0f;
    public float acceleration = 0f;
    public float FinalVelocity = 0f;
    public float maxTime = 60f;
    public float minTime = 50f;
    public float timeElapsed = 0f;
    public float accelerationRate = 1f;
    public float decelerationRate = 0.05f;
    public float targetFinalSpeed = 100f;
    public float moveSpeed = 0f;
    public float baseSpeed = 50f; // Base speed limit
    public float pressTime = 0f;
    public float pressRate = 1f;
    public float depressRate = 1f;

    public float horizontalSpeed = 5f; // Speed for horizontal movement
    public float minX = -4f; // Minimum X-axis limit
    public float maxX = 8f;  // Maximum X-axis limit

    private bool moveLeft = false;
    private bool moveRight = false;

    // TextMeshPro fields for UI display
    public TextMeshProUGUI initialVelocityText;
    public TextMeshProUGUI accelerationText;
    public TextMeshProUGUI timeElapsedText;
    public TextMeshProUGUI finalVelocityText;
    public TextMeshProUGUI speedMeterText;

    public GameObject GameWin;
    public GameObject Gameloose;

    public bool isAccelerating = false; // Flag for button state
    public bool isreachspeed = false; // Flag for button state

    void Update()
    {
        // Update time
        timeElapsed += Time.deltaTime;

        // Handle physics updates
        if (isAccelerating)
        {
            acceleration += accelerationRate * Time.deltaTime;
            pressTime += pressRate * Time.deltaTime;
        }
        else
        {
            acceleration = Mathf.Max(0, acceleration - decelerationRate * Time.deltaTime);
            pressTime = Mathf.Max(0, pressTime - depressRate * Time.deltaTime);
            moveSpeed -= 7 * Time.deltaTime;
            moveSpeed = Mathf.Clamp(moveSpeed, 0, baseSpeed);
        }

        // Calculate FinalVelocity
        FinalVelocity = acceleration * pressTime;

        // Update moveSpeed and clamp it to baseSpeed
        moveSpeed += acceleration * pressTime * Time.deltaTime;
        moveSpeed = Mathf.Clamp(moveSpeed, 0, baseSpeed);

        // Move the spaceship forward
        MoveGameObject(moveSpeed);

        // Handle horizontal movement
        HandleButtonMovement();

        // Update the UI
        UpdateGameInfoUI();

        // Win/Loss Conditions
        
            //if (timeElapsed >= minTime && timeElapsed <= maxTime && isreachspeed)
            //{
            //    sfx_Motion.StepComplete();
            //    sfx_Motion.Level5();
            //}
            if (timeElapsed < maxTime && isreachspeed)
            {
                sfx_Motion.StepComplete();
                sfx_Motion.Level5();
            }
            else if (timeElapsed > maxTime)
            {
                sfx_Motion.MissionFailed();
            }
        
        
    }

    public void StartAccelerating()
    {
        isAccelerating = true;
        Debug.Log("Acceleration started.");
    }

    public void StopAccelerating()
    {
        isAccelerating = false;
        
        Debug.Log("Acceleration stopped.");
    }

    private void MoveGameObject(float moveSpeed)
    {
        // Ensure the object keeps moving forward even with reduced speed
        if (moveSpeed > 0)
        {
            transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    private void HandleButtonMovement()
    {
        if (moveLeft)
        {
            float newX = transform.localPosition.x - horizontalSpeed * Time.deltaTime;
            newX = Mathf.Clamp(newX, minX, maxX);
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
        }

        if (moveRight)
        {
            float newX = transform.localPosition.x + horizontalSpeed * Time.deltaTime;
            newX = Mathf.Clamp(newX, minX, maxX);
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
        }
    }

    // Methods to be assigned to UI Buttons
    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void StopMovingLeft()
    {
        moveLeft = false;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void StopMovingRight()
    {
        moveRight = false;
    }

    private void UpdateGameInfoUI()
    {
        initialVelocityText.text = $"InitialVelocity: {initialVelocity:F0} m/s";
        accelerationText.text = $"Acceleration: {acceleration:F0} m/s²";
        timeElapsedText.text = $"TimeElapsed: {timeElapsed:F0} s";
        finalVelocityText.text = $"FinalVelocity: {FinalVelocity:F0} m";
        speedMeterText.text = $"{Mathf.RoundToInt(FinalVelocity)}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Blockade")
        {
            acceleration = Mathf.Max(0, acceleration - 10);
            moveSpeed = Mathf.Max(0, moveSpeed - 30);
            Debug.Log("Triggered with Blockade. Acceleration reduced.");
        }

        if (other.gameObject.name == "portal")
        {
            if (timeElapsed < maxTime && FinalVelocity >= 400)
            {
                sfx_Motion.StepComplete();
                Collider portalCollider = other.gameObject.GetComponent<Collider>();
                portalCollider.enabled = false;
                sfx_Motion.Level5();
            }
            else if (timeElapsed > maxTime || FinalVelocity <= 400)
            {
                Collider portalCollider = other.gameObject.GetComponent<Collider>();
                portalCollider.enabled = false;
                sfx_Motion.MissionFailed();
            }
            
        }
    }
}
