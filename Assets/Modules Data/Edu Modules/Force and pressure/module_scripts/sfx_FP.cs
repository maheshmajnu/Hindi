using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sfx_FP : MonoBehaviour
{
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public TargetController miniGame1;


    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;

    public ObjectiveController objectiveController;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camera_anim", 0, targetNormalizedTime);
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


    //Saveprogress//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Level3()
    {
        StartCoroutine(DelayLv3MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(1, 0, 2);

    }
    IEnumerator DelayLv3MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv3MiniGame.Output();
    }

    public TargetController lv3MiniGame;
    public void Savepoint1()
    {
        SaveProgress(1, 0, 2);
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
    }

    public void TurnOnGOWithDelay(GameObject obj)
    {
        StartCoroutine(ObjectTurnOnDelay(obj));
    }

    IEnumerator ObjectTurnOnDelay(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ParentCamera(Transform holder)
    {
        this.transform.SetParent(holder);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
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

    public void StepCompleted()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Transform camHolder2Lv5;
    public void PlayAnimation(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void PlayerPosChangeDelay(Transform pos)
    {
        StartCoroutine(ChangePlayerPos(pos));
    }

    public void RotateBar(TargetController rotatable)
    {
        rotatable.rotationCount--;

        if(rotatable.rotationCount == 0)
        {
            rotatable.gameObject.tag = "UnTagged";
            StartCoroutine(BarRotated(rotatable));
        }
    }

    IEnumerator BarRotated(TargetController rotatable)
    {
        yield return new WaitForSeconds(1);
        rotatable.defaultEvent.Invoke();
    }

    IEnumerator ChangePlayerPos(Transform pos)
    {
        yield return new WaitForSeconds(5);
        InventoryManager.Instance.player.ChangePosition(pos);
    }

    public GameObject level5;
    public int index = 0;

    public void Level4Game()
    {
        index++;
        if(index == 3)
        {
            StepCompleted();
            level5.SetActive(true);
        }
    }

    private bool pressureMiniGame = false;
    private float pressureTimer = 5;
    public TextMeshProUGUI pressureTimerText;

    public Camera cam;
    public LayerMask layerMask;
    private void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }


        if (pressureMiniGame)
        {
            pressureTimerText.gameObject.SetActive(true);
            pressureTimer -= Time.deltaTime;

            if(pressureTimer <= 0)
            {
                pressureMiniGame = false;
                MissionFailed();
                return;
            }

            pressureTimerText.text = pressureTimer.ToString("0");

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                pressureBar.gameObject.SetActive(true);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
                {
                    if (raycastHit.collider != null)
                    {
                        barValue += 0.1f;
                        barImageFill.fillAmount = barValue;

                        if( barValue >= 1) 
                        {
                            pressureMiniGame = false;
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                            StepCompleted();
                            return;
                        }

                    }
                }
            }
        }
       
    }

    public void PressureMiniGame()
    {
        pressureMiniGame = true;
    }

    public GameObject pressureBar;
    public Image barImageFill;
    private float barValue;

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject man_1;
    public GameObject man_2;
    public GameObject stadium;
    public GameObject footbal_ground;
    public GameObject table_with_things;
    public GameObject title_1;
    public GameObject title_2;
    public GameObject title_3;
    public GameObject title_4;
    public GameObject title_5;
    public GameObject title_6;
    public GameObject title_7;
    public GameObject title_8;
    public GameObject title_9;
    public GameObject title_10;
    public GameObject title_11;
    public GameObject title_12;
    public GameObject des_1;
    public GameObject des_2;
    public GameObject ballon_with_thread;
    public GameObject roti_balls;
    public GameObject label_1;
    public GameObject label_2;
    public GameObject label_3;
    public GameObject boxes_in_row;
    public GameObject boxes_;
    public GameObject needle_on_cloth;






    // Exp - Animations
    private Animator anim;
    [Header("Explanation anims")]

    public GameObject ballon;
    public GameObject ballon_pressing_anim;
    public GameObject hitting_anim;
    public GameObject roti_with_stick;
    public GameObject cricket_anim;
    public GameObject box;
    public GameObject boy;
    public GameObject foot_ball;
    public GameObject ballon_anim;
    public GameObject attract_anim;
    public GameObject not_attract_anim;
    public GameObject apple;
    public GameObject paper_attract_anim;
    public GameObject rubbing_anim;
    public GameObject cutting_anim;
    public GameObject syrine_anim;
    public GameObject ink_anim;
    public GameObject beaker_anim;
    public GameObject arrow_anim;





    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip eoi_1;
    public AudioClip eoi_2;
    public AudioClip eoi_3;
    public AudioClip eoi_4;
    public AudioClip eof_1;
    public AudioClip eof_2;
    public AudioClip eof_3;
    public AudioClip eof_4;
    public AudioClip eof_5;
    public AudioClip eof_6;
    public AudioClip eof_7;
    public AudioClip eof_8;
    public AudioClip eof_9;
    public AudioClip eof_10;
    public AudioClip eof_11;
    public AudioClip kof_1;
    public AudioClip kof_2;
    public AudioClip kof_3;
    public AudioClip kof_4;
    public AudioClip ncf_1;
    public AudioClip ncf_2;
    public AudioClip gf_1;
    public AudioClip gf_2;
    public AudioClip mf_1;
    public AudioClip mf_2;
    public AudioClip mf_3;
    public AudioClip ef_1;
    public AudioClip ef_2;
    public AudioClip ef_3;
    public AudioClip ef_4;
    public AudioClip pr_1;
    public AudioClip pr_2;
    public AudioClip pr_3;
    public AudioClip pr_4;
    public AudioClip pr_5;
    public AudioClip deop_1;
    public AudioClip deop_2;
    public AudioClip deop_3;
    public AudioClip deop_4;
    public AudioClip dl_1;
    public AudioClip dl_2;
    public AudioClip dl_3;
    public AudioClip dl_4;
    public AudioClip air_1;
    public AudioClip air_2;
    public AudioClip air_3;
    public AudioClip air_4;
    public AudioClip d_ex_1;
    public AudioClip d_ex_2;
    public AudioClip d_ex_3;
    public AudioClip d_ex_4;
    //
    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }
    //

    //
    void _title_1_MethodON()
    {
        title_1.SetActive(true);
    }
    //
    void _title_2_MethodON()
    {
        title_2.SetActive(true);
    }
    //
    void _title_3_MethodON()
    {
        title_3.SetActive(true);
    }
    //
    void _title_4_MethodON()
    {
        title_4.SetActive(true);
    }
    //
    void _title_5_MethodON()
    {
        title_5.SetActive(true);
    }
    //
    void _title_6_MethodON()
    {
        title_6.SetActive(true);
    }
    //
    void _title_7_MethodON()
    {
        title_7.SetActive(true);
    }
    //
    void _title_8_MethodON()
    {
        title_8.SetActive(true);
    }
    //
    void _title_9_MethodON()
    {
        title_9.SetActive(true);
    }
    //
    void _title_10_MethodON()
    {
        title_10.SetActive(true);
    }
    //
    void _title_11_MethodON()
    {
        title_11.SetActive(true);
    }
    //
    void _title_12_MethodON()
    {
        title_12.SetActive(true);
    }
    void _T12MethodOFF()
    {
        title_12.SetActive(false);
    }
    //
    void _des_1_MethodON()
    {
        des_1.SetActive(true);
    }
    //
    void _des_2_MethodON()
    {
        des_2.SetActive(true);
    }
    //
    void _man_1_MethodON()
    {
        man_1.SetActive(true);
    }
    //
    void _man_1_MethodOFF()
    {
        man_1.SetActive(false);
    }
    //
    void _man_2_MethodON()
    {
        man_2.SetActive(true);
    }
    //
    void _man_2_MethodOFF()
    {
        man_2.SetActive(false);
    }
    //
    void _stadium_MethodON()
    {
        stadium.SetActive(true);
    }
    //
    void _stadium_MethodOFF()
    {
        stadium.SetActive(false);
    }
    //
    void _ballon_anim_MethodON()
    {
        ballon_anim.SetActive(true);
    }
    //
    void _ballon_anim_MethodOFF()
    {
        ballon_anim.SetActive(false);
    }
    //
    void _ballon_anim_2_MethodON()
    {
        ballon_anim.SetActive(true);
    }
    //
    void _ballon_anim__2_MethodOFF()
    {
        ballon_anim.SetActive(false);
    }
    //
    void _fb_MethodON()
    {
        footbal_ground.SetActive(true);
    }
    //
    void _fb_MethodOFF()
    {
        footbal_ground.SetActive(false);
    }
    //
    void _table_with_things_MethodON()
    {
        table_with_things.SetActive(true);
    }
    //
    void _table_with_things_MethodOFF()
    {
        table_with_things.SetActive(false);
    }
    //
    void _ballon_with_thread_MethodON()
    {
        ballon_with_thread.SetActive(true);
    }
    //
    void _ballon_with_thread_MethodOFF()
    {
        ballon_with_thread.SetActive(false);
    }
    //
    void _roti_balls_MethodON()
    {
        roti_balls.SetActive(true);
    }
    //
    void _roti_balls_MethodOFF()
    {
        roti_balls.SetActive(false);
    }
    //
    void _roti_with_stick_MethodON()
    {
        roti_with_stick.SetActive(true);
    }
    //
    void _roti_with_stick_MethodOFF()
    {
        roti_with_stick.SetActive(false);
    }
    //
    void _ballon_pressing_MethodON()
    {
        ballon_pressing_anim.SetActive(true);
    }
    //
    void _ballon_pressing_MethodOFF()
    {
        ballon_pressing_anim.SetActive(false);
    }
    //
    void _attract_anim_MethodON()
    {
        attract_anim.SetActive(true);
    }
    //
    void _attract_anim_MethodOFF()
    {
        attract_anim.SetActive(false);
    }
    //
    void _not_attract_anim_MethodON()
    {
        not_attract_anim.SetActive(true);
    }
    //
    void _not_attract_anim_MethodOFF()
    {
        not_attract_anim.SetActive(false);
    }
    //
    void _label_1_MethodON()
    {
        label_1.SetActive(true);
    }
    //
    void _lanel_1_MethodOFF()
    {
        label_1.SetActive(false);
    }
    //
    void _label_2_MethodON()
    {
        label_2.SetActive(true);
    }
    //
    void _lanel_2_MethodOFF()
    {
        label_2.SetActive(false);
    }
    //
    void _label_3_MethodON()
    {
        label_3.SetActive(true);
    }
    //
    void _lanel_3_MethodOFF()
    {
        label_3.SetActive(false);
    }
    //
    void _paper_attract_anim_MethodON()
    {
        paper_attract_anim.SetActive(true);
    }
    //
    void _paper_attract_anim3_MethodOFF()
    {
        paper_attract_anim.SetActive(false);
    }
    //
    void _rubbing_anim_MethodON()
    {
        rubbing_anim.SetActive(true);
    }
    //
    void _rubbing_anim_MethodOFF()
    {
        rubbing_anim.SetActive(false);
    }
    //
    void _boxes_in_row_MethodON()
    {
        boxes_in_row.SetActive(true);
    }
    //
    void _boxes_in_row_MethodOFF()
    {
        boxes_in_row.SetActive(false);
    }
    //
    void _boxes__MethodON()
    {
        boxes_.SetActive(true);
    }
    //
    void _boxes__MethodOFF()
    {
        boxes_.SetActive(false);
    }
    //
    void _cutting_anim__MethodON()
    {
        cutting_anim.SetActive(true);
    }
    //
    void _cutting_anim__MethodOFF()
    {
        cutting_anim.SetActive(false);
    }
    //
    void _needle_on_cloth__MethodON()
    {
        needle_on_cloth.SetActive(true);
    }
    //
    void _needle_on_cloth__MethodOFF()
    {
        needle_on_cloth.SetActive(false);
    }
    //






    //  Jump to point buttons

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





























    //
    void _man_1_Animmethod()
    {

        anim = man_1.GetComponent<Animator>();
        anim.Play("Pushing");
    }
    //
    void _box_pushing__Animmethod()
    {

        anim = man_1.GetComponent<Animator>();
        anim.Play("pushing_2");


        anim = man_2.GetComponent<Animator>();
        anim.Play("pushing_2");

        anim = box.GetComponent<Animator>();
        anim.Play("box_moving_anim");



    }
    //
    void _ballon_Animmethod()
    {

        anim = ballon.GetComponent<Animator>();
        anim.Play("ballon_anim_1");
    }
    //
    //
    void _ballon_rep_Animmethod()
    {

        anim = ballon.GetComponent<Animator>();
        anim.Play("ballon_anim_1", -1, 0);
    }
    //
    void _ballon_1_Animmethod()
    {

        anim = ballon_pressing_anim.GetComponent<Animator>();
        anim.Play("ballon_pressing_anim");
    }
    //
    void _hitting_anim_Animmethod()
    {

        anim = hitting_anim.GetComponent<Animator>();
        anim.Play("ball_hitting");
    }
    //
    void _roti_making_Animmethod()
    {

        anim = roti_with_stick.GetComponent<Animator>();
        anim.Play("roti_making");
    }
    //
    void _cricket_anim_Animmethod()
    {

        anim = cricket_anim.GetComponent<Animator>();
        anim.Play("cricket");
    }
    //
    void _boy_Animmethod()
    {

        anim = boy.GetComponent<Animator>();
        anim.Play("kicking");

        anim = foot_ball.GetComponent<Animator>();
        anim.Play("ball_anim");

    }
    //
    void _boy_1_Animmethod()
    {
        anim = boy.GetComponent<Animator>();
        anim.Play("kicking", -1, 0);

        anim = foot_ball.GetComponent<Animator>();
        anim.Play("ball_anim_2");

       
    }
    //
    void _attract_anim_Animmethod()
    {
        anim = attract_anim.GetComponent<Animator>();
        anim.Play("attract_anim");



    }
    //
    void _not_attract_anim_Animmethod()
    {
        anim = not_attract_anim.GetComponent<Animator>();
        anim.Play("not_attract_anim");

        

    }
    //
    void _apple_Animmethod()
    {
        anim = apple.GetComponent<Animator>();
        anim.Play("apple_fall_anim");

    }
    //
    void _paper_attract_anim_Animmethod()
    {
        anim = paper_attract_anim.GetComponent<Animator>();
        anim.Play("paper_attract_anim");
    }
    //
    void _rubbing_anim_Animmethod()
    {
        anim = rubbing_anim.GetComponent<Animator>();
        anim.Play("rubbing_anim");

        

    }
    //
    void _cuttinging_anim_Animmethod()
    {
        anim = cutting_anim.GetComponent<Animator>();
        anim.Play("cutting_anim");



    }
    //
    void _ink_anim_Animmethod()
    {
        anim = ink_anim.GetComponent<Animator>();
        anim.Play("ink_anim");



    }
    //
    void _syringe_anim_Animmethod()
    {
        anim = syrine_anim.GetComponent<Animator>();
        anim.Play("syringe_anim");



    }
    //
    void _beaker_anim_Animmethod()
    {
        anim = beaker_anim.GetComponent<Animator>();
        anim.Play("beaker_anim");



    }
    //
    void _arrow_anim_Animmethod()
    {
        anim = arrow_anim.GetComponent<Animator>();
        anim.Play("arrow_anim");



    }
    //








    //
    void _eoi_1_audioMethod()
    {
        myAudio.clip = eoi_1;
        myAudio.Play();
    }
    //
    void _eoi_2_audioMethod()
    {
        myAudio.clip = eoi_2;
        myAudio.Play();
    }
    //
    void _eoi_3_audioMethod()
    {
        myAudio.clip = eoi_3;
        myAudio.Play();
    }
    //
    void _eoi_4_audioMethod()
    {
        myAudio.clip = eoi_4;
        myAudio.Play();
    }
    //
    void _eof_1_audioMethod()
    {
        myAudio.clip = eof_1;
        myAudio.Play();
    }
    //
    void _eof_2_audioMethod()
    {
        myAudio.clip = eof_2;
        myAudio.Play();
    }
    //
    void _eof_3_audioMethod()
    {
        myAudio.clip = eof_3;
        myAudio.Play();
    }
    //
    void _eof_4_audioMethod()
    {
        myAudio.clip = eof_4;
        myAudio.Play();
    }
    //
    void _eof_5_audioMethod()
    {
        myAudio.clip = eof_5;
        myAudio.Play();
    }
    //
    void _eof_6_audioMethod()
    {
        myAudio.clip = eof_6;
        myAudio.Play();
    }
    //
    void _eof_7_audioMethod()
    {
        myAudio.clip = eof_7;
        myAudio.Play();
    }
    //
    void _eof_8_audioMethod()
    {
        myAudio.clip = eof_8;
        myAudio.Play();
    }
    //
    void _eof_9_audioMethod()
    {
        myAudio.clip = eof_9;
        myAudio.Play();
    }
    //
    void _eof_10_audioMethod()
    {
        myAudio.clip = eof_10;
        myAudio.Play();
    }
    //
    void _eof_11_audioMethod()
    {
        myAudio.clip = eof_11;
        myAudio.Play();
    }
    //
    void _kof_1_audioMethod()
    {
        myAudio.clip = kof_1;
        myAudio.Play();
    }
    //
    void _kof_2_audioMethod()
    {
        myAudio.clip = kof_2;
        myAudio.Play();
    }
    //
    void _kof_3_audioMethod()
    {
        myAudio.clip = kof_3;
        myAudio.Play();
    }
    //
    void _kof_4_audioMethod()
    {
        myAudio.clip = kof_4;
        myAudio.Play();
    }
    //
    void _ncf_1_audioMethod()
    {
        myAudio.clip = ncf_1;
        myAudio.Play();
    }
    //
    void _ncf_2_audioMethod()
    {
        myAudio.clip = ncf_2;
        myAudio.Play();
    }
    //
    void _gf_1_audioMethod()
    {
        myAudio.clip = gf_1;
        myAudio.Play();
    }
    //
    void _gf_2_audioMethod()
    {
        myAudio.clip = gf_2;
        myAudio.Play();
    }
    //
    void _mf_1_audioMethod()
    {
        myAudio.clip = mf_1;
        myAudio.Play();
    }
    //
    void _mf_2_audioMethod()
    {
        myAudio.clip = mf_2;
        myAudio.Play();
    }
    //
    void _mf_3_audioMethod()
    {
        myAudio.clip = mf_3;
        myAudio.Play();
    }
    //
    void _ef_1_audioMethod()
    {
        myAudio.clip = ef_1;
        myAudio.Play();
    }
    //
    void _ef_2_audioMethod()
    {
        myAudio.clip = ef_2;
        myAudio.Play();
    }
    //
    void _ef_3_audioMethod()
    {
        myAudio.clip = ef_3;
        myAudio.Play();
    }
    //
    void _ef_4_audioMethod()
    {
        myAudio.clip = ef_4;
        myAudio.Play();
    }
    //
    void _pr_1_audioMethod()
    {
        myAudio.clip = pr_1;
        myAudio.Play();
    }
    //
    void _pr_2_audioMethod()
    {
        myAudio.clip = pr_2;
        myAudio.Play();
    }
    //
    void _pr_3_audioMethod()
    {
        myAudio.clip = pr_3;
        myAudio.Play();
    }
    //
    void _pr_4_audioMethod()
    {
        myAudio.clip = pr_4;
        myAudio.Play();
    }
    //
    void _pr_5_audioMethod()
    {
        myAudio.clip = pr_5;
        myAudio.Play();
    }
    //
    void _deop_1_audioMethod()
    {
        myAudio.clip = deop_1;
        myAudio.Play();
    }
    //
    void _deop_2_audioMethod()
    {
        myAudio.clip = deop_2;
        myAudio.Play();
    }
    //
    void _deop_3_audioMethod()
    {
        myAudio.clip = deop_3;
        myAudio.Play();
    }
    //
    void _deop_4_audioMethod()
    {
        myAudio.clip = deop_4;
        myAudio.Play();
    }
    //
    void _dl_1_audioMethod()
    {
        myAudio.clip = dl_1;
        myAudio.Play();
    }
    //
    void _dl_2_audioMethod()
    {
        myAudio.clip = dl_2;
        myAudio.Play();
    }
    //
    void _dl_3_audioMethod()
    {
        myAudio.clip = dl_3;
        myAudio.Play();
    }
    //
    void _dl_4_audioMethod()
    {
        myAudio.clip = dl_4;
        myAudio.Play();
    }
    //
    void _air_1_audioMethod()
    {
        myAudio.clip = air_1;
        myAudio.Play();
    }
    //
    void _air_2_audioMethod()
    {
        myAudio.clip = air_2;
        myAudio.Play();
    }
    //
    void _air_3_audioMethod()
    {
        myAudio.clip = air_3;
        myAudio.Play();
    }
    //
    void _air_4_audioMethod()
    {
        myAudio.clip = air_4;
        myAudio.Play();
    }
    //
    void _d_ex_1_audioMethod()
    {
        myAudio.clip = d_ex_1;
        myAudio.Play();
    }
    //
    void _d_ex_2_audioMethod()
    {
        myAudio.clip = d_ex_2;
        myAudio.Play();
    }
    //
    void _d_ex_3_audioMethod()
    {
        myAudio.clip = d_ex_3;
        myAudio.Play();
    }
    //
    void _d_ex_4_audioMethod()
    {
        myAudio.clip = d_ex_4;
        myAudio.Play();
    }
    //

















}
