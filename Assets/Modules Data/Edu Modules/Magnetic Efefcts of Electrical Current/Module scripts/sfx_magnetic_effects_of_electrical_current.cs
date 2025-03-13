using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sfx_magnetic_effects_of_electrical_current : MonoBehaviour
{


    //public Transform wayPoint1;
    //public MissionWaypoint waypoint;
    //public GameObject waypointCanvas;

    //public void SetWayPoint(Transform target)
    //{
    //    waypoint.player = InventoryManager.Instance.player.transform;
    //    waypointCanvas.SetActive(true);
    //    waypoint.target = target;
    //}

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;

    public ObjectiveController objectiveController;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("Cam anim", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }

        checkpointManager = GameObject.Find("CheckpointManager");
        checkpointManager.GetComponent<CheckpointManager>().objectiveController = objectiveController;
    }

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
            case 1: Level3(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
    }



    


    private void EndGameDelay()
    {
        miniGame.EndMiniGame();
    }

    public TargetController miniGame;

    public void level()
    {
        miniGame.Output();
        Invoke("EndGameDelay", 0.5f);
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

        level();
        InitializeFromCheckpoint();
        //SetWayPoint(wayPoint1);
        //StartCoroutine(DelayLv7MiniGameStart());
        //Level7();

    }

    public Camera cam;
    public LayerMask layerMask;
    private bool canChoose = true;
    public int index;

    
    private void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }


        if (Input.GetMouseButtonDown(0) && canChoose)
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
            {
                if (raycastHit.collider != null)
                {
                    Debug.Log(raycastHit.collider.gameObject.name);
                    if (raycastHit.collider.gameObject.name == "Correct")
                    {
                        miniGames.EndMiniGame();
                        canChoose = false;
                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }
        
    }

    public TargetController miniGames;
    private int miniGameIndex = 0;
    public List<GameObject> questions = new List<GameObject>();

    public void CallTargetController(TargetController targetController)
    {
        StartCoroutine(DelayCallTargetController(targetController));
    }

    IEnumerator DelayCallTargetController(TargetController targetController)
    {
        yield return new WaitForSeconds(1);
        targetController.Output();
    }
    public void TurnOnGOWithDelay(GameObject obj)
    {
        StartCoroutine(ObjectTurnOnDelay(obj));
    }


    IEnumerator ObjectTurnOnDelay(GameObject obj)
    {
        foreach (GameObject objec in questions)
        {
            objec.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        obj.SetActive(true);
    }

    public void ChangeCamHolder(Transform camHolder)
    {
        transform.position = camHolder.position;
        transform.rotation = camHolder.rotation;
    }

    public void ChangeCamHolderWithDelay(Transform camHolder)
    {
        StartCoroutine(ChangeCamHolderDelay(camHolder));
    }

    IEnumerator ChangeCamHolderDelay(Transform camHolder)
    {
        yield return new WaitForSeconds(5);
        transform.position = camHolder.position;
        transform.rotation = camHolder.rotation;
    }

    public void DelayStepComplete()
    {
        Invoke("StepComplete", 2f);
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

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

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void PlayAnimTrigg(Animator anim)
    {
        anim.SetTrigger("Trigger"); // Replace "TriggerName" with the actual name of your trigger
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetBool("Bool", true);
    }

    public void EndAnim(Animator anim)
    {
        anim.SetBool("Bool", false);
    }

    public Transform SpawnPoint1;

    public void Level1()
    {
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        StartCoroutine(DelaySpawnPoint());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    IEnumerator DelaySpawnPoint()
    {
        yield return new WaitForSeconds(2);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint1);


    }

    public void Level2()
    {
        index++;
        

        if (index == 4)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public GameObject QL2;
    public void Level2Completed()
    {
        index++;


        if (index == 2)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            QL2.SetActive(true);
            //Invoke("Level3", 2);
            
        }
    }

    public Transform SpawnPoint2;
    public void Level3()
    {
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint2);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    public void Level3Completed()
    {
        index++;


        if (index == 6)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Invoke("Level4", 2);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public Transform SpawnPoint3;

    public void Level4()
    {
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        StartCoroutine(DelaySpawnPointTransform());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    IEnumerator DelaySpawnPointTransform()
    {
        yield return new WaitForSeconds(2);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint3);
        

    }

    private int compassindex = 0;

    public void Level4Completed()
    {
        compassindex++;


        if (compassindex == 4)
        {
            
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public GameObject Compass1;
    public GameObject Compass2;
    public GameObject Compass3;
    public GameObject Compass4;
    public GameObject pipe1;
    public GameObject pipe2;
    public GameObject pipe3;
    public Collider option1;
    public Collider option2;
    public Collider option3;
    public Collider option4;

    public GameObject Lv5;

    public void Level4Finish()
    {
        index++;

        if (index == 1)
        {
            Compass1.SetActive(true);
            pipe1.SetActive(true);
            option1.enabled = false;
            option2.enabled = true;
            
        }

        if (index == 2)
        {
            Compass2.SetActive(true);
            pipe2.SetActive(true);
            option2.enabled = false;
            option3.enabled = true;

        }

        if (index == 3)
        {
            Compass3.SetActive(true);
            pipe3.SetActive(true);
            option3.enabled = false;
            option4.enabled = true;

        }

        if (index == 4)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Invoke("Level5", 4);
            Compass4.SetActive(true);
            Lv5.SetActive(true);
            option4.enabled = false;
            index = 0;
            
            

        }

        
    }
    public void Level5()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint2);
    }
    public void Level5Completed()
    {
        index++;


        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Invoke("Level7", 2);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public TargetController lv7MiniGame;
    public void Level7()
    {
        StartCoroutine(DelayLv7MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv7MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv7MiniGame.Output();
    }

    

    public TargetController lv8MiniGame;
    public void Level8()
    {
        StartCoroutine(DelayLv8MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv8MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv8MiniGame.Output();
    }

    public void Level8Completed()
    {
        index++;


        if (index == 1)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Invoke("Level9", 2);

        }
    }

    public TargetController lv9MiniGame;
    public void Level9()
    {
        StartCoroutine(DelayLv9MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv9MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv9MiniGame.Output();
    }

    public Transform SpawnPoint4;
    public GameObject lv10;

    public void Level10()
    {
        lv10.SetActive(true);
        StartCoroutine(DelayLevel10());
        
    }

    IEnumerator DelayLevel10()
    {
        yield return new WaitForSeconds(1f);
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint4);

    }


    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject random;
    public GameObject pattern;
    public GameObject poles;
    public GameObject linesz;
    public GameObject linesf;
    public GameObject lineso;
    public GameObject compasse;
    public GameObject compasse2;
    public GameObject compasse3;
    public GameObject compassez;
    public GameObject dot1;
    public GameObject dot2;
    public GameObject text;
    public GameObject circular;
    public GameObject circular_compass;
    public GameObject energyEm;
    public GameObject energyWm;
    public GameObject compassEm;
    public GameObject compassWm;
    public GameObject NM;
    public GameObject SM;
    public GameObject DF;






    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]

    public GameObject motor_anim;
    public GameObject generator_anim;
    public GameObject G1_anim;
    public GameObject G2_anim;
    public GameObject G3_anim;
    public GameObject fuse_anim;
    public GameObject galv_anim;
    public GameObject compass_anim;
    public GameObject compass2_anim;
    public GameObject repel_anim;
    public GameObject attract_anim;
    public GameObject arrow1_anim;
    public GameObject arrow2_anim;
    public GameObject energy_anim;
    public GameObject compasss_anim;
    public GameObject energyE;
    public GameObject energyW;
    public GameObject compassE;
    public GameObject compassW;
    public GameObject rheostat_anim;
    public GameObject ammeter_anim;
    public GameObject paper_anim;
    public GameObject compassdef_anim;
    public GameObject arrow_1_anim;
    public GameObject arrow_2_anim;
    public GameObject solenoidanim;
    public GameObject solenoidballanim;












    //Titles


    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject T6;
    public GameObject T7;
    public GameObject T8;
    public GameObject T9;
    public GameObject T10;
    public GameObject T11;
    public GameObject T12;
    public GameObject T13;
    public GameObject T14;
    public GameObject T15;
    public GameObject T16;
    public GameObject T17;
    public GameObject T18;
    public GameObject T19;



    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;
    public GameObject D6;
    public GameObject D7;
    public GameObject D8;
    public GameObject D9;
    public GameObject D10;
    public GameObject D11;
    public GameObject D12;


    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private bool isMuted = false;

    public void ToggleAudio()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
        Debug.Log("Audio Muted: " + isMuted);
    }

    public void _Jump_To1(float value)
    {
        ToggleAudio();
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


    public AudioClip audio_1b;
    public AudioClip audio_2b;
    public AudioClip audio_3b;
    public AudioClip audio_4b;
    public AudioClip audio_5b;
    public AudioClip audio_6b;
    public AudioClip audio_7b;
    public AudioClip audio_8b;
    public AudioClip audio_9b;
    public AudioClip audio_10b;
    public AudioClip audio_11b;
    public AudioClip audio_12b;
    public AudioClip audio_13b;
    public AudioClip audio_14b;
    public AudioClip audio_15b;
    public AudioClip audio_16b;
    public AudioClip audio_17b;
    public AudioClip audio_18b;
    public AudioClip audio_19b;
    public AudioClip audio_20b;
    public AudioClip audio_21b;
    public AudioClip audio_22b;
    public AudioClip audio_23b;
    public AudioClip audio_24b;
    public AudioClip audio_25b;
    public AudioClip audio_26b;
    public AudioClip audio_27b;
    public AudioClip audio_28b;
    public AudioClip audio_29b;
    public AudioClip audio_30b;
    public AudioClip audio_31b;
    public AudioClip audio_32b;
    public AudioClip audio_33b;
    public AudioClip audio_34b;
    public AudioClip audio_35b;
    public AudioClip audio_36b;
    public AudioClip audio_37b;
    public AudioClip audio_38b;
    public AudioClip audio_39b;
    public AudioClip audio_40b;
    public AudioClip audio_41b;
    public AudioClip audio_42b;
    public AudioClip audio_43b;
    public AudioClip audio_44b;
    public AudioClip audio_45b;
    public AudioClip audio_46b;
    public AudioClip audio_47b;
    public AudioClip audio_48b;
    public AudioClip audio_49b;
    public AudioClip audio_50b;
    public AudioClip audio_51b;
    public AudioClip audio_52b;
    public AudioClip audio_53b;
    public AudioClip audio_54b;
    public AudioClip audio_55b;
    public AudioClip audio_56b;
    public AudioClip audio_57b;
    public AudioClip audio_58b;
    public AudioClip audio_59b;
    public AudioClip audio_60b;
    public AudioClip audio_61b;
    public AudioClip audio_62b;
    public AudioClip audio_63b;
    public AudioClip audio_64b;
    public AudioClip audio_65b;
    public AudioClip audio_66b;
    public AudioClip audio_67b;
    public AudioClip audio_68b;
    public AudioClip audio_69b;
    public AudioClip audio_70b;
    public AudioClip audio_71b;
    public AudioClip audio_72b;
    public AudioClip audio_73b;
    public AudioClip audio_74b;
    public AudioClip audio_75b;
    public AudioClip audio_76b;
    public AudioClip audio_77b;
    public AudioClip audio_78b;
    public AudioClip audio_79b;
    public AudioClip audio_80b;
    public AudioClip audio_81b;
    public AudioClip audio_82b;
    public AudioClip audio_83b;
    public AudioClip audio_84b;
    public AudioClip audio_85b;
    public AudioClip audio_86b;
    public AudioClip audio_87b;
    public AudioClip audio_88b;
    public AudioClip audio_89b;
    public AudioClip audio_90b;
    public AudioClip audio_91b;
    public AudioClip audio_92b;
    public AudioClip audio_93b;
    public AudioClip audio_94b;
    public AudioClip audio_95b;
    public AudioClip audio_96b;
    public AudioClip audio_97b;
    public AudioClip audio_98b;
    public AudioClip audio_99b;
    public AudioClip audio_100b;
    public AudioClip audio_101b;
    public AudioClip audio_102b;
    public AudioClip audio_103b;
    public AudioClip audio_104b;
    public AudioClip audio_105b;
    public AudioClip audio_106b;
    public AudioClip audio_107b;
    public AudioClip audio_108b;























    // Method ON/OFF


    void _DF_MethodON()
    {
        DF.SetActive(true);
    }

    void _DF_MethodoOFF()
    {
        DF.SetActive(false);
    }









    // Animations



    void _motor_anim_animAnimmethod()
    {

        anim = motor_anim.GetComponent<Animator>();
        anim.Play("Motor anim");

    }

    void _generator_anim_animAnimmethod()
    {

        anim = generator_anim.GetComponent<Animator>();
        anim.Play("Generator anim");

    }

    void _G1_anim_animAnimmethod()
    {

        anim = G1_anim.GetComponent<Animator>();
        anim.Play("G1 anim");

    }

    void _G2_anim_animAnimmethod()
    {

        anim = G2_anim.GetComponent<Animator>();
        anim.Play("G2 anim");
    }

    void _G3_anim_animAnimmethod()
    {

        anim = G3_anim.GetComponent<Animator>();
        anim.Play("G3 anim");
    }




    void _fuse_anim_animAnimmethod()
    {

        anim = fuse_anim.GetComponent<Animator>();
        anim.Play("Fuse anim");
    }

    void _galv_anim_animAnimmethod()
    {

        anim = galv_anim.GetComponent<Animator>();
        anim.Play("Galvanometer anim");
    }

    void _solenoid_anim_animAnimmethod()
    {

        anim = solenoidanim.GetComponent<Animator>();
        anim.Play("Solenoid anim");
    }

    void _solenoid_ball_anim_animAnimmethod()
    {

        anim = solenoidballanim.GetComponent<Animator>();
        anim.Play("Solenoid ball anim");
    }

    void _arrow1_anim_animAnimmethod()
    {

        anim = arrow_1_anim.GetComponent<Animator>();
        anim.Play("Arrow 1 anim");
    }

    void _arrow2_anim_animAnimmethod()
    {

        anim = arrow_2_anim.GetComponent<Animator>();
        anim.Play("Arrow 2 anim");
    }







    void _rheostat_anim_animAnimmethod()
    {

        anim = rheostat_anim.GetComponent<Animator>();
        anim.Play("Rheostat anim");
    }

    void _ammeter_anim_animAnimmethod()
    {

        anim = ammeter_anim.GetComponent<Animator>();
        anim.Play("Ammeter anim");
    }

    void _paper_anim_animAnimmethod()
    {

        anim = paper_anim.GetComponent<Animator>();
        anim.Play("Circular paper anim");
    }

    void _compassdef_anim_animAnimmethod()
    {

        anim = compassdef_anim.GetComponent<Animator>();
        anim.Play("Compass two anim");
    }





    void _EnergyE_animAnimmethod()
    {

        anim = energyE.GetComponent<Animator>();
        anim.Play("East anim");
    }


    void _EnergyW_animAnimmethod()
    {

        anim = energyW.GetComponent<Animator>();
        anim.Play("West anim");
    }

    void _CompassE_animAnimmethod()
    {

        anim = compassE.GetComponent<Animator>();
        anim.Play("Compass E anim");
    }

    void _CompassW_animAnimmethod()
    {

        anim = compassW.GetComponent<Animator>();
        anim.Play("Compass W anim");
    }








    void _Energy_animAnimmethod()
    {

        anim = energy_anim.GetComponent<Animator>();
        anim.Play("Energy anim");



        energy_anim.SetActive(true);


    }


    void _Compasss_animAnimmethod()
    {

        anim = compasss_anim.GetComponent<Animator>();
        anim.Play("Compasss anim");



        compasss_anim.SetActive(true);


    }










    void _Compass_animAnimmethod()
    {

        anim = compass_anim.GetComponent<Animator>();
        anim.Play("Compass anim");
    }

    void _Compass2_animAnimmethod()
    {

        anim = compass2_anim.GetComponent<Animator>();
        anim.Play("Compass 2 anim");
    }

    void _repel_animAnimmethod()
    {

        anim = repel_anim.GetComponent<Animator>();
        anim.Play("Repel anim");
    }

    void _attract_animAnimmethod()
    {

        anim = attract_anim.GetComponent<Animator>();
        anim.Play("Attract anim");
    }

    void _arrow1_animAnimmethod()
    {

        anim = arrow1_anim.GetComponent<Animator>();
        anim.Play("Arrow anim");
    }

    void _arrow2_animAnimmethod()
    {

        anim = arrow2_anim.GetComponent<Animator>();
        anim.Play("Arrow 2 anim");
    }














    // Method ON/OFF















    void _SM_MethodON()
    {
        SM.SetActive(true);
    }

    void _SM_MethodoOFF()
    {
        SM.SetActive(false);
    }

    void _NM_MethodON()
    {
        NM.SetActive(true);
    }

    void _NM_MethodoOFF()
    {
        NM.SetActive(false);
    }



    void _energyEm_MethodON()
    {
        energyEm.SetActive(true);
    }

    void _energyEm_MethodoOFF()
    {
        energyEm.SetActive(false);
    }


    void _energyWm_MethodON()
    {
        energyWm.SetActive(true);
    }

    void _energyWm_MethodoOFF()
    {
        energyWm.SetActive(false);
    }



    void _compassEm_MethodON()
    {
        compassEm.SetActive(true);
    }

    void _compassWm_MethodON()
    {
        compassWm.SetActive(true);
    }









    void _circular_MethodON()
    {
        circular.SetActive(true);
    }

    void _circular_compass_MethodON()
    {
        circular_compass.SetActive(true);
    }




    void _text_MethodON()
    {
        text.SetActive(true);
    }
    void _text_MethodoOFF()
    {
        text.SetActive(false);
    }

    void _random_MethodON()
    {
        random.SetActive(true);
    }
    void _random_MethodoOFF()
    {
        random.SetActive(false);
    }

    void _pattern_MethodON()
    {
        pattern.SetActive(true);
    }
    void _pattern_MethodoOFF()
    {
        pattern.SetActive(false);
    }

    void _poles_MethodON()
    {
        poles.SetActive(true);
    }
    void _poles_MethodoOFF()
    {
        poles.SetActive(false);
    }

    void _linesz_MethodON()
    {
        linesz.SetActive(true);
    }
    void _linesz_MethodoOFF()
    {
        linesz.SetActive(false);
    }

    void _lineso_MethodON()
    {
        lineso.SetActive(true);
    }
    void _lineso_MethodoOFF()
    {
        lineso.SetActive(false);
    }

    void _linesf_MethodON()
    {
        linesf.SetActive(true);
    }
    void _linesf_MethodoOFF()
    {
        linesf.SetActive(false);
    }

    void _compasse_MethodON()
    {
        compasse.SetActive(true);
    }
    void _compasse_MethodoOFF()
    {
        compasse.SetActive(false);
    }

    void _compasse2_MethodON()
    {
        compasse2.SetActive(true);
    }
    void _compasse2_MethodoOFF()
    {
        compasse2.SetActive(false);
    }

    void _compasse3_MethodON()
    {
        compasse3.SetActive(true);
    }
    void _compasse3_MethodoOFF()
    {
        compasse3.SetActive(false);
    }

    void _compassez_MethodON()
    {
        compassez.SetActive(true);
    }
    void _compassez_MethodoOFF()
    {
        compassez.SetActive(false);
    }

    void _dot1_MethodON()
    {
        dot1.SetActive(true);
    }
    void _dot1_MethodoOFF()
    {
        dot1.SetActive(false);
    }

    void _dot2_MethodON()
    {
        dot2.SetActive(true);
    }
    void _dot2_MethodoOFF()
    {
        dot2.SetActive(false);
    }






















    //Titles




    //
    void _T1_MethodON()
    {
        T1.SetActive(true);
    }
    void _T1_MethodoOFF()
    {
        T1.SetActive(false);
    }
    //
    void _T2_MethodON()
    {
        T2.SetActive(true);
    }
    void _T2_MethodoOFF()
    {
        T2.SetActive(false);
    }
    //
    void _T3_MethodON()
    {
        T3.SetActive(true);
    }
    
    void _T4_MethodON()
    {
        T4.SetActive(true);
    }
    
    //
    void _T5_MethodON()
    {
        T5.SetActive(true);
    }
    //
    void _T6_MethodON()
    {
        T6.SetActive(true);
    }
    
    //
    void _T7_MethodON()
    {
        T7.SetActive(true);
    }

    void _T8_MethodON()
    {
        T8.SetActive(true);
    }

    void _T9_MethodON()
    {
        T9.SetActive(true);
    }

    void _T10_MethodON()
    {
        T10.SetActive(true);
    }

    void _T11_MethodON()
    {
        T11.SetActive(true);
    }

    void _T12_MethodON()
    {
        T12.SetActive(true);
    }

    void _T13_MethodON()
    {
        T13.SetActive(true);
    }
    void _T13_MethodoOFF()
    {
        T13.SetActive(false);
    }

    void _T14_MethodON()
    {
        T14.SetActive(true);
    }
    void _T14_MethodoOFF()
    {
        T14.SetActive(false);
    }

    void _T15_MethodON()
    {
        T15.SetActive(true);
    }
    void _T15_MethodoOFF()
    {
        T15.SetActive(false);
    }
    void _T16_MethodON()
    {
        T16.SetActive(true);
    }
    void _T16_MethodoOFF()
    {
        T16.SetActive(false);
    }
    void _T17_MethodON()
    {
        T17.SetActive(true);
    }
    void _T17_MethodoOFF()
    {
        T17.SetActive(false);
    }
    void _T18_MethodON()
    {
        T18.SetActive(true);
    }
    void _T18_MethodoOFF()
    {
        T18.SetActive(false);
    }
    void _T19_MethodON()
    {
        T19.SetActive(true);
    }
    void _T19_MethodoOFF()
    {
        T19.SetActive(false);
    }









    // Descriptions


    //
    void _D1_MethodON()
    {
        D1.SetActive(true);
    }
    void _D1_MethodoOFF()
    {
        D1.SetActive(false);
    }
    //
    void _D2_MethodON()
    {
        D2.SetActive(true);
    }
    void _D2_MethodoOFF()
    {
        D2.SetActive(false);
    }
    //
    void _D3_MethodON()
    {
        D3.SetActive(true);
    }
    void _D3_MethodoOFF()
    {
        D3.SetActive(false);
    }
    //
    void _D4_MethodON()
    {
        D4.SetActive(true);
    }
    void _D4_MethodoOFF()
    {
        D4.SetActive(false);
    }
    //
    void _D5_MethodON()
    {
        D5.SetActive(true);
    }
    void _D5_MethodoOFF()
    {
        D5.SetActive(false);
    }
    //
    void _D6_MethodON()
    {
        D6.SetActive(true);
    }
    void _D6_MethodoOFF()
    {
        D6.SetActive(false);
    }
    //
    void _D7_MethodON()
    {
        D7.SetActive(true);
    }
    void _D7_MethodoOFF()
    {
        D7.SetActive(false);
    }
    //
    void _D8_MethodON()
    {
        D8.SetActive(true);
    }
    void _D8_MethodoOFF()
    {
        D8.SetActive(false);
    }
    //
    void _D9_MethodON()
    {
        D9.SetActive(true);
    }
    void _D9_MethodoOFF()
    {
        D9.SetActive(false);
    }
    //
    void _D10_MethodON()
    {
        D10.SetActive(true);
    }
    void _D10_MethodoOFF()
    {
        D10.SetActive(false);
    }
    //
    void _D11_MethodON()
    {
        D11.SetActive(true);
    }
    void _D11_MethodoOFF()
    {
        D11.SetActive(false);
    }
    //
    void _D12_MethodON()
    {
        D12.SetActive(true);
    }
    void _D12_MethodoOFF()
    {
        D12.SetActive(false);
    }


































    // Audio



    void _audio_1b_audioMethod()
    {
        myAudio.clip = audio_1b;
        myAudio.Play();
    }
    void _audio_2b_audioMethod()
    {
        myAudio.clip = audio_2b;
        myAudio.Play();
    }
    void _audio_3b_audioMethod()
    {
        myAudio.clip = audio_3b;
        myAudio.Play();
    }
    void _audio_4b_audioMethod()
    {
        myAudio.clip = audio_4b;
        myAudio.Play();
    }
    void _audio_5b_audioMethod()
    {
        myAudio.clip = audio_5b;
        myAudio.Play();
    }
    void _audio_6b_audioMethod()
    {
        myAudio.clip = audio_6b;
        myAudio.Play();
    }
    void _audio_7b_audioMethod()
    {
        myAudio.clip = audio_7b;
        myAudio.Play();
    }
    void _audio_8b_audioMethod()
    {
        myAudio.clip = audio_8b;
        myAudio.Play();
    }
    void _audio_9b_audioMethod()
    {
        myAudio.clip = audio_9b;
        myAudio.Play();
    }
    void _audio_10b_audioMethod()
    {
        myAudio.clip = audio_10b;
        myAudio.Play();
    }

    void _audio_11b_audioMethod()
    {
        myAudio.clip = audio_11b;
        myAudio.Play();
    }
    void _audio_12b_audioMethod()
    {
        myAudio.clip = audio_12b;
        myAudio.Play();
    }
    void _audio_13b_audioMethod()
    {
        myAudio.clip = audio_13b;
        myAudio.Play();
    }
    void _audio_14b_audioMethod()
    {
        myAudio.clip = audio_14b;
        myAudio.Play();
    }
    void _audio_15b_audioMethod()
    {
        myAudio.clip = audio_15b;
        myAudio.Play();
    }
    void _audio_16b_audioMethod()
    {
        myAudio.clip = audio_16b;
        myAudio.Play();
    }
    void _audio_17b_audioMethod()
    {
        myAudio.clip = audio_17b;
        myAudio.Play();
    }
    void _audio_18b_audioMethod()
    {
        myAudio.clip = audio_18b;
        myAudio.Play();
    }
    void _audio_19b_audioMethod()
    {
        myAudio.clip = audio_19b;
        myAudio.Play();
    }
    void _audio_20b_audioMethod()
    {
        myAudio.clip = audio_20b;
        myAudio.Play();
    }
    void _audio_21b_audioMethod()
    {
        myAudio.clip = audio_21b;
        myAudio.Play();
    }
    void _audio_22b_audioMethod()
    {
        myAudio.clip = audio_22b;
        myAudio.Play();
    }
    void _audio_23b_audioMethod()
    {
        myAudio.clip = audio_23b;
        myAudio.Play();
    }
    void _audio_24b_audioMethod()
    {
        myAudio.clip = audio_24b;
        myAudio.Play();
    }
    void _audio_25b_audioMethod()
    {
        myAudio.clip = audio_25b;
        myAudio.Play();
    }
    void _audio_26b_audioMethod()
    {
        myAudio.clip = audio_26b;
        myAudio.Play();
    }
    void _audio_27b_audioMethod()
    {
        myAudio.clip = audio_27b;
        myAudio.Play();
    }
    void _audio_28b_audioMethod()
    {
        myAudio.clip = audio_28b;
        myAudio.Play();
    }
    void _audio_29b_audioMethod()
    {
        myAudio.clip = audio_29b;
        myAudio.Play();
    }
    void _audio_30b_audioMethod()
    {
        myAudio.clip = audio_30b;
        myAudio.Play();
    }
    void _audio_31b_audioMethod()
    {
        myAudio.clip = audio_31b;
        myAudio.Play();
    }
    void _audio_32b_audioMethod()
    {
        myAudio.clip = audio_32b;
        myAudio.Play();
    }
    void _audio_33b_audioMethod()
    {
        myAudio.clip = audio_33b;
        myAudio.Play();
    }
    void _audio_34b_audioMethod()
    {
        myAudio.clip = audio_34b;
        myAudio.Play();
    }
    void _audio_35b_audioMethod()
    {
        myAudio.clip = audio_35b;
        myAudio.Play();
    }
    void _audio_36b_audioMethod()
    {
        myAudio.clip = audio_36b;
        myAudio.Play();
    }
    void _audio_37b_audioMethod()
    {
        myAudio.clip = audio_37b;
        myAudio.Play();
    }
    void _audio_38b_audioMethod()
    {
        myAudio.clip = audio_38b;
        myAudio.Play();
    }
    void _audio_39b_audioMethod()
    {
        myAudio.clip = audio_39b;
        myAudio.Play();
    }
    void _audio_40b_audioMethod()
    {
        myAudio.clip = audio_40b;
        myAudio.Play();
    }
    void _audio_41b_audioMethod()
    {
        myAudio.clip = audio_41b;
        myAudio.Play();
    }
    void _audio_42b_audioMethod()
    {
        myAudio.clip = audio_42b;
        myAudio.Play();
    }
    void _audio_43b_audioMethod()
    {
        myAudio.clip = audio_43b;
        myAudio.Play();
    }
    void _audio_44b_audioMethod()
    {
        myAudio.clip = audio_44b;
        myAudio.Play();
    }

    void _audio_45b_audioMethod()
    {
        myAudio.clip = audio_45b;
        myAudio.Play();
    }
    void _audio_46b_audioMethod()
    {
        myAudio.clip = audio_46b;
        myAudio.Play();
    }
    void _audio_47b_audioMethod()
    {
        myAudio.clip = audio_47b;
        myAudio.Play();
    }
    void _audio_48b_audioMethod()
    {
        myAudio.clip = audio_48b;
        myAudio.Play();
    }
    void _audio_49b_audioMethod()
    {
        myAudio.clip = audio_49b;
        myAudio.Play();
    }
    void _audio_50b_audioMethod()
    {
        myAudio.clip = audio_50b;
        myAudio.Play();
    }
    void _audio_51b_audioMethod()
    {
        myAudio.clip = audio_51b;
        myAudio.Play();
    }
    void _audio_52b_audioMethod()
    {
        myAudio.clip = audio_52b;
        myAudio.Play();
    }
    void _audio_53b_audioMethod()
    {
        myAudio.clip = audio_53b;
        myAudio.Play();
    }
    void _audio_54b_audioMethod()
    {
        myAudio.clip = audio_54b;
        myAudio.Play();
    }
    void _audio_55b_audioMethod()
    {
        myAudio.clip = audio_55b;
        myAudio.Play();
    }
    void _audio_56b_audioMethod()
    {
        myAudio.clip = audio_56b;
        myAudio.Play();
    }
    void _audio_57b_audioMethod()
    {
        myAudio.clip = audio_57b;
        myAudio.Play();
    }
    void _audio_58b_audioMethod()
    {
        myAudio.clip = audio_58b;
        myAudio.Play();
    }
    void _audio_59b_audioMethod()
    {
        myAudio.clip = audio_59b;
        myAudio.Play();
    }
    void _audio_60b_audioMethod()
    {
        myAudio.clip = audio_60b;
        myAudio.Play();
    }
    void _audio_61b_audioMethod()
    {
        myAudio.clip = audio_61b;
        myAudio.Play();
    }
    void _audio_62b_audioMethod()
    {
        myAudio.clip = audio_62b;
        myAudio.Play();
    }
    void _audio_63b_audioMethod()
    {
        myAudio.clip = audio_63b;
        myAudio.Play();
    }
    void _audio_64b_audioMethod()
    {
        myAudio.clip = audio_64b;
        myAudio.Play();
    }
    void _audio_65b_audioMethod()
    {
        myAudio.clip = audio_65b;
        myAudio.Play();
    }
    void _audio_66b_audioMethod()
    {
        myAudio.clip = audio_66b;
        myAudio.Play();
    }
    void _audio_67b_audioMethod()
    {
        myAudio.clip = audio_67b;
        myAudio.Play();
    }
    void _audio_68b_audioMethod()
    {
        myAudio.clip = audio_68b;
        myAudio.Play();
    }
    void _audio_69b_audioMethod()
    {
        myAudio.clip = audio_69b;
        myAudio.Play();
    }
    void _audio_70b_audioMethod()
    {
        myAudio.clip = audio_70b;
        myAudio.Play();
    }
    void _audio_71b_audioMethod()
    {
        myAudio.clip = audio_71b;
        myAudio.Play();
    }
    void _audio_72b_audioMethod()
    {
        myAudio.clip = audio_72b;
        myAudio.Play();
    }
    void _audio_73b_audioMethod()
    {
        myAudio.clip = audio_73b;
        myAudio.Play();
    }
    void _audio_74b_audioMethod()
    {
        myAudio.clip = audio_74b;
        myAudio.Play();
    }
    void _audio_75b_audioMethod()
    {
        myAudio.clip = audio_75b;
        myAudio.Play();
    }
    void _audio_76b_audioMethod()
    {
        myAudio.clip = audio_76b;
        myAudio.Play();
    }
    void _audio_77b_audioMethod()
    {
        myAudio.clip = audio_77b;
        myAudio.Play();
    }
    void _audio_78b_audioMethod()
    {
        myAudio.clip = audio_78b;
        myAudio.Play();
    }
    void _audio_79b_audioMethod()
    {
        myAudio.clip = audio_79b;
        myAudio.Play();
    }
    void _audio_80b_audioMethod()
    {
        myAudio.clip = audio_80b;
        myAudio.Play();
    }
    void _audio_81b_audioMethod()
    {
        myAudio.clip = audio_81b;
        myAudio.Play();
    }
    void _audio_82b_audioMethod()
    {
        myAudio.clip = audio_82b;
        myAudio.Play();
    }
    void _audio_83b_audioMethod()
    {
        myAudio.clip = audio_83b;
        myAudio.Play();
    }
    void _audio_84b_audioMethod()
    {
        myAudio.clip = audio_84b;
        myAudio.Play();
    }
    void _audio_85b_audioMethod()
    {
        myAudio.clip = audio_85b;
        myAudio.Play();
    }
    void _audio_86b_audioMethod()
    {
        myAudio.clip = audio_86b;
        myAudio.Play();
    }
    void _audio_87b_audioMethod()
    {
        myAudio.clip = audio_87b;
        myAudio.Play();
    }
    void _audio_88b_audioMethod()
    {
        myAudio.clip = audio_88b;
        myAudio.Play();
    }
    void _audio_89b_audioMethod()
    {
        myAudio.clip = audio_89b;
        myAudio.Play();
    }
    void _audio_90b_audioMethod()
    {
        myAudio.clip = audio_90b;
        myAudio.Play();
    }
    void _audio_91b_audioMethod()
    {
        myAudio.clip = audio_91b;
        myAudio.Play();
    }
    void _audio_92b_audioMethod()
    {
        myAudio.clip = audio_92b;
        myAudio.Play();
    }
    void _audio_93b_audioMethod()
    {
        myAudio.clip = audio_93b;
        myAudio.Play();
    }
    void _audio_94b_audioMethod()
    {
        myAudio.clip = audio_94b;
        myAudio.Play();
    }
    void _audio_95b_audioMethod()
    {
        myAudio.clip = audio_95b;
        myAudio.Play();
    }
    void _audio_96b_audioMethod()
    {
        myAudio.clip = audio_96b;
        myAudio.Play();
    }
    void _audio_97b_audioMethod()
    {
        myAudio.clip = audio_97b;
        myAudio.Play();
    }
    void _audio_98b_audioMethod()
    {
        myAudio.clip = audio_98b;
        myAudio.Play();
    }
    void _audio_99b_audioMethod()
    {
        myAudio.clip = audio_99b;
        myAudio.Play();
    }
    void _audio_100b_audioMethod()
    {
        myAudio.clip = audio_100b;
        myAudio.Play();
    }
    void _audio_101b_audioMethod()
    {
        myAudio.clip = audio_101b;
        myAudio.Play();
    }
    void _audio_102b_audioMethod()
    {
        myAudio.clip = audio_102b;
        myAudio.Play();
    }
    void _audio_103b_audioMethod()
    {
        myAudio.clip = audio_103b;
        myAudio.Play();
    }
    void _audio_104b_audioMethod()
    {
        myAudio.clip = audio_104b;
        myAudio.Play();
    }
    void _audio_105b_audioMethod()
    {
        myAudio.clip = audio_105b;
        myAudio.Play();
    }
    void _audio_106b_audioMethod()
    {
        myAudio.clip = audio_106b;
        myAudio.Play();
    }
    void _audio_107b_audioMethod()
    {
        myAudio.clip = audio_107b;
        myAudio.Play();
    }
    void _audio_108b_audioMethod()
    {
        myAudio.clip = audio_108b;
        myAudio.Play();
    }
















































































































}
