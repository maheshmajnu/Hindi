using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sfx_motion : MonoBehaviour
{


    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    private GameObject JustInstantiatedNoPlayerCanvas;


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
            case 1: Level4(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
    }

    




    
    public void level()
    {
        miniGames.Output();

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
        //JustInstantiatedNoPlayerCanvas.SetActive(true);

        InitializeFromCheckpoint();
         level();
         //Level5();

    }

    public void PresistentReloadScene()
    {
        //GameObject loader = GameObject.Find("Sceneloader Canvas");
        //loader.GetComponent<SceneLoader>().LoadScene(1);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
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
        yield return new WaitForSeconds(4);
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

    public void DelayMissionFailed(float time)
    {
        Invoke("MissionFailed", time);
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


    public void MovingCamera(Transform CH)
    {
        
        transform.SetParent(CH);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        
    }

    public void MovingCamerarEnd()
    {
        transform.SetParent(null);
    }

    public void DelayMovingCameraEnd(float time)
    {
        Invoke("MovingCamerarEnd", time);
    }


    public void Level2()
    {
        StartCoroutine(DelayLv2MiniGameStart());
        //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv2MiniGameStart()
    {
        yield return new WaitForSeconds(3);
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

    public void Level3Question()
    {
        StartCoroutine(DelayLv3QMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv3QMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv3QMiniGame.Output();
    }
    public TargetController lv3QMiniGame;

    public void Level4()
    {
        StartCoroutine(DelayLv4MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(1, 0, 3);
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
    public AudioClip para62;
    public AudioClip para63;
    public AudioClip para64;
    public AudioClip para65;
    public AudioClip para66;
    public AudioClip para67;
    public AudioClip para68;

    public GameObject down1;




    // ON - OFF gameobjects
    [Header("Explanation Assets")]
   

    public GameObject distance_text;
    public GameObject time_text;
    public GameObject straight_line;
    public GameObject cross_line;
    public GameObject  t2;
    public GameObject d2;
    public GameObject hori_line;
    public GameObject s2;
    public GameObject time2;
    public GameObject uta;
    public GameObject newton_text;
    public GameObject meters_text;
    public GameObject displac_text;



    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject car_ani;
    public GameObject speed_car_ani;
    public GameObject ear_ani;
    public GameObject galaxy_ani;
    public GameObject satellite_ani;
    public GameObject uniform_ani;
    public GameObject chair_ani;
    public GameObject swing_ani;
    public GameObject ball_ani;
    public GameObject stone_ani;
    public GameObject object_ani;
    public GameObject table_car_ani;
    public GameObject slow_car_ani;

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





    //

    void para62_method()
    {
        myAudio.clip = para62;
        myAudio.Play();
    }


    //

    void para63_method()
    {
        myAudio.clip = para63;
        myAudio.Play();
    }



    //

    void para64_method()
    {
        myAudio.clip = para64;
        myAudio.Play();
    }



    //

    void para65_method()
    {
        myAudio.clip = para65;
        myAudio.Play();
    }




    //

    void para66_method()
    {
        myAudio.clip = para66;
        myAudio.Play();
    }



    //

    void para67_method()
    {
        myAudio.clip = para67;
        myAudio.Play();
    }


    //

    void para68_method()
    {
        myAudio.clip = para68;
        myAudio.Play();
    }


    void _distance_text_MethodON()
    {
        distance_text.SetActive(true);
    }

    void _distance_text_MethodOFF()
    {
        distance_text.SetActive(false);
    }

    void _time_text_MethodON()
    {
        time_text.SetActive(true);
    }

    void _time_text_MethodOFF()
    {
        time_text.SetActive(false);
    }

    void _straight_line_MethodON()
    {
        straight_line.SetActive(true);
    }

    void _straight_line_MethodOFF()
    {
        straight_line.SetActive(false);
    }

    void _cross_line_MethodON()
    {
        cross_line.SetActive(true);
    }

    void _cross_line_MethodOFF()
    {
        cross_line.SetActive(false);
    }

    void _t2_MethodON()
    {
        t2.SetActive(true);
    }

    void _t2_MethodOFF()
    {
        t2.SetActive(false);
    }

    void _d2_MethodON()
    {
        d2.SetActive(true);
    }

    void _d2_MethodOFF()
    {
        d2.SetActive(false);
    }
    void _hori_line_MethodON()
    {
        hori_line.SetActive(true);
    }

    void _hori_line_MethodOFF()
    {
        hori_line.SetActive(false);
    }
    void _s2_MethodON()
    {
        s2.SetActive(true);
    }

    void _s2_MethodOFF()
    {
        s2.SetActive(false);
    }
    void _time2_MethodON()
    {
        time2.SetActive(true);
    }

    void _time2_MethodOFF()
    {
        time2.SetActive(false);
    }
    void _uta_MethodON()
    {
        uta.SetActive(true);
    }

    void _uta_MethodOFF()
    {
        uta.SetActive(false);
    }
    void _newton_text_MethodON()
    {
        newton_text.SetActive(true);
    }

    void _newton_text_MethodOFF()
    {
        newton_text.SetActive(false);
    }

    void _meters_text_MethodON()
    {
        meters_text.SetActive(true);
    }

    void _meters_text_MethodOFF()
    {
        meters_text.SetActive(false);
    }

    void _displac_text_MethodON()
    {
       displac_text.SetActive(true);
    }

    void _displac_text_MethodOFF()
    {
        displac_text.SetActive(false);
    }







    //Animation Play

    void _car_anianimMethod()
    {
        anim = car_ani.GetComponent<Animator>();
        anim.Play("car_ani");
    }

    void speed_car_anianimMethod()
    {
        anim = speed_car_ani.GetComponent<Animator>();
        anim.Play("speed_car_ani");
    }

    void ear_ani_anianimMethod()
    {
        anim = ear_ani.GetComponent<Animator>();
        anim.Play("earth_ani");
    }

    void galaxy_ani_anianimMethod()
    {
        anim = galaxy_ani.GetComponent<Animator>();
        anim.Play("galaxy_ani");
    }

    void satellite_ani_anianimMethod()
    {
        anim = satellite_ani.GetComponent<Animator>();
        anim.Play("satellite_ani");
    }

    void uniform_ani_anianimMethod()
    {
        anim = uniform_ani.GetComponent<Animator>();
        anim.Play("uniform_ani");
    }

    void chair_ani_anianimMethod()
    {
        anim = chair_ani.GetComponent<Animator>();
        anim.Play("chair_ani");
    }

    void swing_ani_anianimMethod()
    {
        anim = swing_ani.GetComponent<Animator>();
        anim.Play("swing");
    }

    void ball_ani_anianimMethod()
    {
        anim = ball_ani.GetComponent<Animator>();
        anim.Play("ball_ani");
    }

    void stone_ani_anianimMethod()
    {
        anim = stone_ani.GetComponent<Animator>();
        anim.Play("stone_ani");
    }

    void object_ani_anianimMethod()
    {
        anim = object_ani.GetComponent<Animator>();
        anim.Play("object_ani");
    }

    void table_car_anianimMethod()
    {
        anim = table_car_ani.GetComponent<Animator>();
        anim.Play("table_car_ani");
    }

    void slow_car_anianimMethod()
    {
        anim = slow_car_ani.GetComponent<Animator>();
        anim.Play("slow_car_ani");
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

    void _down1_MethodON()
    {
        down1.SetActive(true);
    }

    void _down1_MethodOFF()
    {
        down1.SetActive(false);
    }








}
