using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sfx_sound_c9 : MonoBehaviour
{
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
            animator.Play("camera_anim", 0, targetNormalizedTime);
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
            case 1: Level7(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
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

        InitializeFromCheckpoint();
        level();

    }
    
    public void PresistentReloadScene()
    {
        //GameObject loader = GameObject.Find("Sceneloader Canvas");
        //loader.GetComponent<SceneLoader>().LoadScene(1);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public GameObject lv1;

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
        yield return new WaitForSeconds(3);
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

    public void EndGameDelay()
    {
        miniGames.EndMiniGame();
    }

    

    public void level()
    {
        miniGames.Output();
        Invoke("EndGameDelay", 1f);

    }


    public void Level1()
    {
        StartCoroutine(DelayLv1MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv1MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv1MiniGame.Output();
    }
    public TargetController lv1MiniGame;

    public Transform Spawnpoint2;

    public void Level2Player()
    {
        lv1MiniGame.EndMiniGame();
        InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(Spawnpoint2);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    

    public void Level2()
    {
        StartCoroutine(DelayLv2MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv2MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv2MiniGame.Output();
    }
    public TargetController lv2MiniGame;

    public void Level3()
    {
        StartCoroutine(DelayLv3MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv3MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv3MiniGame.Output();
    }
    public TargetController lv3MiniGame;

    public void Level4()
    {
        StartCoroutine(DelayLv4MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv4MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv4MiniGame.Output();
    }
    public TargetController lv4MiniGame;

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

    public void Level6()
    {
        StartCoroutine(DelayLv6MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv6MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv6MiniGame.Output();
    }
    public TargetController lv6MiniGame;

    public void Level7()
    {
        StartCoroutine(DelayLv7MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(1, 0, 6);
    }
    IEnumerator DelayLv7MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv7MiniGame.Output();
    }
    public TargetController lv7MiniGame;

    public Transform Spawnpoint8;

    public void Level8Player()
    {
        lv7MiniGame.EndMiniGame();
        InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(Spawnpoint8);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

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
    public TargetController lv8MiniGame;

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
    public TargetController lv9MiniGame;

    public void Level10()
    {
        StartCoroutine(DelayLv10MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv10MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv10MiniGame.Output();
    }
    public TargetController lv10MiniGame;

    public void Level11()
    {
        StartCoroutine(DelayLv11MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv11MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv11MiniGame.Output();
    }
    public TargetController lv11MiniGame;

    public void Level12()
    {
        StartCoroutine(DelayLv12MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv12MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv12MiniGame.Output();
    }
    public TargetController lv12MiniGame;




    //public GameObject ql1;
    ////public TargetController lv1MiniGame;
    public void Lv12TurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 6)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    //public void Level2()
    //{
    //    StartCoroutine(DelayLv2MiniGameStart());
    //    InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    //}
    //IEnumerator DelayLv2MiniGameStart()
    //{
    //    yield return new WaitForSeconds(1);
    //    lv2MiniGame.Output();
    //}


    //public TargetController lv2MiniGame;

    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject c1;
    public GameObject c2;
    public GameObject c3;
    public GameObject lw;
    public GameObject tw;
    public GameObject title_1;
    public GameObject title_2;
    public GameObject title_3;
    public GameObject title_4;
    public GameObject title_5;
    public GameObject title_6;
    public GameObject title_7;
    public GameObject title_8;
    public GameObject title_9;
    public GameObject title_10;
    public GameObject title_11;
    public GameObject title_12;
    public GameObject title_13;
    public GameObject title_14;
    public GameObject title_15;
    public GameObject title_16;
    public GameObject title_17;
    public GameObject title_18;
    public GameObject exp;
    public GameObject cloud;
    public GameObject mc;
    public GameObject line_1;
    public GameObject line_2;
    public GameObject line_3;
    public GameObject line_4;
    public GameObject waves;
    public GameObject frequency;
    public GameObject rs;


    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject tuning_fork_anim;
    public GameObject sw;
    public GameObject clapping;
    public GameObject jet;
    public GameObject at;
    public GameObject at_anim;
    public GameObject bat;
    public GameObject yelling;
    public GameObject ball;

    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip sn_1;
    public AudioClip sn_2;
    public AudioClip sn_3;
    public AudioClip sn_4;
    public AudioClip sn_5;
    public AudioClip sn_6;
    public AudioClip sn_7;
    public AudioClip sn_8;
    public AudioClip sn_9;
    public AudioClip sn_10;
    public AudioClip sn_11;
    public AudioClip sn_12;
    public AudioClip sn_13;
    public AudioClip sn_14;
    public AudioClip sn_15;
    public AudioClip sn_16;
    public AudioClip sn_17;
    public AudioClip sn_18;
    public AudioClip sn_19;
    public AudioClip sn_20;
    public AudioClip sn_21;
    public AudioClip sn_22;
    public AudioClip sn_23;
    public AudioClip sn_24;
    public AudioClip sn_25;
    public AudioClip sn_26;
    public AudioClip sn_27;
    public AudioClip sn_28;
    public AudioClip sn_29;
    public AudioClip sn_30;
    public AudioClip sn_31;
    public AudioClip sn_32;
    public AudioClip sn_33;
    public AudioClip sn_34;
    public AudioClip sn_35;
    public AudioClip sn_36;
    public AudioClip sn_37;
    public AudioClip sn_38;
    public AudioClip sn_39;
    public AudioClip sn_40;
    public AudioClip sn_41;
    public AudioClip sn_42;
    public AudioClip sn_43;
    public AudioClip sn_44;
    public AudioClip sn_45;
    public AudioClip sn_46;
    public AudioClip sn_47;
    public AudioClip sn_48;
    public AudioClip sn_49;
    public AudioClip sn_50;
    public AudioClip sn_51;
    public AudioClip sn_52;










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





















    //
    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }
    //



    //
    void _title_1_MethodON()
    {
        title_1.SetActive(true);
    }
    //
    void title_2_MethodON()
    {
        title_2.SetActive(true);
    }
    //
    void title_3_MethodON()
    {
        title_3.SetActive(true);
    }
    //
    void title_4_MethodON()
    {
        title_4.SetActive(true);
    }
    //
    void title_5_MethodON()
    {
        title_5.SetActive(true);
    }
    //
    void title_6_MethodON()
    {
        title_6.SetActive(true);
    }
    //
    void title_7_MethodON()
    {
        title_7.SetActive(true);
    }
    //
    void title_8_MethodON()
    {
        title_8.SetActive(true);
    }
    //
    void title_9_MethodON()
    {
        title_9.SetActive(true);
    }
    //
    void title_10_MethodON()
    {
        title_10.SetActive(true);
    }
    //
    void title_11_MethodON()
    {
        title_11.SetActive(true);
    }
    //
    void title_12_MethodON()
    {
        title_12.SetActive(true);
    }
    //
    void title_13_MethodON()
    {
        title_13.SetActive(true);
    }
    //
    void title_14_MethodON()
    {
        title_14.SetActive(true);
    }
    //
    void title_15_MethodON()
    {
        title_15.SetActive(true);
    }
    //
    void title_16_MethodON()
    {
        title_16.SetActive(true);
    }
    //
    void title_17_MethodON()
    {
        title_17.SetActive(true);
    }
    //
    void title_18_MethodON()
    {
        title_18.SetActive(true);
    }
    //
    void _tuning_fork_anim_MethodON()
    {
        tuning_fork_anim.SetActive(true);
    }
    void _tuning_fork_anim_MethodoOFF()
    {
        tuning_fork_anim.SetActive(false);
    }
    //
    void _c1_MethodON()
    {
        c1.SetActive(true);
    }
    void _c1_MethodoOFF()
    {
        c1.SetActive(false);
    }
    //
    void _c2_MethodON()
    {
        c2.SetActive(true);
    }
    void _c2_MethodoOFF()
    {
        c2.SetActive(false);
    }
    //
    void _c3_MethodON()
    {
        c3.SetActive(true);
    }
    void _c3_MethodoOFF()
    {
        c3.SetActive(false);
    }
    //
    void _lw_MethodON()
    {
        lw.SetActive(true);
    }
    void _lw_MethodoOFF()
    {
        lw.SetActive(false);
    }
    //
    void _tw_MethodON()
    {
        tw.SetActive(true);
    }
    void _tw_MethodoOFF()
    {
        tw.SetActive(false);
    }
    //
    void _clapping_MethodON()
    {
        clapping.SetActive(true);
    }
    void _clapping_MethodoOFF()
    {
        clapping.SetActive(false);
    }
    //
    void _exp_MethodON()
    {
        exp.SetActive(true);
    }
    void _exp_MethodoOFF()
    {
        exp.SetActive(false);
    }
    //
    void _sw_MethodON()
    {
        sw.SetActive(true);
    }
    void _sw_MethodoOFF()
    {
        sw.SetActive(false);
    }
    //
    void _mc_MethodON()
    {
        mc.SetActive(true);
    }
    void _mc_MethodoOFF()
    {
        mc.SetActive(false);
    }
    //
    void _lines_MethodON()
    {
        line_1.SetActive(true);
        line_2.SetActive(true);
        line_3.SetActive(true);
        line_4.SetActive(true);

    }
    void _lines_MethodoOFF()
    {
        line_1.SetActive(false);
        line_2.SetActive(false);
        line_3.SetActive(false);
        line_4.SetActive(false);
    }
    //
    void _at_MethodON()
    {
        at.SetActive(true);
        at_anim.SetActive(true);
    }
    void _at_MethodoOFF()
    {
        at.SetActive(false);
        at_anim.SetActive(false);
    }
    //
    void _waves_MethodON()
    {
        waves.SetActive(true);
        
    }
    void _waves_MethodoOFF()
    {
        waves.SetActive(false);
        
    }
    //
    void _frequency_MethodON()
    {
        frequency.SetActive(true);

    }
    void _frequency_MethodoOFF()
    {
        frequency.SetActive(false);

    }
    //
    void _rs_MethodON()
    {
        rs.SetActive(true);
    }
    void _rs_MethodoOFF()
    {
        rs.SetActive(false);
    }
    //
    void _ball_MethodON()
    {
        ball.SetActive(true);
    }
    void _ball_MethodoOFF()
    {
        ball.SetActive(false);
    }
    //
    void _cloud_MethodON()
    {
        cloud.SetActive(true);
    }
    //







    //
    void _tuning_fork_anim_Animmethod()
    {

        anim = tuning_fork_anim.GetComponent<Animator>();
        anim.Play("anim_1");
    }
    //
    void _sw_anim_Animmethod()
    {

        anim = sw.GetComponent<Animator>();
        anim.Play("sw_anim");
    }
    //
    void _clapping_anim_Animmethod()
    {

        anim = clapping.GetComponent<Animator>();
        anim.Play("Clapping");
    }
    //
    void _jet_anim_Animmethod()
    {

        anim = jet.GetComponent<Animator>();
        anim.Play("jet");
    }
    //
    void _at_anim_Animmethod()
    {

        anim = at_anim.GetComponent<Animator>();
        anim.Play("at_anim");
    }
    //
    void _ball_anim_Animmethod()
    {

        anim = ball.GetComponent<Animator>();
        anim.Play("ball_anim");
    }
    //
    void _bat_Animmethod()
    {

        anim = bat.GetComponent<Animator>();
        anim.Play("flapping");
    }
    //
    void _yelling_Animmethod()
    {

        anim = yelling.GetComponent<Animator>();
        anim.Play("yelling");
    }
    //








    //
    void _sn_1_audioMethod()

    {
        myAudio.clip = sn_1;
        myAudio.Play();
    }
    //
    void _sn_2_audioMethod()

    {
        myAudio.clip = sn_2;
        myAudio.Play();
    }
    //
    void _sn_3_audioMethod()

    {
        myAudio.clip = sn_3;
        myAudio.Play();
    }
    //
    void _sn_4_audioMethod()

    {
        myAudio.clip = sn_4;
        myAudio.Play();
    }
    //
    void _sn_5_audioMethod()

    {
        myAudio.clip = sn_5;
        myAudio.Play();
    }
    //
    void _sn_6_audioMethod()

    {
        myAudio.clip = sn_6;
        myAudio.Play();
    }
    //
    void _sn_7_audioMethod()

    {
        myAudio.clip = sn_7;
        myAudio.Play();
    }
    //
    void _sn_8_audioMethod()

    {
        myAudio.clip = sn_8;
        myAudio.Play();
    }
    //
    void _sn_9_audioMethod()

    {
        myAudio.clip = sn_9;
        myAudio.Play();
    }
    //
    void _sn_10_audioMethod()

    {
        myAudio.clip = sn_10;
        myAudio.Play();
    }
    //
    void _sn_11_audioMethod()

    {
        myAudio.clip = sn_11;
        myAudio.Play();
    }
    //
    void _sn_12_audioMethod()

    {
        myAudio.clip = sn_12;
        myAudio.Play();
    }
    //
    void _sn_13_audioMethod()

    {
        myAudio.clip = sn_13;
        myAudio.Play();
    }
    //
    void _sn_14_audioMethod()

    {
        myAudio.clip = sn_14;
        myAudio.Play();
    }
    //
    void _sn_15_audioMethod()

    {
        myAudio.clip = sn_15;
        myAudio.Play();
    }
    //
    void _sn_16_audioMethod()

    {
        myAudio.clip = sn_16;
        myAudio.Play();
    }
    //
    void _sn_17_audioMethod()

    {
        myAudio.clip = sn_17;
        myAudio.Play();
    }
    //
    void _sn_18_audioMethod()

    {
        myAudio.clip = sn_18;
        myAudio.Play();
    }
    //
    void _sn_19_audioMethod()

    {
        myAudio.clip = sn_19;
        myAudio.Play();
    }
    //
    void _sn_20_audioMethod()

    {
        myAudio.clip = sn_20;
        myAudio.Play();
    }
    //
    void _sn_21_audioMethod()

    {
        myAudio.clip = sn_21;
        myAudio.Play();
    }
    //
    void _sn_22_audioMethod()

    {
        myAudio.clip = sn_22;
        myAudio.Play();
    }
    //
    void _sn_23_audioMethod()

    {
        myAudio.clip = sn_23;
        myAudio.Play();
    }
    //
    void _sn_24_audioMethod()

    {
        myAudio.clip = sn_24;
        myAudio.Play();
    }
    //
    void _sn_25_audioMethod()

    {
        myAudio.clip = sn_25;
        myAudio.Play();
    }
    //
    void _sn_26_audioMethod()

    {
        myAudio.clip = sn_26;
        myAudio.Play();
    }
    //
    void _sn_27_audioMethod()

    {
        myAudio.clip = sn_27;
        myAudio.Play();
    }
    //
    void _sn_28_audioMethod()

    {
        myAudio.clip = sn_28;
        myAudio.Play();
    }
    //
    void _sn_29_audioMethod()

    {
        myAudio.clip = sn_29;
        myAudio.Play();
    }
    //
    void _sn_30_audioMethod()

    {
        myAudio.clip = sn_30;
        myAudio.Play();
    }
    //
    void _sn_31_audioMethod()

    {
        myAudio.clip = sn_31;
        myAudio.Play();
    }
    //
    void _sn_32_audioMethod()

    {
        myAudio.clip = sn_32;
        myAudio.Play();
    }
    //
    void _sn_33_audioMethod()

    {
        myAudio.clip = sn_33;
        myAudio.Play();
    }
    //
    void _sn_34_audioMethod()

    {
        myAudio.clip = sn_34;
        myAudio.Play();
    }
    //
    void _sn_35_audioMethod()

    {
        myAudio.clip = sn_35;
        myAudio.Play();
    }
    //
    void _sn_36_audioMethod()

    {
        myAudio.clip = sn_36;
        myAudio.Play();
    }
    //
    void _sn_37_audioMethod()

    {
        myAudio.clip = sn_37;
        myAudio.Play();
    }
    //
    void _sn_38_audioMethod()

    {
        myAudio.clip = sn_38;
        myAudio.Play();
    }
    //
    void _sn_39_audioMethod()

    {
        myAudio.clip = sn_39;
        myAudio.Play();
    }
    //
    void _sn_40_audioMethod()

    {
        myAudio.clip = sn_40;
        myAudio.Play();
    }
    //
    void _sn_41_audioMethod()

    {
        myAudio.clip = sn_41;
        myAudio.Play();
    }
    //
    void _sn_42_audioMethod()

    {
        myAudio.clip = sn_42;
        myAudio.Play();
    }
    //
    void _sn_43_audioMethod()

    {
        myAudio.clip = sn_43;
        myAudio.Play();
    }
    //
    void _sn_44_audioMethod()

    {
        myAudio.clip = sn_44;
        myAudio.Play();
    }
    //
    void _sn_45_audioMethod()

    {
        myAudio.clip = sn_45;
        myAudio.Play();
    }
    //
    void _sn_46_audioMethod()

    {
        myAudio.clip = sn_46;
        myAudio.Play();
    }
    //
    void _sn_47_audioMethod()

    {
        myAudio.clip = sn_47;
        myAudio.Play();
    }
    //
    void _sn_48_audioMethod()

    {
        myAudio.clip = sn_48;
        myAudio.Play();
    }
    //
    void _sn_49_audioMethod()

    {
        myAudio.clip = sn_49;
        myAudio.Play();
    }
    //
    void _sn_50_audioMethod()

    {
        myAudio.clip = sn_50;
        myAudio.Play();
    }
    //
    void _sn_51_audioMethod()

    {
        myAudio.clip = sn_51;
        myAudio.Play();
    }
    //
    void _sn_52_audioMethod()

    {
        myAudio.clip = sn_52;
        myAudio.Play();
    }
    //















}
