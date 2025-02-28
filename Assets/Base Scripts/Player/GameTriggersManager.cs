using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTriggersManager : MonoBehaviour
{
    public string _triggername; 

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            _triggername = other.name;

            /*
           string scriptame = GameObject.Find("_camera_Explanatory").GetComponent<MonoBehaviour>().ToString();
           string scriptame2 = GameObject.Find("_camera_Explanatory").GetComponent<MonoBehaviour>().GetType().Name;
           Debug.Log(scriptame);
           Debug.Log("name:"+scriptame2);
                   */
        } 

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            _triggername = "";
        }
    }
    public void ClearTriggerName()
    { 
            _triggername = ""; 
    }
}
