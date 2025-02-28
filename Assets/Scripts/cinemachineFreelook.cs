using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityStandardAssets.CrossPlatformInput;
 

public class cinemachineFreelook : MonoBehaviour
{
    public cinemachineFreelook CMfreeLook;

    /// <summary>Sensitivity multiplier for x-axis</summary>
    [Tooltip("Sensitivity multiplier for x-axis")]
    public float TouchSensitivityX = 3f;

    /// <summary>Sensitivity multiplier for y-axis</summary>
    [Tooltip("Sensitivity multiplier for y-axis")]
    public float TouchSensitivityY = 3f;

    /// <summary>Input channel to spoof for X axis</summary>
    [Tooltip("Input channel to spoof for X axis")]
    public string TouchXInputMapTo = "Mouse X";

    /// <summary>Input channel to spoof for Y axis</summary>
    [Tooltip("Input channel to spoof for Y axis")]
    public string TouchYInputMapTo = "Mouse Y";

    void Start()
    {
        CinemachineCore.GetInputAxis = GetInputAxis;
    }

    private float GetInputAxis(string axisName)
    {
        if (axisName == TouchXInputMapTo)
            return CrossPlatformInputManager.GetAxis(TouchXInputMapTo) * TouchSensitivityX;
        if (axisName == TouchYInputMapTo)
            return CrossPlatformInputManager.GetAxis(TouchYInputMapTo) * TouchSensitivityY;

        return CrossPlatformInputManager.GetAxis(axisName);
    }
}
