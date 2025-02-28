using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_respiration_in_organisam_class_7 : MonoBehaviour
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
            animator.Play("camera animations", 0, targetNormalizedTime);
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
            case 1: Level6(); break;
            default: level(); break;
        }
    }

    public void SaveProgress(int checkpoint, int currentStep, int currentObjective)
    {
        CheckpointManager.Instance.SaveCheckpoint(checkpoint, currentStep, currentObjective);
    }

    //Skip() {level()}




    
    public void level()
    {
        miniGames.Output();

    }

    public GameObject lv1;


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
        //Level2();
        //StartCoroutine(DelayLv5MiniGameStart());
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
        if (level5MiniGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //clickSound.Play();
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

                            if (currentIndex == 1)
                            {
                                transform.position = level5CamHolder2.position;
                            }

                            if (currentIndex == respOrgans.Count)
                            {
                                level5MiniGameStarted = false;
                                currentIndex = 0;
                                Invoke("Level6", 1);
                            }
                        }
                        else
                        {
                            InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
                        }
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
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
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


    public void Level2()
    {
        StartCoroutine(DelayLv2AMiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    public TargetController lv2AMiniGame;

    IEnumerator DelayLv2AMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv2AMiniGame.Output();
    }


    
    public void Lv2ATurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 2)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(DelayLv2BMiniGameStart());
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public TargetController lv2BMiniGame;

    IEnumerator DelayLv2BMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv2BMiniGame.Output();
    }



    public void Lv2BTurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(DelayLv2CMiniGameStart());
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public TargetController lv2CMiniGame;
    public GameObject OptQ2;

    IEnumerator DelayLv2CMiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv2CMiniGame.Output();
    }



    public void Lv2CTurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 2)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            OptQ2.SetActive(true);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public void Level4()
    {
        StartCoroutine(DelayLv4MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    public TargetController lv4MiniGame;

    IEnumerator DelayLv4MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv4MiniGame.Output();
    }


    public void Lv4TurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 7)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(DelayLv5MiniGameStart());
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public bool level5MiniGameStarted = false;
    public List<string> respOrgans = new List<string>();
    public int currentIndex = 0;

    public Transform level5CamHolder2;
    public void Level5()
    {
         
    }
    public void RespMiniGameStart()
    {
        level5MiniGameStarted = true;
        //InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        //Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator DelayLv5MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        Invoke("RespMiniGameStart", 0.1f);
        lv5MiniGame.Output();
    }

    public TargetController lv5MiniGame;

    //public void Lv5CorrectAnswer()
    //{

    //    index++;


    //    if (index == 3)
    //    {
    //        index = 0;
    //        //InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();

    //        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    //    }

    //}

    public void Level6()
    {
        StartCoroutine(DelayLv6MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    public TargetController lv6MiniGame;

    IEnumerator DelayLv6MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv6MiniGame.Output();
    }

    public Transform lv7SpawnPoint;
   
    public void Level7()
    {
        InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(lv7SpawnPoint);
        
        
    }

    public void CorrectAnswerLv7()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        InventoryManager.Instance.inventryStatic.SetActive(true);
        InventoryManager.Instance.player.ThirdPersonController.canMove = true;
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        Invoke("Level8", 1);
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

    public Transform lv8SpawnPoint;

    public void Level8()
    {
        InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(lv8SpawnPoint);
    }

    public void CorrectAnswerLv8()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        InventoryManager.Instance.inventryStatic.SetActive(true);
        InventoryManager.Instance.player.ThirdPersonController.canMove = true;
        Level9();
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
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

    public void Level9()
    {
        StartCoroutine(DelayLv9MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    public TargetController lv9MiniGame;

    IEnumerator DelayLv9MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv9MiniGame.Output();
    }
    

    


    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip rio;
    public AudioClip WhydoweRespire;
    public AudioClip WhydoweRespire2;
    public AudioClip WhydoweRespire3;
    public AudioClip WhydoweRespire4;
    public AudioClip Breathing1;
    public AudioClip Breathing2;
    public AudioClip Breathing3;
    public AudioClip Breathing4;
    public AudioClip Sneezing;
    public AudioClip Understandingthemechanismofbreathing1;
    public AudioClip Understandingthemechanismofbreathing2;
    public AudioClip Howwebreatheout;







    public AudioClip Breathing_of_insects;
    public AudioClip Respiration_system_in_insects;
    public AudioClip Breathing_in_earthworm;
    public AudioClip Breathing_in_frog;
    public AudioClip Breathing_in_frogp2;
    public AudioClip Breathing_in_frogp3;
    public AudioClip Breathing_in_fish;
    public AudioClip Breathing_in_fishp2;
    public AudioClip How_do_plants_respire;





















    [Header("Explanation anims")]
    private Animator anim;
    public GameObject Exercisekid;
    public GameObject breathing;
    public GameObject jogging;
    public GameObject human_respiration;
    public GameObject exp_tube_anim;
    public GameObject lung_exp;
   
    
    
    
    public GameObject earth_wome_anim;
    public GameObject dolphin_anim;

















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



















    // ON - OFF gameobjects
    [Header("Explanation Assets")]






    public GameObject jagging_kid;
    public GameObject standing_kid;
 
    public GameObject inhalation;
    public GameObject exhalation;
    public GameObject nasalcavity;
    public GameObject chestcavity;
    public GameObject inhalevalue;
    public GameObject exhalevalue;














    // Titles



    public GameObject Respiration_In_organismsT;
    public GameObject Why_do_we_RespireT;
    public GameObject BreathingT;
    public GameObject SneezingT;
    public GameObject Understanding_the_mechanism_of_breathingT;
    public GameObject How_we_breathe_outT;
    public GameObject inheal_exhaleD;
   
    
    
    
    
    public GameObject Breathing_of_insectsT;
    public GameObject Respiration_system_in_insectsT;
    public GameObject Breathing_in_earthwormT;
    public GameObject Breathing_in_frogT;
    public GameObject Buccal_respirationT;
    public GameObject Cutaneous_respirationT;
    public GameObject Breathing_in_fishT;
    public GameObject How_do_plants_respireT;


    public GameObject tracheaD;
    public GameObject fish_grills_uwD;
    public GameObject Breath_through_whealD;
    public GameObject whale_take_breath_15;
    public GameObject spiracle;































    // Audio


    void _rio_audioMethod()

    {
        myAudio.clip = rio;
        myAudio.Play();
    }




    


    void _2WhydoweRespire_audioMethod()

    {
        myAudio.clip = WhydoweRespire;
        myAudio.Play();
    }



   


    void _3WhydoweRespire2_audioMethod()

    {
        myAudio.clip = WhydoweRespire2;
        myAudio.Play();
    }



   


    void _WhydoweRespire3_audioMethod()

    {
        myAudio.clip = WhydoweRespire3;
        myAudio.Play();
    }



    void _WhydoweRespire4_audioMethod()

    {
        myAudio.clip = WhydoweRespire4;
        myAudio.Play();
    }




    void _Breathing1_audioMethod()

    {
        myAudio.clip = Breathing1;
        myAudio.Play();
    }






    void _Breathing2_audioMethod()

    {
        myAudio.clip = Breathing2;
        myAudio.Play();
    }






    void _Breathing3_audioMethod()

    {
        myAudio.clip = Breathing3;
        myAudio.Play();
    }





    void _Breathing4_audioMethod()

    {
        myAudio.clip = Breathing4;
        myAudio.Play();
    }




    void _Sneezing_audioMethod()

    {
        myAudio.clip = Sneezing;
        myAudio.Play();
    }







    void _Understandingthemechanismofbreathing1_audioMethod()

    {
        myAudio.clip = Understandingthemechanismofbreathing1;
        myAudio.Play();
    }








    void _Understandingthemechanismofbreathing2_audioMethod()

    {
        myAudio.clip = Understandingthemechanismofbreathing2;
        myAudio.Play();
    }






    void _Howwebreatheout_audioMethod()

    {
        myAudio.clip = Howwebreatheout;
        myAudio.Play();
    }




    void _Breathing_of_insects_audioMethod()

    {
        myAudio.clip = Breathing_of_insects;
        myAudio.Play();
    }






    void _Respiration_system_in_insects_audioMethod()

    {
        myAudio.clip = Respiration_system_in_insects;
        myAudio.Play();
    }




    void _Breathing_in_earthworm_audioMethod()

    {
        myAudio.clip = Breathing_in_earthworm;
        myAudio.Play();
    }





    void _Breathing_in_frog_audioMethod()

    {
        myAudio.clip = Breathing_in_frog;
        myAudio.Play();
    }






    void _Breathing_in_frogp2_audioMethod()

    {
        myAudio.clip = Breathing_in_frogp2;
        myAudio.Play();
    }






    void _Breathing_in_frogp3_audioMethod()

    {
        myAudio.clip = Breathing_in_frogp3;
        myAudio.Play();
    }





    void _Breathing_in_fish_audioMethod()

    {
        myAudio.clip = Breathing_in_fish;
        myAudio.Play();
    }




    void _Breathing_in_fishp2_audioMethod()

    {
        myAudio.clip = Breathing_in_fishp2;
        myAudio.Play();
    }





    void _How_do_plants_respire_audioMethod()

    {
        myAudio.clip = How_do_plants_respire;
        myAudio.Play();
    }









































    //Animation Play





    void _Exercisekid_plankMethod()
    {
        anim = Exercisekid.GetComponent<Animator>();
        anim.Play("start plank");
    }

    void _Exercisekid_standingMethod()
    {
        anim = Exercisekid.GetComponent<Animator>();
        anim.Play("standing");
    }






    void _breathingMethod()
    {
        anim = breathing.GetComponent<Animator>();
        anim.Play("breath anim");
    }



    void _joggingMethod()
    {
        anim = jogging.GetComponent<Animator>();
        anim.Play("jogging");
    }





    void _human_respirationMethod()
    {
        anim = human_respiration.GetComponent<Animator>();
        anim.Play("human respiration");
    }





    void _exp_tube_animMethod()
    {
        anim = exp_tube_anim.GetComponent<Animator>();
        anim.Play("exp cube lung anim");
    }




    void _lung_expMethod()
    {
        anim = lung_exp.GetComponent<Animator>();
        anim.Play("lung exp");
    }














    void _earth_wome_animMethod()
    {
        anim = earth_wome_anim.GetComponent<Animator>();
        anim.Play("earth worm anim");
    }







    void _dolphin_animMethod()
    {
        anim = dolphin_anim.GetComponent<Animator>();
        anim.Play("Dolphin anmation");
    }



























    // Objects

    void _jagging_kidMethodON()
    {
        jagging_kid.SetActive(true);
    }

    void _jagging_kidMethodOFF()
    {
        jagging_kid.SetActive(false);
    }




    // Objects

    void _standing_kidMethodON()
    {
        standing_kid.SetActive(true);
    }

    void _standing_kidMethodOFF()
    {
        standing_kid.SetActive(false);
    }





    void _inhalationMethodON()
    {
        inhalation.SetActive(true);
    }

    void _inhalationMethodOFF()
    {
        inhalation.SetActive(false);
    }






    void _exhalationMethodON()
    {
        exhalation.SetActive(true);
    }

    void _exhalationMethodOFF()
    {
        exhalation.SetActive(false);
    }








    void _nasalcavityMethodON()
    {
        nasalcavity.SetActive(true);
    }

    void _nasalcavityMethodOFF()
    {
        nasalcavity.SetActive(false);
    }







    void _chestcavityMethodON()
    {
        chestcavity.SetActive(true);
    }

    void _chestcavityMethodOFF()
    {
        chestcavity.SetActive(false);
    }







    void _inhalevalueMethodON()
    {
        inhalevalue.SetActive(true);
    }

    void _inhalevalueMethodOFF()
    {
        inhalevalue.SetActive(false);
    }








    void _exhalevalueMethodON()
    {
        exhalevalue.SetActive(true);
    }

    void _exhalevalueMethodOFF()
    {
        exhalevalue.SetActive(false);
    }




























    // Titles


    void _Respiration_In_organismsTMethodON()
    {
        Respiration_In_organismsT.SetActive(true);
    }

    void _Respiration_In_organismsTMethodOFF()
    {
        Respiration_In_organismsT.SetActive(false);
    }





    


    void _Why_do_we_RespireTMethodON()
    {
        Why_do_we_RespireT.SetActive(true);
    }

    void _Why_do_we_RespireTMethodOFF()
    {
        Why_do_we_RespireT.SetActive(false);
    }






    void _BreathingTMethodON()
    {
        BreathingT.SetActive(true);
    }

    void _BreathingTMethodOFF()
    {
        BreathingT.SetActive(false);
    }






    void _SneezingTMethodON()
    {
        SneezingT.SetActive(true);
    }

    void _SneezingTMethodOFF()
    {
        SneezingT.SetActive(false);
    }




    void _Understanding_the_mechanism_of_breathingTMethodON()
    {
        Understanding_the_mechanism_of_breathingT.SetActive(true);
    }

    void _Understanding_the_mechanism_of_breathingTMethodOFF()
    {
        Understanding_the_mechanism_of_breathingT.SetActive(false);
    }




    void _How_we_breathe_outTMethodON()
    {
        How_we_breathe_outT.SetActive(true);
    }

    void _How_we_breathe_outTMethodOFF()
    {
        How_we_breathe_outT.SetActive(false);
    }





    void _inheal_exhaleDMethodON()
    {
        inheal_exhaleD.SetActive(true);
    }

    void _inheal_exhaleDMethodOFF()
    {
        inheal_exhaleD.SetActive(false);
    }







    void _Breathing_of_insectsTMethodON()
    {
        Breathing_of_insectsT.SetActive(true);
    }

    void _Breathing_of_insectsTMethodOFF()
    {
        Breathing_of_insectsT.SetActive(false);
    }







    void _Respiration_system_in_insectsTMethodON()
    {
        Respiration_system_in_insectsT.SetActive(true);
    }

    void _Respiration_system_in_insectsTMethodOFF()
    {
        Respiration_system_in_insectsT.SetActive(false);
    }






    void _Breathing_in_earthwormTMethodON()
    {
        Breathing_in_earthwormT.SetActive(true);
    }

    void _Breathing_in_earthwormTMethodOFF()
    {
        Breathing_in_earthwormT.SetActive(false);
    }









    void _Breathing_in_frogTMethodON()
    {
        Breathing_in_frogT.SetActive(true);
    }

    void _Breathing_in_frogTMethodOFF()
    {
        Breathing_in_frogT.SetActive(false);
    }





    void _Buccal_respirationTMethodON()
    {
        Buccal_respirationT.SetActive(true);
    }

    void _Buccal_respirationTMethodOFF()
    {
        Buccal_respirationT.SetActive(false);
    }






    void _Cutaneous_respirationTMethodON()
    {
        Cutaneous_respirationT.SetActive(true);
    }

    void _Cutaneous_respirationTMethodOFF()
    {
        Cutaneous_respirationT.SetActive(false);
    }








    void _Breathing_in_fishTMethodON()
    {
        Breathing_in_fishT.SetActive(true);
    }

    void _Breathing_in_fishTMethodOFF()
    {
        Breathing_in_fishT.SetActive(false);
    }







    void _How_do_plants_respireTMethodON()
    {
        How_do_plants_respireT.SetActive(true);
    }

    void _How_do_plants_respireTMethodOFF()
    {
        How_do_plants_respireT.SetActive(false);
    }








    void _tracheaDMethodON()
    {
        tracheaD.SetActive(true);
    }

    void _tracheaDMethodOFF()
    {
        tracheaD.SetActive(false);
    }




    void _fish_grills_uwDMethodON()
    {
        fish_grills_uwD.SetActive(true);
    }

    void _fish_grills_uwDMethodOFF()
    {
        fish_grills_uwD.SetActive(false);
    }






    void _Breath_through_whealDMethodON()
    {
        Breath_through_whealD.SetActive(true);
    }

    void _Breath_through_whealDMethodOFF()
    {
        Breath_through_whealD.SetActive(false);
    }






    void _whale_take_breath_15MethodON()
    {
        whale_take_breath_15.SetActive(true);
    }

    void _whale_take_breath_15MethodOFF()
    {
        whale_take_breath_15.SetActive(false);
    }




    void _spiracleMethodON()
    {
        spiracle.SetActive(true);
    }

    void _spiracleMethodOFF()
    {
        spiracle.SetActive(false);
    }




















}
