using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_class6light : MonoBehaviour
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
            animator.Play("class6 animation", 0, targetNormalizedTime);
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
            case 1: Level4(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
    }

    public void PlayAnimTrigg(Animator anim)
    {
        anim.SetTrigger("Trigger"); // Replace "TriggerName" with the actual name of your trigger
    }


    public void level()
    {
        miniGames.Output();

    }
    public GameObject lv1;

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
    public void Savepoint1()
    {
        SaveProgress(1, 0, 3);
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

        level();
    }

    public Camera cam;
    public LayerMask layerMask;
    private bool canChoose = true;
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
        Invoke("StepComplete", 1f);
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


    //scripts
    [Header("Script GO")]
    public GameObject skynight;

    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject pasted__semi_transparent3;
    public GameObject pasted__box3;

    public GameObject candle;
   
    public GameObject flower_vaseA;
    public GameObject flowers_vaseB;
    public GameObject trouchlightrayA;
    public GameObject trouchlightrayB;
    public GameObject mirror_stand;
    public GameObject moon;
    public GameObject cover_plane;
    public GameObject pinhole_cemara_raysA;
    public GameObject pinhole_cemara_raysB;
    public GameObject sky;
    public GameObject t_o_t;
    public GameObject trouchA;
    public GameObject trouchB;
    public GameObject tubelight;
    public GameObject wooden_boardA;
    public GameObject wooden_boardB;
    public GameObject arrow_board;
    public GameObject flowervaserayA1;
    public GameObject flowervaserayB1;
    public GameObject lightray2;
    public GameObject blank_transparant;
    public GameObject blank_translucent;
    public GameObject blank_opaque;
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
    public GameObject title_13;
    public GameObject title_14;
    public GameObject title_15;

    // Exp - Animations
    private Animator anim;

    [Header("Explanation anims")]

    public GameObject arrow_board_rays;
    public GameObject pinhole_camera;
    public GameObject pinholecamera_rays;
    public GameObject trouchlightrays;
    public GameObject woodenboards;
    public GameObject tot_lightray;
    public GameObject flowervesaraysn;
    public GameObject tot_lightrays1;
    public GameObject lampr;
    public GameObject lamprc;

    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

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







    public AudioClip lt;
    public AudioClip lt_in;
    public AudioClip s_o_lt_;
    public AudioClip s_o_lt_in;
    public AudioClip Ls_obj;
    public AudioClip ty_s_o_lt_;
    public AudioClip na_s_o_lt_;
    public AudioClip na_s_o_lt_in;
    public AudioClip af_s_o_lt_;
    public AudioClip af_s_o_lt_in;
    public AudioClip af_s_o_lt_ex;
    public AudioClip mn;
    public AudioClip pr_o_lt;
    public AudioClip pr_o_lt_1;
    public AudioClip pr_o_lt_2;
    public AudioClip pr_o_lt_3;
    public AudioClip pr_o_lt_4;
    public AudioClip t_o_ta_obj;
    public AudioClip t_o_ta_obj_in;
    public AudioClip t_obj;
    public AudioClip t_obj_in;
    public AudioClip o_obj;
    public AudioClip o_obj_in;
    public AudioClip ta_obj;
    public AudioClip ta_obj_in;
    public AudioClip sa;
    public AudioClip sa_1;
    public AudioClip sa_2;
    public AudioClip sa_3;
    public AudioClip sa_4;
    public AudioClip mirror_reflection;
    public AudioClip mirror_reflection_exp;
    public AudioClip making_of_pinhole;
    public AudioClip working_of_pinhole;

    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }

    //Scripts
    void pasted__box3MethodON()

    {
        pasted__box3.GetComponent<ColorLerp>().enabled = true;
    }

    void skynightMethodON()

    {
        skynight.GetComponent<LightIntensity>().enabled = true;
    }

    //
    void _pasted__semi_transparent3MethodON()
    {
        pasted__semi_transparent3.SetActive(true);
    }
    void _pasted__semi_transparent3MethodOFF()
    {
        pasted__semi_transparent3.SetActive(false);
    }
    void _title_1MethodON()
    {
        title_1.SetActive(true);
    }
    void _title_2MethodON()
    {
        title_2.SetActive(true);
    }
    void _title_3MethodON()
    {
        title_3.SetActive(true);
    }
    void _title_4MethodON()
    {
        title_4.SetActive(true);
    }
    void _title_5MethodON()
    {
        title_5.SetActive(true);
    }
    void _title_6MethodON()
    {
        title_6.SetActive(true);
    }
    void _title_7MethodON()
    {
        title_7.SetActive(true);
    }
    void _title_8MethodON()
    {
        title_8.SetActive(true);
    }
    void _title_9MethodON()
    {
        title_9.SetActive(true);
    }
    void _title_10MethodON()
    {
        title_10.SetActive(true);
    }
    void _title_11MethodON()
    {
        title_11.SetActive(true);
    }
    void _title_12MethodON()
    {
        title_12.SetActive(true);
    }
    void _title_13MethodON()
    {
        title_13.SetActive(true);
    }
    void _title_14MethodON()
    {
        title_14.SetActive(true);
    }
    void _title_15MethodON()
    {
        title_15.SetActive(true);
    }







    void _lamprcMethodON()
    {
        lamprc.SetActive(true);
    }
    void _lamprcMethodOFF()
    {
        lamprc.SetActive(false);
    }
    void _blank_transparantMethodON()
    {
        blank_transparant.SetActive(true);
    }
    void _blank_transparantMethodOFF()
    {
        blank_transparant.SetActive(false);
    }
    void _blank_translucentMethodON()
    {
        blank_translucent.SetActive(true);
    }
    void _blank_translucentMethodOFF()
    {
        blank_translucent.SetActive(false);
    }
    void _blank_opaqueMethodON()
    {
        blank_opaque.SetActive(true);
    }
    void _blank_opaqueMethodOFF()
    {
        blank_opaque.SetActive(false);
    }




    void _lightray2MethodON()
    {
        lightray2.SetActive(true);
    }
    void _lightray2MethodOFF()
    {
        lightray2.SetActive(false);
    }

    void _arrow_board_raysMethodON()
    {
        arrow_board_rays.SetActive(true);
    }
    void _arrow_board_raysMethodOFF()
    {
        arrow_board_rays.SetActive(false);
    }

    void _candleMethodON()
    {
        candle.SetActive(true);
    }
    void _candleMethodOFF()
    {
        candle.SetActive(false);
    }

   

    void _flower_vaseAMethodON()
    {
        flower_vaseA.SetActive(true);
    }
    void _flower_vaseAMethodOFF()
    {
        flower_vaseA.SetActive(false);
    }

    void _flowers_vaseBMethodON()
    {
        flowers_vaseB.SetActive(true);
    }
    void _flowers_vaseBMethodOFF()
    {
        flowers_vaseB.SetActive(false);
    }

   

    void _trouchlightrayAMethodON()
    {
        trouchlightrayA.SetActive(true);
    }
    void _trouchlightrayAMethodOFF()
    {
        trouchlightrayA.SetActive(false);
    }

    void _trouchlightrayBMethodON()
    {
        trouchlightrayB.SetActive(true);
    }
    void _trouchlightrayBMethodOFF()
    {
        trouchlightrayB.SetActive(false);
    }

    void _mirror_standMethodON()
    {
        mirror_stand.SetActive(true);
    }
    void _mirror_standMethodOFF()
    {
        mirror_stand.SetActive(false);
    }

    void _moonMethodON()
    {
        moon.SetActive(true);
    }
    void _moonMethodOFF()
    {
        moon.SetActive(false);
    }

    void _pinhole_cameraMethodON()
    {
        pinhole_camera.SetActive(true);
    }
    void _pinhole_cameraMethodOFF()
    {
        pinhole_camera.SetActive(false);
    }

    void _cover_planeMethodON()
    {
        cover_plane.SetActive(true);
    }
    void _cover_planeMethodOFF()
    {
        cover_plane.SetActive(false);
    }

    void _pinhole_cemara_raysAMethodON()
    {
        pinhole_cemara_raysA.SetActive(true);
    }
    void _pinhole_cemara_raysAMethodOFF()
    {
        pinhole_cemara_raysA.SetActive(false);
    }

    void _pinhole_cemara_raysBMethodON()
    {
        pinhole_cemara_raysB.SetActive(true);
    }
    void _pinhole_cemara_raysBMethodOFF()
    {
        pinhole_cemara_raysB.SetActive(false);
    }

    void _skyMethodON()
    {
        sky.SetActive(true);
    }
    void _skyMethodOFF()
    {
        sky.SetActive(false);
    }

    void _t_o_tMethodON()
    {
        t_o_t.SetActive(true);
    }
    void _t_o_tMethodOFF()
    {
        t_o_t.SetActive(false);
    }

    void _trouchAMethodON()
    {
        trouchA.SetActive(true);
    }
    void _trouchAMethodOFF()
    {
        trouchA.SetActive(false);
    }

    void _trouchBMethodON()
    {
        trouchB.SetActive(true);
    }
    void _trouchBMethodOFF()
    {
        trouchB.SetActive(false);
    }

    void _tubelightMethodON()
    {
        tubelight.SetActive(true);
    }
    void _tubelightMethodOFF()
    {
        tubelight.SetActive(false);
    }

    void _wooden_boardAMethodON()
    {
        wooden_boardA.SetActive(true);
    }
    void _wooden_boardAMethodOFF()
    {
        wooden_boardA.SetActive(false);
    }

    void _wooden_boardBMethodON()
    {
        wooden_boardB.SetActive(true);
    }
    void _wooden_boardBMethodOFF()
    {
        wooden_boardB.SetActive(false);
    }
    void _arrow_boardMethodON()
    {
        arrow_board.SetActive(true);
    }
    void _arrow_boardMethodOFF()
    {
        arrow_board.SetActive(false);
    }
    void _tot_lightrayMethodON()
    {
        tot_lightray.SetActive(true);
    }
    void _tot_lightrayMethodOFF()
    {
        tot_lightray.SetActive(false);
    }
    void _flowervaserayA1MethodON()
    {
        flowervaserayA1.SetActive(true);
    }
    void _flowervaserayA1MethodOFF()
    {
        flowervaserayA1.SetActive(false);
    }
    void _flowervaserayB1MethodON()
    {
        flowervaserayB1.SetActive(true);
    }
    void _flowervaserayB1MethodOFF()
    {
        flowervaserayB1.SetActive(false);
    }
    void _flowervesaraysnMethodON()
    {
        flowervesaraysn.SetActive(true);
    }
    void _flowervesaraysnMethodOFF()
    {
        flowervesaraysn.SetActive(false);
    }
  
    void _tot_lightrays1MethodON()
    {
        tot_lightrays1.SetActive(true);
    }
    void _tot_lightrays1MethodOFF()
    {
        tot_lightrays1.SetActive(false);
    }
    void _lamprMethodON()
    {
        lampr.SetActive(true);
    }
    void _lamprMethodOFF()
    {
        lampr.SetActive(false);
    }
   




    void _tot_lightray_empty_Animmethod()
    {
        anim = tot_lightray.GetComponent<Animator>();
        anim.Play("empty"); 
    }
    void _tot_lightrays1_empty_Animmethod()
    {
        anim = tot_lightrays1.GetComponent<Animator>();
        anim.Play("empty");
    }
    void _arrow_board_rays_Animmethod()
    {

        anim = arrow_board_rays.GetComponent<Animator>();
        anim.Play("arrowboardraysanimation");
    }

    void pinhole_cameraAnimmethod()
    {

        anim = pinhole_camera.GetComponent<Animator>();
        anim.Play("pinholecameraanimation");
    }
   

    void lamp_b_Animmethod()
    {

        anim = pinholecamera_rays.GetComponent<Animator>();
        anim.Play("lamp_anim");
    }

    void pinholecamera_raysAnimmethod()
    {

        anim = pinholecamera_rays.GetComponent<Animator>();
        anim.Play("pinholeraysanimation");
    }
    void trouchlightraysAnimmethod()
    {

        anim = trouchlightrays.GetComponent<Animator>();
        anim.Play("trouchlightraysanimation");
    }
    void woodenboardsAnimmethod()
    {

        anim = woodenboards.GetComponent<Animator>();
        anim.Play("woodencard board animation");
    }
    void tot_lightrayAnimmethod()
    {

        anim = tot_lightray.GetComponent<Animator>();
        anim.Play("totlightrayanimation");
    }
    void flowervesaraysnmethod()
    {

        anim = flowervesaraysn.GetComponent<Animator>();
        anim.Play("nflowervaseraysn");
    }
    void tot_lightrays1method()
    {

        anim = tot_lightrays1.GetComponent<Animator>();
        anim.Play("tot lightrays1 animation");
    }
    void lamprmethod()
    {

        anim = lampr.GetComponent<Animator>();
        anim.Play("lampr animation");
    }
    void lamprcmethod()
    {

        anim = lamprc.GetComponent<Animator>();
        anim.Play("lamprcani");
    }

    void _1_audioMethod()
    {
        myAudio.clip = lt;
        myAudio.Play();
    }

    void _2_audioMethod()
    {
        myAudio.clip = lt_in;
        myAudio.Play();
    }

    void _3_audioMethod()
    {
        myAudio.clip = s_o_lt_;
        myAudio.Play();
    }

    void _4_audioMethod()
    {
        myAudio.clip = s_o_lt_in;
        myAudio.Play();
    }

    void _5_audioMethod()
    {
        myAudio.clip = Ls_obj;
        myAudio.Play();
    }

    void _6_audioMethod()
    {
        myAudio.clip = ty_s_o_lt_;
        myAudio.Play();
    }

    void _7_audioMethod()
    {
        myAudio.clip = na_s_o_lt_;
        myAudio.Play();
    }

    void _8_audioMethod()
    {
        myAudio.clip = na_s_o_lt_in;
        myAudio.Play();
    }

    void _9_audioMethod()
    {
        myAudio.clip = af_s_o_lt_;
        myAudio.Play();
    }

    void _10_audioMethod()
    {
        myAudio.clip = af_s_o_lt_in;
        myAudio.Play();
    }

    void _11_audioMethod()
    {
        myAudio.clip = af_s_o_lt_ex;
        myAudio.Play();
    }

    void _12_audioMethod()
    {
        myAudio.clip = mn;
        myAudio.Play();
    }

    void _13_audioMethod()
    {
        myAudio.clip = pr_o_lt;
        myAudio.Play();
    }

    void _14_audioMethod()
    {
        myAudio.clip = pr_o_lt_1;
        myAudio.Play();
    }

    void _15_audioMethod()
    {
        myAudio.clip = pr_o_lt_2;
        myAudio.Play();
    }

    void _16_audioMethod()
    {
        myAudio.clip = pr_o_lt_3;
        myAudio.Play();
    }

    void _17_audioMethod()
    {
        myAudio.clip = pr_o_lt_4;
        myAudio.Play();
    }

    void _18_audioMethod()
    {
        myAudio.clip = t_o_ta_obj;
        myAudio.Play();
    }

    void _19_audioMethod()
    {
        myAudio.clip = t_o_ta_obj_in;
        myAudio.Play();
    }

    void _20_audioMethod()
    {
        myAudio.clip = t_obj;
        myAudio.Play();
    }

    void _21_audioMethod()
    {
        myAudio.clip = t_obj_in;
        myAudio.Play();
    }

    void _22_audioMethod()
    {
        myAudio.clip = o_obj;
        myAudio.Play();
    }

    void _23_audioMethod()
    {
        myAudio.clip = o_obj_in;
        myAudio.Play();
    }

    void _24_audioMethod()
    {
        myAudio.clip = ta_obj;
        myAudio.Play();
    }

    void _25_audioMethod()
    {
        myAudio.clip = ta_obj_in;
        myAudio.Play();
    }

    void _26_audioMethod()
    {
        myAudio.clip = sa;
        myAudio.Play();
    }

    void _27_audioMethod()
    {
        myAudio.clip = sa_1;
        myAudio.Play();
    }

    void _28_audioMethod()
    {
        myAudio.clip = sa_2;
        myAudio.Play();
    }

    void _29_audioMethod()
    {
        myAudio.clip = sa_3;
        myAudio.Play();
    }

    void _30_audioMethod()
    {
        myAudio.clip = sa_4;
        myAudio.Play();
    }
    void _mirror_reflection_audioMethod()
    {
        myAudio.clip = mirror_reflection;
        myAudio.Play();
    }
    void _mirror_reflection_exp_audioMethod()
    {
        myAudio.clip = mirror_reflection_exp;
        myAudio.Play();
    }
    void _making_of_pinhole_audioMethod()
    {
        myAudio.clip = making_of_pinhole;
        myAudio.Play();
    }
    void _working_of_pinhole_audioMethod()
    {
        myAudio.clip = working_of_pinhole;
        myAudio.Play();
    }

}



