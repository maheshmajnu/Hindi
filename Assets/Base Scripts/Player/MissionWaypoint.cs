using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    public Image img;
    public Transform target;
    public TextMeshProUGUI meter;
    public Vector3 offset;
    public float distance;
    public Transform player;

    [SerializeField]
    private Camera mainCamera;
    private RectTransform imgRectTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        imgRectTransform = img.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (player == null || mainCamera == null || imgRectTransform == null || target == null)
        {
            mainCamera = Camera.main; // Fallback to update camera reference
            return;
        }

        // Use CAMERA'S forward direction instead of player's
        Vector3 directionToTarget = (target.position - mainCamera.transform.position).normalized;
        float dotProduct = Vector3.Dot(directionToTarget, mainCamera.transform.forward);

        bool isTargetBehind = dotProduct < 0;

        // Convert target position to screen space
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position + offset);

        // Screen bounds calculations
        float minX = imgRectTransform.rect.width / 2f;
        float maxX = Screen.width - minX;
        float minY = imgRectTransform.rect.height / 1.33f;
        float maxY = Screen.height - minY;

        if (isTargetBehind)
        {
            // Flip position if behind
            if (screenPos.z < 0) screenPos *= -1;

            // Get screen center position
            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

            // Calculate direction from screen center
            screenPos -= screenCenter;
            float angle = Mathf.Atan2(screenPos.y, screenPos.x);
            angle -= 90f * Mathf.Deg2Rad;

            // Calculate new position at screen edge
            float cos = Mathf.Cos(angle);
            float sin = -Mathf.Sin(angle);
            screenPos = screenCenter + new Vector3(sin * 150, cos * 150, 0);

            // Keep within screen bounds
            float slope = cos / sin;
            screenPos.x = Mathf.Clamp(screenPos.x, minX, maxX);
            screenPos.y = Mathf.Clamp(screenPos.y, minY, maxY);
        }
        else
        {
            // Normal in-front clamping
            screenPos.x = Mathf.Clamp(screenPos.x, minX, maxX);
            screenPos.y = Mathf.Clamp(screenPos.y, minY, maxY);
        }

        img.transform.position = screenPos;

        // Distance calculation
        float distanceToTarget = Vector3.Distance(target.position, player.position) - 5f;
        meter.text = Mathf.Max(distanceToTarget, 0f).ToString("0") + "m";
    }
}