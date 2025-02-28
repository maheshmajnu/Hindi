using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;


public class DelegateAxis : MonoBehaviour
{

    public float TouchSensitivity_x = 10f;
    public float TouchSensitivity_y = 10f;
     

    // Use this for initialization
    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate; 
    }

    private void Update()
    {
    }

    float HandleAxisInputDelegate(string axisName)
    {
        switch (axisName)
        {

            case "Mouse X":
                 
                    if (Input.touchCount > 0 && !IsPointerOverGameObject())
                    {
                        return Input.touches[0].deltaPosition.x / TouchSensitivity_x; 
                    } 
                else
                    { 
                        return Input.GetAxis(axisName);
                    }
                 

            case "Mouse Y":
                if (Input.touchCount > 0 && !IsPointerOverGameObject())
                {
                    return Input.touches[0].deltaPosition.y / TouchSensitivity_y;
                }
                else
                { 
                   // return 0; 
                    return Input.GetAxis(axisName);
                }

            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }

    public static bool IsPointerOverGameObject()
    {
        //check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        //check touch
        if (Input.touchCount > 0)//&& Input.touches[0].phase == TouchPhase.Began)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[i].fingerId))
                { 
                    return true;
                } 
            }
        }

        return false;
    }


    
}