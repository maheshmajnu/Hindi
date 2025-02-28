using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ResetDefaultCMFreelookAxis : MonoBehaviour
{

    public void Start()
    {
        //reset to default mouse control
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }
    void OnEnable()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }
    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X")
        {
            // return 0;
            return Input.GetAxis("Mouse X");
        }
        else if (axisName == "Mouse Y")
        {
            //return 0;
            return Input.GetAxis("Mouse Y");
        }

        return 0;
    }


  
}
