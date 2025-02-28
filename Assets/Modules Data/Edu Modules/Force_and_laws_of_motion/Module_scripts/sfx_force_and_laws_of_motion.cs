using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_force_and_laws_of_motion : MonoBehaviour
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

    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;


    public ObjectiveController objectiveController;
    private NoPlayerMenu noPlayerMenu;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("New Animation", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }

        //==================== INSERT NO-PLAYER Menu  ====================//
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/NotDefaultPlayer_Menu", typeof(GameObject));  // Load No-Player Menu
        JustInstantiatedNoPlayerCanvas = (GameObject)GameObject.Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate No-Player Menu   {  Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);   }
        JustInstantiatedNoPlayerCanvas.SetActive(false);
        //================================================================//

        // Get the NoPlayerMenu component
        noPlayerMenu = JustInstantiatedNoPlayerCanvas.GetComponent<NoPlayerMenu>();
        noPlayerMenu.SetSfxScript(this);


        checkpointManager = GameObject.Find("CheckpointManager");
        checkpointManager.GetComponent<CheckpointManager>().objectiveController = objectiveController;
    }

    private void Start()
    {

        if (isSceneReloaded)
        {
            Skip();
        }
        InitializeFromCheckpoint();

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
            case 1: Level5(); break;
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


    //private void EndGameDelay()
    //{
    //    lv1MiniGame.EndMiniGame();
    //}

   

    public void level()
    {
        lv1MiniGame.Output();
        //Invoke("EndGameDelay", 0.5f);
    }
  
    
    
    
    
    public GameObject lv1;









    public void Level5()
    {
        StartCoroutine(DelayLv5MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        

    }
    IEnumerator DelayLv5MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv5MiniGame.Output();
    }

    public TargetController lv5MiniGame;
    public void Savepoint1()
    {
        SaveProgress(1, 5, 0);
    }


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
        StartCoroutine(MiniGameChangeDelay(miniGame));
    }

    IEnumerator MiniGameChangeDelay(TargetController miniGame)
    {
        foreach(GameObject obj in questions)
        {
            obj.SetActive(false);
        }

        yield return new WaitForSeconds(1.5f);
        previousMiniGame.EndMiniGame();
        previousMiniGame = miniGame;
        miniGame.Output();
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


    //Titles

    public GameObject title1;
    public GameObject title2;
    public GameObject title3;
    public GameObject title4;
    public GameObject title5;
    public GameObject title6;
    public GameObject title7;
    public GameObject title8;
    public GameObject title9;


    public GameObject down1;
    public GameObject down2;
    public GameObject down3;
    public GameObject down4;
    public GameObject down5;
    public GameObject down6;



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







    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip para1;
    public AudioClip para2;
    public AudioClip para3;
    public AudioClip para4;
    public AudioClip para5;
    public AudioClip para6;
    public AudioClip para7;
    public AudioClip para8;
    public AudioClip para9;
    public AudioClip para10;
    public AudioClip para11;
    public AudioClip para12;
    public AudioClip para13;
    public AudioClip para14;
    public AudioClip para15;
    public AudioClip para16;
    public AudioClip para17;
    public AudioClip para18;
    public AudioClip para19;
    public AudioClip para20;
    public AudioClip para21;
    public AudioClip para22;
    public AudioClip para23;
    public AudioClip para24;
    public AudioClip para25;
    public AudioClip para26;
    public AudioClip para27;
    public AudioClip para28;
    public AudioClip para29;
    public AudioClip para30;
    public AudioClip para31;
    public AudioClip para32;
    public AudioClip para33;
    public AudioClip para34;
    public AudioClip para35;
    public AudioClip para36;
    public AudioClip para37;
    public AudioClip para38;
    public AudioClip para39;
    public AudioClip para40;
    public AudioClip para41;
    public AudioClip para42;
    public AudioClip para43;
    public AudioClip para44;
    public AudioClip para45;



    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject push_ani;
    public GameObject pull_ani;
    public GameObject planets_ani;
    public GameObject ball_throw_ani;
    public GameObject balanced_ani;
    public GameObject single_person_ani;
    public GameObject two_person_ani;
    public GameObject unbalanced_ani;
    public GameObject person2_ani;
    public GameObject sp_ani;
    public GameObject box_ani;
    public GameObject first_law_ani;
    public GameObject example_first_law_ani;
    public GameObject lorry_ani;
    public GameObject toy_car_ani;
    public GameObject colliding_ani;
    public GameObject jumping_ani;
    public GameObject car_collision_ani;
    public GameObject car_second_law_ani;
    public GameObject ball_ani;
    public GameObject coin_ani;



    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject N_text;
    public GameObject M_text;
    public GameObject a_text;
    public GameObject b_text;
    public GameObject mathe_text;
    public GameObject cart_text;
    public GameObject toy_car_text;
    public GameObject toy_expla_text;
    public GameObject third_law_formula;
    public GameObject third_text;
    public GameObject car_colli_text;
    public GameObject car_for_text;
    public GameObject one_meter_text;
    public GameObject newton_text;
    public GameObject boat_text;
    public GameObject second_law_text;
    public GameObject opp_direction_text;
    public GameObject last_text;
    public GameObject sec_text;
    public GameObject acc_text;
    public GameObject secondlaw_text;
    public GameObject laws_text;
    public GameObject v_text;

  



    void _title1_MethodON()
    {
        title1.SetActive(true);
    }

    void _title1_MethodOFF()
    {
        title1.SetActive(false);
    }


    void _title2_MethodON()
    {
        title2.SetActive(true);
    }

    void _title2_MethodOFF()
    {
        title2.SetActive(false);
    }

    void _title3_MethodON()
    {
        title3.SetActive(true);
    }

    void _title3_MethodOFF()
    {
        title3.SetActive(false);
    }



    void _title4_MethodON()
    {
        title4.SetActive(true);
    }

    void _title4_MethodOFF()
    {
        title4.SetActive(false);
    }


    void _title5_MethodON()
    {
        title5.SetActive(true);
    }

    void _title5_MethodOFF()
    {
        title5.SetActive(false);
    }


    void _title6_MethodON()
    {
        title6.SetActive(true);
    }

    void _title6_MethodOFF()
    {
        title6.SetActive(false);
    }



    void _title7_MethodON()
    {
        title7.SetActive(true);
    }

    void _title7_MethodOFF()
    {
        title7.SetActive(false);
    }


    void _title8_MethodON()
    {
        title8.SetActive(true);
    }

    void _title8_MethodOFF()
    {
        title8.SetActive(false);
    }



    void _title9_MethodON()
    {
        title9.SetActive(true);
    }

    void _title9_MethodOFF()
    {
        title9.SetActive(false);
    }




    void _down1_MethodON()
    {
        down1.SetActive(true);
    }

    void _down1_MethodOFF()
    {
        down1.SetActive(false);
    }



    void _down2_MethodON()
    {
        down2.SetActive(true);
    }

    void _down2_MethodOFF()
    {
        down2.SetActive(false);
    }



    void _down3_MethodON()
    {
        down3.SetActive(true);
    }

    void _down3_MethodOFF()
    {
        down3.SetActive(false);
    }



    void _down4_MethodON()
    {
        down4.SetActive(true);
    }

    void _down4_MethodOFF()
    {
        down4.SetActive(false);
    }



    void _down5_MethodON()
    {
        down5.SetActive(true);
    }

    void _down5_MethodOFF()
    {
        down5.SetActive(false);
    }



    void _down6_MethodON()
    {
        down6.SetActive(true);
    }

    void _down6_MethodOFF()
    {
        down6.SetActive(false);
    }










    //Audio play

    void para1_method()
    {
        myAudio.clip = para1;
        myAudio.Play();
    }

    //

    void para2_method()
    {
        myAudio.clip = para2;
        myAudio.Play();
    }

    //

    void para3_method()
    {
        myAudio.clip = para3;
        myAudio.Play();
    }


    //

    void para4_method()
    {
        myAudio.clip = para4;
        myAudio.Play();
    }


    //

    void para5_method()
    {
        myAudio.clip = para5;
        myAudio.Play();
    }



    //

    void para6_method()
    {
        myAudio.clip = para6;
        myAudio.Play();
    }


    //

    void para7_method()
    {
        myAudio.clip = para7;
        myAudio.Play();
    }


    //

    void para8_method()
    {
        myAudio.clip = para8;
        myAudio.Play();
    }


    //

    void para9_method()
    {
        myAudio.clip = para9;
        myAudio.Play();
    }


    //

    void para10_method()
    {
        myAudio.clip = para10;
        myAudio.Play();
    }
    //

    void para11_method()
    {
        myAudio.clip = para11;
        myAudio.Play();
    }


    //

    void para12_method()
    {
        myAudio.clip = para12;
        myAudio.Play();
    }


    //

    void para13_method()
    {
        myAudio.clip = para13;
        myAudio.Play();
    }


    //

    void para14_method()
    {
        myAudio.clip = para14;
        myAudio.Play();
    }


    //

    void para15_method()
    {
        myAudio.clip = para15;
        myAudio.Play();
    }



    //

    void para16_method()
    {
        myAudio.clip = para16;
        myAudio.Play();
    }



    //

    void para17_method()
    {
        myAudio.clip = para17;
        myAudio.Play();
    }


    //

    void para18_method()
    {
        myAudio.clip = para18;
        myAudio.Play();
    }



    //

    void para19_method()
    {
        myAudio.clip = para19;
        myAudio.Play();
    }



    //

    void para20_method()
    {
        myAudio.clip = para20;
        myAudio.Play();
    }



    //

    void para21_method()
    {
        myAudio.clip = para21;
        myAudio.Play();
    }


    //

    void para22_method()
    {
        myAudio.clip = para22;
        myAudio.Play();
    }



    //

    void para23_method()
    {
        myAudio.clip = para23;
        myAudio.Play();
    }



    //

    void para24_method()
    {
        myAudio.clip = para24;
        myAudio.Play();
    }


    //

    void para25_method()
    {
        myAudio.clip = para25;
        myAudio.Play();
    }


    //

    void para26_method()
    {
        myAudio.clip = para26;
        myAudio.Play();
    }




    //

    void para27_method()
    {
        myAudio.clip = para27;
        myAudio.Play();
    }


    //

    void para28_method()
    {
        myAudio.clip = para28;
        myAudio.Play();
    }


    //

    void para29_method()
    {
        myAudio.clip = para29;
        myAudio.Play();
    }



    //

    void para30_method()
    {
        myAudio.clip = para30;
        myAudio.Play();
    }



    //

    void para31_method()
    {
        myAudio.clip = para31;
        myAudio.Play();
    }


    //

    void para32_method()
    {
        myAudio.clip = para32;
        myAudio.Play();
    }



    //

    void para33_method()
    {
        myAudio.clip = para33;
        myAudio.Play();
    }


    //

    void para34_method()
    {
        myAudio.clip = para34;
        myAudio.Play();
    }

    void para35_method()
    {
        myAudio.clip = para35;
        myAudio.Play();
    }

    //

    void para36_method()
    {
        myAudio.clip = para36;
        myAudio.Play();
    }

    //

    void para37_method()
    {
        myAudio.clip = para37;
        myAudio.Play();
    }


    //

    void para38_method()
    {
        myAudio.clip = para38;
        myAudio.Play();
    }

    //

    void para39_method()
    {
        myAudio.clip = para39;
        myAudio.Play();
    }


    //

    void para40_method()
    {
        myAudio.clip = para40;
        myAudio.Play();
    }


    //

    void para41_method()
    {
        myAudio.clip = para41;
        myAudio.Play();
    }


    //

    void para42_method()
    {
        myAudio.clip = para42;
        myAudio.Play();
    }


    //

    void para43_method()
    {
        myAudio.clip = para43;
        myAudio.Play();
    }


    //

    void para44_method()
    {
        myAudio.clip = para44;
        myAudio.Play();
    }


    //

    void para45_method()
    {
        myAudio.clip = para45;
        myAudio.Play();
    }







    //Animation Play

    void _push_anianimMethod()
    {
        anim = push_ani.GetComponent<Animator>();
        anim.Play("push_ani");
    }


    void _pull_anianimMethod()
    {
        anim = pull_ani.GetComponent<Animator>();
        anim.Play("pull_ani");
    }


    void _planets_anianimMethod()
    {
        anim = planets_ani.GetComponent<Animator>();
        anim.Play("planets_ani");
    }

    void _ball_throw_anianimMethod()
    {
        anim = ball_throw_ani.GetComponent<Animator>();
        anim.Play("ball_throw_ani");
    }

    void _balanced_anianimMethod()
    {
        anim = balanced_ani.GetComponent<Animator>();
        anim.Play("balanced_ani");
    }


    void _single_personanianimMethod()
    {
        anim = single_person_ani.GetComponent<Animator>();
        anim.Play("single_person_ani");
    }

    void _two_personanianimMethod()
    {
        anim = two_person_ani.GetComponent<Animator>();
        anim.Play("two_person_ani");
    }

    void _unbalanced_animMethod()
    {
        anim = unbalanced_ani.GetComponent<Animator>();
        anim.Play("unbalanced_forces_ani");
    }


    void _person2_animMethod()
    {
        anim = person2_ani.GetComponent<Animator>();
        anim.Play("person2_ani");
    }


    void _sp_animMethod()
    {
        anim = sp_ani.GetComponent<Animator>();
        anim.Play("sp_ani");
    }


    void _box_animMethod()
    {
        anim = box_ani.GetComponent<Animator>();
        anim.Play("box_ani");
    }


    void _first_law_animMethod()
    {
        anim = first_law_ani.GetComponent<Animator>();
        anim.Play("first_law_ani");
    }


    void _example_first_law_animMethod()
    {
        anim = example_first_law_ani.GetComponent<Animator>();
        anim.Play("example_first_law_ani");
    }


    void _lorry_animMethod()
    {
        anim = lorry_ani.GetComponent<Animator>();
        anim.Play("lorry_ani");
    }

    void _toy_car_animMethod()
    {
        anim = toy_car_ani.GetComponent<Animator>();
        anim.Play("toy_car_ani");
    }


    void _colliding_animMethod()
    {
        anim = colliding_ani.GetComponent<Animator>();
        anim.Play("colliding_ani");
    }


    void _jumping_animMethod()
    {
        anim = jumping_ani.GetComponent<Animator>();
        anim.Play("jumping_ani");
    }


    void _car_collision_ani_animMethod()
    {
        anim = car_collision_ani.GetComponent<Animator>();
        anim.Play("car_collision_ani");
    }


    void _car_second_law_ani_animMethod()
    {
        anim = car_second_law_ani.GetComponent<Animator>();
        anim.Play("car_secondlaw_ani");
    }

    void _ball_ani_animMethod()
    {
        anim = ball_ani.GetComponent<Animator>();
        anim.Play("ball_ani");
    }


    void _coin_ani_animMethod()
    {
        anim = coin_ani.GetComponent<Animator>();
        anim.Play("coin_ani");
    }










    void _N_text_MethodON()
    {
        N_text.SetActive(true);
    }

    void _N_text_MethodOFF()
    {
        N_text.SetActive(false);
    }


    void _M_text_MethodON()
    {
        M_text.SetActive(true);
    }

    void _M_text_MethodOFF()
    {
        M_text.SetActive(false);
    }



    void _a_text_MethodON()
    {
        a_text.SetActive(true);
    }

    void _a_text_MethodOFF()
    {
        a_text.SetActive(false);
    }


    void _b_text_MethodON()
    {
        b_text.SetActive(true);
    }

    void _b_text_MethodOFF()
    {
        b_text.SetActive(false);
    }



    void _mathe_text_MethodON()
    {
        mathe_text.SetActive(true);
    }

    void _mathe_text_MethodOFF()
    {
        mathe_text.SetActive(false);
    }


    void _cart_text_MethodON()
    {
        cart_text.SetActive(true);
    }

    void _cart_text_MethodOFF()
    {
        cart_text.SetActive(false);
    }


    void _toy_car_text_MethodON()
    {
        toy_car_text.SetActive(true);
    }

    void _toy_car_text_MethodOFF()
    {
        toy_car_text.SetActive(false);
    }


    void _toy_expla_text_MethodON()
    {
        toy_expla_text.SetActive(true);
    }

    void _toy_expla_text_MethodOFF()
    {
        toy_expla_text.SetActive(false);
    }


    void _third_law_formula_MethodON()
    {
        third_law_formula.SetActive(true);
    }

    void _third_law_formula_MethodOFF()
    {
        third_law_formula.SetActive(false);
    }


    void _third_text_MethodON()
    {
        third_text.SetActive(true);
    }

    void _third_text_MethodOFF()
    {
        third_text.SetActive(false);
    }

    void _car_colli_text_MethodON()
    {
        car_colli_text.SetActive(true);
    }

    void _car_colli_text_MethodOFF()
    {
        car_colli_text.SetActive(false);
    }


    void _car_for_text_MethodON()
    {
        car_for_text.SetActive(true);
    }

    void _car_for_text_MethodOFF()
    {
        car_for_text.SetActive(false);
    }


    void _one_meter_text_MethodON()
    {
        one_meter_text.SetActive(true);
    }

    void _one_meter_text_MethodOFF()
    {
        one_meter_text.SetActive(false);
    }


    void _newton_text_MethodON()
    {
        newton_text.SetActive(true);
    }

    void _newton_text_MethodOFF()
    {
        newton_text.SetActive(false);
    }

    void _boat_text_MethodON()
    {
        boat_text.SetActive(true);
    }

    void _boat_text_MethodOFF()
    {
        boat_text.SetActive(false);
    }


    void _second_law_text_MethodON()
    {
        second_law_text.SetActive(true);
    }

    void _second_law_text_MethodOFF()
    {
        second_law_text.SetActive(false);
    }



    void _opp_direction_text_MethodON()
    {
        opp_direction_text.SetActive(true);
    }

    void _opp_direction_text_MethodOFF()
    {
        opp_direction_text.SetActive(false);
    }


    void _last_text_MethodON()
    {
        last_text.SetActive(true);
    }

    void _last_text_MethodOFF()
    {
        last_text.SetActive(false);
    }


    void _sec_text_MethodON()
    {
        sec_text.SetActive(true);
    }

    void _sec_text_MethodOFF()
    {
        sec_text.SetActive(false);
    }


    void _acc_text_MethodON()
    {
        acc_text.SetActive(true);
    }

    void _acc_text_MethodOFF()
    {
        acc_text.SetActive(false);
    }



    void _secondlaw_text_MethodON()
    {
        secondlaw_text.SetActive(true);
    }

    void _secondlaw_text_MethodOFF()
    {
        secondlaw_text.SetActive(false);
    }


    void _laws_text_MethodON()
    {
        laws_text.SetActive(true);
    }

    void _laws_text_MethodOFF()
    {
        laws_text.SetActive(false);
    }



    void _v_text_MethodON()
    {
        v_text.SetActive(true);
    }

    void _v_text_MethodOFF()
    {
        v_text.SetActive(false);
    }










}
