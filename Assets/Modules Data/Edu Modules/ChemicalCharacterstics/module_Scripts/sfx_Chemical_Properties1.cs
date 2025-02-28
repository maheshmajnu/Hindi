using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.EventSystems;

public class sfx_Chemical_Properties1 : MonoBehaviour
{

    [Header("Explanation assets")]
    public Camera ExpCam;


    [Header("Explanation anims")]
    private Animator anim;

    public GameObject Shelf;

    public GameObject Beaker_1;
    public GameObject Beaker_2;
    public GameObject Beaker_3;
    public GameObject Beaker_4;
    public GameObject Bunsun_Burner_1;
    public GameObject Bunsun_Burner_2;
    public GameObject Camphor;
    public GameObject Clamp;
    public GameObject Clamp_2;
    public GameObject Clamp_Stand;
    public GameObject Clamp_Stand_2;
    public GameObject Grill;
    public GameObject Halogram_Wings;
    public GameObject Halogram_Rays;
    public GameObject HCL;
    public GameObject Heat_Proof_Mat;
    public GameObject Ice_Cubes;
    public GameObject Lime_Water;
    public GameObject Lime_Water_2;
    public GameObject Paper;
    public GameObject Paper2;
    public GameObject Test_Tube_1;
    public GameObject Test_Tube_2;
    public GameObject Test_Tube_3;
    public GameObject Tripod;
    public GameObject Water;
    public GameObject hcleff;

    [Header("Color Change")]
    public GameObject CopperCarbonate;
    public GameObject Feso;


    [Header("Model On/Off")] 
    public GameObject Lime_Paste;
    public GameObject Lime_Stone;
    public GameObject Cube_Mask;
    public GameObject FeO;
    public GameObject CaO;
    public GameObject CuCO3;
    public GameObject FeSO4;
    public GameObject Na2SO3;

    public GameObject Experiments_canvas;


    [Header("Particle System")]

    public GameObject AfterBurner;
    public GameObject AfterBurner_2;
    public GameObject Smoke;
    public GameObject Waterfall;
    public GameObject Bubbles;
    public GameObject Explosion;
    public GameObject Sparkles;

    [Header("Explantion Button Touch System")]
    GameObject hitObj;

    [Header("Audio files")]
    private GameObject _mp3;
    public GameObject mp3_state;
    public GameObject mp3_temperature;
    public GameObject mp3_color;
    public GameObject mp3_gas;


    public AudioSource myAudio;
    public AudioClip game_bgm;

    public AudioClip C_title;
    public AudioClip C_exp;
    public AudioClip C_eg1_title;
    public AudioClip C_eg1_exp1;
    public AudioClip C_eg1_exp2;
    public AudioClip C_eg1_equation;
    public AudioClip C_eg2_title;
    public AudioClip C_eg2_exp1;
    public AudioClip C_eg2_exp2;
    public AudioClip C_eg2_exp3;
    public AudioClip C_eg2_equation;   
 
    public AudioClip S_exp;
    public AudioClip S_conclusion; 
    public AudioClip S_eg1_exp1;
    public AudioClip S_eg1_exp2;
    public AudioClip S_eg1_exp3;
    public AudioClip S_eg1_exp4; 
    public AudioClip S_eg2_title;
    public AudioClip S_eg2_exp1;
    public AudioClip S_eg2_exp2; 

    public AudioClip T_title;
    public AudioClip T_exp;
    public AudioClip T_eg1_equation; 
    public AudioClip T_eg1_exp1;
    public AudioClip T_eg1_exp2;
    public AudioClip T_eg1_exp3;    

    public AudioClip G_title;
    public AudioClip G_exp;
    public AudioClip G_eg1_equation; 
    public AudioClip G_eg1_exp1;
    public AudioClip G_eg1_exp2;
    public AudioClip G_eg1_exp3;  
    public AudioClip G_eg1_exp4;  


    [Header("Gameplay animations")]
    public GameObject hcl_barrel;
    public GameObject naoh_solidWall;
    public GameObject NaohMeltWater;
    public GameObject NaohVCAM;

    public GameObject QuickLime;
    public GameObject FeS;
    public GameObject Wolf;
    public GameObject WolfVCAM;
    public GameObject WolfWall;
    public GameObject Smell_FX;
    public GameObject Wolf_away;
    public GameObject RedFlameFX;

