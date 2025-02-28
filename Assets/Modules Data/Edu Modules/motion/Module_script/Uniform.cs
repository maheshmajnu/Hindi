using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Uniform : MonoBehaviour
{
    public Sfx_motion sfx_Motion;

    // Waypoints and movement variables
    public List<Transform> waypoints;
    public float baseSpeed = 15f;
    public float acceleration = 5f;
    public float deceleration = 2f;
    public float turnSpeed = 5f;

    private int currentWaypointIndex = 0;
    private float currentSpeed = 0f;
    private bool isAccelerating = false;

    // Lap tracking variables
    private int currentLap = 1;
    private float lapStartTime;
    private float lapTime1 = 0f, lapTime2 = 0f;

    // UI elements
    public TextMeshProUGUI carSpeedText;
    public TextMeshProUGUI Lap1;
    public TextMeshProUGUI Lap2;
    public TextMeshProUGUI missionResult;

    void Start()
    {
        // Initialize lap start time
        lapStartTime = Time.time;
    }

    void Update()
    {
        CarMovinglv2();
        UpdateLapTexts();
    }

    void CarMovinglv2()
    {
        if (waypoints.Count == 0) return;

        // Get the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Move towards the waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * currentSpeed * Time.deltaTime;

        // Rotate towards the waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Check if the car is close enough to the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

            // Check if the lap is complete
            if (currentWaypointIndex == 0)
            {
                CompleteLap();
                currentSpeed = 0f;
            }
        }

        // Handle acceleration
        if (isAccelerating)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, baseSpeed);
        }
        else
        {
            currentSpeed = Mathf.Max(currentSpeed - deceleration * Time.deltaTime, 0f);
        }

        UpdateSpeedText();
    }

    void UpdateSpeedText()
    {
        if (carSpeedText != null)
        {
            carSpeedText.text = $"{currentSpeed:F0}"; // Display current speed up to 2 decimal places
        }
    }

    void UpdateLapTexts()
    {
        // Continuously update Lap1 and Lap2 texts as the player progresses through the game
        if (currentLap == 1)
        {
            float lapTime = Time.time - lapStartTime;
            Lap1.text = $"Lap 1: {lapTime:F0}s, Speed: {currentSpeed:F0}";
        }
        else if (currentLap == 2)
        {
            float lapTime = Time.time - lapStartTime;
            Lap2.text = $"Lap 2: {lapTime:F0}s, Speed: {currentSpeed:F0}";
        }
    }

    void CompleteLap()
    {
        float lapTime = Time.time - lapStartTime; // Calculate lap time
        lapStartTime = Time.time; // Reset start time for the next lap

        if (currentLap == 1)
        {
            lapTime1 = lapTime;
            Lap1.text = $"Lap 1: {lapTime:F0}s, Speed: {currentSpeed:F0}"; // Update Lap 1 text
        }
        else if (currentLap == 2)
        {
            lapTime2 = lapTime;
            Lap2.text = $"Lap 2: {lapTime:F0}s, Speed: {currentSpeed:F0}"; // Update Lap 2 text

            // Check if lap times are close enough (within 2 seconds difference)
            float timeDifference = Mathf.Abs(lapTime1 - lapTime2);

            if (timeDifference <= 2f)
            {
                sfx_Motion.StepComplete();
                sfx_Motion.Level3();
            }
            else
            {
                sfx_Motion.MissionFailed();
            }
        }

        currentLap++;
    }

    // UI Button methods
    public void StartAccelerating()
    {
        isAccelerating = true;
        
    }

    public void StopAccelerating()
    {
        isAccelerating = false;
    }
}
