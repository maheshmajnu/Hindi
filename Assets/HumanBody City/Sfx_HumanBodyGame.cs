using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sfx_HumanBodyGame : MonoBehaviour
{
    private string HumanObjective; //value will be coming from human body explanation module as static variable 

    [SerializeField]
    private string[] miniObjectives; private int array_i = 0;

    [Header("Gameplay-canvas")]
    public GameObject Objective_canvas; 
    public TextMeshProUGUI Objective;
    public TextMeshProUGUI Steps;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;  

    [Header("Gameplay assets")]
    private GameObject mainPlayer; 
    /* ------------- */

    [Header("Objective Diseases")]
    private GameObject Disease; 
    public GameObject HeartDisease;
    public GameObject KidneyDisease;

    public GameObject WaypointScript;
    public GameObject WaypointCanvas;

    [Header("Gameplay-Triggers")]
    //triggers and tablets
    //--heart
    public GameObject perindopril;
    public GameObject NearHeart;
    //--kidney
    public GameObject KidneyObj;
    public GameObject NearKidney;
    public Material newKidneyRef_mat;

    private int distance;
    private float qdistance=999f;
    private bool isExecutable = false;


    //qusetion
    public GameObject Q1;
    public GameObject Q2;
    public GameObject Q3;
    public GameObject Q4;
    public GameObject Q5;
    public GameObject Q1H;
    public GameObject Q2H;
    public GameObject Q3H;
    public GameObject Q4H;
    public GameObject Q5H;
    public GameObject redpanel;





    //---------------------------------------------------------------------------------//
    //=================  get DYNAMIC OBJECTIVE VIA staticVariable  =================//
    void Awake(){ 

        if(StaticVariables.Human_Objective!= null){
            HumanObjective = StaticVariables.Human_Objective; 
            if(HumanObjective != null) { 
             FindDisease(HumanObjective);
            } 
        } else { HumanObjective = "Heart";    
        } 

        mainPlayer = GameObject.Find("TPP_Player");  //Get TPP_Player 
    }


    private void Update()
    {
        if (isExecutable)
        {
            distance50q();
        }
        

    }




    //=================  CALLED RESPECTIVE DISEASE METHOD on awake()  =================//
    void FindDisease(string ProblemName){
        if (ProblemName == "Heart") { 
            Heart();
        }  else if (ProblemName == "Kidney"){
            Kidney();
        }
    }
    //---------------------------------------------------------------------------------//
    //================== UPDATE OBJECTIVES in DISEASE METHOD  =========================//

    void Heart(){
        //populate objective canvas text
        Objective.text = "Objective: Treat High Blood Pressure";
        miniObjectives = new string[] { "Select Proper Drug.","Reach The Heart.", "Shoot the Drug on Heart"};
        Steps.text = miniObjectives[array_i]; array_i++;
        
        //save respective disesae in GameObject to activate it later from trigger
        Disease = HeartDisease;
    }

    void Kidney(){
        //populate objective canvas text
        Objective.text = "Objective: Cure Kidney Stones";
        miniObjectives = new string[] { "Select Proper Drug.","Reach The Right Kidney.", "Shoot the Drug on Kidney"};
        Steps.text = miniObjectives[array_i]; array_i++;
        //save respective disesae in GameObject to activate it later from trigger
        Disease = KidneyDisease;
    }
 
    //---------------------------------------------------------------------------------//
    //========================== NEXT TRIGGER TOGGLES  ================================//

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {  


 /* HEART START */       
 if(HumanObjective == "Heart"){

     
            if (other.gameObject.name == "Heart") //heart-High BP Table trigger name is Heart
            {  
                // PickMed -movetospotCAM anim
                StartCoroutine(MoveToSpot(other.gameObject)); //Also this trigger wil be destroyed in this Func
                //next trigger
                NearHeart.SetActive(true);  
                //next objective
                NextObjective();  
            } else if(other.gameObject.name == "NearHeart")
            {
                Destroy(NearHeart);
                //next objective
                NextObjective(); 
            }
 }              
 /* HEART END */    
/* KIDNEY START */  
else if(HumanObjective == "Kidney"){

            if (other.gameObject.name == "Kidney") //heart-High BP Table trigger name is Heart
            {  
                // PickMed -movetospotCAM anim
                StartCoroutine(MoveToSpot(other.gameObject)); //Also this trigger wil be destroyed in this Func
                //next trigger
                NearKidney.SetActive(true);  
                //next objective
                NextObjective();  
            } else if(other.gameObject.name == "NearKidney")
            {
                KidneyObj.GetComponent<Renderer>().material = newKidneyRef_mat;
                Destroy(NearKidney);
                //next objective
                NextObjective(); 
            }    

}
/* KIDNEY END */  
        }
    }    

 
    //---------------------------------------------------------------------------------//
    //Picked-Up the Proper Medicine
    IEnumerator MoveToSpot( GameObject thisObj){ 

         Vector3 Gotoposition = Camera.main.transform.position;
         float elapsedTime = 0;
         float waitTime = 0.5f;
         Vector3 currentPos = thisObj.transform.position;
         while (elapsedTime < waitTime)
        {
            Gotoposition = Camera.main.transform.position; Gotoposition = (currentPos + Gotoposition)/2;
            thisObj.transform.position = Vector3.Lerp(currentPos, Gotoposition, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime; 
            // Yield here
            yield return null;
        }  
        // Activate Shootable Objective
         MakeItShootable();  
        // Make sure we got there
        thisObj.transform.position = Gotoposition;
        //make it child
         thisObj.transform.parent = Camera.main.transform;
         Destroy(thisObj,3f);

        yield return null;

    }

    //CALL this after Picking Medicine
    public void MakeItShootable(){
        isExecutable = true;
        //mission marker ON
        WaypointScript.GetComponent<MissionWaypoint>().target = Disease.transform;
        WaypointScript.GetComponent<MissionWaypoint>().enabled = true;
        WaypointCanvas.SetActive(true);

        //shootable Objective ON
        Disease.SetActive(true);
    }





    //---------------------------------------------------------------------------------// 
    void NextObjective()
    {
        //objective checkbox toggle
        GreenCheckBox.SetActive(true);
        Steps.color = Color.green;
        //reset trigger name
        mainPlayer.GetComponent<GameTriggersManager>()._triggername = "";
        //wait and replace with new objective
        Invoke("CallMeWithWait", 3f);
    }
    void CallMeWithWait()
    {
        Steps.text = miniObjectives[array_i];
        GreenCheckBox.SetActive(false);
        Steps.color = Color.white;
        array_i++;
    } 
    public void LastObjective()
    {
        WaypointScript.GetComponent<MissionWaypoint>().enabled = false;

        GreenCheckBox.SetActive(true);
        Steps.color = Color.green;
        Objective.color = Color.green;

        Invoke("missionPassed", 2f);
    }


    void missionPassed()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject missionCompletePrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/Mission Passed", typeof(GameObject));  // Load Player
        Instantiate(missionCompletePrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate mission-Complete canvas 
    }






    //distance50q
    void distance50q()
    {

        qdistance = WaypointScript.GetComponent<MissionWaypoint>().distance;
        Debug.Log("distance50q"+ qdistance + " obj:"+ HumanObjective);
        if (qdistance < 50f && qdistance != 0f  && HumanObjective == "Heart" )
        {
            isExecutable = false;
            //heartquestionspopupmethondhere
            HeartQUES();

        }
        else if (qdistance < 50f && qdistance != 0f  && HumanObjective == "Kidney" )
        {
            isExecutable = false;
            //kindeyquestionspopupmethondhere
            kindeyQUES();

        }

    }


    void kindeyQUES()
    {
        Q1.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    void HeartQUES()
    {
        Q1H.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void correctans_h1()
    {
        //popupquestionoff
        Q2H.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void correctans_h2()
    {
        //popupquestionoff
        Q3H.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void correctans_h3()
    {
        //popupquestionoff
        Q4H.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void correctans_h4()
    {
        //popupquestionoff
        Q5H.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void correctans_h5()
    {
        //popupquestionoff
        Q5H.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }


    public void correctans_k1()
    {
        //popupquestionoff
        Q2.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void correctans_k2()
    {
        //popupquestionoff
        Q3.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void correctans_k3()
    {
        //popupquestionoff
        Q4.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void correctans_k4()
    {
        //popupquestionoff
        Q5.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void correctans_k5()
    {
        //popupquestionoff
        Q5.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void wrongans()
    {


        StartCoroutine(TurnOnAndOff());

    }


    IEnumerator TurnOnAndOff()


    {
        // Turn on the GameObject       
        redpanel.SetActive(true);

        // Wait for 2 seconds     
        yield return new WaitForSeconds(0.8f);

        // Turn off the GameObject after 2 seconds    
        redpanel.SetActive(false);

    }



}
