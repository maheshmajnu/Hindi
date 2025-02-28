using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders; 

public class StaticVariables : MonoBehaviour
{
    //donot repeat spwaning this on restart
    public static GameObject thisStaticGO;

    //login variables
    public static string Session_Id = "";
    public string debugSession_Id;
    public static string User_Id = "";
    public string debugUser_Id;

    public static string Session_Uname = "";
    public static string Session_Psw = "";
    public static string Session_Lang = "hindi";


    // build application
    public static int App_build_Version = 6;    

    // input  game controls
    public static int gamemode = 1;   //1=pc,2=mobile
    public int debugGamemode;

    // grade-wise class name
    public static string grade_class_name ="";

    // lab chapter path
    public static string Chapter_Filepath = "";
    public static string Topic_Name = "";
    public string debugChapter_Filepath;

    //biology human objective
    public static string Human_Objective = "";

    //addressable Scene Instance reference
    public static SceneInstance previous_LoadedScene;
    public static string scene_addressableKey;


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        if (thisStaticGO == null)
        {
            thisStaticGO = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


    private void Start()
    { 

    }

    private void Update()
    {
        debugGamemode = gamemode;
        debugChapter_Filepath = Chapter_Filepath;
        debugSession_Id = Session_Id;
        debugUser_Id = User_Id;

        
    }
    
}
