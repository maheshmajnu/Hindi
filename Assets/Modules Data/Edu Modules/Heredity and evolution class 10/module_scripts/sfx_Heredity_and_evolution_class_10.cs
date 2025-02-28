using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sfx_Heredity_and_evolution_class_10 : MonoBehaviour
{
    private Animator anim;
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
            animator.Play("camara anim", 0, targetNormalizedTime);
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
            case 1: Level6(); break;
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

    public Transform SpawnPoint6;
    
    public void Level6()
    {
        SaveProgress(1, 0, 5);
        SpawnPointTransform(SpawnPoint6);
        
    }
  



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

    public void EndMiniGame(TargetController targetController)
    {
        StartCoroutine(DelayEndMiniGame(targetController));
    }
    IEnumerator DelayEndMiniGame(TargetController targetController)
    {
        yield return new WaitForSeconds(5);
        targetController.EndMiniGame();
    }

    public void SpawnPointTransform(Transform SpawnPoint)
    {
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        StartCoroutine(DelaySpawnPointTransform(SpawnPoint));

    }

    IEnumerator DelaySpawnPointTransform(Transform SpawnPoint)
    {
        yield return new WaitForSeconds(2);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();

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

    public GameObject lv7q;
    public void Lv7GAME()
    {
        index++;
        

        if (index == 2)
        {
            index = 0;
            lv7q.SetActive(true);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
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


    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;


    public AudioClip audio01;

    public AudioClip audio02;
    public AudioClip audio03;
    public AudioClip audio04;
    public AudioClip audio05;
    public AudioClip audio06;
    public AudioClip audio07;
    public AudioClip audio08;
    public AudioClip audio09;
    public AudioClip audio10;

    public AudioClip audio11;
    public AudioClip audio11a;
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
    public AudioClip voice84;
    public AudioClip voice85;
    public AudioClip voice86;
    public AudioClip voice87;
    public AudioClip voice88;
    public AudioClip voice89;
    public AudioClip voice90;
    public AudioClip voice91;
    public AudioClip voice92;
    public AudioClip voice93;
    public AudioClip voice94;
    public AudioClip voice95;
    public AudioClip voice96;
    public AudioClip voice97;
    public AudioClip voice98;
    public AudioClip voice99;
    public AudioClip voice100;
    public AudioClip voice101;
    public AudioClip voice102;
    public AudioClip voice103;
    public AudioClip voice104;
    public AudioClip voice105;
    public AudioClip voice106;
    public AudioClip voice107;



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































    // Titles



    public GameObject HeredityT;
    public GameObject VariationsT;
    public GameObject AccumulationT;
    public GameObject ImportanceT;
    public GameObject Rules_of_heredityT;
    public GameObject Types_of_traitsT;
    public GameObject Acquired_traitsT;
    public GameObject RulesT;
    public GameObject geneT;
    public GameObject Dominant_traitT;
    public GameObject Recessive_traitT;
    public GameObject Rules_of_inheritanceT;
    public GameObject Mendel_ExperimentsT;
    public GameObject Dihybrid_crossT;
    public GameObject Mendel_lawsT;



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
    public GameObject T20;
    public GameObject T21;
    public GameObject T22;
    public GameObject T23;
    public GameObject T24;












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




    // Object




    public GameObject OB1;
    public GameObject OB2;
    public GameObject OB3;
    public GameObject OB4;
    public GameObject OB5;


    public GameObject OB6;
    public GameObject OB7;
    public GameObject OB8;
    public GameObject OB9;
    public GameObject OB10;




















    void _audio01_audioMethod()

    {
        myAudio.clip = audio01;
        myAudio.Play();
    }



    void _audio02_audioMethod()

    {
        myAudio.clip = audio02;
        myAudio.Play();
    }

    void _audio03_audioMethod()

    {
        myAudio.clip = audio03;
        myAudio.Play();
    }

    void _audio04_audioMethod()

    {
        myAudio.clip = audio04;
        myAudio.Play();
    }

    void _audio05_audioMethod()

    {
        myAudio.clip = audio05;
        myAudio.Play();
    }


    void _audio06_audioMethod()

    {
        myAudio.clip = audio06;
        myAudio.Play();
    }

    void _audio07_audioMethod()

    {
        myAudio.clip = audio07;
        myAudio.Play();
    }


    void _audio08_audioMethod()

    {
        myAudio.clip = audio08;
        myAudio.Play();
    }


    void _audio09_audioMethod()

    {
        myAudio.clip = audio09;
        myAudio.Play();
    }

    void _audio10_audioMethod()

    {
        myAudio.clip = audio10;
        myAudio.Play();
    }


    void _audio11_audioMethod()

    {
        myAudio.clip = audio11;
        myAudio.Play();
    }




    void _audio11a_audioMethod()

    {
        myAudio.clip = audio11a;
        myAudio.Play();
    }
















    void _audio12_audioMethod()

    {
        myAudio.clip = audio12;
        myAudio.Play();
    }

    void _audio13_audioMethod()

    {
        myAudio.clip = audio13;
        myAudio.Play();
    }


    void _audio14_audioMethod()

    {
        myAudio.clip = audio14;
        myAudio.Play();
    }


    void _audio15_audioMethod()

    {
        myAudio.clip = audio15;
        myAudio.Play();
    }


    void _audio16_audioMethod()

    {
        myAudio.clip = audio16;
        myAudio.Play();
    }



    void _audio17_audioMethod()

    {
        myAudio.clip = audio17;
        myAudio.Play();
    }



    void _audio18_audioMethod()

    {
        myAudio.clip = audio18;
        myAudio.Play();
    }



    void _audio19_audioMethod()

    {
        myAudio.clip = audio19;
        myAudio.Play();
    }




    void _audio20_audioMethod()

    {
        myAudio.clip = audio20;
        myAudio.Play();
    }





    void _audio21_audioMethod()

    {
        myAudio.clip = audio21;
        myAudio.Play();
    }

    void _audio22_audioMethod()

    {
        myAudio.clip = audio22;
        myAudio.Play();
    }

    void _audio23_audioMethod()

    {
        myAudio.clip = audio23;
        myAudio.Play();
    }

    void _audio24_audioMethod()

    {
        myAudio.clip = audio24;
        myAudio.Play();
    }

    void _audio25_audioMethod()

    {
        myAudio.clip = audio25;
        myAudio.Play();
    }

    void _audio26_audioMethod()

    {
        myAudio.clip = audio26;
        myAudio.Play();
    }

    void _audio27_audioMethod()

    {
        myAudio.clip = audio27;
        myAudio.Play();
    }

    void _audio28_audioMethod()

    {
        myAudio.clip = audio28;
        myAudio.Play();
    }

    void _audio29_audioMethod()

    {
        myAudio.clip = audio29;
        myAudio.Play();
    }

    void _audio30_audioMethod()

    {
        myAudio.clip = audio30;
        myAudio.Play();
    }






    void _audio31_audioMethod()

    {
        myAudio.clip = audio31;
        myAudio.Play();
    }
    void _audio32_audioMethod()

    {
        myAudio.clip = audio32;
        myAudio.Play();
    }
    void _audio33_audioMethod()

    {
        myAudio.clip = audio33;
        myAudio.Play();
    }
    void _audio34_audioMethod()

    {
        myAudio.clip = audio34;
        myAudio.Play();
    }
    void _audio35_audioMethod()

    {
        myAudio.clip = audio35;
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
    void _voice84_audioMethod()
    {
        myAudio.clip = voice84;
        myAudio.Play();
    }
    //
    void _voice85_audioMethod()
    {
        myAudio.clip = voice85;
        myAudio.Play();
    }
    //
    void _voice86_audioMethod()
    {
        myAudio.clip = voice86;
        myAudio.Play();
    }
    //
    void _voice87_audioMethod()
    {
        myAudio.clip = voice87;
        myAudio.Play();
    }
    //
    void _voice88_audioMethod()
    {
        myAudio.clip = voice88;
        myAudio.Play();
    }
    //
    void _voice89_audioMethod()
    {
        myAudio.clip = voice89;
        myAudio.Play();
    }
    //
    void _voice90_audioMethod()
    {
        myAudio.clip = voice90;
        myAudio.Play();
    }
    //
    void _voice91_audioMethod()
    {
        myAudio.clip = voice91;
        myAudio.Play();
    }
    //
    void _voice92_audioMethod()
    {
        myAudio.clip = voice92;
        myAudio.Play();
    }
    //

    void _voice93_audioMethod()
    {
        myAudio.clip = voice93;
        myAudio.Play();
    }
    //


    void _voice94_audioMethod()
    {
        myAudio.clip = voice94;
        myAudio.Play();
    }
    //

    void _voice95_audioMethod()
    {
        myAudio.clip = voice95;
        myAudio.Play();
    }
    //
    void _voice96_audioMethod()
    {
        myAudio.clip = voice96;
        myAudio.Play();
    }
    //
    void _voice97_audioMethod()
    {
        myAudio.clip = voice97;
        myAudio.Play();
    }
    //
    void _voice98_audioMethod()
    {
        myAudio.clip = voice98;
        myAudio.Play();
    }
    //
    void _voice99_audioMethod()
    {
        myAudio.clip = voice99;
        myAudio.Play();
    }
    //
    void _voice100_audioMethod()
    {
        myAudio.clip = voice100;
        myAudio.Play();
    }
    //
    void _voice101_audioMethod()
    {
        myAudio.clip = voice101;
        myAudio.Play();
    }
    //
    void _voice102_audioMethod()
    {
        myAudio.clip = voice102;
        myAudio.Play();
    }
    //
    void _voice103_audioMethod()
    {
        myAudio.clip = voice103;
        myAudio.Play();
    }
    //
    void _voice104_audioMethod()
    {
        myAudio.clip = voice104;
        myAudio.Play();
    }
    //
    void _voice105_audioMethod()
    {
        myAudio.clip = voice105;
        myAudio.Play();
    }
    //
    void _voice106_audioMethod()
    {
        myAudio.clip = voice106;
        myAudio.Play();
    }
    //
    void _voice107_audioMethod()
    {
        myAudio.clip = voice107;
        myAudio.Play();
    }




































    // Titles


    void _HeredityTMethodON()
    {
        HeredityT.SetActive(true);
    }

    void _HeredityTMethodOFF()
    {
        HeredityT.SetActive(false);
    }

    




    void _VariationsTMethodON()
    {
        VariationsT.SetActive(true);
    }

    void _VariationsTMethodOFF()
    {
        VariationsT.SetActive(false);
    }






    void _AccumulationTMethodON()
    {
        AccumulationT.SetActive(true);
    }

    void _AccumulationTMethodOFF()
    {
        AccumulationT.SetActive(false);
    }



    void _ImportanceTMethodON()
    {
        ImportanceT.SetActive(true);
    }

    void _ImportanceTMethodOFF()
    {
        ImportanceT.SetActive(false);
    }



    void _Rules_of_heredityTMethodON()
    {
        Rules_of_heredityT.SetActive(true);
    }

    void _Rules_of_heredityTMethodOFF()
    {
        Rules_of_heredityT.SetActive(false);
    }



    void _Types_of_traitsTMethodON()
    {
        Types_of_traitsT.SetActive(true);
    }

    void _Types_of_traitsTMethodOFF()
    {
        Types_of_traitsT.SetActive(false);
    }



    void _Acquired_traitsTMethodON()
    {
        Acquired_traitsT.SetActive(true);
    }

    void _Acquired_traitsTMethodOFF()
    {
        Acquired_traitsT.SetActive(false);
    }



    void _RulesTMethodON()
    {
        RulesT.SetActive(true);
    }

    void _RulesTMethodOFF()
    {
        RulesT.SetActive(false);
    }


    void _geneTMethodON()
    {
        geneT.SetActive(true);
    }

    void _geneTMethodOFF()
    {
        geneT.SetActive(false);
    }


    void _Dominant_traitTMethodON()
    {
        Dominant_traitT.SetActive(true);
    }

    void _Dominant_traitTMethodOFF()
    {
        Dominant_traitT.SetActive(false);
    }


    void _Recessive_traitTMethodON()
    {
        Recessive_traitT.SetActive(true);
    }

    void _Recessive_traitTMethodOFF()
    {
        Recessive_traitT.SetActive(false);
    }


    void _Rules_of_inheritanceTMethodON()
    {
        Rules_of_inheritanceT.SetActive(true);
    }

    void _Rules_of_inheritanceTMethodOFF()
    {
        Rules_of_inheritanceT.SetActive(false);
    }





    void _Mendel_ExperimentsTMethodON()
    {
        Mendel_ExperimentsT.SetActive(true);
    }

    void _Mendel_ExperimentsTMethodOFF()
    {
        Mendel_ExperimentsT.SetActive(false);
    }



    void _Dihybrid_crossTMethodON()
    {
        Dihybrid_crossT.SetActive(true);
    }

    void _Dihybrid_crossTMethodOFF()
    {
        Dihybrid_crossT.SetActive(false);
    }



    void _Mendel_lawsTMethodON()
    {
        Mendel_lawsT.SetActive(true);
    }

    void _Mendel_lawsTMethodOFF()
    {
        Mendel_lawsT.SetActive(false);
    }
















    void _T1MethodON()
    {
        T1.SetActive(true);
    }

    void _T1MethodOFF()
    {
        T1.SetActive(false);
    }





    void _T2MethodON()
    {
        T2.SetActive(true);
    }

    void _T2MethodOFF()
    {
        T2.SetActive(false);
    }



    void _T3MethodON()
    {
        T3.SetActive(true);
    }

    void _T3MethodOFF()
    {
        T3.SetActive(false);
    }



    void _T4MethodON()
    {
        T4.SetActive(true);
    }

    void _T4MethodOFF()
    {
        T4.SetActive(false);
    }



    void _T5MethodON()
    {
        T5.SetActive(true);
    }

    void _T5MethodOFF()
    {
        T5.SetActive(false);
    }



    void _T6MethodON()
    {
        T6.SetActive(true);
    }

    void _T6MethodOFF()
    {
        T6.SetActive(false);
    }



    void _T7MethodON()
    {
        T7.SetActive(true);
    }

    void _T7MethodOFF()
    {
        T7.SetActive(false);
    }



    void _T8MethodON()
    {
        T8.SetActive(true);
    }

    void _T8MethodOFF()
    {
        T8.SetActive(false);
    }



    void _T9MethodON()
    {
        T9.SetActive(true);
    }

    void _T9MethodOFF()
    {
        T9.SetActive(false);
    }



    void _T10MethodON()
    {
        T10.SetActive(true);
    }

    void _T10MethodOFF()
    {
        T10.SetActive(false);
    }



    void _T11MethodON()
    {
        T11.SetActive(true);
    }

    void _T11MethodOFF()
    {
        T11.SetActive(false);
    }



    void _T12MethodON()
    {
        T12.SetActive(true);
    }

    void _T12MethodOFF()
    {
        T12.SetActive(false);
    }



    void _T13MethodON()
    {
        T13.SetActive(true);
    }

    void _T13MethodOFF()
    {
        T13.SetActive(false);
    }



    void _T14MethodON()
    {
        T14.SetActive(true);
    }

    void _T14MethodOFF()
    {
        T14.SetActive(false);
    }



    void _T15MethodON()
    {
        T15.SetActive(true);
    }

    void _T15MethodOFF()
    {
        T15.SetActive(false);
    }



    void _T16MethodON()
    {
        T16.SetActive(true);
    }

    void _T16MethodOFF()
    {
        T16.SetActive(false);
    }



    void _T17MethodON()
    {
        T17.SetActive(true);
    }

    void _T17MethodOFF()
    {
        T17.SetActive(false);
    }



    void _T18MethodON()
    {
        T18.SetActive(true);
    }

    void _T18MethodOFF()
    {
        T18.SetActive(false);
    }



    void _T19MethodON()
    {
        T19.SetActive(true);
    }

    void _T19MethodOFF()
    {
        T19.SetActive(false);
    }

    void _T20MethodON()
    {
        T20.SetActive(true);
    }

    void _T20MethodOFF()
    {
        T20.SetActive(false);
    }

    void _T21MethodON()
    {
        T21.SetActive(true);
    }

    void _T21MethodOFF()
    {
        T21.SetActive(false);
    }

    void _T22MethodON()
    {
        T22.SetActive(true);
    }

    void _T22MethodOFF()
    {
        T22.SetActive(false);
    }

    void _T23MethodON()
    {
        T23.SetActive(true);
    }

    void _T23MethodOFF()
    {
        T23.SetActive(false);
    }

    void _T24MethodON()
    {
        T24.SetActive(true);
    }

    void _T24MethodOFF()
    {
        T24.SetActive(false);
    }

































































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















































    // object








    void _OB1MethodON()
    {
        OB1.SetActive(true);
    }

    void _OB1ethodOFF()
    {
        OB1.SetActive(false);
    }



    void _OB2MethodON()
    {
        OB2.SetActive(true);
    }

    void _OB2ethodOFF()
    {
        OB2.SetActive(false);
    }





    void _OB3MethodON()
    {
        OB3.SetActive(true);
    }

    void _OB3ethodOFF()
    {
        OB3.SetActive(false);
    }






    void _OB4MethodON()
    {
        OB4.SetActive(true);
    }

    void _OB4ethodOFF()
    {
        OB4.SetActive(false);
    }





    void _OB5MethodON()
    {
        OB5.SetActive(true);
    }

    void _OB5ethodOFF()
    {
        OB5.SetActive(false);
    }





    void _OB6MethodON()
    {
        OB6.SetActive(true);
    }

    void _OB6ethodOFF()
    {
        OB6.SetActive(false);
    }



    void _OB7MethodON()
    {
        OB7.SetActive(true);
    }

    void _OB7ethodOFF()
    {
        OB7.SetActive(false);
    }



    void _OB8MethodON()
    {
        OB8.SetActive(true);
    }

    void _OB8ethodOFF()
    {
        OB8.SetActive(false);
    }



    void _OB9MethodON()
    {
        OB9.SetActive(true);
    }

    void _OB9ethodOFF()
    {
        OB9.SetActive(false);
    }




























}
