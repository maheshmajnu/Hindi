using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CM_Aim_Offset : MonoBehaviour
{

    public CinemachineCore.Stage m_ApplyAfter;
    public Vector3 m_Offset;
    public CinemachineCameraOffset CM_offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CM_offset.m_Offset = m_Offset;
    }
}
