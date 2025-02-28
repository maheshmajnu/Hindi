using TMPro; // Import TextMeshPro namespace
using Unity.VisualScripting;
using UnityEngine;

public class SpaceshipGame : MonoBehaviour
{
    public Sfx_motion sfx_Motion;
    public float initialVelocity = 0f;
    public float acceleration = 0f;
    public float displacement = 0f;
    public float spaceShipMove;
    public float maxTime = 60f;
    public float minTime = 50f;
    public float timeElapsed = 0f;
    public float accelerationRate = 1f;
    public float decelerationRate = 0.05f;
    public float targetDistance = 1000f;
    public float pressTime = 0f;
    public float pressRate = 1f;
    public float FinalVelocity = 0f;
    public float moveSpeed = 0f;
    public float baseSpeed = 50f;

    public GameObject SpacePetrol;
    public float SpacePetrolSpeed = 2f;
    private Rigidbody rb;

    // TextMeshPro fields for UI display
    public TextMeshProUGUI initialVelocityText;
    public TextMeshProUGUI accelerationText;
    public TextMeshProUGUI timeElapsedText;
    public TextMeshProUGUI displacementText;
    public TextMeshProUGUI speedoMeterText;

    

    

    public bool isAccelerating = false; // Flag for button state
    public bool isreachdestination = false;

    private void Awake()
    {
        rb = SpacePetrol.GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.velocity = Vector3.right * SpacePetrolSpeed;
        

        // Update time
        timeElapsed += Time.deltaTime;

        // Handle physics updates
        if (isAccelerating)
        {
            acceleration += accelerationRate * Time.deltaTime;
            pressTime += pressRate * Time.deltaTime;
        }

        // Calculate FinalVelocity
        FinalVelocity = acceleration * pressTime;

        // Update moveSpeed and clamp it to baseSpeed
        moveSpeed += acceleration * pressTime * Time.deltaTime;
        moveSpeed = Mathf.Clamp(moveSpeed, 0, baseSpeed);

        // Update position using the second equation of motion
        displacement = initialVelocity * timeElapsed + 0.5f * acceleration * Mathf.Pow(timeElapsed, 2);


        // Move the spaceship based on the displacement
        MoveGameObject(moveSpeed);

        // Update the UI
        UpdateGameInfoUI();

        // Win/Loss Conditions
        
            if (timeElapsed >= minTime && timeElapsed <= maxTime && isreachdestination)
            {
                
            }
            
            else if (timeElapsed > maxTime)
            {
                
                sfx_Motion.MissionFailed();
                
            }
        
       
    }

    public void StartAccelerating()
    {
        isAccelerating = true;
        Debug.Log("its working");
    }

    public void StopAccelerating()
    {
        isAccelerating = false;
        Debug.Log("its working");
    }

    private void MoveGameObject(float moveSpeed)
    {
        // Ensure the object keeps moving forward even with reduced speed
        if (moveSpeed > 0)
        {
            transform.localPosition += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.name == "plane")
        {
            isreachdestination = true;
            sfx_Motion.StepComplete();
        }
    }

    private void UpdateGameInfoUI()
    {
        initialVelocityText.text = $"Initial Velocity: {initialVelocity:F0} m/s";
        accelerationText.text = $"Acceleration: {acceleration:F0} m/s²";
        timeElapsedText.text = $"Time Elapsed: {timeElapsed:F0} s";
        displacementText.text = $"Displacement: {displacement:F0} m";
        speedoMeterText.text = $"{FinalVelocity:F0} m";
    }
}
