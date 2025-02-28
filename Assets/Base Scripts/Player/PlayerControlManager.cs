using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PlayerControlManager : MonoBehaviour
{
    //public static int gamemode = 2;  //1=pc,  2=mobile,  3=vr  coming from static variable
    public GameObject menu;

    [Header("Cinemachine Cameras")]
    [SerializeField]
    private GameObject CM_Cam_Mobile;
    [SerializeField]
    private GameObject CM_Cam_PC;

    public GameObject ActiveCamera;


    private void Awake()
    {
        //access MenuSystem gameObject and Hide it
        menu = GameObject.Find("MenuSystem");
        menu.SetActive(false);

        ActiveCamera = CM_Cam_Mobile;

        //Hide Mobile Cam - byDefault
        CM_Cam_PC.SetActive(false);

        if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        //StaticVariables.gamemode = 1;

        //set default controller on Load using static variable
        ChangeGameMode(StaticVariables.gamemode);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("gamemode:" + StaticVariables.gamemode); 
    }

    // Update is called once per frame
    void Update()
    {
        //Escape key was pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            menu.SetActive(true);
        }
    }

    public void ChangeGameMode(int mode)
    {
        if(mode == 1)
        {
            //turn on PC controller
            Debug.Log("turn on PC controller");
            CM_Cam_Mobile.SetActive(false);
            CM_Cam_PC.SetActive(true);
            //Update static variable 
            StaticVariables.gamemode = 1;
            //ActiveCamera
            ActiveCamera = CM_Cam_PC;
        } 
        else if (mode == 2)
        {
            //turn on Mobile Controller
            Debug.Log("turn on Mobile controller");
            CM_Cam_PC.SetActive(false);
            CM_Cam_Mobile.SetActive(true);
            //Update static variable 
            StaticVariables.gamemode = 2;
            //ActiveCamera
            ActiveCamera = CM_Cam_Mobile;
        } 
        else if (mode == 3)
        {
            //turn on VR-BOX Controller
            Debug.Log("turn on VR-BOX controller");
            //Update static variable 
            StaticVariables.gamemode = 3;
            //ActiveCamera
            //ActiveCamera = CM_Cam_VR;
        }

    }
     
}
