using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_water6 : MonoBehaviour
{
    public Transform waypoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypoint.player = InventoryManager.Instance.player.transform;
        waypointCanvas.SetActive(true);
        waypoint.target = target;
    }

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
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        SetWayPoint(waypoint1);
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject water;
    public GameObject floodwater;
    public GameObject floodflow;
    public GameObject pondwater;
    public GameObject lowlevelpond;
    public GameObject groundwaterlevel;
    public GameObject groundwaterlowlevel;
    public GameObject raineff;
    public GameObject farmer;
    public GameObject evopeff;
    public GameObject boiling;
    public GameObject evopeffN;



    //BUTTONS
    public GameObject ui_button;



    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("Cam animation", 0, targetNormalizedTime);
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









    // Top
    public GameObject waterT;
    public GameObject waterpropT;
    public GameObject waterusesT;
    public GameObject watercycleT;
    public GameObject waterCycleWorkingT;
    public GameObject RainwaterT;
    public GameObject GroundwaterT;
    public GameObject whathappensT;
    public GameObject norainT;
    public GameObject watershouldT;
    public GameObject rainwaterharvestT;
    






















// Down
    public GameObject waterD;
    public GameObject seventyD;
    public GameObject TastelessD;
    public GameObject universalsolventD;
    public GameObject solidliquidgasD;
    public GameObject icesnowD;
    public GameObject watervapourD;
    public GameObject atomsD;
    public GameObject h20D;
    public GameObject sixtyD;
    public GameObject basicessentialD;
    public GameObject coolingpurposeD;
    public GameObject electricityD;
    public GameObject ninetyD;
    public GameObject watercyclestagesD;
    public GameObject evoporationD;
    public GameObject transpirationD;
    public GameObject condensationD;
    public GameObject precipitationD;
    public GameObject collectionD;
    public GameObject inindiaD;
    public GameObject overflowD;
    public GameObject prolongedD;
    public GameObject responsibleD;
    public GameObject collectionandstoringD;

























// Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]

    

    public GameObject Drinkinganim;












// Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip audio_1;
    public AudioClip audio_2;
    public AudioClip audio_3;
    public AudioClip audio_4;
    public AudioClip audio_5;
    public AudioClip audio_6;
    public AudioClip audio_7;
    public AudioClip audio_8;
    public AudioClip audio_9;
    public AudioClip audio_10;
    public AudioClip audio_11;
    public AudioClip audio_12;
    public AudioClip audio_13;
    public AudioClip audio_14;
    public AudioClip audio_15;
    public AudioClip audio_16;
    public AudioClip audio_17;
    public AudioClip audio_18;
    public AudioClip audio_19;
    public AudioClip audio_20;
    public AudioClip audio_21;
    public AudioClip audio_22;
    public AudioClip audio_23;
    public AudioClip audio_24;
    public AudioClip audio_25;
    public AudioClip audio_26;
    public AudioClip audio_27;
    public AudioClip audio_28;
    public AudioClip audio_29;
    public AudioClip audio_30;
    public AudioClip audio_31;
    public AudioClip audio_32;
    public AudioClip audio_33;
    public AudioClip audio_34;
    public AudioClip audio_35;
    public AudioClip audio_36;
    public AudioClip audio_37;
    public AudioClip audio_38;
    public AudioClip audio_39;
    public AudioClip audio_40;
    public AudioClip audio_41;
    public AudioClip audio_42;
    public AudioClip audio_43;
    public AudioClip audio_44;
    public AudioClip audio_45;
    public AudioClip audio_46;
    public AudioClip audio_47;
    public AudioClip audio_48;
    public AudioClip audio_49;
    public AudioClip audio_50;
    public AudioClip audio_51;
    public AudioClip audio_52;
    public AudioClip audio_53;
    public AudioClip audio_54;
    public AudioClip audio_55;
    public AudioClip audio_56;
    public AudioClip audio_57;
    public AudioClip audio_58;
    public AudioClip audio_59;
    public AudioClip audio_60;
    public AudioClip audio_61;
    public AudioClip audio_62;
    public AudioClip audio_63;
    public AudioClip audio_64;
    public AudioClip audio_65;
    public AudioClip audio_66;
    public AudioClip audio_67;
    public AudioClip audio_68;
    public AudioClip audio_69;
    public AudioClip audio_70;
    public AudioClip audio_71;
    public AudioClip audio_72;
    public AudioClip audio_73;
    public AudioClip audio_74;
    public AudioClip audio_75;
    public AudioClip audio_76;
    public AudioClip audio_77;
    public AudioClip audio_78;
    public AudioClip audio_79;
    public AudioClip audio_80;
    public AudioClip audio_81;
    public AudioClip audio_82;
    public AudioClip audio_83;
    public AudioClip audio_84;
    public AudioClip audio_85;
    public AudioClip audio_86;

















    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }
    //

    //camera_explanatory play pause

    void _camExp_Pause()
    {
        gameObject.GetComponent<Animator>().speed = 0f;
    }

    //

    void _UI_BUTTON_NMethodON()
    {
        ui_button.SetActive(true);
    }

    //





    //
    void _evopeffNMethodON()
    {
        evopeffN.SetActive(true);
    }
    void _evopeffNMethodoOFF()
    {
        evopeffN.SetActive(false);
    }
    //
    void _boilingMethodON()
    {
        boiling.SetActive(true);
    }
    void _boilingMethodoOFF()
    {
        boiling.SetActive(false);
    }
    //

    void _evopeffMethodON()
    {
        evopeff.SetActive(true);
    }
    void _evopeffMethodoOFF()
    {
        evopeff.SetActive(false);
    }
    //

    void _farmerMethodON()
    {
        farmer.SetActive(true);
    }
    void _farmerMethodoOFF()
    {
        farmer.SetActive(false);
    }
    //

     void _raineffMethodON()
    {
        raineff.SetActive(true);
    }
    void _raineffMethodoOFF()
    {
        raineff.SetActive(false);
    }
    //
     void _WaterMethodON()
    {
        water.SetActive(true);
    }
    void _WaterMethodoOFF()
    {
        water.SetActive(false);
    }
    //
    void _floodwaterMethodON()
    {
        floodwater.SetActive(true);
    }
    void _floodwaterMethodoOFF()
    {
        floodwater.SetActive(false);
    }
    //
    void _floodflowMethodON()
    {
        floodflow.SetActive(true);
    }
    void _floodflowMethodoOFF()
    {
        floodflow.SetActive(false);
    }
    //
    void _pondwaterMethodON()
    {
        pondwater.SetActive(true);
    }
    void _pondwaterMethodoOFF()
    {
        pondwater.SetActive(false);
    }
    //
    void _lowlevelpondMethodON()
    {
        lowlevelpond.SetActive(true);
    }
    void _lowlevelpondMethodoOFF()
    {
        lowlevelpond.SetActive(false);
    }
    //
    void _groundwaterlevelMethodON()
    {
        groundwaterlevel.SetActive(true);
    }
    void _groundwaterlevelMethodoOFF()
    {
        groundwaterlevel.SetActive(false);
    }
    //
    void _groundwaterlowlevelMethodON()
    {
        groundwaterlowlevel.SetActive(true);
    }
    void _groundwaterlowlevelMethodoOFF()
    {
        groundwaterlowlevel.SetActive(false);
    }
    //













