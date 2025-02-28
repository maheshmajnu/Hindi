using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduction_in_animals_07_sfx : MonoBehaviour
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
            animator.Play("Camera animation", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
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
            case 1: Level4(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
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

        InitializeFromCheckpoint();
        level();
        

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
        foreach (GameObject objec in questions)
        {
            objec.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
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
        Invoke("StepComplete", 2f);
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

    public void PlayAnimTrigg(Animator anim)
    {
        anim.SetTrigger("Trigger"); // Replace "TriggerName" with the actual name of your trigger
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetBool("Bool", true);
    }

    public void EndAnim(Animator anim)
    {
        anim.SetBool("Bool", false);
    }



    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject epi_text;
    public GameObject vas_text;
    public GameObject scrotum;
    public GameObject glands_txt;
    public GameObject OFP;
    public GameObject ferti;
    public GameObject zygote;
    public GameObject zygoteform;
    public GameObject embryoform;
    public GameObject ameoba;
    public GameObject son;
    public GameObject syringe;
    public GameObject ovumsyringe;
    public GameObject sheep;
    public GameObject sheepclone;
    public GameObject kid;
    public GameObject kidclone;
    public GameObject penis;








    //Titles


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






















    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;
    public GameObject D6;
    public GameObject D7;
    public GameObject D8;
    public GameObject D9;
    public GameObject D10;
    public GameObject D11;
    public GameObject D12;
    public GameObject D13;
    public GameObject D14;
    public GameObject D15;













    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject SnAboard_anim;
    public GameObject epi_anim;
    public GameObject vas_anim;
    public GameObject scrotum_first_anim;
    public GameObject scrotum_second_anim;
    public GameObject OFP_anim;
    public GameObject sperm_anim;
    public GameObject zygote_anim;
    public GameObject gene_anim;
    public GameObject zygform_anim;
    public GameObject embryoform_anim;
    public GameObject hydra_anim;
    public GameObject ameoba_anim;
    public GameObject syringeovum_anim;
    public GameObject syringe_anim;
    public GameObject iovum_anim;









    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;


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
    public AudioClip audio_101b;
    public AudioClip audio_102b;
    public AudioClip audio_103b;
    public AudioClip audio_104b;











    // Jump to point buttons



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


























    // Method ON/OFF




    void _Epi_text_MethodON()
    {
        epi_text.SetActive(true);
    }
    void _Epi_text_MethodoOFF()
    {
        epi_text.SetActive(false);
    }

    void _vas_text_MethodON()
    {
        vas_text.SetActive(true);
    }
    void _vas_text_MethodoOFF()
    {
        vas_text.SetActive(false);
    }
    void _scrotum_MethodON()
    {
        scrotum.SetActive(true);
    }
    void _scrotum_MethodoOFF()
    {
        scrotum.SetActive(false);
    }

    void _glands_txt_MethodON()
    {
        glands_txt.SetActive(true);
    }
    void _glands_txt_MethodoOFF()
    {
        glands_txt.SetActive(false);
    }

    void _OFP_MethodON()
    {
        OFP.SetActive(true);
    }
    void _OFP_MethodoOFF()
    {
        OFP.SetActive(false);
    }

    void _ferti_MethodON()
    {
        ferti.SetActive(true);
    }
    void ferti_MethodoOFF()
    {
        ferti.SetActive(false);
    }

    void _zygote_MethodON()
    {
        zygote.SetActive(true);
    }
    void _zygote_MethodoOFF()
    {
        zygote.SetActive(false);
    }

    void _zygoteform_MethodON()
    {
        zygoteform.SetActive(true);
    }
    void _zygoteform_MethodoOFF()
    {
        zygoteform.SetActive(false);
    }

    void _embryoform_MethodON()
    {
        embryoform.SetActive(true);
    }
    void _embryoform_MethodoOFF()
    {
        embryoform.SetActive(false);
    }

    void _ameoba_MethodON()
    {
        ameoba.SetActive(true);
    }
    void _ameoba_MethodoOFF()
    {
        ameoba.SetActive(false);
    }

    void _son_MethodON()
    {
        son.SetActive(true);
    }
    void _son_MethodoOFF()
    {
        son.SetActive(false);
    }

    void _syringe_MethodON()
    {
        syringe.SetActive(true);
    }
    void _syringe_MethodoOFF()
    {
        syringe.SetActive(false);
    }

    void _ovumsyringe_MethodON()
    {
        ovumsyringe.SetActive(true);
    }
    void _ovumsyringe_MethodoOFF()
    {
        ovumsyringe.SetActive(false);
    }

    void _sheep_MethodON()
    {
        sheep.SetActive(true);
    }
    void _sheep_MethodoOFF()
    {
        sheep.SetActive(false);
    }

    void _sheepclone_MethodON()
    {
        sheepclone.SetActive(true);
    }
    void _sheepclone_MethodoOFF()
    {
        sheepclone.SetActive(false);
    }

    void _kid_MethodON()
    {
        kid.SetActive(true);
    }
    void _kid_MethodoOFF()
    {
        kid.SetActive(false);
    }

    void _kidclone_MethodON()
    {
        kidclone.SetActive(true);
    }
    void _kidclone_MethodoOFF()
    {
        kidclone.SetActive(false);
    }

    void _penis_MethodON()
    {
        penis.SetActive(true);
    }
    void _penis_MethodoOFF()
    {
        penis.SetActive(false);
    }


























    // Animations




    void _SnA_animationAnimmethod()
    {

        anim = SnAboard_anim.GetComponent<Animator>();
        anim.Play("SnA board anim");
    }
    void _Epi_animationAnimmethod()
    {

        anim = epi_anim.GetComponent<Animator>();
        anim.Play("epi anim");
    }
    void _Vas_animationAnimmethod()
    {

        anim = vas_anim.GetComponent<Animator>();
        anim.Play("Vas anim");
    }
    void _Scrotum1_animationAnimmethod()
    {

        anim = scrotum_first_anim.GetComponent<Animator>();
        anim.Play("Scrotum anim 1");
    }
    void _Scrotum2_animationAnimmethod()
    {

        anim = scrotum_second_anim.GetComponent<Animator>();
        anim.Play("Scrotum anim 2");
    }
    void _OFP_animationAnimmethod()
    {

        anim = OFP_anim.GetComponent<Animator>();
        anim.Play("OFP anim");
    }
    void _sperm_animationAnimmethod()
    {

        anim = sperm_anim.GetComponent<Animator>();
        anim.Play("OFP anim");
    }
    void _zygote_animationAnimmethod()
    {

        anim = zygote_anim.GetComponent<Animator>();
        anim.Play("OFP anim");
    }
    void _Gene_animationAnimmethod()
    {

        anim = gene_anim.GetComponent<Animator>();
        anim.Play("Genes anim");
    }
    void _Zygform_animationAnimmethod()
    {

        anim = zygform_anim.GetComponent<Animator>();
        anim.Play("Zygote form anim");
    }
    void _embryoform_animationAnimmethod()
    {

        anim = embryoform_anim.GetComponent<Animator>();
        anim.Play("Embryo form anim");
    }

    void _hydra_animationAnimmethod()
    {

        anim = hydra_anim.GetComponent<Animator>();
        anim.Play("Hydra anim");
    }

    void _ameoba_animationAnimmethod()
    {

        anim = ameoba_anim.GetComponent<Animator>();
        anim.Play("Ameoba anim");
    }

    void _syringeovum_animationAnimmethod()
    {

        anim = syringeovum_anim.GetComponent<Animator>();
        anim.Play("ovum syringe anim");
    }
    void _syringe_animationAnimmethod()
    {

        anim = syringe_anim.GetComponent<Animator>();
        anim.Play("Syringe 1 anim");
    }
    void _ivom_animationAnimmethod()
    {

        anim = iovum_anim.GetComponent<Animator>();
        anim.Play("Iovum anim");
    }





































    // Audio










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
    void _audio_101b_audioMethod()
    {
        myAudio.clip = audio_101b;
        myAudio.Play();
    }
    void _audio_102b_audioMethod()
    {
        myAudio.clip = audio_102b;
        myAudio.Play();
    }
    void _audio_103b_audioMethod()
    {
        myAudio.clip = audio_103b;
        myAudio.Play();
    }
    void _audio_104b_audioMethod()
    {
        myAudio.clip = audio_104b;
        myAudio.Play();
    }























    //Titles




    //
    void _T1_MethodON()
    {
        T1.SetActive(true);
    }
    void _T1_MethodoOFF()
    {
        T1.SetActive(false);
    }
    //
    void _T2_MethodON()
    {
        T2.SetActive(true);
    }
    void _T2_MethodoOFF()
    {
        T2.SetActive(false);
    }
    //
    void _T3_MethodON()
    {
        T3.SetActive(true);
    }
    void _T3_MethodoOFF()
    {
        T3.SetActive(false);
    }
    //
    void _T4_MethodON()
    {
        T4.SetActive(true);
    }
    void _T4_MethodoOFF()
    {
        T4.SetActive(false);
    }
    //
    void _T5_MethodON()
    {
        T5.SetActive(true);
    }
    void _T5_MethodoOFF()
    {
        T5.SetActive(false);
    }
    //
    void _T6_MethodON()
    {
        T6.SetActive(true);
    }
    void _T6_MethodoOFF()
    {
        T6.SetActive(false);
    }
    //
    void _T7_MethodON()
    {
        T7.SetActive(true);
    }
    void _T7_MethodoOFF()
    {
        T7.SetActive(false);
    }
    //
    void _T8_MethodON()
    {
        T8.SetActive(true);
    }
    void _T8_MethodoOFF()
    {
        T8.SetActive(false);
    }
    //
    void _T9_MethodON()
    {
        T9.SetActive(true);
    }
    void _T9_MethodoOFF()
    {
        T9.SetActive(false);
    }
    //
    void _T10_MethodON()
    {
        T10.SetActive(true);
    }
    void _T10_MethodoOFF()
    {
        T10.SetActive(false);
    }
    //
    void _T11_MethodON()
    {
        T11.SetActive(true);
    }
    void _T11_MethodoOFF()
    {
        T11.SetActive(false);
    }
    //
    void _T12_MethodON()
    {
        T8.SetActive(true);
    }
    void _T12_MethodoOFF()
    {
        T12.SetActive(false);
    }
    //
    void _T13_MethodON()
    {
        T13.SetActive(true);
    }
    void _T13_MethodoOFF()
    {
        T13.SetActive(false);
    }
    //
    void _T14_MethodON()
    {
        T14.SetActive(true);
    }
    void _T14_MethodoOFF()
    {
        T14.SetActive(false);
    }
    //
    void _T15_MethodON()
    {
        T15.SetActive(true);
    }
    void _T15_MethodoOFF()
    {
        T15.SetActive(false);
    }
    //
    void _T16_MethodON()
    {
        T16.SetActive(true);
    }
    void _T16_MethodoOFF()
    {
        T16.SetActive(false);
    }
    //
    void _T17_MethodON()
    {
        T17.SetActive(true);
    }
    void _T17_MethodoOFF()
    {
        T17.SetActive(false);
    }
    //
    void _T18_MethodON()
    {
        T18.SetActive(true);
    }
    void _T18_MethodoOFF()
    {
        T18.SetActive(false);
    }
    //
    void _T19_MethodON()
    {
        T19.SetActive(true);
    }
    void _T19_MethodoOFF()
    {
        T19.SetActive(false);
    }
    void _T20_MethodON()
    {
        T20.SetActive(true);
    }
    void _T20_MethodoOFF()
    {
        T20.SetActive(false);
    }
    //
    void _T21_MethodON()
    {
        T21.SetActive(true);
    }
    void _T21_MethodoOFF()
    {
        T21.SetActive(false);
    }
    //
    void _T22_MethodON()
    {
        T22.SetActive(true);
    }
    void _T22_MethodoOFF()
    {
        T22.SetActive(false);
    }
    //
    void _T23_MethodON()
    {
        T23.SetActive(true);
    }
    void _T23_MethodoOFF()
    {
        T23.SetActive(false);
    }
    //
    void _T24_MethodON()
    {
        T24.SetActive(true);
    }
    void _T24_MethodoOFF()
    {
        T24.SetActive(false);
    }
    //
    void _T25_MethodON()
    {
        T25.SetActive(true);
    }
    void _T25_MethodoOFF()
    {
        T25.SetActive(false);
    }
    //
    void _T26_MethodON()
    {
        T26.SetActive(true);
    }
    void _T26_MethodoOFF()
    {
        T26.SetActive(false);
    }
    //
    void _T27_MethodON()
    {
        T27.SetActive(true);
    }
    void _T27_MethodoOFF()
    {
        T27.SetActive(false);
    }
    //
    void _T28_MethodON()
    {
        T28.SetActive(true);
    }
    void _T28_MethodoOFF()
    {
        T28.SetActive(false);
    }
    //
    void _T29_MethodON()
    {
        T29.SetActive(true);
    }
    void _T29_MethodoOFF()
    {
        T29.SetActive(false);
    }
    //
    void _T30_MethodON()
    {
        T30.SetActive(true);
    }
    void _T30_MethodoOFF()
    {
        T30.SetActive(false);
    }
    //
    void _T31_MethodON()
    {
        T31.SetActive(true);
    }
    void _T31_MethodoOFF()
    {
        T31.SetActive(false);
    }
    //
    void _T32_MethodON()
    {
        T32.SetActive(true);
    }
    void _T32_MethodoOFF()
    {
        T32.SetActive(false);
    }
    //
    void _T33_MethodON()
    {
        T33.SetActive(true);
    }
    void _T33_MethodoOFF()
    {
        T33.SetActive(false);
    }
    //


































    //. Descriptions


    //
    void _D1_MethodON()
    {
        D1.SetActive(true);
    }
    void _D1_MethodoOFF()
    {
        D1.SetActive(false);
    }
    //
    void _D2_MethodON()
    {
        D2.SetActive(true);
    }
    void _D2_MethodoOFF()
    {
        D2.SetActive(false);
    }
    //
    void _D3_MethodON()
    {
        D3.SetActive(true);
    }
    void _D3_MethodoOFF()
    {
        D3.SetActive(false);
    }
    //
    void _D4_MethodON()
    {
        D4.SetActive(true);
    }
    void _D4_MethodoOFF()
    {
        D4.SetActive(false);
    }
    //
    void _D5_MethodON()
    {
        D5.SetActive(true);
    }
    void _D5_MethodoOFF()
    {
        D5.SetActive(false);
    }
    //
    void _D6_MethodON()
    {
        D6.SetActive(true);
    }
    void _D6_MethodoOFF()
    {
        D6.SetActive(false);
    }
    //
    void _D7_MethodON()
    {
        D7.SetActive(true);
    }
    void _D7_MethodoOFF()
    {
        D7.SetActive(false);
    }
    //
    void _D8_MethodON()
    {
        D8.SetActive(true);
    }
    void _D8_MethodoOFF()
    {
        D8.SetActive(false);
    }
    //
    void _D9_MethodON()
    {
        D9.SetActive(true);
    }
    void _D9_MethodoOFF()
    {
        D9.SetActive(false);
    }
    //
    void _D10_MethodON()
    {
        D10.SetActive(true);
    }
    void _D10_MethodoOFF()
    {
        D10.SetActive(false);
    }
    //
    void _D11_MethodON()
    {
        D11.SetActive(true);
    }
    void _D11_MethodoOFF()
    {
        D11.SetActive(false);
    }
    //
    void _D12_MethodON()
    {
        D12.SetActive(true);
    }
    void _D12_MethodoOFF()
    {
        D12.SetActive(false);
    }
    //
    void _D13_MethodON()
    {
        D13.SetActive(true);
    }
    void _D13_MethodoOFF()
    {
        D13.SetActive(false);
    }
    //
    void _D14_MethodON()
    {
        D14.SetActive(true);
    }
    void _D14_MethodoOFF()
    {
        D14.SetActive(false);
    }
    //
    void _D15_MethodON()
    {
        D15.SetActive(true);
    }
    void _D15_MethodoOFF()
    {
        D15.SetActive(false);
    }
    //













    












    
















































}
