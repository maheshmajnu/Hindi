using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_Waste_water_story_7class : MonoBehaviour
{
    public TargetController lv1MiniGame;
    public List<GameObject> questions;
    private TargetController previousMiniGame;

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

    public AudioSource gameSoundFxSource;
    public List<AudioClip> level1AudioClips;
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
            animator.Play("camera animation", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }
        animator = GetComponent<Animator>();
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

  
    

    public void level()
    {
        lv1MiniGame.Output();
        //Invoke("EndGameDelay", 1f);
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
        SaveProgress(1, 0, 4);
    }


    private int index;

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
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MiniGameEnd()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ChangeMiniGame(TargetController miniGame)
    {
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
        StartCoroutine(DelayChangePlayerPos(point));
        
    }

    IEnumerator DelayChangePlayerPos(Transform point)
    {
        yield return new WaitForSeconds(3f);
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(point);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void PlayAnim2(Animator anim)
    {
        anim.SetTrigger("Trigger2");
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

    public Camera cam;
    public LayerMask layerMask;
    //public GameObject level5Q;
    public Animator lvl5Anim;

    private void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
            {
                if (raycastHit.collider != null)
                {
                    //TurnOnGODelay(level5Q);
                    lvl5Anim.SetTrigger("Trigger2");
                }
            }
        }
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







    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject arow_anime;
    public GameObject chlorine_anim;







    // ON - OFF gameobjects
    [Header("Explanation Assets")]




    public GameObject dust;
    public GameObject into;
    public GameObject arows;
    public GameObject sitting;
    public GameObject Cube;
    public GameObject Water_Our_T;
    public GameObject What_is_Sewage_T;
    public GameObject Water_Fre_T;
    public GameObject Treatment_T;
    public GameObject WasteWater_T;
    public GameObject Bar_Screeni_T;
    public GameObject Sedimentation_T;
    public GameObject Biological_T;
    public GameObject chlro_uv;
    public GameObject Chlorination_T;
    public GameObject UV_Treatment_T;
    public GameObject Better_T;
    public GameObject S_and_D_T;
    public GameObject Alternative_T;
    public GameObject Sanitation_T;
    public GameObject Water_is_D;
    public GameObject aerators_D;
    public GameObject Chlorine;
    public GameObject UV_light_D;
    public GameObject diarrhea_D;

    //





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













    void audio1_method()
    {
        myAudio.clip = audio1;
        myAudio.Play();
    }

    //

    void audio2_method()
    {
        myAudio.clip = audio2;
        myAudio.Play();
    }

    //

    void audio3_method()
    {
        myAudio.clip = audio3;
        myAudio.Play();
    }

    //

    void audio4_method()
    {
        myAudio.clip = audio4;
        myAudio.Play();
    }

    //

    void audio5_method()
    {
        myAudio.clip = audio5;
        myAudio.Play();
    }

    //

    void audio6_method()
    {
        myAudio.clip = audio6;
        myAudio.Play();
    }

    //

    void audio7_method()
    {
        myAudio.clip = audio7;
        myAudio.Play();
    }

    //

    void audio8_method()
    {
        myAudio.clip = audio8;
        myAudio.Play();
    }

    //

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












    //Animation Play

    void arow_anime_Method()
    {
        anim = arow_anime.GetComponent<Animator>();
        anim.Play("arow anime");
    }

    void chlorine_anim_Method()
    {
        anim = chlorine_anim.GetComponent<Animator>();
        anim.Play("chlorine anim");
    }










    // ON/OFF object



    void _dust_MethodON()
    {
        dust.SetActive(true);
    }

    void _dust_MethodOFF()
    {
        dust.SetActive(false);
    }


    void _into_MethodON()
    {
        into.SetActive(true);
    }

    void _into_MethodOFF()
    {
        into.SetActive(false);
    }


    void _arows_MethodON()
    {
        arows.SetActive(true);
    }

    void _arows_MethodOFF()
    {
        arows.SetActive(false);
    }

    void _sitting_MethodON()
    {
        sitting.SetActive(true);
    }

    void _sitting_MethodOFF()
    {
        sitting.SetActive(false);
    }

    void _Cube_MethodON()
    {
        Cube.SetActive(true);
    }

    void _Cube_MethodOFF()
    {
        Cube.SetActive(false);
    }

    void _Water_Our_T_MethodON()
    {
        Water_Our_T.SetActive(true);
    }

    void _Water_Our_T_MethodOFF()
    {
        Water_Our_T.SetActive(false);
    }

    void _What_is_Sewage_T_MethodON()
    {
        What_is_Sewage_T.SetActive(true);
    }

    void _What_is_Sewage_T_MethodOFF()
    {
        What_is_Sewage_T.SetActive(false);
    }

    void _Water_Fre_T_MethodON()
    {
        Water_Fre_T.SetActive(true);
    }

    void _Water_Fre_T_MethodOFF()
    {
        Water_Fre_T.SetActive(false);
    }

    void _Treatment_T_MethodON()
    {
        Treatment_T.SetActive(true);
    }

    void _Treatment_T_MethodOFF()
    {
        Treatment_T.SetActive(false);
    }

    void _WasteWater_T_MethodON()
    {
        WasteWater_T.SetActive(true);
    }

    void _WasteWater_T_MethodOFF()
    {
        WasteWater_T.SetActive(false);
    }

    void _Bar_Screeni_T_MethodON()
    {
        Bar_Screeni_T.SetActive(true);
    }

    void _Bar_Screeni_T_MethodOFF()
    {
        Bar_Screeni_T.SetActive(false);
    }

    void _Sedimentation_T_MethodON()
    {
        Sedimentation_T.SetActive(true);
    }

    void _Sedimentation_T_MethodOFF()
    {
        Sedimentation_T.SetActive(false);
    }

    void _Biological_T_MethodON()
    {
        Biological_T.SetActive(true);
    }

    void _Biological_T_MethodOFF()
    {
        Biological_T.SetActive(false);
    }

    void _chlro_uv_MethodON()
    {
        chlro_uv.SetActive(true);
    }

    void _chlro_uv_MethodOFF()
    {
        chlro_uv.SetActive(false);
    }

    void _Chlorination_T_MethodON()
    {
        Chlorination_T.SetActive(true);
    }

    void _Chlorination_T_MethodOFF()
    {
        Chlorination_T.SetActive(false);
    }

    void _UV_Treatment_T_MethodON()
    {
        UV_Treatment_T.SetActive(true);
    }

    void _UV_Treatment_T_MethodOFF()
    {
        UV_Treatment_T.SetActive(false);
    }

    void _Better_T_MethodON()
    {
        Better_T.SetActive(true);
    }

    void _Better_T_MethodOFF()
    {
        Better_T.SetActive(false);
    }

    void _S_and_D_T_MethodON()
    {
        S_and_D_T.SetActive(true);
    }

    void _S_and_D_T_MethodOFF()
    {
        S_and_D_T.SetActive(false);
    }

    void _Alternative_T_MethodON()
    {
        Alternative_T.SetActive(true);
    }

    void _Alternative_T_MethodOFF()
    {
        Alternative_T.SetActive(false);
    }

    void _Sanitation_T_MethodON()
    {
        Sanitation_T.SetActive(true);
    }

    void _Sanitation_T_MethodOFF()
    {
        Sanitation_T.SetActive(false);
    }

    void _Water_is_D_MethodON()
    {
        Water_is_D.SetActive(true);
    }

    void _Water_is_D_MethodOFF()
    {
        Water_is_D.SetActive(false);
    }

    void _aerators_D_MethodON()
    {
        aerators_D.SetActive(true);
    }

    void _aerators_D_MethodOFF()
    {
        aerators_D.SetActive(false);
    }

    void _Chlorine_MethodON()
    {
        Chlorine.SetActive(true);
    }

    void _Chlorine_MethodOFF()
    {
        Chlorine.SetActive(false);
    }

    void _UV_light_D_MethodON()
    {
        UV_light_D.SetActive(true);
    }

    void _UV_light_D_MethodOFF()
    {
        UV_light_D.SetActive(false);
    }

    void _diarrhea_D_MethodON()
    {
        diarrhea_D.SetActive(true);
    }

    void _diarrhea_D_MethodOFF()
    {
        diarrhea_D.SetActive(false);
    }

































}
