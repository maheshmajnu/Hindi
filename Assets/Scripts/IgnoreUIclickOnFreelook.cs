using UnityEngine;
using Cinemachine; 
using UnityEngine.EventSystems;

public class IgnoreUIclickOnFreelook : MonoBehaviour
{
    private int Index, Indexui;
    private float InitSpeedX =300f, InitSpeedY=2f;
    public CinemachineFreeLook cFL; 
    // Start is called before the first frame update
    void Start()
    {
        InitSpeedX = cFL.m_XAxis.Value;
        InitSpeedY = cFL.m_YAxis.Value;
    }

    // Update is called once per frame


    void Update()
    {


        if (Input.touchCount > 0)
        {

            for (int i = 0; i < Input.touches.Length; i++)
            {
                Indexui = i;
                Touch touchui = Input.GetTouch(Indexui);


                if (!EventSystem.current.IsPointerOverGameObject(touchui.fingerId))
                {
                    Debug.Log("Not over UIgameobject");
                    InitSpeedX = cFL.m_XAxis.Value;
                    InitSpeedY = cFL.m_YAxis.Value;
                }

            }
      

            for (int j = 0; j < Input.touches.Length; j++)
            {
                Index = j;
                Touch touch = Input.GetTouch(Index);


                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    Debug.Log("Over UIGameobject"); 
                    
                    cFL.m_YAxis.Value = InitSpeedY;
                    cFL.m_XAxis.Value = InitSpeedX;
                }

            }

            /* 
               if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
               { 

                   cFL.m_YAxis.m_MaxSpeed = 0;
                   cFL.m_XAxis.m_MaxSpeed = 0;
               }
               else
               {
                   cFL.m_YAxis.m_MaxSpeed = InitSpeedX;
                   cFL.m_XAxis.m_MaxSpeed = InitSpeedY;
               }
               */
        }
    }

    }

