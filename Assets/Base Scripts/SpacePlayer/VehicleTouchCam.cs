using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class VehicleTouchCam : MonoBehaviour
{

    Rect notouchable_area;


    [SerializeField] float TouchSensitivity_x = 10f, TouchSensitivity_y = 10f;
    public float xmin, xmax, ymin, ymax; //60,100,0,100

    int InsideAreaTouchId = -1;
    bool Released = false;
    Touch AnalogTouch;
    Vector2 NormalizedAxis = Vector2.zero;

    public RectTransform rectTransform;





    public Transform body; // set here the player transform 
    float xRotation = 0f;
    public Vector2 turn;
    public float sensitivity = 300;
    public Vector3 deltaMove;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
       // CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
        #region Touch Handler (Touch will not work in these rectangles)
        //Setting any of xMin, xMax, yMin and yMax will resize the rectangle
        notouchable_area = new Rect(getX(-50.2f), getY(-78.3f), getX(1100), getY(180));
        /* xmin = getX (-50.2f);
         xmax = getX (1100);
         ymin = getY (-78.3f);
         ymax = getY (180);*/

        #endregion
    }
    public float getX(float valX)
    {
        float x = (valX / 800) * 100;
        return (x / 100) * Screen.width;
    }

    public float getY(float valY)
    {
        float y = (valY / 480) * 100;
        return (y / 100) * Screen.height;
    }

    void OnGUI()
    {
        // GUI.Box(notouchable_area,"");
    }

    bool CheckArea(Vector2 pos)
    {

        Debug.Log("X" + (pos.x / Screen.width) * 100f);
        Debug.Log("Y" + (pos.y / Screen.height) * 100f);
        Vector2 npos = new Vector2((pos.x / Screen.width) * 100f, (pos.y / Screen.height) * 100f);
        if (npos.x > xmin && npos.x < xmax && npos.y > ymin && npos.y < ymax)
        {
            return true;
        }
        else
        {
            return false;
        }

        //RectTransformUtility.RectangleContainsScreenPoint(rectTransform, pos); 
        /*  if (rectTransform.rect.Contains (pos)){ 
                return false;
            } else { 
                 return true;
            } */

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Released)
            {
                InsideAreaTouchId = GetAnalogTouchIDInsideArea(); //-1 = none
            }

            if (InsideAreaTouchId != -1)
            {
                AnalogTouch = Input.GetTouch(InsideAreaTouchId);
                if (Released)
                {
                    if (AnalogTouch.phase == TouchPhase.Began)
                    {
                        Released = false;
                        TouchBegan();
                    }
                }
                else
                {
                    if (AnalogTouch.phase == TouchPhase.Ended) TouchEnd();
                }
            }
            else
            {
                Released = true;
            }
        }
        else
        {
            InsideAreaTouchId = -1;
            Released = true;
        }

        // Debug.Log(InsideAreaTouchId);

        LookCam();
    }

    void LookCam()
    {
        if (Input.touchCount > 0 && InsideAreaTouchId != -1)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            turn.y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;



            xRotation -= turn.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);


            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            body.Rotate(Vector3.up * mouseX);
        }
    }

    int GetAnalogTouchIDInsideArea()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (CheckArea(Input.GetTouch(i).position))
                return i;
        }
        return -1;
    }

    void TouchBegan()
    {
        Released = false;
    }

    void TouchEnd()
    {
        Released = true;
        InsideAreaTouchId = -1;
    }
}
