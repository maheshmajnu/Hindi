using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_7light : MonoBehaviour
{
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public TargetController miniGame1;

    private GameObject JustInstantiatedNoPlayerCanvas;

    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;


    public ObjectiveController objectiveController;
    private NoPlayerMenu noPlayerMenu;

    private void Awake()
    {
        {
            animator = GetComponent<Animator>();

            // If there is a target time stored, jump to that animation keyframe
            if (targetNormalizedTime >= 0f)
            {
                animator.Play("7class camera", 0, targetNormalizedTime);
                targetNormalizedTime = -1f; // Reset after use
            }
        }
        animator = GetComponent<Animator>();
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
            case 1: Level5(); break;
            default: level(); break;
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

        level();
    }

    public GameObject lv1;
    public void level()
    {
        miniGame1.Output();
    }

    public void Level5()
    {
        StartCoroutine(DelayLv5MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(1, 0, 4);

    }
    IEnumerator DelayLv5MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv5MiniGame.Output();
    }

    public TargetController lv5MiniGame;



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

    public void ChangeCamHolderWithDelay(Transform camHolder)
    {
        StartCoroutine(ChangeCamHolderDelay(camHolder));
    }

    IEnumerator ChangeCamHolderDelay(Transform camHolder)
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = camHolder.position;
        transform.rotation = camHolder.rotation;
    }

    private int index = 0;
    public void MultiSelectAnswer(int count)
    {
        index++;

        if(index == count)
        {
            index = 0;
            StepComplete();
        }
    }

    private int currentIndex = 0;
    public LayerMask layerMask;
    public Camera cam;
    public List<string> answers = new List<string>();
    private bool canChoose = true;
    public Transform camHolder2;
    public Transform camHolder3;
    public GameObject lv3q1;

    private void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }

        if (canChoose)
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
                        if (raycastHit.collider.gameObject.name == answers[currentIndex])
                        {
                            currentIndex++;
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();

                            if(currentIndex == 4)
                            {
                                ChangeCamHolderWithDelay(camHolder2);
                            }

                            if(currentIndex == 5)
                            {
                                ChangeCamHolderWithDelay(camHolder3);
                                TurnOnGOWithDelay(lv3q1);
                                canChoose = false;
                            }
                        }
                        else
                        {
                            MissionFailed();
                        }
                    }
                }
            }
        }
    }

    public void TurnOffRaycast()
    {
        canChoose = false;
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
    public GameObject lampr;
    public GameObject candle;
    public GameObject arrow_board;
    public GameObject trouch;
    public GameObject tubelight;
    public GameObject convex_mirrior_board;
    public GameObject mirror_stand_1;
    public GameObject mirrorstand_2;
    public GameObject ontableblock;
    public GameObject sidetableblock;
    public GameObject lampr_1;
    public GameObject tablelghtray1;
    public GameObject tablelghtray2;
    public GameObject pCube54;
    public GameObject pCube50;
    public GameObject s_arc;
    public GameObject t_arc;
    public GameObject cc_n_cv_d;
    public GameObject convex_concave;
    public GameObject Glasses;
    public GameObject headlight;
    public GameObject lens;
    public GameObject Telescope;
    public GameObject Magni_glass;
    public GameObject red_car_;
    public GameObject trouch_1;
    public GameObject trouch_2;
    public GameObject dentist_mirror;
    public GameObject camera_ob;
    public GameObject camera_ob_1;
    public GameObject convex_n_concave_lens;
    public GameObject concave;
    public GameObject convex;
    public GameObject concave_spoon;
    public GameObject convex_spoon;
    public GameObject theater;
    public GameObject theatre_fbx;
    public GameObject lightray2;
    public GameObject Kid;
    public GameObject kid_1;
    public GameObject convex_concave_sph;
    public GameObject moon;
    public GameObject sky;
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
    public GameObject TITLE_11;
    public GameObject TITLE_12;
    public GameObject TITLE_13;
    public GameObject TITLE_14;
    public GameObject TITLE_15;

    // Exp - Animations
    private Animator anim;
    
    [Header("Explanation anims")]

    public GameObject arrow_board_rays;
    public GameObject wooden_boards;
    public GameObject angle_rays;
    public GameObject pencil;
    public GameObject table_light_ray;
    public GameObject rays_reflection;
    public GameObject convex_l;
    public GameObject concave_l;



    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

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

    public AudioClip pencil_erect;
    public AudioClip penci;
    public AudioClip lens_intro;
    public AudioClip examples;
    public AudioClip concave_lens;
    public AudioClip convex_lens;
    public AudioClip magnifying_glass;
    public AudioClip real_image;
    public AudioClip virtual_image;
    public AudioClip reflection_of_light;
    public AudioClip law_of_reflection_title;
    public AudioClip accoding_to_law_of_reflection;
    public AudioClip law_of_reflection_exp;
    public AudioClip mirror_reflection_of_human;
    public AudioClip mirror;
    public AudioClip exper;
    public AudioClip concave_m;
    public AudioClip convex_m;
    public AudioClip concave_mirror_exp_with_spoon;
    public AudioClip convex_mirror_exp;
    public AudioClip concave_and_convex_spoon;
    public AudioClip uses_of_concave_mirror;
    public AudioClip uses_of_convex_mirror;





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


















    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }

    void _TITLE_1MethodON()
    {
        TITLE_1.SetActive(true);
    }
    void _TITLE_2MethodON()
    {
        TITLE_2.SetActive(true);
    }
    void _TITLE_3MethodON()
    {
        TITLE_3.SetActive(true);
    }
    void _TITLE_4MethodON()
    {
        TITLE_4.SetActive(true);
    }
    void _TITLE_5MethodON()
    {
        TITLE_5.SetActive(true);
    }
    void _TITLE_6MethodON()
    {
        TITLE_6.SetActive(true);
    }
    void _TITLE_7MethodON()
    {
        TITLE_7.SetActive(true);
    }
    void _TITLE_8MethodON()
    {
        TITLE_8.SetActive(true);
    }
    void _TITLE_9MethodON()
    {
        TITLE_9.SetActive(true);
    }
    void _TITLE_10MethodON()
    {
        TITLE_10.SetActive(true);
    }
    void _TITLE_11MethodON()
    {
        TITLE_11.SetActive(true);
    }
    void _TITLE_12MethodON()
    {
        TITLE_12.SetActive(true);
    }
    void _TITLE_13MethodON()
    {
        TITLE_13.SetActive(true);
    }
    void _TITLE_14MethodON()
    {
        TITLE_14.SetActive(true);
    }
    void _TITLE_15MethodON()
    {
        TITLE_15.SetActive(true);
    }



    void skynightMethodON()

    {
        skynight.GetComponent<LightIntensity>().enabled = true;
    }
    void skynightMethodOFF()

    {
        skynight.GetComponent<LightIntensity>().myLight.intensity = 1f;
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

    void _pencil_erect_audioMethod()
    {
        myAudio.clip = pencil_erect;
        myAudio.Play();
    }
    void _penci_audioMethod()
    {
        myAudio.clip = penci;
        myAudio.Play();
    }
    void _lens_intro_audioMethod()
    {
        myAudio.clip = lens_intro;
        myAudio.Play();
    }
    void _examples_audioMethod()
    {
        myAudio.clip = examples;
        myAudio.Play();
    }
    void _concave_lens_audioMethod()
    {
        myAudio.clip = concave_lens;
        myAudio.Play();
    }
    void _convex_lens_audioMethod()
    {
        myAudio.clip = convex_lens;
        myAudio.Play();
    }
    void _magnifying_glass_audioMethod()
    {
        myAudio.clip = magnifying_glass;
        myAudio.Play();
    }
    void _real_image_audioMethod()
    {
        myAudio.clip = real_image;
        myAudio.Play();
    }
    void _virtual_image_audioMethod()
    {
        myAudio.clip = virtual_image;
        myAudio.Play();
    }
    void _reflection_of_light_audioMethod()
    {
        myAudio.clip = reflection_of_light;
        myAudio.Play();
    }
    void _law_of_reflection_title_audioMethod()
    {
        myAudio.clip = law_of_reflection_title;
        myAudio.Play();
    }
    void _accoding_to_law_of_reflection_audioMethod()
    {
        myAudio.clip = accoding_to_law_of_reflection;
        myAudio.Play();
    }
    void _law_of_reflection_exp_audioMethod()
    {
        myAudio.clip = law_of_reflection_exp;
        myAudio.Play();
    }
    void _mirror_reflection_of_human_audioMethod()
    {
        myAudio.clip = mirror_reflection_of_human;
        myAudio.Play();
    }
    void _mirror_audioMethod()
    {
        myAudio.clip = mirror;
        myAudio.Play();
    }
    void _exper_audioMethod()
    {
        myAudio.clip = exper;
        myAudio.Play();
    }
    void _concave_m_audioMethod()
    {
        myAudio.clip = concave_m;
        myAudio.Play();
    }
    void _convex_m_audioMethod()
    {
        myAudio.clip = convex_m;
        myAudio.Play();
    }
    void _concave_mirror_exp_with_spoon_audioMethod()
    {
        myAudio.clip = concave_mirror_exp_with_spoon;
        myAudio.Play();
    }

    void _convex_mirror_exp_audioMethod()
    {
        myAudio.clip = convex_mirror_exp;
        myAudio.Play();
    }
    void _concave_and_convex_spoon_audioMethod()
    {
        myAudio.clip = concave_and_convex_spoon;
        myAudio.Play();
    }
    void _uses_of_concave_mirror_audioMethod()
    {
        myAudio.clip = uses_of_concave_mirror;
        myAudio.Play();
    }
    void _uses_of_convex_mirror_audioMethod()
    {
        myAudio.clip = uses_of_convex_mirror;
        myAudio.Play();
    }
















    void _moonMethodON()
    {
        moon.SetActive(true);
    }
    void _moonMethodOFF()
    {
        moon.SetActive(false);
    }
    void _skyMethodON()
    {
        sky.SetActive(true);
    }
    void _skyMethodOFF()
    {
        sky.SetActive(false);
    }





    void _convex_concave_sphMethodON()
    {
        convex_concave_sph.SetActive(true);
    }
    void _convex_concave_sphMethodOFF()
    {
        convex_concave_sph.SetActive(false);
    }


    //



    void _lamprMethodON()
    {
        lampr.SetActive(true);
    }
    void _lamprMethodOFF()
    {
        lampr.SetActive(false);
    }

    //

    void _candleMethodON()
    {
        candle.SetActive(true);
    }
    void _candleMethodOFF()
    {
        candle.SetActive(false);
    }

    void _tubelightMethodON()
    {
        tubelight.SetActive(true);
    }
    void _tubelightMethodOFF()
    {
        tubelight.SetActive(false);
    }


    void _arrow_boardMethodON()
    {
        arrow_board.SetActive(true);
    }
    void _arrow_boardMethodOFF()
    {
        arrow_board.SetActive(false);
    }

    void _trouchMethodON()
    {
        trouch.SetActive(true);
    }
    void _trouchMethodOFF()
    {
        trouch.SetActive(false);
    }

    void _convex_mirrior_boardMethodON()
    {
        convex_mirrior_board.SetActive(true);
    }
    void _convex_mirrior_boardMethodOFF()
    {
        convex_mirrior_board.SetActive(false);
    }
    void _mirror_stand_1MethodON()
    {
        mirror_stand_1.SetActive(true);
    }
    void _mirror_stand_1MethodOFF()
    {
        mirror_stand_1.SetActive(false);
    }
    void _mirrorstand_2MethodON()
    {
        mirrorstand_2.SetActive(true);
    }
    void _mirrorstand_2MethodOFF()
    {
        mirrorstand_2.SetActive(false);
    }
    void _ontableblockMethodON()
    {
        ontableblock.SetActive(true);
    }
    void _ontableblockMethodOFF()
    {
        ontableblock.SetActive(false);
    }
    void _sidetableblockMethodON()
    {
        sidetableblock.SetActive(true);
    }
    void _sidetableblockMethodOFF()
    {
        sidetableblock.SetActive(false);
    }
    void _lampr_1MethodON()
    {
        lampr_1.SetActive(true);
    }
    void _lampr_1MethodOFF()
    {
        lampr_1.SetActive(false);
    }
    void _tablelghtray1MethodON()
    {
        tablelghtray1.SetActive(true);
    }
    void _tablelghtray1MethodOFF()
    {
        tablelghtray1.SetActive(false);
    }

    void _tablelghtray2MethodON()
    {
        tablelghtray2.SetActive(true);
    }
    void _tablelghtray2MethodOFF()
    {
        tablelghtray2.SetActive(false);
    }
    void _pCube54MethodON()
    {
        pCube54.SetActive(true);
    }
    void _pCube54MethodOFF()
    {
        pCube54.SetActive(false);
    }
    void _pCube50MethodON()
    {
        pCube50.SetActive(true);
    }
    void _pCube50MethodOFF()
    {
        pCube50.SetActive(false);
    }
    void _s_arcMethodON()
    {
        s_arc.SetActive(true);
    }
    void _s_arcMethodOFF()
    {
        s_arc.SetActive(false);
    }
    void _t_arcMethodON()
    {
        t_arc.SetActive(true);
    }
    void _t_arcMethodOFF()
    {
        t_arc.SetActive(false);
    }
    
    void _wooden_boardsMethodON()
    {
        wooden_boards.SetActive(true);
    }
    void _wooden_boardsMethodOFF()
    {
        wooden_boards.SetActive(false);
    }
    void _pencilMethodON()
    {
        pencil.SetActive(true);
    }
    void _pencilMethodOFF()
    {
        pencil.SetActive(false);
    }

    void _cc_n_cv_dMethodON()
    {
        cc_n_cv_d.SetActive(true);
    }
    void _cc_n_cv_dMethodOFF()
    {
        cc_n_cv_d.SetActive(false);
    }
    void _convex_concaveMethodON()
    {
        convex_concave.SetActive(true);
    }
    void _convex_concaveMethodOFF()
    {
        convex_concave.SetActive(false);
    }
    void _GlassesMethodON()
    {
        Glasses.SetActive(true);
    }
    void _GlassesMethodOFF()
    {
        Glasses.SetActive(false);
    }

    void _headlightMethodON()
    {
        headlight.SetActive(true);
    }
    void _headlightMethodOFF()
    {
        headlight.SetActive(false);
    }
    void _lensMethodON()
    {
        lens.SetActive(true);
    }
    void _lensMethodOFF()
    {
        lens.SetActive(false);
    }
    void _TelescopeMethodON()
    {
        Telescope.SetActive(true);
    }
    void _TelescopeMethodOFF()
    {
        Telescope.SetActive(false);
    }
    void _Magni_glassMethodON()
    {
        Magni_glass.SetActive(true);
    }
    void _Magni_glassMethodOFF()
    {
        Magni_glass.SetActive(false);
    }
    void _red_car_MethodON()
    {
        red_car_.SetActive(true);
    }
    void _red_car_MethodOFF()
    {
        red_car_.SetActive(false);
    }
    void _arrow_board_raysMethodON()
    {
        arrow_board_rays.SetActive(true);
    }
    void _arrow_board_raysMethodOFF()
    {
        arrow_board_rays.SetActive(false);
    }
    void _wooden_boards_MethodON()
    {
        wooden_boards.SetActive(true);
    }
    void _wooden_boards_MethodOFF()
    {
        wooden_boards.SetActive(false);
    }
    void _angle_raysMethodON()
    {
        angle_rays.SetActive(true);
    }
    void _angle_raysMethodOFF()
    {
        angle_rays.SetActive(false);
    }
    void _table_light_rayMethodON()
    {
        table_light_ray.SetActive(true);
    }
    void _table_light_rayMethodOFF()
    {
        table_light_ray.SetActive(false);
    }
    void _rays_reflectionMethodON()
    {
        rays_reflection.SetActive(true);
    }
    void _rays_reflectionMethodOFF()
    {
        rays_reflection.SetActive(false);
    }
    void _convex_lMethodON()
    {
        convex_l.SetActive(true);
    }
    void _convex_lMethodOFF()
    {
        convex_l.SetActive(false);
    }
    void _concave_lMethodON()
    {
        concave_l.SetActive(true);
    }
    void _concave_lMethodOFF()
    {
        concave_l.SetActive(false);
    }
    void _trouch_1MethodON()
    {
        trouch_1.SetActive(true);
    }
    void _trouch_1MethodOFF()
    {
        trouch_1.SetActive(false);
    }
    void _trouch_2MethodON()
    {
        trouch_2.SetActive(true);
    }
    void _trouch_2MethodOFF()
    {
        trouch_2.SetActive(false);
    }
    void _dentist_mirrorMethodON()
    {
        dentist_mirror.SetActive(true);
    }
    void _dentist_mirrorMethodOFF()
    {
        dentist_mirror.SetActive(false);
    }
    void _camera_obMethodON()
    {
        camera_ob.SetActive(true);
    }
    void _camera_obMethodOFF()
    {
        camera_ob.SetActive(false);
    }

    void _camera_ob_1MethodON()
    {
        camera_ob_1.SetActive(true);
    }
    void _camera_ob_1MethodOFF()
    {
        camera_ob_1.SetActive(false);
    }
    void _convex_n_concave_lensMethodON()
    {
        convex_n_concave_lens.SetActive(true);
    }
    void _convex_n_concave_lensMethodOFF()
    {
        convex_n_concave_lens.SetActive(false);
    }
    void _concaveMethodON()
    {
        concave.SetActive(true);
    }
    void _concaveMethodOFF()
    {
        concave.SetActive(false);
    }
    void _convexMethodON()
    {
        convex.SetActive(true);
    }
    void _convexMethodOFF()
    {
        convex.SetActive(false);
    }
    void _concave_spoonMethodON()
    {
        concave_spoon.SetActive(true);
    }
    void _concave_spoonMethodOFF()
    {
        concave_spoon.SetActive(false);
    }
    void _convex_spoonMethodON()
    {
        convex_spoon.SetActive(true);
    }
    void _convex_spoonMethodOFF()
    {
        convex_spoon.SetActive(false);
    }
    void _theaterMethodON()
    {
        theater.SetActive(true);
    }
    void _theaterMethodOFF()
    {
        theater.SetActive(false);
    }
    void _theatre_fbxMethodON()
    {
        theatre_fbx.SetActive(true);
    }
    void _theatre_fbxMethodOFF()
    {
        theatre_fbx.SetActive(false);
    }
    void _lightray2MethodON()
    {
        lightray2.SetActive(true);
    }
    void _lightray2MethodOFF()
    {
        lightray2.SetActive(false);
    }
    void _KidMethodON()
    {
        Kid.SetActive(true);
    }
    void _KidMethodOFF()
    {
        Kid.SetActive(false);
    }
    void _kid_1MethodON()
    {
        kid_1.SetActive(true);
    }
    void _kid_1MethodOFF()
    {
        kid_1.SetActive(false);
    }


    void arrow_board_raysAnimmethod()
    {

        anim = arrow_board_rays.GetComponent<Animator>();
        anim.Play("arrowboardraysanimation");
    }

    void wooden_boardsAnimmethod()
    {

        anim = wooden_boards.GetComponent<Animator>();
        anim.Play("woodencard board animation");
    }
    void angle_raysAnimmethod()
    {

        anim = angle_rays.GetComponent<Animator>();
        anim.Play("angle rays animation");
    }
    void pencilAnimmethod()
    {

        anim = pencil.GetComponent<Animator>();
        anim.Play("pencil animation");
    }
    void table_light_rayAnimmethod()
    {

        anim = table_light_ray.GetComponent<Animator>();
        anim.Play("table light rays animation");
    }
    void rays_reflectionAnimmethod()
    {

        anim = rays_reflection.GetComponent<Animator>();
        anim.Play("rays_reflection_animation");
    }
    void convex_lAnimmethod()
    {

        anim = convex_l.GetComponent<Animator>();
        anim.Play("convex lines");
    }
    void concave_lAnimmethod()
    {

        anim = concave_l.GetComponent<Animator>();
        anim.Play("concave lines");
    }




}