    public GameObject StrontiumNitrate; 

    [Header("Gameplay assets")]
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public Camera MainCam;
    private GameObject mainPlayer; 

    public string trigname;
    public string Interactable_ColliderName;
    public GameObject colliderObj = null;
    private bool DoingColliderAct = true;

    [Header("Gameplay-Triggers")]
    //triggers and objects
    public GameObject questionareatrigger;
    public GameObject Lever0_trig;
    public GameObject Lever1_trig;
    public GameObject Lever2_trig;
    public GameObject Lever3_trig;
    public GameObject Lever4_trig;
    public GameObject Lever5_trig;
    public GameObject Lever6_trig;
    public GameObject Lever7_trig;
    private bool trigger4_entered = false;
    private bool SrNO3_picked = false;
    public GameObject Q1;
    public GameObject Q2;
    public GameObject Q3;
    public GameObject Q4;
    public GameObject redpanel;


    // [Header("Lines&texts")]
    private string[] miniObjectives = { "कोई रास्ता ढूंढो", "जीवित रहने के लिए वार्मअप - क्विक लाइम ढूंढें और उठाएं(1/2)", "जीवित रहने के लिए वार्मअप - गर्मी पैदा करने के लिए पानी में क्विक लाइम मिलाएं (2/2)", "क्षेत्र का अन्वेषण करें, उपयोगी रसायन खोजें", "क्षेत्र का अन्वेषण करें", "दुर्गंध पैदा करने और जानवर को डराने के लिए एचसीएल के साथ FeS मिलाएं", "लाल ज्वाला पैदा करने और द्वीप से भागने के लिए स्ट्रोंटियम नाइट्रेट का उपयोग करें" }; private int array_i = 1;

    [Header("Gameplay-canvas")]
    public GameObject Objective_canvas;
    public GameObject UI_assets;
    public TextMeshProUGUI Objective;
    public TextMeshProUGUI Steps;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;

    public Material redMaterialRef;
    private Material defaultMaterial;
    public Material error_RedMaterial;

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void _StartCam_Evolution_of_gas()
        {
            mp3_gas.SetActive(true);
            _mp3 = mp3_gas;
            anim = gameObject.GetComponent<Animator>();
            anim.Play("Camera_Evolution Of Gas");
        }
        public void _StartCam_Change_in_Temp()
        {
            mp3_temperature.SetActive(true);
            _mp3 = mp3_temperature;
            anim = gameObject.GetComponent<Animator>();
            anim.Play("Camera_Change In Temp");
        }
        public void _StartCam_Change_in_color()
        {
            mp3_color.SetActive(true);
            _mp3 = mp3_color;
            anim = gameObject.GetComponent<Animator>();
            anim.Play("Camera_Change In Colour");
        }
        public void _StartCam_change_in_state()
        {
            mp3_state.SetActive(true);
            _mp3 = mp3_state;
            anim = gameObject.GetComponent<Animator>();
            anim.Play("Change_In_state_Camera");
        }


