using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleporting : MonoBehaviour
{
    //Portal Props
    public GameObject closestObj;
    private string PortalName, Portal_EndPoint;
    //Train Props
    private GameObject mainTrain;
    [SerializeField]
    private GameObject[] trainstops;
    private GameObject TrainCanvas;
    private GameObject TrainInsideTrigger;
    [SerializeField]
    private GameObject TrainCam;


    private void Awake()
    {
        //Get Main Train
        mainTrain = GameObject.FindGameObjectWithTag("MainTrain");
        //Get Trigger inside Train
        TrainInsideTrigger = GameObject.FindGameObjectWithTag("TrainStation-Central");
        //Get Train Stops
        trainstops = GameObject.FindGameObjectsWithTag("TrainStop");
        //Deactivate Train Stops
        deactivateTrainStops();
    }

    // Start is called before the first frame update
    void Start()
    {
        closestObj = FindClosestSpawnPoint();

        TrainCanvas = GameObject.Find("Canvas_Station_selector");
        if(TrainCanvas != null) { TrainCanvas.SetActive(false);  }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //on exit from chapter selection menu
    public GameObject FindClosestSpawnPoint()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("spawnpoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // find closest train spawn point from player
    public GameObject FindClosestTrainSpawnPoint()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("TrainSpawnPoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }



    //Teleportation
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Portal")
        {
            PortalName = other.name;
            Portal_EndPoint = PortalName + "_Endpoint";
            PortalTravel(Portal_EndPoint);

        }
        //Train Station Triggers
        else if (other.tag == "TrainStart")
        {
            //spawn train on nearest point
            if (mainTrain != null)
            {
                closestObj = FindClosestTrainSpawnPoint();
                mainTrain.GetComponent<CharacterController>().enabled = false; mainTrain.SetActive(false);
                mainTrain.GetComponent<CharacterController>().transform.position = closestObj.transform.position;
                mainTrain.GetComponent<CharacterController>().enabled = true;
                //HaltTrain
                TrainTowerToChemistryDirection();
                HaltTrain();
            }


        }
        else if (other.tag == "TrainStation-Chem")
        {
            //spawn train on nearest point
            if (mainTrain != null)
            {
                closestObj = FindClosestTrainSpawnPoint(); 
                mainTrain.GetComponent<CharacterController>().enabled = false; mainTrain.SetActive(false);
                mainTrain.GetComponent<CharacterController>().transform.position = closestObj.transform.position;
                mainTrain.GetComponent<CharacterController>().enabled = true;

                //target next station direction for train alignment
                GotoChemistryBuilding();
                HaltTrain();
            }

        }
        else if (other.tag == "TrainStation-Bio")
        {
            //spawn train on nearest point
            if (mainTrain != null)
            {
                closestObj = FindClosestTrainSpawnPoint(); 
                mainTrain.GetComponent<CharacterController>().enabled = false; mainTrain.SetActive(false);
                mainTrain.GetComponent<CharacterController>().transform.position = closestObj.transform.position;
                mainTrain.GetComponent<CharacterController>().enabled = true;
                //target next station direction for train alignment
                GotoBiologyBuilding();
                HaltTrain();
            }

        }
        else if (other.tag == "TrainStation-Phy")
        {
            //spawn train on nearest point
            if (mainTrain != null)
            {
                closestObj = FindClosestTrainSpawnPoint(); 
                mainTrain.GetComponent<CharacterController>().enabled = false; mainTrain.SetActive(false);
                mainTrain.GetComponent<CharacterController>().transform.position = closestObj.transform.position;
                mainTrain.GetComponent<CharacterController>().enabled = true;
                //target next station direction for train alignment
                GotoPhysicsBuilding();
                HaltTrain();
            }

        }

        else if (other.tag == "TrainStation-Central") {

            if (TrainCanvas != null) {
                //Set Cursor to  be visible
                Cursor.visible = true;

                TrainCanvas.SetActive(true);
               // other.gameObject.SetActive(false); //hide this trigger inside train
            }
                
        }

        else if (other.tag == "Train Station Exit")
        {

            if (TrainCanvas != null)
            {
                //Show this trigger inside train = station canvas set active true
                TrainInsideTrigger.SetActive(true);
                 
            }

        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "TrainStation-Central")
        {
            if (TrainCanvas != null)
            {
                //Set Cursor not to be visible
                Cursor.visible = false;

                TrainCanvas.SetActive(false);
            } 
        }
    }


        public void GotoChemistryBuilding()
    {
        if (mainTrain != null)
        {
            mainTrain.GetComponent<TrainWayPointManager>().i = 0;
            mainTrain.GetComponent<TrainWayPointManager>().UpdateNavTargetStation();
            SetTrainSpeed();
        }

    }
    public void GotoBiologyBuilding()
    {

        if (mainTrain != null)
        {
            mainTrain.GetComponent<TrainWayPointManager>().i = 1;
            mainTrain.GetComponent<TrainWayPointManager>().UpdateNavTargetStation();
            SetTrainSpeed();
        }
    }
    public void GotoPhysicsBuilding()
    {

        if (mainTrain != null)
        {
            mainTrain.GetComponent<TrainWayPointManager>().i = 2;
            mainTrain.GetComponent<TrainWayPointManager>().UpdateNavTargetStation();
            SetTrainSpeed();
        }
    }

    public void TrainTowerToChemistryDirection() {
        if (mainTrain != null)
        {
            mainTrain.GetComponent<TrainWayPointManager>().i = 3;
            mainTrain.GetComponent<TrainWayPointManager>().UpdateNavTargetStation();
            SetTrainSpeed();
        }
    }

    public void SetTrainSpeed()
    {
        // deactivate all trainstops
        deactivateTrainStops();
        //add speed to train
        NavMeshAgent navMeshAgent = mainTrain.GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 5f;
        //Train canvas hide
        TrainCanvas.SetActive(false);  
    }

    void deactivateTrainStops()
    {
        // deactivate all trainstops
        foreach (GameObject Tstop in trainstops)
        {
            Tstop.SetActive(false);
        }
    }
    void HaltTrain()
    {
        mainTrain.SetActive(true);
        //activate All train stops 
        foreach (GameObject Tstop in trainstops)
        {
            Tstop.SetActive(true);
        }

    }

    //Portal travel - travel to gameobject name with suffix "_Endpoint"
    void PortalTravel(string travelpoint_name)
    {
        GameObject travelpoint = GameObject.Find(travelpoint_name);
        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.GetComponent<CharacterController>().transform.position = travelpoint.transform.position;
        gameObject.GetComponent<CharacterController>().enabled = true;
    }


    public void TrainCamEnable()
    {
        SetTrainSpeed();
        //train inside trigger off
        TrainInsideTrigger.SetActive(false); 
        //Train Cam ON
        TrainCam.SetActive(true);
        //player movement disable
        gameObject.GetComponent<PlayerMove>().enabled = false;
    }


 


}
