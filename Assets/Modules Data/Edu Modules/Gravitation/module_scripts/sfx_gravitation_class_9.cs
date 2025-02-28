using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_gravitation_class_9 : MonoBehaviour
{
    public TargetController lv1MiniGame;
    public List<GameObject> questions;
    private TargetController previousMiniGame;

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

    public AudioSource gameSoundFxSource;
    public List<AudioClip> level1AudioClips;
    private int index;

    private GameObject JustInstantiatedNoPlayerCanvas;

    private void Awake()
    {

        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camera anim 1", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }


        //==================== INSERT NO-PLAYER Menu  ====================//
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/NotDefaultPlayer_Menu", typeof(GameObject));  // Load No-Player Menu
        JustInstantiatedNoPlayerCanvas = (GameObject)GameObject.Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate No-Player Menu   {  Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);   }
        JustInstantiatedNoPlayerCanvas.SetActive(false);
        //================================================================//

        checkpointManager = GameObject.Find("CheckpointManager");
        checkpointManager.GetComponent<CheckpointManager>().objectiveController = objectiveController;
    }

    

    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;


    public ObjectiveController objectiveController;
    private NoPlayerMenu noPlayerMenu;

    

    private void Start()
    {

        if (isSceneReloaded)
        {
            Skip();
            InitializeFromCheckpoint();
        }
        

    }

    public void RestartFromCheckPoint()
    {

        isSceneReloaded = true;
        Debug.Log("RestartFromCheckpoint is called from sfx_How_do_organisms_reproduce_10th_class");
        InitializeFromCheckpoint();
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(1);
        Debug.Log("Scene loaded");

        StartCoroutine(RestoreAfterReload());
    }

    IEnumerator RestoreAfterReload()
    {

        yield return new WaitForSeconds(0.1f); // Wait for one frame to ensure the scene reloads fully
        InitializeFromCheckpoint();
    }

    public void RestartFomStart()
    {
        // Mark the scene as reloaded
        isSceneReloaded = true;
        CheckpointManager.Instance.ResetCheckpoints();
        RestartFromCheckPoint();
    }

    public void DestroyCheckpoint()
    {
        isSceneReloaded = false;
        Destroy(checkpointManager.gameObject);
    }

    private bool shouldSkipLevel1 = false;

    private void InitializeFromCheckpoint()
    {
        var (checkpoint, currentStep, currentObjective) = CheckpointManager.Instance.LoadCheckpoint();
        CheckpointManager.Instance.RestoreCheckpoint();

        shouldSkipLevel1 = checkpoint != 0 || currentStep != 0 || currentObjective != 0;


        switch (checkpoint)
        {
            case 0: level(); break;
            //case 1: Level4(); break;
            //case 1: Level8(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
    }

    //Skip() {level()}




    private void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }
    }
    public void level()
    {
        lv1MiniGame.Output();

    }

    public GameObject lv1;






    

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
        JustInstantiatedNoPlayerCanvas.SetActive(true);

        InitializeFromCheckpoint();
        level();
        
        previousMiniGame = lv1MiniGame;
    }
    public void MiniGameStart()
    {
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

    public void ChangeMiniGame(TargetController miniGame)
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        StartCoroutine(MiniGameChangeDelay(miniGame));
    }

    IEnumerator MiniGameChangeDelay(TargetController miniGame)
    {
        foreach (GameObject obj in questions)
        {
            obj.SetActive(false);
        }

        yield return new WaitForSeconds(1.5f);
        previousMiniGame.EndMiniGame();
        previousMiniGame = miniGame;
        miniGame.Output();
    }

    public void ChangePlayerPos(Transform point)
    {
        InventoryManager.Instance.player.ChangePosition(point);
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void TurnOnGODelay(GameObject obj)
    {
        StartCoroutine(GOTurnOnDelay(obj));
    }

    IEnumerator GOTurnOnDelay(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MissionFailed()
    {
        // Call the missionFailed method in ObjectiveController
        objectiveController.missionFailed();
        GameObject missionFailedPrefabObj = GameObject.Find("Mission Failed(Clone)"); // Ensure this name matches the instantiated object
        if (missionFailedPrefabObj != null)
        {
            MenuSystem menuSystem = missionFailedPrefabObj.GetComponent<MenuSystem>();
            if (menuSystem != null)
            {
                // Assign this script to the MenuSystem's sfxScript property
                menuSystem.SetSfxScript(this);
            }
        }
    }

    public void StepComp()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        
    }





    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip audio1;
    public AudioClip audio2;
    public AudioClip audio3;
    public AudioClip audio4;
    public AudioClip audio5;
    public AudioClip audio6;
    public AudioClip audio7;
    public AudioClip audio8;
    public AudioClip audio9;
    public AudioClip audio10;
    public AudioClip audio11;
    public AudioClip audio12;
    public AudioClip audio13;
    public AudioClip audio14;
    public AudioClip audio15;
    public AudioClip audio16;
    public AudioClip audio17;
    public AudioClip audio18;
    public AudioClip audio19;
    public AudioClip audio20;
    public AudioClip audio21;
    public AudioClip audio22;
    public AudioClip audio23;
    public AudioClip audio24;
    public AudioClip audio25;
    public AudioClip audio26;
    public AudioClip audio27;
    public AudioClip audio28;
    public AudioClip audio29;
    public AudioClip audio30;
    public AudioClip audio31;
    public AudioClip audio32;
    public AudioClip audio33;
    public AudioClip audio34;
    public AudioClip audio35;
    public AudioClip audio36;
    public AudioClip audio37;
    public AudioClip audio38;
    public AudioClip audio39;
    public AudioClip audio40;
    public AudioClip audio41;
    public AudioClip audio42;
    public AudioClip audio43;
    public AudioClip audio44;
    public AudioClip audio45;
    public AudioClip audio46;
    public AudioClip audio47;
    public AudioClip audio48;
    public AudioClip audio49;
    public AudioClip audio50;
    public AudioClip audio51;
    public AudioClip audio52;
    public AudioClip audio53;
    public AudioClip audio54;

    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;
    public GameObject Ball_anim;
    public GameObject stone_anim;
    public GameObject apple_anim;
    public GameObject earth_and_moon_anim;
    public GameObject Ring_animation;
    public GameObject sun_and_earth_anim;
    public GameObject four_arrow_anim;
    public GameObject garbage_anim;
    public GameObject down_arrow_anim;
    public GameObject sky_anim;
    public GameObject ammeter;
    public GameObject frel_fall_anim;
    public GameObject aeroplane_anim;
    public GameObject walking_ani;
    public GameObject fluied_anim;
    public GameObject spere_beaker_anim;
    public GameObject square_beaker;

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject soft_balls;
    public GameObject boy;
    public GameObject Intro_T;
    public GameObject force_of_grav_T;
    public GameObject cemtripetal_T;
    public GameObject universal_l_g_T;
    public GameObject unit_value_T;
    public GameObject importance_T;
    public GameObject Free_fall_T;
    public GameObject Acceleration_due_T;
    public GameObject Difference_w_and_m_T;
    public GameObject Mass_T;
    public GameObject weight_T;
    public GameObject freely_fall_T;
    public GameObject thrust_T;
    public GameObject Pressure_T;
    public GameObject use_presure_T;
    public GameObject presure_fluds_T;
    public GameObject buoncy_T;
    public GameObject Factors_affec_T;
    public GameObject Archimedes_principle_T;
    public GameObject Relative_Density_T;
    public GameObject evry_thing_D;
    public GameObject sir_issac_newton_D;
    public GameObject the_force_D;
    public GameObject the_force_due_to_D;
    public GameObject the_value_D;
    public GameObject Thrust_and_pressure_T;
    public GameObject the_upwards_D;

   

    void audio1_method()
    {
        myAudio.clip = audio1;
        myAudio.Play();
    }

    void audio2_method()
    {
        myAudio.clip = audio2;
        myAudio.Play();
    }

    void audio3_method()
    {
        myAudio.clip = audio3;
        myAudio.Play();
    }

    void audio4_method()
    {
        myAudio.clip = audio4;
        myAudio.Play();
    }

    void audio5_method()
    {
        myAudio.clip = audio5;
        myAudio.Play();
    }

    void audio6_method()
    {
        myAudio.clip = audio6;
        myAudio.Play();
    }

    void audio7_method()
    {
        myAudio.clip = audio7;
        myAudio.Play();
    }

    void audio8_method()
    {
        myAudio.clip = audio8;
        myAudio.Play();
    }

    void audio9_method()
    {
        myAudio.clip = audio9;
        myAudio.Play();
    }

    //

    void audio10_method()
    {
        myAudio.clip = audio10;
        myAudio.Play();
    }

    //

    void audio11_method()
    {
        myAudio.clip = audio11;
        myAudio.Play();
    }

    //

    void audio12_method()
    {
        myAudio.clip = audio12;
        myAudio.Play();
    }

    //

    void audio13_method()
    {
        myAudio.clip = audio13;
        myAudio.Play();
    }

    //

    void audio14_method()
    {
        myAudio.clip = audio14;
        myAudio.Play();
    }

    //

    void audio15_method()
    {
        myAudio.clip = audio15;
        myAudio.Play();
    }

    //

    void audio16_method()
    {
        myAudio.clip = audio16;
        myAudio.Play();
    }

    //

    void audio17_method()
    {
        myAudio.clip = audio17;
        myAudio.Play();
    }

    //

    void audio18_method()
    {
        myAudio.clip = audio18;
        myAudio.Play();
    }

    //

    void audio19_method()
    {
        myAudio.clip = audio19;
        myAudio.Play();
    }

    //

    void audio20_method()
    {
        myAudio.clip = audio20;
        myAudio.Play();
    }

    //

    void audio21_method()
    {
        myAudio.clip = audio21;
        myAudio.Play();
    }

    //

    void audio22_method()
    {
        myAudio.clip = audio22;
        myAudio.Play();
    }

    //

    void audio23_method()
    {
        myAudio.clip = audio23;
        myAudio.Play();
    }

    //

    void audio24_method()
    {
        myAudio.clip = audio24;
        myAudio.Play();
    }

    //

    void audio25_method()
    {
        myAudio.clip = audio25;
        myAudio.Play();
    }

    //

    void audio26_method()
    {
        myAudio.clip = audio26;
        myAudio.Play();
    }

    //

    void audio27_method()
    {
        myAudio.clip = audio27;
        myAudio.Play();
    }

    //

    void audio28_method()
    {
        myAudio.clip = audio28;
        myAudio.Play();
    }

    //

    void audio29_method()
    {
        myAudio.clip = audio29;
        myAudio.Play();
    }

    //

    void audio30_method()
    {
        myAudio.clip = audio30;
        myAudio.Play();
    }

    //

    void audio31_method()
    {
        myAudio.clip = audio31;
        myAudio.Play();
    }

    //

    void audio32_method()
    {
        myAudio.clip = audio32;
        myAudio.Play();
    }

    //

    void audio33_method()
    {
        myAudio.clip = audio33;
        myAudio.Play();
    }

    //

    void audio34_method()
    {
        myAudio.clip = audio34;
        myAudio.Play();
    }

    //

    void audio35_method()
    {
        myAudio.clip = audio35;
        myAudio.Play();
    }

    //

    void audio36_method()
    {
        myAudio.clip = audio36;
        myAudio.Play();
    }

    //

    void audio37_method()
    {
        myAudio.clip = audio37;
        myAudio.Play();
    }

    //

    void audio38_method()
    {
        myAudio.clip = audio38;
        myAudio.Play();
    }

    //

    void audio39_method()
    {
        myAudio.clip = audio39;
        myAudio.Play();
    }

    //

    void audio40_method()
    {
        myAudio.clip = audio40;
        myAudio.Play();
    }

    //

    void audio41_method()
    {
        myAudio.clip = audio41;
        myAudio.Play();
    }

    //

    void audio42_method()
    {
        myAudio.clip = audio42;
        myAudio.Play();
    }

    //

    void audio43_method()
    {
        myAudio.clip = audio43;
        myAudio.Play();
    }

    //

    void audio44_method()
    {
        myAudio.clip = audio44;
        myAudio.Play();
    }

    //

    void audio45_method()
    {
        myAudio.clip = audio45;
        myAudio.Play();
    }

    //

    void audio46_method()
    {
        myAudio.clip = audio46;
        myAudio.Play();
    }

    //

    void audio47_method()
    {
        myAudio.clip = audio47;
        myAudio.Play();
    }

    //

    void audio48_method()
    {
        myAudio.clip = audio48;
        myAudio.Play();
    }

    //

    void audio49_method()
    {
        myAudio.clip = audio49;
        myAudio.Play();
    }

    //

    void audio50_method()
    {
        myAudio.clip = audio50;
        myAudio.Play();
    }

    //

    void audio51_method()
    {
        myAudio.clip = audio51;
        myAudio.Play();
    }

    //

    void audio52_method()
    {
        myAudio.clip = audio52;
        myAudio.Play();
    }

    //

    void audio53_method()
    {
        myAudio.clip = audio53;
        myAudio.Play();
    }

    //

    void audio54_method()
    {
        myAudio.clip = audio54;
        myAudio.Play();
    }











    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    

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

































    //Animation Play

    void Ball_anim_Method()
    {
        anim = Ball_anim.GetComponent<Animator>();
        anim.Play("Ball anim");
    }

    void stone_anim_Method()
    {
        anim = stone_anim.GetComponent<Animator>();
        anim.Play("stone anim");
    }

    void apple_anim_Method()
    {
        anim = apple_anim.GetComponent<Animator>();
        anim.Play("apple anim");
    }

    void earth_and_moon_anim_Method()
    {
        anim = earth_and_moon_anim.GetComponent<Animator>();
        anim.Play("earth and moon anim");
    }

    void Ring_animation_Method()
    {
        anim = Ring_animation.GetComponent<Animator>();
        anim.Play("Ring animation");
    }

    void sun_and_earth_anim_Method()
    {
        anim = sun_and_earth_anim.GetComponent<Animator>();
        anim.Play("sun and earth anim");
    }

    void four_arrow_anim_Method()
    {
        anim = four_arrow_anim.GetComponent<Animator>();
        anim.Play("4 arrow anim");
    }

    void garbage_anim_Method()
    {
        anim = garbage_anim.GetComponent<Animator>();
        anim.Play("garbage anim");
    }

    void down_arrow_anim_Method()
    {
        anim = down_arrow_anim.GetComponent<Animator>();
        anim.Play("down arrow anim");
    }

    void sky_anim_Method()
    {
        anim = sky_anim.GetComponent<Animator>();
        anim.Play("sky anim");
    }

    void ammeter_Method()
    {
        anim = ammeter.GetComponent<Animator>();
        anim.Play("ammeter");
    }

    void frel_fall_anim_Method()
    {
        anim = frel_fall_anim.GetComponent<Animator>();
        anim.Play("frel fall anim");
    }

    void aeroplane_anim_Method()
    {
        anim = aeroplane_anim.GetComponent<Animator>();
        anim.Play("aeroplane anim");
    }

    void walking_ani_Method()
    {
        anim = walking_ani.GetComponent<Animator>();
        anim.Play("walking ani");
    }

    void fluied_anim_Method()
    {
        anim = fluied_anim.GetComponent<Animator>();
        anim.Play("fluied anim");
    }

    void spere_beaker_anim_Method()
    {
        anim = spere_beaker_anim.GetComponent<Animator>();
        anim.Play("spere beaker anim");
    }

    void square_beaker_Method()
    {
        anim = square_beaker.GetComponent<Animator>();
        anim.Play("square beaker");
    }








    // ON/OFF object







    void _soft_balls_MethodON()
    {
        soft_balls.SetActive(true);
    }

    void _soft_balls_MethodOFF()
    {
        soft_balls.SetActive(false);
    }


    void _boy_MethodON()
    {
        boy.SetActive(true);
    }

    void _boy_MethodOFF()
    {
        boy.SetActive(false);
    }







    // ON/OFF Title & Discription





    void _Intro_T_MethodON()
    {
        Intro_T.SetActive(true);
    }

    void _Intro_T_MethodOFF()
    {
        Intro_T.SetActive(false);
    }


    void _force_of_grav_T_MethodON()
    {
        force_of_grav_T.SetActive(true);
    }

    void _force_of_grav_T_MethodOFF()
    {
        force_of_grav_T.SetActive(false);
    }


    void _cemtripetal_T_MethodON()
    {
        cemtripetal_T.SetActive(true);
    }

    void _cemtripetal_T_MethodOFF()
    {
        cemtripetal_T.SetActive(false);
    }


    void _universal_l_g_T_MethodON()
    {
        universal_l_g_T.SetActive(true);
    }

    void _universal_l_g_T_MethodOFF()
    {
        universal_l_g_T.SetActive(false);
    }


    void _unit_value_T_MethodON()
    {
        unit_value_T.SetActive(true);
    }

    void _unit_value_T_MethodOFF()
    {
        unit_value_T.SetActive(false);
    }


    void _importance_T_MethodON()
    {
        importance_T.SetActive(true);
    }

    void _importance_T_MethodOFF()
    {
        importance_T.SetActive(false);
    }


    void _Free_fall_T_MethodON()
    {
        Free_fall_T.SetActive(true);
    }

    void _Free_fall_T_MethodOFF()
    {
        Free_fall_T.SetActive(false);
    }


    void _Acceleration_due_T_MethodON()
    {
        Acceleration_due_T.SetActive(true);
    }

    void _Acceleration_due_T_MethodOFF()
    {
        Acceleration_due_T.SetActive(false);
    }


    void _Difference_w_and_m_T_MethodON()
    {
        Difference_w_and_m_T.SetActive(true);
    }

    void _Difference_w_and_m_T_MethodOFF()
    {
        Difference_w_and_m_T.SetActive(false);
    }


    void _Mass_T_MethodON()
    {
        Mass_T.SetActive(true);
    }

    void _Mass_T_MethodOFF()
    {
        Mass_T.SetActive(false);
    }


    void _weight_T_MethodON()
    {
        weight_T.SetActive(true);
    }

    void _weight_T_MethodOFF()
    {
        weight_T.SetActive(false);
    }


    void _freely_fall_T_MethodON()
    {
        freely_fall_T.SetActive(true);
    }

    void _freely_fall_T_MethodOFF()
    {
        freely_fall_T.SetActive(false);
    }


    void _thrust_T_MethodON()
    {
        thrust_T.SetActive(true);
    }

    void _thrust_T_MethodOFF()
    {
        thrust_T.SetActive(false);
    }


    void _Pressure_T_MethodON()
    {
        Pressure_T.SetActive(true);
    }

    void _Pressure_T_MethodOFF()
    {
        Pressure_T.SetActive(false);
    }


    void _use_presure_T_MethodON()
    {
        use_presure_T.SetActive(true);
    }

    void _use_presure_T_MethodOFF()
    {
        use_presure_T.SetActive(false);
    }


    void _presure_fluds_T_MethodON()
    {
        presure_fluds_T.SetActive(true);
    }

    void _presure_fluds_T_MethodOFF()
    {
        presure_fluds_T.SetActive(false);
    }


    void _buoncy_T_MethodON()
    {
        buoncy_T.SetActive(true);
    }

    void _buoncy_T_MethodOFF()
    {
        buoncy_T.SetActive(false);
    }


    void _Factors_affec_T_MethodON()
    {
        Factors_affec_T.SetActive(true);
    }

    void _Factors_affec_T_MethodOFF()
    {
        Factors_affec_T.SetActive(false);
    }


    void _Archimedes_principle_T_MethodON()
    {
        Archimedes_principle_T.SetActive(true);
    }

    void _Archimedes_principle_T_MethodOFF()
    {
        Archimedes_principle_T.SetActive(false);
    }


    void _Relative_Density_T_MethodON()
    {
        Relative_Density_T.SetActive(true);
    }

    void _Relative_Density_T_MethodOFF()
    {
        Relative_Density_T.SetActive(false);
    }


    void _evry_thing_D_MethodON()
    {
        evry_thing_D.SetActive(true);
    }

    void _evry_thing_D_MethodOFF()
    {
        evry_thing_D.SetActive(false);
    }


    void _sir_issac_newton_D_MethodON()
    {
        sir_issac_newton_D.SetActive(true);
    }

    void _sir_issac_newton_D_MethodOFF()
    {
        sir_issac_newton_D.SetActive(false);
    }


    void _the_force_D_MethodON()
    {
        the_force_D.SetActive(true);
    }

    void _the_force_D_MethodOFF()
    {
        the_force_D.SetActive(false);
    }


    void _the_force_due_to_D_MethodON()
    {
        the_force_due_to_D.SetActive(true);
    }

    void _the_force_due_to_D_MethodOFF()
    {
        the_force_due_to_D.SetActive(false);
    }


    void _the_value_D_MethodON()
    {
        the_value_D.SetActive(true);
    }

    void _the_value_D_MethodOFF()
    {
        the_value_D.SetActive(false);
    }


    void _Thrust_and_pressure_T_MethodON()
    {
        Thrust_and_pressure_T.SetActive(true);
    }

    void _Thrust_and_pressure_T_MethodOFF()
    {
        Thrust_and_pressure_T.SetActive(false);
    }


    void _the_upwards_D_T_MethodON()
    {
        the_upwards_D.SetActive(true);
    }

    void _the_upwards_D_MethodOFF()
    {
        the_upwards_D.SetActive(false);
    }



















}
