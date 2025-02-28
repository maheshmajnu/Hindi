using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_Our_environment : MonoBehaviour
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
            animator.Play("Camera animation", 0, targetNormalizedTime);
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
            case 1: Level3(); break;
            case 2: Level4(); break;
            default: level(); break;
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
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();

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

    public void PlayParticleEffect(ParticleSystem particleEffect)
    {
        
          particleEffect.Play(); 
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


    public Animator door;
    public void Level1()
    {
        index++;


        if (index == 4)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            PlayAnimTrigg(door);
            //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public ParticleSystem Fire;


    public void Level2()
    {
        
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            PlayParticleEffect(Fire);

    }

    public Transform SpawnPoint3;
    public GameObject lion;
    public void Level3()
    {
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        StartCoroutine(DelayLevel3Start());
        
    }

    IEnumerator DelayLevel3Start()
    {
        yield return new WaitForSeconds(1f);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint3);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        lion.SetActive(true);
    }
    public void Savepoint1()
    {
        SaveProgress(1, 0, 2);
    }

    public Transform SpawnPoint4;
    public void Level4()
    {
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        StartCoroutine(DelayLevel4Start());

    }

    IEnumerator DelayLevel4Start()
    {
        yield return new WaitForSeconds(1f);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint4);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }
    public void Savepoint2()
    {
        SaveProgress(2, 0, 3);
    }

    public Transform SpawnPoint5;
    public void Level5()
    {
        InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(SpawnPoint5);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    public void Level5Game()
    {
        index++;


        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            
            //InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }
    private int currentIndex;
    public void Working()
    {
        currentIndex++;


        if (currentIndex == 1000000)
        {
            Debug.Log("it is working");
        }
        
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject Bacteria;
    public GameObject vulture;
    public GameObject PCDpic;
    public GameObject prism1;
    public GameObject prism2;
    public GameObject eatingfish;
    public GameObject No;
    public GameObject fullforms;
    public GameObject twotypes;
    public GameObject leaffall;
    public GameObject sunrays;
    public GameObject bond;
    public GameObject reuse;
    public GameObject horse;
    public GameObject horseskeleton;
    public GameObject greenleaf;
    public GameObject brownleaf;
    public GameObject bacterialeaf;





    public GameObject T1Intro;
    public GameObject T2Ecosystem;
    public GameObject T3Foodchainweb;
    public GameObject T4biomag;
    public GameObject T5ozone;
    public GameObject T6managing;
    public GameObject T7biodegrade;
    public GameObject T8decomp;
    public GameObject T9nonbiodegrade;
    public GameObject D1Ecomeans;
    public GameObject D2NatEcoExp;
    public GameObject D3ArtiEcoExp;
    public GameObject D4PCD;
    public GameObject D5Herbivores;
    public GameObject D6Carnivores;
    public GameObject D7Omnivores;
    public GameObject D8Ultimate;
    public GameObject D9Primary;
    public GameObject D10Secondary;
    public GameObject D11Tertiary;
    public GameObject D12OnepercentD;
    public GameObject D13pesticideamount;
    public GameObject D14chemicaldeposits;
    public GameObject D15manD;
    public GameObject D16biomagni;
    public GameObject D17wecannot;
    public GameObject D18healthhazard;
    public GameObject D19O3;
    public GameObject D20ozonepoisionous;
    public GameObject D21ozoneformation;
    public GameObject D22CFC;
    public GameObject D231987;
    public GameObject D24100D;
    public GameObject D25biodegrade;
    public GameObject D26disposable;
    public GameObject D27decompdefD;



    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject vulture_anim;
    public GameObject fish_anim;
    public GameObject leaf_anim;
    public GameObject sunrays_anim;




    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip intro;
    public AudioClip in_nature;
    public AudioClip balance_nature;
    public AudioClip biotic;
    public AudioClip ecosystem_formation;
    public AudioClip biotic_abiotic;
    public AudioClip natural_eco_exp;
    public AudioClip artificial_eco_exp;
    public AudioClip ecosystem;
    public AudioClip diff_org_in_ecosystem;
    public AudioClip water_organisms;
    public AudioClip water_plants;
    public AudioClip bacteria;
    public AudioClip biotic_pond_eco;
    public AudioClip abiotic_pond_eco;
    public AudioClip three_types;
    public AudioClip producers;
    public AudioClip herbivores;
    public AudioClip carnivores;
    public AudioClip omnivores;
    public AudioClip consumers;
    public AudioClip decomposers;
    public AudioClip decomposition;
    public AudioClip foodchain_web;
    public AudioClip living_things_energy;
    public AudioClip deer_to_lion;
    public AudioClip where_deer_get;
    public AudioClip deer_from_plant;
    public AudioClip plant_from_sun;
    public AudioClip sun_ultimate_source;
    public AudioClip primary_consumers;
    public AudioClip secondary_consumers;
    public AudioClip teritiary_consumers;
    public AudioClip energy_flow;
    public AudioClip plant_deer_sun;
    public AudioClip plant_rabbit_sun;
    public AudioClip food_chain_def;
    public AudioClip trophic_levels;
    public AudioClip one_organism_to_another;
    public AudioClip energy_flow_in_foodchain;
    public AudioClip one_percent;
    public AudioClip from_plant_to_animal_body;
    public AudioClip body_heat;
    public AudioClip digestion;
    public AudioClip ten_percent;
    public AudioClip level1_to_2;
    public AudioClip level2_to_3;
    public AudioClip we_can_observe;
    public AudioClip unidirectional;
    public AudioClip decline_in_organism_count;
    public AudioClip many_foodchains;
    public AudioClip foodweb;




    // Part 2 audio

    public AudioClip a1;
    public AudioClip a2;
    public AudioClip a3;
    public AudioClip a4;
    public AudioClip a5;
    public AudioClip a6;
    public AudioClip a7;
    public AudioClip a8;
    public AudioClip a9;
    public AudioClip a10;
    public AudioClip a11;
    public AudioClip a12;
    public AudioClip a13;
    public AudioClip a14;
    public AudioClip a15;
    public AudioClip a16;
    public AudioClip a17;
    public AudioClip a18;
    public AudioClip a19;
    public AudioClip a20;
    public AudioClip a21;
    public AudioClip a22;
    public AudioClip a23;
    public AudioClip a24;
    public AudioClip a25;
    public AudioClip a26;
    public AudioClip a27;
    public AudioClip a28;
    public AudioClip a29;
    public AudioClip a30;
    public AudioClip a31;
    public AudioClip a32;
    public AudioClip a33;
    public AudioClip a34;
    public AudioClip a35;
    public AudioClip a36;
    public AudioClip a37;
    public AudioClip a38;
    public AudioClip a39;
    public AudioClip a40;
    public AudioClip a41;
    public AudioClip a42;
    public AudioClip a43;
    public AudioClip a44;
    public AudioClip a45;
    public AudioClip a46;
    public AudioClip a47;
    public AudioClip a48;
    public AudioClip a49;
    public AudioClip a50;
    public AudioClip a51;
    public AudioClip a52;
    public AudioClip a53;
    public AudioClip a54;
    public AudioClip a55;
    public AudioClip a56;
    public AudioClip a57;
    public AudioClip a58;
    public AudioClip a59;
    public AudioClip a60;
    public AudioClip a61;
    public AudioClip a62;
    public AudioClip a63;
    public AudioClip a64;
    public AudioClip a65;

    public GameObject gameplayObject;
    public GameObject explanationObject;

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
    void _greenleaf_MethodON()
    {
        greenleaf.SetActive(true);
    }
    void _greenleaf_MethodoOFF()
    {
        greenleaf.SetActive(false);
    }
    //
    void _brownleaf_MethodON()
    {
        brownleaf.SetActive(true);
    }
    void _brownleaf_MethodoOFF()
    {
        brownleaf.SetActive(false);
    }
    //
    void _bacterialeaf_MethodON()
    {
        bacterialeaf.SetActive(true);
    }
    void _bacterialeaf_MethodoOFF()
    {
        bacterialeaf.SetActive(false);
    }
    





















    





























































//Titles



    //
    void _T1Intro_MethodON()
    {
        T1Intro.SetActive(true);
    }
    void _T1Intro_MethodoOFF()
    {
        T1Intro.SetActive(false);
    }
    //
    void _T2Ecosystem_MethodON()
    {
        T2Ecosystem.SetActive(true);
    }
    void _T2Ecosystem_MethodoOFF()
    {
        T2Ecosystem.SetActive(false);
    }
    //
    void _T3Foodchainweb_MethodON()
    {
        T3Foodchainweb.SetActive(true);
    }
    void _T3Foodchainweb_MethodoOFF()
    {
        T3Foodchainweb.SetActive(false);
    }
    //
    void _T4Biomag_MethodON()
    {
        T4biomag.SetActive(true);
    }
    void _T4Biomag_MethodoOFF()
    {
        T4biomag.SetActive(false);
    }
    //
    void _T5ozone_MethodON()
    {
        T5ozone.SetActive(true);
    }
    void _T5ozone_MethodoOFF()
    {
        T5ozone.SetActive(false);
    }
    //
    void _T6managing_MethodON()
    {
        T6managing.SetActive(true);
    }
    void _T6managing_MethodoOFF()
    {
        T6managing.SetActive(false);
    }
    //
    void _T7biodegrade_MethodON()
    {
        T7biodegrade.SetActive(true);
    }
    void _T7biodegrade_MethodoOFF()
    {
        T7biodegrade.SetActive(false);
    }
    //
    void _T8decomp_MethodON()
    {
        T8decomp.SetActive(true);
    }
    void _T9decomp_MethodoOFF()
    {
        T8decomp.SetActive(false);
    }
    //
    void _T9nonbiodegrade_MethodON()
    {
        T9nonbiodegrade.SetActive(true);
    }
    void _T9nonbiodegrade_MethodoOFF()
    {
        T9nonbiodegrade.SetActive(false);
    }































    //
    void _D1Ecomeans_MethodON()
    {
        D1Ecomeans.SetActive(true);
    }
    void _D1Ecomeans_MethodoOFF()
    {
        D1Ecomeans.SetActive(false);
    }
    //
    void _D2NatEcoExp_MethodON()
    {
        D2NatEcoExp.SetActive(true);
    }
    void _D2NatEcoExp_MethodoOFF()
    {
        D2NatEcoExp.SetActive(false);
    }
    //
    void _D3ArtiEcoExp_MethodON()
    {
        D3ArtiEcoExp.SetActive(true);
    }
    void _D3ArtiEcoExp_MethodoOFF()
    {
        D3ArtiEcoExp.SetActive(false);
    }
    //
    void _D4PCD_MethodON()
    {
        D4PCD.SetActive(true);
    }
    void _D4PCD_MethodoOFF()
    {
        D4PCD.SetActive(false);
    }
    //
    void _D5Herbivores_MethodON()
    {
        D5Herbivores.SetActive(true);
    }
    void _D5Herbivores_MethodoOFF()
    {
        D5Herbivores.SetActive(false);
    }
    //
    void _D6Carnivores_MethodON()
    {
        D6Carnivores.SetActive(true);
    }
    void _D6Carnivores_MethodoOFF()
    {
        D6Carnivores.SetActive(false);
    }
    //
    void _D7Omnivores_MethodON()
    {
        D7Omnivores.SetActive(true);
    }
    void _D7Omnivores_MethodoOFF()
    {
        D7Omnivores.SetActive(false);
    }
    //
    void _D8Ultimate_MethodON()
    {
        D8Ultimate.SetActive(true);
    }
    void _D8Ultimate_MethodoOFF()
    {
        D8Ultimate.SetActive(false);
    }
    //
    void _D9Primary_MethodON()
    {
        D9Primary.SetActive(true);
    }
    void _D9Primary_MethodoOFF()
    {
        D9Primary.SetActive(false);
    }
    //
    void _D10Secondary_MethodON()
    {
        D10Secondary.SetActive(true);
    }
    void _D10Secondary_MethodoOFF()
    {
        D10Secondary.SetActive(false);
    }
    //
    void _D11Tertiary_MethodON()
    {
        D11Tertiary.SetActive(true);
    }
    void _D11Tertiary_MethodoOFF()
    {
        D11Tertiary.SetActive(false);
    }
    //
    void _D12OnepercentD_MethodON()
    {
        D12OnepercentD.SetActive(true);
    }
    void _D12OnepercentD_MethodoOFF()
    {
        D12OnepercentD.SetActive(false);
    }
    //
    void _D13pesticideamount_MethodON()
    {
        D13pesticideamount.SetActive(true);
    }
    void _D13pesticideamount_MethodoOFF()
    {
        D13pesticideamount.SetActive(false);
    }
    //
    void _D14chemicaldeposits_MethodON()
    {
        D14chemicaldeposits.SetActive(true);
    }
    void _D14chemicaldeposits_MethodoOFF()
    {
        D14chemicaldeposits.SetActive(false);
    }
    //
    void _D15manD_MethodON()
    {
        D15manD.SetActive(true);
    }
    void _D15manD_MethodoOFF()
    {
        D15manD.SetActive(false);
    }
    //
    void _D16biomagni_MethodON()
    {
        D16biomagni.SetActive(true);
    }
    void _D16biomagni_MethodoOFF()
    {
        D16biomagni.SetActive(false);
    }
    //
    void _D17wecannot_MethodON()
    {
        D17wecannot.SetActive(true);
    }
    void _D17wecannot_MethodoOFF()
    {
        D17wecannot.SetActive(false);
    }
    //
    void _D18healthhazard_MethodON()
    {
        D18healthhazard.SetActive(true);
    }
    void _D18healthhazard_MethodoOFF()
    {
        D18healthhazard.SetActive(false);
    }
    //
    void _D19O3_MethodON()
    {
        D19O3.SetActive(true);
    }
    void _D19O3_MethodoOFF()
    {
        D19O3.SetActive(false);
    }
    //
    void _D20ozonepoisionous_MethodON()
    {
        D20ozonepoisionous.SetActive(true);
    }
    void _D20ozonepoisionous_MethodoOFF()
    {
        D20ozonepoisionous.SetActive(false);
    }
    //
    void _D21ozoneformation_MethodON()
    {
        D21ozoneformation.SetActive(true);
    }
    void _D21ozoneformation_MethodoOFF()
    {
        D21ozoneformation.SetActive(false);
    }
    //
    void _D22CFC_MethodON()
    {
        D22CFC.SetActive(true);
    }
    void _D22CFC_MethodoOFF()
    {
        D22CFC.SetActive(false);
    }
    //
    void _D231987_MethodON()
    {
        D231987.SetActive(true);
    }
    void _D231987_MethodoOFF()
    {
        D231987.SetActive(false);
    }
    //
    void _D24100D_MethodON()
    {
        D24100D.SetActive(true);
    }
    void _D24100D_MethodoOFF()
    {
        D24100D.SetActive(false);
    }
    //
    void _D25biodegrade_MethodON()
    {
        D25biodegrade.SetActive(true);
    }
    void _D25biodegrade_MethodoOFF()
    {
        D25biodegrade.SetActive(false);
    }
    //
    void _D26disposable_MethodON()
    {
        D26disposable.SetActive(true);
    }
    void _D26disposable_MethodoOFF()
    {
        D26disposable.SetActive(false);
    }
    //
    void _D27decompdefD_MethodON()
    {
        D27decompdefD.SetActive(true);
    }
    void _D27decompdefD_MethodoOFF()
    {
        D27decompdefD.SetActive(false);
    }




















    
    
    
    
    //
    void _Bacteria_MethodON()
    {
        Bacteria.SetActive(true);
    }
    void _Bacteria_MethodoOFF()
    {
        Bacteria.SetActive(false);
    }
    //

    void _vulture_MethodON()
    {
        vulture.SetActive(true);
    }
    void _vulture_MethodoOFF()
    {
        vulture.SetActive(false);
    }
    //
    void _PCDpic_MethodON()
    {
        PCDpic.SetActive(true);
    }
    void _PCDpic_MethodoOFF()
    {
        PCDpic.SetActive(false);
    }
    //
    void _prism1_MethodON()
    {
        prism1.SetActive(true);
    }
    void _prism1_MethodoOFF()
    {
        prism1.SetActive(false);
    }
    //
    void _prism2_MethodON()
    {
        prism2.SetActive(true);
    }
    void _prism2_MethodoOFF()
    {
        prism2.SetActive(false);
    }
    //
    void _eatingfish_MethodON()
    {
        eatingfish.SetActive(true);
    }
    void _eatingfish_MethodoOFF()
    {
        eatingfish.SetActive(false);
    }
    //
    void _No_MethodON()
    {
        No.SetActive(true);
    }
    void _No_MethodoOFF()
    {
        No.SetActive(false);
    }
    //
    void _fullforms_MethodON()
    {
        fullforms.SetActive(true);
    }
    void _fullforms_MethodoOFF()
    {
        fullforms.SetActive(false);
    }
    //
    void _twotypes_MethodON()
    {
        twotypes.SetActive(true);
    }
    void _twotypes_MethodoOFF()
    {
        twotypes.SetActive(false);
    }
    //
    void _leaffall_MethodON()
    {
        leaffall.SetActive(true);
    }
    void _leaffall_MethodoOFF()
    {
        leaffall.SetActive(false);
    }
    //
    void _sunrays_MethodON()
    {
        sunrays.SetActive(true);
    }
    void _sunrays_MethodoOFF()
    {
        sunrays.SetActive(false);
    }
    //
    void _bond_MethodON()
    {
        bond.SetActive(true);
    }
    void _bond_MethodoOFF()
    {
        sunrays.SetActive(false);
    }
    //
    void _reuse_MethodON()
    {
        reuse.SetActive(true);
    }
    void _reuse_MethodoOFF()
    {
        reuse.SetActive(false);
    }
    //
    void _horse_MethodON()
    {
        horse.SetActive(true);
    }
    void _horse_MethodoOFF()
    {
        horse.SetActive(false);
    }
    //
    void _horseskeleton_MethodON()
    {
        horseskeleton.SetActive(true);
    }
    void _horseskeleton_MethodoOFF()
    {
        horseskeleton.SetActive(false);
    }
    //























// Animations




void _Vulture_animationAnimmethod()
    {

        anim = vulture.GetComponent<Animator>();
        anim.Play("Vulture anim");
    }

    //

    void _Fish_animationAnimmethod()
    {

        anim = fish_anim.GetComponent<Animator>();
        anim.Play("Fish animation");
    }
    //
    void _Leaf_animationAnimmethod()
    {

        anim = leaf_anim.GetComponent<Animator>();
        anim.Play("Leaf animation");
    }
    //
    void _SunRay_animationAnimmethod()
    {

        anim = sunrays_anim.GetComponent<Animator>();
        anim.Play("Sun ray aniamtion");
    }

















    //
    void _1intro_audioMethod()

    {
        myAudio.clip = intro;
        myAudio.Play();
    }

    void _2_in_nature_audioMethod()

    {
        myAudio.clip = in_nature;
        myAudio.Play();
    }

    void _3balance_nature_audioMethod()

    {
        myAudio.clip = balance_nature;
        myAudio.Play();
    }

    void _4biotic_audioMethod()

    {
        myAudio.clip = biotic;
        myAudio.Play();
    }

    void _5ecosystem_formation_audioMethod()

    {
        myAudio.clip = ecosystem_formation;
        myAudio.Play();
    }

    void _6biotic_abiotic_audioMethod()

    {
        myAudio.clip = biotic_abiotic;
        myAudio.Play();
    }

    void _7natural_eco_exp_audioMethod()

    {
        myAudio.clip = natural_eco_exp;
        myAudio.Play();
    }

    void _8artificial_eco_exp_audioMethod()

    {
        myAudio.clip = artificial_eco_exp;
        myAudio.Play();
    }

    void _9ecosystem_audioMethod()

    {
        myAudio.clip = ecosystem;
        myAudio.Play();
    }

    void _10diff_org_in_ecosystem_audioMethod()

    {
        myAudio.clip = diff_org_in_ecosystem;
        myAudio.Play();
    }

    void _11water_organisms_audioMethod()

    {
        myAudio.clip = water_organisms;
        myAudio.Play();
    }

    void _12water_plants_audioMethod()

    {
        myAudio.clip = water_plants;
        myAudio.Play();
    }

    void _13bacteria_audioMethod()

    {
        myAudio.clip = bacteria;
        myAudio.Play();
    }

    void _14biotic_pond_eco_audioMethod()

    {
        myAudio.clip = biotic_pond_eco;
        myAudio.Play();
    }

    void _15abiotic_pond_eco_audioMethod()

    {
        myAudio.clip = abiotic_pond_eco;
        myAudio.Play();
    }

    void _16three_types_audioMethod()

    {
        myAudio.clip = three_types;
        myAudio.Play();
    }

    void _17producers_audioMethod()

    {
        myAudio.clip = producers;
        myAudio.Play();
    }

    void _18herbivores_audioMethod()

    {
        myAudio.clip = herbivores;
        myAudio.Play();
    }

    void _19carnivores_audioMethod()

    {
        myAudio.clip = carnivores;
        myAudio.Play();
    }
    
    void _20omnivores_audioMethod()

    {
        myAudio.clip = omnivores;
        myAudio.Play();
    }

    void _21consumers_audioMethod()

    {
        myAudio.clip = consumers;
        myAudio.Play();
    }

    void _22decomposers_audioMethod()

    {
        myAudio.clip = decomposers;
        myAudio.Play();
    }

    void _23decomposition_audioMethod()

    {
        myAudio.clip = decomposition;
        myAudio.Play();
    }

    void _24foodchain_web_audioMethod()

    {
        myAudio.clip = foodchain_web;
        myAudio.Play();
    }

    void _25living_things_energy_audioMethod()

    {
        myAudio.clip = living_things_energy;
        myAudio.Play();
    }

    void _26deer_to_lion_audioMethod()

    {
        myAudio.clip = deer_to_lion;
        myAudio.Play();
    }

    void _27where_deer_get_audioMethod()

    {
        myAudio.clip = where_deer_get;
        myAudio.Play();
    }

    void _28deer_from_plant_audioMethod()

    {
        myAudio.clip = deer_from_plant;
        myAudio.Play();
    }

    void _29plant_from_sun_audioMethod()

    {
        myAudio.clip = plant_from_sun;
        myAudio.Play();
    }

    void _30sun_ultimate_source_audioMethod()

    {
        myAudio.clip = sun_ultimate_source;
        myAudio.Play();
    }

    void _31primary_consumers_audioMethod()

    {
        myAudio.clip = primary_consumers;
        myAudio.Play();
    }

    void _32secondary_consumers_audioMethod()

    {
        myAudio.clip = secondary_consumers;
        myAudio.Play();
    }

    void _33teritiary_consumersaudioMethod()

    {
        myAudio.clip = teritiary_consumers;
        myAudio.Play();
    }

    void _34energy_flow_audioMethod()

    {
        myAudio.clip = energy_flow;
        myAudio.Play();
    }

    void _35plant_deer_sun_audioMethod()

    {
        myAudio.clip = plant_deer_sun;
        myAudio.Play();
    }

    void _36plant_rabbit_sun_audioMethod()

    {
        myAudio.clip = plant_rabbit_sun;
        myAudio.Play();
    }

    void _37food_chain_def_audioMethod()

    {
        myAudio.clip = food_chain_def;
        myAudio.Play();
    }

    void _38trophic_levels_audioMethod()

    {
        myAudio.clip = trophic_levels;
        myAudio.Play();
    }

    void _39one_organism_to_another_audioMethod()

    {
        myAudio.clip = one_organism_to_another;
        myAudio.Play();
    }

    void _40energy_flow_in_foodchain_audioMethod()

    {
        myAudio.clip = energy_flow_in_foodchain;
        myAudio.Play();
    }

    void _41one_percent_audioMethod()

    {
        myAudio.clip = one_percent;
        myAudio.Play();
    }

    void _42from_plant_to_animal_body_audioMethod()

    {
        myAudio.clip = from_plant_to_animal_body;
        myAudio.Play();
    }
    
    void _43body_heat_audioMethod()

    {
        myAudio.clip = body_heat;
        myAudio.Play();
    }

    void _44digestion_audioMethod()

    {
        myAudio.clip = digestion;
        myAudio.Play();
    }

    void _45ten_percent_audioMethod()

    {
        myAudio.clip = ten_percent;
        myAudio.Play();
    }

    void _46level1_to_2_audioMethod()

    {
        myAudio.clip = level1_to_2;
        myAudio.Play();
    }

    void _47level2_to_3_audioMethod()

    {
        myAudio.clip = level2_to_3;
        myAudio.Play();
    }

    void _48we_can_observe_audioMethod()

    {
        myAudio.clip = we_can_observe;
        myAudio.Play();
    }

    void _49unidirectional_audioMethod()

    {
        myAudio.clip = unidirectional;
        myAudio.Play();
    }

    void _50decline_in_organism_count_audioMethod()

    {
        myAudio.clip = decline_in_organism_count;
        myAudio.Play();
    }

    void _51many_foodchains_audioMethod()

    {
        myAudio.clip = many_foodchains;
        myAudio.Play();
    }

    void _52foodweb_audioMethod()

    {
        myAudio.clip = foodweb;
        myAudio.Play();
    }
    


//Part2audio



void _P2A1_audioMethod()

    {
        myAudio.clip = a1;
        myAudio.Play();
    }
    void _P2A2_audioMethod()

    {
        myAudio.clip = a2;
        myAudio.Play();
    }
    void _P2A3_audioMethod()

    {
        myAudio.clip = a3;
        myAudio.Play();
    }
    void _P2A4_audioMethod()

    {
        myAudio.clip = a4;
        myAudio.Play();
    }
    void _P2A5_audioMethod()

    {
        myAudio.clip = a5;
        myAudio.Play();
    }
    void _P2A6_audioMethod()

    {
        myAudio.clip = a6;
        myAudio.Play();
    }
    void _P2A7_audioMethod()

    {
        myAudio.clip = a7;
        myAudio.Play();
    }
    void _P2A8_audioMethod()

    {
        myAudio.clip = a8;
        myAudio.Play();
    }
    void _P2A9_audioMethod()

    {
        myAudio.clip = a9;
        myAudio.Play();
    }
    void _P2A10_audioMethod()

    {
        myAudio.clip = a10;
        myAudio.Play();
    }
    void _P2A11_audioMethod()

    {
        myAudio.clip = a11;
        myAudio.Play();
    }
    void _P2A12_audioMethod()

    {
        myAudio.clip = a12;
        myAudio.Play();
    }
    void _P2A13_audioMethod()

    {
        myAudio.clip = a13;
        myAudio.Play();
    }
    void _P2A14_audioMethod()

    {
        myAudio.clip = a14;
        myAudio.Play();
    }
    void _P2A15_audioMethod()

    {
        myAudio.clip = a15;
        myAudio.Play();
    }
    void _P2A16_audioMethod()

    {
        myAudio.clip = a16;
        myAudio.Play();
    }
    void _P2A17_audioMethod()

    {
        myAudio.clip = a17;
        myAudio.Play();
    }
    void _P2A18_audioMethod()

    {
        myAudio.clip = a18;
        myAudio.Play();
    }
    void _P2A19_audioMethod()

    {
        myAudio.clip = a19;
        myAudio.Play();
    }
    void _P2A20_audioMethod()

    {
        myAudio.clip = a20;
        myAudio.Play();
    }
    void _P2A21_audioMethod()

    {
        myAudio.clip = a21;
        myAudio.Play();
    }
    void _P2A22_audioMethod()

    {
        myAudio.clip = a22;
        myAudio.Play();
    }
    void _P2A23_audioMethod()

    {
        myAudio.clip = a23;
        myAudio.Play();
    }
    void _P2A24_audioMethod()

    {
        myAudio.clip = a24;
        myAudio.Play();
    }
    void _P2A25_audioMethod()

    {
        myAudio.clip = a25;
        myAudio.Play();
    }
    void _P2A26_audioMethod()

    {
        myAudio.clip = a26;
        myAudio.Play();
    }
    void _P2A27_audioMethod()

    {
        myAudio.clip = a27;
        myAudio.Play();
    }
    void _P2A28_audioMethod()

    {
        myAudio.clip = a28;
        myAudio.Play();
    }
    void _P2A29_audioMethod()

    {
        myAudio.clip = a29;
        myAudio.Play();
    }
    void _P2A30_audioMethod()

    {
        myAudio.clip = a30;
        myAudio.Play();
    }
    void _P2A31_audioMethod()

    {
        myAudio.clip = a31;
        myAudio.Play();
    }
    void _P2A32_audioMethod()

    {
        myAudio.clip = a32;
        myAudio.Play();
    }
    void _P2A33_audioMethod()

    {
        myAudio.clip = a33;
        myAudio.Play();
    }
    void _P2A34_audioMethod()

    {
        myAudio.clip = a34;
        myAudio.Play();
    }
    void _P2A35_audioMethod()

    {
        myAudio.clip = a35;
        myAudio.Play();
    }
    void _P2A36_audioMethod()

    {
        myAudio.clip = a36;
        myAudio.Play();
    }
    void _P2A37_audioMethod()

    {
        myAudio.clip = a37;
        myAudio.Play();
    }
    void _P2A38_audioMethod()

    {
        myAudio.clip = a38;
        myAudio.Play();
    }
    void _P2A39_audioMethod()

    {
        myAudio.clip = a39;
        myAudio.Play();
    }
    void _P2A40_audioMethod()

    {
        myAudio.clip = a40;
        myAudio.Play();
    }
    void _P2A41_audioMethod()

    {
        myAudio.clip = a41;
        myAudio.Play();
    }
    void _P2A42_audioMethod()

    {
        myAudio.clip = a42;
        myAudio.Play();
    }
    void _P2A43_audioMethod()

    {
        myAudio.clip = a43;
        myAudio.Play();
    }
    void _P2A44_audioMethod()

    {
        myAudio.clip = a44;
        myAudio.Play();
    }
    void _P2A45_audioMethod()

    {
        myAudio.clip = a45;
        myAudio.Play();
    }
    void _P2A46_audioMethod()

    {
        myAudio.clip = a46;
        myAudio.Play();
    }
    void _P2A47_audioMethod()

    {
        myAudio.clip = a47;
        myAudio.Play();
    }
    void _P2A48_audioMethod()

    {
        myAudio.clip = a48;
        myAudio.Play();
    }
    void _P2A49_audioMethod()

    {
        myAudio.clip = a49;
        myAudio.Play();
    }
    void _P2A50_audioMethod()

    {
        myAudio.clip = a50;
        myAudio.Play();
    }
    void _P2A51_audioMethod()

    {
        myAudio.clip = a51;
        myAudio.Play();
    }
    void _P2A52_audioMethod()

    {
        myAudio.clip = a52;
        myAudio.Play();
    }
    void _P2A53_audioMethod()

    {
        myAudio.clip = a53;
        myAudio.Play();
    }
    void _P2A54_audioMethod()

    {
        myAudio.clip = a54;
        myAudio.Play();
    }
    void _P2A55_audioMethod()

    {
        myAudio.clip = a55;
        myAudio.Play();
    }
    void _P2A56_audioMethod()

    {
        myAudio.clip = a56;
        myAudio.Play();
    }
    void _P2A57_audioMethod()

    {
        myAudio.clip = a57;
        myAudio.Play();
    }
    void _P2A58_audioMethod()

    {
        myAudio.clip = a58;
        myAudio.Play();
    }
    void _P2A59_audioMethod()

    {
        myAudio.clip = a59;
        myAudio.Play();
    }
    void _P2A60_audioMethod()

    {
        myAudio.clip = a60;
        myAudio.Play();
    }
    void _P2A61_audioMethod()

    {
        myAudio.clip = a61;
        myAudio.Play();
    }
    void _P2A62_audioMethod()

    {
        myAudio.clip = a62;
        myAudio.Play();
    }
    void _P2A63_audioMethod()

    {
        myAudio.clip = a63;
        myAudio.Play();
    }
    void _P2A64_audioMethod()

    {
        myAudio.clip = a64;
        myAudio.Play();
    }
    void _P2A65_audioMethod()

    {
        myAudio.clip = a56;
        myAudio.Play();
    }







    void _Goto_menuMethodON()
    {
        ////open initial city scene
        //GameObject loader = GameObject.Find("Sceneloader Canvas");
        //loader.GetComponent<SceneLoader>().LoadScene(0);
        ////SceneManager.LoadScene("Miniworld"); 
        ///
        gameplayObject.SetActive(true);
        explanationObject.SetActive(false);
    }





















}



