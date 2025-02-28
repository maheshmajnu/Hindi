using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetModuleManager : MonoBehaviour
{
    public PlayerFunctionsController mainPlayer;
    // Start is called before the first frame update
    void Start()
    {
        InitializeScene();
    }

    void InitializeScene()
    {
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/PlayerPrefabs/Main/__Player", typeof(GameObject));  // Load Player
        Instantiate(SelectedPrefabObj, new Vector3(0, 2, 0), Quaternion.identity);  // Instantiate Player 
        mainPlayer = GameObject.Find("TPP_Player").GetComponent<PlayerFunctionsController>();
        GameObject spawnpoint = mainPlayer.GetComponent<Teleporting>().FindClosestSpawnPoint();  //find spawnpoint
        //teleport Player
        mainPlayer.GetComponent<CharacterController>().enabled = false;
        mainPlayer.GetComponent<CharacterController>().transform.position = spawnpoint.transform.position;
        mainPlayer.GetComponent<CharacterController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
