using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_light_class8 : MonoBehaviour
{

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
            animator.Play("Camara animation", 0, targetNormalizedTime);
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
            case 2: Level8(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
    }




   

    public void level()
    {
        miniGames[miniGameIndex].Output();
    }
    public GameObject lv1;

    public TargetController miniGame5;
    public TargetController miniGame8;
    public void Level5()
    {
        miniGame5.Output();
        SaveProgress(1, 0, 4);
    }
    public void Savepoint1()
    {
        SaveProgress(1, 0, 4);

    }
    public void Savepoint2()
    {
        SaveProgress(2, 0, 7);
    }
    public void Level8()
    {
        miniGame8.Output();
        SaveProgress(2, 0, 7);
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
        InventoryManager.Instance.inventryStatic.SetActive(true);

        InitializeFromCheckpoint();
        level();

        
        canChoose = true;
        InitializeFromCheckpoint();
    }

    public Camera cam;
    public LayerMask layerMask;
    public List<TargetController> miniGames = new List<TargetController>();
    public List<GameObject> questions = new List<GameObject>();
    public List<string> correctObjs = new List<string>();
    private int correctObjIndex = 0;
    private int miniGameIndex = 0;
    private bool canChoose = true;
    private int camHolderIndex;
    private List<Transform> camHolders;

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
                        correctObjIndex++;
                        miniGames[miniGameIndex].defaultEvent.Invoke();
                    }
                    else if(raycastHit.collider.gameObject.name == "B")
                    {
                        ChangeCamHolderLV4(camHolders[camHolderIndex]);
                    }
                    else if (raycastHit.collider.gameObject.name == "C")
                    {
                        ChangeCamHolderLV5(camHolders[camHolderIndex]);
                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }
    }

    public Transform mainLV4CH;
    public Transform mainLV5CH;

    public void ChangeCamHolderLV4(Transform newCamHolder)
    {
        transform.position = newCamHolder.position;
        transform.rotation = newCamHolder.rotation;
        StartCoroutine(BackToMainCH(mainLV4CH));
    }

    public void ChangeCamHolderLV5(Transform newCamHolder)
    {
        transform.position = newCamHolder.position;
        transform.rotation = newCamHolder.rotation;
        StartCoroutine(BackToMainCH(mainLV5CH));
    }

    IEnumerator BackToMainCH(Transform camHolder)
    {
        camHolderIndex++;
        yield return new WaitForSeconds(5);
        transform.position = camHolder.position;
        transform.rotation = camHolder.rotation;
    }

    private int ind = 0;
    public void Lev3Correct()
    {
        ind++;
        if (ind == 2)
        {
            ind = 0;
            MoveToNextMiniGame();
            TurnOffRaycast();
            StepComplete();
            return;
        }
        StepComplete();
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
        yield return new WaitForSeconds(2);
        obj.SetActive(true);
    }

    public void TurnOffRaycast()
    {
        canChoose = false;
    }

    public void MoveToNextMiniGame()
    {
        foreach (GameObject obj in questions)
        {
            obj.SetActive(false);
        }

        canChoose = true;
        miniGames[miniGameIndex].EndMiniGame();
        miniGameIndex++;
        miniGames[miniGameIndex].Output();
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

    public void Level8Game()
    {
        index++;

        if (index == 6)
        {
            index = 0;
            StepComplete();
            Invoke("level8b", 1);
        }
    }

    public TargetController minigame8b;

    public void level8b()
    {
        minigame8b.Output();
    }

    public void PlayAnimBool(Animator anim)
    {
        anim.SetBool("Bool", true);
    }

    public void EndAnimBool(Animator anim)
    {
        anim.SetBool("Bool", false);
    }

    public void AnimBack(Animator anim)
    {
        foreach (GameObject obj in questions)
        {
            obj.SetActive(false);
        }

        anim.SetBool("Bool", false);
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

    public void CallTargetController(TargetController targetController)
    {
        StartCoroutine(DelayCallTargetController(targetController));
    }

    IEnumerator DelayCallTargetController(TargetController targetController)
    {
        yield return new WaitForSeconds(5);
        targetController.Output();
    }

    public void ChangePlayerPosDelay(Transform playerPos)
    {
        StartCoroutine(ChangePlayerPos(playerPos));
    }

    IEnumerator ChangePlayerPos(Transform newPos)
    {
        yield return new WaitForSeconds(2);
        InventoryManager.Instance.player.ChangePosition(newPos);
    }

    public int index = 0;

    public void MirrorLV3()
    {
        index++;

        if(index == 2)
        {
            index = 0;
            StepComplete();
        }
    }



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
    public GameObject T19x;
    public GameObject T20;
    public GameObject T21;










    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;
    public GameObject D6;


    [Header("Explanation anims")]
    private Animator anim;

    public GameObject kalidoscop_anim;
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

    void T19x_MethodON()
    {
        T19x.SetActive(true);
    }
    void T19x_MethodOFF()
    {
        T19x.SetActive(false);
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










    //Animation Play





    void _kalidoscop_animMethod()
    {
        anim = kalidoscop_anim.GetComponent<Animator>();
        anim.Play("kalidoscop anim");
    }









}
