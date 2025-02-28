using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnvironmentChecker;
//using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(menuName = "Scriptable Objects/Climbable Blocks")]
public class MatchTarget : ScriptableObject
{
    [Header("Checking Obstacle height")]
    [SerializeField] string animationName;
    [SerializeField] float minimumHeight;
    [SerializeField] float maximumHeight;

    [Header("Rotating Player towards Obstacle")]
    [SerializeField] bool lookAtObstacle;
    public Quaternion RequiredRotation{get; set;}

    [Header("Target Matching")]
    [SerializeField] bool allowTargetMatching = true;
    [SerializeField] AvatarTarget compareBodyPart;
    [SerializeField] float compareStartTime;
    [SerializeField] float compareEndTime;
    public Vector3 ComparePosition { get; set;}


    public bool CheckIfAvailable(ObstacleInfo hitData, Transform player) 
    {
        float checkHeight = hitData.heightInfo.point.y - player.position.y;

       // if (checkHeight < 0) {
            checkHeight = Math.Abs(checkHeight);
            checkHeight = Mathf.Round(checkHeight * 100.0f) / 100.0f;
         
        Debug.Log("checkHeight: "+ checkHeight);

        if (checkHeight < minimumHeight || checkHeight > maximumHeight)
        {
            return false;
        }
        else 
        {
            if (lookAtObstacle)
            {
                RequiredRotation = Quaternion.LookRotation(-hitData.hitInfo.normal);
            }

            if(allowTargetMatching)
            {
                ComparePosition = hitData.heightInfo.point;
            }
            return true;
        }
         
    }
    
    public string AnimationName => animationName;
    public bool LookAtObstacle => lookAtObstacle;
    public bool AllowTargetMatching => allowTargetMatching;
    public AvatarTarget CompareBodyPart  => compareBodyPart;
    public float CompareStartTime => compareStartTime;
    public float CompareEndTime => compareEndTime;

}
