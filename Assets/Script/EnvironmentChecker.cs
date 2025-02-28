using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 

public class EnvironmentChecker : MonoBehaviour
{
    public Vector3 rayOffset = new Vector3(0, 0.2f, 0);
     
    private float raylength = 0.45f;
    public float heightRayLength = 6f;
    public LayerMask obstaclelayer;
     


    public RaycastHit hitInfo1;
    public RaycastHit heightInfo1;
    private void Update()
    {
        if (Input.GetKeyDown("x")) { 
            var rayOrigin = transform.position + rayOffset;
            bool hitFound1 = Physics.Raycast(rayOrigin, transform.forward, out hitInfo1, raylength, obstaclelayer);
            Debug.DrawRay(rayOrigin, transform.forward * raylength, (hitFound1) ? Color.red : Color.green); 
            if (hitFound1)
            {
                var heightOrigin = hitInfo1.point + Vector3.up * heightRayLength;
                bool heightHitFound1 = Physics.Raycast(heightOrigin, Vector3.down, out heightInfo1, heightRayLength, obstaclelayer);
                Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (heightHitFound1) ? Color.yellow : Color.green);

                float checkHeight = heightInfo1.point.y - transform.position.y;
                Debug.Log("checkHeight: "+ checkHeight);
            }
        }
    }

 



    public ObstacleInfo CheckObstacle() {

           //forward ray check
        var hitData = new ObstacleInfo();

        var rayOrigin = transform.position + rayOffset;
        hitData.hitFound = Physics.Raycast(rayOrigin, transform.forward, out hitData.hitInfo, raylength, obstaclelayer);
        Debug.DrawRay(rayOrigin, transform.forward * raylength, (hitData.hitFound) ? Color.red : Color.green);

        //Check player has no Roof over his head
        hitData.RoofHitFound = Physics.Raycast(rayOrigin, transform.up, out hitData.RoofhitInfo, 4f, obstaclelayer);
        Debug.DrawRay(rayOrigin, transform.up * 4f, (hitData.RoofHitFound) ? Color.red : Color.green);


        if (hitData.hitFound && !hitData.RoofHitFound)
        {
            var heightOrigin = hitData.hitInfo.point + Vector3.up * heightRayLength;
            hitData.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.heightInfo, heightRayLength, obstaclelayer);
            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitData.heightHitFound) ? Color.blue : Color.green); 
        }

        return hitData;
    }

 

    public struct ObstacleInfo 
    {
        public bool RoofHitFound; 
        public bool hitFound;
        public bool heightHitFound;

        
        public RaycastHit RoofhitInfo; 
        public RaycastHit hitInfo;
        public RaycastHit heightInfo;
    }
}
