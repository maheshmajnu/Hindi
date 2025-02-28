using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_tissues_c9 : MonoBehaviour
{

    public Transform waypoint1;
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
        myAudio.enabled = false;
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
    public GameObject title_18;
    public GameObject title_19;
    public GameObject title_20;
    public GameObject title_21;
    public GameObject title_22;
    public GameObject title_23;
    public GameObject title_24;
    public GameObject title_25;
    public GameObject title_26;
    public GameObject title_27;
    public GameObject title_28;
    public GameObject title_29;
    public GameObject title_30;
    public GameObject title_31;
    public GameObject title_32;
    public GameObject title_33;
    public GameObject title_34;
    public GameObject title_35;
    public GameObject title_36;
    public GameObject title_37;
    public GameObject title_38;
    public GameObject title_39;
    public GameObject des_1;
    public GameObject des_2;
    public GameObject des_3;
    public GameObject des_4;
    public GameObject des_5;
    public GameObject des_6;
    public GameObject des_7;
    public GameObject des_8;
    public GameObject des_9;
    public GameObject des_10;
    public GameObject des_11;
    public GameObject des_12;
    public GameObject des_13;
    public GameObject des_14;
    public GameObject unicellular;
    public GameObject root;
    public GameObject onion_exp;

    public GameObject planttissues;
    public GameObject animaltissues;

    public GameObject button;


    // Exp - animation

    private Animator anim;

    [Header("Explanation anims")]

    public GameObject vvvv;
    public GameObject boy;
    public GameObject male;
    public GameObject male_;
    public GameObject exdperimentofplantcells;
    public GameObject musculemovemnet;




    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip intro;
    public AudioClip Tissue;
    public AudioClip Plantandanimaltissue_1;
    public AudioClip Plantandanimaltissue_2;
    public AudioClip Typesofplanttissues;
    public AudioClip Meristematictissues;
    public AudioClip Permanenttissue;
    public AudioClip Experiment;
    public AudioClip Characteristicsofmeristematictissues;
    public AudioClip Typesofmeristematictissues;
    public AudioClip Apicalmeristematictissue;
    public AudioClip Lateralmeristematicytissue;
    public AudioClip Intercalarymeristematictissues;
    public AudioClip Functionsofmeristematictissues;
    public AudioClip Permanenttissues;
    public AudioClip Characteristicsofpermanenttissues;
    public AudioClip Typesofpermanenttissues;
    public AudioClip Simplepermanenttissues;
    public AudioClip Typesofsimplepermanenttissues;
    public AudioClip Parenchyma;
    public AudioClip Collenchyma;
    public AudioClip Sclerenchyma;
    public AudioClip experimentofvariouscells;
    public AudioClip variouscellsintro;
    public AudioClip FunctionsofsimplepermanentTissues;
    public AudioClip Typesofcomplexpermanenttissues;
    public AudioClip Xylem;
    public AudioClip Phloem;
    public AudioClip Functionsofcomplexpermanenttissues;
    public AudioClip animaltissue;
    public AudioClip Epithelialtissuesintro;
    public AudioClip Epithelialtissuesau2;
    public AudioClip Epithelialtissuesau3;
    public AudioClip Epithelialtissuesau4;
    public AudioClip Typesofepithelialtissues;
    public AudioClip squamousepithelium;
    public AudioClip Simplesquamousepithelialtissue;
    public AudioClip Stratifiedsquamousepithelialtissue;
    public AudioClip CuboidalEpithelialtissue;
    public AudioClip Glandularepithelium;
    public AudioClip Columnarepitheliumtissues;
    public AudioClip connecticetissue;
    public AudioClip blood;
    public AudioClip bone;
    public AudioClip Ligament;
    public AudioClip Tendons;
    public AudioClip Cartilage;
    public AudioClip Areolartissue;
    public AudioClip Adiposetissue;
    public AudioClip Musculartissue;
    public AudioClip typesofmusculartissues;
    public AudioClip Voluntarymuscles;
    public AudioClip involuntarymuscles;
    public AudioClip Cardiacmuscle;
    public AudioClip Nervoustissue;





    void _camExp_Pause()
    {
        gameObject.GetComponent<Animator>().speed = 0f;
    }
    //


    //
    void planttissues_MethodON()
    {
        planttissues.SetActive(true);
    }

    void planttissues_MethodOFF()
    {
        planttissues.SetActive(false);
    }
    //
    void animalnttissues_MethodON()
    {
        animaltissues.SetActive(true);
    }

    void animaltissues_MethodOFF()
    {
        animaltissues.SetActive(false);
    }
    //

    //
    void title_1_MethodON()
    {
        title_1.SetActive(true);
    }
    //
    void title_2_MethodON()
    {
        title_2.SetActive(true);
    }
    //
    void title_3_MethodON()
    {
        title_3.SetActive(true);
    }
    //
    void title_4_MethodON()
    {
        title_4.SetActive(true);
    }
    //
    void title_5_MethodON()
    {
        title_5.SetActive(true);
    }
    //
    void title_6_MethodON()
    {
        title_6.SetActive(true);
    }
    //
    void title_7_MethodON()
    {
        title_7.SetActive(true);
    }
    //
    void title_8_MethodON()
    {
        title_8.SetActive(true);
    }
    //
    //
    void title_9_MethodON()
    {
        title_9.SetActive(true);
    }
    //
    void title_10_MethodON()
    {
        title_10.SetActive(true);
    }
    //
    void title_11_MethodON()
    {
        title_11.SetActive(true);
    }
    //
    void title_12_MethodON()
    {
        title_12.SetActive(true);
    }
    //
    void title_13_MethodON()
    {
        title_13.SetActive(true);
    }
    //
    void title_14_MethodON()
    {
        title_14.SetActive(true);
    }
    //
    void title_15_MethodON()
    {
        title_15.SetActive(true);
    }
    //
    void title_16_MethodON()
    {
        title_16.SetActive(true);
    }
    //
    void title_17_MethodON()
    {
        title_17.SetActive(true);
    }
    //
    void title_18_MethodON()
    {
        title_18.SetActive(true);
    }
    //
    void title_19_MethodON()
    {
        title_19.SetActive(true);
    }
    //
    void title_20_MethodON()
    {
        title_20.SetActive(true);
    }
    //
    void title_21_MethodON()
    {
        title_21.SetActive(true);
    }
    //
    void title_22_MethodON()
    {
        title_22.SetActive(true);
    }
    //
    void title_23_MethodON()
    {
        title_23.SetActive(true);
    }
    //
    void title_24_MethodON()
    {
        title_24.SetActive(true);
    }
    //
    void title_25_MethodON()
    {
        title_25.SetActive(true);
    }
    //
    void title_26_MethodON()
    {
        title_26.SetActive(true);
    }
    //
    void title_27_MethodON()
    {
        title_27.SetActive(true);
    }
    //
    void title_28_MethodON()
    {
        title_28.SetActive(true);
    }
    //
    void title_29_MethodON()
    {
        title_29.SetActive(true);
    }
    //
    void title_30_MethodON()
    {
        title_30.SetActive(true);
    }
    //
    void title_31_MethodON()
    {
        title_31.SetActive(true);
    }
    //
    void title_32_MethodON()
    {
        title_32.SetActive(true);
    }
    //
    void title_33_MethodON()
    {
        title_34.SetActive(true);
    }
    //
    void title_34_MethodON()
    {
        title_34.SetActive(true);
    }
    //
    void title_35_MethodON()
    {
        title_35.SetActive(true);
    }
    //
    void title_36_MethodON()
    {
        title_36.SetActive(true);
    }
    //
    void title_37_MethodON()
    {
        title_37.SetActive(true);
    }
    //
    void title_38_MethodON()
    {
        title_38.SetActive(true);
    }
    //
    //
    void title_39_MethodON()
    {
        title_39.SetActive(true);
    }
    //
    void des_1_MethodON()
    {
        des_1.SetActive(true);
    }
    //
    void des_2_MethodON()
    {
        des_2.SetActive(true);
    }
    //
    void des_3_MethodON()
    {
        des_3.SetActive(true);
    }
    //
    void des_4_MethodON()
    {
        des_4.SetActive(true);
    }
    //
    void des_5_MethodON()
    {
        des_5.SetActive(true);
    }
    //
    void des_6_MethodON()
    {
        des_6.SetActive(true);
    }
    //
    void des_7_MethodON()
    {
        des_7.SetActive(true);
    }
    //
    void des_8_MethodON()
    {
        des_8.SetActive(true);
    }
    //
    void des_9_MethodON()
    {
        des_9.SetActive(true);
    }
    //
    void des_10_MethodON()
    {
        des_10.SetActive(true);
    }
    //
    void des_11_MethodON()
    {
        des_11.SetActive(true);

    }
    //
    void des_12_MethodON()
    {
        des_12.SetActive(true);
    }
    //
    void des_13_MethodON()
    {
        des_13.SetActive(true);
    }
    //
    void des_14_MethodON()
    {
        des_14.SetActive(true);
    }
    //

    void unicellular_MethodON()
    {
        unicellular.SetActive(true);
    }

    void unicellular_MethodOFF()
    {
        unicellular.SetActive(false);
    }
    //

    void onion_exp_MethodON()
    {
        onion_exp.SetActive(true);
    }

    void onion_exp_MethodOFF()
    {
        onion_exp.SetActive(false);
    }
    //

    void root_MethodON()
    {
        root.SetActive(true);
    }
    //

    //
    void boy_MethodON()
    {
        boy.SetActive(true);
    }

    void boy_MethodOFF()
    {
        boy.SetActive(false);
    }

    //
    void male1_MethodON()
    {
        male.SetActive(true);
    }

    void male1_MethodOFF()
    {
        male.SetActive(false);
    }

    //
    void male2_MethodON()
    {
        male_.SetActive(true);
    }

    void male2_MethodOFF()
    {
        male_.SetActive(false);
    }

    //













    //animations

    //
    void _vvvv_Animmethod()
    {

        anim = vvvv.GetComponent<Animator>();
        anim.Play("vvvv");
    }
    //
    void _boy_Animmethod()
    {

        anim = boy.GetComponent<Animator>();
        anim.Play("Shaking Hands 2");
    }
    //
    void _male1_Animmethod()
    {

        anim = male.GetComponent<Animator>();
        anim.Play("Walking");
    }
    //
    void _male2_Animmethod()
    {

        anim = male_.GetComponent<Animator>();
        anim.Play("Shaking Hands 3");
    }
    //
    void experimentofplantcells_Animmethod()
    {

        anim = exdperimentofplantcells.GetComponent<Animator>();
        anim.Play("experiment_of_cells_in_plant_stem");
    }
    //
    void musculemovement_Animmethod()
    {

        anim = musculemovemnet.GetComponent<Animator>();
        anim.Play("muscle movement");
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

























    //audios




    //
    void _intro_audioMethod()

    {
        myAudio.clip = intro;
        myAudio.Play();
    }
    //
    void _Tissue_audioMethod()

    {
        myAudio.clip = Tissue;
        myAudio.Play();
    }
    //
    void _Plantandanimaltissue_1_audioMethod()

    {
        myAudio.clip = Plantandanimaltissue_1;
        myAudio.Play();
    }
    //
    void _Plantandanimaltissue_2_audioMethod()

    {
        myAudio.clip = Plantandanimaltissue_2;
        myAudio.Play();
    }
    //
    void _Typesofplanttissues_audioMethod()

    {
        myAudio.clip = Typesofplanttissues;
        myAudio.Play();
    }
    //
    void _Meristematictissues_audioMethod()

    {
        myAudio.clip = Meristematictissues;
        myAudio.Play();
    }
    //
    void _Permanenttissue_audioMethod()

    {
        myAudio.clip = Permanenttissue;
        myAudio.Play();
    }
    //
    void Experiment_audioMethod()

    {
        myAudio.clip = Experiment;
        myAudio.Play();
    }
    //
    void Characteristicsofmeristematictissues_audioMethod()

    {
        myAudio.clip = Characteristicsofmeristematictissues;
        myAudio.Play();
    }
    //
    void Typesofmeristematictissues_audioMethod()

    {
        myAudio.clip = Typesofmeristematictissues;
        myAudio.Play();
    }
    //
    void Apicalmeristematictissue_audioMethod()

    {
        myAudio.clip = Apicalmeristematictissue;
        myAudio.Play();
    }
    //
    void Lateralmeristematicytissue_audioMethod()

    {
        myAudio.clip = Lateralmeristematicytissue;
        myAudio.Play();
    }
    //
    void Intercalarymeristematictissues_audioMethod()

    {
        myAudio.clip = Intercalarymeristematictissues;
        myAudio.Play();
    }
    //
    void Functionsofmeristematictissues_audioMethod()

    {
        myAudio.clip = Functionsofmeristematictissues;
        myAudio.Play();
    }
    //
    void Permanenttissues_audioMethod()

    {
        myAudio.clip = Permanenttissues;
        myAudio.Play();
    }
    //
    void Characteristicsofpermanenttissues_audioMethod()

    {
        myAudio.clip = Characteristicsofpermanenttissues;
        myAudio.Play();
    }
    //
    void Typesofpermanenttissues_audioMethod()

    {
        myAudio.clip = Typesofpermanenttissues;
        myAudio.Play();
    }
    //
    void _Simplepermanenttissues_audioMethod()

    {
        myAudio.clip = Simplepermanenttissues;
        myAudio.Play();
    }
    //
    void Typesofsimplepermanenttissues_audioMethod()

    {
        myAudio.clip = Typesofsimplepermanenttissues;
        myAudio.Play();
    }
    //
    void Parenchyma_audioMethod()

    {
        myAudio.clip = Parenchyma;
        myAudio.Play();
    }
    //
    void Collenchyma_audioMethod()

    {
        myAudio.clip = Collenchyma;
        myAudio.Play();
    }
    //
    void Sclerenchyma_audioMethod()

    {
        myAudio.clip = Sclerenchyma;
        myAudio.Play();
    }
    //
    void experimentofvariouscells_audioMethod()

    {
        myAudio.clip = experimentofvariouscells;
        myAudio.Play();
    }
    //
    void variouscellsintro_audioMethod()

    {
        myAudio.clip = variouscellsintro;
        myAudio.Play();
    }
    //
    void FunctionsofsimplepermanentTissues_audioMethod()

    {
        myAudio.clip = FunctionsofsimplepermanentTissues;
        myAudio.Play();
    }
    //
    void _Typesofcomplexpermanenttissues_audioMethod()

    {
        myAudio.clip = Typesofcomplexpermanenttissues;
        myAudio.Play();
    }
    //
    void Xylem_audioMethod()

    {
        myAudio.clip = Xylem;
        myAudio.Play();
    }
    //
    void Phloem_audioMethod()

    {
        myAudio.clip = Phloem;
        myAudio.Play();
    }
    //
    void Functionsofcomplexpermanenttissues_audioMethod()

    {
        myAudio.clip = Functionsofcomplexpermanenttissues;
        myAudio.Play();
    }
    //
    void animaltissuesintro_audioMethod()

    {
        myAudio.clip = animaltissue;
        myAudio.Play();
    }
    //
    void epithelialtissuesintro_audioMethod()

    {
        myAudio.clip = Epithelialtissuesintro;
        myAudio.Play();
    }
    //
    void epithelialtissuesau2_audioMethod()

    {
        myAudio.clip = Epithelialtissuesau2;
        myAudio.Play();
    }
    //
    void epithelialtissuesau3_audioMethod()

    {
        myAudio.clip = Epithelialtissuesau3;
        myAudio.Play();
    }
    //
    void epithelialtissuesau4_audioMethod()

    {
        myAudio.clip = Epithelialtissuesau4;
        myAudio.Play();
    }
    //
    void typesofepithelialtissues_audioMethod()

    {
        myAudio.clip = Typesofepithelialtissues;
        myAudio.Play();
    }
    //
    void Squamousepithelial_audioMethod()

    {
        myAudio.clip = squamousepithelium;
        myAudio.Play();
    }
    //
    void simplesquamousepithelialtissues_audioMethod()

    {
        myAudio.clip = Simplepermanenttissues;
        myAudio.Play();
    }
    //
    void stratifiedsquamousepithelialtissues_audioMethod()

    {
        myAudio.clip = Stratifiedsquamousepithelialtissue;
        myAudio.Play();
    }
    //
    void cuboidalepithelialtissues_audioMethod()

    {
        myAudio.clip = CuboidalEpithelialtissue;
        myAudio.Play();
    }
    //
    void glandularepithelium_audioMethod()

    {
        myAudio.clip = Glandularepithelium;
        myAudio.Play();
    }
    //
    void columnarepitheliumtisssues_audioMethod()

    {
        myAudio.clip = Columnarepitheliumtissues;
        myAudio.Play();
    }
    //
    void connectivetissue_audioMethod()

    {
        myAudio.clip = connecticetissue;
        myAudio.Play();
    }
    //
    void blood_audioMethod()

    {
        myAudio.clip = blood;
        myAudio.Play();
    }
    //
    void bone_audioMethod()

    {
        myAudio.clip = bone;
        myAudio.Play();
    }
    //
    void ligament_audioMethod()

    {
        myAudio.clip = Ligament;
        myAudio.Play();
    }
    //
    void tendons_audioMethod()

    {
        myAudio.clip = Tendons;
        myAudio.Play();
    }
    //
    void cartilage_audioMethod()

    {
        myAudio.clip = Cardiacmuscle;
        myAudio.Play();
    }
    //
    void Areolartissue_audioMethod()

    {
        myAudio.clip = Areolartissue;
        myAudio.Play();
    }
    //
    void adiposetissue_audioMethod()

    {
        myAudio.clip = Adiposetissue;
        myAudio.Play();
    }
    //
    void musculartissue_audioMethod()

    {
        myAudio.clip = Musculartissue;
        myAudio.Play();
    }
    //
    void typrsofmuscles_audioMethod()

    {
        myAudio.clip = typesofmusculartissues;
        myAudio.Play();
    }
    //
    void Voluntarymuscles_audioMethod()

    {
        myAudio.clip = Voluntarymuscles;
        myAudio.Play();
    }
    //
    void involuntarymuscles_audioMethod()

    {
        myAudio.clip = involuntarymuscles;
        myAudio.Play();
    }
    //
    void Cardiacmuscle_audioMethod()

    {
        myAudio.clip = Cardiacmuscle;
        myAudio.Play();
    }
    //
    void Nervoustissue_audioMethod()

    {
        myAudio.clip = Nervoustissue;
        myAudio.Play();
    }
    //


    void camerastop_buttonshow()
    {
        gameObject.GetComponent<Animator>().speed = 0f;

        button.SetActive(true);

    }

    public void TurnOffGameObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void TurnOnGameObject(GameObject obj)
    {
        obj.SetActive(true);
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
            animator.Play("camera anim", 0, targetNormalizedTime);
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
            case 1: Level2(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
    }

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

    public int index = 0;
    public GameObject animalTissue;
    public void ArrangedFlowChartPlants(TargetController miniGame)
    {
        index++;

        if(index == 12)
        {
            index = 0;
            miniGame.EndMiniGame();
            animalTissue.SetActive(true);
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public TargetController Question1MiniGanme;

    public void Level2()
    {
        StartCoroutine(DelayL2MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(1, 0, 2);

    }
    IEnumerator DelayL2MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        Question1MiniGanme.Output();
    }

   

    public void ArrangedFlowChartAnimals(TargetController miniGame)
    {
        index++;

        if (index == 18)
        {
            index = 0;
            miniGame.EndMiniGame();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
            Question1MiniGanme.Output();
            SaveProgress(1, 0, 2);
        }
    }

    public List<GameObject> questions = new List<GameObject>();
    public void CorrectAnswer(TargetController miniGame)
    {
        miniGame.Output();
        index++;

        if(index == 8)
        {
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }

        foreach (GameObject obj in questions)
        {
            obj.SetActive(false);
        }
    }

    public void QuestionsPopUp(GameObject obj)
    {
        StartCoroutine(Questions(obj));
    }

    IEnumerator Questions(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
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

    public void MissionPassed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
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

}