    void _Evolution_of_gas()
    {
        Shelf.transform.rotation = Quaternion.Euler(0, 270, 0);
    }
    void _Change_in_Temp()
    {
        Shelf.transform.rotation = Quaternion.Euler(0, 90, 0); 
    }
    void _Change_in_color()
    {
        Shelf.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    void _change_in_state()
    {
        Shelf.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void _Beaker_1Method()
    {
        anim = Beaker_1.GetComponent<Animator>();
        anim.Play("Beaker_1Action");
    }


    void _Mask_Method()
    {
     /*   anim = Cube_Mask.GetComponent<Animator>();
        anim.Play("Cube_Mask");*/
    }


    void _Beaker_2Method()
    {
        anim = Beaker_2.GetComponent<Animator>();
        anim.Play("Beaker_2Action");
    }

   
    void _Beaker_3Method()
    {
        anim = Beaker_3.GetComponent<Animator>();
        anim.Play("Beaker_3Action");

        anim = Ice_Cubes.GetComponent<Animator>();
        anim.Play("Ice_Cubes_In");

        Camphor.SetActive(false);

        
        Smoke.SetActive(false);

    }

    
            

    void _Beaker_4Method()
    {
        anim = Beaker_4.GetComponent<Animator>();
        anim.Play("Beaker_4Action");
    }


    void _Bunsun_Burner_2Method()
    {
        anim = Bunsun_Burner_2.GetComponent<Animator>();
        anim.Play("Bunsun_Burner_2Action");
    }


    void _Bunsun_Burner_1Method()
    {
        anim = Bunsun_Burner_1.GetComponent<Animator>();
        anim.Play("Bunsun_Burner_Action");
    }


    void _Camphor_Method()
    {
        anim = Camphor.GetComponent<Animator>();
        anim.Play("Camphor_Action");
    }


    void _Clamp_Method()
    {
        anim = Clamp.GetComponent<Animator>();
        anim.Play("Clamp_Action");
    }


    void _Clamp_2_Method()
    {
        anim = Clamp_2.GetComponent<Animator>();
        anim.Play("Clamp_2Action");
    }


    void _Clamp_Stand_Method()
    {
        anim = Clamp_Stand.GetComponent<Animator>();
        anim.Play("Clamp_StandAction");
    }


    void _Clamp_Stand_2Method()
    {
        anim = Clamp_Stand_2.GetComponent<Animator>();
        anim.Play("Clamp_Stand_2Action");
    }


    void _Grill_Method()
    {
        anim = Grill.GetComponent<Animator>();
        anim.Play("Grill_Action");
    }


    void _Halogram_Wings_Method()
    {
        anim = Halogram_Wings.GetComponent<Animator>();
        anim.Play("Halogram_Wing");
    }


    void _Halogram_Rays_Method()
    {
        anim = Halogram_Rays.GetComponent<Animator>();
        anim.Play("Halogram_RaysAction");
    }


   
    void _Heat_Proof_Mat_Method()
    {
        anim = Heat_Proof_Mat.GetComponent<Animator>();
        anim.Play("Heat_Proof_MatAction");
    }


    void _Ice_Cubes_Method()
    {
        anim = Ice_Cubes.GetComponent<Animator>();
        anim.Play("Ice_CubesAction");

        Water.SetActive(true);

        anim = Water.GetComponent<Animator>();
        anim.Play("Water_Action");
    }


    void _Lime_Water_Method()
    {
        anim = Lime_Water.GetComponent<Animator>();
        anim.Play("Lime_WaterAction");

        anim = Lime_Water_2.GetComponent<Animator>();
        anim.Play("Lime_water_2");

        Waterfall.SetActive(true);
    }


    void _Paper_Method()
    {
        anim = Paper.GetComponent<Animator>();
        anim.Play("Paper_Action");
    }


    void _Test_Tube_1_Method()
    {
        anim = Test_Tube_1.GetComponent<Animator>();
        anim.Play("Test_Tube_1Action");

       /* anim = HCL.GetComponent<Animator>();
        anim.Play("HCL_ Action");*/
    }


    void _HCL_Method()
    {
        anim = HCL.GetComponent<Animator>();
        anim.Play("HCL_Action");
    }

    void _Test_Tube_2_Method()
    {
        anim = Test_Tube_2.GetComponent<Animator>();
        anim.Play("Test_Tube_2Action"); 
    }

    void _feso_colorchange()
    {
        Feso.GetComponent<ColorLerp>().enabled = true;
    }


    void _Test_Tube_3_Method()
    {
        anim = Test_Tube_3.GetComponent<Animator>();
        anim.Play("Test_Tube_3Action");
    }

    void _Tripod_Method()
    {
        anim = Tripod.GetComponent<Animator>();
        anim.Play("Tripod_Action");
    }

    
    void _Change_In_State_OutMethod()
    {
        Ice_Cubes.SetActive(false);

        AfterBurner.SetActive(false);

        anim = Tripod.GetComponent<Animator>();
        anim.Play("Tripod_OutAction");

        anim = Beaker_3.GetComponent<Animator>();
        anim.Play("Beaker_3_OutAction");

        anim = Bunsun_Burner_1.GetComponent<Animator>();
        anim.Play("Bunsun_Burner_OutAction");

        anim = Grill.GetComponent<Animator>();
        anim.Play("Grill_OutAction");

        anim = Heat_Proof_Mat.GetComponent<Animator>();
        anim.Play("Heat_proof_mat_OutAction");

        anim = Water.GetComponent<Animator>();
        anim.Play("Water_OutAction");

    }


    void _Change_In_Temp_OutMethod()
    {
        anim = Beaker_1.GetComponent<Animator>();
        anim.Play("Beaker_1_OutAction");
    }


    void _Change_In_Colour_OutMethod()
    {
        anim = Clamp_Stand.GetComponent<Animator>();
        anim.Play("Clamp_Stand_OutAction");

        anim = Clamp.GetComponent<Animator>();
        anim.Play("Clamp_OutAction");

        anim = Bunsun_Burner_2.GetComponent<Animator>();
        anim.Play("Bunsun_Burner_2_OutAction");

        anim = Test_Tube_2.GetComponent<Animator>();
        anim.Play("Test_Tube_2_OutAction");

        AfterBurner_2.SetActive(false);
    }


    void _Evolution_Of_Gas_OutMethod()
    {
        anim = Clamp_Stand_2.GetComponent<Animator>();
        anim.Play("Clamp_Stand_2_OutAction");

        anim = Test_Tube_1.GetComponent<Animator>();
        anim.Play("Test_Tube_1_OutAction");

        anim = Clamp_2.GetComponent<Animator>();
        anim.Play("Clamp_2_OutAction");

        anim = Paper.GetComponent<Animator>();
        anim.Play("Paper_OutAction");

        HCL.SetActive(false);
    }


    void _Lime_Water_OnMethod()
    {
        Lime_Water.SetActive(true);
    }


    void _Explosion_OnMethod()
    {
        Explosion.SetActive(true);

        Lime_Paste.SetActive(true);

        Lime_Stone.SetActive(false);

        Lime_Water.SetActive(false);

        Bubbles.SetActive(false);
    }


    void _Bubbles_OnMethod()
    {
        Bubbles.SetActive(true);
    }


    void _Waterfall_OffMethod()
    {
        Waterfall.SetActive(false);
    }



    void _Burner_OnMethod()
    {
        AfterBurner.SetActive(true);
    }


    void _Burner_2_OnMethod()
    {
        AfterBurner_2.SetActive(true); 
    }

    void _copperCarbonate_colorchange()
    {
        CopperCarbonate.GetComponent<ColorLerp>().enabled = true;
    }

    void _Smoke_OnMethod()
    {
        Smoke.SetActive(true);
    }

        
    void _HCL_OnMethod()
    {
        HCL.SetActive(true);
        _HCL_Method();
    }


    void _Sparkles_OnMethod()
    {
        Sparkles.SetActive(true);
    }


    void _Sparkles_OffMethod()
    {
        Sparkles.SetActive(false);
    }

    void _Paper2_OnMethod()
    {
        Paper2.SetActive(true);
    }

    void _Paper2_OffMethod()
    {
        Paper2.SetActive(false);
    }

    void _HCLeff_OnMethod()
    {
        hcleff.SetActive(true);
    }

    void _HCLeff_OffMethod()
    {
        hcleff.SetActive(false);
    }

    void _FeO_onMethod()
    {
        FeO.SetActive(true);
    }


    void _FeO_offMethod()
    {
        FeO.SetActive(false);
    }


    void _CaO_onMethod()
    {
        CaO.SetActive(true);
    }


    void _CaO_offMethod()
    {
        CaO.SetActive(false);
    }


    void _CuCO3_onMethod()
    {
        CuCO3.SetActive(true);
    }


    void _CuCO3_offMethod()
    {
        CuCO3.SetActive(false);
    }


    void _FeSO4_onMethod()
    {
        FeSO4.SetActive(true);
    }

    void _FeSO4_offMethod()
    {
        FeSO4.SetActive(false);
    }

    void _Na2SO3_onMethod()
    {
        Na2SO3.SetActive(true);
    }


    void _Na2SO3_offMethod()
    {
        Na2SO3.SetActive(false);
    }

    void _Experiment_finish()
    {
        //stop audio
        _mp3.GetComponent<AudioSource>().Stop();_mp3.SetActive(false);
        //enable go btn collider
        hitObj.GetComponent<CapsuleCollider>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    { 

        ScreenPointTouch();

        //=============GAMEPLAY==============//
        /* Get Player Entered Trigger Names*/
        EnteredTriggerName();
        /* Gameplay triggers */
        DoTriggerActions();
        /* Get interactable Collider Name */
        HoverdColliderName();
        //===================================//

    }

    void ScreenPointTouch()
    {
        Camera MainCam = gameObject.GetComponent<Camera>();
        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //on mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            //check if mouse click 
            if (Physics.Raycast(ray, out hit))
            { 
                if (hit.collider.gameObject.tag == "Interactable") 
                { 
                    if (hit.collider.gameObject.name == "GoSwitch")
                    {
                        hitObj = hit.collider.gameObject;
                        anim = hitObj.GetComponent<Animator>();
                        anim.Play("goBtn");
                        hitObj.GetComponent<CapsuleCollider>().enabled = false;
                        StartCoroutine(_Exp_Canvas_Show());
                    }
                }
            }
        }
    }

   
    IEnumerator _Exp_Canvas_Show()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Experiments_canvas.SetActive(true);
    }





    public GameObject playerObj;


    public void _ResetnInitialize()
    {


        //=======stop ongoing explanatory audio=====//
        if(_mp3 != null) { _mp3.SetActive(false); }

        //myAudio.Stop();
        myAudio.clip = game_bgm;
        myAudio.Play(); myAudio.loop = true;
        //==========================================//
        anim = ExpCam.GetComponent<Animator>();
        anim.Play("empty");

        Scene_Gameplay.SetActive(true);
        Scene_Explantion.SetActive(false);

        //==================== INSERT PLAYER  ====================//
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/PlayerPrefabs/Main/__Player", typeof(GameObject));  // Load Player  // Load Player
        Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate Player 
        mainPlayer = GameObject.Find("TPP_Player");  //Get TPP_Player
        GameObject spawnpoint = mainPlayer.GetComponent<Teleporting>().FindClosestSpawnPoint();  //find spawnpoint
        //teleport Player
        mainPlayer.GetComponent<CharacterController>().enabled = false;
        mainPlayer.GetComponent<CharacterController>().transform.position = spawnpoint.transform.position;
        mainPlayer.GetComponent<CharacterController>().enabled = true;
        //Diable Player Camera Auido Listerner
        GameObject.Find("MainCamera").GetComponent<AudioListener>().enabled = false;
        gameObject.GetComponent<Camera>().enabled = false;
        //========================================================//
         

        //============= POPULATE OBJECTIVE CANVAS TEXT FOR GAMEPLAY ============//
        //Show objective canvas
        Objective_canvas.SetActive(true);


        //Objective = GameObject.Find("Main_Objective").GetComponent<TextMeshProUGUI>();
        Objective.text = "उद्देश्य: द्वीप से भाग जाओ";

        //Steps = GameObject.Find("Steps").GetComponent<TextMeshProUGUI>();
        Steps.text = miniObjectives[0];
        //==========================================================//

        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

     void LastObjective()
    {
        GreenCheckBox.SetActive(true);
        Steps.color = Color.green;
        Objective.color = Color.green;
        Invoke("missionPassed", 2f);
    }
    void NextObjective()
    {
        //clear triggername
        mainPlayer.GetComponent<GameTriggersManager>().ClearTriggerName();

        //objective checkbox toggle
        GreenCheckBox.SetActive(true);
        Steps.color = Color.green; 
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

    void missionPassed()
    {
        Cursor.visible = true; 
        GameObject missionCompletePrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/Mission Passed", typeof(GameObject));  // Load Player
        Instantiate(missionCompletePrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate mission-Complete canvas 
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
         if (trigname == "question_area_trigger")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //display the question
                Q1.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //triggeroff
                questionareatrigger.SetActive(false);


            }

        }
        else  if  (trigname == "trigger1-quicklime")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                QuickLime.SetActive(false);

                //Delete this trigger
                Destroy(Lever0_trig);


                //popupquestion
                Q2.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //next objective
                NextObjective();

            }

        }
        else if (trigname == "trigger2-water")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                QuickLime.SetActive(true);
                anim = QuickLime.GetComponent<Animator>();
                anim.Play("quicklime mix");

                //Delete this trigger
                Destroy(Lever2_trig);
                //Activate next trigger
                Lever3_trig.SetActive(true);
                FeS.SetActive(true);

                //next objective
                NextObjective();

            }

        }
        else if (trigname == "trigger3-FeS")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                FeS.SetActive(false);

                //popupqyestion
                Q3.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;


                //next objective
                NextObjective();

            }

        }
        else if (trigname == "trigger4-Wolf" && !trigger4_entered)
        {
            trigger4_entered = true;
            //Delete this trigger
            Destroy(Lever4_trig);

            WolfVCAM.SetActive(true);
            Wolf.SetActive(true);
            WolfWall.SetActive(true);

            //Activate next trigger
            Lever5_trig.SetActive(true);

            //next objective
            NextObjective();

        }
        else if (trigname == "trigger5-smell")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //mix fes in hcl
                FeS.SetActive(true);
                anim = FeS.GetComponent<Animator>();
                anim.Play("fes-hcl");
                //turnon smell fx
                Smell_FX.SetActive(true);
                //wolf away
                anim = Wolf_away.GetComponent<Animator>();
                anim.Play("wolf away");
                //wolf running
                anim = Wolf.GetComponent<Animator>();
                anim.Play("Wolf_run");

                //Delete this trigger
                Destroy(Lever5_trig);
                

                //next objective
                NextObjective();

                Lever6_trig.SetActive(true);    

            }

        }
        else if (trigname == "trigger6-srno3")
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                //popupquestion
                Q4.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                StrontiumNitrate.SetActive(false);
                SrNO3_picked = true;
                Destroy(Lever6_trig);
            }
        }
        else if (trigname == "trigger7-flare" && SrNO3_picked)
        {
            if (Input.GetKeyDown(KeyCode.E) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed)
            {
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed = false;
                RedFlameFX.SetActive(true);
                Destroy(Lever7_trig);
                //next objective
                LastObjective();
            }
        }
    }
    //get Collider name that player Hovered
    void HoverdColliderName()
    {
        if (mainPlayer != null)
        {
             colliderObj = mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj;
            if (colliderObj != null)
            {
                Interactable_ColliderName = mainPlayer.GetComponent<PlayerFunctionsController>().CrosshairHitObj.name;
                if (Interactable_ColliderName != null)
                {
                   // Debug.Log("collider:" + Interactable_ColliderName ); 
                    if ( DoingColliderAct && ( ((Input.GetMouseButtonDown(0) && !IsPointerOverUIObject()) || mainPlayer.GetComponent<PlayerFunctionsController>().InteractIspressed) ) )
                    {
                        DoingColliderAct = false;
                        DoColliderAction();
                    }
                }
            }
        }
    }

    //call respective collider actions based on collider names
    void DoColliderAction()
    { 
            Debug.Log("DoingColliderAct");
            Debug.Log("collider:" + Interactable_ColliderName + "clicked do something");
        if (Interactable_ColliderName == "HCL")
        {
            DoingColliderAct = true;

            //activate cutscene CAM
            NaohVCAM.SetActive(true);
            // pour hcl on naoh animation
            anim = hcl_barrel.GetComponent<Animator>();
            anim.Play("hcl out");
            //shrink naoh
            anim = naoh_solidWall.GetComponent<Animator>();
            anim.Play("NaOH");
            //raise water
            anim = NaohMeltWater.GetComponent<Animator>();
            anim.Play("water raise");
            //destroy trigger
            Destroy(Lever0_trig);
            //next
            NextObjective();


        }
        else if (Interactable_ColliderName == "hno3" || Interactable_ColliderName == "h2so4")
        {

            //wrong selection 
            defaultMaterial = colliderObj.GetComponent<Renderer>().material;
            colliderObj.GetComponent<Renderer>().material = redMaterialRef;
            StartCoroutine(RedDefaultMat());
        }

    
    }



    IEnumerator RedDefaultMat()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        colliderObj.GetComponent<Renderer>().material = error_RedMaterial; 
        StartCoroutine(DefaultMat());
    }

    IEnumerator DefaultMat()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        colliderObj.GetComponent<Renderer>().material = defaultMaterial; 

        DoingColliderAct = true;
    }




    //buttons

    public void correctans_1()
    {
        //Debug.Log("hcl");

        

        //Activate next trigger
        Lever1_trig.SetActive(true);

        //popupquestionoff
        Q1.SetActive(false);

        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }


    }

    public void correctans_2()
    {

        //Delete this trigger
        Destroy(Lever1_trig);
        //Activate next trigger
        Lever2_trig.SetActive(true);

        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        

    }
    public void correctans_3()
    {

        //Delete this trigger
        Destroy(Lever3_trig);
        //Activate next trigger
        Lever4_trig.SetActive(true);

        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        

    }
    public void correctans_4()
    {

        //Activate next trigger
        Lever7_trig.SetActive(true);

        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        

    }



    public void wrongans_()
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
