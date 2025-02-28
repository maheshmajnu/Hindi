using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bio_ExcretorySysClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gotoClass()
    {
        //save prefabClasspath to static variable
        Debug.Log("gotoclass2");
        StaticVariables.Chapter_Filepath = "SceneRooms/ExcretorySystem/_Main Variant";
        StaticVariables.Topic_Name = "Human Excretory System";

        //open Room scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(3);
        //SceneManager.LoadScene("Room"); 


    }
}
