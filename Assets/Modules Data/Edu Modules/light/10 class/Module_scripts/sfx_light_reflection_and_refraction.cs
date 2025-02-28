using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sfx_light_reflection_and_refraction : MonoBehaviour
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
            animator.Play("camera_ani", 0, targetNormalizedTime);
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

    public void InitializeFromCheckpoint()
    {
        var (checkpoint, currentStep, currentObjective) = CheckpointManager.Instance.LoadCheckpoint();
        CheckpointManager.Instance.RestoreCheckpoint();

        shouldSkipLevel1 = checkpoint != 0 || currentStep != 0 || currentObjective != 0;


        switch (checkpoint)
        {
            case 0: Level1(); break;
            case 1: Level4(); break;
            case 2: Level8(); break;
            case 3: Level12(); break;
            case 4: Level16(); break;
            default: Level1(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
    }

    //Skip() {level()}




   
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

        //miniGames.Output();
        Level1();
        InitializeFromCheckpoint();
        //Level4();

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

    public void Level1()
    {
        miniGames.Output();
        StartCoroutine(DelayLv1MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv1MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        
    }

    //public GameObject ql1;
   //public TargetController lv1MiniGame;
    public void Lv1Complete()
    {
        index++;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        if (index == 4)
        {
            index = 0;
            Invoke("Level2", 1);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
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
        SaveProgress(1, 0, 3);
        StartCoroutine(DelayLv4MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv4MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv4MiniGame.Output();
    }
    public TargetController lv4MiniGame;

    public void Lv4ATurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Level4B();
            
        }
    }

    public void Level4B()
    {
        StartCoroutine(DelayLv4BMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv4BMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv4BMiniGame.Output();
    }
    public TargetController lv4BMiniGame;

    public void Lv4BTurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Level5();
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

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
    }
    IEnumerator DelayLv7MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv7MiniGame.Output();
    }
    public TargetController lv7MiniGame;

    public void Level8()
    {
        SaveProgress(2, 0, 7);
        StartCoroutine(DelayLv8MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv8MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv8MiniGame.Output();
    }
    public TargetController lv8MiniGame;

    public void Lv8TurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 1)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(DelayLv8BMiniGameStart());
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    IEnumerator DelayLv8BMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv8BMiniGame.Output();
    }
    public TargetController lv8BMiniGame;

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
        SaveProgress(3, 0, 11);
        StartCoroutine(DelayLv12MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv12MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv12MiniGame.Output();
    }
    public TargetController lv12MiniGame;

    public void Level13()
    {
        StartCoroutine(DelayLv13MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv13MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv13MiniGame.Output();
    }
    public TargetController lv13MiniGame;

    public void Level14()
    {
        StartCoroutine(DelayLv14MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv14MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv14MiniGame.Output();
    }
    public TargetController lv14MiniGame;

    public void Level15()
    {
        StartCoroutine(DelayLv15MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv15MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv15MiniGame.Output();
    }
    public TargetController lv15MiniGame;

    public void Level15B()
    {
        StartCoroutine(DelayLv15BMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv15BMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv15BMiniGame.Output();
    }
    public TargetController lv15BMiniGame;

    public void Lv15BTurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Level15C();

        }
    }

    public void Level15C()
    {
        StartCoroutine(DelayLv15CMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv15CMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv15CMiniGame.Output();
    }
    public TargetController lv15CMiniGame;

    public void Lv15CTurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Level16();

        }
    }

    public void Level16()
    {
        SaveProgress(4, 0, 15);
        StartCoroutine(DelayLv16MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv16MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv16MiniGame.Output();
    }
    public TargetController lv16MiniGame;

    public void Level17()
    {
        StartCoroutine(DelayLv17MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv17MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv17MiniGame.Output();
    }
    public TargetController lv17MiniGame;

    public void lv17EndGame()
    {
        lv17MiniGame.EndMiniGame();
    }

    public Transform SpawnPoint18;
    public void Level18()
    {
        InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint18);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
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
    public GameObject title25;
    public GameObject title26;
    public GameObject title27;
    public GameObject title28;
    public GameObject title29;
    public GameObject title30;
    public GameObject title31;
    public GameObject title32;
    public GameObject title33;
    public GameObject title34;
    public GameObject title35;
    public GameObject title36;
    public GameObject title37;
    public GameObject title38;
    public GameObject title39;
    public GameObject title40;
    public GameObject title41;
    public GameObject title42;
    public GameObject title43;
    public GameObject title44;
    public GameObject title45;
    public GameObject title46;
    public GameObject title47;
    public GameObject title48;
    public GameObject title49;
    public GameObject title50;
    public GameObject title51;
    public GameObject title52;
    public GameObject title53;
    public GameObject title54;
    public GameObject title55;
    public GameObject title56;
    public GameObject title57;
    public GameObject title58;
    public GameObject title59;
    public GameObject title60;
    public GameObject title61;
    public GameObject title62;
    public GameObject title63;
    public GameObject title64;
    public GameObject title65;
    public GameObject title66;

    public GameObject down1;
    public GameObject down2;





    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject torch_light_ani;
    public GameObject ani_1;
    public GameObject ani_2;
    public GameObject ani_3;
    public GameObject ani_4;
    public GameObject ani_6;
    public GameObject parll_ani1;
    public GameObject parll_ani2;
    public GameObject parll_ani3;
    public GameObject glass_slab_ani;
    public GameObject fifty_ani;
    public GameObject nines_ani;
    public GameObject lightee_ani;
    public GameObject obli_ani;










    // ON - OFF gameobjects
    [Header("Explanation Assets")]
   
   
    public GameObject texttwo;
    public GameObject textthree;
    public GameObject textfour;
    public GameObject textfive;
    public GameObject textsix;
    public GameObject textseven;
    public GameObject texteight;
    public GameObject textnine;
    public GameObject full_part;
    public GameObject cc_text;
    public GameObject pole_text;
    public GameObject coc_text;
    public GameObject roc_text;
    public GameObject pa_text;
    public GameObject sign_text;
    public GameObject measur_text;
    public GameObject polx_text;
    public GameObject dire_text;
    public GameObject height_text;
    public GameObject he2_text;
    public GameObject mirr_text;
    public GameObject mag_text;
    public GameObject hei_text;
    public GameObject audio66_text;
    public GameObject video66_text;
    public GameObject audio71_text;
    public GameObject circles;
    public GameObject dots;
    public GameObject lines;
    public GameObject op;
    public GameObject foo;
    public GameObject fl;
    public GameObject nine_text;
    public GameObject nin_text;
    public GameObject thi_text;
    public GameObject bhi_text;
    public GameObject yhi_text;





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
    public AudioClip para69;
    public AudioClip para70;
    public AudioClip para71;
    public AudioClip para72;
    public AudioClip para73;
    public AudioClip para74;
    public AudioClip para75;
    public AudioClip para76;
    public AudioClip para77;
    public AudioClip para78;
    public AudioClip para79;
    public AudioClip para80;
    public AudioClip para81;
    public AudioClip para82;
    public AudioClip para83;
    public AudioClip para84;
    public AudioClip para85;
    public AudioClip para86;
    public AudioClip para87;
    public AudioClip para88;
    public AudioClip para89;
    public AudioClip para90;
    public AudioClip para91;
    public AudioClip para92;
    public AudioClip para93;
    public AudioClip para94;
    public AudioClip para95;
    public AudioClip para96;
    public AudioClip para97;

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

    void _title25_MethodON()
    {
        title25.SetActive(true);
    }

    void _title25_MethodOFF()
    {
        title25.SetActive(false);
    }

    void _title26_MethodON()
    {
        title26.SetActive(true);
    }

    void _title26_MethodOFF()
    {
        title26.SetActive(false);
    }

    void _title27_MethodON()
    {
        title27.SetActive(true);
    }

    void _title27_MethodOFF()
    {
        title27.SetActive(false);
    }

    void _title28_MethodON()
    {
        title28.SetActive(true);
    }

    void _title28_MethodOFF()
    {
        title28.SetActive(false);
    }

    void _title29_MethodON()
    {
        title29.SetActive(true);
    }

    void _title29_MethodOFF()
    {
        title29.SetActive(false);
    }

    void _title30_MethodON()
    {
        title30.SetActive(true);
    }

    void _title30_MethodOFF()
    {
        title30.SetActive(false);
    }


    void _title31_MethodON()
    {
        title31.SetActive(true);
    }

    void _title31_MethodOFF()
    {
        title31.SetActive(false);
    }

    void _title32_MethodON()
    {
        title32.SetActive(true);
    }

    void _title32_MethodOFF()
    {
        title32.SetActive(false);
    }

    void _title33_MethodON()
    {
        title33.SetActive(true);
    }

    void _title33_MethodOFF()
    {
        title33.SetActive(false);
    }

    void _title34_MethodON()
    {
        title34.SetActive(true);
    }

    void _title34_MethodOFF()
    {
        title34.SetActive(false);
    }


    void _title35_MethodON()
    {
        title35.SetActive(true);
    }

    void _title35_MethodOFF()
    {
        title35.SetActive(false);
    }

    void _title36_MethodON()
    {
        title36.SetActive(true);
    }

    void _title36_MethodOFF()
    {
        title36.SetActive(false);
    }

    void _title37_MethodON()
    {
        title37.SetActive(true);
    }

    void _title37_MethodOFF()
    {
        title37.SetActive(false);
    }

    void _title38_MethodON()
    {
        title38.SetActive(true);
    }

    void _title38_MethodOFF()
    {
        title38.SetActive(false);
    }

    void _title39_MethodON()
    {
        title39.SetActive(true);
    }

    void _title39_MethodOFF()
    {
        title39.SetActive(false);
    }

    void _title40_MethodON()
    {
        title40.SetActive(true);
    }

    void _title40_MethodOFF()
    {
        title40.SetActive(false);
    }

    void _title41_MethodON()
    {
        title41.SetActive(true);
    }

    void _title41_MethodOFF()
    {
        title41.SetActive(false);
    }

    void _title42_MethodON()
    {
        title42.SetActive(true);
    }

    void _title42_MethodOFF()
    {
        title42.SetActive(false);
    }


    void _title43_MethodON()
    {
        title43.SetActive(true);
    }

    void _title43_MethodOFF()
    {
        title43.SetActive(false);
    }

    void _title44_MethodON()
    {
        title44.SetActive(true);
    }

    void _title44_MethodOFF()
    {
        title44.SetActive(false);
    }


    void _title45_MethodON()
    {
        title45.SetActive(true);
    }

    void _title45_MethodOFF()
    {
        title45.SetActive(false);
    }

    void _title46_MethodON()
    {
        title46.SetActive(true);
    }

    void _title46_MethodOFF()
    {
        title46.SetActive(false);
    }


    void _title47_MethodON()
    {
        title47.SetActive(true);
    }

    void _title47_MethodOFF()
    {
        title47.SetActive(false);
    }

    void _title48_MethodON()
    {
        title48.SetActive(true);
    }

    void _title48_MethodOFF()
    {
        title48.SetActive(false);
    }

    void _title49_MethodON()
    {
        title49.SetActive(true);
    }

    void _title49_MethodOFF()
    {
        title49.SetActive(false);
    }


    void _title50_MethodON()
    {
        title50.SetActive(true);
    }

    void _title50_MethodOFF()
    {
        title50.SetActive(false);
    }

    void _title51_MethodON()
    {
        title51.SetActive(true);
    }

    void _title51_MethodOFF()
    {
        title51.SetActive(false);
    }

    void _title52_MethodON()
    {
        title52.SetActive(true);
    }

    void _title52_MethodOFF()
    {
        title52.SetActive(false);
    }

    void _title53_MethodON()
    {
        title53.SetActive(true);
    }

    void _title53_MethodOFF()
    {
        title53.SetActive(false);
    }

    void _title54_MethodON()
    {
        title54.SetActive(true);
    }

    void _title54_MethodOFF()
    {
        title54.SetActive(false);
    }

    void _title55_MethodON()
    {
        title55.SetActive(true);
    }

    void _title55_MethodOFF()
    {
        title55.SetActive(false);
    }

    void _title56_MethodON()
    {
        title56.SetActive(true);
    }

    void _title56_MethodOFF()
    {
        title56.SetActive(false);
    }


    void _title57_MethodON()
    {
        title57.SetActive(true);
    }

    void _title57_MethodOFF()
    {
        title57.SetActive(false);
    }


    void _title58_MethodON()
    {
        title58.SetActive(true);
    }

    void _title58_MethodOFF()
    {
        title58.SetActive(false);
    }

    void _title59_MethodON()
    {
        title59.SetActive(true);
    }

    void _title59_MethodOFF()
    {
        title59.SetActive(false);
    }


    void _title60_MethodON()
    {
        title60.SetActive(true);
    }

    void _title60_MethodOFF()
    {
        title60.SetActive(false);
    }


    void _title61_MethodON()
    {
        title61.SetActive(true);
    }

    void _title61_MethodOFF()
    {
        title61.SetActive(false);
    }


    void _title62_MethodON()
    {
        title62.SetActive(true);
    }

    void _title62_MethodOFF()
    {
        title62.SetActive(false);
    }

    void _title63_MethodON()
    {
        title63.SetActive(true);
    }

    void _title63_MethodOFF()
    {
        title63.SetActive(false);
    }

    void _title64_MethodON()
    {
        title64.SetActive(true);
    }

    void _title64_MethodOFF()
    {
        title64.SetActive(false);
    }

    void _title65_MethodON()
    {
        title65.SetActive(true);
    }

    void _title65_MethodOFF()
    {
        title65.SetActive(false);
    }

    void _title66_MethodON()
    {
        title66.SetActive(true);
    }

    void _title66_MethodOFF()
    {
        title66.SetActive(false);
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














    //Animation Play

    void torch_light_ani_anim_Method()
    {
        anim = torch_light_ani.GetComponent<Animator>();
        anim.Play("torch_light_ani");


    }

    void ani_1_anim_Method()
    {
        anim = ani_1.GetComponent<Animator>();
        anim.Play("ani_1");


    }

    void ani_2_anim_Method()
    {
        anim = ani_2.GetComponent<Animator>();
        anim.Play("ani_2");


    }


    void ani_3_anim_Method()
    {
        anim = ani_3.GetComponent<Animator>();
        anim.Play("ani_3");


    }

    void ani_4_anim_Method()
    {
        anim = ani_4.GetComponent<Animator>();
        anim.Play("ani_4");


    }

    void ani_6_anim_Method()
    {
        anim = ani_6.GetComponent<Animator>();
        anim.Play("ani_6");


    }

    void parll_ani1_anim_Method()
    {
        anim = parll_ani1.GetComponent<Animator>();
        anim.Play("parll_ani1");


    }


    void parll_ani2_anim_Method()
    {
        anim = parll_ani2.GetComponent<Animator>();
        anim.Play("parll_ani2");


    }


    void parll_ani3_anim_Method()
    {
        anim = parll_ani3.GetComponent<Animator>();
        anim.Play("parll_ani3");


    }

    void glass_slab_ani_anim_Method()
    {
        anim = glass_slab_ani.GetComponent<Animator>();
        anim.Play("glass_slab_ani");


    }


    void fifty_ani_anim_Method()
    {
        anim = fifty_ani.GetComponent<Animator>();
        anim.Play("fifty_ani");


    }

    void nines_ani_anim_Method()
    {
        anim = nines_ani.GetComponent<Animator>();
        anim.Play("nines_ani");


    }

    void lightee_ani_anim_Method()
    {
        anim = lightee_ani.GetComponent<Animator>();
        anim.Play("lightee_ani");


    }

    void obli_ani_anim_Method()
    {
        anim = obli_ani.GetComponent<Animator>();
        anim.Play("obli_ani");


    }




    void _texttwo_MethodON()
    {
        texttwo.SetActive(true);
    }

    void _texttwo_MethodOFF()
    {
        texttwo.SetActive(false);
    }


    void _textthree_MethodON()
    {
        textthree.SetActive(true);
    }

    void _textthree_MethodOFF()
    {
        textthree.SetActive(false);
    }

    void _textfour_MethodON()
    {
        textfour.SetActive(true);
    }

    void _textfour_MethodOFF()
    {
        textfour.SetActive(false);
    }



    void _textfive_MethodON()
    {
        textfive.SetActive(true);
    }

    void _textfive_MethodOFF()
    {
        textfive.SetActive(false);
    }


    void _textsix_MethodON()
    {
        textsix.SetActive(true);
    }

    void _textsix_MethodOFF()
    {
        textsix.SetActive(false);
    }


    void _textseven_MethodON()
    {
        textseven.SetActive(true);
    }

    void _textseven_MethodOFF()
    {
        textseven.SetActive(false);
    }


    void _texteight_MethodON()
    {
        texteight.SetActive(true);
    }

    void _texteight_MethodOFF()
    {
        texteight.SetActive(false);
    }


    void _textnine_MethodON()
    {
        textnine.SetActive(true);
    }

    void _textnine_MethodOFF()
    {
        textnine.SetActive(false);
    }

    void _full_part_MethodON()
    {
        full_part.SetActive(true);
    }

    void _full_part_MethodOFF()
    {
        full_part.SetActive(false);
    }

    void _cc_text_MethodON()
    {
        cc_text.SetActive(true);
    }

    void _cc_text_MethodOFF()
    {
        cc_text.SetActive(false);
    }


    void _pole_text_MethodON()
    {
        pole_text.SetActive(true);
    }

    void _pole_text_MethodOFF()
    {
        pole_text.SetActive(false);
    }

    void _coc_text_MethodON()
    {
        coc_text.SetActive(true);
    }

    void _coc_text_MethodOFF()
    {
        coc_text.SetActive(false);
    }

    void _roc_text_MethodON()
    {
        roc_text.SetActive(true);
    }

    void _roc_text_MethodOFF()
    {
        roc_text.SetActive(false);
    }

    void _pa_text_MethodON()
    {
        pa_text.SetActive(true);
    }

    void _pa_text_MethodOFF()
    {
        pa_text.SetActive(false);
    }


    void _sign_text_MethodON()
    {
        sign_text.SetActive(true);
    }

    void _sign_text_MethodOFF()
    {
        sign_text.SetActive(false);
    }

    void _measur_text_MethodON()
    {
        measur_text.SetActive(true);
    }

    void _measur_text_MethodOFF()
    {
        measur_text.SetActive(false);
    }


    void _polx_text_MethodON()
    {
        polx_text.SetActive(true);
    }

    void _polx_text_MethodOFF()
    {
        polx_text.SetActive(false);
    }


    void _dire_text_MethodON()
    {
        dire_text.SetActive(true);
    }

    void _dire_text_MethodOFF()
    {
        dire_text.SetActive(false);
    }


    void _height_text_MethodON()
    {
        height_text.SetActive(true);
    }

    void _height_text_MethodOFF()
    {
        height_text.SetActive(false);
    }


    void _he2_text_MethodON()
    {
        he2_text.SetActive(true);
    }

    void _he2_text_MethodOFF()
    {
        he2_text.SetActive(false);
    }

    void _mirr_text_MethodON()
    {
        mirr_text.SetActive(true);
    }

    void _mirr_text_MethodOFF()
    {
        mirr_text.SetActive(false);
    }


    void _mag_text_MethodON()
    {
        mag_text.SetActive(true);
    }

    void _mag_text_MethodOFF()
    {
        mag_text.SetActive(false);
    }

    void _hei_text_MethodON()
    {
        hei_text.SetActive(true);
    }

    void _hei_text_MethodOFF()
    {
        hei_text.SetActive(false);
    }


    void _audio66_text_MethodON()
    {
        audio66_text.SetActive(true);
    }

    void _audio66_text_MethodOFF()
    {
        audio66_text.SetActive(false);
    }



    void _video66_text_MethodON()
    {
        video66_text.SetActive(true);
    }

    void _video66_text_MethodOFF()
    {
        video66_text.SetActive(false);
    }

    void _audio71_text_MethodON()
    {
        audio71_text.SetActive(true);
    }

    void _audio71_text_MethodOFF()
    {
        audio71_text.SetActive(false);
    }


    void _circles_MethodON()
    {
        circles.SetActive(true);
    }

    void _circles_MethodOFF()
    {
        circles.SetActive(false);
    }

    void _dots_MethodON()
    {
        dots.SetActive(true);
    }

    void _dots_MethodOFF()
    {
        dots.SetActive(false);
    }

    void _lines_MethodON()
    {
        lines.SetActive(true);
    }

    void _lines_MethodOFF()
    {
        lines.SetActive(false);
    }


    void _op_MethodON()
    {
        op.SetActive(true);
    }

    void _op_MethodOFF()
    {
        op.SetActive(false);
    }

    void _foo_MethodON()
    {
        foo.SetActive(true);
    }

    void _foo_MethodOFF()
    {
        foo.SetActive(false);
    }

    void _fl_MethodON()
    {
        fl.SetActive(true);
    }

    void _fl_MethodOFF()
    {
        fl.SetActive(false);
    }

    void _nine_text_MethodON()
    {
        nine_text.SetActive(true);
    }

    void _nine_text_MethodOFF()
    {
        nine_text.SetActive(false);
    }

    void _nin_text_MethodON()
    {
        nin_text.SetActive(true);
    }

    void _nin_text_MethodOFF()
    {
        nin_text.SetActive(false);
    }

    void _thi_text_MethodON()
    {
        thi_text.SetActive(true);
    }

    void _thi_text_MethodOFF()
    {
        thi_text.SetActive(false);
    }

    void _bhi_text_MethodON()
    {
        bhi_text.SetActive(true);
    }

    void _bhi_text_MethodOFF()
    {
        bhi_text.SetActive(false);
    }


    void _yhi_text_MethodON()
    {
        yhi_text.SetActive(true);
    }

    void _yhi_text_MethodOFF()
    {
        yhi_text.SetActive(false);
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




    //Audio play

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



    //

    void para69_method()
    {
        myAudio.clip = para69;
        myAudio.Play();
    }





    //

    void para70_method()
    {
        myAudio.clip = para70;
        myAudio.Play();
    }


    //

    void para71_method()
    {
        myAudio.clip = para71;
        myAudio.Play();
    }



    //

    void para72_method()
    {
        myAudio.clip = para72;
        myAudio.Play();
    }



    //

    void para73_method()
    {
        myAudio.clip = para73;
        myAudio.Play();
    }




    //

    void para74_method()
    {
        myAudio.clip = para74;
        myAudio.Play();
    }



    //

    void para75_method()
    {
        myAudio.clip = para75;
        myAudio.Play();
    }


    //

    void para76_method()
    {
        myAudio.clip = para76;
        myAudio.Play();
    }


    //

    void para77_method()
    {
        myAudio.clip = para77;
        myAudio.Play();
    }



    //

    void para78_method()
    {
        myAudio.clip = para78;
        myAudio.Play();
    }



    //

    void para79_method()
    {
        myAudio.clip = para79;
        myAudio.Play();
    }



    //

    void para80_method()
    {
        myAudio.clip = para80;
        myAudio.Play();
    }




    //

    void para81_method()
    {
        myAudio.clip = para81;
        myAudio.Play();
    }




    //

    void para82_method()
    {
        myAudio.clip = para82;
        myAudio.Play();
    }





    //

    void para83_method()
    {
        myAudio.clip = para83;
        myAudio.Play();
    }


    //

    void para84_method()
    {
        myAudio.clip = para84;
        myAudio.Play();
    }



    //

    void para85_method()
    {
        myAudio.clip = para85;
        myAudio.Play();
    }



    //

    void para86_method()
    {
        myAudio.clip = para86;
        myAudio.Play();
    }




    //

    void para87_method()
    {
        myAudio.clip = para87;
        myAudio.Play();
    }



    //

    void para88_method()
    {
        myAudio.clip = para88;
        myAudio.Play();
    }


    //

    void para89_method()
    {
        myAudio.clip = para89;
        myAudio.Play();
    }



    //

    void para90_method()
    {
        myAudio.clip = para90;
        myAudio.Play();
    }





    //

    void para91_method()
    {
        myAudio.clip = para91;
        myAudio.Play();
    }


    //

    void para92_method()
    {
        myAudio.clip = para92;
        myAudio.Play();
    }



    //

    void para93_method()
    {
        myAudio.clip = para93;
        myAudio.Play();
    }



    //

    void para94_method()
    {
        myAudio.clip = para94;
        myAudio.Play();
    }




    //

    void para95_method()
    {
        myAudio.clip = para95;
        myAudio.Play();
    }



    //

    void para96_method()
    {
        myAudio.clip = para96;
        myAudio.Play();
    }


    //

    void para97_method()
    {
        myAudio.clip = para97;
        myAudio.Play();
    }


























































}
