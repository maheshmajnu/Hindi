using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NoPlayerMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuSystem;

    public MonoBehaviour sfxScript; // Use MonoBehaviour to allow any script to be assigned

    
    public void SetSfxScript(MonoBehaviour sfx)
    {
        sfxScript = sfx;
    }

    // Start is called before the first frame update
    void Start()
    {
        menuSystem.SetActive(true);
    }


    void Update()
    {
        //Escape key was pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            menu.SetActive(true);
        }
    }
    public void GotoMainMenu()
    {
        if (sfxScript != null)
        {
            sfxScript.Invoke("DestroyCheckpoint", 0f);
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
        
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(1);
        Debug.Log("Scene loaded");
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
            PresistentReloadScene();
        }
        

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
