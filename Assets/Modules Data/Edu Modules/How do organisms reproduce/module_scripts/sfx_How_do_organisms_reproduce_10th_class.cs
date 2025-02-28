using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class sfx_How_do_organisms_reproduce_10th_class : MonoBehaviour
{

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public GameObject JustInstantiatedNoPlayerCanvas;

    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;


    public ObjectiveController objectiveController;
    public NoPlayerMenu noPlayerMenu;

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
            case 0: Level1(); break;
            case 1: Level1(); break;
            case 2: Level2(); break;
            case 3: Level3(); break;
            case 4: Level4(); break;
            case 5: Level5(); break;
            case 6: Level6(); break;
            case 7: Level7(); break;
            case 8: Level8(); break;
            case 9: Level9(); break;
            case 10: Level11(); break;
            case 11: Level12(); break;
            default: Level1(); break;
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


        InitializeFromCheckpoint();
        Level1();
        
        

    }

    public GameObject lv1;

    public void Level1()
    {
        
        miniGames.Output();
        
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

    public void Level2()
    {
        StartCoroutine(DelayLv2MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(2, 0, 1);
        
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
        SaveProgress(3, 0, 2);
        
    }
    IEnumerator DelayLv3MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv3MiniGame.Output();
    }

    public TargetController lv3MiniGame;

    public void Level4()
    {
        StartCoroutine(DelayLv4AMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        
        SaveProgress(4, 0, 3);
        
    }
    IEnumerator DelayLv4AMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv4AMiniGame.Output();
    }
    

    public TargetController lv4AMiniGame;
    public void Lv4ATurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 9)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(DelayLv4BMiniGameStart());
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    IEnumerator DelayLv4BMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv4BMiniGame.Output();
    }

    public TargetController lv4BMiniGame;

    public void Lv4BCorrectAnswer()
    {
        
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(DelayLv4CMiniGameStart());
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        
    }

    IEnumerator DelayLv4CMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv4CMiniGame.Output();
    }

    public TargetController lv4CMiniGame;

    public void Lv4CCorrectAnswer()
    {

        index++;
        

        if (index == 3)
        {
            index = 0;
            //InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }

    }

    public void Level5()
    {
        StartCoroutine(DelayLv5AMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(5, 0, 4);
        
    }
    IEnumerator DelayLv5AMiniGameStart()
    {
        yield return new WaitForSeconds(1f);
        lv5AMiniGame.Output();
    }


    public TargetController lv5AMiniGame;

    public void Lv5ACorrectAnswer()
    {
        index++;
        

        if (index == 2)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(DelayLv5BMiniGameStart());
            
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    IEnumerator DelayLv5BMiniGameStart()
    {
        yield return new WaitForSeconds(1f);
        lv5BMiniGame.Output();
    }

    public TargetController lv5BMiniGame;

    public void Lv5BCorrectAnswer(Animator anim)
    {
        PlayAnimTrigg(anim);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        StartCoroutine(DelayLv5B1MiniGameStart());
        //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();

    }

    IEnumerator DelayLv5B1MiniGameStart()
    {
        yield return new WaitForSeconds(4f);
        lv5B1MiniGame.Output();
    }

    public TargetController lv5B1MiniGame;

    public void Lv5B1CorrectAnswer(Animator anim)
    {
        PlayAnimTrigg(anim);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        Level6();
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();

    }

    public void PlayAnimTrigg(Animator anim)
    {
        anim.SetTrigger("Trigger"); // Replace "TriggerName" with the actual name of your trigger
    }



    public void Level6()
    {
        StartCoroutine(DelayLv6AMiniGameStart());
        SaveProgress(6, 0, 5);
        
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv6AMiniGameStart()
    {
        yield return new WaitForSeconds(4);
        lv6AMiniGame.Output();
    }


    public TargetController lv6AMiniGame;
    public void Lv6ATurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(DelayLv6BMiniGameStart());
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    IEnumerator DelayLv6BMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv6BMiniGame.Output();
    }


    public TargetController lv6BMiniGame;
    public void Lv6BTurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 1)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public void Level7()
    {
        StartCoroutine(DelayLv7BMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(7, 0, 6);
        
    }
    IEnumerator DelayLv7BMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv7BMiniGame.Output();
    }


    public TargetController lv7BMiniGame;
    public TargetController lv7B1MiniGame;
    //public TargetController lv7BCHMiniGame;
    //public TargetController lv7B1CHMiniGame;
    public void Lv7Bcorrect()
    {
        index++;
        

        if (index == 4)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(DelayLv7B1MiniGame());
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    IEnumerator DelayLv7B1MiniGame()
    {
        yield return new WaitForSeconds(1);
        lv7B1MiniGame.Output();
    }

    //IEnumerator DelayLv7BCHMiniGame()
    //{
    //    yield return new WaitForSeconds(1);
    //    lv7BCHMiniGame.Output();
    //}

    //public void SecondGame7B()
    //{
    //    lv7B1MiniGame.Output();
    //}


    public void Lv7B1correct()
    {
        index++;


        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            
            Level8();
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

   

    public void Level8()
    {
        StartCoroutine(DelayLv8AMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(8, 0, 7);
        
    }
    IEnumerator DelayLv8AMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv8AMiniGame.Output();
    }


    public TargetController lv8AMiniGame;
    public void Lv8ATurnOnMeshRend(MeshRenderer mesh)
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

    public void Lv8CorrectAnswer()
    {

        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();

    }

    public void Level9()
    {
        StartCoroutine(DelayLv9AMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv9AMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv9AMiniGame.Output();
    }


    public TargetController lv9AMiniGame;
    public void Lv9ATurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 5)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public void Lv9CorrectAnswer()
    {

        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();

        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();

    }

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
    
    public void Lv11correct()
    {
        index++;


        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Level12();
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

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




    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip voice1;
    public AudioClip voice2;
    public AudioClip voice3;
    public AudioClip voice4;
    public AudioClip voice5;
    public AudioClip voice6;
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
    public GameObject T27;
    public GameObject T28;
    public GameObject T29;






    public GameObject T30;
    public GameObject T31;
    public GameObject T32;
    public GameObject T33;
    public GameObject T34;
    public GameObject T35;
    public GameObject T36;
    public GameObject T37;
    public GameObject T38;
    public GameObject T39;
    public GameObject T40;
    public GameObject T41;





































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




































    [Header("Explanation anims")]
    private Animator anim;
    public GameObject algeanim;
    public GameObject anthemanim;
    public GameObject antheranim2;
    public GameObject antheranim3;
    public GameObject flower_polination_anim;
    public GameObject orange_anim;
    public GameObject seeding_anim;
    public GameObject penis_lifting;
    public GameObject penis_anim;
    public GameObject sperm_anim;



    public GameObject beby_anim;







    // Object




    public GameObject OB1;
    public GameObject OB2;
    public GameObject OB3;
    public GameObject OB4;
    public GameObject OB5;





















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
    //
    void _voice6_audioMethod()
    {
        myAudio.clip = voice6;
        myAudio.Play();
    }
    //
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


    void T27_MethodOFF()
    {
        T27.SetActive(false);
    }
    //
    void T27_MethodON()
    {
        T27.SetActive(true);
    }
    //


    void T28_MethodOFF()
    {
        T28.SetActive(false);
    }
    //
    void T28_MethodON()
    {
        T28.SetActive(true);
    }
    //




    void T29_MethodOFF()
    {
        T29.SetActive(false);
    }
    //
    void T29_MethodON()
    {
        T29.SetActive(true);
    }
    //

































    void T30_MethodOFF()
    {
        T30.SetActive(false);
    }
    //
    void T30_MethodON()
    {
        T30.SetActive(true);
    }
    //






    void T31_MethodOFF()
    {
        T31.SetActive(false);
    }
    //
    void T3_1MethodON()
    {
        T31.SetActive(true);
    }
    //



    void T32_MethodOFF()
    {
        T32.SetActive(false);
    }
    //
    void T32_MethodON()
    {
        T32.SetActive(true);
    }
    //



    void T33_MethodOFF()
    {
        T33.SetActive(false);
    }
    //
    void T33_MethodON()
    {
        T33.SetActive(true);
    }
    //



    void T34_MethodOFF()
    {
        T34.SetActive(false);
    }
    //
    void T34_MethodON()
    {
        T34.SetActive(true);
    }
    //



    void T35_MethodOFF()
    {
        T35.SetActive(false);
    }
    //
    void T35_MethodON()
    {
        T35.SetActive(true);
    }
    //



    void T36_MethodOFF()
    {
        T36.SetActive(false);
    }
    //
    void T36_MethodON()
    {
        T36.SetActive(true);
    }
    //



    void T37_MethodOFF()
    {
        T37.SetActive(false);
    }
    //
    void T37_MethodON()
    {
        T37.SetActive(true);
    }
    //



    void T38_MethodOFF()
    {
        T38.SetActive(false);
    }
    //
    void T38_MethodON()
    {
        T38.SetActive(true);
    }
    //



    void T39_MethodOFF()
    {
        T39.SetActive(false);
    }
    //
    void T39_MethodON()
    {
        T39.SetActive(true);
    }
    //



    void T40_MethodOFF()
    {
        T40.SetActive(false);
    }
    //
    void T40_MethodON()
    {
        T40.SetActive(true);
    }
    //







    void T41_MethodOFF()
    {
        T41.SetActive(false);
    }
    //
    void T41_MethodON()
    {
        T41.SetActive(true);
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









































    //Animation Play





    void _algeanim_plankMethod()
    {
        anim = algeanim .GetComponent<Animator>();
        anim.Play("alge anim");
    }





    void _anthemanim_plankMethod()
    {
        anim = anthemanim.GetComponent<Animator>();
        anim.Play("antham anim");
    }





    void _antheranim2_plankMethod()
    {
        anim = antheranim2.GetComponent<Animator>();
        anim.Play("anther anim2");
    }






    void _antheranim3_plankMethod()
    {
        anim = antheranim3.GetComponent<Animator>();
        anim.Play("anther anim3");
    }







    void _flower_polination_anim_plankMethod()
    {
        anim = flower_polination_anim.GetComponent<Animator>();
        anim.Play("flower polination");
    }





    void _orange_anim_plankMethod()
    {
        anim = orange_anim.GetComponent<Animator>();
        anim.Play("orange anim");
    }







    void _seeding_anim_plankMethod()
    {
        anim = seeding_anim.GetComponent<Animator>();
        anim.Play("seeding anim");
    }




    void _penis_lifting_plankMethod()
    {
        anim = penis_lifting.GetComponent<Animator>();
        anim.Play("pines lifting anim");
    }








    void _penis_anim_plankMethod()
    {
        anim = penis_anim.GetComponent<Animator>();
        anim.Play("penis anim");
    }





    void _sperm_anim_plankMethod()
    {
        anim = sperm_anim.GetComponent<Animator>();
        anim.Play("sperm anim");
    }






    void _beby_anim_plankMethod()
    {
        anim = beby_anim.GetComponent<Animator>();
        anim.Play("fetus anim");
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

















}
