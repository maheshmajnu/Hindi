using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_mm : MonoBehaviour
{

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

    public Transform cameraTransform;
    public List<GameObject> level1Questions;
    public List<Transform> level1CamHolders;
    private int index;

    private GameObject JustInstantiatedNoPlayerCanvas;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("mm camera animation", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }

        animator = GetComponent<Animator>();
        //==================== INSERT NO-PLAYER Menu  ====================//
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/NotDefaultPlayer_Menu", typeof(GameObject));  // Load No-Player Menu
        JustInstantiatedNoPlayerCanvas = (GameObject)GameObject.Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate No-Player Menu   {  Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);   }
        JustInstantiatedNoPlayerCanvas.SetActive(false);
        //================================================================//
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
        JustInstantiatedNoPlayerCanvas.SetActive(true);
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(DelayQuestions());
    }

    IEnumerator DelayQuestions()
    {
        cameraTransform.position = level1CamHolders[index].position;
        cameraTransform.rotation = level1CamHolders[index].rotation;
        yield return new WaitForSeconds(2);
        level1Questions[index].SetActive(true);
    }

    public void CorrectAnswerL1()
    {
        index++;

        foreach (GameObject obj in level1Questions)
        {
            obj.SetActive(false);
        }


        if (index == 7)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
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
            return;
        }

        StartCoroutine(DelayQuestions());
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    public void Level2Dailer()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    bool measureMentMiniGameStarted = false;
    bool finalMiniGameStarted = false;
    public void MeasurementMiniGameStart()
    {
        measureMentMiniGameStarted = true;
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void FinalMiniGameStart()
    {
        finalMiniGameStarted = false;
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void MiniGameStart()
    {
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

    public Camera cam;
    public LayerMask layermask;
    bool btnUsed = false;
    private void Update()
    {
        if (measureMentMiniGameStarted && !btnUsed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layermask))
                {
                    if (raycastHit2.collider != null)
                    {
                        ShowOptionsLv3();
                        btnUsed = true;
                    }
                }
            }
        }

        if (finalMiniGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layermask))
                {
                    if (raycastHit2.collider != null && raycastHit2.collider.gameObject.name == "Correct")
                    {
                        level4MiniGame.EndMiniGame();
                        finalMiniGameStarted = false;
                        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }
    }

    public List<GameObject> level3Options = new List<GameObject>();
    public TargetController level3MiniGame;
    public TargetController level4MiniGame;
    private void ShowOptionsLv3()
    {
        level3Options[index].SetActive(true);
    }

    public void CorrectAnswerL3()
    {
        index++;
        btnUsed = false;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();

        foreach (GameObject obj in level3Options)
        {
            obj.SetActive(false);
        }

        if (index == 3)
        {
            level3MiniGame.EndMiniGame();
        }
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject Car;
    public GameObject boat;
    public GameObject bike;
    public GameObject bullcart;
    public GameObject Honey_Bee;
    public GameObject stone_wheel;
    public GameObject blackboard;
    public GameObject rope_M;
    public GameObject rope;
    public GameObject KidPose;
    public GameObject rope_s;
    public GameObject TITLE_1;
    public GameObject TITLE_2;
    public GameObject TITLE_3;
    public GameObject TITLE_4;
    public GameObject TITLE_5;
    public GameObject TITLE_6;
    public GameObject TITLE_7;
    public GameObject TITLE_8;
    public GameObject TITLE_9;
    public GameObject TITLE_10;
    public GameObject TextTMP;
    public GameObject TextTMP2;
    public GameObject TextTMP3;
    public GameObject TextTMP4;
    public GameObject TextTMP5;
    public GameObject Textfeet;
    public GameObject Arrows;
    public GameObject Arrowssmall;
    public GameObject Arrowssmall1;
    public GameObject Arrowssmall2;
    public GameObject re;
    public GameObject un;
    public GameObject t_tape1;



    // Exp - Animations
    private Animator anim;
    [Header("Explanation anims")]
    public GameObject bus;
    public GameObject school_bus;
    public GameObject Train;
    public GameObject Aeroplane;
    public GameObject Car_1;
    public GameObject Cube_line;
    public GameObject rope_ani;
    public GameObject D_Holder;
    public GameObject fan_r;
    public GameObject honey_bee;
    public GameObject KidWalking;
    public GameObject kidw;
    public GameObject pendulum_s;
    public GameObject AeroplaneR;
    public GameObject Bolt;
    public GameObject Car_interior;









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

    public AudioClip audio_2;
    public AudioClip audio_3;
    public AudioClip audio_4;
    public AudioClip audio_5;
    public AudioClip audio_6;
    public AudioClip audio_7;
    public AudioClip audio_8;
    public AudioClip audio_9;
    public AudioClip audio_10;
    public AudioClip audio_11;
    public AudioClip audio_12;
    public AudioClip audio_13;
    public AudioClip audio_14;
    public AudioClip audio_15;
    public AudioClip audio_16;
    public AudioClip audio_17;
    public AudioClip audio_18;
    public AudioClip audio_19;
    public AudioClip audio_20;
    public AudioClip audio_21;
    public AudioClip audio_22;
    public AudioClip audio_23;
    public AudioClip audio_24;
    public AudioClip audio_25;
    public AudioClip audio_26;
    public AudioClip audio_27;
    public AudioClip audio_28;
    public AudioClip audio_29;
    public AudioClip audio_30;
    public AudioClip audio_31;


    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }









    void _t_tape1MethodON()
    {
        t_tape1.SetActive(true);
    }
    void _t_tape1MethodOFF()
    {
        t_tape1.SetActive(false);
    }









    void _reMethodON()
    {
        re.SetActive(true);
    }
    void _reMethodOFF()
    {
        re.SetActive(false);
    }
    void _unMethodON()
    {
        un.SetActive(true);
    }
    void _unMethodOFF()
    {
        un.SetActive(false);
    }
    void _ArrowssmallMethodON()
    {
        Arrowssmall.SetActive(true);
    }
    void _ArrowssmallMethodOFF()
    {
        Arrowssmall.SetActive(false);
    }
    void _Arrowssmall1MethodON()
    {
        Arrowssmall1.SetActive(true);
    }
    void _Arrowssmall1MethodOFF()
    {
        Arrowssmall1.SetActive(false);
    }
    void _Arrowssmall2MethodON()
    {
        Arrowssmall2.SetActive(true);
    }
    void _Arrowssmall2MethodOFF()
    {
        Arrowssmall2.SetActive(false);
    }
    void _ArrowsMethodON()
    {
        Arrows.SetActive(true);
    }
    void _ArrowsMethodOFF()
    {
        Arrows.SetActive(false);
    }
    void _Car_interiorMethodON()
    {
        Car_interior.SetActive(true);
    }
    void _Car_interiorMethodOFF()
    {
        Car_interior.SetActive(false);
    }
    void _TextfeetMethodON()
    {
        Textfeet.SetActive(true);
    }
    void _TextfeetMethodOFF()
    {
        Textfeet.SetActive(false);
    }
    void _TextTMPMethodON()
    {
        TextTMP.SetActive(true);
    }
    void _TextTMPMethodOFF()
    {
        TextTMP.SetActive(false);
    }
    void _TextTMP2MethodON()
    {
        TextTMP2.SetActive(true);
    }
    void _TextTMP2MethodOFF()
    {
        TextTMP2.SetActive(false);
    }
    void _TextTMP3MethodON()
    {
        TextTMP3.SetActive(true);
    }
    void _TextTMP3MethodOFF()
    {
        TextTMP3.SetActive(false);
    }
    void _TextTMP4MethodON()
    {
        TextTMP4.SetActive(true);
    }
    void _TextTMP4MethodOFF()
    {
        TextTMP4.SetActive(false);
    }
    void _TextTMP5MethodON()
    {
        TextTMP5.SetActive(true);
    }
    void _TextTMP5MethodOFF()
    {
        TextTMP5.SetActive(false);
    }












    void _TITLE_1MethodON()
    {
        TITLE_1.SetActive(true);
    }

    void _TITLE_1MethodOFF()
    {
        TITLE_1.SetActive(false);
    }



    void _TITLE_2MethodON()
    {
        TITLE_2.SetActive(true);
    }
    void _TITLE_2MethodOFF()
    {
        TITLE_2.SetActive(false);
    }



    void _TITLE_3MethodON()
    {
        TITLE_3.SetActive(true);
    }


    void _TITLE_3MethodOFF()
    {
        TITLE_3.SetActive(false);
    }




    void _TITLE_4MethodON()
    {
        TITLE_4.SetActive(true);
    }


    void _TITLE_4MethodOFF()
    {
        TITLE_4.SetActive(false);
    }




    void _TITLE_5MethodON()
    {
        TITLE_5.SetActive(true);
    }


    void _TITLE_5MethodOFF()
    {
        TITLE_5.SetActive(false);
    }









    void _TITLE_6MethodON()
    {
        TITLE_6.SetActive(true);
    }

    void _TITLE_6MethodOFF()
    {
        TITLE_6.SetActive(false);
    }







    void _TITLE_7MethodON()
    {
        TITLE_7.SetActive(true);
    }

    void _TITLE_7MethodOFF()
    {
        TITLE_7.SetActive(false);
    }






    void _TITLE_8MethodON()
    {
        TITLE_8.SetActive(true);
    }

    void _TITLE_8MethodOFF()
    {
        TITLE_8.SetActive(false);
    }






    void _TITLE_9MethodON()
    {
        TITLE_9.SetActive(true);
    }


    void _TITLE_9MethodOFF()
    {
        TITLE_9.SetActive(false);
    }






    void _TITLE_10MethodON()
    {
        TITLE_10.SetActive(true);
    }

    void _TITLE_10MethodOFF()
    {
        TITLE_10.SetActive(false);
    }






    void _AeroplaneRMethodON()
    {
        AeroplaneR.SetActive(true);
    }
    void _AeroplaneRMethodOFF()
    {
        AeroplaneR.SetActive(false);
    }
    void _rope_sMethodON()
    {
        rope_s.SetActive(true);
    }
    void _rope_sMethodOFF()
    {
        rope_s.SetActive(false);
    }
    void _KidPoseMethodON()
    {
        KidPose.SetActive(true);
    }
    void _KidPoseMethodOFF()
    {
        KidPose.SetActive(false);
    }
    void _kidwMethodON()
    {
        kidw.SetActive(true);
    }
    void _kidwMethodOFF()
    {
        kidw.SetActive(false);
    }
    void _rope_aniMethodON()
    {
        rope_ani.SetActive(true);
    }
    void _rope_aniMethodOFF()
    {
        rope_ani.SetActive(false);
    }
    void _rope_MMethodON()
    {
        rope_M.SetActive(true);
    }
    void _rope_MMethodOFF()
    {
        rope_M.SetActive(false);
    }
    void _ropeMethodON()
    {
        rope.SetActive(true);
    }
    void _ropeMethodOFF()
    {
        rope.SetActive(false);
    }
    void _blackboardMethodON()
    {
        blackboard.SetActive(true);
    }
    void _blackboardMethodOFF()
    {
        blackboard.SetActive(false);
    }
    void _CarMethodON()
    {
        Car.SetActive(true);
    }
    void _CarMethodOFF()
    {
        Car.SetActive(false);
    }
    void _Car_1MethodON()
    {
        Car_1.SetActive(true);
    }
    void _Car_1MethodOFF()
    {
        Car_1.SetActive(false);
    }
    void _TrainMethodON()
    {
        Train.SetActive(true);
    }
    void _TrainMethodOFF()
    {
        Train.SetActive(false);
    }
    void _boatMethodON()
    {
        boat.SetActive(true);
    }
    void _boatMethodOFF()
    {
        boat.SetActive(false);
    }
    void _AeroplaneMethodON()
    {
        Aeroplane.SetActive(true);
    }
    void _AeroplaneMethodOFF()
    {
        Aeroplane.SetActive(false);
    }
    void _bikeMethodON()
    {
        bike.SetActive(true);
    }
    void _bikeMethodOFF()
    {
        bike.SetActive(false);
    }
    void _bullcartMethodON()
    {
        bullcart.SetActive(true);
    }
    void _bullcartMethodOFF()
    {
        bullcart.SetActive(false);
    }
    void _busMethodON()
    {
        bus.SetActive(true);
    }
    void _busMethodOFF()
    {
        bus.SetActive(false);
    }
    void _Honey_BeeMethodON()
    {
        Honey_Bee.SetActive(true);
    }
    void _Honey_BeeMethodOFF()
    {
        Honey_Bee.SetActive(false);
    }
    void _school_busMethodON()
    {
        school_bus.SetActive(true);
    }
    void _school_busMethodOFF()
    {
        school_bus.SetActive(false);
    }
    void _stone_wheelMethodON()
    {
        stone_wheel.SetActive(true);
    }
    void _stone_wheelMethodOFF()
    {
        stone_wheel.SetActive(false);
    }
    void _Cube_lineMethodON()
    {
        Cube_line.SetActive(true);
    }
    void _Cube_lineMethodOFF()
    {
        Cube_line.SetActive(false);
    }
    void _Car_interiorAnimmethod()
    {

        anim = Car_interior.GetComponent<Animator>();
        anim.Play("carmovinganimation");
    }
    void _BoltAnimmethod()
    {

        anim = Bolt.GetComponent<Animator>();
        anim.Play("speedometer animation");
    }
    void _AeroplaneRAnimmethod()
    {

        anim = AeroplaneR.GetComponent<Animator>();
        anim.Play("plane animation");
    }
    void _pendulum_sAnimmethod()
    {

        anim = pendulum_s.GetComponent<Animator>();
        anim.Play("pendulum animation");
    }
    void _KidWalkingAnimmethod()
    {

        anim = KidWalking.GetComponent<Animator>();
        anim.Play("kidwalking ani");
    }
    void _kidwAnimmethod()
    {

        anim = kidw.GetComponent<Animator>();
        anim.Play("kidw");
    }
    void _school_bus_empty_Animmethod()
    {
        anim = school_bus.GetComponent<Animator>();
        anim.Play("empty");
    }
    void _bus_empty_Animmethod()
    {
        anim = bus.GetComponent<Animator>();
        anim.Play("empty");
    }
    void _Train_empty_Animmethod()
    {
        anim = Train.GetComponent<Animator>();
        anim.Play("empty");
    }

    void _rope_aniAnimmethod()
    {

        anim = rope_ani.GetComponent<Animator>();
        anim.Play("thread animation");
    }
    void _busAnimmethod()
    {

        anim = bus.GetComponent<Animator>();
        anim.Play("bus animation");
    }
    void _school_busAnimmethod()
    {

        anim = school_bus.GetComponent<Animator>();
        anim.Play("school bus ani");
    }
    void _TrainAnimmethod()
    {

        anim = Train.GetComponent<Animator>();
        anim.Play("train animation");
    }

    void _AeroplaneAnimmethod()
    {

        anim = Aeroplane.GetComponent<Animator>();
        anim.Play("plane animation");
    }
    void _Car_1Animmethod()
    {

        anim = Car_1.GetComponent<Animator>();
        anim.Play("car animation");
    }
    void _Cube_lineAnimmethod()
    {

        anim = Cube_line.GetComponent<Animator>();
        anim.Play("cube animation");
    }
    void _D_HolderAnimmethod()
    {

        anim = D_Holder.GetComponent<Animator>();
        anim.Play("sec anim");
    }
    void _fan_rAnimmethod()
    {

        anim = fan_r.GetComponent<Animator>();
        anim.Play("fan animation");
    }
    void _honey_beeAnimmethod()
    {

        anim = honey_bee.GetComponent<Animator>();
        anim.Play("honey animation");
    }



    void _audio_2_audioMethod()
    {
        myAudio.clip = audio_2;
        myAudio.Play();
    }
    void _audio_3_audioMethod()
    {
        myAudio.clip = audio_3;
        myAudio.Play();
    }
    void _audio_4_audioMethod()
    {
        myAudio.clip = audio_4;
        myAudio.Play();
    }
    void _audio_5_audioMethod()
    {
        myAudio.clip = audio_5;
        myAudio.Play();
    }
    void _audio_6_audioMethod()
    {
        myAudio.clip = audio_6;
        myAudio.Play();
    }
    void _audio_7_audioMethod()
    {
        myAudio.clip = audio_7;
        myAudio.Play();
    }
    void _audio_8_audioMethod()
    {
        myAudio.clip = audio_8;
        myAudio.Play();
    }
    void _audio_9_audioMethod()
    {
        myAudio.clip = audio_9;
        myAudio.Play();
    }
    void _audio_10_audioMethod()
    {
        myAudio.clip = audio_10;
        myAudio.Play();
    }
    void _audio_11_audioMethod()
    {
        myAudio.clip = audio_11;
        myAudio.Play();
    }
    void _audio_12_audioMethod()
    {
        myAudio.clip = audio_12;
        myAudio.Play();
    }
    void _audio_13_audioMethod()
    {
        myAudio.clip = audio_13;
        myAudio.Play();
    }
    void _audio_14_audioMethod()
    {
        myAudio.clip = audio_14;
        myAudio.Play();
    }
    void _audio_15_audioMethod()
    {
        myAudio.clip = audio_15;
        myAudio.Play();
    }
    void _audio_16_audioMethod()
    {
        myAudio.clip = audio_16;
        myAudio.Play();
    }
    void _audio_17_audioMethod()
    {
        myAudio.clip = audio_17;
        myAudio.Play();
    }
    void _audio_18_audioMethod()
    {
        myAudio.clip = audio_18;
        myAudio.Play();
    }
    void _audio_19_audioMethod()
    {
        myAudio.clip = audio_19;
        myAudio.Play();
    }
    void _audio_20_audioMethod()
    {
        myAudio.clip = audio_20;
        myAudio.Play();
    }
    void _audio_21_audioMethod()
    {
        myAudio.clip = audio_21;
        myAudio.Play();
    }
    void _audio_22_audioMethod()
    {
        myAudio.clip = audio_22;
        myAudio.Play();
    }
    void _audio_23_audioMethod()
    {
        myAudio.clip = audio_23;
        myAudio.Play();
    }
    void _audio_24_audioMethod()
    {
        myAudio.clip = audio_24;
        myAudio.Play();
    }
    void _audio_25_audioMethod()
    {
        myAudio.clip = audio_25;
        myAudio.Play();
    }
    void _audio_26_audioMethod()
    {
        myAudio.clip = audio_26;
        myAudio.Play();
    }
    void _audio_27_audioMethod()
    {
        myAudio.clip = audio_27;
        myAudio.Play();
    }
    void _audio_28_audioMethod()
    {
        myAudio.clip = audio_28;
        myAudio.Play();
    }
    void _audio_29_audioMethod()
    {
        myAudio.clip = audio_29;
        myAudio.Play();
    }
    void _audio_30_audioMethod()
    {
        myAudio.clip = audio_30;
        myAudio.Play();
    }
    void _audio_31_audioMethod()
    {
        myAudio.clip = audio_31;
        myAudio.Play();
    }








}