//Titles
     

     void _WaterTMethodON()
    {
        waterT.SetActive(true);
    }
    void _WaterTMethodoOFF()
    {
        waterT.SetActive(false);
    }
    //
    void _WaterPropDMethodON()
    {
        waterpropT.SetActive(true);
    }
    void _WaterPropTMethodoOFF()
    {
        waterpropT.SetActive(false);
    }
    //
    void _WaterUsesTMethodON()
    {
        waterusesT.SetActive(true);
    }
    void _WaterUsesTMethodoOFF()
    {
        waterusesT.SetActive(false);
    }
    //
    void _WaterCycleTMethodON()
    {
        watercycleT.SetActive(true);
    }
    void _WaterCycleTMethodoOFF()
    {
        watercycleT.SetActive(false);
    }
    //
    void _WaterCycleWorkingTMethodON()
    {
        waterCycleWorkingT.SetActive(true);
    }
    void _WaterCycleWorkingTMethodoOFF()
    {
        waterCycleWorkingT.SetActive(false);
    }
    //
    void _RainwaterTMethodON()
    {
        RainwaterT.SetActive(true);
    }
    void _RainwaterTMethodoOFF()
    {
        RainwaterT.SetActive(false);
    }
    //
    void _GroundwaterTMethodON()
    {
        GroundwaterT.SetActive(true);
    }
    void _GroundwaterTMethodoOFF()
    {
        GroundwaterT.SetActive(false);
    }
    //

    void _whathappensTMethodON()
    {
        whathappensT.SetActive(true);
    }
    void _whathappensTMethodoOFF()
    {
        whathappensT.SetActive(false);
    }
    //
    void _norainTMethodON()
    {
        norainT.SetActive(true);
    }
    void _norainTMethodoOFF()
    {
        norainT.SetActive(false);
    }
    //
    void _watershouldTMethodON()
    {
        watershouldT.SetActive(true);
    }
    void _watershouldTMethodoOFF()
    {
        watershouldT.SetActive(false);
    }
    //
    void _rainwaterharvestTMethodON()
    {
        rainwaterharvestT.SetActive(true);
    }
    void _rainwaterharvestTMethodoOFF()
    {
        rainwaterharvestT.SetActive(false);
    }
    






















//Down
       

    
    void _WaterDMethodON()
    {
        waterD.SetActive(true);
    }
    void _WaterDMethodoOFF()
    {
        waterD.SetActive(false);
    }
    //
    void _SeventyDMethodON()
    {
        seventyD.SetActive(true);
    }
    void _SeventyDMethodoOFF()
    {
        seventyD.SetActive(false);
    }
    //
    void _TastelessDMethodON()
    {
        TastelessD.SetActive(true);
    }
    void _TastelessDMethodoOFF()
    {
        TastelessD.SetActive(false);
    }
    //
    void _universalsolventDMethodON()
    {
        universalsolventD.SetActive(true);
    }
    void _universalsolventDMethodoOFF()
    {
        universalsolventD.SetActive(false);
    }
    //
    void _solidliquidgasDMethodON()
    {
        solidliquidgasD.SetActive(true);
    }
    void _solidliquidgasDMethodoOFF()
    {
        solidliquidgasD.SetActive(false);
    }
    //
    void _icesnowDMethodON()
    {
        icesnowD.SetActive(true);
    }
    void _icesnowDMethodoOFF()
    {
        icesnowD.SetActive(false);
    }
    //
    void _watervapourDMethodON()
    {
        watervapourD.SetActive(true);
    }
    void _watervapourDMethodoOFF()
    {
        watervapourD.SetActive(false);
    }
    //
    void _atomsDMethodON()
    {
        atomsD.SetActive(true);
    }
    void _atomsDMethodoOFF()
    {
        atomsD.SetActive(false);
    }
    //
    void _h20DMethodON()
    {
        h20D.SetActive(true);
    }
    void _h20DMethodoOFF()
    {
        h20D.SetActive(false);
    }
    //
    void _sixtyDMethodON()
    {
        sixtyD.SetActive(true);
    }
    void _sixtyDMethodoOFF()
    {
        sixtyD.SetActive(false);
    }
    //
    void _basicessentialDMethodON()
    {
        basicessentialD.SetActive(true);
    }
    void _basicessentialDMethodoOFF()
    {
        basicessentialD.SetActive(false);
    }
    //
    void _coolingpurposeDMethodON()
    {
        coolingpurposeD.SetActive(true);
    }
    void _coolingpurposeDMethodoOFF()
    {
        coolingpurposeD.SetActive(false);
    }
    //
    void _electricityDMethodON()
    {
        electricityD.SetActive(true);
    }
    void _electricityDMethodoOFF()
    {
        electricityD.SetActive(false);
    }
    //

    void _ninetyDMethodON()
    {
        ninetyD.SetActive(true);
    }
    void _ninetyDMethodoOFF()
    {
        ninetyD.SetActive(false);
    }
    //
    void _watercyclestagesDMethodON()
    {
        watercyclestagesD.SetActive(true);
    }
    void _watercyclestagesDMethodoOFF()
    {
        watercyclestagesD.SetActive(false);
    }
    //
    void _evoporationDMethodON()
    {
        evoporationD.SetActive(true);
    }
    void _evoporationDMethodoOFF()
    {
        evoporationD.SetActive(false);
    }
    //
    void _transpirationDMethodON()
    {
        transpirationD.SetActive(true);
    }
    void _transpirationDMethodoOFF()
    {
        transpirationD.SetActive(false);
    }
    //
    void _condensationDMethodON()
    {
        condensationD.SetActive(true);
    }
    void _condensationDMethodoOFF()
    {
        condensationD.SetActive(false);
    }
    //
    void _precipitationDMethodON()
    {
        precipitationD.SetActive(true);
    }
    void _precipitationDMethodoOFF()
    {
        precipitationD.SetActive(false);
    }
    //
    void _collectionDMethodON()
    {
        collectionD.SetActive(true);
    }
    void _collectionDMethodoOFF()
    {
        collectionD.SetActive(false);
    }
    //

    void _inindiaDMethodON()
    {
        inindiaD.SetActive(true);
    }
    void _inindiaDMethodoOFF()
    {
        inindiaD.SetActive(false);
    }
    //
    void _overflowDMethodON()
    {
        overflowD.SetActive(true);
    }
    void _overflowDMethodoOFF()
    {
        overflowD.SetActive(false);
    }
    //
    void _prolongedDMethodON()
    {
        prolongedD.SetActive(true);
    }
    void _prolongedDMethodoOFF()
    {
        prolongedD.SetActive(false);
    }
    //
    void _responsibleDMethodON()
    {
        responsibleD.SetActive(true);
    }
    void _responsibleDMethodoOFF()
    {
        responsibleD.SetActive(false);
    }
    //
    void _collectionandstoringDMethodON()
    {
        collectionandstoringD.SetActive(true);
    }
    void _collectionandstoringDMethodoOFF()
    {
        collectionandstoringD.SetActive(false);
    }
    //
    

       

























