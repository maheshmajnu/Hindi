using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sfx_control_and_coordination_10_class : MonoBehaviour
{


    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    private Animator anim;
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
            animator.Play("camara animation", 0, targetNormalizedTime);
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

    //Skip() {level()}




    
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
        JustInstantiatedNoPlayerCanvas.SetActive(true);

        InitializeFromCheckpoint();
        level();
        
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

    public GameObject ql1;
    //public TargetController lv1MiniGame;
    public void Lv1TurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 6)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            ql1.SetActive(true);
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
        SaveProgress(1, 0, 2);
        StartCoroutine(DelayLv3AMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv3AMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv3AMiniGame.Output();
    }


    public TargetController lv3AMiniGame;


    public void Lv3ATurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 9)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Level3B();
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public void Level3B()
    {
        StartCoroutine(DelayLv3BMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv3BMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv3BMiniGame.Output();
    }


    public TargetController lv3BMiniGame;

    public void Level3C()
    {
        StartCoroutine(DelayLv3CMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv3CMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv3CMiniGame.Output();
    }


    public TargetController lv3CMiniGame;

    public void Lv3CTurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 5)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Level4();
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }
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





    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip voice1;
    public AudioClip voice2;
    public AudioClip voice3;
    public AudioClip voice4;
    public AudioClip voice5;

    public AudioClip voice7;
    public AudioClip voice8;
    public AudioClip voice9;
    public AudioClip voice10;
    public AudioClip voice11;
    public AudioClip voice12;
    public AudioClip voice13;
    public AudioClip voice14;
    public AudioClip voice15;
    public AudioClip voice16;
    public AudioClip voice17;
    public AudioClip voice18;
    public AudioClip voice19;
    public AudioClip voice20;
    public AudioClip voice21;
    public AudioClip voice22;
    public AudioClip voice23;
    public AudioClip voice24;
    public AudioClip voice25;
    public AudioClip voice26;
    public AudioClip voice27;
    public AudioClip voice28;
    public AudioClip voice29;
    public AudioClip voice30;
    public AudioClip voice31;
    public AudioClip voice32;
    public AudioClip voice33;
    public AudioClip voice34;
    public AudioClip voice35;
    public AudioClip voice36;
    public AudioClip voice37;
    public AudioClip voice38;
    public AudioClip voice39;
    public AudioClip voice40;
    public AudioClip voice41;
    public AudioClip voice42;
    public AudioClip voice43;
    public AudioClip voice44;
    public AudioClip voice45;
    public AudioClip voice46;
    public AudioClip voice47;
    public AudioClip voice48;
    public AudioClip voice49;
    public AudioClip voice50;
    public AudioClip voice51;
    public AudioClip voice52;
    public AudioClip voice53;
    public AudioClip voice54;
    public AudioClip voice55;
    public AudioClip voice56;
    public AudioClip voice57;




    public AudioClip voice58;
    public AudioClip voice59;
    public AudioClip voice60;
    public AudioClip voice61;
    public AudioClip voice62;
    public AudioClip voice63;
    public AudioClip voice64;
    public AudioClip voice65;
    public AudioClip voice66;
    public AudioClip voice67;
    public AudioClip voice68;
    public AudioClip voice69;
    public AudioClip voice70;
    public AudioClip voice71;
    public AudioClip voice72;
    public AudioClip voice73;




    public AudioClip voice74;
    public AudioClip voice75;
    public AudioClip voice76;
    public AudioClip voice77;
    public AudioClip voice78;
    public AudioClip voice79;
    public AudioClip voice80;
    public AudioClip voice81;
    public AudioClip voice82;
    public AudioClip voice83;







    // ON - OFF gameobjects
    [Header("Explanation Assets")]



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
    public GameObject T14a;
    public GameObject T14ab;

    public GameObject T15;
    public GameObject T16;
    public GameObject T17;
    public GameObject T18;
    public GameObject T19;
    public GameObject T20;
    public GameObject T21;
    public GameObject T22;
    public GameObject T23;
    public GameObject T24;
    public GameObject T25;
    public GameObject T26;







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




    public GameObject ob1;
    public GameObject ob2;
    public GameObject ob3;
    public GameObject ob4;




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

    void _voice1_audioMethod()
    {
        myAudio.clip = voice1;
        myAudio.Play();
    }

    //
    void _Svoice2_audioMethod()
    {
        myAudio.clip = voice2;
        myAudio.Play();
    }
    //
    void _voice3_audioMethod()
    {
        myAudio.clip = voice3;
        myAudio.Play();
    }
    //
    void _voice4_audioMethod()
    {
        myAudio.clip = voice4;
        myAudio.Play();
    }
    //
    void _voice5_audioMethod()
    {
        myAudio.clip = voice5;
        myAudio.Play();
    }
 
    void _voice7_audioMethod()
    {
        myAudio.clip = voice7;
        myAudio.Play();
    }
    //
    void _voice8_audioMethod()
    {
        myAudio.clip = voice8;
        myAudio.Play();
    }
    //
    void _voice9_audioMethod()
    {
        myAudio.clip = voice9;
        myAudio.Play();
    }
    //
    void _voice10_audioMethod()
    {
        myAudio.clip = voice10;
        myAudio.Play();
    }
    //
    void _voice11_audioMethod()
    {
        myAudio.clip = voice11;
        myAudio.Play();
    }
    //
    void _voice12_audioMethod()
    {
        myAudio.clip = voice12;
        myAudio.Play();
    }
    //
    void _voice13_audioMethod()
    {
        myAudio.clip = voice13;
        myAudio.Play();
    }
    //
    void _voice14_audioMethod()
    {
        myAudio.clip = voice14;
        myAudio.Play();
    }
    //
    void _voice15_audioMethod()
    {
        myAudio.clip = voice15;
        myAudio.Play();
    }
    //
    void _voice16_audioMethod()
    {
        myAudio.clip = voice16;
        myAudio.Play();
    }
    //
    void _voice17_audioMethod()
    {
        myAudio.clip = voice17;
        myAudio.Play();
    }
    //
    void _voice18_audioMethod()
    {
        myAudio.clip = voice18;
        myAudio.Play();
    }
    //
    void _voice19_audioMethod()
    {
        myAudio.clip = voice19;
        myAudio.Play();
    }
    //
    void _voice20_audioMethod()
    {
        myAudio.clip = voice20;
        myAudio.Play();
    }
    //
    void _voice21_audioMethod()
    {
        myAudio.clip = voice21;
        myAudio.Play();
    }
    //
    void _voice22_audioMethod()
    {
        myAudio.clip = voice22;
        myAudio.Play();
    }
    //
    void _voice23_audioMethod()
    {
        myAudio.clip = voice23;
        myAudio.Play();
    }
    //
    void _voice24_audioMethod()
    {
        myAudio.clip = voice24;
        myAudio.Play();
    }
    //
    void _voice25_audioMethod()
    {
        myAudio.clip = voice25;
        myAudio.Play();
    }
    //
    void _voice26_audioMethod()
    {
        myAudio.clip = voice26;
        myAudio.Play();
    }
    //
    void _voice27_audioMethod()
    {
        myAudio.clip = voice27;
        myAudio.Play();
    }
    //
    void _voice28_audioMethod()
    {
        myAudio.clip = voice28;
        myAudio.Play();
    }
    //
    void _voice29_audioMethod()
    {
        myAudio.clip = voice29;
        myAudio.Play();
    }
    //
    void _voice30_audioMethod()
    {
        myAudio.clip = voice30;
        myAudio.Play();
    }
    //
    void _voice31_audioMethod()
    {
        myAudio.clip = voice31;
        myAudio.Play();
    }
    //
    void _voice32_audioMethod()
    {
        myAudio.clip = voice32;
        myAudio.Play();
    }
    //
    void _voice33_audioMethod()
    {
        myAudio.clip = voice33;
        myAudio.Play();
    }
    //
    void _voice34_audioMethod()
    {
        myAudio.clip = voice34;
        myAudio.Play();
    }
    //
    void _voice35_audioMethod()
    {
        myAudio.clip = voice35;
        myAudio.Play();
    }
    //
    void _voice36_audioMethod()
    {
        myAudio.clip = voice36;
        myAudio.Play();
    }
    //
    void _voice37_audioMethod()
    {
        myAudio.clip = voice37;
        myAudio.Play();
    }
    //
    void _voice38_audioMethod()
    {
        myAudio.clip = voice38;
        myAudio.Play();
    }
    //
    void _voice39_audioMethod()
    {
        myAudio.clip = voice39;
        myAudio.Play();
    }
    //
    void _voice40_audioMethod()
    {
        myAudio.clip = voice40;
        myAudio.Play();
    }
    //
    void _voice41_audioMethod()
    {
        myAudio.clip = voice41;
        myAudio.Play();
    }
    //
    void _voice42_audioMethod()
    {
        myAudio.clip = voice42;
        myAudio.Play();
    }
    //
    void _voice43_audioMethod()
    {
        myAudio.clip = voice43;
        myAudio.Play();
    }
    //
    void _voice44_audioMethod()
    {
        myAudio.clip = voice44;
        myAudio.Play();
    }
    //
    void _voice45_audioMethod()
    {
        myAudio.clip = voice45;
        myAudio.Play();
    }
    //
    void _voice46_audioMethod()
    {
        myAudio.clip = voice46;
        myAudio.Play();
    }
    //
    void _voice47_audioMethod()
    {
        myAudio.clip = voice47;
        myAudio.Play();
    }
    //
    void _voice48_audioMethod()
    {
        myAudio.clip = voice48;
        myAudio.Play();
    }
    //
    void _voice49_audioMethod()
    {
        myAudio.clip = voice49;
        myAudio.Play();
    }
    //
    void _voice50_audioMethod()
    {
        myAudio.clip = voice50;
        myAudio.Play();
    }
    //
    void _voice51_audioMethod()
    {
        myAudio.clip = voice51;
        myAudio.Play();
    }
    //
    void _voice52_audioMethod()
    {
        myAudio.clip = voice52;
        myAudio.Play();
    }
    //
    void _voice53_audioMethod()
    {
        myAudio.clip = voice53;
        myAudio.Play();
    }
    //
    void _voice54_audioMethod()
    {
        myAudio.clip = voice54;
        myAudio.Play();
    }
    //
    void _voice55_audioMethod()
    {
        myAudio.clip = voice55;
        myAudio.Play();
    }
    //
    void _voice56_audioMethod()
    {
        myAudio.clip = voice56;
        myAudio.Play();
    }
    //
    void _voice57_audioMethod()
    {
        myAudio.clip = voice57;
        myAudio.Play();
    }






    //
    void _voice58_audioMethod()
    {
        myAudio.clip = voice58;
        myAudio.Play();
    }
    //
    void _voice59_audioMethod()
    {
        myAudio.clip = voice59;
        myAudio.Play();
    }
    //
    void _voice60_audioMethod()
    {
        myAudio.clip = voice60;
        myAudio.Play();
    }
    //
    void _voice61_audioMethod()
    {
        myAudio.clip = voice61;
        myAudio.Play();
    }
    //
    void _voice62_audioMethod()
    {
        myAudio.clip = voice62;
        myAudio.Play();
    }
    //
    void _voice63_audioMethod()
    {
        myAudio.clip = voice63;
        myAudio.Play();
    }
    //
    void _voice64_audioMethod()
    {
        myAudio.clip = voice64;
        myAudio.Play();
    }
    //
    void _voice65_audioMethod()
    {
        myAudio.clip = voice65;
        myAudio.Play();
    }
    //
    void _voice66_audioMethod()
    {
        myAudio.clip = voice66;
        myAudio.Play();
    }
    //
    void _voice67_audioMethod()
    {
        myAudio.clip = voice67;
        myAudio.Play();
    }
    //
    void _voice68_audioMethod()
    {
        myAudio.clip = voice68;
        myAudio.Play();
    }
    //
    void _voice69_audioMethod()
    {
        myAudio.clip = voice69;
        myAudio.Play();
    }
    //
    void _voice70_audioMethod()
    {
        myAudio.clip = voice70;
        myAudio.Play();
    }
    //
    void _voice71_audioMethod()
    {
        myAudio.clip = voice71;
        myAudio.Play();
    }
    //
    void _voice72_audioMethod()
    {
        myAudio.clip = voice72;
        myAudio.Play();
    }
    //
    void _voice73_audioMethod()
    {
        myAudio.clip = voice73;
        myAudio.Play();
    }




    //
    void _voice74_audioMethod()
    {
        myAudio.clip = voice74;
        myAudio.Play();
    }
    //
    void _voice75_audioMethod()
    {
        myAudio.clip = voice75;
        myAudio.Play();
    }
    //
    void _voice76_audioMethod()
    {
        myAudio.clip = voice76;
        myAudio.Play();
    }
    //
    void _voice77_audioMethod()
    {
        myAudio.clip = voice77;
        myAudio.Play();
    }
    //
    void _voice78_audioMethod()
    {
        myAudio.clip = voice78;
        myAudio.Play();
    }
    //
    void _voice79_audioMethod()
    {
        myAudio.clip = voice79;
        myAudio.Play();
    }
    //
    void _voice80_audioMethod()
    {
        myAudio.clip = voice80;
        myAudio.Play();
    }
    //
    void _voice81_audioMethod()
    {
        myAudio.clip = voice81;
        myAudio.Play();
    }
    //
    void _voice82_audioMethod()
    {
        myAudio.clip = voice82;
        myAudio.Play();
    }
    //
    void _voice83_audioMethod()
    {
        myAudio.clip = voice83;
        myAudio.Play();
    }
    //








    // Line on/off






    //
    void T1_MethodOFF()
    {
        T1.SetActive(false);
    }
    //
    void T1_MethodON()
    {
        T1.SetActive(true);
    }
    //
    void T2_MethodOFF()
    {
        T2.SetActive(false);
    }
    //
    void T2_MethodON()
    {
        T2.SetActive(true);
    }
    //
    void T3_MethodOFF()
    {
        T3.SetActive(false);
    }
    //
    void T3_MethodON()
    {
        T3.SetActive(true);
    }
    //
    void T4_MethodOFF()
    {
        T4.SetActive(false);
    }
    //
    void T4_MethodON()
    {
        T4.SetActive(true);
    }
    //
    void T5_MethodOFF()
    {
        T5.SetActive(false);
    }
    //
    void T5_MethodON()
    {
        T5.SetActive(true);
    }
    //
    void T6_MethodOFF()
    {
        T6.SetActive(false);
    }
    //
    void T6_MethodON()
    {
        T6.SetActive(true);
    }
    //
    void T7_MethodOFF()
    {
        T7.SetActive(false);
    }
    //
    void T7_MethodON()
    {
        T7.SetActive(true);
    }
    //
    void T8_MethodOFF()
    {
        T8.SetActive(false);
    }
    //
    void T8_MethodON()
    {
        T8.SetActive(true);
    }
    //
    void T9_MethodOFF()
    {
        T9.SetActive(false);
    }
    //
    void T9_MethodON()
    {
        T9.SetActive(true);
    }
    //
    void T10_MethodOFF()
    {
        T10.SetActive(false);
    }
    //
    void T10_MethodON()
    {
        T10.SetActive(true);
    }
    //
    void T11_MethodOFF()
    {
        T11.SetActive(false);
    }
    //
    void T11_MethodON()
    {
        T11.SetActive(true);
    }
    //
    void T12_MethodOFF()
    {
        T12.SetActive(false);
    }
    //
    void T12_MethodON()
    {
        T12.SetActive(true);
    }
    //
    void T13_MethodOFF()
    {
        T13.SetActive(false);
    }
    //
    void T13_MethodON()
    {
        T13.SetActive(true);
    }
    //
    void T14_MethodOFF()
    {
        T14.SetActive(false);
    }
    //
    void T14_MethodON()
    {
        T14.SetActive(true);
    }



    //
    void T14a_MethodOFF()
    {
        T14a.SetActive(false);
    }
    //
    void T14a_MethodON()
    {
        T14a.SetActive(true);
    }



    //
    void T14ab_MethodOFF()
    {
        T14ab.SetActive(false);
    }
    //
    void T14ab_MethodON()
    {
        T14ab.SetActive(true);
    }




























    //
    void T15_MethodOFF()
    {
        T15.SetActive(false);
    }
    //
    void T15_MethodON()
    {
        T15.SetActive(true);
    }
    //
    void T16_MethodOFF()
    {
        T16.SetActive(false);
    }
    //
    void T16_MethodON()
    {
        T16.SetActive(true);
    }
    //
    void T17_MethodOFF()
    {
        T17.SetActive(false);
    }
    //
    void T17_MethodON()
    {
        T17.SetActive(true);
    }
    //
    void T18_MethodOFF()
    {
        T18.SetActive(false);
    }
    //
    void T18_MethodON()
    {
        T18.SetActive(true);
    }
    //
    void T19_MethodOFF()
    {
        T19.SetActive(false);
    }
    //
    void T19_MethodON()
    {
        T19.SetActive(true);
    }
    //
    void T20_MethodOFF()
    {
        T20.SetActive(false);
    }
    //
    void T20_MethodON()
    {
        T20.SetActive(true);
    }
    //
    void T21_MethodOFF()
    {
        T21.SetActive(false);
    }
    //
    void T21_MethodON()
    {
        T21.SetActive(true);
    }
    //
    void T22_MethodOFF()
    {
        T22.SetActive(false);
    }
    //
    void T22_MethodON()
    {
        T22.SetActive(true);
    }
    //
    void T23_MethodOFF()
    {
        T23.SetActive(false);
    }
    //
    void T23_MethodON()
    {
        T23.SetActive(true);
    }
    //


    void T24_MethodOFF()
    {
        T24.SetActive(false);
    }
    //
    void T24_MethodON()
    {
        T24.SetActive(true);
    }
    //




    void T25_MethodOFF()
    {
        T25.SetActive(false);
    }
    //
    void T25_MethodON()
    {
        T25.SetActive(true);
    }
    //

    void T26_MethodOFF()
    {
        T26.SetActive(false);
    }
    //
    void T26_MethodON()
    {
        T26.SetActive(true);
    }
    //









    void _D1MethodON()
    {
        D1.SetActive(true);
    }

    void _D1MethodOFF()
    {
        D1.SetActive(false);
    }





    void _D2MethodON()
    {
        D2.SetActive(true);
    }

    void _D2MethodOFF()
    {
        D2.SetActive(false);
    }



    void _D3MethodON()
    {
        D3.SetActive(true);
    }

    void _D3MethodOFF()
    {
        D3.SetActive(false);
    }



    void _D4MethodON()
    {
        D4.SetActive(true);
    }

    void _D4MethodOFF()
    {
        D4.SetActive(false);
    }



    void _D5MethodON()
    {
        D5.SetActive(true);
    }

    void _D5MethodOFF()
    {
        D5.SetActive(false);
    }



    void _D6MethodON()
    {
        D6.SetActive(true);
    }

    void _D6MethodOFF()
    {
        D6.SetActive(false);
    }



    void _D7MethodON()
    {
        D7.SetActive(true);
    }

    void _D7MethodOFF()
    {
        D7.SetActive(false);
    }








    void _D8MethodON()
    {
        D8.SetActive(true);
    }

    void _D8MethodOFF()
    {
        D8.SetActive(false);
    }



    void _D9MethodON()
    {
        D9.SetActive(true);
    }

    void _D9MethodOFF()
    {
        D9.SetActive(false);
    }



    void _D10MethodON()
    {
        D10.SetActive(true);
    }

    void _D10MethodOFF()
    {
        D10.SetActive(false);
    }



    void _D11MethodON()
    {
        D11.SetActive(true);
    }

    void _D11MethodOFF()
    {
        D11.SetActive(false);
    }



    void _D12MethodON()
    {
        D12.SetActive(true);
    }

    void _D12MethodOFF()
    {
        D12.SetActive(false);
    }



    void _D13MethodON()
    {
        D13.SetActive(true);
    }

    void _D13MethodOFF()
    {
        D13.SetActive(false);
    }



    void _D14MethodON()
    {
        D14.SetActive(true);
    }

    void _D14MethodOFF()
    {
        D14.SetActive(false);
    }



    void _D15MethodON()
    {
        D15.SetActive(true);
    }

    void _D15MethodOFF()
    {
        D15.SetActive(false);
    }



    void _D16MethodON()
    {
        D16.SetActive(true);
    }

    void _D16MethodOFF()
    {
        D16.SetActive(false);
    }



    void _D17MethodON()
    {
        D17.SetActive(true);
    }

    void _D17MethodOFF()
    {
        D17.SetActive(false);
    }



    void _D18MethodON()
    {
        D18.SetActive(true);
    }

    void _D18MethodOFF()
    {
        D18.SetActive(false);
    }



    void _D19MethodON()
    {
        D19.SetActive(true);
    }

    void _D19MethodOFF()
    {
        D19.SetActive(false);
    }



    void _D20MethodON()
    {
        D20.SetActive(true);
    }

    void _D20MethodOFF()
    {
        D20.SetActive(false);
    }





















    void _ob1MethodON()
    {
        ob1.SetActive(true);
    }

    void _ob1MethodOFF()
    {
        ob1.SetActive(false);
    }







    void _ob2MethodON()
    {
        ob2.SetActive(true);
    }

    void _ob2MethodOFF()
    {
        ob2.SetActive(false);
    }










    void _ob3MethodON()
    {
        ob3.SetActive(true);
    }

    void _ob3MethodOFF()
    {
        ob3.SetActive(false);
    }







    void _ob4MethodON()
    {
        ob4.SetActive(true);
    }

    void _ob4MethodOFF()
    {
        ob4.SetActive(false);
    }





















}
