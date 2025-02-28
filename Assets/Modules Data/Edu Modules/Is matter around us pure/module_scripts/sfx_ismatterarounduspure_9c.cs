using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sfx_ismatterarounduspure_9c : MonoBehaviour
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

    public void Level3()
    {
        StartCoroutine(DelayLv3MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        SaveProgress(1, 0, 2);

    }
    IEnumerator DelayLv3MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv3MiniGame.Output();
    }

    public TargetController lv3MiniGame;
    public void Savepoint1()
    {
        SaveProgress(1, 0, 2);
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

        InitializeFromCheckpoint();
        level();
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject part1;
    public GameObject part2;


    public GameObject matter;
    public GameObject metals_;
    public GameObject nonmetals;
    public GameObject chart1;
    public GameObject chart2;
    public GameObject chart3;
    public GameObject chart4;
    public GameObject Properties_of_a_Solution1;
    public GameObject Properties_of_a_Solution2;
    public GameObject Properties_of_a_Solution3;
    public GameObject Properties_of_a_Solution4;
    public GameObject Properties_of_a_Solution5;
    public GameObject Properties_of_a_Solution6;
    public GameObject Concentration_of_a_Solution1;
    public GameObject Concentration_of_a_Solution2;
    public GameObject Properties_of_a_Suspension1;
    public GameObject Properties_of_a_Suspension2;
    public GameObject Properties_of_a_Suspension3;
    public GameObject Properties_of_a_Suspension4;
    public GameObject Properties_of_a_Suspension5;

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
    public GameObject T34;

    //des

    public GameObject Des1;


    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]

    public GameObject SnAboard_anim;
    public GameObject epi_anim;
    public GameObject pouring_iron_fillings;
    public GameObject pouring_sulfur;
    public GameObject mix_anim;
    public GameObject magnet_anim;
    public GameObject ironfillingsattract_anim;
    public GameObject magnet_anim2;
    public GameObject carbon_disulfide;
    public GameObject atom;
    public GameObject h2o;
    public GameObject hittinganim;
    public GameObject tyndal_effect_;
    public GameObject tyndal_effect_2;
    public GameObject evaporation_;
    public GameObject immiscible_liquids;
    public GameObject Centrifugation_;
    public GameObject sublimationanim;
    public GameObject Crystallization_anim;



    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;


    public AudioClip Introduction;
    public AudioClip Classification_of_matter;
    public AudioClip Substance;
    public AudioClip Elements_1;
    public AudioClip Elements_2;
    public AudioClip types_of_elements;
    public AudioClip metals;
    public AudioClip non_metals;
    public AudioClip metalloids;
    public AudioClip Compounds;
    public AudioClip mixtures;
    public AudioClip Activity;
    public AudioClip Phase;
    public AudioClip Homogeneous_Materials;
    public AudioClip Heterogeneous_Mixtures;
    public AudioClip Solutions1;
    public AudioClip Solutions2;
    public AudioClip Solutions3;
    public AudioClip Solutions4;
    public AudioClip Solutions5;
    public AudioClip Types_of_Solutions;
    public AudioClip unsaturated_solution;
    public AudioClip saturated_solution;
    public AudioClip supersaturated_solution;
    public AudioClip Properties_of_a_Solution;
    public AudioClip Concentration_of_solution;
    public AudioClip Suspension;
    public AudioClip Properties_of_Suspension;
    public AudioClip Colloidal_solution;
    public AudioClip Tyndall_Effect;

    public AudioClip Separating_the_Components_of_a_Mixture;
    public AudioClip Evaporation;
    public AudioClip Centrifugation;
    public AudioClip Separation_of_Two_Immiscible_Liquids;
    public AudioClip Sublimation;
    public AudioClip Chromatography;
    public AudioClip Separation_of_two_miscible_liquids;
    public AudioClip Distillation;
    public AudioClip Application_of_fractional_distillation;
    public AudioClip Crystallization;
    public AudioClip Crystallizationactivity;
    public AudioClip Physical_Change;
    public AudioClip Chemical_Change;

   



    // Method ON/OFF

    //
    void part1_MethodOFF()
    {
        part1.SetActive(false);
    }
    //
    void part2_MethodON()
    {
        part2.SetActive(true);
    }
    //













    //
    //
    void _T1_MethodON()
    {
        T1.SetActive(true);
    }
    //
    void _T2_MethodON()
    {
        T2.SetActive(true);
    }
    //
    void _T3_MethodON()
    {
        T3.SetActive(true);
    }
    //
    void _T4_MethodON()
    {
        T4.SetActive(true);
    }
    //
    void _T5_MethodON()
    {
        T5.SetActive(true);
    }
    //
    void _T6_MethodON()
    {
        T6.SetActive(true);
    }
    //
    void _T7_MethodON()
    {
        T7.SetActive(true);
    }
    //
    void _T8_MethodON()
    {
        T8.SetActive(true);
    }
    //
    void _T9_MethodON()
    {
        T9.SetActive(true);
    }
    //
    void _T10_MethodON()
    {
        T10.SetActive(true);
    }
    //
    void _T11_MethodON()
    {
        T11.SetActive(true);
    }
    //
    void _T12_MethodON()
    {
        T8.SetActive(true);
    }
    //
    void _T13_MethodON()
    {
        T13.SetActive(true);
    }
    //
    void _T14_MethodON()
    {
        T14.SetActive(true);
    }
    //
    void _T15_MethodON()
    {
        T15.SetActive(true);
    }
    //
    void _T16_MethodON()
    {
        T16.SetActive(true);
    }
    //
    void _T17_MethodON()
    {
        T17.SetActive(true);
    }
    //
    void _T18_MethodON()
    {
        T18.SetActive(true);
    }
    //
    void _T19_MethodON()
    {
        T19.SetActive(true);
    }
    //
    void _T20_MethodON()
    {
        T20.SetActive(true);
    }
    //
    void _T21_MethodON()
    {
        T21.SetActive(true);
    }
    //
    void _T22_MethodON()
    {
        T22.SetActive(true);
    }
    //
    void _T23_MethodON()
    {
        T23.SetActive(true);
    }
    //
    void _T24_MethodON()
    {
        T24.SetActive(true);
    }
    //
    void _T25_MethodON()
    {
        T25.SetActive(true);
    }
    //
    void _T26_MethodON()
    {
        T26.SetActive(true);
    }
    //
    void _T27_MethodON()
    {
        T27.SetActive(true);
    }
    //
    void _T28_MethodON()
    {
        T28.SetActive(true);
    }
    //
    void _T29_MethodON()
    {
        T29.SetActive(true);
    }
    //
    void _T30_MethodON()
    {
        T30.SetActive(true);
    }
    //
    void _T31_MethodON()
    {
        T31.SetActive(true);
    }
    //
    void _T32_MethodON()
    {
        T32.SetActive(true);
    }
    //
    void _T33_MethodON()
    {
        T33.SetActive(true);
    }
    //
    void _T34_MethodON()
    {
        T34.SetActive(true);
    }
    //




    //
    void matter_MethodON()
    {
        matter.SetActive(true);
    }
    //
    void metals_MethodON()
    {
        metals_.SetActive(true);
    }
    //
    void nonmetals_MethodON()
    {
        nonmetals.SetActive(true);
    }
    //
    void chart1_MethodON()
    {
        chart1.SetActive(true);
    }
    //
    void chart2_MethodON()
    {
        chart2.SetActive(true);
    }
    //
    void chart3_MethodON()
    {
        chart3.SetActive(true);
    }
    //
    void chart4_MethodON()
    {
        chart4.SetActive(true);
    }
    //
    void Properties_of_a_Solution1_MethodON()
    {
        Properties_of_a_Solution1.SetActive(true);
    }

    //
    void Properties_of_a_Solution1_Methodoff()
    {
        Properties_of_a_Solution1.SetActive(false);
    }

    //
    void Properties_of_a_Solution2_MethodON()
    {
        Properties_of_a_Solution2.SetActive(true);
    }

    //
    void Properties_of_a_Solution2_Methodoff()
    {
        Properties_of_a_Solution2.SetActive(false);
    }

    //
    void Properties_of_a_Solution3_MethodON()
    {
        Properties_of_a_Solution3.SetActive(true);
    }

    //
    void Properties_of_a_Solution3_Methodoff()
    {
        Properties_of_a_Solution3.SetActive(false);
    }

    //
    void Properties_of_a_Solution4_MethodON()
    {
        Properties_of_a_Solution4.SetActive(true);
    }

    //
    void Properties_of_a_Solution4_Methodoff()
    {
        Properties_of_a_Solution4.SetActive(false);
    }

    //
    void Properties_of_a_Solution5_MethodON()
    {
        Properties_of_a_Solution5.SetActive(true);
    }

    //
    void Properties_of_a_Solution5_Methodoff()
    {
        Properties_of_a_Solution5.SetActive(false);
    }

    //
    void Properties_of_a_Solution6_MethodON()
    {
        Properties_of_a_Solution6.SetActive(true);
    }

    //
    void Properties_of_a_Solution6_Methodoff()
    {
        Properties_of_a_Solution6.SetActive(false);
    }

    //
    void Concentration_of_a_Solution1_MethodON()
    {
        Concentration_of_a_Solution1.SetActive(true);
    }
    //
    void Concentration_of_a_Solution2_MethodON()
    {
        Concentration_of_a_Solution2.SetActive(true);
    }
    //
    void Properties_of_a_Suspension1_MethodON()
    {
        Properties_of_a_Suspension1.SetActive(true);
    }
    //
    void Properties_of_a_Suspension2_MethodON()
    {
        Properties_of_a_Suspension2.SetActive(true);
    }
    //
    void Properties_of_a_Suspension3_MethodON()
    {
        Properties_of_a_Suspension3.SetActive(true);
    }
    //
    void Properties_of_a_Suspension4_MethodON()
    {
        Properties_of_a_Suspension4.SetActive(true);
    }
    //
    void Properties_of_a_Suspension5_MethodON()
    {
        Properties_of_a_Suspension5.SetActive(true);
    }
    //








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















    // Animations

    void _SnA_animationAnimmethod()
    {

        anim = SnAboard_anim.GetComponent<Animator>();
        anim.Play("SnA board anim");
    }
    //
    void pouring_iron_fillings_Animmethod()
    {

        anim = pouring_iron_fillings.GetComponent<Animator>();
        anim.Play("pouring iron fillings");
    }

    void pouring_sulfur_Animmethod()
    {

        anim = pouring_sulfur.GetComponent<Animator>();
        anim.Play("pouring sulfur powder");
    }

    void mix_anim_Animmethod()
    {

        anim = mix_anim.GetComponent<Animator>();
        anim.Play("mix anim");
    }

    void pouring_iron_fillings2_Animmethod()
    {

        anim = pouring_iron_fillings.GetComponent<Animator>();
        anim.Play("pouring iron fillings 2");
    }

    void pouring_sulfur2_Animmethod()
    {

        anim = pouring_sulfur.GetComponent<Animator>();
        anim.Play("pouring sulfur powder 2");
    }

    void magnet_anim_Animmethod()
    {

        anim = magnet_anim.GetComponent<Animator>();
        anim.Play("magnet anim");
    }

    void ironfillingsattract_anim_Animmethod()
    {

        anim = magnet_anim.GetComponent<Animator>();
        anim.Play("magnet anim");
    }


    void magnet_anim2_Animmethod()
    {

        anim = magnet_anim.GetComponent<Animator>();
        anim.Play("magnet anim");
    }

    void carbondisfulidepouring_Animmethod()
    {

        anim = carbon_disulfide.GetComponent<Animator>();
        anim.Play("carbon_disulfide_pouring");
    }

    void atom_Animmethod()
    {

        anim = atom.GetComponent<Animator>();
        anim.Play("atom_anim");
    }

    void h2o_Animmethod()
    {

        anim = h2o.GetComponent<Animator>();
        anim.Play("h2o");
    }

    void hittinganim_Animmethod()
    {

        anim = hittinganim.GetComponent<Animator>();
        anim.Play("hitting_anim");
    }

    void tyndal_effect__Animmethod()
    {

        anim = tyndal_effect_.GetComponent<Animator>();
        anim.Play("tyndal_effect_anim");
    }

    void tyndal_effect_2_Animmethod()
    {

        anim = tyndal_effect_2.GetComponent<Animator>();
        anim.Play("Tyndall Effect");
    }




    //
    void evaporation_Animmethod()
    {

        anim = evaporation_.GetComponent<Animator>();
        anim.Play("Evaporation");
    }
    //
    void immiscibleliquids_Animmethod()
    {

        anim = immiscible_liquids.GetComponent<Animator>();
        anim.Play("immiscible liquids anim");
    }
    //
    void centrifugationanim_Animmethod()
    {

        anim = Centrifugation_.GetComponent<Animator>();
        anim.Play("Centrifugation_anim");
    }
    //

    void sublimationanim_Animmethod()
    {

        anim = sublimationanim.GetComponent<Animator>();
        anim.Play("sublimation anim");
    }
    //
    void Crystallizationanim_Animmethod()
    {

        anim = Crystallization_anim.GetComponent<Animator>();
        anim.Play("Crystallization anim");
    }
    //







    // Audio

    //
    void introduction_audioMethod()
    {
        myAudio.clip = Introduction;
        myAudio.Play();
    }
    //
    void Classification_of_matter_audioMethod()
    {
        myAudio.clip = Classification_of_matter;
        myAudio.Play();
    }
    //
    void Substance_audioMethod()
    {
        myAudio.clip = Substance;
        myAudio.Play();
    }
    //
    void Elements_1_audioMethod()
    {
        myAudio.clip = Elements_1;
        myAudio.Play();
    }
    //
    void Elements_2_audioMethod()
    {
        myAudio.clip = Elements_2;
        myAudio.Play();
    }
    //
    void types_of_elements_audioMethod()
    {
        myAudio.clip = types_of_elements;
        myAudio.Play();
    }
    //
    void metals_audioMethod()
    {
        myAudio.clip = metals;
        myAudio.Play();
    }
    //
    void non_metals_audioMethod()
    {
        myAudio.clip = non_metals;
        myAudio.Play();
    }
    //
    void metalloids_audioMethod()
    {
        myAudio.clip = metalloids;
        myAudio.Play();
    }
    //
    void Compounds_audioMethod()
    {
        myAudio.clip = Compounds;
        myAudio.Play();
    }
    //
    void mixtures_audioMethod()
    {
        myAudio.clip = mixtures;
        myAudio.Play();
    }
    //
    void Activity_audioMethod()
    {
        myAudio.clip = Activity;
        myAudio.Play();
    }
    //
    void Phase_audioMethod()
    {
        myAudio.clip = Phase;
        myAudio.Play();
    }
    //
    void Homogeneous_Materials_audioMethod()
    {
        myAudio.clip = Homogeneous_Materials;
        myAudio.Play();
    }
    //
    void Heterogeneous_Mixtures_audioMethod()
    {
        myAudio.clip = Heterogeneous_Mixtures;
        myAudio.Play();
    }
    //
    void Solutions1_audioMethod()
    {
        myAudio.clip = Solutions1;
        myAudio.Play();
    }
    //
    void Solutions2_audioMethod()
    {
        myAudio.clip = Solutions2;
        myAudio.Play();
    }
    //
    void Solutions3_audioMethod()
    {
        myAudio.clip = Solutions3;
        myAudio.Play();
    }
    //
    void Solutions4_audioMethod()
    {
        myAudio.clip = Solutions4;
        myAudio.Play();
    }

    //
    void Solutions5_audioMethod()
    {
        myAudio.clip = Solutions5;
        myAudio.Play();
    }
    //
    void Types_of_Solutions_audioMethod()
    {
        myAudio.clip = Types_of_Solutions;
        myAudio.Play();
    }
    //
    void unsaturated_solution_audioMethod()
    {
        myAudio.clip = unsaturated_solution;
        myAudio.Play();
    }
    //
    void saturated_solution_audioMethod()
    {
        myAudio.clip = saturated_solution;
        myAudio.Play();
    }
    //
    void supersaturated_solution_audioMethod()
    {
        myAudio.clip = supersaturated_solution;
        myAudio.Play();
    }
    //
    void Properties_of_a_Solution_audioMethod()
    {
        myAudio.clip = Properties_of_a_Solution;
        myAudio.Play();
    }
    //
    void Concentration_of_solution_audioMethod()
    {
        myAudio.clip = Concentration_of_solution;
        myAudio.Play();
    }
    //
    void Suspension_audioMethod()
    {
        myAudio.clip = Suspension;
        myAudio.Play();
    }
    //
    void Properties_of_Suspension_audioMethod()
    {
        myAudio.clip = Properties_of_Suspension;
        myAudio.Play();
    }
    //
    void Colloidal_solution_audioMethod()
    {
        myAudio.clip = Colloidal_solution;
        myAudio.Play();
    }
    //
    void Tyndall_Effect_audioMethod()
    {
        myAudio.clip = Tyndall_Effect;
        myAudio.Play();
    }
    //


    //
    void Separating_the_Components_of_a_Mixture_audioMethod()
    {
        myAudio.clip = Separating_the_Components_of_a_Mixture;
        myAudio.Play();
    }
    //
    void Evaporation_audioMethod()
    {
        myAudio.clip = Evaporation;
        myAudio.Play();
    }
    //
    void Centrifugation_audioMethod()
    {
        myAudio.clip = Centrifugation;
        myAudio.Play();
    }
    //
    void Separation_of_Two_Immiscible_Liquids_audioMethod()
    {
        myAudio.clip = Separation_of_Two_Immiscible_Liquids;
        myAudio.Play();
    }
    //
    void Sublimation_audioMethod()
    {
        myAudio.clip = Sublimation;
        myAudio.Play();
    }
    //
    void Chromatography_audioMethod()
    {
        myAudio.clip = Chromatography;
        myAudio.Play();
    }
    //
    void Separation_of_two_miscible_liquids_audioMethod()
    {
        myAudio.clip = Separation_of_two_miscible_liquids;
        myAudio.Play();
    }
    //
    void Distillation_audioMethod()
    {
        myAudio.clip = Distillation;
        myAudio.Play();
    }
    //
    void Application_of_fractional_distillation_audioMethod()
    {
        myAudio.clip = Application_of_fractional_distillation;
        myAudio.Play();
    }
    //
    void Crystallization_audioMethod()
    {
        myAudio.clip = Crystallization;
        myAudio.Play();
    }
    //
    void Crystallizationactivity_audioMethod()
    {
        myAudio.clip = Crystallizationactivity;
        myAudio.Play();
    }
    //
    void Physical_Change_audioMethod()
    {
        myAudio.clip = Physical_Change;
        myAudio.Play();
    }
    //
    void Chemical_Change_audioMethod()
    {
        myAudio.clip = Chemical_Change;
        myAudio.Play();
    }
    //

    public void TurnOnMesh(MeshRenderer meshRenderer)
    {
        meshRenderer.enabled = true;
    }

    public List<TargetController> miniGames = new List<TargetController>();
    public List<GameObject> doors = new List<GameObject>();
    private int objecInd = 0;
    private int miniGameInd = 0;
    public void PlacedObject(int index)
    {
        objecInd++;

        if(objecInd == index)
        {
            objecInd = 0;
            miniGames[miniGameInd].EndMiniGame();
            doors[miniGameInd].SetActive(true);
            miniGameInd++;
            StepComplete();
        }
    }

    public void PlayAnimTrigg(Animator anim)
    {
        anim.SetTrigger("Trigger"); // Replace "TriggerName" with the actual name of your trigger
    }

    private TargetController currentMiniGame;
    public List<Button> questionBtn = new List<Button>();
    public GameObject questions;
    public void PlayAnim(TargetController obj)
    {
        int correctIndex = int.Parse(obj.checkName);
        currentMiniGame = obj;

        for (int i = 0; i < questionBtn.Count; i++)
        {
            if (i == correctIndex)
            {
                questionBtn[i].onClick.AddListener(CorrectAnswer);
            }
            else
            {
                questionBtn[i].onClick.AddListener(WrongAnswer);
            }
        }
        StartCoroutine(QuestionDelay());
        Animator anim = obj.gameObject.GetComponent<Animator>();
        anim.SetTrigger("Trigger");
    }

    public void TurnOnGameobjects(GameObject gameobj)
    {
        gameobj.SetActive(true);
    }

    public void CorrectAnswer()
    {
        questions.SetActive(false);
        currentMiniGame.EndMiniGame();
    }

    public void WrongAnswer()
    {
        questions.SetActive(false);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    IEnumerator QuestionDelay()
    {
        yield return new WaitForSeconds(3);
        questions.SetActive(true);
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

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    private int index = 0;
    public void Lv1()
    {
        index++;


        if (index == 5)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }

    public List<GameObject> leve1Questions = new List<GameObject>();
    private int ind;
    public void OpenDoor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        leve1Questions[ind].gameObject.SetActive(true);
        ind++;
    }

    public void Level1CorrectAnswer(Animator anim)
    {
        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        anim.SetTrigger("Trigger");

        foreach (GameObject go in leve1Questions)
        {
            go.SetActive(false);
        }
    }

    public void WrongAnswerLevel1()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    public List<GameObject> leve2Questions = new List<GameObject>();
    private int ind2;
    public void OpenDoorL2()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        leve2Questions[ind2].gameObject.SetActive(true);
        ind2++;
    }

    public void Level2CorrectAnswer(Animator anim)
    {
        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        anim.SetTrigger("Trigger");

        foreach (GameObject go in leve2Questions)
        {
            go.SetActive(false);
        }
    }

    public void WrongAnswerLevel2()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }
}
