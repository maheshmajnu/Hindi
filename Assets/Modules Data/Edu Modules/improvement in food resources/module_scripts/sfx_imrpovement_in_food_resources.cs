using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_imrpovement_in_food_resources : MonoBehaviour
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
            //case 1: Level5Spawnpoint(); break;
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


  

    private Animator anim;

    public Transform waypoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypointCanvas.SetActive(true);
        waypoint.player = InventoryManager.Instance.player.transform;
        waypoint.target = target;
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

        SetWayPoint(waypoint1);
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
        yield return new WaitForSeconds(1);
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

    
    public void SpawnPointTransform(Transform SpawnPoint)
    {
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        
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

    public void Level9()
    {
        StartCoroutine(DelayLv9MiniGameStart());
        lv8MiniGame.EndMiniGame();
        

    }
    IEnumerator DelayLv9MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        SpawnPointTransform(SP9);
    }

    public Transform SP9;

    private int handpick;
    public void HandPick(GameObject weedPlant)
    {

            handpick++;
            weedPlant.SetActive(false);

            if (handpick == 14)
            {
                handpick = 0;
                StepComplete();
            }
        
    }

    private GameObject pestiSideBag;
    public void WearPestiside(GameObject obj)
    {
        pestiSideBag = obj;
        hasCollectedPesticide = true;
        obj.GetComponent<BoxCollider>().enabled = false;
        obj.transform.SetParent(InventoryManager.Instance.player.bagHolder);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        StepComplete();
    }

    private int weedCount;
    private bool hasCollectedPesticide = false;
    public void DestroyWeedPlants(GameObject weedPlant)
    {
        if (hasCollectedPesticide)
        {
            weedCount++;
            weedPlant.SetActive(false);

            if (weedCount == 4)
            {
                weedCount = 0;
                pestiSideBag.SetActive(false);
                Level8();
                StepComplete();
            }
        }

    }
    //public GameObject lv5a;
    //public GameObject lv5b;

    //public void Level5()
    //{
    //    index++;


    //    if (index == 6)
    //    {
    //        index = 0;
    //        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    //        lv5a.SetActive(false);
    //        lv5b.SetActive(true);
    //        //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    //    }
    //}

    //public GameObject lv6;

    //public void Level5B()
    //{
    //    index++;


    //    if (index == 7)
    //    {
    //        index = 0;
    //        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    //        lv5b.SetActive(false);
    //        lv6.SetActive(true);
    //        //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    //    }
    //}
    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject title_1;
    
    public GameObject title_2a;
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
    public GameObject des_15;
    public GameObject des_16;
    public GameObject des_17;
    public GameObject des_18;
    public GameObject des_19;
    public GameObject des_20;
    public GameObject des_21;
    public GameObject des_22;
    public GameObject des_23;
    public GameObject des_24;
    public GameObject des_25;
    public GameObject des_26;
    public GameObject des_27;
    public GameObject des_28;
    public GameObject des_29;
    public GameObject des_30;
    public GameObject des_31;
    public GameObject des_32;
    public GameObject des_33;
    public GameObject des_34;
    public GameObject des_35;
    public GameObject des_36;
    public GameObject des_37;
    public GameObject des_38;
    public GameObject des_39;
    public GameObject des_40;
    public GameObject des_41;
    public GameObject des_42;
    public GameObject des_43;
    public GameObject des_44;
    public GameObject texts;
    public GameObject clouds;
    public GameObject label_1;
    public GameObject label_2;
    public GameObject crops_yield;
    public GameObject wob;
    public GameObject subdes_1;
    public GameObject subdes_2;
    public GameObject subdes_3;
    public GameObject subdes_4;
    public GameObject subdes_5;
    public GameObject subdes_6;
    public GameObject subdes_7;
    public GameObject subdes_8;
    public GameObject subdes_9;
    public GameObject subdes_10;
    public GameObject bi;


    public GameObject fishone;
    public GameObject fishtwo;
    public GameObject fishthree;


    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip intro1;
    public AudioClip intro2;
    public AudioClip intro3;
    public AudioClip intro4;
    public AudioClip intro5;
    public AudioClip intro6;
    public AudioClip intro7;
    public AudioClip improvementincrops_title;
    public AudioClip improvementincrops_intro1;
    public AudioClip improvementincrops_intro2;
    public AudioClip improvementincrops_intro3;
    public AudioClip improvementincrops_intro4;
    public AudioClip improvementincrops_intro5;
    public AudioClip rabiandkharifcrops_title;
    public AudioClip rabiandkharifcrops_intro1;
    public AudioClip kharifcrops_intro;
    public AudioClip rabicrops_intro;
    public AudioClip stagesofframingpratices;
    public AudioClip classificationofimprovingcrops;
    public AudioClip cropvarietyimprovement_title;
    public AudioClip cropvarietyimprovement_intro1;
    public AudioClip cropvarietyimprovement_intro2;
    public AudioClip cropvarietyimprovement_intro3;
    public AudioClip Factorsforcropvarietyimprovement_title;
    public AudioClip Factorsforcropvarietyimprovement_intro;
    public AudioClip Higheryield_title;
    public AudioClip Higheryield_intro;
    public AudioClip Improvedquality_title;
    public AudioClip Improvedquality_intro;
    public AudioClip Bioticandabioticresistance_title;
    public AudioClip Bioticandabioticresistance_intro;
    public AudioClip Changeinmaturityduration_title;
    public AudioClip Changeinmaturityduration_intro;
    public AudioClip Wideradaptability_title;
    public AudioClip Wideradaptability_intro;
    public AudioClip Desirableagronomiccharacteristics_title;
    public AudioClip Desirableagronomiccharacteristics_intro;
    public AudioClip Cropproductionmanagement_title;
    public AudioClip Cropproductionmanagement_intro1;
    public AudioClip Cropproductionmanagement_intro2;
    public AudioClip Nutrientmanagement_title;
    public AudioClip Nutrientmanagement_intro;
    public AudioClip manure_title;
    public AudioClip manure_intro1;
    public AudioClip manure_intro2;
    public AudioClip compost_title;
    public AudioClip compost_intro;
    public AudioClip vermicompost_title;
    public AudioClip vermicompost_intro;
    public AudioClip greenmanure_title;
    public AudioClip greenmanure_intro;
    public AudioClip Fertilizers_title; 
    public AudioClip Fertilizers_intro1;
    public AudioClip Fertilizers_intro2;
    public AudioClip organicfarming_title;
    public AudioClip organicfarming_intro;
    public AudioClip irrigation_title;
    public AudioClip irrigation_intro1;
    public AudioClip irrigation_intro2;
    public AudioClip wells;
    public AudioClip canals;
    public AudioClip riverliftsystem;
    public AudioClip tanks;
    public AudioClip croppingpattern;
    public AudioClip mixedcropping;
    public AudioClip intercropping;
    public AudioClip croprotation;
    public AudioClip cropprotectionmanagement_intro1;
    public AudioClip cropprotectionmanagement_intro2;
    public AudioClip cropprotectionmanagement_intro3;
    public AudioClip cropprotectionmanagement_intro4;
    public AudioClip cropprotectionmanagement_intro5;
    public AudioClip cropprotectionmanagement_intro6;
    public AudioClip cropprotectionmanagement_intro7;
    public AudioClip storageofgrains_intro1;
    public AudioClip storageofgrains_intro2;
    public AudioClip storageofgrains_intro3;
    public AudioClip animalhusbandry;
    public AudioClip cattlefarming_intro1;
    public AudioClip cattlefarming_intro2;
    public AudioClip cattlefarming_intro3;
    public AudioClip cattlefarming_intro4;
    public AudioClip cattlefarming_intro5;
    public AudioClip cattlefarming_intro6;






    public AudioClip audio01;

    public AudioClip audio02;
    public AudioClip audio03;
    public AudioClip audio04;
    public AudioClip audio05;
    public AudioClip audio06;
    public AudioClip audio07;
    public AudioClip audio08;
    public AudioClip audio09;
    public AudioClip audio10;

    public AudioClip audio11;
   
    public AudioClip audio12;
    public AudioClip audio13;
    public AudioClip audio14;
    public AudioClip audio15;
    public AudioClip audio16;
    public AudioClip audio17;
    public AudioClip audio18;
    public AudioClip audio19;
    public AudioClip audio20;

    public AudioClip audio21;
    public AudioClip audio22;
    public AudioClip audio23;
    public AudioClip audio24;
    public AudioClip audio25;
    public AudioClip audio26;
    public AudioClip audio27;
    public AudioClip audio28;
    public AudioClip audio29;
    public AudioClip audio30;



    public AudioClip audio31;
    public AudioClip audio32;
    public AudioClip audio33;
    public AudioClip audio34;





































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



    //Part 2 Models Methods


    void _Fish1_MethodON()
    {
        fishone.SetActive(true);
    }

    void _Fish1_5MethodOFF()
    {
        fishone.SetActive(false);
    }

    //


    void _Fish2_MethodON()
    {
        fishtwo.SetActive(true);
    }

    void _Fish2_5MethodOFF()
    {
        fishtwo.SetActive(false);
    }

    //


    void _Fish3_MethodON()
    {
        fishthree.SetActive(true);
    }

    void _Fish3_MethodOFF()
    {
        fishthree.SetActive(false);
    }






    //Part2 Audios


    void _audio01_audioMethod()

    {
        myAudio.clip = audio01;
        myAudio.Play();
    }



    void _audio02_audioMethod()

    {
        myAudio.clip = audio02;
        myAudio.Play();
    }

    void _audio03_audioMethod()

    {
        myAudio.clip = audio03;
        myAudio.Play();
    }

    void _audio04_audioMethod()

    {
        myAudio.clip = audio04;
        myAudio.Play();
    }

    void _audio05_audioMethod()

    {
        myAudio.clip = audio05;
        myAudio.Play();
    }


    void _audio06_audioMethod()

    {
        myAudio.clip = audio06;
        myAudio.Play();
    }

    void _audio07_audioMethod()

    {
        myAudio.clip = audio07;
        myAudio.Play();
    }


    void _audio08_audioMethod()

    {
        myAudio.clip = audio08;
        myAudio.Play();
    }


    void _audio09_audioMethod()

    {
        myAudio.clip = audio09;
        myAudio.Play();
    }

    void _audio10_audioMethod()

    {
        myAudio.clip = audio10;
        myAudio.Play();
    }


    void _audio11_audioMethod()

    {
        myAudio.clip = audio11;
        myAudio.Play();
    }


















    void _audio12_audioMethod()

    {
        myAudio.clip = audio12;
        myAudio.Play();
    }

    void _audio13_audioMethod()

    {
        myAudio.clip = audio13;
        myAudio.Play();
    }


    void _audio14_audioMethod()

    {
        myAudio.clip = audio14;
        myAudio.Play();
    }


    void _audio15_audioMethod()

    {
        myAudio.clip = audio15;
        myAudio.Play();
    }


    void _audio16_audioMethod()

    {
        myAudio.clip = audio16;
        myAudio.Play();
    }



    void _audio17_audioMethod()

    {
        myAudio.clip = audio17;
        myAudio.Play();
    }



    void _audio18_audioMethod()

    {
        myAudio.clip = audio18;
        myAudio.Play();
    }



    void _audio19_audioMethod()

    {
        myAudio.clip = audio19;
        myAudio.Play();
    }




    void _audio20_audioMethod()

    {
        myAudio.clip = audio20;
        myAudio.Play();
    }





    void _audio21_audioMethod()

    {
        myAudio.clip = audio21;
        myAudio.Play();
    }

    void _audio22_audioMethod()

    {
        myAudio.clip = audio22;
        myAudio.Play();
    }

    void _audio23_audioMethod()

    {
        myAudio.clip = audio23;
        myAudio.Play();
    }

    void _audio24_audioMethod()

    {
        myAudio.clip = audio24;
        myAudio.Play();
    }

    void _audio25_audioMethod()

    {
        myAudio.clip = audio25;
        myAudio.Play();
    }

    void _audio26_audioMethod()

    {
        myAudio.clip = audio26;
        myAudio.Play();
    }

    void _audio27_audioMethod()

    {
        myAudio.clip = audio27;
        myAudio.Play();
    }

    void _audio28_audioMethod()

    {
        myAudio.clip = audio28;
        myAudio.Play();
    }

    void _audio29_audioMethod()

    {
        myAudio.clip = audio29;
        myAudio.Play();
    }

    void _audio30_audioMethod()

    {
        myAudio.clip = audio30;
        myAudio.Play();
    }







    void _audio31_audioMethod()

    {
        myAudio.clip = audio31;
        myAudio.Play();
    }
    void _audio32_audioMethod()

    {
        myAudio.clip = audio32;
        myAudio.Play();
    }
    void _audio33_audioMethod()

    {
        myAudio.clip = audio33;
        myAudio.Play();
    }
    void _audio34_audioMethod()

    {
        myAudio.clip = audio34;
        myAudio.Play();
    }



































    void _title_2aMethodON()
    {
        title_2a.SetActive(true);
    }

    void _title_2aMethodOFF()
    {
        title_2a.SetActive(false);
    }















    void _title_5MethodON()
    {
        title_5.SetActive(true);
    }

    void _title_5MethodOFF()
    {
        title_5.SetActive(false);
    }





    void _title_6MethodON()
    {
        title_6.SetActive(true);
    }

    void _title_6MethodOFF()
    {
        title_6.SetActive(false);
    }





    void _title_7MethodON()
    {
        title_7.SetActive(true);
    }

    void _title_7MethodOFF()
    {
        title_7.SetActive(false);
    }





    void _title_8MethodON()
    {
        title_8.SetActive(true);
    }

    void _title_8MethodOFF()
    {
        title_8.SetActive(false);
    }





    void _title_9MethodON()
    {
        title_9.SetActive(true);
    }

    void _title_9MethodOFF()
    {
        title_9.SetActive(false);
    }





    void _title_10MethodON()
    {
        title_10.SetActive(true);
    }

    void _title_10MethodOFF()
    {
        title_10.SetActive(false);
    }





    void _title_11MethodON()
    {
        title_11.SetActive(true);
    }

    void _title_11MethodOFF()
    {
        title_11.SetActive(false);
    }





    void _title_12MethodON()
    {
        title_12.SetActive(true);
    }

    void _title_12MethodOFF()
    {
        title_12.SetActive(false);
    }





    void _title_13MethodON()
    {
        title_13.SetActive(true);
    }

    void _title_13MethodOFF()
    {
        title_13.SetActive(false);
    }





    void _title_14MethodON()
    {
        title_14.SetActive(true);
    }

    void _title_14MethodOFF()
    {
        title_14.SetActive(false);
    }





    void _title_15MethodON()
    {
        title_15.SetActive(true);
    }

    void _title_15MethodOFF()
    {
        title_15.SetActive(false);
    }





    void _title_16MethodON()
    {
        title_16.SetActive(true);
    }

    void _title_16MethodOFF()
    {
        title_16.SetActive(false);
    }





    void _title_17MethodON()
    {
        title_17.SetActive(true);
    }

    void _title_17MethodOFF()
    {
        title_17.SetActive(false);
    }





    void _title_18MethodON()
    {
        title_18.SetActive(true);
    }

    void _title_18MethodOFF()
    {
        title_18.SetActive(false);
    }





    void _title_19MethodON()
    {
        title_19.SetActive(true);
    }

    void _title_19MethodOFF()
    {
        title_19.SetActive(false);
    }





    void _title_20MethodON()
    {
        title_20.SetActive(true);
    }

    void _title_20MethodOFF()
    {
        title_20.SetActive(false);
    }





    void _title_21MethodON()
    {
        title_21.SetActive(true);
    }

    void _title_21MethodOFF()
    {
        title_21.SetActive(false);
    }





    void _title_22MethodON()
    {
        title_22.SetActive(true);
    }

    void _title_22MethodOFF()
    {
        title_22.SetActive(false);
    }






    void _title_23MethodON()
    {
        title_23.SetActive(true);
    }

    void _title_23MethodOFF()
    {
        title_23.SetActive(false);
    }


































    void title_1_MethodON()
    {
        title_1.SetActive(true);
    }
    //
 
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
    void des_15_MethodON()
    {
        des_15.SetActive(true);
    }
    //
    void des_16_MethodON()
    {
        des_16.SetActive(true);
    }
    //
    void des_17_MethodON()
    {
        des_17.SetActive(true);
    }
    //
    void des_18_MethodON()
    {
        des_18.SetActive(true);
    }
    //
    void des_19_MethodON()
    {
        des_19.SetActive(true);
    }
    //
    void des_20_MethodON()
    {
        des_20.SetActive(true);
    }
    //
    void des_21_MethodON()
    {
        des_21.SetActive(true);
    }
    //
    void des_22_MethodON()
    {
        des_22.SetActive(true);
    }
    //
    void des_23_MethodON()
    {
        des_23.SetActive(true);
    }
    //
    void des_24_MethodON()
    {
        des_24.SetActive(true);
    }
    //
    void des_25_MethodON()
    {
        des_25.SetActive(true);
    }
    //
    void des_26_MethodON()
    {
        des_26.SetActive(true);
    }
    //
    void des_27_MethodON()
    {
        des_27.SetActive(true);
    }
    //
    void des_28_MethodON()
    {
        des_28.SetActive(true);
    }
    //
    void des_29_MethodON()
    {
        des_29.SetActive(true);
    }
    //
    void des_30_MethodON()
    {
        des_30.SetActive(true);
    }
    //
    void des_31_MethodON()
    {
        des_31.SetActive(true);
    }
    //
    void des_32_MethodON()
    {
        des_32.SetActive(true);
    }
    //
    void des_33_MethodON()
    {
        des_33.SetActive(true);
    }
    //
    void des_34_MethodON()
    {
        des_34.SetActive(true);
    }
    //
    void des_35_MethodON()
    {
        des_35.SetActive(true);
    }
    //
    void des_36_MethodON()
    {
        des_36.SetActive(true);
    }
    //
    void des_37_MethodON()
    {
        des_37.SetActive(true);
    }
    //
    void des_38_MethodON()
    {
        des_38.SetActive(true);
    }
    //
    void des_39_MethodON()
    {
        des_39.SetActive(true);
    }
    //
    void des_40_MethodON()
    {
        des_40.SetActive(true);
    }
    //
    void des_41_MethodON()
    {
        des_41.SetActive(true);
    }
    //
    void des_42_MethodON()
    {
        des_42.SetActive(true);
    }
    //
    void des_43_MethodON()
    {
        des_43.SetActive(true);
    }
    //
    void des_44_MethodON()
    {
        des_44.SetActive(true);

    }
    //
    void subdes_1_MethodON()
    {
        subdes_1.SetActive(true);
    }
    //
    void subdes_2_MethodON()
    {
        subdes_2.SetActive(true);
    }
    //
    void subdes_3_MethodON()
    {
        subdes_3.SetActive(true);
    }
    //
    void subdes_4_MethodON()
    {
        subdes_4.SetActive(true);
    }
    //
    void subdes_5_MethodON()
    {
        subdes_5.SetActive(true);
    }
    //
    void subdes_6_MethodON()
    {
        subdes_6.SetActive(true);
    }
    //
    void subdes_7_MethodON()
    {
        subdes_7.SetActive(true);
    }
    //
    void subdes_8_MethodON()
    {
        subdes_8.SetActive(true);
    }
    //
    void subdes_9_MethodON()
    {
        subdes_9.SetActive(true);
    }
    //
    void subdes_10_MethodON()
    {
        subdes_10.SetActive(true);
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
    void clouds_MethodON()
    {
        clouds.SetActive(true);
    }

    void clouds_MethodOFF()
    {
        clouds.SetActive(false);
    }
    //
    void label_1_MethodON()
    {
        label_1.SetActive(true);
    }

    void label_1_MethodOFF()
    {
        label_1.SetActive(false);
    }
    //
    void label_2_MethodON()
    {
        label_2.SetActive(true);
    }

    void label_2_MethodOFF()
    {
        label_2.SetActive(false);
    }
    //
    void cropsyield_MethodON()
    {
        crops_yield.SetActive(true);
    }

    void cropsyield_MethodOFF()
    {
        crops_yield.SetActive(false);
    }
    //
    void wob_MethodON()
    {
        wob.SetActive(true);
    }

    void wob_MethodOFF()
    {
        wob.SetActive(false);
    }
    //
    void bi_MethodON()
    {
        bi.SetActive(true);
    }

    void bi_MethodOFF()
    {
        bi.SetActive(false);
    }
    // 









    //audios

    //
    void _intro1_audioMethod()

    {
        myAudio.clip = intro1;
        myAudio.Play();
    }
    //
    void _intro2_audioMethod()

    {
        myAudio.clip = intro2;
        myAudio.Play();
    }
    //
    void _intro3_audioMethod()

    {
        myAudio.clip = intro3;
        myAudio.Play();
    }
    //
    void _intro4_audioMethod()

    {
        myAudio.clip = intro4;
        myAudio.Play();
    }
    //
    void _intro5_audioMethod()

    {
        myAudio.clip = intro5;
        myAudio.Play();
    }
    //
    void _intro6_audioMethod()

    {
        myAudio.clip = intro6;
        myAudio.Play();
    }
    //
    void _intro7_audioMethod()

    {
        myAudio.clip = intro7;
        myAudio.Play();
    }
    //
    void _improvementincropyields_title_audioMethod()

    {
        myAudio.clip = improvementincrops_title;
        myAudio.Play();
    }
    //
    void _improvementincropyields_intro1_audioMethod()

    {
        myAudio.clip = improvementincrops_intro1;
        myAudio.Play();
    }
    //
    void _improvementincropyields_intro2_audioMethod()

    {
        myAudio.clip = improvementincrops_intro2;
        myAudio.Play();
    }
    //
    void _improvementincropyields_intro3_audioMethod()

    {
        myAudio.clip = improvementincrops_intro3;
        myAudio.Play();
    }
    //
    void _improvementincropyields_intro4_audioMethod()

    {
        myAudio.clip = improvementincrops_intro4;
        myAudio.Play();
    }
    //
    void _improvementincropyields_intro5_audioMethod()

    {
        myAudio.clip = improvementincrops_intro5;
        myAudio.Play();
    }
    //
    void _rabiandkharifcrops_title_audioMethod()

    {
        myAudio.clip = rabiandkharifcrops_title;
        myAudio.Play();
    }
    //
    void _rabiandkharifcrops_intro1_audioMethod()

    {
        myAudio.clip = rabiandkharifcrops_intro1;
        myAudio.Play();
    }
    //
    void _kharifcrops_intro_audioMethod()

    {
        myAudio.clip = kharifcrops_intro;
        myAudio.Play();
    }
    //
    void _rabicrops_intro_audioMethod()

    {
        myAudio.clip = rabicrops_intro;
        myAudio.Play();
    }
    //
    void _stagingoffarmingpartice_audioMethod()

    {
        myAudio.clip = stagesofframingpratices;
        myAudio.Play();
    }
    //
    void _classificationofimprovingcrops_audioMethod()

    {
        myAudio.clip = classificationofimprovingcrops;
        myAudio.Play();
    }
    //
    void _cropvariety_title_audioMethod()

    {
        myAudio.clip = cropvarietyimprovement_title;
        myAudio.Play();
    }
    //
    void _cropvariety_intro1_audioMethod()

    {
        myAudio.clip = cropvarietyimprovement_intro1;
        myAudio.Play();
    }
    //
    void _cropvariety_intro2_audioMethod()

    {
        myAudio.clip = cropvarietyimprovement_intro2;
        myAudio.Play();
    }
    //
    void _cropvariety_intro3_audioMethod()

    {
        myAudio.clip = cropvarietyimprovement_intro3;
        myAudio.Play();
    }
    //
    void Factorsforcropvarietyimprovement_title_audioMethod()

    {
        myAudio.clip = Factorsforcropvarietyimprovement_title;
        myAudio.Play();
    }
    //
    void Factorsforcropvarietyimprovement_intro_audioMethod()

    {
        myAudio.clip = Factorsforcropvarietyimprovement_intro;
        myAudio.Play();
    }
    //
    void Higheryield_title_audioMethod()

    {
        myAudio.clip = Higheryield_title;
        myAudio.Play();
    }
    //
    void Higheryield_intro_audioMethod()

    {
        myAudio.clip = Higheryield_intro;
        myAudio.Play();
    }
    //
    void Improvedquality_title_audioMethod()

    {
        myAudio.clip = Improvedquality_title;
        myAudio.Play();
    }
    //
    void Improvedquality_intro_audioMethod()

    {
        myAudio.clip = Improvedquality_intro;
        myAudio.Play();
    }
    //
    void Bioticandabioticresistance_title_audioMethod()

    {
        myAudio.clip = Bioticandabioticresistance_title;
        myAudio.Play();
    }
    //
    void Bioticandabioticresistance_intro_audioMethod()

    {
        myAudio.clip = Bioticandabioticresistance_intro;
        myAudio.Play();
    }
    //
    void Changeinmaturityduration_title_audioMethod()

    {
        myAudio.clip = Changeinmaturityduration_title;
        myAudio.Play();
    }
    //
    void Changeinmaturityduration_intro_audioMethod()

    {
        myAudio.clip = Changeinmaturityduration_intro;
        myAudio.Play();
    }
    //
    void Wideradaptability_title_audioMethod()

    {
        myAudio.clip = Wideradaptability_title;
        myAudio.Play();
    }
    //
    void Wideradaptability_intro_audioMethod()

    {
        myAudio.clip = Wideradaptability_intro;
        myAudio.Play();
    }
    //
    void Desirableagronomiccharacteristics_title_audioMethod()

    {
        myAudio.clip = Desirableagronomiccharacteristics_title;
        myAudio.Play();
    }
    //
    void Desirableagronomiccharacteristics_intro_audioMethod()

    {
        myAudio.clip = Desirableagronomiccharacteristics_intro;
        myAudio.Play();
    }
    //
    void Cropproductionmanagement_title_audioMethod()

    {
        myAudio.clip = Cropproductionmanagement_title;
        myAudio.Play();
    }
    //
    void Cropproductionmanagement_intro1_audioMethod()

    {
        myAudio.clip = Cropproductionmanagement_intro1;
        myAudio.Play();
    }
    //
    void Cropproductionmanagement_intro2_audioMethod()

    {
        myAudio.clip = Cropproductionmanagement_intro2;
        myAudio.Play();
    }
    //
    void Nutrientmanagement_title_audioMethod()

    {
        myAudio.clip = Nutrientmanagement_title;
        myAudio.Play();
    }
    //
    void Nutrientmanagement_intro_audioMethod()

    {
        myAudio.clip = Nutrientmanagement_intro;
        myAudio.Play();
    }
    //
    void manure_title_audioMethod()

    {
        myAudio.clip = manure_title;
        myAudio.Play();
    }
    //
    void manure_intro1_audioMethod()

    {
        myAudio.clip = manure_intro1;
        myAudio.Play();
    }
    //
    void manure_intro2_audioMethod()

    {
        myAudio.clip = manure_intro2;
        myAudio.Play();
    }
    //
    void compost_title_audioMethod()

    {
        myAudio.clip = compost_title;
        myAudio.Play();
    }
    //
    void compost_intro_audioMethod()

    {
        myAudio.clip = compost_intro;
        myAudio.Play();
    }
    //
    void vermicompost_title_audioMethod()

    {
        myAudio.clip = vermicompost_title;
        myAudio.Play();
    }
    //
    void vermicompost_intro_audioMethod()

    {
        myAudio.clip = vermicompost_intro;
        myAudio.Play();
    }
    //
    void greenmanure_title_audioMethod()

    {
        myAudio.clip = greenmanure_title;
        myAudio.Play();
    }
    //
    void greenmanure_intro_audioMethod()

    {
        myAudio.clip = greenmanure_intro;
        myAudio.Play();
    }
    //
    void fertilizers_title_audioMethod()

    {
        myAudio.clip = Fertilizers_title;
        myAudio.Play();
    }
    //
    void fertilizers_intro1_audioMethod()

    {
        myAudio.clip = Fertilizers_intro1;
        myAudio.Play();
    }
    //
    void fertilizers_intro2_audioMethod()

    {
        myAudio.clip = Fertilizers_intro2;
        myAudio.Play();
    }
    //
    void organicfarming_title_audioMethod()

    {
        myAudio.clip = organicfarming_title;
        myAudio.Play();
    }
    //
    void organicfarming_intro_audioMethod()

    {
        myAudio.clip = organicfarming_intro;
        myAudio.Play();
    }
    //
    void irrigation_title_audioMethod()

    {
        myAudio.clip = irrigation_title;
        myAudio.Play();
    }
    //
    void irrigation_intro1_audioMethod()

    {
        myAudio.clip = irrigation_intro1;
        myAudio.Play();
    }
    //
    void irrigation_intro2_audioMethod()

    {
        myAudio.clip = irrigation_intro2;
        myAudio.Play();
    }
    //
    void wells_audioMethod()

    {
        myAudio.clip = wells;
        myAudio.Play();
    }
    //
    void canals_audioMethod()

    {
        myAudio.clip = canals;
        myAudio.Play();
    }
    //
    void riverliftsystem_audioMethod()

    {
        myAudio.clip = riverliftsystem;
        myAudio.Play();
    }
    //
    void tanks_audioMethod()

    {
        myAudio.clip = tanks;
        myAudio.Play();
    }
    //
    void croppingpatterns_audioMethod()

    {
        myAudio.clip = croppingpattern;
        myAudio.Play();
    }
    //
    void mixedcropping_audioMethod()

    {
        myAudio.clip = mixedcropping;
        myAudio.Play();
    }
    //
    void intercropping_audioMethod()

    {
        myAudio.clip = intercropping;
        myAudio.Play();
    }
    //
    void croprotation_audioMethod()

    {
        myAudio.clip = croprotation;
        myAudio.Play();
    }
    //
    void cropprotectionmanagement_intro1_audioMethod()

    {
        myAudio.clip = cropprotectionmanagement_intro1;
        myAudio.Play();
    }
    //
    void cropprotectionmanagement_intro2_audioMethod()

    {
        myAudio.clip = cropprotectionmanagement_intro2;
        myAudio.Play();
    }
    //
    void cropprotectionmanagement_intro3_audioMethod()

    {
        myAudio.clip = cropprotectionmanagement_intro3;
        myAudio.Play();
    }
    //
    void cropprotectionmanagement_intro4_audioMethod()

    {
        myAudio.clip = cropprotectionmanagement_intro4;
        myAudio.Play();
    }
    //
    void cropprotectionmanagement_intro5_audioMethod()

    {
        myAudio.clip = cropprotectionmanagement_intro5;
        myAudio.Play();
    }
    //
    void cropprotectionmanagement_intro6_audioMethod()

    {
        myAudio.clip = cropprotectionmanagement_intro6;
        myAudio.Play();
    }
    //
    void cropprotectionmanagement_intro7_audioMethod()

    {
        myAudio.clip = cropprotectionmanagement_intro7;
        myAudio.Play();
    }
    //
    void storgaeofgrains_intro1_audioMethod()

    {
        myAudio.clip = storageofgrains_intro1;
        myAudio.Play();
    }
    //
    void storgaeofgrains_intro2_audioMethod()

    {
        myAudio.clip = storageofgrains_intro2;
        myAudio.Play();
    }
    //
    void storgaeofgrains_intro3_audioMethod()

    {
        myAudio.clip = storageofgrains_intro3;
        myAudio.Play();
    }
    //
    void animalhusbandry_audioMethod()

    {
        myAudio.clip = animalhusbandry;
        myAudio.Play();
    }
    //
    void cattlefarming_intro1_audioMethod()

    {
        myAudio.clip = storageofgrains_intro3;
        myAudio.Play();
    }
    //


















































}