using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_cells9 : MonoBehaviour
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
            animator.Play("Cam animation", 0, targetNormalizedTime);
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
    public GameObject lv5;

    public void Level5()
    {
        lv5.SetActive(true);
        StartCoroutine(DelayLv5MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    IEnumerator DelayLv5MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv5MiniGame.Output();
    }

    public TargetController lv5MiniGame;
    public void Savepoint1()
    {
        SaveProgress(1, 0, 5);
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
        level();
    }

    public Camera cam;
    public LayerMask layerMask;
    public List<TargetController> miniGames = new List<TargetController>();
    public List<GameObject> questions = new List<GameObject>();
    private int miniGameIndex = 0;
    private bool canChoose = true;

    private void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }


        if (Input.GetMouseButtonDown(0) && contMinigameStarted && canChoose)
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
                        miniGames[miniGameIndex].defaultEvent.Invoke();
                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public List<GameObject> shapeObjs = new List<GameObject>();
    public List<GameObject> microScopes = new List<GameObject>();
    private int shapeIndex = 0;

    public void Lv2MicroScopeSelect(GameObject obj)
    {
        if(obj.name == "Correct")
        {
            Lev2MicroScopeCorrect();
        }
        else
        {
            MissionFailed();
        }
    }

    public void Lev2MicroScopeCorrect()
    {
        foreach (GameObject obj in microScopes)
        {
            obj.name = "a";
        }

        shapeIndex++;

        if (shapeIndex == 3)
        {
            shapeIndex = 0;
            Invoke("Level3Start",2);
            StepComplete();
            return;
        }

        microScopes[shapeIndex].name = "Correct";
        StepComplete();
    }

    public void Lev3ShapeCorrect()
    {
        foreach (GameObject obj in shapeObjs)
        {
            obj.name = "a";
        }

        shapeIndex++;

        if (shapeIndex == 3)
        {
            shapeIndex = 0;
            MoveToNextMiniGame();
            StepComplete();
            return;
        }

        shapeObjs[shapeIndex].name = "Correct";
        StepComplete();
    }

    private bool contMinigameStarted = false;

    public void Level3Start()
    {
        contMinigameStarted=true;
        miniGames[miniGameIndex].Output();
    }

    public void TurnOnGOWithDelay(GameObject obj)
    {
        StartCoroutine(ObjectTurnOnDelay(obj));
    }

    IEnumerator ObjectTurnOnDelay(GameObject obj)
    {
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

    public void PlayAnimBool(Animator anim)
    {
        anim.SetBool("Bool", true);
    }

    public void AnimBack(Animator anim)
    {
        foreach (GameObject obj in questions)
        {
            obj.SetActive(false);
        }

        anim.SetBool("Bool", false);
    }

    
    public void TurnOnMeshRenderer(MeshRenderer mr)
    {
        shapeIndex++;
        mr.enabled = true;

        if(shapeIndex == 6)
        {
            MoveToNextMiniGame();
            StepComplete();
        }
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

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public Light myLight;

    public GameObject bull;
    public GameObject dog;
    public GameObject kid;
    public GameObject firstmicro;
    public GameObject compoundmicro;
    public GameObject electronicmicro;
    public GameObject Marcello;
    public GameObject roberthooke;
    public GameObject roberthookecells;
    public GameObject Levin;
    public GameObject robertbrown;
    public GameObject robertbrowncells;
    public GameObject Purkinje;
    public GameObject twoguys;
    public GameObject rudolf;
    public GameObject smolslice;
    public GameObject dropper;
    public GameObject needle;
    public GameObject drip;
    public GameObject glassslide;
    public GameObject glassslidetop;
    public GameObject dome;
    public GameObject sps;
    public GameObject pill;
    public GameObject inouttext;
    public GameObject cellsaptext;
    public GameObject tonoplasttext;
    public GameObject plastidstext;

//


    public GameObject cellsdiscoveryT;
    public GameObject compoundmicroscopeT;
    public GameObject typesizeT;
    public GameObject celltypesT;
    public GameObject unicellularT;
    public GameObject multicellularT;
    public GameObject cellshapeT;
    public GameObject labordivisionT;
    public GameObject typeofcellnucleasT;
    public GameObject structuralunitT;
    public GameObject plasmamembraneT;
    public GameObject cellwallT;
    public GameObject nucleasT;
    public GameObject cytoplasmT;
    public GameObject CellorganellesT;
    public GameObject EndoplasmicreticulumT;
    public GameObject ribosomesT;
    public GameObject golgiapparatusT;
    public GameObject mitochondriaT;
    public GameObject LysosomesT;
    public GameObject VacuoleT;
    public GameObject PlastidsT;
    public GameObject GranualsT;
    public GameObject celldivisionT;
    public GameObject MitosisT;
    public GameObject MeiosisT;
    public GameObject IntroductionT;


    //

    public GameObject cellsD;
    public GameObject MarcelloD;
    public GameObject roberthookeD;
    public GameObject LevinD;
    public GameObject robertbrownD;
    public GameObject PurkinjeD;
    public GameObject omniscellulaD;
    public GameObject organismsdivided;
    public GameObject wholeorganismD;
    public GameObject trillionD;
    public GameObject cellmembraneD;
    public GameObject pseudopodiaD;
    public GameObject divisionoflaborD;
    public GameObject cellorganellesD;
    public GameObject prokaryoticcellsD;
    public GameObject prokaryotesD;
    public GameObject eukaryoticcellsD;
    public GameObject eukaryotesD;
    public GameObject hypertonicD;
    public GameObject isotonicD;
    public GameObject hypotonicD;
    public GameObject plasmolysisD;
    public GameObject rigidD;
    public GameObject celluloseD;
    public GameObject harshD;
    public GameObject nucleasD;
    public GameObject nucelarD;
    public GameObject dnaD;
    public GameObject genesD;
    public GameObject chromatinD;
    public GameObject nucleoidD;
    public GameObject cytoplasmD;
    public GameObject protoplasmD;
    public GameObject cytosolD;
    public GameObject rerD;
    public GameObject biogenesisD;
    public GameObject ribosomesD;
    public GameObject camiloD;
    public GameObject glycoD;
    public GameObject powerhouseD;
    public GameObject innerouterD;
    public GameObject atp;
    public GameObject cleaningD;
    public GameObject suicidebagsD;
    public GameObject vacuoleD;
    public GameObject plastidsD;
    public GameObject coloredplastidsD;
    public GameObject chloroplastsD;
    public GameObject granulesD;
    public GameObject celldivD;
    public GameObject mmD;

    
   

      // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]

    

    public GameObject onionslide;
    public GameObject topslideanim;
    public GameObject needleanim;
    public GameObject bacteriaanim;
    public GameObject amoebanim;
    public GameObject spsanim;
    public GameObject diffusionanim;
    public GameObject high;
    public GameObject same;
    public GameObject highcell;
    public GameObject samecell;
    public GameObject lyzoblastanim;
    public GameObject mitoanim;
    public GameObject mieoanim;
    public GameObject mitocanim;
    public GameObject shrinkanim;
    public GameObject shrinkcellHanim;




    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip audio_1;
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
    public AudioClip audio_32;
    public AudioClip audio_33;
    public AudioClip audio_34;
    public AudioClip audio_35;
    public AudioClip audio_36;
    public AudioClip audio_37;
    public AudioClip audio_38;
    public AudioClip audio_39;
    public AudioClip audio_40;
    public AudioClip audio_41;
    public AudioClip audio_42;
    public AudioClip audio_43;
    public AudioClip audio_44;
    public AudioClip audio_45;
    public AudioClip audio_46;


//Part2



    public AudioClip audio_1b;
    public AudioClip audio_2b;
    public AudioClip audio_3b;
    public AudioClip audio_4b;
    public AudioClip audio_5b;
    public AudioClip audio_6b;
    public AudioClip audio_7b;
    public AudioClip audio_8b;
    public AudioClip audio_9b;
    public AudioClip audio_10b;
    public AudioClip audio_11b;
    public AudioClip audio_12b;
    public AudioClip audio_13b;
    public AudioClip audio_14b;
    public AudioClip audio_15b;
    public AudioClip audio_16b;
    public AudioClip audio_17b;
    public AudioClip audio_18b;
    public AudioClip audio_19b;
    public AudioClip audio_20b;
    public AudioClip audio_21b;
    public AudioClip audio_22b;
    public AudioClip audio_23b;
    public AudioClip audio_24b;
    public AudioClip audio_25b;
    public AudioClip audio_26b;
    public AudioClip audio_27b;
    public AudioClip audio_28b;
    public AudioClip audio_29b;
    public AudioClip audio_30b;
    public AudioClip audio_31b;
    public AudioClip audio_32b;
    public AudioClip audio_33b;
    public AudioClip audio_34b;
    public AudioClip audio_35b;
    public AudioClip audio_36b;
    public AudioClip audio_37b;
    public AudioClip audio_38b;
    public AudioClip audio_39b;
    public AudioClip audio_40b;
    public AudioClip audio_41b;
    public AudioClip audio_42b;
    public AudioClip audio_43b;
    public AudioClip audio_44b;
    public AudioClip audio_45b;
    public AudioClip audio_46b;
    public AudioClip audio_47b;
    public AudioClip audio_48b;
    public AudioClip audio_49b;
    public AudioClip audio_50b;
    public AudioClip audio_51b;
    public AudioClip audio_52b;
    public AudioClip audio_53b;
    public AudioClip audio_54b;
    public AudioClip audio_55b;
    public AudioClip audio_56b;
    public AudioClip audio_57b;
    public AudioClip audio_58b;
    public AudioClip audio_59b;
    public AudioClip audio_60b;
    public AudioClip audio_61b;
    public AudioClip audio_62b;
    public AudioClip audio_63b;
    public AudioClip audio_64b;
    public AudioClip audio_65b;
    public AudioClip audio_66b;
    public AudioClip audio_67b;
    public AudioClip audio_68b;
    public AudioClip audio_69b;
    public AudioClip audio_70b;
    public AudioClip audio_71b;
    public AudioClip audio_72b;
    public AudioClip audio_73b;
    public AudioClip audio_74b;
    public AudioClip audio_75b;
    public AudioClip audio_76b;
    public AudioClip audio_77b;
    public AudioClip audio_78b;
    public AudioClip audio_79b;
    public AudioClip audio_80b;
    public AudioClip audio_81b;
    public AudioClip audio_82b;
    public AudioClip audio_83b;
    public AudioClip audio_84b;
    public AudioClip audio_85b;
    public AudioClip audio_86b;
    public AudioClip audio_87b;
    public AudioClip audio_88b;
    public AudioClip audio_89b;
    public AudioClip audio_90b;
    public AudioClip audio_91b;
    public AudioClip audio_92b;
    public AudioClip audio_93b;
    public AudioClip audio_94b;
    public AudioClip audio_95b;
    public AudioClip audio_96b;
    public AudioClip audio_97b;
    public AudioClip audio_98b;
    public AudioClip audio_99b;
    public AudioClip audio_100b;
    // Light

    //1
    void _animal_cell_light_position1MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(344.137817f, 22.7329998f, 501.663422f);
    }
    //2    diff cells
    void _animal_cell_light_position2MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(343.759003f, 27.6930008f, 496.026794f);
    }
    //3    ameoba
    void _animal_cell_light_position3MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(340.716003f, 27.5820007f, 491.053009f);
    }
    //4     2 cells
    void _animal_cell_light_position4MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(361.270996f, 26.5489998f, 492.234009f);
    }
    //5   bacteria
    void _animal_cell_light_position5MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(335.160004f, 22.7660465f, 516.575989f);
    }
    //6   co2 o2
    void _animal_cell_light_position6MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(344.586456f, 28.1560001f, 489.151001f);
    }
    //7    left beaker
    void _animal_cell_light_position7MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(347.600006f, 28.3250008f, 486.100006f);
    }
    //8   right beaker
    void _animal_cell_light_position8MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(346.757996f, 28.0209999f, 486.942993f);
    }
    //9   chromosome
    void _animal_cell_light_position9MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(344.950989f, 28.4860001f, 489.914001f);
    }
    //10   DNA
    void _animal_cell_light_position10MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(345.742004f, 28.5419998f, 489.270996f);
    }
    //11   ribosomes
    void _animal_cell_light_position11MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(329.106995f, 23.1079998f, 516.768005f);
    }
    //12   animal cell main
    void _animal_cell_light_position12MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(330.225006f, 22.9839993f, 511.024994f);
    }
    //13   plant cell
    void _animal_cell_light_position13MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(331.230011f, 23.6539993f, 506.214996f);
    }
    //14  mitosis
    void _animal_cell_light_position14MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(335.071014f, 23.2889996f, 489.696014f);
    }
    //15  mitosis left
    void _animal_cell_light_position15MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(331.250824f, 23.9200001f, 494.950012f);
    }
    //16   mitosis right
    void _animal_cell_light_position16MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(331.250824f, 23.1000004f, 498.25f);
    }
    //17   meiosis left
    void _animal_cell_light_position17MethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(334.494995f, 24.1200008f, 496.218994f);
    }
    //18   meiosis right
    void _animal_cell_light_position18MethodON()
    {
        myLight.intensity = 2f;
        myLight.transform.position = new Vector3(332.859985f, 24.1200008f, 501.619995f);
    }



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





















    void _rigidDMethodON()
    {
        rigidD.SetActive(true);
    }
    void _rigidDMethodoOFF()
    {
        rigidD.SetActive(false);
    }
    //
    void _celluloseDMethodON()
    {
        celluloseD.SetActive(true);
    }
    void _celluloseDMethodoOFF()
    {
        celluloseD.SetActive(false);
    }
    //
    void _harshDMethodON()
    {
        harshD.SetActive(true);
    }
    void _harshDtMethodoOFF()
    {
        harshD.SetActive(false);
    }
    //
    void _CellsaptextMethodON()
    {
        cellsaptext.SetActive(true);
    }
    void _CellsaptextMethodoOFF()
    {
        cellsaptext.SetActive(false);
    }
    //
    void _TonoplasttextMethodON()
    {
        tonoplasttext.SetActive(true);
    }
    void _TonoplasttextMethodoOFF()
    {
        tonoplasttext.SetActive(false);
    }
    //
    void _plastidstextMethodON()
    {
        plastidstext.SetActive(true);
    }
    void _plastidstextMethodoOFF()
    {
        plastidstext.SetActive(false);
    }
    //
    void _MMDMethodON()
    {
        mmD.SetActive(true);
    }
    void _MMDMethodoOFF()
    {
        mmD.SetActive(false);
    }
    //
    void _MitosisTMethodON()
    {
        MitosisT.SetActive(true);
    }
    void _MitosisTMethodoOFF()
    {
        MitosisT.SetActive(false);
    }
    //
    void _MeiosisTMethodON()
    {
        MeiosisT.SetActive(true);
    }
    void _MeiosisTMethodoOFF()
    {
        MeiosisT.SetActive(false);
    }

    void _IntroductionTMethodON()
    {
        IntroductionT.SetActive(true);
    }
    void _IntroductionTMethodoOFF()
    {
        IntroductionT.SetActive(false);
    }
    //
    void _celldivDMethodON()
    {
        celldivD.SetActive(true);
    }
    void _celldivDMethodoOFF()
    {
        celldivD.SetActive(false);
    }
    //
    void _ATPDMethodON()
    {
        atp.SetActive(true);
    }
    void _ATPDMethodoOFF()
    {
        atp.SetActive(false);
    }
    //
    void _cleaningDMethodON()
    {
        cleaningD.SetActive(true);
    }
    void _cleaningDMethodoOFF()
    {
        cleaningD.SetActive(false);

    }
    //
    void _suicidebagsDMethodON()
    {
        suicidebagsD.SetActive(true);
    }
    void _suicidebagsDMethodoOFF()
    {
        suicidebagsD.SetActive(false);
    }
    //
    void _vacuoleDMethodON()
    {
        vacuoleD.SetActive(true);
    }
    void _vacuoleDMethodoOFF()
    {
        vacuoleD.SetActive(false);
    }
    //
    void _plastidsDMethodON()
    {
        plastidsD.SetActive(true);
    }
    void _plastidsDMethodoOFF()
    {
        plastidsD.SetActive(false);
    }
    //
    void _coloredplastidsDMethodON()
    {
        coloredplastidsD.SetActive(true);
    }
    void _coloredplastidsDMethodoOFF()
    {
        coloredplastidsD.SetActive(false);
    }
    //
    void _chloroplastsDMethodON()
    {
        chloroplastsD.SetActive(true);
    }
    void _chloroplastsDMethodoOFF()
    {
        chloroplastsD.SetActive(false);
    }
    //
    void _granulesDMethodON()
    {
        granulesD.SetActive(true);
    }
    void _granulesDMethodoOFF()
    {
        granulesD.SetActive(false);
    }
    //
    void _InOutTextMethodON()
    {
        inouttext.SetActive(true);
    }
    void _InOutTextMethodoOFF()
    {
        inouttext.SetActive(false);
    }
    //
    void _PillMethodON()
    {
        pill.SetActive(true);
    }
    void _PillMethodoOFF()
    {
        pill.SetActive(false);
    }
    //
    void _SpsMethodON()
    {
        sps.SetActive(true);
    }
    void _SpsMethodoOFF()
    {
        sps.SetActive(false);
    }
    //
    void _DomeMethodON()
    {
        dome.SetActive(true);
    }
    void _DomeMethodoOFF()
    {
        dome.SetActive(false);
    }
    //
    void _DripMethodON()
    {
        drip.SetActive(true);
    }
    void _DripMethodoOFF()
    {
        drip.SetActive(false);
    }
    //
    void _glassslideMethodON()
    {
        glassslide.SetActive(true);
    }
    void _glassslideMethodoOFF()
    {
        glassslide.SetActive(false);
    }
    //
    void _glassslidetopMethodON()
    {
        glassslidetop.SetActive(true);
    }
    void _glassslidetopMethodoOFF()
    {
        glassslidetop.SetActive(false);
    }
    //
    void _DropperMethodON()
    {
        dropper.SetActive(true);
    }
    void _DropperMethodoOFF()
    {
        dropper.SetActive(false);
    }
    //
    void _NeedleMethodON()
    {
        needle.SetActive(true);
    }
    void _NeedleMethodoOFF()
    {
        needle.SetActive(false);
    }
    //
    void _SmolSliceMethodON()
    {
        smolslice.SetActive(true);
    }
    void _SmolSliceMethodoOFF()
    {
        smolslice.SetActive(false);
    }
    //
    void _DogMethodON()
    {
        dog.SetActive(true);
    }
    void _DogMethodoOFF()
    {
        dog.SetActive(false);
    }
    //
    void _MarcelloMethodON()
    {
        Marcello.SetActive(true);
    }
    void _MarcelloMethodoOFF()
    {
        Marcello.SetActive(false);
    }
    //
    void _RoberthookeMethodON()
    {
        roberthooke.SetActive(true);
    }
    void _RoberthookeMethodoOFF()
    {
        roberthooke.SetActive(false);
    }
    //
    void _RoberthookecellsMethodON()
    {
        roberthookecells.SetActive(true);
    }
    void _RoberthookecellsMethodoOFF()
    {
        roberthookecells.SetActive(false);
    }
    //
    void _LevinMethodON()
    {
        Levin.SetActive(true);
    }
    void _LevinMethodoOFF()
    {
        Levin.SetActive(false);
    }
    //
    void _RobertMethodON()
    {
        robertbrown.SetActive(true);
    }
    void _RobertMethodoOFF()
    {
        robertbrown.SetActive(false);
    }
    //
    void _RobertbrowncellsMethodON()
    {
        robertbrowncells.SetActive(true);
    }
    void _RobertbrowncellsMethodoOFF()
    {
        robertbrowncells.SetActive(false);
    }
    //
    void _PurkinjeMethodON()
    {
        Purkinje.SetActive(true);
    }
    void _PurkinjeMethodoOFF()
    {
        Purkinje.SetActive(false);
    }
    //
    void _TwoguysMethodON()
    {
        twoguys.SetActive(true);
    }
    void _TwoguysMethodoOFF()
    {
        twoguys.SetActive(false);
    }
    //
    void _RudolfMethodON()
    {
        rudolf.SetActive(true);
    }
    void _RudolfMethodoOFF()
    {
        rudolf.SetActive(false);
    }
    //





















    void _CellDiscoveryTMethodON()
    {
        cellsdiscoveryT.SetActive(true);
    }
    void _CellDiscoveryTMethodoOFF()
    {
        cellsdiscoveryT.SetActive(false);
    }
    //
    void _CompoundmicroTMethodON()
    {
        compoundmicroscopeT.SetActive(true);
    }
    void _CompoundmicroTMethodoOFF()
    {
        compoundmicroscopeT.SetActive(false);
    }
    //
    void _TypeSizeTMethodON()
    {
        typesizeT.SetActive(true);
    }
    void _TypeSizeTMethodoOFF()
    {
        typesizeT.SetActive(false);
    }
    //
    void _CellTypesTMethodON()
    {
        celltypesT.SetActive(true);
    }
    void _CellTypesTMethodoOFF()
    {
        celltypesT.SetActive(false);
    }
    //
    void _UnicellularTMethodON()
    {
        unicellularT.SetActive(true);
    }
    void _UnicellularTMethodoOFF()
    {
        unicellularT.SetActive(false);
    }
    //
    void _MulticellularTMethodON()
    {
        multicellularT.SetActive(true);
    }
    void _MulticellularTMethodoOFF()
    {
        multicellularT.SetActive(false);
    }
    //
    void _CellShapeTMethodON()
    {
        cellshapeT.SetActive(true);
    }
    void _CellShapeTMethodoOFF()
    {
        cellshapeT.SetActive(false);
    }
    //
    void _LabordivisionTMethodON()
    {
        labordivisionT.SetActive(true);
    }
    void _LabordivisionTMethodoOFF()
    {
        labordivisionT.SetActive(false);
    }
    //
    void _TypesofCellDivisionTMethodON()
    {
        typeofcellnucleasT.SetActive(true);
    }
    void _TypesofCellDivisionTMethodoOFF()
    {
        typeofcellnucleasT.SetActive(false);
    }
    //
    void _StructuralUnitTMethodON()
    {
        structuralunitT.SetActive(true);
    }
    void _StructuralUnitTMethodoOFF()
    {
        structuralunitT.SetActive(false);
    }
    //
    void _PlasmaMembraneTMethodON()
    {
        plasmamembraneT.SetActive(true);
    }
    void _PlasmaMembraneTMethodoOFF()
    {
        plasmamembraneT.SetActive(false);
    }
    //
    void _CellWallTMethodON()
    {
        cellwallT.SetActive(true);
    }
    void _CellWallTMethodoOFF()
    {
        cellwallT.SetActive(false);
    }
    //
    void _NucleasTMethodON()
    {
        nucleasT.SetActive(true);
    }
    void _NucleasTMethodoOFF()
    {
        nucleasT.SetActive(false);
    }
    //
    void _CytoplasmTMethodON()
    {
        cytoplasmT.SetActive(true);
    }
    void _CytoplasmTMethodoOFF()
    {
        cytoplasmT.SetActive(false);
    }
    //
    void _CellOrganellesTMethodON()
    {
        CellorganellesT.SetActive(true);
    }
    void _CellOrganellesTMethodoOFF()
    {
        CellorganellesT.SetActive(false);
    }
    //

    void _EndoplasmicreticulumTMethodON()
    {
        EndoplasmicreticulumT.SetActive(true);
    }
    void _EndoplasmicreticulumTMethodoOFF()
    {
        EndoplasmicreticulumT.SetActive(false);
    }
    //
    void _RibosomesTMethodON()
    {
        ribosomesT.SetActive(true);
    }
    void _RibosomesTMethodoOFF()
    {
        ribosomesT.SetActive(false);
    }
    //
    void _GolgiTMethodON()
    {
        golgiapparatusT.SetActive(true);
    }
    void _GolgiTMethodoOFF()
    {
        golgiapparatusT.SetActive(false);
    }
    //
    void _MitochondriaTMethodON()
    {
        mitochondriaT.SetActive(true);
    }
    void _MitochondriaTMethodoOFF()
    {
        mitochondriaT.SetActive(false);
    }
    //
    void _LysosomesTMethodON()
    {
        LysosomesT.SetActive(true);
    }
    void _LysosomesTMethodoOFF()
    {
        LysosomesT.SetActive(false);
    }
    //
    void _VacuoleTMethodON()
    {
        VacuoleT.SetActive(true);
    }
    void _VacuoleTMethodoOFF()
    {
        VacuoleT.SetActive(false);
    }
    //
    void _PlastidsTMethodON()
    {
        PlastidsT.SetActive(true);
    }
    void _PlastidsTMethodoOFF()
    {
        PlastidsT.SetActive(false);
    }
    //
    void _GranualsTMethodON()
    {
        GranualsT.SetActive(true);
    }
    void _GranualsTMethodoOFF()
    {
        GranualsT.SetActive(false);
    }
    //
    
    void _CellDivisionTMethodON()
    {
        celldivisionT.SetActive(true);
    }
    void _CellDivisionTMethodoOFF()
    {
        celldivisionT.SetActive(false);
    }
    //





















    void _CellDMethodON()
    {
        cellsD.SetActive(true);
    }
    void _CellDMethodoOFF()
    {
        cellsD.SetActive(false);
    }
    //
    void _MarcellomalpighiDMethodON()
    {
        MarcelloD.SetActive(true);
    }
    void _MarcellomalpighiDMethodoOFF()
    {
        MarcelloD.SetActive(false);
    }
    //
    void _RobertHookeDMethodON()
    {
        roberthookeD.SetActive(true);
    }
    void _RobertHookeDMethodoOFF()
    {
        roberthookeD.SetActive(false);
    }
    //
    void _LevinHawkDMethodON()
    {
        LevinD.SetActive(true);
    }
    void _LevinHawkDMethodoOFF()
    {
        LevinD.SetActive(false);
    }
    //
    void _RobertBrownDMethodON()
    {
        robertbrownD.SetActive(true);
    }
    void _RobertBrownDMethodoOFF()
    {
        robertbrownD.SetActive(false);
    }
    //
    void _PurkinjeDMethodON()
    {
        PurkinjeD.SetActive(true);
    }
    void _PurkinjeDMethodoOFF()
    {
        PurkinjeD.SetActive(false);
    }
    //
    void _omnisDMethodON()
    {
        omniscellulaD.SetActive(true);
    }
    void _omnisDMethodoOFF()
    {
        omniscellulaD.SetActive(false);
    }
    //
    void _TypeofcellsDMethodON()
    {
        organismsdivided.SetActive(true);
    }
    void _TypesofcellsDMethodoOFF()
    {
        organismsdivided.SetActive(false);
    }
    //
    void _UnicellularDMethodON()
    {
        wholeorganismD.SetActive(true);
    }
    void _UnicellularDMethodoOFF()
    {
        wholeorganismD.SetActive(false);
    }
    //
    void _TrillionDMethodON()
    {
        trillionD.SetActive(true);
    }
    void _TrillionDMethodoOFF()
    {
        trillionD.SetActive(false);
    }
    //
    void _CellmembraneDMethodON()
    {
        cellmembraneD.SetActive(true);
    }
    void _CellmembraneDMethodoOFF()
    {
        cellmembraneD.SetActive(false);
    }
    //
    void _PsudoDMethodON()
    {
        pseudopodiaD.SetActive(true);
    }
    void _PsudoDMethodoOFF()
    {
        pseudopodiaD.SetActive(false);
    }
    //
    void _DivisionoflabourDMethodON()
    {
        divisionoflaborD.SetActive(true);
    }
    void _DivisionoflabourDMethodoOFF()
    {
        divisionoflaborD.SetActive(false);
    }
    //
    void _OrganellesDMethodON()
    {
        cellorganellesD.SetActive(true);
    }
    void _OrganellesDMethodoOFF()
    {
        cellorganellesD.SetActive(false);
    }
    //
    void _ProCellsDMethodON()
    {
        prokaryoticcellsD.SetActive(true);
    }
    void _ProCellsDMethodoOFF()
    {
        prokaryoticcellsD.SetActive(false);
    }
    //
    void _ProsDMethodON()
    {
        prokaryotesD.SetActive(true);
    }
    void _ProsDMethodoOFF()
    {
        prokaryotesD.SetActive(false);
    }
    //
    void _EukarCellsDMethodON()
    {
        eukaryoticcellsD.SetActive(true);
    }
    void _EukarCellsDMethodoOFF()
    {
        eukaryoticcellsD.SetActive(false);
    }
    //
    void _EukaryotesDMethodON()
    {
        eukaryotesD.SetActive(true);
    }
    void _EukaryotesDMethodoOFF()
    {
        eukaryotesD.SetActive(false);
    }
    //
    void _HypertonicDMethodON()
    {
        hypertonicD.SetActive(true);
    }
    void _HypertonicDMethodoOFF()
    {
        hypertonicD.SetActive(false);
    }
    //
    void _IsotonicDMethodON()
    {
        isotonicD.SetActive(true);
    }
    void _IsotonicDMethodoOFF()
    {
        isotonicD.SetActive(false);
    }
    //
    void _HypotonicDMethodON()
    {
        hypotonicD.SetActive(true);
    }
    void _HypotonicDMethodoOFF()
    {
        hypotonicD.SetActive(false);
    }
    //
    void _PlasmolysisDMethodON()
    {
        plasmolysisD.SetActive(true);
    }
    void _PlasmolysisDMethodoOFF()
    {
        plasmolysisD.SetActive(false);
    }
    //
    void _NucleasDMethodON()
    {
        nucleasD.SetActive(true);
    }
    void _NucleasDMethodoOFF()
    {
        nucleasD.SetActive(false);
    }
    //
    void _NuclearDMethodON()
    {
        nucelarD.SetActive(true);
    }
    void _NuclearDMethodoOFF()
    {
        nucelarD.SetActive(false);
    }
    //
    void _DNADMethodON()
    {
        dnaD.SetActive(true);
    }
    void _DNADMethodoOFF()
    {
        dnaD.SetActive(false);
    }
    //
    void _GenesDMethodON()
    {
        genesD.SetActive(true);
    }
    void _GenesDMethodoOFF()
    {
        genesD.SetActive(false);
    }
    //
    void _ChromatinDMethodON()
    {
        chromatinD.SetActive(true);
    }
    void _ChromatinDMethodoOFF()
    {
        chromatinD.SetActive(false);
    }
    //
    void _NucleoidDMethodON()
    {
        nucleoidD.SetActive(true);
    }
    void _NucleoidDMethodoOFF()
    {
        nucleoidD.SetActive(false);
    }
    //
    void _CytoplasmDMethodON()
    {
        cytoplasmD.SetActive(true);
    }
    void _CytoplasmDMethodoOFF()
    {
        cytoplasmD.SetActive(false);
    }
    //
    void _ProtoplasmDMethodON()
    {
        protoplasmD.SetActive(true);
    }
    void _ProtoplasmDMethodoOFF()
    {
        protoplasmD.SetActive(false);
    }
    //
    void _CytosolDMethodON()
    {
        cytosolD.SetActive(true);
    }
    void _CytosolDMethodoOFF()
    {
        cytosolD.SetActive(false);
    }
    //
    void _RerDMethodON()
    {
        rerD.SetActive(true);
    }
    void _RerDMethodoOFF()
    {
        rerD.SetActive(false);
    }
    //
    void _BiogenesisDMethodON()
    {
        biogenesisD.SetActive(true);
    }
    void _BiogenesisDMethodoOFF()
    {
        biogenesisD.SetActive(false);
    }
    //
    void _RibosomesDMethodON()
    {
        ribosomesD.SetActive(true);
    }
    void _RibosomesDMethodoOFF()
    {
        ribosomesD.SetActive(false);
    }
    //
    void _CamiloDMethodON()
    {
        camiloD.SetActive(true);
    }
    void _CamiloDMethodoOFF()
    {
        camiloD.SetActive(false);
    }
    //
    void _GlycoDMethodON()
    {
        glycoD.SetActive(true);
    }
    void _GlycoDMethodoOFF()
    {
        glycoD.SetActive(false);
    }
    //
    void _PowerHouseDMethodON()
    {
        powerhouseD.SetActive(true);
    }
    void _PowerHouseDMethodoOFF()
    {
        powerhouseD.SetActive(false);
    }
    //
    void _InOutDMethodON()
    {
        innerouterD.SetActive(true);
    }
    void _InOutDMethodoOFF()
    {
        innerouterD.SetActive(false);
    }
    //
    















    void _Onionslice_animationAnimmethod()
    {

        anim = onionslide.GetComponent<Animator>();
        anim.Play("Onion slice animation");
    }
























    void _Topslide_animationAnimmethod()
    {

        anim = topslideanim.GetComponent<Animator>();
        anim.Play("Top slide animation");
    }
    void _Needle_animationAnimmethod()
    {

        anim = needleanim.GetComponent<Animator>();
        anim.Play("Needle animation");
    }
    void _Bacteria_animationAnimmethod()
    {

        anim = bacteriaanim.GetComponent<Animator>();
        anim.Play("Bacteria animation");
    }
    void _Aboeba_animationAnimmethod()
    {

        anim = amoebanim.GetComponent<Animator>();
        anim.Play("Amoeba animation");
    }
    void _Sps_animationAnimmethod()
    {

        anim = spsanim.GetComponent<Animator>();
        anim.Play("Sps anim");
    }
    void _Diffusion_animationAnimmethod()
    {

        anim = diffusionanim.GetComponent<Animator>();
        anim.Play("Diffusion anim");
    }
    void _High_animationAnimmethod()
    {

        anim = high.GetComponent<Animator>();
        anim.Play("High concentration", -1, 0 );
    }
    void _HighCell_animationAnimmethod()
    {

        anim = highcell.GetComponent<Animator>();
        anim.Play("High cell");
    }
    void _Same_animationAnimmethod()
    {

        anim = same.GetComponent<Animator>();
        anim.Play("Same concentration");
    }
    void _SameCell_animationAnimmethod()
    {

        anim = samecell.GetComponent<Animator>();
        anim.Play("Same cell");
    }
    void _Lyzoblast_animationAnimmethod()
    {

        anim = lyzoblastanim.GetComponent<Animator>();
        anim.Play("Lyzo blast");
    }
    void _Mitosis_animationAnimmethod()
    {

        anim = mitoanim.GetComponent<Animator>();
        anim.Play("Mitosis anim");
    }
    void _Meiosis_animationAnimmethod()
    {

        anim = mieoanim.GetComponent<Animator>();
        anim.Play("Meiosis anim");
    }
    void _MitoC_animationAnimmethod()
    {

        anim = mitocanim.GetComponent<Animator>();
        anim.Play("Mitosis c anim");
    }
    void _ShrinkCell_animationAnimmethod()
    {

        anim = shrinkanim.GetComponent<Animator>();
        anim.Play("Cell parts shrink");
    }
    void _ShrinkCellH_animationAnimmethod()
    {

        anim = shrinkcellHanim.GetComponent<Animator>();
        anim.Play("shrink h2o");
    }











      






      void _audio_1_audioMethod()   
    {
        myAudio.clip = audio_1;
        myAudio.Play();
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
    void _audio_32_audioMethod()
    {
        myAudio.clip = audio_32;
        myAudio.Play();
    }
    void _audio_33_audioMethod()
    {
        myAudio.clip = audio_33;
        myAudio.Play();
    }
    void _audio_34_audioMethod()
    {
        myAudio.clip = audio_34;
        myAudio.Play();
    }
    void _audio_35_audioMethod()
    {
        myAudio.clip = audio_35;
        myAudio.Play();
    }
    void _audio_36_audioMethod()
    {
        myAudio.clip = audio_36;
        myAudio.Play();
    }
    void _audio_37_audioMethod()
    {
        myAudio.clip = audio_37;
        myAudio.Play();
    }
    void _audio_38_audioMethod()
    {
        myAudio.clip = audio_38;
        myAudio.Play();
    }
    void _audio_39_audioMethod()
    {
        myAudio.clip = audio_39;
        myAudio.Play();
    }
    void _audio_40_audioMethod()
    {
        myAudio.clip = audio_40;
        myAudio.Play();
    }
    void _audio_41_audioMethod()
    {
        myAudio.clip = audio_41;
        myAudio.Play();
    }
    void _audio_42_audioMethod()
    {
        myAudio.clip = audio_42;
        myAudio.Play();
    }
    void _audio_43_audioMethod()
    {
        myAudio.clip = audio_43;
        myAudio.Play();
    }
    void _audio_44_audioMethod()
    {
        myAudio.clip = audio_44;
        myAudio.Play();
    }
    void _audio_45_audioMethod()
    {
        myAudio.clip = audio_45;
        myAudio.Play();
    }
    void _audio_46_audioMethod()
    {
        myAudio.clip = audio_46;
        myAudio.Play();
    }



//Part2


    void _audio_1b_audioMethod()
    {
        myAudio.clip = audio_1b;
        myAudio.Play();
    }
    void _audio_2b_audioMethod()
    {
        myAudio.clip = audio_2b;
        myAudio.Play();
    }
    void _audio_3b_audioMethod()
    {
        myAudio.clip = audio_3b;
        myAudio.Play();
    }
    void _audio_4b_audioMethod()
    {
        myAudio.clip = audio_4b;
        myAudio.Play();
    }
    void _audio_5b_audioMethod()
    {
        myAudio.clip = audio_5b;
        myAudio.Play();
    }
    void _audio_6b_audioMethod()
    {
        myAudio.clip = audio_6b;
        myAudio.Play();
    }
    void _audio_7b_audioMethod()
    {
        myAudio.clip = audio_7b;
        myAudio.Play();
    }
    void _audio_8b_audioMethod()
    {
        myAudio.clip = audio_8b;
        myAudio.Play();
    }
    void _audio_9b_audioMethod()
    {
        myAudio.clip = audio_9b;
        myAudio.Play();
    }
    void _audio_10b_audioMethod()
    {
        myAudio.clip = audio_10b;
        myAudio.Play();
    }

    void _audio_11b_audioMethod()
    {
        myAudio.clip = audio_11b;
        myAudio.Play();
    }
    void _audio_12b_audioMethod()
    {
        myAudio.clip = audio_12b;
        myAudio.Play();
    }
    void _audio_13b_audioMethod()
    {
        myAudio.clip = audio_13b;
        myAudio.Play();
    }
    void _audio_14b_audioMethod()
    {
        myAudio.clip = audio_14b;
        myAudio.Play();
    }
    void _audio_15b_audioMethod()
    {
        myAudio.clip = audio_15b;
        myAudio.Play();
    }
    void _audio_16b_audioMethod()
    {
        myAudio.clip = audio_16b;
        myAudio.Play();
    }
    void _audio_17b_audioMethod()
    {
        myAudio.clip = audio_17b;
        myAudio.Play();
    }
    void _audio_18b_audioMethod()
    {
        myAudio.clip = audio_18b;
        myAudio.Play();
    }
    void _audio_19b_audioMethod()
    {
        myAudio.clip = audio_19b;
        myAudio.Play();
    }
    void _audio_20b_audioMethod()
    {
        myAudio.clip = audio_20b;
        myAudio.Play();
    }
    void _audio_21b_audioMethod()
    {
        myAudio.clip = audio_21b;
        myAudio.Play();
    }
    void _audio_22b_audioMethod()
    {
        myAudio.clip = audio_22b;
        myAudio.Play();
    }
    void _audio_23b_audioMethod()
    {
        myAudio.clip = audio_23b;
        myAudio.Play();
    }
    void _audio_24b_audioMethod()
    {
        myAudio.clip = audio_24b;
        myAudio.Play();
    }
    void _audio_25b_audioMethod()
    {
        myAudio.clip = audio_25b;
        myAudio.Play();
    }
    void _audio_26b_audioMethod()
    {
        myAudio.clip = audio_26b;
        myAudio.Play();
    }
    void _audio_27b_audioMethod()
    {
        myAudio.clip = audio_27b;
        myAudio.Play();
    }
    void _audio_28b_audioMethod()
    {
        myAudio.clip = audio_28b;
        myAudio.Play();
    }
    void _audio_29b_audioMethod()
    {
        myAudio.clip = audio_29b;
        myAudio.Play();
    }
    void _audio_30b_audioMethod()
    {
        myAudio.clip = audio_30b;
        myAudio.Play();
    }
    void _audio_31b_audioMethod()
    {
        myAudio.clip = audio_31b;
        myAudio.Play();
    }
    void _audio_32b_audioMethod()
    {
        myAudio.clip = audio_32b;
        myAudio.Play();
    }
    void _audio_33b_audioMethod()
    {
        myAudio.clip = audio_33b;
        myAudio.Play();
    }
    void _audio_34b_audioMethod()
    {
        myAudio.clip = audio_34b;
        myAudio.Play();
    }
    void _audio_35b_audioMethod()
    {
        myAudio.clip = audio_35b;
        myAudio.Play();
    }
    void _audio_36b_audioMethod()
    {
        myAudio.clip = audio_36b;
        myAudio.Play();
    }
    void _audio_37b_audioMethod()
    {
        myAudio.clip = audio_37b;
        myAudio.Play();
    }
    void _audio_38b_audioMethod()
    {
        myAudio.clip = audio_38b;
        myAudio.Play();
    }
    void _audio_39b_audioMethod()
    {
        myAudio.clip = audio_39b;
        myAudio.Play();
    }
    void _audio_40b_audioMethod()
    {
        myAudio.clip = audio_40b;
        myAudio.Play();
    }
    void _audio_41b_audioMethod()
    {
        myAudio.clip = audio_41b;
        myAudio.Play();
    }
    void _audio_42b_audioMethod()
    {
        myAudio.clip = audio_42b;
        myAudio.Play();
    }
    void _audio_43b_audioMethod()
    {
        myAudio.clip = audio_43b;
        myAudio.Play();
    }
    void _audio_44b_audioMethod()
    {
        myAudio.clip = audio_44b;
        myAudio.Play();
    }
    void _audio_45b_audioMethod()
    {
        myAudio.clip = audio_45b;
        myAudio.Play();
    }
    void _audio_46b_audioMethod()
    {
        myAudio.clip = audio_46b;
        myAudio.Play();
    }
    void _audio_47b_audioMethod()
    {
        myAudio.clip = audio_47b;
        myAudio.Play();
    }
    void _audio_48b_audioMethod()
    {
        myAudio.clip = audio_48b;
        myAudio.Play();
    }
    void _audio_49b_audioMethod()
    {
        myAudio.clip = audio_49b;
        myAudio.Play();
    }
    void _audio_50b_audioMethod()
    {
        myAudio.clip = audio_50b;
        myAudio.Play();
    }
    void _audio_51b_audioMethod()
    {
        myAudio.clip = audio_51b;
        myAudio.Play();
    }
    void _audio_52b_audioMethod()
    {
        myAudio.clip = audio_52b;
        myAudio.Play();
    }
    void _audio_53b_audioMethod()
    {
        myAudio.clip = audio_53b;
        myAudio.Play();
    }
    void _audio_54b_audioMethod()
    {
        myAudio.clip = audio_54b;
        myAudio.Play();
    }
    void _audio_55b_audioMethod()
    {
        myAudio.clip = audio_55b;
        myAudio.Play();
    }
    void _audio_56b_audioMethod()
    {
        myAudio.clip = audio_56b;
        myAudio.Play();
    }
    void _audio_57b_audioMethod()
    {
        myAudio.clip = audio_57b;
        myAudio.Play();
    }
    void _audio_58b_audioMethod()
    {
        myAudio.clip = audio_58b;
        myAudio.Play();
    }
    void _audio_59b_audioMethod()
    {
        myAudio.clip = audio_59b;
        myAudio.Play();
    }
    void _audio_60b_audioMethod()
    {
        myAudio.clip = audio_60b;
        myAudio.Play();
    }
    void _audio_61b_audioMethod()
    {
        myAudio.clip = audio_61b;
        myAudio.Play();
    }
    void _audio_62b_audioMethod()
    {
        myAudio.clip = audio_62b;
        myAudio.Play();
    }
    void _audio_63b_audioMethod()
    {
        myAudio.clip = audio_63b;
        myAudio.Play();
    }
    void _audio_64b_audioMethod()
    {
        myAudio.clip = audio_64b;
        myAudio.Play();
    }
    void _audio_65b_audioMethod()
    {
        myAudio.clip = audio_65b;
        myAudio.Play();
    }
    void _audio_66b_audioMethod()
    {
        myAudio.clip = audio_66b;
        myAudio.Play();
    }
    void _audio_67b_audioMethod()
    {
        myAudio.clip = audio_67b;
        myAudio.Play();
    }
    void _audio_68b_audioMethod()
    {
        myAudio.clip = audio_68b;
        myAudio.Play();
    }
    void _audio_69b_audioMethod()
    {
        myAudio.clip = audio_69b;
        myAudio.Play();
    }
    void _audio_70b_audioMethod()
    {
        myAudio.clip = audio_70b;
        myAudio.Play();
    }
    void _audio_71b_audioMethod()
    {
        myAudio.clip = audio_71b;
        myAudio.Play();
    }
    void _audio_72b_audioMethod()
    {
        myAudio.clip = audio_72b;
        myAudio.Play();
    }
    void _audio_73b_audioMethod()
    {
        myAudio.clip = audio_73b;
        myAudio.Play();
    }
    void _audio_74b_audioMethod()
    {
        myAudio.clip = audio_74b;
        myAudio.Play();
    }
    void _audio_75b_audioMethod()
    {
        myAudio.clip = audio_75b;
        myAudio.Play();
    }
    void _audio_76b_audioMethod()
    {
        myAudio.clip = audio_76b;
        myAudio.Play();
    }
    void _audio_77b_audioMethod()
    {
        myAudio.clip = audio_77b;
        myAudio.Play();
    }
    void _audio_78b_audioMethod()
    {
        myAudio.clip = audio_78b;
        myAudio.Play();
    }
    void _audio_79b_audioMethod()
    {
        myAudio.clip = audio_79b;
        myAudio.Play();
    }
    void _audio_80b_audioMethod()
    {
        myAudio.clip = audio_80b;
        myAudio.Play();
    }
    void _audio_81b_audioMethod()
    {
        myAudio.clip = audio_81b;
        myAudio.Play();
    }
    void _audio_82b_audioMethod()
    {
        myAudio.clip = audio_82b;
        myAudio.Play();
    }
    void _audio_83b_audioMethod()
    {
        myAudio.clip = audio_83b;
        myAudio.Play();
    }
    void _audio_84b_audioMethod()
    {
        myAudio.clip = audio_84b;
        myAudio.Play();
    }
    void _audio_85b_audioMethod()
    {
        myAudio.clip = audio_85b;
        myAudio.Play();
    }
    void _audio_86b_audioMethod()
    {
        myAudio.clip = audio_86b;
        myAudio.Play();
    }
    void _audio_87b_audioMethod()
    {
        myAudio.clip = audio_87b;
        myAudio.Play();
    }
    void _audio_88b_audioMethod()
    {
        myAudio.clip = audio_88b;
        myAudio.Play();
    }
    void _audio_89b_audioMethod()
    {
        myAudio.clip = audio_89b;
        myAudio.Play();
    }
    void _audio_90b_audioMethod()
    {
        myAudio.clip = audio_90b;
        myAudio.Play();
    }
    void _audio_91b_audioMethod()
    {
        myAudio.clip = audio_91b;
        myAudio.Play();
    }
    void _audio_92b_audioMethod()
    {
        myAudio.clip = audio_92b;
        myAudio.Play();
    }
    void _audio_93b_audioMethod()
    {
        myAudio.clip = audio_93b;
        myAudio.Play();
    }
    void _audio_94b_audioMethod()
    {
        myAudio.clip = audio_94b;
        myAudio.Play();
    }
    void _audio_95b_audioMethod()
    {
        myAudio.clip = audio_95b;
        myAudio.Play();
    }
    void _audio_96b_audioMethod()
    {
        myAudio.clip = audio_96b;
        myAudio.Play();
    }
    void _audio_97b_audioMethod()
    {
        myAudio.clip = audio_97b;
        myAudio.Play();
    }
    void _audio_98b_audioMethod()
    {
        myAudio.clip = audio_98b;
        myAudio.Play();
    }
    void _audio_99b_audioMethod()
    {
        myAudio.clip = audio_99b;
        myAudio.Play();
    }
    void _audio_100b_audioMethod()
    {
        myAudio.clip = audio_100b;
        myAudio.Play();
    }
    
    

    
    










    








}
