using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor;
using UnityEngine;

public class sfx_electricity : MonoBehaviour
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
            animator.Play("camera_ani", 0, targetNormalizedTime);
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
            case 1: Level5(); break;
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
    
    public void Level5()
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
        SaveProgress(1, 0, 4);
    }

    public Transform waypoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypointCanvas.SetActive(true);
        waypoint.player = InventoryManager.Instance.player.transform;
        waypoint.target = target;
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
        //StartCoroutine(DelayLv7MiniGameStart());

        InitializeFromCheckpoint();
        level();
        SetWayPoint(waypoint1);

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

    public GameObject Qlv2;
    public GameObject GlowBulb;
    public void Level2()
    {
        index++;
        

        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Qlv2.SetActive(true);
            GlowBulb.SetActive(true);
            //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
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
    public GameObject title10;
    public GameObject title11;
    public GameObject title12;
    public GameObject title13;
    public GameObject title14;
    public GameObject title15;
    public GameObject title16;
    public GameObject title17;
    public GameObject title18;
    public GameObject title19;
    public GameObject title20;
    public GameObject title21;
    public GameObject title22;
    public GameObject title23;
    public GameObject title24;

    public GameObject down1;
    public GameObject down2;
    public GameObject down3;


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



    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject electric_ani;
    public GameObject ele_pot_ani;
    public GameObject pt_water_ani;



    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject one_text;
    public GameObject two_text;
    public GameObject metal_wire_text;
    public GameObject battery_text;
    public GameObject three_text;
    public GameObject amperes_text;
    public GameObject four_text;
    public GameObject five_text;
    public GameObject power_source_text;
    public GameObject conductor_text;
    public GameObject six_text;
    public GameObject seven_text;
    public GameObject eight_text;
    public GameObject nine_text;
    public GameObject ten_text;
    public GameObject eleven_text;
    public GameObject twelve_text;
    public GameObject thirteen_text;
    public GameObject fourteen_text;
    public GameObject resistor;
    public GameObject fifteen_text;
    public GameObject res;
    public GameObject sixteen_text;
    public GameObject seventeen_text;
    public GameObject eighteen_text;
    public GameObject nineteen_text;
    public GameObject twenty_text;
    public GameObject twone_text;
    public GameObject twtwo_text;
    public GameObject twthree_text;
    public GameObject refri_text;
    public GameObject kittle_text;
    public GameObject television_text;
    public GameObject twfour_text;
    public GameObject twfive_text;
    public GameObject heater_text;
    public GameObject bulb_text;
    public GameObject plug_text;
    public GameObject twseven_text;
    public GameObject tweight_text;
    public GameObject twnine_text;
    public GameObject thirty_text;
    public GameObject thione_text;
    public GameObject thitwo_text;
    public GameObject thithree_text;




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





    void _title15_MethodON()
    {
        title15.SetActive(true);
    }

    void _title15_MethodOFF()
    {
        title15.SetActive(false);
    }





    void _title16_MethodON()
    {
        title16.SetActive(true);
    }

    void _title16_MethodOFF()
    {
        title16.SetActive(false);
    }







    void _title17_MethodON()
    {
        title17.SetActive(true);
    }

    void _title17_MethodOFF()
    {
        title17.SetActive(false);
    }





    void _title18_MethodON()
    {
        title18.SetActive(true);
    }

    void _title18_MethodOFF()
    {
        title18.SetActive(false);
    }




    void _title19_MethodON()
    {
        title19.SetActive(true);
    }

    void _title19_MethodOFF()
    {
        title19.SetActive(false);
    }


    void _title20_MethodON()
    {
        title20.SetActive(true);
    }

    void _title20_MethodOFF()
    {
        title20.SetActive(false);
    }


    void _title21_MethodON()
    {
        title21.SetActive(true);
    }

    void _title21_MethodOFF()
    {
        title21.SetActive(false);
    }


    void _title22_MethodON()
    {
        title22.SetActive(true);
    }

    void _title22_MethodOFF()
    {
        title22.SetActive(false);
    }


    void _title23_MethodON()
    {
        title23.SetActive(true);
    }

    void _title23_MethodOFF()
    {
        title23.SetActive(false);
    }

    void _title24_MethodON()
    {
        title24.SetActive(true);
    }

    void _title24_MethodOFF()
    {
        title24.SetActive(false);
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







    //Animation Play

    void _electric_anianimMethod()
    {
        anim = electric_ani.GetComponent<Animator>();
        anim.Play("electric_ani");
    }


    void _ele_pot_anianimMethod()
    {
        anim = ele_pot_ani.GetComponent<Animator>();
        anim.Play("electric_potential_ani");
    }


    void _pt_water_anianimMethod()
    {
        anim = pt_water_ani.GetComponent<Animator>();
        anim.Play("pt_water_ani");
    }




    void _one_text_MethodON()
    {
        one_text.SetActive(true);
    }

    void _one_text_MethodOFF()
    {
        one_text.SetActive(false);
    }


    void _two_text_MethodON()
    {
        two_text.SetActive(true);
    }

    void _two_text_MethodOFF()
    {
        two_text.SetActive(false);
    }


    void _metal_wire_text_MethodON()
    {
        metal_wire_text.SetActive(true);
    }

    void _metal_wire_text_MethodOFF()
    {
        metal_wire_text.SetActive(false);
    }


    void _battery_text_MethodON()
    {
        battery_text.SetActive(true);
    }

    void _battery_text_MethodOFF()
    {
        battery_text.SetActive(false);
    }


    void _three_text_MethodON()
    {
        three_text.SetActive(true);
    }

    void _three_text_MethodOFF()
    {
        three_text.SetActive(false);
    }


    void _amperes_text_MethodON()
    {
        amperes_text.SetActive(true);
    }

    void _amperes_text_MethodOFF()
    {
        amperes_text.SetActive(false);
    }

    void _four_text_MethodON()
    {
        four_text.SetActive(true);
    }

    void _four_text_MethodOFF()
    {
        four_text.SetActive(false);
    }



    void _five_text_MethodON()
    {
        five_text.SetActive(true);
    }

    void _five_text_MethodOFF()
    {
        five_text.SetActive(false);
    }


    void _power_source_text_MethodON()
    {
        power_source_text.SetActive(true);
    }

    void _power_source_text_MethodOFF()
    {
        power_source_text.SetActive(false);
    }


    void _conductor_text_MethodON()
    {
        conductor_text.SetActive(true);
    }

    void _conductor_text_MethodOFF()
    {
        conductor_text.SetActive(false);
    }


    void _six_text_MethodON()
    {
        six_text.SetActive(true);
    }

    void _six_text_MethodOFF()
    {
        six_text.SetActive(false);
    }


    void _seven_text_MethodON()
    {
        seven_text.SetActive(true);
    }

    void _seven_text_MethodOFF()
    {
        seven_text.SetActive(false);
    }


    void _eight_text_MethodON()
    {
        eight_text.SetActive(true);
    }

    void _eight_text_MethodOFF()
    {
        eight_text.SetActive(false);
    }



    void _nine_text_MethodON()
    {
        nine_text.SetActive(true);
    }

    void _nine_text_MethodOFF()
    {
        nine_text.SetActive(false);
    }



    void _ten_text_MethodON()
    {
        ten_text.SetActive(true);
    }

    void _ten_text_MethodOFF()
    {
        ten_text.SetActive(false);
    }


    void _eleven_text_MethodON()
    {
        eleven_text.SetActive(true);
    }

    void _eleven_text_MethodOFF()
    {
        eleven_text.SetActive(false);
    }

    void _twelve_text_MethodON()
    {
        twelve_text.SetActive(true);
    }

    void _twelve_text_MethodOFF()
    {
        twelve_text.SetActive(false);
    }

    void _thirteen_text_MethodON()
    {
        thirteen_text.SetActive(true);
    }

    void _thirteen_text_MethodOFF()
    {
        thirteen_text.SetActive(false);
    }


    void _fourteen_text_MethodON()
    {
        fourteen_text.SetActive(true);
    }

    void _fourteen_text_MethodOFF()
    {
        fourteen_text.SetActive(false);
    }



    void _resistor_MethodON()
    {
        resistor.SetActive(true);
    }

    void _resistor_MethodOFF()
    {
        resistor.SetActive(false);
    }

    void _fifteen_text_MethodON()
    {
        fifteen_text.SetActive(true);
    }

    void _fifteen_text_MethodOFF()
    {
        fifteen_text.SetActive(false);
    }


    void _res_MethodON()
    {
        res.SetActive(true);
    }

    void _res_MethodOFF()
    {
        res.SetActive(false);
    }

    void _sixteen_text_MethodON()
    {
       sixteen_text.SetActive(true);
    }

    void _sixteen_text_MethodOFF()
    {
        sixteen_text.SetActive(false);
    }


    void _seventeen_text_MethodON()
    {
        seventeen_text.SetActive(true);
    }

    void _seventeen_text_MethodOFF()
    {
        seventeen_text.SetActive(false);
    }


    void _eighteen_text_MethodON()
    {
        eighteen_text.SetActive(true);
    }

    void _eighteen_text_MethodOFF()
    {
        eighteen_text.SetActive(false);
    }


    void _nineteen_text_MethodON()
    {
        nineteen_text.SetActive(true);
    }

    void _nineteen_text_MethodOFF()
    {
        nineteen_text.SetActive(false);
    }

    void _twenty_text_MethodON()
    {
        twenty_text.SetActive(true);
    }

    void _twenty_text_MethodOFF()
    {
        twenty_text.SetActive(false);
    }


    void _twone_text_MethodON()
    {
        twone_text.SetActive(true);
    }

    void _twone_text_MethodOFF()
    {
        twone_text.SetActive(false);
    }

    void _twtwo_text_MethodON()
    {
        twtwo_text.SetActive(true);
    }

    void _twtwo_text_MethodOFF()
    {
        twtwo_text.SetActive(false);
    }


    void _twthree_text_MethodON()
    {
        twthree_text.SetActive(true);
    }

    void _twthree_text_MethodOFF()
    {
        twthree_text.SetActive(false);
    }


    void _refri_text_MethodON()
    {
        refri_text.SetActive(true);
    }

    void _refri_text_MethodOFF()
    {
        refri_text.SetActive(false);
    }

    void _kittle_text_MethodON()
    {
        kittle_text.SetActive(true);
    }

    void _kittle_text_MethodOFF()
    {
        kittle_text.SetActive(false);
    }


    void _television_text_MethodON()
    {
        television_text.SetActive(true);
    }

    void _television_text_MethodOFF()
    {
        television_text.SetActive(false);
    }

    void _twfour_text_MethodON()
    {
        twfour_text.SetActive(true);
    }

    void _twfour_text_MethodOFF()
    {
        twfour_text.SetActive(false);
    }


    void _twfive_text_MethodON()
    {
        twfive_text.SetActive(true);
    }

    void _twfive_text_MethodOFF()
    {
        twfive_text.SetActive(false);
    }


    void _heater_text_MethodON()
    {
        heater_text.SetActive(true);
    }

    void _heater_text_MethodOFF()
    {
        heater_text.SetActive(false);
    }


    void _bulb_text_MethodON()
    {
        bulb_text.SetActive(true);
    }

    void _bulb_text_MethodOFF()
    {
        bulb_text.SetActive(false);
    }


    

    void _plug_text_MethodON()
    {
        plug_text.SetActive(true);
    }

    void _plug_text_MethodOFF()
    {
        plug_text.SetActive(false);
    }


    void _twseven_text_MethodON()
    {
        twseven_text.SetActive(true);
    }

    void _twseven_text_MethodOFF()
    {
        twseven_text.SetActive(false);
    }


    void _tweight_text_MethodON()
    {
        tweight_text.SetActive(true);
    }

    void _tweight_text_MethodOFF()
    {
        tweight_text.SetActive(false);
    }


    void _twnine_text_MethodON()
    {
        twnine_text.SetActive(true);
    }

    void _twnine_text_MethodOFF()
    {
        twnine_text.SetActive(false);
    }


    void _thirty_text_MethodON()
    {
        thirty_text.SetActive(true);
    }

    void _thirty_text_MethodOFF()
    {
        thirty_text.SetActive(false);
    }


    void _thione_text_MethodON()
    {
        thione_text.SetActive(true);
    }

    void _thione_text_MethodOFF()
    {
        thione_text.SetActive(false);
    }

    void _thitwo_text_MethodON()
    {
        thitwo_text.SetActive(true);
    }

    void _thitwo_text_MethodOFF()
    {
        thitwo_text.SetActive(false);
    }

    void _thithree_text_MethodON()
    {
        thithree_text.SetActive(true);
    }

    void _thithree_text_MethodOFF()
    {
        thithree_text.SetActive(false);
    }








}
