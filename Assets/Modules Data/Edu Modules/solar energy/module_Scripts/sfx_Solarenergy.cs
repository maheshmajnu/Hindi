using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class sfx_Solarenergy : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Explanation assets")]
    public Camera ExpCam;

    // [Header("Particlesystems")]

    [Header("Audio files")]
    public AudioSource myAudio;
    public AudioClip game_bgm;

    public AudioClip Solarintro_audio;
    public AudioClip procedure_audio; 

    [Header("Exlanation Anims")]
    public GameObject Solarray1;
    public GameObject Solarray2;
    public GameObject Solarray3;
    public GameObject Clouds;
    public GameObject Houselamp; 

    [Header("Gameplay Anims")]
    public GameObject boat;
    public GameObject pole;
    public GameObject lever;
    public GameObject Sheet;
    private Animator anim;

    [Header("Gameplay assets")]
    private GameObject mainPlayer;
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public Camera MainCam;

    public GameObject NPC_Player; 

    public string trigname;
    public string Interactable_ColliderName;

    [Header("Gameplay-Triggers")]
    //triggers and objects
    public GameObject Motor_boat;
    public GameObject solarpanel;
    public GameObject solarpanel_boat;
    public GameObject Lever0_trig;
    public GameObject Lever1_trig;
    public GameObject Lever2_trig;
    public GameObject Lever3_trig;
    public GameObject DC1;
    public GameObject DC2;
    public GameObject DC3;
    public GameObject DC4;
    public GameObject DC5;
    public GameObject redpanel;
    public GameObject SQ1;
    public GameObject SQ2;
    public GameObject SQ3;

    // [Header("Lines&texts")]
    private string[] miniObjectives = { "Find the Motor.","Find the Solar Panel.", "Power Up The Boat", "Push Boat onto Sea" }; private int array_i = 1;

    [Header("Gameplay-canvas")]
    public GameObject Objective_canvas;
    public GameObject UI_assets;
    public TextMeshProUGUI Objective;
    public TextMeshProUGUI Steps;
    public TextMeshProUGUI color;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;


     void Update()
    {
        //===================================//
        /* Get Player Entered Trigger Names*/
        EnteredTriggerName();
        /* Gameplay triggers */
        DoTriggerActions(); 
        //===================================//
    }


    //animation Play

    void Audio_intro_method()
    {
        myAudio.clip = Solarintro_audio;
        myAudio.Play();
    }

    void _procedureaudio()
    {
        myAudio.clip = procedure_audio;
        myAudio.Play();
    }


    void Gameplay_Method()
    {
        pole.SetActive(true);
        lever.SetActive(true);
        Sheet.SetActive(true);
    }

    void Solarray_Method()
    {
        anim = Solarray1.GetComponent<Animator>();
        anim.Play("beam 1_Cylinder_001Action");

        anim = Solarray2.GetComponent<Animator>();
        anim.Play("beam 2_Cylinder_001Action_001");

        anim = Solarray3.GetComponent<Animator>();
        anim.Play("beam 3_Cylinder_001Action_002");
    }

    void Clouds_Method()
    {
        anim = Clouds.GetComponent<Animator>();
        anim.Play("Clouds");
    }

    void Houselamp_method()
    {
        Houselamp.SetActive(true);
    }

    public void _ResetnInitialize()
    {


        //=======stop ongoing explanatory audio=====//
        myAudio.Stop();
        myAudio.clip = game_bgm;
        myAudio.Play(); myAudio.loop = true;
        //==========================================//
        anim = ExpCam.GetComponent<Animator>();
        anim.Play("empty");

        Scene_Gameplay.SetActive(true);
        Scene_Explantion.SetActive(false);

        //==================== INSERT PLAYER  ====================//
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/PlayerPrefabs/Main/__Player", typeof(GameObject));  // Load Player
        Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate Player 
        mainPlayer = GameObject.Find("TPP_Player");  //Get TPP_Player
        GameObject spawnpoint = mainPlayer.GetComponent<Teleporting>().FindClosestSpawnPoint();  //find spawnpoint
        //teleport Player
        mainPlayer.GetComponent<CharacterController>().enabled = false;
        mainPlayer.GetComponent<CharacterController>().transform.position = spawnpoint.transform.position;
        mainPlayer.GetComponent<CharacterController>().enabled = true;
        //Diable Player Camera Auido Listerner
        GameObject.Find("MainCamera").GetComponent<AudioListener>().enabled = false;
        //========================================================//

        //==================== INSERT NPC  ====================//
        //NPC_Player.SetActive(true);
        //========================================================//

        //============= POPULATE OBJECTIVE CANVAS TEXT FOR GAMEPLAY ============//
        //Show objective canvas
        Objective_canvas.SetActive(true);


        //Objective = GameObject.Find("Main_Objective").GetComponent<TextMeshProUGUI>();
        Objective.text = "Objective: Escape The Island.";

        //Steps = GameObject.Find("Steps").GetComponent<TextMeshProUGUI>();
        Steps.text = miniObjectives[0];
        //==========================================================//
    }

    void EnteredTriggerName()
    {
        if (mainPlayer != null)
        {
            trigname = mainPlayer.GetComponent<GameTriggersManager>()._triggername;
        }
    }

    //call respective trigger actions based on trigger names
    void DoTriggerActions()
    {
        if (trigname == "MotorPickTrigger")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //Perform relevant action
                PanelMotor();
             
                //next objective
                NextObjective(); 
            }
        }
        else if (trigname == "SolarPickTrigger")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //Perform relevant action
                PanelPickUp();  
                //next objective
                NextObjective(); 
            }
        }
        else if (trigname == "SolarPlaceTrigger")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //Perform relevant action
                PlacePanel();
                //next objective
                NextObjective();
            }
        } else if (trigname == "PushBoatTrigger")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //Perform relevant action
                PushBoat();
                //next objective
                LastObjective();
            }
        }
    }



    void PanelMotor()
    {
       

        //Delete this trigger
        Destroy(Lever0_trig);
      
        //display the question
        DC1.SetActive(true);



       



    }

    void PanelPickUp()
    {
        //Delete this trigger
        Destroy(Lever1_trig);


        //display the question
        SQ1.SetActive(true);




    }
    void PlacePanel()
    {
        //Delete this trigger
        Destroy(Lever2_trig);

        //next objective
        NextObjective();



        //Activate next trigger
        Lever3_trig.SetActive(true);





    }
    void PushBoat()
    {
        //Delete this trigger
        Destroy(Lever3_trig);


        anim = boat.GetComponent<Animator>();
        anim.Play("boat_slide");


    }


    //buttons

    public void correctans_1DC()
    {  

        

        //display the question
        DC2.SetActive(true);


        

    }

    public void correctans_2DC()
    {

        //display the question
        DC3.SetActive(true);

    }

    public void correctans_3DC()
    {

        //display the question
        DC4.SetActive(true);


    }

    public void correctans_4DC()
    {
        //display the question
        DC5.SetActive(true);
    }

    public void correctans_5DC()
    {
        //Activate next trigger
        Lever1_trig.SetActive(true);

        //Pick (Hide) Motor
        Motor_boat.SetActive(false);

    }


    public void correctans_1S()
    {

        //display the question
        SQ2.SetActive(true);

    }

    public void correctans_2S()
    {

       

        //display the question
        SQ3.SetActive(true);

    }

    public void correctans_3S()
    {

        //Activate next trigger
        Lever2_trig.SetActive(true);

        //Pick (Hide) solarpanel
        solarpanel_boat.SetActive(true);


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


    void LastObjective()
    {
        GreenCheckBox.SetActive(true);
        Steps.color = Color.green;
        Objective.color = Color.green;

        Invoke("missionPassed", 2f);

    }

    void missionPassed()
    {
        GameObject missionCompletePrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/Mission Passed", typeof(GameObject));  // Load Player
        Instantiate(missionCompletePrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate mission-Complete canvas 
    }
}

  



