using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_coal_petroleum : MonoBehaviour
{
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

    public void Skip()
    {
        myAudio.Stop();
        //myAudio.clip = game_bgm;
        //myAudio.Play(); myAudio.loop = true;
        //==========================================//
        anim = GetComponent<Animator>();
        anim.enabled = false;

        Scene_Gameplay.SetActive(true);
        Scene_Explantion.SetActive(false);

        InventoryManager.Instance.GetComponent<GamePlayManager>().InitializeScene();
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject se;
    public GameObject diesel_engine;
    public GameObject electric_engine;
    public GameObject line_1;
    public GameObject line_2;
    public GameObject line_3;
    public GameObject line_4;
    public GameObject water_text;
    public GameObject bb_;
    public GameObject traffic_lights;
    public GameObject red_;
    public GameObject green_;
    public GameObject types_of_resources;
    public GameObject natural_resources;
    public GameObject Types_of_Natural_Resources;
    public GameObject Inexhaustible_natural_resources;
    public GameObject Exhaustible_natural_resources;
    public GameObject coal;
    public GameObject coal_formation;
    public GameObject products_of_coal;
    public GameObject coke;
    public GameObject tar_coal;
    public GameObject coal_gas_experiment;
    public GameObject Refinery_Defination;
    public GameObject Refinery_Explanation;
    public GameObject CGI_Gas;
    public GameObject description_1;
    public GameObject description_2;
    public GameObject description_3;
    public GameObject description_4;
    public GameObject description_5;
    public GameObject description_6;
    public GameObject description_7;
    public GameObject description_8;
    public GameObject description_9;
    public GameObject description_4_1;
    public GameObject description_5_1;
    public GameObject description_6_1;
    public GameObject UI_button_1;
    public GameObject UI_button_2;
    public GameObject UI_button_3;
    public GameObject UI_button_4;







    // Exp - Animations
    private Animator anim;
    [Header("Explanation anims")]

    public GameObject red_car;
    public GameObject yellow_car;
    public GameObject yellow_car_2;
    public GameObject baum_ld2_mobile_6;
    public GameObject bladehead;
    public GameObject bladehead_2;
    public GameObject bladehead_3;
    public GameObject turbine_wheel;
    public GameObject shaft;
    public GameObject gate_slider;
    public GameObject base_water;
    public GameObject dam_water;
    public GameObject leaver;
    public GameObject cube;
    public GameObject hand;
    public GameObject yc_trye_1;
    public GameObject yc_trye_2;
    public GameObject yc_trye_3;
    public GameObject yc_trye_4;


    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip in_1;
    public AudioClip in_2;
    public AudioClip in_3;
    public AudioClip in_4;
    public AudioClip in_5;
    public AudioClip ToRes;
    public AudioClip ToRes_ex;
    public AudioClip ToNaRes;
    public AudioClip InExhNaRes;
    public AudioClip ExhNaRes;
    public AudioClip ca;
    public AudioClip pr_o_ca;
    public AudioClip ca_for;
    public AudioClip ck;
    public AudioClip ca_tar;
    public AudioClip ca_gas_exp_in;
    public AudioClip ck_i_tube;
    public AudioClip br_on;
    public AudioClip ca_gas_exp;
    public AudioClip ref_def;
    public AudioClip ref_exp;
    public AudioClip ref_ex;
    public AudioClip bb;
    public AudioClip na_gas;
    public AudioClip cng;
    public AudioClip cr_in;
    public AudioClip cr;
    public AudioClip cr_s;
    public AudioClip tr_pr;
    public AudioClip fr;
    public AudioClip pr;
    public AudioClip wr;
    public AudioClip co;
    public AudioClip wm;
    public AudioClip mn;
    public AudioClip ng;
    public AudioClip cr_b;
  

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }










    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camera_anim", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }
    }

    public void _Jump_To1(float value)
    {
        RestartSceneWithKeyframe(value);
    }

    private void RestartSceneWithKeyframe(float normalizedTime)
    {
        targetNormalizedTime = normalizedTime; // Store the keyframe to jump to
                                               //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(1);
    }

















    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }

    //camera_explanatory play pause

    void _camExp_Pause() {
        gameObject.GetComponent<Animator>().speed = 0f;
    }

    //
    void _streamengineMethodON()
    {
        se.SetActive(true);
    }

    void _streamengineMethodOFF()
    {
        se.SetActive(false);
    }

    void _diesel_engineMethodON()
    {
        diesel_engine.SetActive(true);
    }

    void _diesel_engineMethodOFF()
    {
        diesel_engine.SetActive(false);
    }

    void _electric_engineMethodON()
    {
        electric_engine.SetActive(true);
    }

    void _electric_engineMethodOFF()
    {
        electric_engine.SetActive(false);
    }

    void _yellow_car_MethodON()
    {
        yellow_car.SetActive(true);
    }

    void _yellow_car_MethodOFF()
    {
        yellow_car.SetActive(false);
    }

    void _red_car_MethodON()
    {
        red_car.SetActive(true);
    }

    void _red_car_MethodOFF()
    {
        red_car.SetActive(false);
    }

    void _line_1_MethodON()
    {
        line_1.SetActive(true);
    }

    void _line_1_MethodOFF()
    {
        line_1.SetActive(false);
    }

    void _line_2_MethodON()
    {
        line_2.SetActive(true);
    }

    void _line_2_MethodOFF()
    {
        line_2.SetActive(false);
    }

    void _line_3_MethodON()
    {
        line_3.SetActive(true);
    }

    void _line_3_MethodOFF()
    {
        line_3.SetActive(false);
    }

    void _line_4_MethodON()
    {
        line_4.SetActive(true);
    }

    void _line_4_MethodOFF()
    {
        line_4.SetActive(false);
    }

    void _water_text_4_MethodON()
    {
        water_text.SetActive(true);
    }

    void _water_text_MethodOFF()
    {
        water_text.SetActive(false);
    }

  
    void _tl_MethodON()
    {
        traffic_lights.SetActive(true);
    }

    void _tl_MethodOFF()
    {
        traffic_lights.SetActive(false);
    }


    void _red_MethodON()
    {
        red_.SetActive(true);
    }

    void _red_MethodOFF()
    {
        red_.SetActive(false);
    }

    void _green_MethodON()
    {
        green_.SetActive(true);
    }

    void _green_MethodOFF()
    {
        green_.SetActive(false);
    }


    void _panel_5_MethodON()
    {
        types_of_resources.SetActive(true);
    }


    void _panel_6_MethodON()
    {
        natural_resources.SetActive(true);
    }

    

    void _panel_7_MethodON()
    {
        Types_of_Natural_Resources.SetActive(true);
    }

    


    void _panel_8_MethodON()
    {
        Inexhaustible_natural_resources.SetActive(true);
    }

   
    void _panel_9_MethodON()
    {
        Exhaustible_natural_resources.SetActive(true);
    }


    void _panel_10_MethodON()
    {
        coal.SetActive(true);
    }

   

    void _panel_11_MethodON()
    {
        coal_formation.SetActive(true);
    }

    
    void _panel_12_MethodON()
    {
        products_of_coal.SetActive(true);
    }

   
    void _panel_13_MethodON()
    {
        coke.SetActive(true);
    }

   

    void _panel_14_MethodON()
    {
        tar_coal.SetActive(true);
    }

   
    void _panel_15_MethodON()
    {
        coal_gas_experiment.SetActive(true);
    }

   

    void _panel_16_MethodON()
    {
        Refinery_Defination.SetActive(true);
    }

    

    void _panel_17_MethodON()
    {
        Refinery_Explanation.SetActive(true);
    }



    void _panel_19_MethodON()
    {
        CGI_Gas.SetActive(true);
    }

    
    void _description_1_MethodON()
    {
        description_1.SetActive(true);

    }

    
    //
    void _description_2_MethodON()
    {
        description_2.SetActive(true);

    }

  
    //
    void _description_3_MethodON()
    {
        description_3.SetActive(true);

    }

    
    //
    void _description_4_MethodON()
    {
        description_4.SetActive(true);

    }

   
    //
    void _description_5_MethodON()
    {
        description_5.SetActive(true);

    }

    
    //
    void _description_6_MethodON()
    {
        description_6.SetActive(true);

    }

    
    //
    void _description_7_MethodON()
    {
        description_7.SetActive(true);

    }

   
    //
    void _description_8_MethodON()
    {
        description_8.SetActive(true);

    }

    
    //
    void _description_9_MethodON()
    {
        description_9.SetActive(true);

    }

    
    //
    void _description_4_1_MethodON()
    {
        description_4_1.SetActive(true);

    }

    
    //
    void _description_5_1_MethodON()
    {
        description_5_1.SetActive(true);

    }

    //
    void _description_6_1_MethodON()
    {
        description_6_1.SetActive(true);

    }

    
    //
    void _UI_button_1_MethodON()
    {
        UI_button_1.SetActive(true);

    }

   
    //
    void _UI_button_2_MethodON()
    {
        UI_button_2.SetActive(true);

    }

    
    //
    void _UI_button_3_MethodON()
    {
        UI_button_3.SetActive(true);

    }

    
    //
    void _UI_button_4_MethodON()
    {
        UI_button_4.SetActive(true);

    }

    
    //
    void _bb_MethodON()
    {
        bb_.SetActive(true);

    }

    void _bb_MethodOFF()
    {
        bb_.SetActive(false);
    }
    //

    void _streamengine_Animmethod()
    {

        anim = se.GetComponent<Animator>();
        anim.Play("train_anim");
    }

    void _electricengine_Animmethod()
    {

        anim = electric_engine.GetComponent<Animator>();
        anim.Play("electric_train_anim");
    }

    void _dieselengine_Animmethod()
    {

        anim = diesel_engine.GetComponent<Animator>();
        anim.Play("diesel_train_anim");
    }

    void _yellow_car_Animmethod()
    {

        anim = yellow_car.GetComponent<Animator>();
        anim.Play("car_anim");
    }

    void _yellow_car_2_Animmethod()
    {

        anim = yellow_car.GetComponent<Animator>();
        anim.Play("yelow_car_anim_2");

    }

    void _yellow_car_3_Animmethod()
    {

        anim = yellow_car_2.GetComponent<Animator>();
        anim.Play("CAR_LAST_ANIM");

    }

    void _tryes_rotate_Animmethod()
    {

        anim = yc_trye_1.GetComponent<Animator>();
        anim.Play("tyre_anim");

        anim = yc_trye_2.GetComponent<Animator>();
        anim.Play("tyre_anim");

        anim = yc_trye_3.GetComponent<Animator>();
        anim.Play("tyre_anim");

        anim = yc_trye_4.GetComponent<Animator>();
        anim.Play("tyre_anim");

    }


    void _tryes_Animmethod()
    {

        anim = yc_trye_1.GetComponent<Animator>();
        anim.Play("empty");

        anim = yc_trye_2.GetComponent<Animator>();
        anim.Play("empty");

        anim = yc_trye_3.GetComponent<Animator>();
        anim.Play("empty");

        anim = yc_trye_4.GetComponent<Animator>();
        anim.Play("empty");

    }


    void _hand_Animmethod()
    {

        anim = hand.GetComponent<Animator>();
        anim.Play("hand");

    }


    void _red_car_Animmethod()
    {

        anim = red_car.GetComponent<Animator>();
        anim.Play("red_car_anim");
    }

    void _baum_ld2_mobile_6_Animmethod()
    {

        anim = baum_ld2_mobile_6.GetComponent<Animator>();
        anim.Play("tree_anim");
    }

    void _cube_Animmethod()
    {

        anim =cube.GetComponent<Animator>();
        anim.Play("cube");
    }


    void _hydro_Animmethod()
    {

        anim = bladehead.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_2.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_3.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = turbine_wheel.GetComponent<Animator>();
        anim.Play("wheel_wheel");
        anim = turbine_wheel.GetComponent<Animator>();
        anim.Play("wheel_wheel");
        anim = shaft.GetComponent<Animator>();
        anim.Play("shaft_shaft");
        anim = base_water.GetComponent<Animator>();
        anim.Play("base water _base water Action");
        anim = leaver.GetComponent<Animator>();
        anim.Play("wheel leaver_wheel leaverAction");
        anim = gate_slider.GetComponent<Animator>();
        anim.Play("gate slider_gate sliderAction");
        anim = dam_water.GetComponent<Animator>();
        anim.Play("dam_anim");
    }

    void _in_1_audioMethod()
    {
        myAudio.clip = in_1;
        myAudio.Play();
    }

    void _in_2_audioMethod()
    {
        myAudio.clip = in_2;
        myAudio.Play();
    }

    void _in_3_audioMethod()
    {
        myAudio.clip = in_3;
        myAudio.Play();
    }

    void _in_4_audioMethod()
    {
        myAudio.clip = in_4;
        myAudio.Play();
    }

    void _in_5_audioMethod()
    {
        myAudio.clip = in_5;
        myAudio.Play();
    }

    void _ToRes_audioMethod()
    {
        myAudio.clip = ToRes;
        myAudio.Play();
    }

    void _ToRes_ex_audioMethod()
    {
        myAudio.clip = ToRes_ex;
        myAudio.Play();
    }

    void _ToNaRes_audioMethod()
    {
        myAudio.clip = ToNaRes;
        myAudio.Play();
    }

    void _InExhNaRes_audioMethod()
    {
        myAudio.clip = InExhNaRes;
        myAudio.Play();
    }

    void _ExhNaRes_audioMethod()
    {
        myAudio.clip = ExhNaRes;
        myAudio.Play();
    }

    void _ca_audioMethod()
    {
        myAudio.clip = ca;
        myAudio.Play();
    }

    void _ck_audioMethod()
    {
        myAudio.clip = ck;
        myAudio.Play();
    }

    void _pr_o_ca_audioMethod()
    {
        myAudio.clip = pr_o_ca;
        myAudio.Play();
    }

    void _ca_for_audioMethod()
    {
        myAudio.clip = ca_for;
        myAudio.Play();
    }

    void _ca_tar_audioMethod()
    {
        myAudio.clip = ca_tar;
        myAudio.Play();
    }

    void _ca_gas_exp_in_audioMethod()
    {
        myAudio.clip = ca_gas_exp_in;
        myAudio.Play();
    }

    void _ck_i_tube_audioMethod()
    {
        myAudio.clip = ck_i_tube;
        myAudio.Play();
    }

    void _br_on_audioMethod()
    {
        myAudio.clip = br_on;
        myAudio.Play();
    }

    void _ca_gas_exp_audioMethod()
    {
        myAudio.clip = ca_gas_exp;
        myAudio.Play();
    }

    void _ref_def_audioMethod()
    {
        myAudio.clip = ref_def;
        myAudio.Play();
    }

    void _ref_exp_audioMethod()
    {
        myAudio.clip = ref_exp;
        myAudio.Play();
    }

    void _ref_ex_audioMethod()
    {
        myAudio.clip = ref_ex;
        myAudio.Play();
    }

    void _fr_audioMethod()
    {
        myAudio.clip = fr;
        myAudio.Play();
    }

    void _wm_audioMethod()
    {
        myAudio.clip = wm;
        myAudio.Play();
    }

    void _wr_audioMethod()
    {
        myAudio.clip = wr;
        myAudio.Play();
    }

    void _co_audioMethod()
    {
        myAudio.clip = co;
        myAudio.Play();
    }

    void _pr_audioMethod()
    {
        myAudio.clip = pr;
        myAudio.Play();
    }

    void _ng_audioMethod()
    {
        myAudio.clip = ng;
        myAudio.Play();
    }

    void _mn_audioMethod()
    {
        myAudio.clip = mn;
        myAudio.Play();
    }


    void _bb_audioMethod()
    {
        myAudio.clip = bb;
        myAudio.Play();
    }

    void _na_gas_audioMethod()
    {
        myAudio.clip = na_gas;
        myAudio.Play();
    }

    void _cng_audioMethod()
    {
        myAudio.clip = cng;
        myAudio.Play();
    }

    void _cr_in_audioMethod()
    {
        myAudio.clip = cr_in;
        myAudio.Play();
    }

    void _cr_audioMethod()
    {
        myAudio.clip = cr;
        myAudio.Play();
    }

    void _cr_s_audioMethod()
    {
        myAudio.clip = cr_s;
        myAudio.Play();
    }

    void _tr_pr_audioMethod()
    {
        myAudio.clip = tr_pr;
        myAudio.Play();
    }

    void _cr_b_audioMethod()
    {
        myAudio.clip = cr_b;
        myAudio.Play();
    }

    public int index;
    public void PickUpEvent()
    {
        index++;
        if(index == 2)
        {
            index = 0;
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public Animator windmillAnim;
    public GameObject pertroleumCan;

    public void WindMill()
    {
        windmillAnim.SetTrigger("Trigger");
        pertroleumCan.SetActive(true);
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void PlaceObjectsLevel1(GameObject obj)
    {
        index++;
        obj.SetActive(true);

        if(index == 2)
        {
            index = 0;
            WindMill();
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }

    }

    public List<GameObject> barrel = new List<GameObject>();
    bool canTurnOnMachine = true;
    bool canCollect = false;
    public void MachineAnim(Animator anim)
    {
        if(crudeOilCollected)
        {
            if(canTurnOnMachine && !canCollect)
            {
                canTurnOnMachine = false;
                StartCoroutine(DelaymachineOn());

                anim.SetTrigger(index.ToString());
                index++;

                if (index == 4)
                {
                    index = 0;
                    InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
                    InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
                }
            }
        }
        else
        {
            MissionFailed();
        }
    }

    IEnumerator DelaymachineOn()
    {
        yield return new WaitForSeconds(2f);
        canCollect = true;
    }

    public BoxCollider machineStartColoider;
    public BoxCollider machineCollectColoider;
    public void Collect()
    {
        if(canCollect)
        {
            canCollect = false;
            canTurnOnMachine = true;
            //foreach (GameObject obj in barrel)
            //{
            //    obj.SetActive(false);
            //}

            barrel[index - 1].SetActive(true);
            machineStartColoider.enabled = true;
            machineCollectColoider.enabled = true;
        }
    }

    public void ObjectiveComplete()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    bool crudeOilCollected = false;
    public void PickUpCrudeOil()
    {
        crudeOilCollected = true;
        InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void StepComp()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void TurnOnGameObject(GameObject obj)
    {
        StartCoroutine(DelayTurnOnGameObject(obj));
    }

    IEnumerator DelayTurnOnGameObject(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        obj.SetActive(true);

    }
}
