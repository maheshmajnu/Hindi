using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LabRoomManager : MonoBehaviour
{
    public GameObject ChapterManager;
    public GameObject _mainCam;
    public GameObject WCanvas;
    public GameObject WCanvas_slide_indicators;

    public GameObject[] Chapterprefabs; 

    public string LabName;

//OneTouch Buildings
    private Camera MainCam;
    private GameObject mainPlayer;
    private GameObject colliderObj;
    private GameObject OneTouchMenu; 
    private GameObject RecentHitCollider = null;
    private float clickTime = 0f; private float delay = 0.3f; 

    private void Start()
    {
        MainCam = Camera.main;
        mainPlayer = GameObject.Find("TPP_Player");  //Get TPP_Player
        OneTouchMenu = GameObject.Find("OneTouchMenu");  //Get OneTouchMenu
    }

    private void Update()
    {
        HighlightOneTouchBuildings_OnCrosshair();
         /*
        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        */

        DoColliderAction(); // action 1 -- for PC check if mouse click 

        //on mouse click 
        if (Input.GetMouseButtonDown(0))
        {

            

            //action 2  ---   for mobile touch check if touched  
            /*
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider);
               if (hit.collider != null && hit.collider.name == "Biology-OneTouch")
                {
                    Debug.Log("Biology-OneTouch");
                }
               else if (hit.collider != null && hit.collider.name == "Physics-OneTouch")
                {
                    Debug.Log("Physics-OneTouch");
                }
                else if (hit.collider != null && hit.collider.name == "Chemistry-OneTouch")
                {
                    Debug.Log("Chemistry-OneTouch");
                }
            }
            */
        }
    }

    void HighlightOneTouchBuildings_OnCrosshair(){

        colliderObj = mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj;
        if(colliderObj != RecentHitCollider) { 

            if (colliderObj != null && (colliderObj.name == "Biology-OneTouch" || colliderObj.name == "Chemistry-OneTouch" || colliderObj.name == "Physics-OneTouch" || colliderObj.name == "Center-OneTouch" ))
            { 
              
              if(RecentHitCollider != null){  RecentHitCollider.GetComponent<Outline>().ResetOutlineColor();} //hovered on objs consecutively

                RecentHitCollider = colliderObj;
                colliderObj.GetComponent<Outline>().ChangeOutlineColor();
            } 
            else 
            { 
               if(RecentHitCollider != null){ 
                RecentHitCollider.GetComponent<Outline>().ResetOutlineColor();
                RecentHitCollider = null;
                } 
            } 

        }

    }

    void DoColliderAction()
    {
        colliderObj = mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj;
        if (colliderObj != null && colliderObj.name == "Biology-OneTouch")
        {
            if(Input.GetMouseButtonDown(0)){ clickTime = Time.time;}
            if (Input.GetMouseButtonUp(0)){ if(Time.time - clickTime <= delay){
                OneTouchMenu.SetActive(false);
                GotoLabView("Lab3"); }
            }
        } 
        else if (colliderObj != null && colliderObj.name == "Physics-OneTouch")
        {
            if(Input.GetMouseButtonDown(0)){ clickTime = Time.time;}
            if (Input.GetMouseButtonUp(0)){ if(Time.time - clickTime <= delay){
                OneTouchMenu.SetActive(false);
                GotoLabView("Lab1"); }
            }
        }
        else if (colliderObj != null && colliderObj.name == "Chemistry-OneTouch")
        {
            if(Input.GetMouseButtonDown(0)){ clickTime = Time.time;}
            if (Input.GetMouseButtonUp(0)){ if(Time.time - clickTime <= delay){
                OneTouchMenu.SetActive(false);
                GotoLabView("Lab2"); }
            }
        } 
        else if (colliderObj != null && colliderObj.name == "Center-OneTouch")
        {
            if(Input.GetMouseButtonDown(0)){ clickTime = Time.time;}
            if (Input.GetMouseButtonUp(0)){ if(Time.time - clickTime <= delay){
                OneTouchMenu.SetActive(false);
                }
            }
        }
    }


        #region On LAB trigger Enter
        public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "labs")
        {   
            LabName = other.gameObject.name; 
            GotoLabView(LabName); 
        }
    }
    #endregion



    #region Open/Call Respective LAB's LabRooms-Chapter Methods
    void GotoLabView(string LabName)
    {
        PlayerOnScreenControlUI_off();

        Debug.Log("Called Method for "+LabName);
        if (LabName == "Lab1")
        {
            //load Lab1 resourece slider
            //PhysicsLab();
            PhyAllClasses();
        }
        else if (LabName == "Lab2")
        {
            //load Lab2 resourece slider
            //ChemistryLab();
            PhyAllClasses();
        }
        else if (LabName == "Lab3")
        {
            //load Lab3 resourece slider
            BiologyLab();
        } 
        else if (LabName == "Lab-Sea")
        {
            //load Lab - Sea resourece slider
            SeaLab();
        }
        else if (LabName == "Lab-Space")
        {
            //load Lab - Sea resourece slider
            SpaceLab();
        }
        else if (LabName == "Lab-Human")
        {
            //load Lab - Sea resourece slider
            HumanLab();
        }

    }
    #endregion


    #region Player Teleport ToNearest Spawn When he enters Lab 
    void PlayerTeleportToNearestSpawn()
    {
        Debug.Log("Player Teleported To Nearest Spawn");
        GameObject spawnpoint = gameObject.GetComponent<Teleporting>().FindClosestSpawnPoint();
        //GameObject spawnpoint = gameObject.GetComponent<Teleporting>().closestObj;
        Debug.Log(spawnpoint.name);



        Debug.Log("Chapter-Obj activated");
        ChapterManager.SetActive(true);
        _mainCam.SetActive(false);
        Cursor.visible = true;

        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.GetComponent<CharacterController>().transform.position = spawnpoint.transform.position;
        gameObject.GetComponent<CharacterController>().enabled = true; 
    }
    #endregion 



    #region Exit LabRoom
    public void ExitLabView()
    {
        PlayerOnScreenControlUI_on();

        Cursor.visible = false;
        _mainCam.SetActive(true);
        ChapterManager.SetActive(false);
    }
    #endregion


    #region Clear Prev LabRooms-Chapter UIs
    public bool ClearChaptersUI()
    {
        //Clear existing chapters  
        bool Case = false;
        Debug.Log("Clear existing Chapter UI's");
        int prevchildcount = WCanvas.transform.childCount - 1;
        Debug.Log("prevchildcount:" + prevchildcount);
        for (int i = 0; i <= prevchildcount; i++)
        {
            GameObject.Destroy(WCanvas.transform.GetChild(i).gameObject);
            Debug.Log("deleted " + WCanvas.transform.GetChild(i).gameObject);
            if (i == prevchildcount)
            {
                WCanvas.transform.DetachChildren();
                Debug.Log("count after deleting: " + WCanvas.transform.childCount);
                Case = true;
            }
        }
        return Case;
        
    }
    #endregion

    void ResetNewChapIndicators()
    {
        WCanvas_slide_indicators.GetComponent<ChapterSwipe>().EnableNewIndicators();
    }


    #region All LabRooms-Class Grade-Wise Methods HERE

    void PhyAllClasses()
    {
        if (ClearChaptersUI()) {
            //Start Instantize New Class-wise UI's
            Debug.Log("Start Instantize New class-wise UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/Grade-Wise/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load Lab1 resource slider 
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set WCanvas as parent for this resource
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }
            PlayerTeleportToNearestSpawn(); 
        } 
    }
    void ChemAllClasses()
    {

    } 
    #endregion

    #region All LabRooms-Chapter Methods HERE
    public void PhysicsLab()
    {
        ChapterManager.SetActive(false);
        //if prev chapter UI cleared successfully
        if (ClearChaptersUI())
        {
            //Start Instantize New Chapter UI's
            Debug.Log("Start Instantize New Chapter UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/PhysicsLab/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load Lab1 resource slider 
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set WCanvas as parent for this resource
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }

            //Both for each executions are complete now call PlayerTeleportToNearestSpawn()  
            PlayerTeleportToNearestSpawn(); 
        }   
        /*
        //get selected prefab from dropdown 
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("prefabs/Button", typeof(GameObject));
        //load Lab1 resourece slider 
        GameObject myparent = Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);
        //set emptyparent as parent for this newRoadChunk
        myparent.transform.SetParent(WCanvas.transform, false); 
        */
    }


    public void ExclusiveClassChapterLab()
    {
        ChapterManager.SetActive(false);
        //if prev chapter UI cleared successfully
        if (ClearChaptersUI())
        {
            //Start Instantize New Chapter UI's
            Debug.Log("Start Instantize New Chapter UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/PhysicsLab/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load PhysicsLab resource slider
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set WCanvas as parent for this resource
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }

            //Both for each executions are complete now call PlayerTeleportToNearestSpawn()  
            PlayerTeleportToNearestSpawn();
        }
    }


    public void SixClassChapterLab()
    {
        ChapterManager.SetActive(false);
        //if prev chapter UI cleared successfully
        if (ClearChaptersUI())
        {
            //Start Instantize New Chapter UI's
            Debug.Log("Start Instantize New Chapter UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/6Class/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load Lab1 resource slider 
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set WCanvas as parent for this resource
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }

            //Both for each executions are complete now call PlayerTeleportToNearestSpawn()  
            PlayerTeleportToNearestSpawn();
        } 
    }

    public void SevenClassChapterLab()
    {
        ChapterManager.SetActive(false);
        //if prev chapter UI cleared successfully
        if (ClearChaptersUI())
        {
            //Start Instantize New Chapter UI's
            Debug.Log("Start Instantize New Chapter UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/7Class/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load Lab1 resource slider 
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set WCanvas as parent for this resource
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }

            //Both for each executions are complete now call PlayerTeleportToNearestSpawn()  
            PlayerTeleportToNearestSpawn();
        } 
    }
    public void EightClassChapterLab()
    {
        ChapterManager.SetActive(false);
        //if prev chapter UI cleared successfully
        if (ClearChaptersUI())
        {
            //Start Instantize New Chapter UI's
            Debug.Log("Start Instantize New Chapter UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/8Class/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load Lab1 resource slider 
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set WCanvas as parent for this resource
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }

            //Both for each executions are complete now call PlayerTeleportToNearestSpawn()  
            PlayerTeleportToNearestSpawn();
        }
    }

    public void _9ClassChapterLab()
    {
        ChapterManager.SetActive(false);
        //if prev chapter UI cleared successfully
        if (ClearChaptersUI())
        {
            //Start Instantize New Chapter UI's
            Debug.Log("Start Instantize New Chapter UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/9Class/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load Lab1 resource slider 
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set WCanvas as parent for this resource
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }

            //Both for each executions are complete now call PlayerTeleportToNearestSpawn()  
            PlayerTeleportToNearestSpawn();
        }
    }

    public void _10ClassChapterLab()
    {
        ChapterManager.SetActive(false);
        //if prev chapter UI cleared successfully
        if (ClearChaptersUI())
        {
            //Start Instantize New Chapter UI's
            Debug.Log("Start Instantize New Chapter UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/10Class/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load Lab1 resource slider 
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set WCanvas as parent for this resource
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }

            //Both for each executions are complete now call PlayerTeleportToNearestSpawn()  
            PlayerTeleportToNearestSpawn();
        }
    }







    public void ChemistryLab()
    {
        //if prev chapter UI cleared successfully
        if (ClearChaptersUI())
        {
            //Start Instantize New Chapter UI's
            Debug.Log("Start Instantize New Chapter UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/ChemistryLab/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load Lab1 resourece slider 
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set emptyparent as parent for this newRoadChunk
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }

            //Both for each executions are complete now call PlayerTeleportToNearestSpawn()  
            PlayerTeleportToNearestSpawn(); 
        } 
    }

    public void BiologyLab()
    {
        //if prev chapter UI cleared successfully
        if (ClearChaptersUI())
        {
            //Start Instantize New Chapter UI's
            Debug.Log("Start Instantize New Chapter UI's");
            Chapterprefabs = Resources.LoadAll<GameObject>("Labs/BiologyLab/Prefabs");
            foreach (GameObject ChapEle in Chapterprefabs)
            {
                //load Lab1 resourece slider 
                GameObject myChapParent = Instantiate(ChapEle, new Vector3(0, 0, 0), Quaternion.identity);
                //set emptyparent as parent for this newRoadChunk
                myChapParent.transform.SetParent(WCanvas.transform, false);
                Debug.Log("instantiated" + ChapEle);
            }

            //Both for each executions are complete now call PlayerTeleportToNearestSpawn()  
            PlayerTeleportToNearestSpawn();
            
        } 
    }




    void SeaLab()
    {
        //save prefabClasspath to static variable 
        //StaticVariables.Chapter_Filepath = "SceneRooms/SeaLife/_Main Variant"; 
        //open Room scene
        SceneManager.LoadScene("SeaLife");
    }

    void SpaceLab()
    {
        //save prefabClasspath to static variable 
        //StaticVariables.Chapter_Filepath = "SceneRooms/SeaLife/_Main Variant"; 
        //open Room scene
        SceneManager.LoadScene("SpaceLife");
    }

    void HumanLab()
    {
        //save prefabClasspath to static variable 
        //StaticVariables.Chapter_Filepath = "SceneRooms/SeaLife/_Main Variant"; 
        //open Room scene
        SceneManager.LoadScene("HumanBody");
    }
    #endregion



    void PlayerOnScreenControlUI_off()
    {
        GameObject.Find("Crosshair-Canvas").GetComponent<Canvas>().enabled = false;
        var item = GameObject.Find("Mobile controls UI Canvas");
        if (item != null)
        {
            item.GetComponent<Canvas>().enabled = false;
        }
    }
    void PlayerOnScreenControlUI_on()
    {
        GameObject.Find("Crosshair-Canvas").GetComponent<Canvas>().enabled = true;
        var item = GameObject.Find("Mobile controls UI Canvas");
        if (item != null)
        {
            item.GetComponent<Canvas>().enabled = true;
        }
    }

}
