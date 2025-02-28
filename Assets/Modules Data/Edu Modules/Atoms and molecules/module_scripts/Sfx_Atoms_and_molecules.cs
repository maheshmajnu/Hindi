using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx_Atoms_and_molecules : MonoBehaviour
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
            case 1: Level5Spawnpoint(); break;
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


    public Transform SpawnPoint5;
    
    public void Level5Spawnpoint()
    {
        InventoryManager.Instance.inventryStatic.SetActive(false);
        StartCoroutine(DelaySpawnPoint());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        
    }

    IEnumerator DelaySpawnPoint()
    {
        yield return new WaitForSeconds(0.5f);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint5);
    }
    public void Savepoint1()
    {
        SaveProgress(1, 0, 3);
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

        //SetWayPoint(wayPoint1); 

        InitializeFromCheckpoint();
        level();

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

    

    public GameObject lv5a;
    public GameObject lv5b;

    public void Level5()
    {
        index++;


        if (index == 6)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            lv5a.SetActive(false);
            lv5b.SetActive(true);
            //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public GameObject lv6;

    public void Level5B()
    {
        index++;


        if (index == 7)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            lv5b.SetActive(false);
            lv6.SetActive(true);
            //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    
    public GameObject C_Anions;
    public GameObject C_Poly;
    public GameObject Cations;
    public GameObject Anions;
    public GameObject Poly;
    public GameObject lv7door;


    public void Level6()
    {
        index++;


        if (index == 4)
        {
            
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            
            Cations.SetActive(true);
            C_Anions.SetActive(true);
            Anions.SetActive(false);
            //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
        if (index == 8)
        {
            
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            
            Anions.SetActive(true);
            C_Anions.SetActive(false);
            Poly.SetActive(false);
            C_Poly.SetActive(true);
            //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
        if (index == 12)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Poly.SetActive(true);
            lv6.SetActive(false);
            lv7door.SetActive(false);
            //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public GameObject lv8door;

    public void Level7()
    {
        index++;


        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            lv8door.SetActive(false);
            
            //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }



    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject h20_text;
    public GameObject building;
    public GameObject scifiRatio;
    public GameObject scifiSymbol;
    public GameObject Fe;
    public GameObject Na;
    public GameObject K;
    public GameObject coText;
    public GameObject table3;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject utext;
    public GameObject wateru;
    public GameObject wateraddtext;
    public GameObject relativemasstext;
    public GameObject octo;
    public GameObject oh4;
    public GameObject bonds;






    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject exp_anim;
    public GameObject wm_anim;








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
    public GameObject D13;
    public GameObject D14;
    public GameObject D15;
    public GameObject D16;
    public GameObject D17;
    public GameObject D18;
    public GameObject D19;
    public GameObject D20;
    public GameObject D21;








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
    public AudioClip audio_109b;
    public AudioClip audio_110b;
    public AudioClip audio_111b;
    public AudioClip audio_112b;
    public AudioClip audio_113b;
    public AudioClip audio_114b;
    public AudioClip audio_115b;
    public AudioClip audio_116b;
    public AudioClip audio_117b;
    public AudioClip audio_118b;
    public AudioClip audio_119b;
    public AudioClip audio_120b;
    public AudioClip audio_121b;
    public AudioClip audio_122b;
    public AudioClip audio_123b;
    public AudioClip audio_124b;
    public AudioClip audio_125b;
    public AudioClip audio_126b;
    public AudioClip audio_127b;
    public AudioClip audio_128b;
    public AudioClip audio_129b;
    public AudioClip audio_130b;
    public AudioClip audio_131b;
    public AudioClip audio_132b;
    public AudioClip audio_133b;
    public AudioClip audio_134b;
    public AudioClip audio_135b;
    public AudioClip audio_136b;
    public AudioClip audio_137b;
    public AudioClip audio_138b;
    public AudioClip audio_139b;
    public AudioClip audio_140b;
    public AudioClip audio_141b;
    public AudioClip audio_142b;
    public AudioClip audio_143b;

    public AudioClip audio_s1;
    public AudioClip audio_s2;
    public AudioClip audio_s3;
    public AudioClip audio_s4;
    public AudioClip audio_s5;
    public AudioClip audio_s6;
    public AudioClip audio_s7;
    public AudioClip audio_s8;
    public AudioClip audio_s9;
    public AudioClip audio_s10;
    public AudioClip audio_s11;
    public AudioClip audio_s12;
    public AudioClip audio_s13;
    public AudioClip audio_s14;
    public AudioClip audio_s15;
    public AudioClip audio_s16;







    // jump to point buttons

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
























    // Audio


    void _audio_S1_audioMethod()
    {
        myAudio.clip = audio_s1;
        myAudio.Play();
    }
    void _audio_S2_audioMethod()
    {
        myAudio.clip = audio_s2;
        myAudio.Play();
    }
    void _audio_S3_audioMethod()
    {
        myAudio.clip = audio_s3;
        myAudio.Play();
    }
    void _audio_S4_audioMethod()
    {
        myAudio.clip = audio_s4;
        myAudio.Play();
    }
    void _audio_S5_audioMethod()
    {
        myAudio.clip = audio_s5;
        myAudio.Play();
    }
    void _audio_S6_audioMethod()
    {
        myAudio.clip = audio_s6;
        myAudio.Play();
    }
    void _audio_S7_audioMethod()
    {
        myAudio.clip = audio_s7;
        myAudio.Play();
    }
    void _audio_S8_audioMethod()
    {
        myAudio.clip = audio_s8;
        myAudio.Play();
    }
    void _audio_S9_audioMethod()
    {
        myAudio.clip = audio_s9;
        myAudio.Play();
    }
    void _audio_S10_audioMethod()
    {
        myAudio.clip = audio_s10;
        myAudio.Play();
    }
    void _audio_S11_audioMethod()
    {
        myAudio.clip = audio_s11;
        myAudio.Play();
    }
    void _audio_S12_audioMethod()
    {
        myAudio.clip = audio_s12;
        myAudio.Play();
    }
    void _audio_S13_audioMethod()
    {
        myAudio.clip = audio_s13;
        myAudio.Play();
    }
    void _audio_S14_audioMethod()
    {
        myAudio.clip = audio_s14;
        myAudio.Play();
    }
    void _audio_S15_audioMethod()
    {
        myAudio.clip = audio_s15;
        myAudio.Play();
    }













    // Animations


    void _wm_animAnimmethod()
    {

        anim = wm_anim.GetComponent<Animator>();
        anim.Play("Weight machine anim");
    }

    void _Exp_animAnimmethod()
    {

        anim = exp_anim.GetComponent<Animator>();
        anim.Play("Exp 1 anim");
    }



















    // Method ON/OFF










    void _bonds_MethodON()
    {
        bonds.SetActive(true);
    }
    void _bonds_MethodoOFF()
    {
        bonds.SetActive(false);
    }

    void _oh4_MethodON()
    {
        oh4.SetActive(true);
    }
    void _oh4_MethodoOFF()
    {
        oh4.SetActive(false);
    }

    void _octo_MethodON()
    {
        octo.SetActive(true);
    }
    void _octo_MethodoOFF()
    {
        octo.SetActive(false);
    }

    void _relativemasstext_MethodON()
    {
        relativemasstext.SetActive(true);
    }
    void _relativemasstext_MethodoOFF()
    {
        relativemasstext.SetActive(false);
    }


    void _wateraddtext_MethodON()
    {
        wateraddtext.SetActive(true);
    }
    void _wateraddtext_MethodoOFF()
    {
        wateraddtext.SetActive(false);
    }

    void _wateru_MethodON()
    {
        wateru.SetActive(true);
    }
    void _wateru_MethodoOFF()
    {
        wateru.SetActive(false);
    }

    void _utext_MethodON()
    {
        utext.SetActive(true);
    }
    void _utext_MethodoOFF()
    {
        utext.SetActive(false);
    }

    void _one_MethodON()
    {
        one.SetActive(true);
    }
    void _one_MethodoOFF()
    {
        one.SetActive(false);
    }



    void _two_MethodON()
    {
        two.SetActive(true);
    }
    void _two_MethodoOFF()
    {
        two.SetActive(false);
    }



    void _three_MethodON()
    {
        three.SetActive(true);
    }
    void _three_MethodoOFF()
    {
        three.SetActive(false);
    }

    void _four_MethodON()
    {
        four.SetActive(true);
    }
    void _four_MethodoOFF()
    {
        four.SetActive(false);
    }












    void _table3_MethodON()
    {
        table3.SetActive(true);
    }
    void _table3_MethodoOFF()
    {
        table3.SetActive(false);
    }


    void _coText_MethodON()
    {
        coText.SetActive(true);
    }
    void _coText_MethodoOFF()
    {
        coText.SetActive(false);
    }

    void _Fe_MethodON()
    {
        Fe.SetActive(true);
    }
    void _Fe_MethodoOFF()
    {
        Fe.SetActive(false);
    }

    void _K_MethodON()
    {
        K.SetActive(true);
    }
    void _K_MethodoOFF()
    {
        K.SetActive(false);
    }

    void _Na_MethodON()
    {
        Na.SetActive(true);
    }
    void _Na_MethodoOFF()
    {
        Na.SetActive(false);
    }








    void _scifiSymbol_MethodON()
    {
        scifiSymbol.SetActive(true);
    }
    void _scifiSymbol_MethodoOFF()
    {
        scifiSymbol.SetActive(false);
    }


    void _scifiRatio_MethodON()
    {
        scifiRatio.SetActive(true);
    }
    void _scifiRatio_MethodoOFF()
    {
        scifiRatio.SetActive(false);
    }

    void _building_MethodON()
    {
        building.SetActive(true);
    }
    void _building_MethodoOFF()
    {
        building.SetActive(false);
    }

    void _h20_text_MethodON()
    {
        h20_text.SetActive(true);
    }
    void _h20_text_MethodoOFF()
    {
        h20_text.SetActive(false);
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
    //
    void _D13_MethodON()
    {
        D13.SetActive(true);
    }
    void _D13_MethodoOFF()
    {
        D13.SetActive(false);
    }
    //
    void _D14_MethodON()
    {
        D14.SetActive(true);
    }
    void _D14_MethodoOFF()
    {
        D14.SetActive(false);
    }
    //
    void _D15_MethodON()
    {
        D15.SetActive(true);
    }
    void _D15_MethodoOFF()
    {
        D15.SetActive(false);
    }
    //
    void _D16_MethodON()
    {
        D16.SetActive(true);
    }
    void _D16_MethodoOFF()
    {
        D16.SetActive(false);
    }
    //
    void _D17_MethodON()
    {
        D17.SetActive(true);
    }
    void _D17_MethodoOFF()
    {
        D17.SetActive(false);
    }
    //
    void _D18_MethodON()
    {
        D18.SetActive(true);
    }
    void _D18_MethodoOFF()
    {
        D18.SetActive(false);
    }
    //
    void _D19_MethodON()
    {
        D19.SetActive(true);
    }
    void _D19_MethodoOFF()
    {
        D19.SetActive(false);
    }
    //
    void _D20_MethodON()
    {
        D20.SetActive(true);
    }
    void _D20_MethodoOFF()
    {
        D20.SetActive(false);
    }
    //
    void _D21_MethodON()
    {
        D21.SetActive(true);
    }
    void _D21_MethodoOFF()
    {
        D21.SetActive(false);
    }
    //







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
    void _T3_MethodoOFF()
    {
        T3.SetActive(false);
    }
    //
    void _T4_MethodON()
    {
        T4.SetActive(true);
    }
    void _T4_MethodoOFF()
    {
        T4.SetActive(false);
    }
    //
    void _T5_MethodON()
    {
        T5.SetActive(true);
    }
    void _T5_MethodoOFF()
    {
        T5.SetActive(false);
    }
    //
    void _T6_MethodON()
    {
        T6.SetActive(true);
    }
    void _T6_MethodoOFF()
    {
        T6.SetActive(false);
    }
    //
    void _T7_MethodON()
    {
        T7.SetActive(true);
    }
    void _T7_MethodoOFF()
    {
        T7.SetActive(false);
    }
    //
    void _T8_MethodON()
    {
        T8.SetActive(true);
    }
    void _T8_MethodoOFF()
    {
        T8.SetActive(false);
    }
    //
    void _T9_MethodON()
    {
        T9.SetActive(true);
    }
    void _T9_MethodoOFF()
    {
        T9.SetActive(false);
    }
    //
    void _T10_MethodON()
    {
        T10.SetActive(true);
    }
    void _T10_MethodoOFF()
    {
        T10.SetActive(false);
    }
    //
    void _T11_MethodON()
    {
        T11.SetActive(true);
    }
    void _T11_MethodoOFF()
    {
        T11.SetActive(false);
    }
    //
    void _T12_MethodON()
    {
        T12.SetActive(true);
    }
    void _T12_MethodoOFF()
    {
        T12.SetActive(false);
    }
    //
    void _T13_MethodON()
    {
        T13.SetActive(true);
    }
    void _T13_MethodoOFF()
    {
        T13.SetActive(false);
    }
    //
    void _T14_MethodON()
    {
        T14.SetActive(true);
    }
    void _T14_MethodoOFF()
    {
        T14.SetActive(false);
    }
    //
    void _T15_MethodON()
    {
        T15.SetActive(true);
    }
    void _T15_MethodoOFF()
    {
        T15.SetActive(false);
    }
    //
    void _T16_MethodON()
    {
        T16.SetActive(true);
    }
    void _T16_MethodoOFF()
    {
        T16.SetActive(false);
    }
    //
    void _T17_MethodON()
    {
        T17.SetActive(true);
    }
    void _T17_MethodoOFF()
    {
        T17.SetActive(false);
    }
    //
    void _T18_MethodON()
    {
        T18.SetActive(true);
    }
    void _T18_MethodoOFF()
    {
        T18.SetActive(false);
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
    void _audio_109b_audioMethod()
    {
        myAudio.clip = audio_109b;
        myAudio.Play();
    }
    void _audio_110b_audioMethod()
    {
        myAudio.clip = audio_110b;
        myAudio.Play();
    }
    void _audio_111b_audioMethod()
    {
        myAudio.clip = audio_111b;
        myAudio.Play();
    }
    void _audio_112b_audioMethod()
    {
        myAudio.clip = audio_112b;
        myAudio.Play();
    }
    void _audio_113b_audioMethod()
    {
        myAudio.clip = audio_113b;
        myAudio.Play();
    }
    void _audio_114b_audioMethod()
    {
        myAudio.clip = audio_114b;
        myAudio.Play();
    }
    void _audio_115b_audioMethod()
    {
        myAudio.clip = audio_115b;
        myAudio.Play();
    }
    void _audio_116b_audioMethod()
    {
        myAudio.clip = audio_116b;
        myAudio.Play();
    }
    void _audio_117b_audioMethod()
    {
        myAudio.clip = audio_117b;
        myAudio.Play();
    }
    void _audio_118b_audioMethod()
    {
        myAudio.clip = audio_118b;
        myAudio.Play();
    }
    void _audio_119b_audioMethod()
    {
        myAudio.clip = audio_119b;
        myAudio.Play();
    }
    void _audio_120b_audioMethod()
    {
        myAudio.clip = audio_120b;
        myAudio.Play();
    }
    void _audio_121b_audioMethod()
    {
        myAudio.clip = audio_121b;
        myAudio.Play();
    }
    void _audio_122b_audioMethod()
    {
        myAudio.clip = audio_122b;
        myAudio.Play();
    }
    void _audio_123b_audioMethod()
    {
        myAudio.clip = audio_123b;
        myAudio.Play();
    }
    void _audio_124b_audioMethod()
    {
        myAudio.clip = audio_124b;
        myAudio.Play();
    }
    void _audio_125b_audioMethod()
    {
        myAudio.clip = audio_125b;
        myAudio.Play();
    }
    void _audio_126b_audioMethod()
    {
        myAudio.clip = audio_126b;
        myAudio.Play();
    }
    void _audio_127b_audioMethod()
    {
        myAudio.clip = audio_127b;
        myAudio.Play();
    }
    void _audio_128b_audioMethod()
    {
        myAudio.clip = audio_128b;
        myAudio.Play();
    }
    void _audio_129b_audioMethod()
    {
        myAudio.clip = audio_129b;
        myAudio.Play();
    }
    void _audio_130b_audioMethod()
    {
        myAudio.clip = audio_130b;
        myAudio.Play();
    }
    void _audio_131b_audioMethod()
    {
        myAudio.clip = audio_131b;
        myAudio.Play();
    }
    void _audio_132b_audioMethod()
    {
        myAudio.clip = audio_132b;
        myAudio.Play();
    }
    void _audio_133b_audioMethod()
    {
        myAudio.clip = audio_133b;
        myAudio.Play();
    }
    void _audio_134b_audioMethod()
    {
        myAudio.clip = audio_134b;
        myAudio.Play();
    }

    void _audio_135b_audioMethod()
    {
        myAudio.clip = audio_135b;
        myAudio.Play();
    }
    void _audio_136b_audioMethod()
    {
        myAudio.clip = audio_136b;
        myAudio.Play();
    }
    void _audio_137b_audioMethod()
    {
        myAudio.clip = audio_137b;
        myAudio.Play();
    }
    void _audio_138b_audioMethod()
    {
        myAudio.clip = audio_138b;
        myAudio.Play();
    }
    void _audio_139b_audioMethod()
    {
        myAudio.clip = audio_139b;
        myAudio.Play();
    }
    void _audio_140b_audioMethod()
    {
        myAudio.clip = audio_140b;
        myAudio.Play();
    }
    void _audio_141b_audioMethod()
    {
        myAudio.clip = audio_141b;
        myAudio.Play();
    }
    void _audio_142b_audioMethod()
    {
        myAudio.clip = audio_142b;
        myAudio.Play();
    }
    void _audio_143b_audioMethod()
    {
        myAudio.clip = audio_143b;
        myAudio.Play();
    }






}
