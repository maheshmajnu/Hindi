using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonUniform : MonoBehaviour
{
    public Sfx_motion sfx_Motion;
    // Waypoints and movement variables
    public List<Transform> waypoints;
    public float baseSpeed = 10f;
    public float acceleration = 5f;
    public float deceleration = 2f;
    public float turnSpeed = 5f;

    private int currentWaypointIndex = 0;
    private float currentSpeed = 0f;
    private bool isAccelerating = false;

    // Lap tracking variables
    private int currentLap = 1;
    private float lapStartTime;
    private float[] lapTimes = new float[3]; // Store lap times for 3 laps

    // UI elements
    public TextMeshProUGUI carSpeedText;
    public TextMeshProUGUI Lap1;
    public TextMeshProUGUI Lap2;
    public TextMeshProUGUI Lap3;
    public TextMeshProUGUI missionResult;

    void Start()
    {
        lapStartTime = Time.time; // Initialize lap start time
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
        // Continuously update Lap1, Lap2, and Lap3 texts as the player progresses through the game
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
        else if (currentLap == 3)
        {
            float lapTime = Time.time - lapStartTime;
            Lap3.text = $"Lap 3: {lapTime:F0}s, Speed: {currentSpeed:F0}";
        }
    }

    void CompleteLap()
    {
        float lapTime = Time.time - lapStartTime; // Calculate lap time
        lapTimes[currentLap - 1] = lapTime; // Store lap time
        lapStartTime = Time.time; // Reset start time for the next lap

        if (currentLap == 1)
        {
            Lap1.text = $"Lap 1: {lapTime:F0} s, Speed:  {currentSpeed:F0}";
        }
        else if (currentLap == 2)
        {
            Lap2.text = $"Lap 2: {lapTime:F0} s, Speed:  {currentSpeed:F0}";
        }
        else if (currentLap == 3)
        {
            Lap3.text = $"Lap 3: {lapTime:F0} s, Speed:  {currentSpeed:F0}";

            // Check mission result
            if (IsMissionFailed())
            {
                sfx_Motion.MissionFailed();
            }
            else
            {
                sfx_Motion.StepComplete();
                sfx_Motion.Level3Question();
            }
        }

        currentLap++;
    }

    bool IsMissionFailed()
    {
        // Ensure all lap times are within 1 second of each other
        return Mathf.Abs(lapTimes[0] - lapTimes[1]) <= 2f &&
               Mathf.Abs(lapTimes[1] - lapTimes[2]) <= 2f &&
               Mathf.Abs(lapTimes[0] - lapTimes[2]) <= 2f;
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
