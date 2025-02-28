using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_human_eye : MonoBehaviour
{
    public TargetController lv1MiniGame;
    public List<GameObject> questions;
    private TargetController previousMiniGame;

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

    public AudioSource gameSoundFxSource;
    public List<AudioClip> level1AudioClips;
    private int index;

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
            animator.Play("cam_anim", 0, targetNormalizedTime);
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




    private void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }
    }


    //private void EndGameDelay()
    //{
    //    lv1MiniGame.EndMiniGame();
    //}

   

    public void level()
    {
        lv1MiniGame.Output();
        //Invoke("EndGameDelay", 1f);
    }
    public GameObject lv1;

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
        JustInstantiatedNoPlayerCanvas.SetActive(true);

        InitializeFromCheckpoint();
        level();
        previousMiniGame = lv1MiniGame;
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

    public void ChangeMiniGame(TargetController miniGame)
    {
        StartCoroutine(MiniGameChangeDelay(miniGame));
    }

    IEnumerator MiniGameChangeDelay(TargetController miniGame)
    {
        foreach (GameObject obj in questions)
        {
            obj.SetActive(false);
        }

        yield return new WaitForSeconds(1.5f);
        previousMiniGame.EndMiniGame();
        previousMiniGame = miniGame;
        miniGame.Output();
    }

    public void ChangeCamHolder(Transform camHolder)
    {
        this.transform.position = camHolder.position;
        this.transform.rotation = camHolder.rotation;
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetBool("Bool", true);
    }

    public void EndAnim(Animator anim)
    {
        anim.SetBool("Bool", false);
    }

    public void TurnOnGODelay(GameObject obj)
    {
        StartCoroutine(GOTurnOnDelay(obj));
    }

    IEnumerator GOTurnOnDelay(GameObject obj)
    {
        foreach(GameObject obj2 in questions)
        {
            obj2.SetActive(false);
        }
        yield return new WaitForSeconds(1);
        obj.SetActive(true);
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

    public void StepComp()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }



    // ON - OFF gameobjects
    [Header("Explanation Assets")]
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
    public GameObject title_16;
    public GameObject title_17;







    public GameObject concave_lens;
    public GameObject convex_lens;
    public GameObject biofocal_lens;
    public GameObject cataract;
    public GameObject humaneye;
    public GameObject texts;
    public GameObject label;
    public GameObject twinklingofthestars;
    public GameObject sunrise_and_sunset;
    public GameObject advanced_surise;
    public GameObject delayed_sunset;




    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]

    public GameObject myopia_rays;
    public GameObject Hypermetropia_rays_1;
    public GameObject Hypermetropia_rays_2;
    public GameObject Presbyopia_rays;
    public GameObject refraction_through_a_prism;
    public GameObject dispersion_of_white_light_by_a_glass_prism;
    public GameObject tyndall_effect_anim;
    public GameObject rays_anim;

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
    public AudioClip Twinkling_of_stars_title;
    public AudioClip Twinkling_of_stars_intro;
    public AudioClip tyndall_effect_title;
    public AudioClip tyndall_effect_intro1;
    public AudioClip tyndall_effect_intro2;
    public AudioClip why_is_the_sky_blue_title;
    public AudioClip why_is_the_sky_blue_intro1;
    public AudioClip why_is_the_sky_blue_intro2;
    public AudioClip advanced_sunrise_title;
    public AudioClip advanced_sunrise_intro;
    public AudioClip delayed_sunset_title;
    public AudioClip delayed_sunset_intro;




   
    void title_1_MethodON()
    {
        title_1.SetActive(true);
    }
    void _T1MethodOFF()
    {
        title_1.SetActive(false);
    }
    //
    void title_2_MethodON()
    {
        title_2.SetActive(true);
    }
    void _T2MethodOFF()
    {
        title_2.SetActive(false);
    }
    //
    void title_3_MethodON()
    {
        title_3.SetActive(true);
    }
    void _T3MethodOFF()
    {
        title_3.SetActive(false);
    }
    //
    void title_4_MethodON()
    {
        title_4.SetActive(true);
    }
    void _T4MethodOFF()
    {
        title_4.SetActive(false);
    }
    //
    void title_5_MethodON()
    {
        title_5.SetActive(true);
    }
    void _T5MethodOFF()
    {
        title_5.SetActive(false);
    }
    //
    void title_6_MethodON()
    {
        title_6.SetActive(true);
    }
    void _T6MethodOFF()
    {
        title_6.SetActive(false);
    }
    //
    void title_7_MethodON()
    {
        title_7.SetActive(true);
    }
    void _T7MethodOFF()
    {
        title_7.SetActive(false);
    }
    //
    void title_8_MethodON()
    {
        title_8.SetActive(true);
    }
    void _T8MethodOFF()
    {
        title_8.SetActive(false);
    }
    //
    //
    void title_9_MethodON()
    {
        title_9.SetActive(true);
    }
    void _T9MethodOFF()
    {
        title_9.SetActive(false);
    }
    //
    void title_10_MethodON()
    {
        title_10.SetActive(true);
    }
    void _T10MethodOFF()
    {
        title_10.SetActive(false);
    }
    //

    //
    void title_11_MethodON()
    {
        title_11.SetActive(true);
    }
    void _T11MethodOFF()
    {
        title_11.SetActive(false);
    }
    //
    void title_12_MethodON()
    {
        title_12.SetActive(true);
    }
    void _T12MethodOFF()
    {
        title_12.SetActive(false);
    }
    //
    void title_13_MethodON()
    {
        title_13.SetActive(true);
    }
    void _T13MethodOFF()
    {
        title_13.SetActive(false);
    }
    //
    void title_14_MethodON()
    {
        title_14.SetActive(true);
    }
    void _T14MethodOFF()
    {
        title_14.SetActive(false);
    }
    //
    void title_15_MethodON()
    {
        title_15.SetActive(true);
    }
    void _T15MethodOFF()
    {
        title_15.SetActive(false);
    }

    void title_16_MethodON()
    {
        title_16.SetActive(true);
    }
    void _T16MethodOFF()
    {
        title_16.SetActive(false);
    }

    void title_17_MethodON()
    {
        title_17.SetActive(true);
    }
    void _T17MethodOFF()
    {
        title_17.SetActive(false);
    }
    //
    void humaneye_MethodON()
    {
        humaneye.SetActive(true);
    }

    void humaneye_MethodOFF()
    {
        humaneye.SetActive(false);
    }

    //
    void myopia_rays_1_MethodON()
    {
        myopia_rays.SetActive(true);
    }

    void myopia_rays_1_MethodOFF()
    {
        myopia_rays.SetActive(false);
    }

    //
    void myopia_rays_2_MethodON()
    {
        myopia_rays.SetActive(true);
    }

    void myopia_rays_2_MethodOFF()
    {
        myopia_rays.SetActive(false);
    }

    //
    void concave_lens_MethodON()
    {
        concave_lens.SetActive(true);
    }

    void concave_lens_MethodOFF()
    {
        concave_lens.SetActive(false);
    }

    //
    void Hypermetropia_rays_1_MethodON()
    {
        Hypermetropia_rays_1.SetActive(true);
    }

    void Hypermetropia_rays_1_MethodOFF()
    {
        Hypermetropia_rays_1.SetActive(false);
    }

    //
    void Hypermetropia_rays_2_MethodON()
    {
        Hypermetropia_rays_2.SetActive(true);
    }

    void Hypermetropia_rays_2_MethodOFF()
    {
        Hypermetropia_rays_2.SetActive(false);
    }

    //
    void convex_lens_MethodON()
    {
        convex_lens.SetActive(true);
    }

    void convex_lens_MethodOFF()
    {
        convex_lens.SetActive(false);
    }

    //
    void Presbyopia_rays_MethodON()
    {
        Presbyopia_rays.SetActive(true);
    }

    void Presbyopia_rays_MethodOFF()
    {
        Presbyopia_rays.SetActive(false);
    }
    //
    void biofocal_lens_MethodON()
    {
        biofocal_lens.SetActive(true);
    }

    void biofocal_lens_MethodOFF()
    {
        biofocal_lens.SetActive(false);
    }

    //
    void cataract_MethodON()
    {
        cataract.SetActive(true);
    }

    void cataract_MethodOFF()
    {
        cataract.SetActive(false);
    }

    //
    void refraction_through_a_prism__MethodON()
    {
        refraction_through_a_prism.SetActive(true);
    }

    void refraction_through_a_prism__MethodOFF()
    {
        refraction_through_a_prism.SetActive(false);
    }

    //
    void dispersion_of_white_light_by_a_glass_prism_MethodON()
    {
        dispersion_of_white_light_by_a_glass_prism.SetActive(true);
    }

    void dispersion_of_white_light_by_a_glass_prism_MethodOFF()
    {
        dispersion_of_white_light_by_a_glass_prism.SetActive(false);
    }

    //
    void texts_MethodON()
    {
        texts.SetActive(true);
    }

    void texts_MethodOFF()
    {
        texts.SetActive(false);
    }
    //
    void label_MethodON()
    {
        label.SetActive(true);
    }

    void label_MethodOFF()
    {
        label.SetActive(false);
    }

    //
    void twinklingofthestars_MethodON()
    {
        twinklingofthestars.SetActive(true);
    }

    void twinklingofthestars_MethodOFF()
    {
        twinklingofthestars.SetActive(false);
    }

    //
    void tyndall_effect_anim_MethodON()
    {
        tyndall_effect_anim.SetActive(true);
    }

    void tyndall_effect_anim_MethodOFF()
    {
        tyndall_effect_anim.SetActive(false);
    }

    //
    void rays_anim_MethodON()
    {
        rays_anim.SetActive(true);
    }

    void rays_anim_MethodOFF()
    {
        rays_anim.SetActive(false);
    }
    //
    void sunriseandsunset_MethodON()
    {
        sunrise_and_sunset.SetActive(true);
    }

    void sunriseandsunset_MethodOFF()
    {
        sunrise_and_sunset.SetActive(false);
    }
    //
    void advanced_sunrise_MethodON()
    {
        advanced_surise.SetActive(true);
    }

    void advanced_sunrise_MethodOFF()
    {
        advanced_surise.SetActive(false);
    }
    //
    void delayed_sunset_MethodON()
    {
        delayed_sunset.SetActive(true);
    }

    void delayed_sunset_MethodOFF()
    {
        delayed_sunset.SetActive(false);
    }
    //

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

    //animatons

    //
    void _myopia_rays_animationAnimmethod()
    {

        anim = myopia_rays.GetComponent<Animator>();
        anim.Play("myopia_rays_anim");
    }
    //
    void _myopia_rays_correction_animationAnimmethod()
    {

        anim = myopia_rays.GetComponent<Animator>();
        anim.Play("myopia_Correction_rays_anim");
    }
    //
    void _hypermetopia_rays__animationAnimmethod()
    {

        anim = Hypermetropia_rays_1.GetComponent<Animator>();
        anim.Play("Hypermetropia_rays_anim");
    }
    //
    void _hypermetopia_rays_correction_animationAnimmethod()
    {

        anim = Hypermetropia_rays_2.GetComponent<Animator>();
        anim.Play("Hypermetropia_correction_rays_anim");
    }
    //
    void _Presbyopia_rays_animationAnimmethod()
    {

        anim = Presbyopia_rays.GetComponent<Animator>();
        anim.Play("Presbyopia_rays_anim");
    }
    //
    void _Presbyopia_rays_correction_animationAnimmethod()
    {

        anim = Presbyopia_rays.GetComponent<Animator>();
        anim.Play("Presbyopia_correction_rays_anim");
    }
    //
    void _refraction_through_a_prism_1_animationAnimmethod()
    {

        anim = refraction_through_a_prism.GetComponent<Animator>();
        anim.Play("refraction_through_a_prism_1");
    }
    //
    void _refraction_through_a_prism_2_animationAnimmethod()
    {

        anim = refraction_through_a_prism.GetComponent<Animator>();
        anim.Play("refraction_through_a_prism_2");
    }
    //
    void _dispersion_of_white_light_by_a_glass_prism_animationAnimmethod()
    {

        anim = dispersion_of_white_light_by_a_glass_prism.GetComponent<Animator>();
        anim.Play("dispersion_of_white_light_by_a_glass_prism");
    }
    //
    void _tyndall_effect_anim_animationAnimmethod()
    {

        anim = tyndall_effect_anim.GetComponent<Animator>();
        anim.Play("tyndall_effect_anim");
    }
    //
    void _rays_anim_animationAnimmethod()
    {

        anim = rays_anim.GetComponent<Animator>();
        anim.Play("rays_anim_from_sun");
    }
    //





    //audios

    //
    void _humaneye_audioMethod()

    {
        myAudio.clip = a1;
        myAudio.Play();
    }
    //
    void _humaneye_title_audioMethod()

    {
        myAudio.clip = a2;
        myAudio.Play();
    }
    //
    void _a3_audioMethod()

    {
        myAudio.clip = a3;
        myAudio.Play();
    }
    //
    void _a4_audioMethod()

    {
        myAudio.clip = a4;
        myAudio.Play();
    }
    //
    void _a5_audioMethod()

    {
        myAudio.clip = a5;
        myAudio.Play();
    }
    //
    void _a6_audioMethod()

    {
        myAudio.clip = a6;
        myAudio.Play();
    }
    //
    void _a7_audioMethod()

    {
        myAudio.clip = a7;
        myAudio.Play();
    }
    //
    void _a8_audioMethod()

    {
        myAudio.clip = a8;
        myAudio.Play();
    }
    //
    void _a9_audioMethod()

    {
        myAudio.clip = a9;
        myAudio.Play();
    }
    //
    void _a10_audioMethod()

    {
        myAudio.clip = a10;
        myAudio.Play();
    }
    //
    void _a11_audioMethod()

    {
        myAudio.clip = a11;
        myAudio.Play();
    }
    //
    void _a12_audioMethod()

    {
        myAudio.clip = a12;
        myAudio.Play();
    }
    //
    void _a13_audioMethod()

    {
        myAudio.clip = a13;
        myAudio.Play();
    }
    //
    void _a14_audioMethod()

    {
        myAudio.clip = a14;
        myAudio.Play();
    }
    //
    void _a15_audioMethod()

    {
        myAudio.clip = a15;
        myAudio.Play();
    }
    //
    void _a16_audioMethod()

    {
        myAudio.clip = a16;
        myAudio.Play();
    }
    //
    void _a17_audioMethod()

    {
        myAudio.clip = a17;
        myAudio.Play();
    }
    //
    void _a18_audioMethod()

    {
        myAudio.clip = a18;
        myAudio.Play();
    }
    //
    void _a19_audioMethod()

    {
        myAudio.clip = a19;
        myAudio.Play();
    }
    //
    void _a20_audioMethod()

    {
        myAudio.clip = a20;
        myAudio.Play();
    }
    //
    void _a21_audioMethod()

    {
        myAudio.clip = a21;
        myAudio.Play();
    }
    //
    void _a22_audioMethod()

    {
        myAudio.clip = a22;
        myAudio.Play();
    }
    //
    void _a23_audioMethod()

    {
        myAudio.clip = a23;
        myAudio.Play();
    }
    //
    void _a24_audioMethod()

    {
        myAudio.clip = a24;
        myAudio.Play();
    }
    //
    void _a25_audioMethod()

    {
        myAudio.clip = a25;
        myAudio.Play();
    }
    //
    void _a26_audioMethod()

    {
        myAudio.clip = a26;
        myAudio.Play();
    }
    //
    void _a27_audioMethod()

    {
        myAudio.clip = a27;
        myAudio.Play();
    }
    //
    void _a28_audioMethod()

    {
        myAudio.clip = a28;
        myAudio.Play();
    }
    //
    void _a29_audioMethod()

    {
        myAudio.clip = a29;
        myAudio.Play();
    }
    //
    void _a30_audioMethod()

    {
        myAudio.clip = a30;
        myAudio.Play();
    }
    //
    void _a31_audioMethod()

    {
        myAudio.clip = a31;
        myAudio.Play();
    }
    //
    void _a32_audioMethod()

    {
        myAudio.clip = a32;
        myAudio.Play();
    }
    //
    void _twinklingofthestars_title_audioMethod()

    {
        myAudio.clip = Twinkling_of_stars_title;
        myAudio.Play();
    }
    //
    void _twinklingofthestars_intro_audioMethod()

    {
        myAudio.clip = Twinkling_of_stars_intro;
        myAudio.Play();
    }
    //
    void _tyndalleffect_title_audioMethod()

    {
        myAudio.clip = tyndall_effect_title;
        myAudio.Play();
    }
    //
    void _tyndalleffect_intro1_audioMethod()

    {
        myAudio.clip = tyndall_effect_intro1;
        myAudio.Play();
    }
    //
    void _tyndalleffect_intro2_audioMethod()

    {
        myAudio.clip = tyndall_effect_intro2;
        myAudio.Play();
    }
    //
    void _whyistheskyblue_title_audioMethod()

    {
        myAudio.clip = why_is_the_sky_blue_title;
        myAudio.Play();
    }
    //
    void _whyistheskyblue_intro1_audioMethod()

    {
        myAudio.clip = why_is_the_sky_blue_intro1;

        myAudio.Play();
    }
    //
    void _whyistheskyblue_intro2_audioMethod()

    {
        myAudio.clip = why_is_the_sky_blue_intro2;
        myAudio.Play();
    }
    //
    void _advancedsunrise_title_audioMethod()

    {
        myAudio.clip = advanced_sunrise_title;
        myAudio.Play();
    }
    //
    void _advancedsunrise_intro_audioMethod()

    {
        myAudio.clip = advanced_sunrise_intro;

        myAudio.Play();
    }
    //
    void _delayedsunset_title_audioMethod()

    {
        myAudio.clip = delayed_sunset_title;
        myAudio.Play();
    }
    //
    void _delayedsunset_intro_audioMethod()

    {
        myAudio.clip = delayed_sunset_intro;

        myAudio.Play();
    }
    //










}
