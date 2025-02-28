using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_nutrition_in_plants_class_7 : MonoBehaviour
{

    public Transform waypoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypointCanvas.SetActive(true);
        waypoint.player = InventoryManager.Instance.player.transform;     
        waypoint.target = target;
    }

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    private Animator anim;
    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;

    public ObjectiveController objectiveController;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camera animation", 0, targetNormalizedTime);
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
            //case 1: Level2(); break;
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

    //public Collider orchid;
    //public Collider mushroom;
    //public Collider venusflytrap;
    //public Collider Cuscata;

    //public void Level2()
    //{
    //    orchid.enabled = true;
    //    mushroom.enabled = true;
    //    venusflytrap.enabled = true;
    //    Cuscata.enabled = true;
    //    SaveProgress(1, 4, 0);

    //}
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



    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip NUTRINS_OF_PLANTS;
    public AudioClip MODE_OF_NURIATION;
    public AudioClip TYPES_OF_NUTRITION;
    public AudioClip AUTOTROPHS;
    public AudioClip HETEROTRPHS;
    public AudioClip PHOTOSYNTHASYS;
    public AudioClip MAGER_PHOTOSINTHASYS;
    public AudioClip HOW_DO_PLANTS_APTAINED_PLANTS;
    public AudioClip HOW_DO_PLANTS_WOTER_PHO;
    public AudioClip OTHER_MDE_OF_NUTRITION;
    public AudioClip PARASITIC;
    public AudioClip INSECTIVOROUS;
    public AudioClip SAPROPHYTIC;
    public AudioClip SYMBIOTIC;









    // Titles



    public GameObject Mode_of_nutrition_in_plantsT;
    public GameObject AUTOTROPHICT;
    public GameObject HETEROTROPHICT;
    public GameObject PhotosynthesisT;
    public GameObject MAJOR_STEPS_FOR_PHOTOSYNTHESIST;
    public GameObject How_do_plants_obtain_carbon_dioxideT;
    public GameObject How_do_plants_obtain_water_for_photosynthesisT;
    public GameObject Other_modes_of_nutrition_in_plantsT;
    public GameObject Parasitic_mode_of_nutritionT;
    public GameObject Insectivorous_modeT;
    public GameObject Saprophytic_mode_of_nutritionT;
    public GameObject Symbiotic_mode_of_nutritionT;







    public GameObject AUTOTROPHIC;
    public GameObject HETEROTROPHIC;
    public GameObject c_h_o;
    public GameObject c_h_o_text;
    public GameObject ray;
    public GameObject modes_of_nutrition;
    public GameObject parasitic_mode_of_nutrition;
    public GameObject Insectivorous_mode;
    public GameObject saprophytic_mode_of_nutrition;
    public GameObject Symbiotic_mode_onutrition;
    public GameObject guard_cells;





    public GameObject autotrophD;








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




























   
 











    // Audio


    void _NUTRINS_OF_PLANTS_audioMethod()

    {
        myAudio.clip = NUTRINS_OF_PLANTS;
        myAudio.Play();
    }








    


    void _MODE_OF_NURIATION_audioMethod()

    {
        myAudio.clip = MODE_OF_NURIATION;
        myAudio.Play();
    }





   


    void _TYPES_OF_NUTRITION_audioMethod()

    {
        myAudio.clip = TYPES_OF_NUTRITION;
        myAudio.Play();
    }





  


    void _AUTOTROPHS_audioMethod()

    {
        myAudio.clip = AUTOTROPHS;
        myAudio.Play();
    }





  


    void _HETEROTRPHS_audioMethod()

    {
        myAudio.clip = HETEROTRPHS;
        myAudio.Play();
    }




  


    void _PHOTOSYNTHASYS_audioMethod()

    {
        myAudio.clip = PHOTOSYNTHASYS;
        myAudio.Play();
    }







    void _MAGER_PHOTOSINTHASYS_audioMethod()

    {
        myAudio.clip = MAGER_PHOTOSINTHASYS;
        myAudio.Play();
    }






    void _HOW_DO_PLANTS_APTAINED_PLANTS_audioMethod()

    {
        myAudio.clip = HOW_DO_PLANTS_APTAINED_PLANTS;
        myAudio.Play();
    }





    void _OTHER_MDE_OF_NUTRITION_audioMethod()

    {
        myAudio.clip = OTHER_MDE_OF_NUTRITION;
        myAudio.Play();
    }



    void _HOW_DO_PLANTS_WOTER_PHO_audioMethod()

    {
        myAudio.clip = HOW_DO_PLANTS_WOTER_PHO;
        myAudio.Play();
    }






    void _PARASITIC_audioMethod()

    {
        myAudio.clip = PARASITIC;
        myAudio.Play();
    }




    void _INSECTIVOROUS_audioMethod()

    {
        myAudio.clip = INSECTIVOROUS;
        myAudio.Play();
    }






    void _SAPROPHYTIC_audioMethod()

    {
        myAudio.clip = SAPROPHYTIC;
        myAudio.Play();
    }



    void _SYMBIOTIC_audioMethod()

    {
        myAudio.clip = SYMBIOTIC;
        myAudio.Play();
    }

















    // Titles


    void _Mode_of_nutrition_in_plantsTMethodON()
    {
        Mode_of_nutrition_in_plantsT.SetActive(true);
    }

    void _Mode_of_nutrition_in_plantsTMethodOFF()
    {
        Mode_of_nutrition_in_plantsT.SetActive(false);
    }









    void _AUTOTROPHICTMethodON()
    {
        AUTOTROPHICT.SetActive(true);
    }

    void _AUTOTROPHICTMethodOFF()
    {
        AUTOTROPHICT.SetActive(false);
    }








    void _HETEROTROPHICTMethodON()
    {
        HETEROTROPHICT.SetActive(true);
    }

    void _HETEROTROPHICTMethodOFF()
    {
        HETEROTROPHICT.SetActive(false);
    }






    void _PhotosynthesisTMethodON()
    {
        PhotosynthesisT.SetActive(true);
    }

    void _PhotosynthesisTMethodOFF()
    {
        PhotosynthesisT.SetActive(false);
    }







    void _MAJOR_STEPS_FOR_PHOTOSYNTHESISTMethodON()
    {
        MAJOR_STEPS_FOR_PHOTOSYNTHESIST.SetActive(true);
    }

    void _MAJOR_STEPS_FOR_PHOTOSYNTHESISTMethodOFF()
    {
        MAJOR_STEPS_FOR_PHOTOSYNTHESIST.SetActive(false);
    }








    void _How_do_plants_obtain_carbon_dioxideTMethodON()
    {
        How_do_plants_obtain_carbon_dioxideT.SetActive(true);
    }

    void _How_do_plants_obtain_carbon_dioxideTMethodOFF()
    {
        How_do_plants_obtain_carbon_dioxideT.SetActive(false);
    }






    void _How_do_plants_obtain_water_for_photosynthesisTMethodON()
    {
        How_do_plants_obtain_water_for_photosynthesisT.SetActive(true);
    }

    void _How_do_plants_obtain_water_for_photosynthesisTMethodOFF()
    {
        How_do_plants_obtain_water_for_photosynthesisT.SetActive(false);
    }







    void _Other_modes_of_nutrition_in_plantsTMethodON()
    {
        Other_modes_of_nutrition_in_plantsT.SetActive(true);
    }

    void _Other_modes_of_nutrition_in_plantsTMethodOFF()
    {
        Other_modes_of_nutrition_in_plantsT.SetActive(false);
    }







    void _Parasitic_mode_of_nutritionTMethodON()
    {
        Parasitic_mode_of_nutritionT.SetActive(true);
    }

    void _Parasitic_mode_of_nutritionTMethodOFF()
    {
        Parasitic_mode_of_nutritionT.SetActive(false);
    }









    void _Insectivorous_modeTMethodON()
    {
        Insectivorous_modeT.SetActive(true);
    }

    void _Insectivorous_modeTMethodOFF()
    {
        Insectivorous_modeT.SetActive(false);
    }








    void _Saprophytic_mode_of_nutritionTMethodON()
    {
        Saprophytic_mode_of_nutritionT.SetActive(true);
    }

    void _Saprophytic_mode_of_nutritionTMethodOFF()
    {
        Saprophytic_mode_of_nutritionT.SetActive(false);
    }







    void _Symbiotic_mode_of_nutritionTMethodON()
    {
        Symbiotic_mode_of_nutritionT.SetActive(true);
    }

    void _Symbiotic_mode_of_nutritionTMethodOFF()
    {
        Symbiotic_mode_of_nutritionT.SetActive(false);
    }



























    void _AUTOTROPHICMethodON()
    {
        AUTOTROPHIC.SetActive(true);
    }

    void _AUTOTROPHICMethodOFF()
    {
        AUTOTROPHIC.SetActive(false);
    }







    void _HETEROTROPHICMethodON()
    {
        HETEROTROPHIC.SetActive(true);
    }

    void _HETEROTROPHICMethodOFF()
    {
        HETEROTROPHIC.SetActive(false);
    }








    void _c_h_oMethodON()
    {
        c_h_o.SetActive(true);
    }

    void _c_h_oMethodOFF()
    {
        c_h_o.SetActive(false);
    }







    void _c_h_o_textMethodON()
    {
        c_h_o_text.SetActive(true);
    }

    void _c_h_o_textMethodOFF()
    {
        c_h_o_text.SetActive(false);
    }








    void _rayMethodON()
    {
        ray.SetActive(true);
    }

    void _rayMethodOFF()
    {
        ray.SetActive(false);
    }







    void _modes_of_nutritionMethodON()
    {
        modes_of_nutrition.SetActive(true);
    }

    void _modes_of_nutritionMethodOFF()
    {
        modes_of_nutrition.SetActive(false);
    }






    void _parasitic_mode_of_nutritionMethodON()
    {
        parasitic_mode_of_nutrition.SetActive(true);
    }

    void _parasitic_mode_of_nutritionMethodOFF()
    {
        parasitic_mode_of_nutrition.SetActive(false);
    }








    void _Insectivorous_modeMethodON()
    {
        Insectivorous_mode.SetActive(true);
    }

    void _Insectivorous_modeMethodOFF()
    {
        Insectivorous_mode.SetActive(false);
    }








    void _saprophytic_mode_of_nutritionMethodON()
    {
        saprophytic_mode_of_nutrition.SetActive(true);
    }

    void _saprophytic_mode_of_nutritionMethodOFF()
    {
        saprophytic_mode_of_nutrition.SetActive(false);
    }









    void _Symbiotic_mode_onutritionMethodON()
    {
        Symbiotic_mode_onutrition.SetActive(true);
    }

    void _Symbiotic_mode_onutritionMethodOFF()
    {
        Symbiotic_mode_onutrition.SetActive(false);
    }









    void _guard_cellsMethodON()
    {
        guard_cells.SetActive(true);
    }

    void _guard_cellsMethodOFF()
    {
        guard_cells.SetActive(false);
    }








    void _autotrophDMethodON()
    {
        autotrophD.SetActive(true);
    }

    void _autotrophDMethodOFF()
    {
        autotrophD .SetActive(false);
    }


    private int pipeCount = 8;
    private int currentPipeCount = 0;
    public GameObject timeCrystal;

    public void CheckRotatblePipes()
    {
        currentPipeCount++;
        Debug.Log("PIPE ROTATED " + currentPipeCount);
        if (currentPipeCount == pipeCount)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            timeCrystal.SetActive(true);
        }
    }

    public GameObject correctGlass;
    public GameObject brokenGlass;
    public BoxCollider maskOnj;
    public BoxCollider co2;
    public BoxCollider o2Obj;
    public void GlassBreak()
    {
        maskOnj.isTrigger = true;
        co2.isTrigger = true;
        correctGlass.SetActive(false);
        brokenGlass.SetActive(true);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public GameObject mask;
    public void WearMask()
    {
        mask.SetActive(false);
        InventoryManager.Instance.player.mask.SetActive(true);
    }

    public void ReleaseCO2()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    private BoxCollider doorCol;
    public void ReleaseO2()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        doorCol.isTrigger = true;
    }

    public void CrystalObjUse(Animator anim)
    {
        anim.SetTrigger("Grow");
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Transform outSideSpawnPoint;
    public void SpawnOutside()
    {
        waypointCanvas.SetActive(false);
        InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().FadeInAndOut();
        Invoke("Outside", 0.5f);
    }

    public void Outside()
    {
        InventoryManager.Instance.player.ChangePosition(outSideSpawnPoint);
        //InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        //InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
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
    public void StepComp()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();

    }
















}
