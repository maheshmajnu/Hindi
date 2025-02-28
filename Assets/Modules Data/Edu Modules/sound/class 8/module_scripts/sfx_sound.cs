using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_sound : MonoBehaviour
{
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

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

    public void SetWayPoint(Transform target)
    {
        waypoint.player = InventoryManager.Instance.player.transform;
        waypointCanvas.SetActive(true);
        waypoint.target = target;
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
            case 1: Level3(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
    }

   

    private void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }

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
    


    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

    public AudioSource gameSoundFxSource;
    public List<AudioClip> level1AudioClips;
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

        StartCoroutine(PlayAudioLv1());
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


    IEnumerator PlayAudioLv1()
    {
        gameSoundFxSource.Stop();
        yield return new WaitForSeconds(2);
        gameSoundFxSource.clip = level1AudioClips[index];
        gameSoundFxSource.Play();
    }

    public void CorrectAnswerLv1(int ind)
    {
        if (ind == index)
        {
            index++;

            if (index == 3)
            {
                index = 0;
                gameSoundFxSource.Stop();
                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                StartCoroutine(DelayLv2MiniGameStart());
                InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
                return;
            }

            StartCoroutine(PlayAudioLv1());
        }
        else
        {
            MissionFailed();
        }
    }

    IEnumerator DelayLv2MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv2MiniGame.Output();
    }

    public TargetController lv2MiniGame;
    public void Lv2TurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 5)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Invoke("Level3", 1);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public Transform lv3SpawnPoint;
    public GameObject lv3;
    public void Level3()
    {
        SaveProgress(1, 0, 2);
        lv3.SetActive(true);
        InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(lv3SpawnPoint);
        lv2MiniGame.EndMiniGame();
        Lv3Ques();
    }

    void Lv3Ques()
    {
        StartCoroutine(popUpQuesLv3());
    }

    public GameObject lv3Ques;
    IEnumerator popUpQuesLv3()
    {
        yield return new WaitForSeconds(1);
        lv3Ques.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public GameObject treesLv3;
    public void CorrectAnswerLv3()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        InventoryManager.Instance.inventryStatic.SetActive(true);
        InventoryManager.Instance.player.ThirdPersonController.canMove = true;
        treesLv3.SetActive(true);
        lv3Ques.SetActive(false);
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

    public List<GameObject> wrongSigns = new List<GameObject>();
    public void CorrectSignBoardLv3()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void PlaceObjects(GameObject obj)
    {
        obj.SetActive(true);
    }

    public Transform cameraTransform;
    public List<GameObject> level4Questions;
    public List<Transform> level4CamHolders;

    public void Level4()
    {
        Invoke("DelayLevel4", 2);
        Invoke("Fade", 1);
    }

    public void Fade()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    public void DelayLevel4()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(DelayQuestions());
    }

    IEnumerator DelayQuestions()
    {
        cameraTransform.position = level4CamHolders[index].position;
        cameraTransform.rotation = level4CamHolders[index].rotation;
        yield return new WaitForSeconds(2);
        level4Questions[index].SetActive(true);
    }

    public void CorrectAnswerL4()
    {
        index++;

        foreach (GameObject obj in level4Questions)
        {
            obj.SetActive(false);
        }


        if (index == 5)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
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
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
            return;
        }

        StartCoroutine(DelayQuestions());
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


    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject harmonium;
    public GameObject phone;
    public GameObject flute;
    public GameObject guitar;
    public GameObject lungs;
    public GameObject laryux;
    public GameObject ear;
    public GameObject L1;
    public GameObject L2;
    public GameObject L3;
    public GameObject L4;
    public GameObject L5;
    public GameObject L6;
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
    public GameObject title_19;
    public GameObject title_20;
    public GameObject title_21;
    public GameObject title_22;
    public GameObject title_23;
    public GameObject title_24;
    public GameObject title_25;
    public GameObject des_1;
    public GameObject des_2;
    public GameObject des_3;
    public GameObject des_4;
    public GameObject clouds;
    public GameObject Articulator;
    public GameObject heli;


    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject drums_anim;
    public GameObject guitar_anim;
    public GameObject flute_anim;
    public GameObject helicopter;



    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip in_1;
    public AudioClip sp_vb_1;
    public AudioClip sp_vb_2;
    public AudioClip sp_vb_3;
    public AudioClip sp_vb_4;
    public AudioClip sp_vb_5;
    public AudioClip sp_vb_6;
    public AudioClip sp_vb_7;
    public AudioClip sp_vb_8;
    public AudioClip sp_vb_9;
    public AudioClip sp_vb_10;
    public AudioClip sp_vb_11;
    public AudioClip sp_vb_12;
    public AudioClip sp_vb_13;
    public AudioClip sp_vb_14;
    public AudioClip sp_vb_15;
    public AudioClip sp_h_1;
    public AudioClip sp_h_2;
    public AudioClip sp_h_3;
    public AudioClip sp_h_4;
    public AudioClip sp_h_5;
    public AudioClip sp_h_6;
    public AudioClip sp_h_7;
    public AudioClip sp_h_8;
    public AudioClip sp_h_9;
    public AudioClip sp_h_10;
    public AudioClip ear_1;
    public AudioClip ear_2;
    public AudioClip ear_3;
    public AudioClip ear_4;
    public AudioClip ear_5;
    public AudioClip ear_6;
    public AudioClip ear_7;
    public AudioClip ear_8;
    public AudioClip np_1;
    public AudioClip np_2;
    public AudioClip np_3;
    public AudioClip np_4;
    public AudioClip np_5;
    public AudioClip np_6;
    public AudioClip np_7;
    public AudioClip np_8;
    public AudioClip np_9;
    public AudioClip np_10;
    public AudioClip np_11;
    public AudioClip np_12;
    public AudioClip np_13;
    public AudioClip np_14;
    public AudioClip np_15;
    public AudioClip np_16;
    public AudioClip song;
    public AudioClip guitar_music;
    public AudioClip flute_music;
    public AudioClip drums_music;
    public AudioClip Au17;
    public AudioClip AU18;
    public AudioClip AU19;
    public AudioClip AU20;
    public AudioClip AU21;
    public AudioClip AU22;
    public AudioClip AU23;
    public AudioClip AU24;
    public AudioClip AU25;
    public AudioClip AU26;
    public AudioClip AU27;
    public AudioClip AU28;
    public AudioClip AU29;




    // Jump to point buttons


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


















    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }
    //

    void _Heli_MethodON()
    {
        heli.SetActive(true);
    }

    void _Heli_MethodOFF()
    {
        heli.SetActive(true);
    }

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
    void title_19_MethodON()
    {
        title_19.SetActive(true);
    }
    //
    void title_20_MethodON()
    {
        title_20.SetActive(true);
    }
    //
    void title_21_MethodON()
    {
        title_21.SetActive(true);
    }
    //
    void title_22_MethodON()
    {
        title_22.SetActive(true);
    }
    //
    void title_23_MethodON()
    {
        title_23.SetActive(true);
    }
    //
    void title_24_MethodON()
    {
        title_24.SetActive(true);
    }
    //
    void title_25_MethodON()
    {
        title_25.SetActive(true);
    }
    //
    void des_1_MethodON()
    {
        des_1.SetActive(true);
    }
    //
    void des_2_MethodON()
    {
        des_2.SetActive(true);
    }
    //
    void des_3_MethodON()
    {
        des_3.SetActive(true);
    }
    //
    void des_4_MethodON()
    {
        des_4.SetActive(true);
    }


    //
    void _harmonium_MethodON()
    {
        harmonium.SetActive(true);
    }
    void _harmonium_MethodoOFF()
    {
        harmonium.SetActive(false);
    }
    //
    void _guitar_MethodON()
    {
        guitar.SetActive(true);
    }
    void _guitar_MethodoOFF()
    {
        guitar.SetActive(false);
    }
    //
    void _phone_MethodON()
    {
        phone.SetActive(true);
    }
    void _phone_MethodoOFF()
    {
        phone.SetActive(false);
    }
    //
    void _flute_MethodON()
    {
        flute.SetActive(true);
    }
    void _flute_MethodoOFF()
    {
        flute.SetActive(false);
    }
    //
    void _lungs_MethodON()
    {
        lungs.SetActive(true);
    }
    void _lungs_MethodoOFF()
    {
        lungs.SetActive(false);
    }
    //
    void _laryux_MethodON()
    {
        laryux.SetActive(true);
    }
    void _laryux_MethodoOFF()
    {
        laryux.SetActive(false);
    }
    //
    void _laryux_1_MethodON()
    {
        laryux.SetActive(true);
    }
    void _laryux_1_MethodoOFF()
    {
        laryux.SetActive(false);
    }
    //
    void _ear_MethodON()
    {
        ear.SetActive(true);
    }
    void _ear_MethodoOFF()
    {
        ear.SetActive(false);
    }
    //
    void _L1_MethodON()
    {
        L1.SetActive(true);
    }
    void _L1_MethodoOFF()
    {
        L1.SetActive(false);
    }
    //
    void _L2_MethodON()
    {
        L2.SetActive(true);
    }
    void _L2_MethodoOFF()
    {
        L2.SetActive(false);
    }
    //
    void _L3_MethodON()
    {
        L3.SetActive(true);
    }
    void _L3_MethodoOFF()
    {
        L3.SetActive(false);
    }
    //
    void _L4_MethodON()
    {
        L4.SetActive(true);
    }
    void _L4_MethodoOFF()
    {
        L4.SetActive(false);
    }
    //
    void _L5_MethodON()
    {
        L5.SetActive(true);
    }
    void _L5_MethodoOFF()
    {
        L5.SetActive(false);
    }
    //
    void _L6_MethodON()
    {
        L6.SetActive(true);
    }
    void _L6_MethodoOFF()
    {
        L6.SetActive(false);
    }
    //
    //
    void _clouds_MethodON()
    {
        clouds.SetActive(true);
    }
    void _clouds_MethodoOFF()
    {
        clouds.SetActive(false);
    }
    //







    //
    void _flute_anim_MethodON()
    {
        flute_anim.SetActive(true);
    }
    void _flute_anim_MethodoOFF()
    {
        flute_anim.SetActive(false);
    }
    //
    //
    void _guitar_anim_MethodON()
    {
        guitar_anim.SetActive(true);
    }
    void _guitar_anim_MethodoOFF()
    {
        guitar_anim.SetActive(false);
    }
    //
    //
    void _drums_anim_MethodON()
    {
        drums_anim.SetActive(true);
    }
    void _drums_anim_MethodoOFF()
    {
        drums_anim.SetActive(false);
    }
    //
    void _fgd_anim_MethodON()
    {
        flute_anim.SetActive(true);
        drums_anim.SetActive(true);
        guitar_anim.SetActive(true);
    }
    void _fgd_anim_MethodoOFF()
    {

        flute_anim.SetActive(false);
        drums_anim.SetActive(false);
        guitar_anim.SetActive(false);
    }
    //
    void _Articulator_MethodON()
    {
        Articulator.SetActive(true);
    }
    void _Articulator_MethodoOFF()
    {
        Articulator.SetActive(false);
    }
    //


    //
    void _guitar_anim_Animmethod()
    {

        anim = guitar_anim.GetComponent<Animator>();
        anim.Play("guitar_anim");
    }
    //
    void _drums_anim_Animmethod()
    {

        anim = drums_anim.GetComponent<Animator>();
        anim.Play("Playing Drums");
    }
    //
    void _helicopter_Animmethod()
    {
        Debug.Log("helicopter: ");
        anim = helicopter.GetComponent<Animator>();
        anim.Play("helicopter");
    }
    //





    //
    void _in_1_audioMethod()

    {
        myAudio.clip = in_1;
        myAudio.Play();
    }
    //
    void _sp_vb_1_audioMethod()

    {
        myAudio.clip = sp_vb_1;
        myAudio.Play();
    }
    //
    void _sp_vb_2_audioMethod()

    {
        myAudio.clip = sp_vb_2;
        myAudio.Play();
    }
    //
    void _sp_vb_3_audioMethod()

    {
        myAudio.clip = sp_vb_3;
        myAudio.Play();
    }
    //
    void _sp_vb_4_audioMethod()

    {
        myAudio.clip = sp_vb_4;
        myAudio.Play();
    }
    //
    void _sp_vb_5_audioMethod()

    {
        myAudio.clip = sp_vb_5;
        myAudio.Play();
    }
    //
    void _sp_vb_6_audioMethod()

    {
        myAudio.clip = sp_vb_6;
        myAudio.Play();
    }
    //
    void _sp_vb_7_audioMethod()

    {
        myAudio.clip = sp_vb_7;
        myAudio.Play();
    }
    //
    void _sp_vb_8_audioMethod()

    {
        myAudio.clip = sp_vb_8;
        myAudio.Play();
    }
    //
    void _sp_vb_9_audioMethod()

    {
        myAudio.clip = sp_vb_9;
        myAudio.Play();
    }
    //
    void _sp_vb_10_audioMethod()

    {
        myAudio.clip = sp_vb_10;
        myAudio.Play();
    }
    //
    void _sp_vb_11_audioMethod()

    {
        myAudio.clip = sp_vb_11;
        myAudio.Play();
    }
    //
    void _sp_vb_12_audioMethod()

    {
        myAudio.clip = sp_vb_12;
        myAudio.Play();
    }
    //
    void _sp_vb_13_audioMethod()

    {
        myAudio.clip = sp_vb_13;
        myAudio.Play();
    }
    //
    void _sp_vb_14_audioMethod()

    {
        myAudio.clip = sp_vb_14;
        myAudio.Play();
    }
    //
    void _sp_vb_15_audioMethod()

    {
        myAudio.clip = sp_vb_15;
        myAudio.Play();
    }

 
    //
    void _sp_h_1_audioMethod()

    {
        myAudio.clip = sp_h_1;
        myAudio.Play();
    }
    //
    void _sp_h_2_audioMethod()

    {
        myAudio.clip = sp_h_2;
        myAudio.Play();
    }
    //
    void _sp_h_3_audioMethod()

    {
        myAudio.clip = sp_h_3;
        myAudio.Play();
    }
    //
    void _sp_h_4_audioMethod()

    {
        myAudio.clip = sp_h_4;
        myAudio.Play();
    }
    //
    void _sp_h_5_audioMethod()

    {
        myAudio.clip = sp_h_5;
        myAudio.Play();
    }
    //
    void _sp_h_6_audioMethod()

    {
        myAudio.clip = sp_h_6;
        myAudio.Play();
    }
    //
    void _sp_h_7_audioMethod()

    {
        myAudio.clip = sp_h_7;
        myAudio.Play();
    }
    //
    void _sp_h_8_audioMethod()

    {
        myAudio.clip = sp_h_8;
        myAudio.Play();
    }
    //
    void _sp_h_9_audioMethod()

    {
        myAudio.clip = sp_h_9;
        myAudio.Play();
    }
    //
    void _sp_h_10_audioMethod()

    {
        myAudio.clip = sp_h_10;
        myAudio.Play();
    }
    //
    void _ear_1_audioMethod()

    {
        myAudio.clip = ear_1;
        myAudio.Play();
    }
    //
    void _ear_2_audioMethod()

    {
        myAudio.clip = ear_2;
        myAudio.Play();
    }
    //

    void _ear_3_audioMethod()

    {
        myAudio.clip = ear_3;
        myAudio.Play();
    }
    //

    void _ear_4_audioMethod()

    {
        myAudio.clip = ear_4;
        myAudio.Play();
    }
    //

    void _ear_5_audioMethod()

    {
        myAudio.clip = ear_5;
        myAudio.Play();
    }
    //

    void _ear_6_audioMethod()

    {
        myAudio.clip = ear_6;
        myAudio.Play();
    }
    //

    void _ear_7_audioMethod()

    {
        myAudio.clip = ear_7;
        myAudio.Play();
    }
    //
    void _ear_8_audioMethod()

    {
        myAudio.clip = ear_8;
        myAudio.Play();
    }
    //
    void _np_1_audioMethod()

    {
        myAudio.clip = np_1;
        myAudio.Play();
    }
    //
    void _np_2_audioMethod()

    {
        myAudio.clip = np_2;
        myAudio.Play();
    }
    //
    void _np_3_audioMethod()

    {
        myAudio.clip = np_3;
        myAudio.Play();
    }
    //
    void _np_4_audioMethod()

    {
        myAudio.clip = np_4;
        myAudio.Play();
    }
    //
    void _np_5_audioMethod()

    {
        myAudio.clip = np_5;
        myAudio.Play();
    }
    //
    void _np_6_audioMethod()

    {
        myAudio.clip = np_6;
        myAudio.Play();
    }
    //
    void _np_7_audioMethod()

    {
        myAudio.clip = np_7;
        myAudio.Play();
    }
    //
    void _np_8_audioMethod()

    {
        myAudio.clip = np_8;
        myAudio.Play();
    }
    //
    void _np_9_audioMethod()

    {
        myAudio.clip = np_9;
        myAudio.Play();
    }
    //
    void _np_10_audioMethod()

    {
        myAudio.clip = np_10;
        myAudio.Play();
    }
    //
    void _np_11_audioMethod()

    {
        myAudio.clip = np_11;
        myAudio.Play();
    }
    //
    void _np_12_audioMethod()

    {
        myAudio.clip = np_12;
        myAudio.Play();
    }
    //
    void _np_13_audioMethod()

    {
        myAudio.clip = np_13;
        myAudio.Play();
    }
    //
    void _np_14_audioMethod()

    {
        myAudio.clip = np_14;
        myAudio.Play();
    }
    //
    void _np_15_audioMethod()

    {
        myAudio.clip = np_15;
        myAudio.Play();
    }
    //
    void _np_16_audioMethod()

    {
        myAudio.clip = np_16;
        myAudio.Play();
    }
    void AU17_audioMethod()

    {
        myAudio.clip = Au17;
        myAudio.Play();
    }
    void AU18_audioMethod()

    {
        myAudio.clip = AU18;
        myAudio.Play();
    }
    void AU19_audioMethod()

    {
        myAudio.clip = AU19;
        myAudio.Play();
    }
    void AU20_audioMethod()

    {
        myAudio.clip = AU20;
        myAudio.Play();
    }
    void AU21_audioMethod()

    {
        myAudio.clip = AU21;
        myAudio.Play();
    }
    void AU22_audioMethod()

    {
        myAudio.clip = AU22;
        myAudio.Play();
    }
    void AU23_audioMethod()

    {
        myAudio.clip = AU23;
        myAudio.Play();
    }
    void AU24_audioMethod()

    {
        myAudio.clip = AU24;
        myAudio.Play();
    }
    void AU25_audioMethod()

    {
        myAudio.clip = AU25;
        myAudio.Play();
    }
    void AU26_audioMethod()

    {
        myAudio.clip = AU26;
        myAudio.Play();
    }
    void AU27_audioMethod()

    {
        myAudio.clip = AU27;
        myAudio.Play();
    }
    void AU28_audioMethod()

    {
        myAudio.clip = AU28;
        myAudio.Play();
    }
    void AU29_audioMethod()

    {
        myAudio.clip = AU29;
        myAudio.Play();
    }
    //
    void _song_audioMethod()

    {
        myAudio.clip = song;
        myAudio.Play();
    }
    //
    void _guitar_music_audioMethod()

    {
        myAudio.clip = guitar_music;
        myAudio.Play();
    }
    //
    void _flute_music_audioMethod()

    {
        myAudio.clip = flute_music;
        myAudio.Play();
    }
    //
    void _drums_music_audioMethod()

    {
        myAudio.clip = drums_music;
        myAudio.Play();
    }
    //




}
