using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSceneClassSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject Main_6Class;
    [SerializeField]
    private GameObject Main_7Class;
    [SerializeField]
    private GameObject Main_8Class;
    [SerializeField]
    private GameObject Main_9Class;
    [SerializeField]
    private GameObject Main_10Class;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ADSceneClassSwitcher(string classKey)
    {
        if(classKey == "6c")
        {
            Main_6Class.SetActive(true);
        } 
        else if (classKey == "7c")
        {
            Main_7Class.SetActive(true);
        }
        else if (classKey == "8c")
        {
            Main_8Class.SetActive(true);
        }
        else if (classKey == "9c")
        {
            Main_9Class.SetActive(true);
        }
        else if (classKey == "10c")
        {
            Main_10Class.SetActive(true);
        } 

    }
}
