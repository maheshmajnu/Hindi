using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_matter_in_our_Surroundings : MonoBehaviour
{

    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

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
        
    }


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

        level();
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

    public void ObjectOnHandPlacement(GameObject obj)
    {
        obj.transform.SetParent(InventoryManager.Instance.player.objectHolder);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }

    bool hasFlameThrower = false;
    public void PickedUpFlameThrower()
    {
        hasFlameThrower = true;
    }

    public Animator door1Anim;
    public GameObject lockDoor1;
    public GameObject flameThrower;
    public void OpenDoorLv1(BoxCollider col)
    {
        if(hasFlameThrower)
        {
            door1Anim.SetTrigger("Trigger");
            lockDoor1.SetActive(false);
            flameThrower.SetActive(false);
        }
        else
        {
            col.enabled = true;
        }
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void StepComp()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject stone;
    public GameObject table;
    public GameObject rabbit;
    public GameObject wolf;
    public GameObject wooden_block;
    public GameObject phone;
    public GameObject duster;
    public GameObject pencil;
    public GameObject oil;
    public GameObject jusice;
    public GameObject cock;
    public GameObject milk;
    public GameObject water_2;
    public GameObject ice_1;
    public GameObject ice_2;
    public GameObject ice_3;
    public GameObject airtight;
    public GameObject lpg;
    public GameObject cng;
    public GameObject aryabatta;
    public GameObject alberteinsten;
    public GameObject salt;
    public GameObject sponge;
    public GameObject beaker;
    public GameObject beaker1;
    public GameObject beaker2;
    public GameObject ice;
    public GameObject ice1;
    public GameObject ice2;
    public GameObject ice3;
    public GameObject ice4;
    public GameObject ice5;
    public GameObject ice6;
    public GameObject pan;
    public GameObject car;
    public GameObject food;
    public GameObject balloon;
    public GameObject salt_plate;
    public GameObject meltinf_points;
    public GameObject themometor_stand;
    public GameObject dry_ice;
    public GameObject cloth;
    public GameObject particles;
    public GameObject sublimation;
    public GameObject test_tube;
    public GameObject china_dish;
    public GameObject air_cooler;
    public GameObject mat;
    public GameObject sweat;
    public GameObject glass;
    public GameObject campfire;

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
    public GameObject des_1;
    public GameObject des_2;
    public GameObject des_3;
    public GameObject des_4;
    public GameObject des_5;













    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject kid_anim;
    public GameObject tea_anim;
    public GameObject beakerspoon_anim;
    public GameObject liquide_cly_aniam;
    public GameObject oil_aniam;
    public GameObject oil_aniam_1;
    public GameObject oil_aniam_2;
    public GameObject oil_aniam_3;
    public GameObject gas_aniam;
    public GameObject syringe_anim;
    public GameObject rubber_anim;
    public GameObject liquide_anim;
    public GameObject particles_anim;
    public GameObject gas_anim;
    public GameObject ice_anim;
    public GameObject melting_anim;
    public GameObject particles1_anim;
    public GameObject varparization_anim;
    public GameObject pressure_anim;
    public GameObject kineticenergy_anim;
    public GameObject liquidekinetice_anim;
    public GameObject escap_anim;
    public GameObject fan_anim;
    public GameObject exercise_anim;
    public GameObject kid_anim1;





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
    public AudioClip voice25_1;
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
    void title_13_MethodON()
    {
        title_13.SetActive(true);
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










    // Line on/off

    //
    void arybatta_MethodOFF()
    {
        aryabatta.SetActive(false);
    }
    //
    void arybatta_MethodON()
    {
        aryabatta.SetActive(true);
    }
    //
    void alberteinsten_MethodOFF()
    {
        alberteinsten.SetActive(false);
    }
    //
    void alberteinsten_MethodON()
    {
        alberteinsten.SetActive(true);
    }
    //
    void table_MethodOFF()
    {
        table.SetActive(false);
    }
    //
    void table_MethodON()
    {
        table.SetActive(true);
    }
    //
    void stone_MethodOFF()
    {
        stone.SetActive(false);
    }
    //
    void stone_MethodON()
    {
        stone.SetActive(true);
    }
    //

    void rabbit_MethodOFF()
    {
        rabbit.SetActive(false);
    }
    //
    void rabbit_MethodON()
    {
        rabbit.SetActive(true);
    }
    //

    void wolf_MethodOFF()
    {
        wolf.SetActive(false);
    }
    //
    void wolf_MethodON()
    {
        wolf.SetActive(true);
    }
    //
    void wooden_block_MethodOFF()
    {
        wooden_block.SetActive(false);
    }
    //
    void wooden_block_MethodON()
    {
        wooden_block.SetActive(true);
    }
    //

    void phone_MethodOFF()
    {
        phone.SetActive(false);
    }
    //
    void phone_MethodON()
    {
        phone.SetActive(true);
    }
    //
    void duster_MethodOFF()
    {
        duster.SetActive(false);
    }
    //
    void duster_MethodON()
    {
        duster.SetActive(true);
    }
    //
    void pencil_MethodOFF()
    {
        pencil.SetActive(false);
    }
    //
    void pencil_MethodON()
    {
        pencil.SetActive(true);
    }
    //
    void oil_MethodOFF()
    {
        oil.SetActive(false);
    }
    //
    void oil_MethodON()
    {
        oil.SetActive(true);
    }
    //
    void jusice_MethodOFF()
    {
        jusice.SetActive(false);
    }
    //
    void jusice_MethodON()
    {
        jusice.SetActive(true);
    }
    //
    void cock_MethodOFF()
    {
        cock.SetActive(false);
    }
    //
    void cock_MethodON()
    {
        cock.SetActive(true);
    }
    //
    void milk_MethodOFF()
    {
        milk.SetActive(false);
    }
    //
    void milk_MethodON()
    {
        milk.SetActive(true);
    }
    //
    void water2_MethodOFF()
    {
        water_2.SetActive(false);
    }
    //
    void water2_MethodON()
    {
        water_2.SetActive(true);
    }
    //
    void ice__MethodOFF()
    {
        ice.SetActive(false);
    }
    //
    void ice_MethodON()
    {
        ice.SetActive(true);
    }
    //
    void ice1_MethodOFF()
    {
        ice1.SetActive(false);
    }
    //
    void ice1_MethodON()
    {
        ice1.SetActive(true);
    }
    //
    void ice2_MethodOFF()
    {
        ice2.SetActive(false);
    }
    //
    void ice2_MethodON()
    {
        ice2.SetActive(true);
    }
    //
    void ice3_MethodOFF()
    {
        ice3.SetActive(false);
    }
    //
    void ice3_MethodON()
    {
        ice3.SetActive(true);
    }
    //
    void ice4_MethodOFF()
    {
        ice4.SetActive(false);
    }
    //
    void ice4_MethodON()
    {
        ice4.SetActive(true);
    }
    //
    void ice5_MethodOFF()
    {
        ice5.SetActive(false);
    }
    //
    void ice5_MethodON()
    {
        ice5.SetActive(true);
    }
    //
    void ice6_MethodOFF()
    {
        ice6.SetActive(false);
    }
    //
    void ice6_MethodON()
    {
        ice6.SetActive(true);
    }
    //
    void car_MethodOFF()
    {
        car.SetActive(false);
    }
    //
    void car_MethodON()
    {
        car.SetActive(true);
    }
    //
    void food_MethodOFF()
    {
        food.SetActive(false);
    }
    //
    void food_MethodON()
    {
        food.SetActive(true);
    }
    //
    void balloon_MethodOFF()
    {
        balloon.SetActive(false);
    }
    //
    void balloon_MethodON()
    {
        balloon.SetActive(true);
    }
    //
    void salt_plate_MethodOFF()
    {
        salt_plate.SetActive(false);
    }
    //
    void salt_plate_MethodON()
    {
        salt_plate.SetActive(true);
    }
    //
    void melting_point_MethodOFF()
    {
        meltinf_points.SetActive(false);
    }
    //
    void melting_point_MethodON()
    {
        meltinf_points.SetActive(true);
    }
    //
    void themometor_stand_MethodOFF()
    {
        themometor_stand.SetActive(false);
    }
    //
    void themometor_stand_MethodON()
    {
        themometor_stand.SetActive(true);
    }
    //
    void dry_ice_MethodOFF()
    {
        dry_ice.SetActive(false);
    }
    //
    void dry_ice_MethodON()
    {
        dry_ice.SetActive(true);
    }
    //
    void cloth_MethodOFF()
    {
        cloth.SetActive(false);
    }
    //
    void cloth_MethodON()
    {
        cloth.SetActive(true);
    }
    //
    void particles_MethodOFF()
    {
        particles.SetActive(false);
    }
    //
    void particles_MethodON()
    {
        particles.SetActive(true);
    }
    //
    void test_tube_MethodOFF()
    {
        test_tube.SetActive(false);
    }
    //
    void test_tube_MethodON()
    {
        test_tube.SetActive(true);
    }
    //
    void sublimation_MethodOFF()
    {
        sublimation.SetActive(false);
    }
    //
    void sublimation_MethodON()
    {
        sublimation.SetActive(true);
    }
    //
    void chia_dish_MethodOFF()
    {
        china_dish.SetActive(false);
    }
    //
    void china_dish_MethodON()
    {
        china_dish.SetActive(true);
    }
    //
    void air_cooler_MethodOFF()
    {
        air_cooler.SetActive(false);
    }
    //
    void air_cooler_MethodON()
    {
       air_cooler.SetActive(true);
    }
    //
    void mat_MethodOFF()
    {
        mat.SetActive(false);
    }
    //
    void mat_MethodON()
    {
        mat.SetActive(true);
    }
    //
    void sweat_MethodOFF()
    {
        sweat.SetActive(false);
    }
    //
    void sweat_MethodON()
    {
        sweat.SetActive(true);
    }
    //
    void glass_MethodOFF()
    {
        glass.SetActive(false);
    }
    //
    void glass_MethodON()
    {
        glass.SetActive(true);
    }
    //
    void camp_fire_MethodOFF()
    {
        campfire.SetActive(false);
    }
    //
    void camp_fire_MethodON()
    {
        campfire.SetActive(true);
    }
    //
   
























    // Animations

    void kid_aimAnimmethod()
    {

        anim = kid_anim.GetComponent<Animator>();
        anim.Play("kid anim");
    }
    //
    void tea_animAnimmethod()
    {

        anim = tea_anim.GetComponent<Animator>();
        anim.Play("tea anim");
    }
    //
    void beakeer_spoonAnimmethod()
    {

        anim = beakerspoon_anim.GetComponent<Animator>();
        anim.Play("beakerspoon anim");
    }
    //
    void rubberAnimmethod()
    {

        anim = rubber_anim.GetComponent<Animator>();
        anim.Play("rubber anim");
    }
    //
    void liquide_animAnimmethod()
    {

        anim = liquide_anim.GetComponent<Animator>();
        anim.Play("liquide anim");
    }
    //
    void liquide_cly_animAnimmethod()
    {

        anim = liquide_cly_aniam.GetComponent<Animator>();
        anim.Play("liquide cly anim");
    }
    //
    void oilAnimmethod()
    {

        anim = oil_aniam.GetComponent<Animator>();
        anim.Play("oil anim");
    }
    //
    void oil_anim1Animmethod()
    {

        anim = oil_aniam_1.GetComponent<Animator>();
        anim.Play("oil anim 1");
    }
    //
    void oil_anim2Animmethod()
    {

        anim = oil_aniam_2.GetComponent<Animator>();
        anim.Play("oil anim 2");
    }
    //
    void oil_anim3Animmethod()
    {

        anim = oil_aniam_3.GetComponent<Animator>();
        anim.Play("oil anim 3");
    }
    //
    void particlesAnimmethod()
    {

        anim = particles_anim.GetComponent<Animator>();
        anim.Play("particles anim");
    }
    //
    void gas_animAnimmethod()
    {

        anim = gas_anim.GetComponent<Animator>();
        anim.Play("gas anim");
    }
    //
    void syring_animAnimmethod()
    {

        anim = syringe_anim.GetComponent<Animator>();
        anim.Play("syring anim");
    }
    //
    void ice_animAnimmethod()
    {

        anim = ice_anim.GetComponent<Animator>();
        anim.Play("ice anim");
    }
    //
    void melting_animAnimmethod()
    {

        anim = melting_anim.GetComponent<Animator>();
        anim.Play("melting anim");
    }
    //
    void particles1Animmethod()

    {

        anim = particles1_anim.GetComponent<Animator>();
        anim.Play("particles1 anim");
    }
    //
    void varparization_animAnimmethod()
    {

        anim = varparization_anim.GetComponent<Animator>();
        anim.Play("varpartization anim");
    }
    //
    void pressureAnimmethod()
    {

        anim = pressure_anim.GetComponent<Animator>();
        anim.Play("pressure anim");
    }
    //
    void kineticAnimmethod()
    {

        anim = kineticenergy_anim.GetComponent<Animator>();
        anim.Play("kineticenergy anim");
    }
    //
    void liquidekineticAnimmethod()
    {

        anim = liquidekinetice_anim.GetComponent<Animator>();
        anim.Play("liquidekinetice anim");
    }
    //
    void escape_animAnimmethod()
    {

        anim = escap_anim.GetComponent<Animator>();
        anim.Play("escap anim");
    }
    //
    void fan_animAnimmethod()
    {

        anim = fan_anim.GetComponent<Animator>();
        anim.Play("fan anim");
    }
    //
    void exercise_animAnimmethod()
    {

        anim = exercise_anim.GetComponent<Animator>();
        anim.Play("exercise anim");
    }
    //
    void kid1_animAnimmethod()
    {

        anim = kid_anim1.GetComponent<Animator>();
        anim.Play("kid anim 1");
    }
    //









































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
    void _voice25_1_audioMethod()
    {
        myAudio.clip = voice25_1;
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
























































}