void _Waternimmethod()
    {

        anim = Drinkinganim.GetComponent<Animator>();
        anim.Play("Drinking anim");
    }










void _audio_1_audioMethod()
    {
        myAudio.clip = audio_1;
        myAudio.Play();
    }
    void _audio_2_audioMethod()
    {
        myAudio.clip = audio_2;
        myAudio.Play();
    }
    void _audio_3_audioMethod()
    {
        myAudio.clip = audio_3;
        myAudio.Play();
    }
    void _audio_4_audioMethod()
    {
        myAudio.clip = audio_4;
        myAudio.Play();
    }
    void _audio_5_audioMethod()
    {
        myAudio.clip = audio_5;
        myAudio.Play();
    }
    void _audio_6_audioMethod()
    {
        myAudio.clip = audio_6;
        myAudio.Play();
    }
    void _audio_7_audioMethod()
    {
        myAudio.clip = audio_7;
        myAudio.Play();
    }
    void _audio_8_audioMethod()
    {
        myAudio.clip = audio_8;
        myAudio.Play();
    }
    void _audio_9_audioMethod()
    {
        myAudio.clip = audio_9;
        myAudio.Play();
    }
    void _audio_10_audioMethod()
    {
        myAudio.clip = audio_10;
        myAudio.Play();
    }

    void _audio_11_audioMethod()
    {
        myAudio.clip = audio_11;
        myAudio.Play();
    }
    void _audio_12_audioMethod()
    {
        myAudio.clip = audio_12;
        myAudio.Play();
    }
    void _audio_13_audioMethod()
    {
        myAudio.clip = audio_13;
        myAudio.Play();
    }
    void _audio_14_audioMethod()
    {
        myAudio.clip = audio_14;
        myAudio.Play();
    }
    void _audio_15_audioMethod()
    {
        myAudio.clip = audio_15;
        myAudio.Play();
    }
    void _audio_16_audioMethod()
    {
        myAudio.clip = audio_16;
        myAudio.Play();
    }
    void _audio_17_audioMethod()
    {
        myAudio.clip = audio_17;
        myAudio.Play();
    }
    void _audio_18_audioMethod()
    {
        myAudio.clip = audio_18;
        myAudio.Play();
    }
    void _audio_19_audioMethod()
    {
        myAudio.clip = audio_19;
        myAudio.Play();
    }
    void _audio_20_audioMethod()
    {
        myAudio.clip = audio_20;
        myAudio.Play();
    }
    void _audio_21_audioMethod()
    {
        myAudio.clip = audio_21;
        myAudio.Play();
    }
    void _audio_22_audioMethod()
    {
        myAudio.clip = audio_22;
        myAudio.Play();
    }
    void _audio_23_audioMethod()
    {
        myAudio.clip = audio_23;
        myAudio.Play();
    }
    void _audio_24_audioMethod()
    {
        myAudio.clip = audio_24;
        myAudio.Play();
    }
    void _audio_25_audioMethod()
    {
        myAudio.clip = audio_25;
        myAudio.Play();
    }
    void _audio_26_audioMethod()
    {
        myAudio.clip = audio_26;
        myAudio.Play();
    }
    void _audio_27_audioMethod()
    {
        myAudio.clip = audio_27;
        myAudio.Play();
    }
    void _audio_28_audioMethod()
    {
        myAudio.clip = audio_28;
        myAudio.Play();
    }
    void _audio_29_audioMethod()
    {
        myAudio.clip = audio_29;
        myAudio.Play();
    }
    void _audio_30_audioMethod()
    {
        myAudio.clip = audio_30;
        myAudio.Play();
    }
    void _audio_31_audioMethod()
    {
        myAudio.clip = audio_31;
        myAudio.Play();
    }
    void _audio_32_audioMethod()
    {
        myAudio.clip = audio_32;
        myAudio.Play();
    }
    void _audio_33_audioMethod()
    {
        myAudio.clip = audio_33;
        myAudio.Play();
    }
    void _audio_34_audioMethod()
    {
        myAudio.clip = audio_34;
        myAudio.Play();
    }
    void _audio_35_audioMethod()
    {
        myAudio.clip = audio_35;
        myAudio.Play();
    }
    void _audio_36_audioMethod()
    {
        myAudio.clip = audio_36;
        myAudio.Play();
    }
    void _audio_37_audioMethod()
    {
        myAudio.clip = audio_37;
        myAudio.Play();
    }
    void _audio_38_audioMethod()
    {
        myAudio.clip = audio_38;
        myAudio.Play();
    }
    void _audio_39_audioMethod()
    {
        myAudio.clip = audio_39;
        myAudio.Play();
    }
    void _audio_40_audioMethod()
    {
        myAudio.clip = audio_40;
        myAudio.Play();
    }
    void _audio_41_audioMethod()
    {
        myAudio.clip = audio_41;
        myAudio.Play();
    }
    void _audio_42_audioMethod()
    {
        myAudio.clip = audio_42;
        myAudio.Play();
    }
    void _audio_43_audioMethod()
    {
        myAudio.clip = audio_43;
        myAudio.Play();
    }
    void _audio_44_audioMethod()
    {
        myAudio.clip = audio_44;
        myAudio.Play();
    }
    void _audio_45_audioMethod()
    {
        myAudio.clip = audio_45;
        myAudio.Play();
    }
    void _audio_46_audioMethod()
    {
        myAudio.clip = audio_46;
        myAudio.Play();
    }
    void _audio_47_audioMethod()
    {
        myAudio.clip = audio_47;
        myAudio.Play();
    }
    void _audio_48_audioMethod()
    {
        myAudio.clip = audio_48;
        myAudio.Play();
    }
    void _audio_49_audioMethod()
    {
        myAudio.clip = audio_49;
        myAudio.Play();
    }
    void _audio_50_audioMethod()
    {
        myAudio.clip = audio_50;
        myAudio.Play();
    }
    void _audio_51_audioMethod()
    {
        myAudio.clip = audio_51;
        myAudio.Play();
    }
    void _audio_52_audioMethod()
    {
        myAudio.clip = audio_52;
        myAudio.Play();
    }
    void _audio_53_audioMethod()
    {
        myAudio.clip = audio_53;
        myAudio.Play();
    }
    void _audio_54_audioMethod()
    {
        myAudio.clip = audio_54;
        myAudio.Play();
    }
    void _audio_55_audioMethod()
    {
        myAudio.clip = audio_55;
        myAudio.Play();
    }
    void _audio_56_audioMethod()
    {
        myAudio.clip = audio_56;
        myAudio.Play();
    }
    void _audio_57_audioMethod()
    {
        myAudio.clip = audio_57;
        myAudio.Play();
    }
    void _audio_58_audioMethod()
    {
        myAudio.clip = audio_58;
        myAudio.Play();
    }
    void _audio_59_audioMethod()
    {
        myAudio.clip = audio_59;
        myAudio.Play();
    }
    void _audio_60_audioMethod()
    {
        myAudio.clip = audio_60;
        myAudio.Play();
    }
    void _audio_61_audioMethod()
    {
        myAudio.clip = audio_61;
        myAudio.Play();
    }
    void _audio_62_audioMethod()
    {
        myAudio.clip = audio_62;
        myAudio.Play();
    }
    void _audio_63_audioMethod()
    {
        myAudio.clip = audio_63;
        myAudio.Play();
    }
    void _audio_64_audioMethod()
    {
        myAudio.clip = audio_64;
        myAudio.Play();
    }
    void _audio_65_audioMethod()
    {
        myAudio.clip = audio_65;
        myAudio.Play();
    }
    void _audio_66_audioMethod()
    {
        myAudio.clip = audio_66;
        myAudio.Play();
    }
    void _audio_67_audioMethod()
    {
        myAudio.clip = audio_67;
        myAudio.Play();
    }
    void _audio_68_audioMethod()
    {
        myAudio.clip = audio_68;
        myAudio.Play();
    }
    void _audio_69_audioMethod()
    {
        myAudio.clip = audio_69;
        myAudio.Play();
    }
    void _audio_70_audioMethod()
    {
        myAudio.clip = audio_70;
        myAudio.Play();
    }
    void _audio_71_audioMethod()
    {
        myAudio.clip = audio_71;
        myAudio.Play();
    }
    void _audio_72_audioMethod()
    {
        myAudio.clip = audio_72;
        myAudio.Play();
    }
    void _audio_73_audioMethod()
    {
        myAudio.clip = audio_73;
        myAudio.Play();
    }
    void _audio_74_audioMethod()
    {
        myAudio.clip = audio_74;
        myAudio.Play();
    }
    void _audio_75_audioMethod()
    {
        myAudio.clip = audio_75;
        myAudio.Play();
    }
    void _audio_76_audioMethod()
    {
        myAudio.clip = audio_76;
        myAudio.Play();
    }
    void _audio_77_audioMethod()
    {
        myAudio.clip = audio_77;
        myAudio.Play();
    }
    void _audio_78_audioMethod()
    {
        myAudio.clip = audio_78;
        myAudio.Play();
    }
    void _audio_79_audioMethod()
    {
        myAudio.clip = audio_79;
        myAudio.Play();
    }
    void _audio_80_audioMethod()
    {
        myAudio.clip = audio_80;
        myAudio.Play();
    }
    void _audio_81_audioMethod()
    {
        myAudio.clip = audio_81;
        myAudio.Play();
    }
    void _audio_82_audioMethod()
    {
        myAudio.clip = audio_82;
        myAudio.Play();
    }
    void _audio_83_audioMethod()
    {
        myAudio.clip = audio_83;
        myAudio.Play();
    }
    void _audio_84_audioMethod()
    {
        myAudio.clip = audio_84;
        myAudio.Play();
    }
    void _audio_85_audioMethod()
    {
        myAudio.clip = audio_85;
        myAudio.Play();
    }
    void _audio_86_audioMethod()
    {
        myAudio.clip = audio_86;
        myAudio.Play();
    }

    private bool hasRained;
    public void MiniGameStart()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MiniGameEnd()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(true);
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

    public void TurnOnLiver()
    {
        if(hasRained)
        {
            StepComplete();
        }
        else
        {
            MissionFailed();
        }
    }

    public GameObject waterObj;
    public void PondFull()
    {
        waterObj.SetActive(true);
    }

    private int index;
    public TargetController waterCycleGame;

    public void WaterCycleMiniGame(MeshRenderer mr)
    {
        mr.enabled = true;
        index++;
        if(index == 3)
        {
            waterCycleGame.EndMiniGame();
            hasRained = true;
            StepComplete();
            PondFull();
        }
    }

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }


































}
