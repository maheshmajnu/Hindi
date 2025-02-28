using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx_7_Forests : MonoBehaviour
{
    public Transform wayPoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypoint.player = InventoryManager.Instance.player.transform;
        waypointCanvas.SetActive(true);
        waypoint.target = target;
    }

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

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
        SetWayPoint(wayPoint1);
    }




    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject Floodwater;
    public GameObject sunlight;
    public GameObject O2;
    public GameObject rain;












    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject bike_anim;
    public GameObject sunlight_anim;
    public GameObject O2_anim;

































    //Titles


    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject T6;
    public GameObject T7;
    public GameObject T8;
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
    public GameObject D22;
    public GameObject D23;















    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip a1;
    public AudioClip a2;
    public AudioClip a3;
    public AudioClip a4;
    public AudioClip a5;
    public AudioClip a6;
    public AudioClip a7;
    public AudioClip a8;
    public AudioClip a9;
    public AudioClip a10;
    public AudioClip a11;
    public AudioClip a12;
    public AudioClip a13;
    public AudioClip a14;
    public AudioClip a15;
    public AudioClip a16;
    public AudioClip a17;
    public AudioClip a18;
    public AudioClip a19;
    public AudioClip a20;
    public AudioClip a21;
    public AudioClip a22;
    public AudioClip a23;
    public AudioClip a24;
    public AudioClip a25;
    public AudioClip a26;
    public AudioClip a27;
    public AudioClip a28;
    public AudioClip a29;
    public AudioClip a30;
    public AudioClip a31;
    public AudioClip a32;
    public AudioClip a33;
    public AudioClip a34;
    public AudioClip a35;
    public AudioClip a36;
    public AudioClip a37;
    public AudioClip a38;
    public AudioClip a39;
    public AudioClip a40;
    public AudioClip a41;
    public AudioClip a42;
    public AudioClip a43;
    public AudioClip a44;
    public AudioClip a45;
    public AudioClip a46;
    public AudioClip a47;
    public AudioClip a48;
    public AudioClip a49;
    public AudioClip a50;
    public AudioClip a51;
    public AudioClip a52;
    public AudioClip a53;
    public AudioClip a54;
    public AudioClip a55;
    public AudioClip a56;
    public AudioClip a57;
    public AudioClip a58;
    public AudioClip a59;
    public AudioClip a60;
    public AudioClip a61;
    public AudioClip a62;
    public AudioClip a63;
    public AudioClip a64;
    public AudioClip a65;
    public AudioClip a66;
    public AudioClip a67;
    public AudioClip a68;
    public AudioClip a69;
    public AudioClip a70;
    public AudioClip a71;
    public AudioClip a72;
    public AudioClip a73;

























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

























































    //Objects





    void _Rain_MethodON()
    {
        rain.SetActive(true);
    }
    void _Rain_MethodoOFF()
    {
        rain.SetActive(false);
    }
    //

    void _Floodwater_MethodON()
    {
        Floodwater.SetActive(true);
    }
    void _Floodwater_MethodoOFF()
    {
        Floodwater.SetActive(false);
    }
    //
    void _sunlight_MethodON()
    {
        sunlight.SetActive(true);
    }
    void _sunlight_MethodoOFF()
    {
        sunlight.SetActive(false);
    }
    //
    void _O2_MethodON()
    {
        O2.SetActive(true);
    }
    void _O2_MethodoOFF()
    {
        O2.SetActive(false);
    }
    //


























    //Animations





    void _Bike_animationAnimmethod()
    {

        anim = bike_anim.GetComponent<Animator>();
        anim.Play("bike anim");
    }
    //
    void _Sunlight_animationAnimmethod()
    {

        anim = sunlight_anim.GetComponent<Animator>();
        anim.Play("Sun light animation");
    }
    //
    void _O2_animationAnimmethod()
    {

        anim = O2_anim.GetComponent<Animator>();
        anim.Play("O2 animation");
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
    void _D22_MethodON()
    {
        D22.SetActive(true);
    }
    void _D22_MethodoOFF()
    {
        D22.SetActive(false);
    }
    //
    void _D23_MethodON()
    {
        D23.SetActive(true);
    }
    void _D23_MethodoOFF()
    {
        D23.SetActive(false);
    }










    // Audio









    void _P2A1_audioMethod()

    {
        myAudio.clip = a1;
        myAudio.Play();
    }
    void _P2A2_audioMethod()

    {
        myAudio.clip = a2;
        myAudio.Play();
    }
    void _P2A3_audioMethod()

    {
        myAudio.clip = a3;
        myAudio.Play();
    }
    void _P2A4_audioMethod()

    {
        myAudio.clip = a4;
        myAudio.Play();
    }
    void _P2A5_audioMethod()

    {
        myAudio.clip = a5;
        myAudio.Play();
    }
    void _P2A6_audioMethod()

    {
        myAudio.clip = a6;
        myAudio.Play();
    }
    void _P2A7_audioMethod()

    {
        myAudio.clip = a7;
        myAudio.Play();
    }
    void _P2A8_audioMethod()

    {
        myAudio.clip = a8;
        myAudio.Play();
    }
    void _P2A9_audioMethod()

    {
        myAudio.clip = a9;
        myAudio.Play();
    }
    void _P2A10_audioMethod()

    {
        myAudio.clip = a10;
        myAudio.Play();
    }
    void _P2A11_audioMethod()

    {
        myAudio.clip = a11;
        myAudio.Play();
    }
    void _P2A12_audioMethod()

    {
        myAudio.clip = a12;
        myAudio.Play();
    }
    void _P2A13_audioMethod()

    {
        myAudio.clip = a13;
        myAudio.Play();
    }
    void _P2A14_audioMethod()

    {
        myAudio.clip = a14;
        myAudio.Play();
    }
    void _P2A15_audioMethod()

    {
        myAudio.clip = a15;
        myAudio.Play();
    }
    void _P2A16_audioMethod()

    {
        myAudio.clip = a16;
        myAudio.Play();
    }
    void _P2A17_audioMethod()

    {
        myAudio.clip = a17;
        myAudio.Play();
    }
    void _P2A18_audioMethod()

    {
        myAudio.clip = a18;
        myAudio.Play();
    }
    void _P2A19_audioMethod()

    {
        myAudio.clip = a19;
        myAudio.Play();
    }
    void _P2A20_audioMethod()

    {
        myAudio.clip = a20;
        myAudio.Play();
    }
    void _P2A21_audioMethod()

    {
        myAudio.clip = a21;
        myAudio.Play();
    }
    void _P2A22_audioMethod()

    {
        myAudio.clip = a22;
        myAudio.Play();
    }
    void _P2A23_audioMethod()

    {
        myAudio.clip = a23;
        myAudio.Play();
    }
    void _P2A24_audioMethod()

    {
        myAudio.clip = a24;
        myAudio.Play();
    }
    void _P2A25_audioMethod()

    {
        myAudio.clip = a25;
        myAudio.Play();
    }
    void _P2A26_audioMethod()

    {
        myAudio.clip = a26;
        myAudio.Play();
    }
    void _P2A27_audioMethod()

    {
        myAudio.clip = a27;
        myAudio.Play();
    }
    void _P2A28_audioMethod()

    {
        myAudio.clip = a28;
        myAudio.Play();
    }
    void _P2A29_audioMethod()

    {
        myAudio.clip = a29;
        myAudio.Play();
    }
    void _P2A30_audioMethod()

    {
        myAudio.clip = a30;
        myAudio.Play();
    }
    void _P2A31_audioMethod()

    {
        myAudio.clip = a31;
        myAudio.Play();
    }
    void _P2A32_audioMethod()

    {
        myAudio.clip = a32;
        myAudio.Play();
    }
    void _P2A33_audioMethod()

    {
        myAudio.clip = a33;
        myAudio.Play();
    }
    void _P2A34_audioMethod()

    {
        myAudio.clip = a34;
        myAudio.Play();
    }
    void _P2A35_audioMethod()

    {
        myAudio.clip = a35;
        myAudio.Play();
    }
    void _P2A36_audioMethod()

    {
        myAudio.clip = a36;
        myAudio.Play();
    }
    void _P2A37_audioMethod()

    {
        myAudio.clip = a37;
        myAudio.Play();
    }
    void _P2A38_audioMethod()

    {
        myAudio.clip = a38;
        myAudio.Play();
    }
    void _P2A39_audioMethod()

    {
        myAudio.clip = a39;
        myAudio.Play();
    }
    void _P2A40_audioMethod()

    {
        myAudio.clip = a40;
        myAudio.Play();
    }
    void _P2A41_audioMethod()

    {
        myAudio.clip = a41;
        myAudio.Play();
    }
    void _P2A42_audioMethod()

    {
        myAudio.clip = a42;
        myAudio.Play();
    }
    void _P2A43_audioMethod()

    {
        myAudio.clip = a43;
        myAudio.Play();
    }
    void _P2A44_audioMethod()

    {
        myAudio.clip = a44;
        myAudio.Play();
    }
    void _P2A45_audioMethod()

    {
        myAudio.clip = a45;
        myAudio.Play();
    }
    void _P2A46_audioMethod()

    {
        myAudio.clip = a46;
        myAudio.Play();
    }
    void _P2A47_audioMethod()

    {
        myAudio.clip = a47;
        myAudio.Play();
    }
    void _P2A48_audioMethod()

    {
        myAudio.clip = a48;
        myAudio.Play();
    }
    void _P2A49_audioMethod()

    {
        myAudio.clip = a49;
        myAudio.Play();
    }
    void _P2A50_audioMethod()

    {
        myAudio.clip = a50;
        myAudio.Play();
    }
    void _P2A51_audioMethod()

    {
        myAudio.clip = a51;
        myAudio.Play();
    }
    void _P2A52_audioMethod()

    {
        myAudio.clip = a52;
        myAudio.Play();
    }
    void _P2A53_audioMethod()

    {
        myAudio.clip = a53;
        myAudio.Play();
    }
    void _P2A54_audioMethod()

    {
        myAudio.clip = a54;
        myAudio.Play();
    }
    void _P2A55_audioMethod()

    {
        myAudio.clip = a55;
        myAudio.Play();
    }
    void _P2A56_audioMethod()

    {
        myAudio.clip = a56;
        myAudio.Play();
    }
    void _P2A57_audioMethod()

    {
        myAudio.clip = a57;
        myAudio.Play();
    }
    void _P2A58_audioMethod()

    {
        myAudio.clip = a58;
        myAudio.Play();
    }
    void _P2A59_audioMethod()

    {
        myAudio.clip = a59;
        myAudio.Play();
    }
    void _P2A60_audioMethod()

    {
        myAudio.clip = a60;
        myAudio.Play();
    }
    void _P2A61_audioMethod()

    {
        myAudio.clip = a61;
        myAudio.Play();
    }
    void _P2A62_audioMethod()

    {
        myAudio.clip = a62;
        myAudio.Play();
    }
    void _P2A63_audioMethod()

    {
        myAudio.clip = a63;
        myAudio.Play();
    }
    void _P2A64_audioMethod()

    {
        myAudio.clip = a64;
        myAudio.Play();
    }
    void _P2A65_audioMethod()

    {
        myAudio.clip = a65;
        myAudio.Play();
    }
    void _P2A66_audioMethod()

    {
        myAudio.clip = a66;
        myAudio.Play();
    }
    void _P2A67_audioMethod()

    {
        myAudio.clip = a67;
        myAudio.Play();
    }
    void _P2A68_audioMethod()

    {
        myAudio.clip = a68;
        myAudio.Play();
    }
    void _P2A69_audioMethod()

    {
        myAudio.clip = a69;
        myAudio.Play();
    }
    void _P2A70_audioMethod()

    {
        myAudio.clip = a70;
        myAudio.Play();
    }
    void _P2A71_audioMethod()

    {
        myAudio.clip = a71;
        myAudio.Play();
    }
    void _P2A72_audioMethod()

    {
        myAudio.clip = a72;
        myAudio.Play();
    }
    void _P2A73_audioMethod()

    {
        myAudio.clip = a73;
        myAudio.Play();
    }

    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;

    public ObjectiveController objectiveController;
    public GameObject findplayer;
    public MenuSystem menu;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("Camera animation", 0, targetNormalizedTime);
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

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void Delaystepcom()
    {
        Invoke("StepComplete", 3);
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


    private bool miniGame1Started;
    public Camera cam;
    public TargetController miniGame1;
    public TargetController miniGame2;
    public TargetController miniGame3;
    public LayerMask layerMask;
    public List<string> forestParts;
    public int currentIndex;
    private void Update()
    {

        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }

        findplayer = GameObject.FindGameObjectWithTag("Player");
        if (findplayer != null)
        {
            Transform menuTransform = findplayer.transform.Find("MenuSystem");
            if (menuTransform != null)
            {
                menu = menuTransform.GetComponent<MenuSystem>();
                if (menu != null)
                {
                    menu.SetSfxScript(this);
                    Debug.Log("MenuSystem script found and SFX script assigned.");
                }
            }
        }
        if (miniGame1Started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
                {
                    if (raycastHit.collider != null)
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        if (raycastHit.collider.gameObject.name == forestParts[currentIndex])
                        {
                            currentIndex++;
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                            if (currentIndex == 3)
                            {
                                currentIndex = 0;
                                miniGame1Started = false;
                                miniGame1.EndMiniGame();
                            }
                        }
                        else
                        {
                            Debug.Log("Mission Failed");
                        }
                    }
                }
            }
        }
    }

    public void MiniGameStart()
    {
        InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().FadeOut();
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        miniGame1Started = true;
    }

    public void MiniGameEnd()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(true);
    }

    public GameObject forestBox;
    public void ForestProductsMiniGame()
    {
        currentIndex++;
        forestBox.tag = "Finish";
        if(currentIndex == 4)
        {
            currentIndex = 0;
            miniGame2.EndMiniGame();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void Level3()
    {
        StartCoroutine(DelayLv3MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(1, 0, 2);

    }
    IEnumerator DelayLv3MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        miniGame3.Output();
    }


    public void FoodChaninMiniGame()
    {
        currentIndex++;
        if (currentIndex == 5)
        {
            currentIndex = 0;
            miniGame3.EndMiniGame();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public GameObject trees;
    public void PlantTrees()
    {
        trees.SetActive(true);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }
































































































































}
