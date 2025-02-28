using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class sfx : MonoBehaviour
{
  
    private GameObject mainPlayer;
    public string trigname;
    public string Interactable_ColliderName;
    private string[] miniObjectives = { "Connect the Generator.","Align the Turbine.","Lift The Dam Gate" }; private int array_i=1;

    // Start is called before the first frame update
    [Header("Explanation anims")] 
    public GameObject SkipBtn;

    public GameObject Gateslider; 
    public GameObject Damwater;
    public GameObject Basewater;
    public GameObject shaft;
    public GameObject Turbinewheel;
    public GameObject fan;
    public GameObject Genhouse;


    [Header("Particlesystems")]
    public GameObject Groundwater_particle;
    public GameObject Verticalwater_particle;
    public GameObject Groundwater2_particle;

    [Header("Audio files")]
    public AudioSource myAudio;
    public AudioClip Hydrointro_audio;
    public AudioClip HydroDefinition_audio;
    public AudioClip Gateslider_waterflow_audio;
    public AudioClip Turbinewheel_audio;
    public AudioClip Shaft_audio;
    public AudioClip Gearbox_audio;
    public AudioClip Generator_audio;
    public AudioClip Substationtohouse_audio;
    public AudioClip Conclusion_audio;

    public AudioClip game_bgm;

    private Animator anim;

    [Header("Lines&texts")]
    public GameObject Line_turbine;
    public GameObject Line_Shaft;
    public GameObject Line_Gearbox;
    public GameObject Line_generator;

    [Header("Color Transparancy Props")]
    private float alpha; 
    private bool change1 = false;
    private bool change2 = false;

    [Header("Gameplay-canvas")]
    public GameObject Objective_canvas;
    public GameObject UI_assets;
    public TextMeshProUGUI Objective;
    public TextMeshProUGUI Steps;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;

    [Header("Gameplay anims")]
    public GameObject lever0;
    public GameObject lever1;
    public GameObject lever2;
    public GameObject Pointlight;

    [Header("Gameplay-Triggers")]
    public GameObject first_trig;
    public GameObject Lever1_trig;
    public GameObject Lever2_trig;

    //questions
    public GameObject QUES1;
    public GameObject QUES2;
    public GameObject QUES3;
    public GameObject QUES4;
    public GameObject redpanel;

    void Update()  
    {
        //===================================//
        /* Get Player Entered Trigger Names*/
        EnteredTriggerName();
        /* Gameplay triggers */
        DoTriggerActions();
        /* Get interactable Collider Name */
        //HoverdColliderName();
        //===================================//


        Room1ToAlpha(change1);
        Room2ToAlpha(change2);
    }



    void Room1ToAlpha(bool change1)
    {
        if (change1)
        {
            Renderer renderer = Genhouse.GetComponent<Renderer>();
            alpha = renderer.material.color.a;
            if (alpha > 0.1f)
            {
                alpha -= 1f * Time.deltaTime;
                print(alpha);
                Color newColor = new Color(1, 1, 1, alpha);
                renderer.material.color = newColor;
            }
            change1 = false;
        } 
    }
    void Room2ToAlpha(bool change2)
    {
        if (change2)
        {
            Renderer renderer = Genhouse.GetComponent<Renderer>();
            alpha = renderer.material.color.a;
            if (alpha > 0.1f)
            {
                alpha -= 1f * Time.deltaTime;
                print(alpha);
                Color newColor = new Color(1, 1, 1, alpha);
                renderer.material.color = newColor;
            }
            change2 = false;
        }
    }





    //animation Play
    void _GatesliderMethod()
    {
        anim = Gateslider.GetComponent<Animator>();
        anim.Play("gate slider_gate sliderAction");
    }

    void _DamwaterMethod()
    {
        //animation
        anim = Damwater.GetComponent<Animator>();
        anim.Play("Scene");
    } 
  
        void _BasewaterMethod()
    {
        anim = Basewater.GetComponent<Animator>();
        anim.Play("base water _base water Action");
    }

    public void _TurbinewheelMethod()
    {
        //animating wheel
        anim = Turbinewheel.GetComponent<Animator>();
        anim.Play("wheel_wheel");


        //particlesystem
        Groundwater_particle.SetActive(true);
        Verticalwater_particle.SetActive(true);
        Groundwater2_particle.SetActive(true);

        //animating lable 
        Line_turbine.SetActive(true); 
    }

    void _ShaftMethod()
    {
        //animating shaft
        anim = shaft.GetComponent<Animator>();
        anim.Play("shaft_shaft");

        //animating label
        Line_Shaft.SetActive(true);

    }

    void _FanMethod()
    {
        anim = fan.GetComponent<Animator>();
        anim.Play("fan_fan");
    }

    void Hydrointro_method()
    {
        myAudio.clip=Hydrointro_audio;
        myAudio.Play();
    }

    void Definition_method()
    {
        myAudio.clip = HydroDefinition_audio;
        myAudio.Play();
    }


    void Gateslider_waterflow_method()
    {
        myAudio.clip = Gateslider_waterflow_audio;
        myAudio.Play();

    }

    void Turbinewheel_method()
    {
        myAudio.clip = Turbinewheel_audio;
        myAudio.Play();
    }

    void Shaft_method()
    {
        
        myAudio.clip = Shaft_audio;
        myAudio.Play();
    }

    void Gearbox_method()
    {
        //Audio clip
        myAudio.clip = Gearbox_audio;
        myAudio.Play();

    }

    void Generator_method()
    {
        //Audio clip
        myAudio.clip = Generator_audio;
        myAudio.Play();

    }

    void Substationtohouse_method()
    {
        myAudio.clip = Substationtohouse_audio;
        myAudio.Play();
    }

    void Conclusion_method()
    {
        myAudio.clip = Conclusion_audio;
        myAudio.Play();
    }

    void _LineGearboxmethod()
    {
        //Animating label
        Line_Gearbox.SetActive(true);
    }

    void _LineGeneratormethod()
    {
        //Animating label
        Line_generator.SetActive(true);
    }

    void generatorhouse_transparent()
    {
        change1 = true; 

        /*
        Color c = new Color(0f, 0f, 0f, 0.2f);
        // Genhouse.GetComponent<>.material.color.a = 0.5f;
        Renderer renderer = Genhouse.GetComponent<Renderer>(); 
        renderer.material.color = c;  
        */
    }


    //Reset And Initialize
    public void Reset_n_Initialize()
    {
        //======Stop ongoing explanatory audio======//
        //myAudio.Stop();
        myAudio.clip = game_bgm;
        myAudio.Play(); myAudio.loop = true;
        //==========================================//
        anim = gameObject.GetComponent<Animator>();
        anim.Play("empty");
        SkipBtn.SetActive(false);

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



        //==================== RESET Module For GAMEPLAY  ====================// 
        //Lables Reset 
        UI_assets.SetActive(false);
        //Turbine animation-Reset
        anim = Turbinewheel.GetComponent<Animator>();
        anim.Play("wheel_wheel gameplay");
        //Damwater animation-Reset
        anim = Damwater.GetComponent<Animator>();
        anim.Play("empty");
        //Fan animation-Reset
        anim = fan.GetComponent<Animator>();
        anim.Play("empty");
        //particlesystem-Reset
        Groundwater_particle.SetActive(false);
        Verticalwater_particle.SetActive(false);
        Groundwater2_particle.SetActive(false);
        //Gateslider-Reset
        anim = Gateslider.GetComponent<Animator>();
        anim.Play("empty");
        //=========================================================//


        //============= POPULATE OBJECTIVE CANVAS TEXT FOR GAMEPLAY ============//
        //Show objective canvas
        Objective_canvas.SetActive(true);
        //Objective = GameObject.Find("Main_Objective").GetComponent<TextMeshProUGUI>();
        Objective.text = "Objective: Light up the Building Using Water Energy.";

        //Steps = GameObject.Find("Steps").GetComponent<TextMeshProUGUI>();
        Steps.text = miniObjectives[0]; 
        //==========================================================//
    }














    //get Collider name that player Hovered
    void HoverdColliderName()
    {
        if (mainPlayer != null)
        {
            GameObject colliderObj = mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj;
            if (colliderObj != null)
            {
                Interactable_ColliderName = mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj.name;
                if (Interactable_ColliderName != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        DoColliderAction();
                    }
                }
            }   
        }
    }

    //call respective collider actions based on collider names
    void DoColliderAction()
    {
        Debug.Log("collider:"+ Interactable_ColliderName + "clicked do something");
    }

    //get trigger name that player entered
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
        if (trigname == "Firsttrigger")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //POPUPQUESTIONPANEL
                QUES1.SetActive(true);

                
            }
        }
        else if (trigname == "levertrigger1")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //POPUPQUESTIONPANEL
                QUES2.SetActive(true);

                //next objective
                NextObjective();
                
            }

        }
        else if (trigname == "levertrigger2")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {

                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //POPUPQUESTIONPANEL
                QUES3.SetActive(true);

                
            }
        } 
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
        Cursor.visible = true;
        GameObject missionCompletePrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/Mission Passed", typeof(GameObject));  // Load Player
        Instantiate(missionCompletePrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate mission-Complete canvas 
    }


    //buttons
    public void correctans_1()
    {


        //turn ON generator 
        anim = lever0.GetComponent<Animator>();
        anim.Play("leaver_leaverAction");
        //activate next trigger
        Destroy(first_trig);
        Lever1_trig.SetActive(true);
        //next objective
        NextObjective();

    }

    public void correctans_2()
    {


        //turn ON generator 
        anim = lever0.GetComponent<Animator>();
        anim.Play("leaver_leaverAction");
        //activate next trigger
        Destroy(first_trig);
        Lever1_trig.SetActive(false);
        Lever2_trig.SetActive(true);
        //next objective
        NextObjective();

    }

    public void correctans_3()
    {
        //POPUPQUESTIONPANEL
        QUES4.SetActive(true);

        //Delete this trigger
        Destroy(Lever2_trig);

    }

    public void correctans_4()
    {


        //Perform relevant animation
        Debug.Log("leaver_leaverAction");
        anim = lever2.GetComponent<Animator>();
        anim.Play("leaver_leaverAction");
        _GatesliderMethod();
        _DamwaterMethod();
        _FanMethod();
        Pointlight.SetActive(true);

        //Delete this trigger
        Destroy(Lever2_trig);

        //next objective
        LastObjective();

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
