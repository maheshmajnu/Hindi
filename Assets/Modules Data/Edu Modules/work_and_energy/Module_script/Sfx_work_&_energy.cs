using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sfx_work_and_energy : MonoBehaviour
{

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
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
            case 1: Level6(); break;
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
        JustInstantiatedNoPlayerCanvas.SetActive(true);

        level();
        
    }

    public GameObject lv1;
    public void level()
    {
        miniGames.Output();
        //SaveProgress(0,0,0);
        //InitializeFromCheckpoint();
    }

    public void Level6()
    {
        StartCoroutine(DelayLv6MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(1, 0, 5);
    }
    IEnumerator DelayLv6MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv6MiniGame.Output();
    }
    public TargetController lv6MiniGame;

    public Camera cam;
    public LayerMask layerMask;
    private bool canChoose = true;
    public int index;
    private void Update()
    {
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

        if (shouldSkipLevel1)
        {

            lv1.SetActive(false);

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

    //public void EndGameDelay()
    //{
    //    miniGames.EndMiniGame();
    //}

    


    //public void Level1()
    //{
    //    StartCoroutine(DelayLv1MiniGameStart());
    //    InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    //}
    //IEnumerator DelayLv1MiniGameStart()
    //{
    //    yield return new WaitForSeconds(1);
    //    lv1MiniGame.Output();
    //}

    //public GameObject ql1;
    ////public TargetController lv1MiniGame;
    //public void Lv1TurnOnMeshRend(MeshRenderer mesh)
    //{
    //    index++;
    //    mesh.enabled = true;

    //    if (index == 6)
    //    {
    //        index = 0;
    //        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    //        ql1.SetActive(true);
    //        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    //    }
    //}

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


    //Titles




    public GameObject title1;
    public GameObject title2;
    public GameObject title3;
    public GameObject title4a;
    public GameObject title4;
    public GameObject title5;
    public GameObject title6;
    public GameObject title7;
    public GameObject title8;
    public GameObject title9;
    public GameObject title10;
    public GameObject title11;
    public GameObject title11a;
    public GameObject title12;
    public GameObject title13;
    public GameObject title14;


    public GameObject down1;
    public GameObject down2;
    public GameObject down3;
    public GameObject down4;
    public GameObject down5;



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
    public AudioClip para46;
    public AudioClip para47;
    public AudioClip para48;
    public AudioClip para49;
    public AudioClip para50;
    public AudioClip para51;
    public AudioClip para52;
    public AudioClip para53;
    public AudioClip para54;
    public AudioClip para55;
    public AudioClip para56;
    public AudioClip para57;
    public AudioClip para58;
    public AudioClip para59;
    public AudioClip para60;
    public AudioClip para61;



    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject s_text;
    public GameObject f_text;
    public GameObject lose_gain_text;
    public GameObject h_text;
    public GameObject workdone_text;
    public GameObject ho_text;
    public GameObject lawsofenergy1_text;
    public GameObject lawsofenergy2_text;
    public GameObject power_text;
    public GameObject power_text1;
    public GameObject motor_text;
    public GameObject motorexp_text;
    public GameObject scentist_text;
    public GameObject formsenergy_text;
    public GameObject mass_text;
    public GameObject m_text;
    public GameObject propine;
    public GameObject gt_text;
    public GameObject sin_text;

    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject running_ani;
    public GameObject lorry_pushing_ani;
    public GameObject pushing_ani;
    public GameObject climbing_ani;
    public GameObject pulling_ani;
    public GameObject ballthrow_ani;
    public GameObject table_ball_ani;
    public GameObject car_ani;
    public GameObject wind_ani;
    public GameObject speedcar_ani;
    public GameObject slowcar_ani;
    public GameObject boxrise_ani;
    public GameObject fallingball_ani;
    public GameObject bag_pick_ani;
    public GameObject rubber_band_ani;
    public GameObject rubber_reverse_ani;
    public GameObject fast_runner_ani;
    public GameObject slow_runner_ani;
    public GameObject laws_of_energy_ani;











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








    void _title4a_MethodON()
    {
        title4a.SetActive(true);
    }

    void _title4a_MethodOFF()
    {
        title4a.SetActive(false);
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

    void _title10_MethodON()
    {
        title10.SetActive(true);
    }

    void _title10_MethodOFF()
    {
        title10.SetActive(false);
    }


    void _title11_MethodON()
    {
        title11.SetActive(true);
    }

    void _title11_MethodOFF()
    {
        title11.SetActive(false);
    }







    void _title11a_MethodON()
    {
        title11a.SetActive(true);
    }

    void _title11a_MethodOFF()
    {
        title11a.SetActive(false);
    }


















    void _title12_MethodON()
    {
        title12.SetActive(true);
    }

    void _title12_MethodOFF()
    {
        title12.SetActive(false);
    }


    void _title13_MethodON()
    {
        title13.SetActive(true);
    }

    void _title13_MethodOFF()
    {
        title13.SetActive(false);
    }



    void _title14_MethodON()
    {
        title14.SetActive(true);
    }

    void _title14_MethodOFF()
    {
        title14.SetActive(false);
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


    //

    void para46_method()
    {
        myAudio.clip = para46;
        myAudio.Play();
    }


    //

    void para47_method()
    {
        myAudio.clip = para47;
        myAudio.Play();
    }


    //

    void para48_method()
    {
        myAudio.clip = para48;
        myAudio.Play();
    }


    //

    void para49_method()
    {
        myAudio.clip = para49;
        myAudio.Play();
    }



    //

    void para50_method()
    {
        myAudio.clip = para50;
        myAudio.Play();
    }



    //

    void para51_method()
    {
        myAudio.clip = para51;
        myAudio.Play();
    }



    //

    void para52_method()
    {
        myAudio.clip = para52;
        myAudio.Play();
    }


    //

    void para53_method()
    {
        myAudio.clip = para53;
        myAudio.Play();
    }



    //

    void para54_method()
    {
        myAudio.clip = para54;
        myAudio.Play();
    }



    //

    void para55_method()
    {
        myAudio.clip = para55;
        myAudio.Play();
    }


    //

    void para56_method()
    {
        myAudio.clip = para56;
        myAudio.Play();
    }



    //

    void para57_method()
    {
        myAudio.clip = para57;
        myAudio.Play();
    }


    //

    void para58_method()
    {
        myAudio.clip = para58;
        myAudio.Play();
    }


    //

    void para59_method()
    {
        myAudio.clip = para59;
        myAudio.Play();
    }


    //

    void para60_method()
    {
        myAudio.clip = para60;
        myAudio.Play();
    }


    //

    void para61_method()
    {
        myAudio.clip = para61;
        myAudio.Play();
    }



    void _s_text_MethodON()
    {
        s_text.SetActive(true);
    }

    void _s_text_MethodOFF()
    {
        s_text.SetActive(false);
    }


    void _f_text_MethodON()
    {
        f_text.SetActive(true);
    }

    void _f_text_MethodOFF()
    {
        f_text.SetActive(false);
    }


    void _lose_gain_text_MethodON()
    {
        lose_gain_text.SetActive(true);
    }

    void _lose_gain_text_MethodOFF()
    {
        lose_gain_text.SetActive(false);
    }

    void _h_text_MethodON()
    {
        h_text.SetActive(true);
    }

    void _h_text_MethodOFF()
    {
        h_text.SetActive(false);
    }

    void _workdone_text_MethodON()
    {
        workdone_text.SetActive(true);
    }

    void _workdone_text_MethodOFF()
    {
        workdone_text.SetActive(false);
    }


    void _ho_text_MethodON()
    {
        ho_text.SetActive(true);
    }

    void _ho_text_MethodOFF()
    {
        ho_text.SetActive(false);
    }

    void _lawsofenergy1text_MethodON()
    {
        lawsofenergy1_text.SetActive(true);
    }

    void _lawsofenergy1_text_MethodOFF()
    {
        lawsofenergy1_text.SetActive(false);
    }

    void _lawsofenergy2text_MethodON()
    {
        lawsofenergy2_text.SetActive(true);
    }

    void _lawsofenergy2_text_MethodOFF()
    {
        lawsofenergy2_text.SetActive(false);
    }

    void _power_text_MethodON()
    {
        power_text.SetActive(true);
    }

    void _power_text_MethodOFF()
    {
        power_text.SetActive(false);
    }

    void _power_text1_MethodON()
    {
        power_text1.SetActive(true);
    }

    void _power_text1_MethodOFF()
    {
        power_text1.SetActive(false);
    }

    void _motor_text_MethodON()
    {
        motor_text.SetActive(true);
    }

    void _motor_text_MethodOFF()
    {
        motor_text.SetActive(false);
    }

    void _motorexp_text_MethodON()
    {
        motorexp_text.SetActive(true);
    }

    void _motorexp_text_MethodOFF()
    {
        motorexp_text.SetActive(false);
    }

    void _scentist_text_MethodON()
    {
        scentist_text.SetActive(true);
    }

    void _scentist_text_MethodOFF()
    {
        scentist_text.SetActive(false);
    }

    void _formsenergy_text_MethodON()
    {
        formsenergy_text.SetActive(true);
    }

    void _formsenergy_text_MethodOFF()
    {
        formsenergy_text.SetActive(false);
    }

    void _mass_text_MethodON()
    {
        mass_text.SetActive(true);
    }

    void _mass_text_MethodOFF()
    {
        mass_text.SetActive(false);
    }

    void _m_text_MethodON()
    {
        m_text.SetActive(true);
    }

    void _m_text_MethodOFF()
    {
        m_text.SetActive(false);
    }

    void _propine_MethodON()
    {
        propine.SetActive(true);
    }

    void _propine_MethodOFF()
    {
        propine.SetActive(false);
    }

    void _gt_text_MethodON()
    {
        gt_text.SetActive(true);
    }

    void _gt_text_MethodOFF()
    {
        gt_text.SetActive(false);
    }

    void _sin_text_MethodON()
    {
        sin_text.SetActive(true);
    }

    void _sin_text_MethodOFF()
    {
        sin_text.SetActive(false);
    }






    //Animation Play

    void _running_anianimMethod()
    {
        anim = running_ani.GetComponent<Animator>();
        anim.Play("running_ani");
    }


    void _lorry_pushing_anianimMethod()
    {
        anim = lorry_pushing_ani.GetComponent<Animator>();
        anim.Play("lorry_pushing_ani");
    }

    void _pushing_anianimMethod()
    {
        anim = pushing_ani.GetComponent<Animator>();
        anim.Play("pushing_ani");
    }

    void _climbing_anianimMethod()
    {
        anim = climbing_ani.GetComponent<Animator>();
        anim.Play("climbling_ani");
    }

    void _pulling_anianimMethod()
    {
        anim = pulling_ani.GetComponent<Animator>();
        anim.Play("pulling_ani");
    }

    void _ballthrow_anianimMethod()
    {
        anim = ballthrow_ani.GetComponent<Animator>();
        anim.Play("ball_throw_ani");
    }

    void _table_ball_anianimMethod()
    {
        anim = table_ball_ani.GetComponent<Animator>();
        anim.Play("table_ball_ani");
    }

    void _car_anianimMethod()
    {
        anim = car_ani.GetComponent<Animator>();
        anim.Play("car_ani");
    }

    void _wind_anianimMethod()
    {
        anim = wind_ani.GetComponent<Animator>();
        anim.Play("wind_mill_ani");
    }

    void _speedcar_anianimMethod()
    {
        anim = speedcar_ani.GetComponent<Animator>();
        anim.Play("speed_car_ani");
    }

    void _slowcar_anianimMethod()
    {
        anim = slowcar_ani.GetComponent<Animator>();
        anim.Play("slowcar_ani");
    }

    void _boxrise_anianimMethod()
    {
        anim = boxrise_ani.GetComponent<Animator>();
        anim.Play("boxrise_ani");
    }

    void _fallingball_anianimMethod()
    {
        anim = fallingball_ani.GetComponent<Animator>();
        anim.Play("fallingball_ani");
    }

    void _bag_pick_anianimMethod()
    {
        anim = bag_pick_ani.GetComponent<Animator>();
        anim.Play("bag_pickin_ani");
    }

    void _rubber_band_anianimMethod()
    {
        anim = rubber_band_ani.GetComponent<Animator>();
        anim.Play("rubber_band_ani");
    }

    void _rubber_reverse_anianimMethod()
    {
        anim = rubber_reverse_ani.GetComponent<Animator>();
        anim.Play("rubber_reverse_ani");
    }

    void _fast_runner_anianimMethod()
    {
        anim = fast_runner_ani.GetComponent<Animator>();
        anim.Play("fast_runner_ani");
    }

    void _slow_runner_anianimMethod()
    {
        anim = slow_runner_ani.GetComponent<Animator>();
        anim.Play("slow_runner_ani");
    }

    void _laws_energy_anianimMethod()
    {
        anim = laws_of_energy_ani.GetComponent<Animator>();
        anim.Play("laws_of_energy_ani");
    }



}
