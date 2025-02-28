using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_metals_and_non_metals : MonoBehaviour
{
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
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
            case 0: level1(); break;
            case 1: Level6(); break;
            default: level1(); break;
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

        InitializeFromCheckpoint();
        level1();
    }

    public Camera cam;
    public LayerMask layerMask;
    private bool canChoose = true;
    public int index;
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
        yield return new WaitForSeconds(2f);
        obj.SetActive(true);
    }

    public void TurnOffGOWithDelay(GameObject obj)
    {
        StartCoroutine(ObjectTurnOffDelay(obj));
    }


    IEnumerator ObjectTurnOffDelay(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        obj.SetActive(false);
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


    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
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

    public List<GameObject> level1ObjectsTurnOff = new List<GameObject>();
    public List<GameObject> level1ObjectsTurnOn = new List<GameObject>();
    public TargetController level1MiniGame;
    public void Level1MiniGameStart()
    {
        StartCoroutine(Level1Delay());
    }

    private IEnumerator Level1Delay()
    {
        yield return new WaitForSeconds(2);

        foreach (GameObject obj in level1ObjectsTurnOff)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in level1ObjectsTurnOn)
        { obj.SetActive(true); }
        yield return new WaitForSeconds(2);
        level1MiniGame.EndMiniGame();
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void PlayAnimationDelay(Animator anim)
    {
        StartCoroutine(AnimationDelay(anim));
    }

    IEnumerator AnimationDelay(Animator anim)
    {
        yield return new WaitForSeconds(2);
        anim.SetTrigger("Trigger");
    }

    private void EndGameDelay()
    {
        miniGame.EndMiniGame();
    }

    public TargetController miniGame;

    public void level1()
    {
        miniGame.Output();
        Invoke("EndGameDelay", 1f);

    }

    public GameObject lv1;

    public GameObject lv2;
    public Transform Spawnpoint2;

    public void Level2()
    {
        StartCoroutine(DelayLevel2());
    }

    IEnumerator DelayLevel2()
    {
        yield return new WaitForSeconds(2);
        lv2.SetActive(true);
        
    }


    public void Level2game()
    {
        index++;


        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Invoke("Level3", 2);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public GameObject lv3;


    public void Level3()
    {
        lv2.SetActive(false);
        lv3.SetActive(true);

    }


    public void Level3game()
    {
        index++;


        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Invoke("Level4", 2);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public GameObject lv4;


    public void Level4()
    {
        lv3.SetActive(false);
        lv4.SetActive(true);

    }


    public void Level4game()
    {
        index++;


        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            Invoke("Level6", 2);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }


    public GameObject lv6;


    public void Level6()
    {
        lv4.SetActive(false);
        lv6.SetActive(true);
        StartCoroutine(DelayLevel6());

        SaveProgress(1, 0, 4);

    }

    IEnumerator DelayLevel6()
    {
        yield return new WaitForSeconds(2);
        
        
        InventoryManager.Instance.player.ChangePosition(Spawnpoint2);
        //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }


    public void Level6game()
    {
        index++;


        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }
    public void Level8()
    {
        StartCoroutine(DelayLv8MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv8MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv8MiniGame.Output();
    }

    public TargetController lv8MiniGame;


    //public void Level18()
    //{
    //    InventoryManager.Instance.inventryStatic.SetActive(false);
    //    InventoryManager.Instance.player.ChangePosition(SpawnPoint18);
    //    InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    //}




    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject aluminum;
    public GameObject copper;
    public GameObject gold;
    public GameObject hammer;
    public GameObject hammer1;
    public GameObject mercury;
    public GameObject mercury_Cinnabar;
    public GameObject nail;
    public GameObject nail1;
    public GameObject ore;
    public GameObject perdic_table;
    public GameObject reactivity_series;
    public GameObject silver;
    public GameObject wire;
    public GameObject bell;
    public GameObject iron_wire;
    public GameObject wire_1;
    public GameObject gallium;
    public GameObject Aluminium_oxide;
    public GameObject calcination;
    public GameObject compounds;
    public GameObject copper_oxide;
    public GameObject heat;
    public GameObject metal_oxide;
    public GameObject roasting;
    public GameObject Sodium_cation;
    public GameObject thermite_reaction;
    public GameObject Oxides;
    public GameObject Alkalis;
    public GameObject POTASSIUM_an;
    public GameObject kerosene_oil;
    public GameObject iron;
    public GameObject magnesium_oxidation;
    public GameObject copper_oxides;
    public GameObject metal_oxide1;
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


    public GameObject metals_atoms;
    public GameObject non_metals_atoms;



    private Animator animator;




   


    //private Animator animator;

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





























    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject Aluminium_atom_anim;
    public GameObject Calcium_atom_anim;
    public GameObject chlorine_anim;
    public GameObject hammer_anim;
    public GameObject iron_atom_anim;
    public GameObject Magnesium_atom_anim;
    public GameObject Metals_react_with_Solutions_of_other_Metal_Salts_anim;
    public GameObject Pottasium_atom_anim;
    public GameObject Sodium_atom_anim;
    public GameObject POTASSIUM_anim;
    public GameObject iron_anim;
    public GameObject magnesium_anim;
    public GameObject Metals_Acids;
    public GameObject Solutions_acids;
    public GameObject metals_atoms_anim;
    public GameObject non_metals_atoms_anim;





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
    public AudioClip voice100;
    public AudioClip voice101;
    public AudioClip voice102;
    public AudioClip voice103;
    public AudioClip voice104;
    public AudioClip voice105;
    public AudioClip voice106;
    public AudioClip voice107;
    public AudioClip voice108;
    public AudioClip voice109;
    public AudioClip voice110;
    public AudioClip voice111;
    public AudioClip voice112;
    public AudioClip voice113;
    public AudioClip voice114;
    public AudioClip voice115;
    public AudioClip voice116;
    public AudioClip voice117;
    public AudioClip voice118;
    public AudioClip voice119;
    public AudioClip voice120;
    public AudioClip voice121;
    public AudioClip voice122;
    public AudioClip voice123;
    public AudioClip voice124;
    public AudioClip voice125;
    public AudioClip voice126;
    public AudioClip voice127;
    public AudioClip voice128;
    public AudioClip voice129;
    public AudioClip voice130;
    public AudioClip voice131;
    public AudioClip voice132;
    public AudioClip voice133;
    public AudioClip voice134;
    public AudioClip voice135;
    public AudioClip voice136;
    public AudioClip voice137;








    // Line on/off

    //
    void aluminum_MethodOFF()
    {
        aluminum.SetActive(false);
    }
    //
    void aluminum_MethodON()
    {
        aluminum.SetActive(true);
    }
    //
    void copper_MethodOFF()
    {
        copper.SetActive(false);
    }
    //
    void copper_MethodON()
    {
        copper.SetActive(true);
    }
    //
    void gold_MethodOFF()
    {
        gold.SetActive(false);
    }
    //
    void gold_MethodON()
    {
        gold.SetActive(true);
    }
    //
    void hammer_MethodOFF()
    {
        hammer.SetActive(false);
    }
    //
    void hammer_MethodON()
    {
        hammer.SetActive(true);
    }
    //
    void hammer1_MethodOFF()
    {
        hammer1.SetActive(false);
    }
    //
    void hammer1_MethodON()
    {
        hammer1.SetActive(true);
    }
    //
    void mercury_MethodOFF()
    {
        mercury.SetActive(false);
    }
    //
    void mercury_MethodON()
    {
        mercury.SetActive(true);
    }
    //
    void mercury_Cinnabar_MethodOFF()
    {
        mercury_Cinnabar.SetActive(false);
    }
    //
    void mercury_Cinnabar_MethodON()
    {
        mercury_Cinnabar.SetActive(true);
    }
    //
    void nail_MethodOFF()
    {
        nail.SetActive(false);
    }
    //
    void nail_MethodON()
    {
        nail.SetActive(true);
    }
    //
    void nail1_MethodOFF()
    {
        nail1.SetActive(false);
    }
    //
    void nail1_MethodON()
    {
        nail1.SetActive(true);
    }
    //
    void perdic_table_MethodOFF()
    {
        perdic_table.SetActive(false);
    }
    //
    void perdic_table_MethodON()
    {
        perdic_table.SetActive(true);
    }
    //
    void ore_MethodOFF()
    {
        ore.SetActive(false);
    }
    //
    void ore_MethodON()
    {
        ore.SetActive(true);
    }
    //
    void reactivity_series_MethodOFF()
    {
        reactivity_series.SetActive(false);
    }
    //
    void reactivity_series_MethodON()
    {
        reactivity_series.SetActive(true);
    }
    //
    void silver_MethodOFF()
    {
        silver.SetActive(false);
    }
    //
    void silver_MethodON()
    {
        silver.SetActive(true);
    }
    //
    void wire_MethodOFF()
    {
        wire.SetActive(false);
    }
    //
    void wire_MethodON()
    {
        wire.SetActive(true);
    }
    //
    void bell_MethodOFF()
    {
        bell.SetActive(false);
    }
    //
    void bell_MethodON()
    {
        bell.SetActive(true);
    }
    //
    void iron_wire_MethodOFF()
    {
        iron_wire.SetActive(false);
    }
    //
    void iron_wire_MethodON()
    {
        iron_wire.SetActive(true);
    }
    //
    void wire_1_MethodOFF()
    {
        wire_1.SetActive(false);
    }
    //
    void wire_1_MethodON()
    {
        wire_1.SetActive(true);
    }
    //
    void gallium_MethodOFF()
    {
        gallium.SetActive(false);
    }
    //
    void gallium_MethodON()
    {
        gallium.SetActive(true);
    }
    //
    void Aluminium_oxide_MethodOFF()
    {
        Aluminium_oxide.SetActive(false);
    }
    //
    void Aluminium_oxide_MethodON()
    {
        Aluminium_oxide.SetActive(true);
    }
    //
    void calcination_MethodOFF()
    {
        calcination.SetActive(false);
    }
    //
    void calcination_MethodON()
    {
        calcination.SetActive(true);
    }
    //
    void compounds_MethodOFF()
    {
        compounds.SetActive(false);
    }
    //
    void compounds_MethodON()
    {
        compounds.SetActive(true);
    }
    //
    void copper_oxide_MethodOFF()
    {
        copper_oxide.SetActive(false);
    }
    //
    void copper_oxide_MethodON()
    {
        copper_oxide.SetActive(true);
    }
    //
    void heat_MethodOFF()
    {
        heat.SetActive(false);
    }
    //
    void heat_MethodON()
    {
        heat.SetActive(true);
    }
    //
    void metal_oxide_MethodOFF()
    {
        metal_oxide.SetActive(false);
    }
    //
    void metal_oxide_MethodON()
    {
        metal_oxide.SetActive(true);
    }
    //
    void roasting_MethodOFF()
    {
        roasting.SetActive(false);
    }
    //
    void roasting_MethodON()
    {
        roasting.SetActive(true);
    }
    //
    void Sodium_cation_MethodOFF()
    {
        Sodium_cation.SetActive(false);
    }
    //
    void Sodium_cation_MethodON()
    {
        Sodium_cation.SetActive(true);
    }
    //
    void thermite_reaction_MethodOFF()
    {
        thermite_reaction.SetActive(false);
    }
    //
    void thermite_reaction_MethodON()
    {
        thermite_reaction.SetActive(true);
    }
    //
    void Oxides_MethodOFF()
    {
        Oxides.SetActive(false);
    }
    //
    void Oxides_MethodON()
    {
        Oxides.SetActive(true);
    }
    //
    void Alkalis_MethodOFF()
    {
        Alkalis.SetActive(false);
    }
    //
    void Alkalis_MethodON()
    {
        Alkalis.SetActive(true);
    }
    //
    void POTASSIUM_an_MethodOFF()
    {
        POTASSIUM_an.SetActive(false);
    }
    //
    void POTASSIUM_an_MethodON()
    {
        POTASSIUM_an.SetActive(true);
    }
    //
    void kerosene_oil_MethodOFF()
    {
        kerosene_oil.SetActive(false);
    }
    //
    void kerosene_oil_MethodON()
    {
        kerosene_oil.SetActive(true);
    }
    //
    void iron_MethodOFF()
    {
        iron.SetActive(false);
    }
    //
    void iron_MethodON()
    {
        iron.SetActive(true);
    }
    //
    void magnesium_oxidation_MethodOFF()
    {
        magnesium_oxidation.SetActive(false);
    }
    //
    void magnesium_oxidation_MethodON()
    {
        magnesium_oxidation.SetActive(true);
    }
    //
    void copper_oxides_MethodOFF()
    {
        copper_oxides.SetActive(false);
    }
    //
    void copper_oxides_MethodON()
    {
        copper_oxides.SetActive(true);
    }
    //
    void metal_oxide1_MethodOFF()
    {
        metal_oxide1.SetActive(false);
    }
    //
    void metal_oxide1_MethodON()
    {
        metal_oxide1.SetActive(true);
    }
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
    void T31_MethodON()
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



























    void metals_atoms_MethodOFF()
    {
        metals_atoms.SetActive(false);
    } 
    //
    void metals_atoms_MethodON()
    {
        metals_atoms.SetActive(true);
    }
    //

    void non_metals_atoms_MethodOFF()
    {
        non_metals_atoms.SetActive(false);
    }
    //
    void non_metals_atoms_MethodON()
    {
        non_metals_atoms.SetActive(true);
    }
    //









































    // Animations

    void Aluminium_atom_animAnimmethod()
    {

        anim = Aluminium_atom_anim.GetComponent<Animator>();
        anim.Play("Aluminium_atom_anim");
    }
    //
    void Calcium_atom_animAnimmethod()
    {

        anim = Calcium_atom_anim.GetComponent<Animator>();
        anim.Play("Calcium_atom_anim");
    }
    //
    void chlorine_animAnimmethod()
    {

        anim = chlorine_anim.GetComponent<Animator>();
        anim.Play("chlorine_anim");
    }
    //
    void hammer_animAnimmethod()
    {

        anim = hammer_anim.GetComponent<Animator>();
        anim.Play("hammer_anim");
    }
    //
    void iron_atom_animAnimmethod()
    {

        anim = iron_atom_anim.GetComponent<Animator>();
        anim.Play("iron_atom_anim");
    }
    //
    void Magnesium_atom_animAnimmethod()
    {

        anim = Magnesium_atom_anim.GetComponent<Animator>();
        anim.Play("Magnesium_atom_anim");
    }
    //
    void Metals_react_with_Solutions_of_other_Metal_Salts_animAnimmethod()
    {

        anim = Metals_react_with_Solutions_of_other_Metal_Salts_anim.GetComponent<Animator>();
        anim.Play("Metals_react_with_Solutions_of_other_Metal_Salts_anim");
    }
    //
    void Pottasium_atom_animAnimmethod()
    {

        anim = Pottasium_atom_anim.GetComponent<Animator>();
        anim.Play("Pottasium_atom_anim");
    }
    //
    void Sodium_atom_animAnimmethod()
    {

        anim = Sodium_atom_anim.GetComponent<Animator>();
        anim.Play("Sodium_atom_anim");
    }
    //
    void POTASSIUM_animAnimmethod()
    {

        anim = POTASSIUM_anim.GetComponent<Animator>();
        anim.Play("POTASSIUM_anim");
    }
    //
    void iron_animAnimmethod()
    {

        anim = iron_anim.GetComponent<Animator>();
        anim.Play("iron_anim");
    }
    //
    void magnesium_animAnimmethod()
    {

        anim = magnesium_anim.GetComponent<Animator>();
        anim.Play("magnesium_anim");
    }
    //
    void Metals_AcidsAnimmethod()
    {

        anim = Metals_Acids.GetComponent<Animator>();
        anim.Play("Metals_Acids");
    }
    //
    void Solutions_acidsAnimmethod()
    {

        anim = Solutions_acids.GetComponent<Animator>();
        anim.Play("Solutions_acids");
    }
    //
    void metals_atoms_animAnimmethod()
    {

        anim = metals_atoms_anim.GetComponent<Animator>();
        anim.Play("metals_atoms_anim");
    }
    //
    void nom_metals_atoms_animAnimmethod()
    {

        anim = non_metals_atoms_anim.GetComponent<Animator>();
        anim.Play("non_metals_atoms_anim");
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
    void _voice100_audioMethod()
    {
        myAudio.clip = voice100;
        myAudio.Play();
    }
    //
    void _voice101_audioMethod()
    {
        myAudio.clip = voice101;
        myAudio.Play();
    }
    //
    void _voice102_audioMethod()
    {
        myAudio.clip = voice102;
        myAudio.Play();
    }
    //
    void _voice103_audioMethod()
    {
        myAudio.clip = voice103;
        myAudio.Play();
    }
    //
    void _voice104_audioMethod()
    {
        myAudio.clip = voice104;
        myAudio.Play();
    }
    //
    void _voice105_audioMethod()
    {
        myAudio.clip = voice105;
        myAudio.Play();
    }
    //
    void _voice106_audioMethod()
    {
        myAudio.clip = voice106;
        myAudio.Play();
    }
    //
    void _voice107_audioMethod()
    {
        myAudio.clip = voice107;
        myAudio.Play();
    }
    //
    void _voice108_audioMethod()
    {
        myAudio.clip = voice108;
        myAudio.Play();
    }
    //
    void _voice109_audioMethod()
    {
        myAudio.clip = voice109;
        myAudio.Play();
    }
    //
    void _voice110_audioMethod()
    {
        myAudio.clip = voice110;
        myAudio.Play();
    }
    //
    void _voice111_audioMethod()
    {
        myAudio.clip = voice111;
        myAudio.Play();
    }
    //
    void _voice112_audioMethod()
    {
        myAudio.clip = voice112;
        myAudio.Play();
    }
    //
    void _voice113_audioMethod()
    {
        myAudio.clip = voice113;
        myAudio.Play();
    }
    //
    void _voice114_audioMethod()
    {
        myAudio.clip = voice114;
        myAudio.Play();
    }
    //
    void _voice115_audioMethod()
    {
        myAudio.clip = voice115;
        myAudio.Play();
    }
    //
    void _voice116_audioMethod()
    {
        myAudio.clip = voice116;
        myAudio.Play();
    }
    //
    void _voice117_audioMethod()
    {
        myAudio.clip = voice117;
        myAudio.Play();
    }
    //
    void _voice118_audioMethod()
    {
        myAudio.clip = voice118;
        myAudio.Play();
    }
    //
    void _voice119_audioMethod()
    {
        myAudio.clip = voice119;
        myAudio.Play();
    }
    //
    void _voice120_audioMethod()
    {
        myAudio.clip = voice120;
        myAudio.Play();
    }
    //
    void _voice121_audioMethod()
    {
        myAudio.clip = voice121;
        myAudio.Play();
    }
    //
    void _voice122_audioMethod()
    {
        myAudio.clip = voice122;
        myAudio.Play();
    }
    //
    void _voice123_audioMethod()
    {
        myAudio.clip = voice123;
        myAudio.Play();
    }
    //
    void _voice124_audioMethod()
    {
        myAudio.clip = voice124;
        myAudio.Play();
    }
    //
    void _voice125_audioMethod()
    {
        myAudio.clip = voice125;
        myAudio.Play();
    }
    //
    void _voice126_audioMethod()
    {
        myAudio.clip = voice126;
        myAudio.Play();
    }
    //
    void _voice127_audioMethod()
    {
        myAudio.clip = voice127;
        myAudio.Play();
    }
    //
    void _voice128_audioMethod()
    {
        myAudio.clip = voice128;
        myAudio.Play();
    }
    //
    void _voice129_audioMethod()
    {
        myAudio.clip = voice129;
        myAudio.Play();
    }
    //
    void _voice130_audioMethod()
    {
        myAudio.clip = voice130;
        myAudio.Play();
    }
    //
    void _voice131_audioMethod()
    {
        myAudio.clip = voice131;
        myAudio.Play();
    }
    //
    void _voice132_audioMethod()
    {
        myAudio.clip = voice132;
        myAudio.Play();
    }
    //
    void _voice133_audioMethod()
    {
        myAudio.clip = voice133;
        myAudio.Play();
    }
    //
    void _voice134_audioMethod()
    {
        myAudio.clip = voice134;
        myAudio.Play();
    }
    //
    void _voice135_audioMethod()
    {
        myAudio.clip = voice135;
        myAudio.Play();
    }
    //
    void _voice136_audioMethod()
    {
        myAudio.clip = voice136;
        myAudio.Play();
    }
    //
    void _voice137_audioMethod()
    {
        myAudio.clip = voice137;
        myAudio.Play();
    }
    //


    
}
