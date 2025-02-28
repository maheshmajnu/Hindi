using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sfx_life_processes_10_class : MonoBehaviour
{

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

        InitializeFromCheckpoint();
        level();
    }

    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;


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
    public AudioClip audio35;
    public AudioClip audio36;
    public AudioClip audio37;
    public AudioClip audio38;
    public AudioClip audio39;
    public AudioClip audio40;



    public AudioClip audio41;
    public AudioClip audio42;
    public AudioClip audio43;
    public AudioClip audio44;
    public AudioClip audio45;
    public AudioClip audio46;
    public AudioClip audio47;
    public AudioClip audio48;
    public AudioClip audio49;
    public AudioClip audio50;







    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject liver;
    public GameObject mouth;
    public GameObject pancreas;
    public GameObject large_intestine;
    public GameObject anus;
    public GameObject nostrils;


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






















    // Titles



    public GameObject Life_ProcessesT;
    public GameObject NutritionT;
    public GameObject autotrophic_nutritionT;
    public GameObject Hete_nutritionsT;
    public GameObject Saprophytic_nutritionT;
    public GameObject Parasitic_nutritionT;
    public GameObject Holozoic_nutritionT;
    public GameObject nutrition_in_human_beingsT;
    public GameObject emulsification_of_fatsT;
    public GameObject absorption_of_nutrientsT;
    public GameObject RespirationT;
    public GameObject Anaerobic_respirationT;
    public GameObject alcoholic_fermentationT;
    public GameObject human_respiratory_systemT;
    public GameObject exchange_of_gasesT;
    public GameObject stomachT;





    public GameObject nutritionsD;
    public GameObject photosynthasisD;
    public GameObject Exchange_of_gasesD;
    public GameObject carbon_dioxideD;
    public GameObject chloroplastD;
    public GameObject ChlorophyllD;
    public GameObject small_intestineD;
    public GameObject pancreatic_juiceD;
    public GameObject bile_juiceD;
    public GameObject trypsinD;
    public GameObject ATP_D;
    public GameObject alcoholic_fermentationD;
    public GameObject branchesD;
    public GameObject AlveoliD;
    public GameObject BloodD;











    [Header("Explanation anims")]
    private Animator anim;

    public GameObject alcohal_anim;
    public GameObject leaf_anim;
    public GameObject ambeoa;


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
    void _audio35_audioMethod()

    {
        myAudio.clip = audio35;
        myAudio.Play();
    }
    void _audio36_audioMethod()

    {
        myAudio.clip = audio36;
        myAudio.Play();
    }
    void _audio37_audioMethod()

    {
        myAudio.clip = audio37;
        myAudio.Play();
    }
    void _audio38_audioMethod()

    {
        myAudio.clip = audio38;
        myAudio.Play();
    }
    void _audio39_audioMethod()

    {
        myAudio.clip = audio39;
        myAudio.Play();
    }
    void _audio40_audioMethod()

    {
        myAudio.clip = audio40;
        myAudio.Play();
    }




    void _audio41_audioMethod()

    {
        myAudio.clip = audio41;
        myAudio.Play();
    }
    void _audio42_audioMethod()

    {
        myAudio.clip = audio42;
        myAudio.Play();
    }
    void _audio43_audioMethod()

    {
        myAudio.clip = audio43;
        myAudio.Play();
    }
    void _audio44_audioMethod()

    {
        myAudio.clip = audio44;
        myAudio.Play();
    }
    void _audio45_audioMethod()

    {
        myAudio.clip = audio45;
        myAudio.Play();
    }
    void _audio46_audioMethod()

    {
        myAudio.clip = audio46;
        myAudio.Play();
    }
    void _audio47_audioMethod()

    {
        myAudio.clip = audio47;
        myAudio.Play();
    }
    void _audio48_audioMethod()

    {
        myAudio.clip = audio48;
        myAudio.Play();
    }
    void _audio49_audioMethod()

    {
        myAudio.clip = audio49;
        myAudio.Play();
    }
    void _audio50_audioMethod()

    {
        myAudio.clip = audio50;
        myAudio.Play();
    }



















    // Objects

    void _liverMethodON()
    {
        liver.SetActive(true);
    }

    void _liverMethodOFF()
    {
        liver.SetActive(false);
    }




    void _mouthMethodON()
    {
        mouth.SetActive(true);
    }

    void _mouthMethodOFF()
    {
        mouth.SetActive(false);
    }




    void _pancreasMethodON()
    {
        pancreas.SetActive(true);
    }

    void _pancreasMethodOFF()
    {
        pancreas.SetActive(false);
    }



    void _large_intestineMethodON()
    {
        large_intestine.SetActive(true);
    }

    void _large_intestineMethodOFF()
    {
        large_intestine.SetActive(false);
    }



    void _anusMethodON()
    {
        anus.SetActive(true);
    }

    void _anusMethodOFF()
    {
        anus.SetActive(false);
    }






    void _nostrilsMethodON()
    {
        nostrils.SetActive(true);
    }

    void _nostrilsMethodOFF()
    {
        nostrils.SetActive(false);
    }
























    // Titles


    void _Life_ProcessesTMethodON()
    {
        Life_ProcessesT.SetActive(true);
    }

    void _Life_ProcessesTMethodOFF()
    {
        Life_ProcessesT.SetActive(false);
    }





    void _NutritionTMethodON()
    {
        NutritionT.SetActive(true);
    }

    void _NutritionTMethodOFF()
    {
        NutritionT.SetActive(false);
    }


    void _autotrophic_nutritionTMethodON()
    {
        autotrophic_nutritionT.SetActive(true);
    }

    void _autotrophic_nutritionTMethodOFF()
    {
        autotrophic_nutritionT.SetActive(false);
    }


    void _Hete_nutritionsTMethodON()
    {
        Hete_nutritionsT.SetActive(true);
    }

    void _Hete_nutritionsTMethodOFF()
    {
        Hete_nutritionsT.SetActive(false);
    }


    void _Saprophytic_nutritionTMethodON()
    {
        Saprophytic_nutritionT.SetActive(true);
    }

    void _Saprophytic_nutritionTMethodOFF()
    {
        Saprophytic_nutritionT.SetActive(false);
    }


    void _Parasitic_nutritionTMethodON()
    {
        Parasitic_nutritionT.SetActive(true);
    }

    void _Parasitic_nutritionTMethodOFF()
    {
        Parasitic_nutritionT.SetActive(false);
    }


    void _Holozoic_nutritionTMethodON()
    {
        Holozoic_nutritionT.SetActive(true);
    }

    void _Holozoic_nutritionTMethodOFF()
    {
        Holozoic_nutritionT.SetActive(false);
    }


    void _nutrition_in_human_beingsTMethodON()
    {
        nutrition_in_human_beingsT.SetActive(true);
    }

    void _nutrition_in_human_beingsTMethodOFF()
    {
        nutrition_in_human_beingsT.SetActive(false);
    }


    void _emulsification_of_fatsTMethodON()
    {
        emulsification_of_fatsT.SetActive(true);
    }

    void _emulsification_of_fatsTMethodOFF()
    {
        emulsification_of_fatsT.SetActive(false);
    }







    void _absorption_of_nutrientsTMethodON()
    {
        absorption_of_nutrientsT.SetActive(true);
    }

    void _absorption_of_nutrientsTMethodOFF()
    {
        absorption_of_nutrientsT.SetActive(false);
    }







    void _RespirationTMethodON()
    {
        RespirationT.SetActive(true);
    }

    void _RespirationTMethodOFF()
    {
        RespirationT.SetActive(false);
    }







    void _Anaerobic_respirationTMethodON()
    {
        Anaerobic_respirationT.SetActive(true);
    }

    void _Anaerobic_respirationTMethodOFF()
    {
        Anaerobic_respirationT.SetActive(false);
    }








    void _alcoholic_fermentationTMethodON()
    {
        alcoholic_fermentationT.SetActive(true);
    }

    void _alcoholic_fermentationTMethodOFF()
    {
        alcoholic_fermentationT.SetActive(false);
    }








    void _human_respiratory_systemTMethodON()
    {
        human_respiratory_systemT.SetActive(true);
    }

    void _human_respiratory_systemTMethodOFF()
    {
        human_respiratory_systemT.SetActive(false);
    }








    void _exchange_of_gasesTMethodON()
    {
        exchange_of_gasesT.SetActive(true);
    }

    void _exchange_of_gasesTMethodOFF()
    {
        exchange_of_gasesT.SetActive(false);
    }

    void _stomachTMethodON()
    {
        stomachT.SetActive(true);
    }

    void _stomachTMethodOFF()
    {
        stomachT.SetActive(false);
    }













































    void _nutritionsDMethodON()
    {
        nutritionsD.SetActive(true);
    }

    void _nutritionsDMethodOFF()
    {
        nutritionsD.SetActive(false);
    }





    void _photosynthasisDMethodON()
    {
        photosynthasisD.SetActive(true);
    }

    void _photosynthasisDMethodOFF()
    {
        photosynthasisD.SetActive(false);
    }




    void _Exchange_of_gasesDMethodON()
    {
        Exchange_of_gasesD.SetActive(true);
    }

    void _Exchange_of_gasesDMethodOFF()
    {
        Exchange_of_gasesD.SetActive(false);
    }



    void _carbon_dioxideDMethodON()
    {
        carbon_dioxideD.SetActive(true);
    }

    void _carbon_dioxideDMethodOFF()
    {
        carbon_dioxideD.SetActive(false);
    }





    void _chloroplastDMethodON()
    {
        chloroplastD.SetActive(true);
    }

    void _chloroplastDMethodOFF()
    {
        chloroplastD.SetActive(false);
    }






    void _ChlorophyllDMethodON()
    {
        ChlorophyllD.SetActive(true);
    }

    void _ChlorophyllDMethodOFF()
    {
        ChlorophyllD.SetActive(false);
    }





    void _small_intestineDMethodON()
    {
        small_intestineD.SetActive(true);
    }

    void _small_intestineDMethodOFF()
    {
        small_intestineD.SetActive(false);
    }






    void _pancreatic_juiceDMethodON()
    {
        pancreatic_juiceD.SetActive(true);
    }

    void _pancreatic_juiceDMethodOFF()
    {
        pancreatic_juiceD.SetActive(false);
    }





    void _bile_juiceDMethodON()
    {
        bile_juiceD.SetActive(true);
    }

    void _bile_juiceDMethodOFF()
    {
        bile_juiceD.SetActive(false);
    }





    void _trypsinDMethodON()
    {
        trypsinD.SetActive(true);
    }

    void _trypsinDMethodOFF()
    {
        trypsinD.SetActive(false);
    }



    void _ATP_DMethodON()
    {
        ATP_D.SetActive(true);
    }

    void _ATP_DMethodOFF()
    {
        ATP_D.SetActive(false);
    }






    void _alcoholic_fermentationDMethodON()
    {
        alcoholic_fermentationD.SetActive(true);
    }

    void _alcoholic_fermentationDMethodOFF()
    {
        alcoholic_fermentationD.SetActive(false);
    }




    void _branchesDMethodON()
    {
        branchesD.SetActive(true);
    }

    void _branchesDMethodOFF()
    {
        branchesD.SetActive(false);
    }





    void _AlveoliDMethodON()
    {
        AlveoliD.SetActive(true);
    }

    void _AlveoliDMethodOFF()
    {
        AlveoliD.SetActive(false);
    }




    void _BloodDMethodON()
    {
        BloodD.SetActive(true);
    }

    void _BloodDMethodOFF()
    {
        BloodD.SetActive(false);
    }






















    //Animation Play





    void _alcohal_animMethod()
    {
        anim = alcohal_anim.GetComponent<Animator>();
        anim.Play("alcohal anim");
    }







    void _leaf_animMethod()
    {
        anim = leaf_anim.GetComponent<Animator>();
        anim.Play("leaf anim");
    }





    void _ambeoaMethod()
    {
        anim = ambeoa.GetComponent<Animator>();
        anim.Play("ambeoa");
    }


    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;

    
    public GameObject findplayer;
    public MenuSystem menu;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camara anim", 0, targetNormalizedTime);
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
            case 1: TransportPlayerToLevel2(); break;
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
    private int currentIndex = 0;
    private bool canUsePlant = false;
    public void CollectLevel1()
    {
        currentIndex++;
        if (currentIndex == 3)
        {
            currentIndex = 0;
            canUsePlant = true;
            plant.tag = "Interactable";
        }
    }

    public GameObject plant;

    public void UsePlant()
    {
        if (canUsePlant)
        {
            TransportPlayerToLevel2();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public Transform level2SpawnPoint;
    private void TransportPlayerToLevel2()
    {
        StartCoroutine(delaySpawnPoint());
        SaveProgress(1, 0, 1);
    }

    IEnumerator delaySpawnPoint()
    {
        yield return new WaitForSeconds(0.5f);
        InventoryManager.Instance.player.ChangePosition(level2SpawnPoint);
    }

    private int plantCount = 0;
    private int meatCount = 0;
    public bool collectedPlant = false;
    public bool collectedMeat = false;

    public ObjectiveController objectiveController;
    public void CollectPlant()
    {
        Debug.Log("COLLECTED PLANT");
        if (objectiveController.currentObjective == 1 && objectiveController.currentStep == 0)
        {
            if (plantCount == 0)
            {
                plantCount++;
                InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
            }
        }
        else if (objectiveController.currentObjective == 1 && objectiveController.currentStep == 2)
        {
            Debug.Log("COLLECTED PLANT");

            if (plantCount == 1)
            {
                Debug.Log("COLLECTED PLANT");
                collectedPlant = true;

                if (collectedPlant && collectedMeat)
                {
                    InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
                    InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
                }
            }
        }
        else
        {
            MissionFailed();
        }


    }

    public void StepComplete()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void CollectMeat()
    {
        Debug.Log("COLLECTED MEAT");
        if (objectiveController.currentObjective == 1 && objectiveController.currentStep == 1)
        {
            if (meatCount == 0)
            {
                meatCount++;
                InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
            }
        }
        else if (objectiveController.currentObjective == 1 && objectiveController.currentStep == 2)
        {
            Debug.Log("COLLECTED MEAT");

            if (meatCount == 1)
            {
                Debug.Log("COLLECTED MEAT");

                collectedMeat = true;

                if (collectedPlant && collectedMeat)
                {
                    InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
                    InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
                }
            }
        }
        else
        {
            MissionFailed();
        }
    }

    public AudioSource clickSound;
    private bool level3MiniGameStarted = false;
    private bool level4MiniGameStarted = false;
    public Camera cam;
    public LayerMask layerMask;
    public List<string> humanOrgans = new List<string>();
    public List<string> respOrgans = new List<string>();
    public Animator foodAnim;
    public Transform level3CamHolder2;
    public Transform level4CamHolder2;
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

        if (level3MiniGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickSound.Play();
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
                {
                    if (raycastHit.collider != null)
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        if (raycastHit.collider.gameObject.name == humanOrgans[currentIndex])
                        {
                            currentIndex++;
                            foodAnim.SetTrigger(currentIndex.ToString());
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();

                            if (currentIndex == 2)
                            {
                                transform.position = level3CamHolder2.position;
                            }

                            if (currentIndex == humanOrgans.Count)
                            {
                                level3MiniGameStarted = false;
                                currentIndex = 0;
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

        if (level4MiniGameStarted)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                clickSound.Play();
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
                {
                    if (raycastHit.collider != null)
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        Debug.Log(respOrgans[currentIndex]);
                        if (raycastHit.collider.gameObject.name == respOrgans[currentIndex])
                        {
                            currentIndex++;
                            raycastHit.collider.enabled = false;
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();

                            if (currentIndex == 2)
                            {
                                transform.position = level4CamHolder2.position;
                            }

                            if (currentIndex == respOrgans.Count)
                            {
                                level4MiniGameStarted = false;
                                currentIndex = 0;
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

    public int index;

    public void Correctanswer3()
    {
        index++;
        if (index == 3)
        {
            index = 0;
            StepComplete();
        }
    }

    public void ShowQuestionsL3()
    {
        StartCoroutine(ShowQuestionsLevel3());
    }

    IEnumerator ShowQuestionsLevel3()
    {
        yield return new WaitForSeconds(1);
        level3Questions[questionIndex].SetActive(true);
    }

    public List<GameObject> level3Questions;
    public TargetController level3MiniGame;
    public TargetController level4MiniGame;
    private int questionIndex = 0;
    public void CorrectAnswer()
    {
        questionIndex++;

        foreach (GameObject go in level3Questions)
        {
            go.SetActive(false);
        }
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void Level4()
    {
        StartCoroutine(DelayLv4MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        

    }
    IEnumerator DelayLv4MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        level4MiniGame.Output();
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

    public void HumanMiniGameStart()
    {
        level3MiniGameStarted = true;
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RespMiniGameStart()
    {
        level4MiniGameStarted = true;
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

    public Image bloodBar;
    public void LeachAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
        bloodBar.fillAmount += 0.1f;

        if(bloodBar.fillAmount == 1)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }


















































}
