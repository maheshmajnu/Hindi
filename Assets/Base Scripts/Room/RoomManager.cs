using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class RoomManager : MonoBehaviour
{
    private string filepath;
    //on awake
    void Awake()
    {
        //get the respective class filepath from static variable
        filepath = StaticVariables.Chapter_Filepath;

        if(filepath != null) { 
        //get selected prefab  & instantiate respective class prefab 
        GameObject SelectedPrefabObj = (GameObject)Resources.Load(filepath, typeof(GameObject)); 
        Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity); 


        } else
        {
            //invalid file path - shows blank scene
            //write code here to go back
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
