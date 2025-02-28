using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduction_of_plants_sfx_ : MonoBehaviour
{

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public TargetController miniGame1;
    public Animator anim;
    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;

    public ObjectiveController objectiveController;



    private void Awake()

    {
        animator = GetComponent<Animator>();
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
    public void Savepoint1()
    {
        SaveProgress(1, 0, 2);
    }

    public TargetController lv3MiniGame;
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

    private int lv1Ind = 0;
    public void Lv1()
    {
        lv1Ind++;

        if(lv1Ind == 4)
        {
            StepCompleted();
        }
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetBool("Bool", true);
    }

    public void EndAnim(Animator anim)
    {
        anim.SetBool("Bool", false);
    }

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

    public void StartMiniGameWithDelay(TargetController targetController)
    {
        StartCoroutine(MiniGameStartDelay(targetController));
    }

    IEnumerator MiniGameStartDelay(TargetController targetController)
    {
        yield return new WaitForSeconds(1);
        targetController.Output();
    }

    IEnumerator ChangeCamHolderDelay(Transform camHolder)
    {
        yield return new WaitForSeconds(2f);
        transform.position = camHolder.position;
        transform.rotation = camHolder.rotation;
    }

    private int index = 0;
    public void MultiSelectAnswer(int count)
    {
        index++;

        if (index == count)
        {
            StepCompleted();
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

    public void StepComplete(GameObject obj)
    {
        TurnOnGOWithDelay(obj);
    }

    public void StepCompleted()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Transform camHolder2Lv5;
    public void PlayAnimation(Animator anim)
    {
        anim.SetTrigger("Trigger");
        StartCoroutine(DelayAnimEnd());
    }

    public void PlayAnimationtrigger(Animator anim)
    {
        anim.SetTrigger("Trigger");
        
    }

    IEnumerator DelayAnimEnd()
    {
        yield return new WaitForSeconds(3);
        ChangeCamHolderWithDelay(camHolder2Lv5);
    }

    public Camera cam;
    public LayerMask layerMask;
    bool selfPolyDone = false;
    bool crossPolyDone = false;
    private int plyCount = 0;
    private string previousClicked;
    public Animator beeself;
    public Animator beecross;
    private void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
            {
                if (raycastHit.collider != null)
                {

                    if (!selfPolyDone)
                    {
                        if (previousClicked != null && previousClicked == raycastHit.collider.gameObject.name)
                        {
                            plyCount++;
                            if (plyCount == 2)
                            {
                                plyCount = 0;
                                selfPolyDone = true;
                                previousClicked = null;
                                PlayAnimationtrigger(beeself);
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                            }
                        }
                        else if(previousClicked == null)
                        {
                            previousClicked = raycastHit.collider.gameObject.name;
                            plyCount++;
                        }
                        else
                        {
                            MissionFailed();
                           
                        }
                    }
                    else if(selfPolyDone && !crossPolyDone)
                    {
                        if (previousClicked != null && previousClicked != raycastHit.collider.gameObject.name)
                        {
                            plyCount++;
                            if (plyCount == 2)
                            {
                                plyCount = 0;
                                crossPolyDone = true;
                                PlayAnimationtrigger(beecross);
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                            }
                        }
                        else if (previousClicked == null)
                        {
                            previousClicked = raycastHit.collider.gameObject.name;
                            plyCount++;
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

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject bee_anim;
    public GameObject bread;
    public GameObject dry_yeast;
    public GameObject flower_parts;
    public GameObject flower_anim;
    public GameObject fragmentation;
    public GameObject hibiscus;
    public GameObject microscope;
    public GameObject plant;
    public GameObject potato;
    public GameObject spore_formation_in_rhizopus;
    public GameObject yeast_cells;
    public GameObject seed;
    public GameObject Introduction;
    public GameObject What_is_reproduction;
    public GameObject Parts_of_flower;
    public GameObject Stamen;
    public GameObject Anther_and_filament;
    public GameObject pollen;
    public GameObject Types_of_reproduction;
    public GameObject Vegetative_propagation;
    public GameObject Stem;
    public GameObject Leaf;
    public GameObject Budding;
    public GameObject Fragmentation;
    public GameObject Spore_formation;
    public GameObject Unisexual;
    public GameObject Sexual_Reproduction;
    public GameObject Pollination;
    public GameObject Fertilization;
    public GameObject Seed_dispersal;








    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip voice_1;
    public AudioClip voice_2;
    public AudioClip voice_3;
    public AudioClip voice_4;
    public AudioClip voice_5;
    public AudioClip voice_6;
    public AudioClip voice_7;
    public AudioClip voice_8;
    public AudioClip voice_9;
    public AudioClip voice_10;
    public AudioClip voice_11;
    public AudioClip voice_12;
    public AudioClip voice_13;
    public AudioClip voice_14;
    public AudioClip voice_15;
    public AudioClip voice_16;
    public AudioClip voice_17;
    public AudioClip voice_18;
    public AudioClip voice_19;
    public AudioClip voice_20;
    public AudioClip voice_21;
    public AudioClip voice_22;
    public AudioClip voice_23;
    public AudioClip voice_24;
    public AudioClip voice_25;
    public AudioClip voice_26;
    public AudioClip voice_27;
    public AudioClip voice_28;
    public AudioClip voice_29;
    public AudioClip voice_30;
    public AudioClip voice_31;
    public AudioClip voice_32;
    public AudioClip voice_33;
    public AudioClip voice_34;
    public AudioClip voice_35;
    public AudioClip voice_36;
    public AudioClip voice_37;
    public AudioClip voice_38;
    public AudioClip voice_39;
    public AudioClip voice_40;
    public AudioClip voice_41;
    public AudioClip voice_42;
    public AudioClip voice_43;
    public AudioClip voice_44;
    public AudioClip voice_45;
    public AudioClip voice_46;
















    private Animator animator;


    public void _Jump_To1(float value)
    {
        animator.Play("camera_anim", 0, value);
    }





















    // Line on/off

    void _breadmethod()
    {
        bread.SetActive(true);
    }
    //
    void _dryyeastmethod()
    {
        dry_yeast.SetActive(true);
    }
    //
    void _flowerpartsmethod()
    {
        flower_parts.SetActive(true);
    }
    //
    void _fragmentationmethod()
    {
        fragmentation.SetActive(true);
    }
    //
    void _hibiscusmethod()
    {
        hibiscus.SetActive(true);
    }
    //
    void _microscopemethod()
    {
        microscope.SetActive(true);
    }
    //
    void _plantmethod()
    {
        plant.SetActive(true);
    }
    //
    void _potatomethod()
    {
        potato.SetActive(true);
    }
    //
    void _sporemethod()
    {
        spore_formation_in_rhizopus.SetActive(true);
    }
    //
    void _yeastcellmethod()
    {
        yeast_cells.SetActive(true);
    }
    //
    void _seednmethod()
    {
        seed.SetActive(true);
    }
    //
    void _Introductionmethod()
    {
        Introduction.SetActive(true);
    }
    //
    void _reproductionmethod()
    {
        What_is_reproduction.SetActive(true);
    }
    //
    void _Parts_flowermethod()
    {
        Parts_of_flower.SetActive(true);
    }
    //
    void _stamenmethod()
    {
        Stamen.SetActive(true);
    }
    //
    void _Antherandfilamentmethod()
    {
        Anther_and_filament.SetActive(true);
    }
    //
    void _pollenmethod()
    {
        pollen.SetActive(true);
    }
    //
    void _typesofreproductionmethod()
    {
        Types_of_reproduction.SetActive(true);
    }
    //
    void _vegetativepropagationmethod()
    {
        Vegetative_propagation.SetActive(true);
    }
    //
    void _stemmethod()
    {
        Stem.SetActive(true);
    }
    //
    void _leafmethod()
    {
        Leaf.SetActive(true);
    }
    //
    void _buddingmethod()
    {
        Budding.SetActive(true);
    }
    //
    void _Fragmentationmethod()
    {
        Fragmentation.SetActive(true);
    }
    //
    void _sporeformationmethod()
    {
        Spore_formation.SetActive(true);
    }
    //
    void _unisecualmethod()
    {
        Unisexual.SetActive(true);
    }
    //
    void _sexualreproductionmethod()
    {
        Sexual_Reproduction.SetActive(true);
    }
    //
    void _pollinationmethod()
    {
        Pollination.SetActive(true);
    }
    //
    void _fertilizationmethod()
    {
        Fertilization.SetActive(true);
    }
    //
    void _seddispersalmethod()
    {
        Seed_dispersal.SetActive(true);
    }
    //




    //Audio play
    //
    void voice1_method()
    {
        myAudio.clip = voice_1;
        myAudio.Play();

    }
    //
    void voice2_method()
    {
        myAudio.clip = voice_2;
        myAudio.Play();

    }
    //
    void voice3_method()
    {
        myAudio.clip = voice_3;
        myAudio.Play();

    }
    //
    void voice4_method()
    {
        myAudio.clip = voice_4;
        myAudio.Play();

    }
    //
    void voice5_method()
    {
        myAudio.clip = voice_5;
        myAudio.Play();

    }
    //
    void voice6_method()
    {
        myAudio.clip = voice_6;
        myAudio.Play();

    }
    //
    void voice7_method()
    {
        myAudio.clip = voice_7;
        myAudio.Play();

    }
    //
    void voice8_method()
    {
        myAudio.clip = voice_8;
        myAudio.Play();

    }
    //
    void voice9_method()
    {
        myAudio.clip = voice_9;
        myAudio.Play();

    }
    //
    void voice10_method()
    {
        myAudio.clip = voice_10;
        myAudio.Play();

    }
    //
    void voice11_method()
    {
        myAudio.clip = voice_11;
        myAudio.Play();

    }
    //
    void voice12_method()
    {
        myAudio.clip = voice_12;
        myAudio.Play();

    }
    //

    void voice13_method()
    {
        myAudio.clip = voice_13;
        myAudio.Play();

    }
    //
    void voice14_method()
    {
        myAudio.clip = voice_14;
        myAudio.Play();

    }
    //
    void voice15_method()
    {
        myAudio.clip = voice_15;
        myAudio.Play();

    }
    //
    void voice16_method()
    {
        myAudio.clip = voice_16;
        myAudio.Play();

    }
    //
    void voice17_method()
    {
        myAudio.clip = voice_17;
        myAudio.Play();

    }
    //
    void voice18_method()
    {
        myAudio.clip = voice_18;
        myAudio.Play();

    }
    //
    void voice19_method()
    {
        myAudio.clip = voice_19;
        myAudio.Play();

    }
    //
    void voice20_method()
    {
        myAudio.clip = voice_20;
        myAudio.Play();

    }
    //
    void voice21_method()
    {
        myAudio.clip = voice_21;
        myAudio.Play();

    }
    //
    void voice22_method()
    {
        myAudio.clip = voice_22;
        myAudio.Play();

    }
    //
    void voice23_method()
    {
        myAudio.clip = voice_23;
        myAudio.Play();

    }
    //
    void voice24_method()
    {
        myAudio.clip = voice_24;
        myAudio.Play();

    }
    //
    void voice25_method()
    {
        myAudio.clip = voice_25;
        myAudio.Play();

    }
    //
    void voice26_method()
    {
        myAudio.clip = voice_26;
        myAudio.Play();

    }
    //
    void voice27_method()
    {
        myAudio.clip = voice_27;
        myAudio.Play();

    }
    //
    void voice28_method()
    {
        myAudio.clip = voice_28;
        myAudio.Play();

    }
    //
    void voice29_method()
    {
        myAudio.clip = voice_29;
        myAudio.Play();

    }
    //
    void voice30_method()
    {
        myAudio.clip = voice_30;
        myAudio.Play();

    }
    //
    void voice31_method()
    {
        myAudio.clip = voice_31;
        myAudio.Play();

    }
    //
    void voice32_method()
    {
        myAudio.clip = voice_32;
        myAudio.Play();

    }
    //
    void voice33_method()
    {
        myAudio.clip = voice_33;
        myAudio.Play();

    }
    //
    void voice34_method()
    {
        myAudio.clip = voice_34;
        myAudio.Play();

    }
    //
    void voice35_method()
    {
        myAudio.clip = voice_35;
        myAudio.Play();

    }
    //
    void voice36_method()
    {
        myAudio.clip = voice_36;
        myAudio.Play();

    }
    //
    void voice37_method()
    {
        myAudio.clip = voice_37;
        myAudio.Play();

    }
    //
    void voice38_method()
    {
        myAudio.clip = voice_38;
        myAudio.Play();

    }
    //
    void voice39_method()
    {
        myAudio.clip = voice_39;
        myAudio.Play();

    }
    //
    void voice40_method()
    {
        myAudio.clip = voice_40;
        myAudio.Play();

    }
    //
    void voice41_method()
    {
        myAudio.clip = voice_41;
        myAudio.Play();

    }
    //
    void voice42_method()
    {
        myAudio.clip = voice_42;
        myAudio.Play();

    }
    //
    void voice43_method()
    {
        myAudio.clip = voice_44;
        myAudio.Play();

    }
    //
    void voice44_method()
    {
        myAudio.clip = voice_44;
        myAudio.Play();

    }
    //
    void voice45_method()
    {
        myAudio.clip = voice_45;
        myAudio.Play();

    }
    //
    void voice46_method()
    {
        myAudio.clip = voice_46;
        myAudio.Play();

    }
    //









































































}
