using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{ 
    PlayerControlManager _PlayerControlManger;
    private GameObject PlayerObject;

    [Header("Menu Panels")]
    private GameObject _controlPanel;
    private GameObject _menuPanel;

    private string url = "http://www.playvroomstudio.com/User/login.php";

    public MonoBehaviour sfxScript; // Use MonoBehaviour to allow any script to be assigned
   

    public void SetSfxScript(MonoBehaviour sfx)
    {
        sfxScript = sfx;
    }


    private void Awake()
    {
        
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        //access all panels in menusystem and hide them accordingly
        _controlPanel = GameObject.Find("ControlPanel");
        _menuPanel = GameObject.Find("MenuPanel");

        if (_controlPanel != null) { _controlPanel.SetActive(false); }
        _menuPanel.SetActive(true);


        //access Player Control Manager Script
        PlayerObject = GameObject.Find("PlayerControl Manager");
        if (PlayerObject != null)
        {
            _PlayerControlManger = PlayerObject.GetComponent<PlayerControlManager>();
        }
         
        // this should be last line only for Awake()
        //gameObject.SetActive(false);
    }


    public void Switch_PC_controller()
    {
        if (_PlayerControlManger != null)
        {
            _PlayerControlManger.ChangeGameMode(1); // 1 = Keyboard&Mouse
        } 
    }
    
    public void Switch_Mobile_controller()
    {
        if (_PlayerControlManger != null)
        {
            _PlayerControlManger.ChangeGameMode(2); // 2 = mobile
        } 
    }
    
    public void Switch_VRBox_controller()
    {
        if (_PlayerControlManger != null)
        {
            _PlayerControlManger.ChangeGameMode(3); // 3 = VR
        }  
    }

    public void GotoMainMenu()
    {
        if (sfxScript != null)
        {
            sfxScript.Invoke("DestroyCheckpoint", 0f);
        }
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.DestroyCheckpointManager();
        }
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void PresistentReloadScene()
    {
        if (sfxScript != null)
        {

            sfxScript.Invoke("RestartFomStart", 0f);
        }
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.ResetCheckpoints();
        }
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(1);

    }


    public void RestartFromCheckPoint()
    {
        Debug.Log("button pressed");
        if (sfxScript != null)
        {
            // Dynamically call the RestartFromCheckPoint method on the assigned sfx script
            sfxScript.Invoke("RestartFromCheckPoint", 0f);
        }
        else
        {
            GameObject loader = GameObject.Find("Sceneloader Canvas");
            loader.GetComponent<SceneLoader>().LoadScene(1);
        }


    }

    //set cursor OFF
    public void SetCursor_ON()
    {
        Cursor.visible = true;
    }
    public void Feedback()
    {
        string nurl = url + "?u=" + StaticVariables.Session_Uname + "&p=" + StaticVariables.Session_Psw + "&go=feedback" + "&module_name=" + StaticVariables.Topic_Name;
        Application.OpenURL(nurl);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
