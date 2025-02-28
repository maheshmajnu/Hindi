using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Acidsbases_and_salts_class_10 : MonoBehaviour
{

    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypoint.player = InventoryManager.Instance.player.transform;
        waypointCanvas.SetActive(true);
        waypoint.target = target;
    }

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
        //StartCoroutine(DelayLv5MiniGameStart());
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        lv5.SetActive(true);
        

    }
    IEnumerator DelayLv5MiniGameStart()
    {
        yield return new WaitForSeconds(1);
        lv5MiniGame.Output();
    }

    public TargetController lv5MiniGame;
    public void Savepoint1()
    {
        SaveProgress(1, 0, 4);
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
    }

    //Titles

    public GameObject title1;
    public GameObject title2;
    public GameObject title3;
    public GameObject title4;
    public GameObject title5;
    public GameObject title6;
    public GameObject title7;
    public GameObject title8;
    public GameObject title9;
    public GameObject title10;
    public GameObject title11;
    public GameObject title12;
    public GameObject title13;
    public GameObject title14;
    public GameObject title15;
    public GameObject title16;
    public GameObject title17;
    public GameObject title18;
    public GameObject title19;
    public GameObject title20;
    public GameObject title21;
    public GameObject title22;
    public GameObject title23;
    public GameObject title24;
    public GameObject title25;


    public GameObject down1;
    public GameObject down2;
    public GameObject down3;
    public GameObject down4;
    public GameObject down5;
    public GameObject down6;
    public GameObject down7;
    public GameObject down8;
    public GameObject down9;
   










    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject Acids_and_Bases_React_with_each_other;
    public GameObject Base_in_a_Water_Solution;
    public GameObject Carbonates_and_Metal_Hydrogencarbonates;
    public GameObject curd1;
    public GameObject dilution;
    public GameObject Metallic_Oxides_with_Acids;
    public GameObject Methyl_orange;
    public GameObject oifactory;
    public GameObject phenolphthalein;
    public GameObject react_metals;
    public GameObject red_and_blue_litmus;
    public GameObject Red_cabbage;
    public GameObject turmeric_powder;
    public GameObject vanilla;
    public GameObject water_conducts_electricity;
    public GameObject lemon;
    public GameObject vinegar;
    public GameObject hair;
    public GameObject toothpast;
    public GameObject dev;
    public GameObject red;
    public GameObject change_litmus_paper;
    public GameObject acid_text;
    public GameObject acid_formula;
    public GameObject base_text;
    public GameObject base_formula;
    public GameObject metalcarb_acid;
    public GameObject metalcarb_acid_f;
    public GameObject phe;
    public GameObject hcl;
    public GameObject naoh;
    public GameObject acid_base_text;
    public GameObject acid_base_f;
    public GameObject metalox_f;
    public GameObject metalox_text;
    public GameObject phi;
    public GameObject audio_text;
    public GameObject acidic;
    public GameObject basic;
    public GameObject strong_acid;
    public GameObject weak_acid;
    public GameObject strong_base;
    public GameObject weak_base;
    public GameObject acidity_text;
    public GameObject brush_boy;
    public GameObject recrys_formula;
    public GameObject bleaching_formula;
    public GameObject k_text;
    public GameObject plaster_formula;
    public GameObject ab_text;
    public GameObject hibiscus;
    public GameObject cloth;
    public GameObject h2gas;
    public GameObject cl2gas;
    public GameObject naoh2;












    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject v;

    public GameObject litmus;

    public GameObject mythl;
    public GameObject pheno;

    public GameObject r_litmus;

    public GameObject r_mythl;
    public GameObject r_pheno;
    public GameObject refrig;

    public GameObject paper;

    public GameObject re;
    public GameObject base_metal;
    public GameObject metalox;
    public GameObject ab;
    public GameObject rightaw;
    public GameObject rub;
   






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










    void _title1_MethodON()
    {
        title1.SetActive(true);
    }

    void _title1_MethodOFF()
    {
        title1.SetActive(false);
    }

    void _title2_MethodON()
    {
        title2.SetActive(true);
    }

    void _title2_MethodOFF()
    {
        title2.SetActive(false);
    }

    void _title3_MethodON()
    {
        title3.SetActive(true);
    }

    void _title3_MethodOFF()
    {
        title3.SetActive(false);
    }

    void _title4_MethodON()
    {
        title4.SetActive(true);
    }

    void _title4_MethodOFF()
    {
        title4.SetActive(false);
    }


    void _title5_MethodON()
    {
        title5.SetActive(true);
    }

    void _title5_MethodOFF()
    {
        title5.SetActive(false);
    }

    void _title6_MethodON()
    {
        title6.SetActive(true);
    }

    void _title6_MethodOFF()
    {
        title6.SetActive(false);
    }
    void _title7_MethodON()
    {
        title7.SetActive(true);
    }

    void _title7_MethodOFF()
    {
        title7.SetActive(false);
    }

    void _title8_MethodON()
    {
        title8.SetActive(true);
    }

    void _title8_MethodOFF()
    {
        title8.SetActive(false);
    }

    void _title9_MethodON()
    {
        title9.SetActive(true);
    }

    void _title9_MethodOFF()
    {
        title9.SetActive(false);
    }

    void _title10_MethodON()
    {
        title10.SetActive(true);
    }

    void _title10_MethodOFF()
    {
        title10.SetActive(false);
    }

    void _title11_MethodON()
    {
        title11.SetActive(true);
    }

    void _title11_MethodOFF()
    {
        title11.SetActive(false);
    }


    void _title12_MethodON()
    {
        title12.SetActive(true);
    }

    void _title12_MethodOFF()
    {
        title12.SetActive(false);
    }

    void _title13_MethodON()
    {
        title13.SetActive(true);
    }

    void _title13_MethodOFF()
    {
        title13.SetActive(false);
    }
    void _title14_MethodON()
    {
        title14.SetActive(true);
    }

    void _title14_MethodOFF()
    {
        title14.SetActive(false);
    }
    void _title15_MethodON()
    {
        title15.SetActive(true);
    }

    void _title15_MethodOFF()
    {
        title15.SetActive(false);
    }

    void _title16_MethodON()
    {
        title16.SetActive(true);
    }

    void _title16_MethodOFF()
    {
        title16.SetActive(false);
    }

    void _title17_MethodON()
    {
        title17.SetActive(true);
    }

    void _title17_MethodOFF()
    {
        title17.SetActive(false);
    }

    void _title18_MethodON()
    {
        title18.SetActive(true);
    }

    void _title18_MethodOFF()
    {
        title18.SetActive(false);
    }


    void _title19_MethodON()
    {
        title19.SetActive(true);
    }

    void _title19_MethodOFF()
    {
        title19.SetActive(false);
    }

    void _title20_MethodON()
    {
        title20.SetActive(true);
    }

    void _title20_MethodOFF()
    {
        title20.SetActive(false);
    }
    void _title21_MethodON()
    {
        title21.SetActive(true);
    }

    void _title21_MethodOFF()
    {
        title21.SetActive(false);
    }
    void _title22_MethodON()
    {
        title22.SetActive(true);
    }

    void _title22_MethodOFF()
    {
        title22.SetActive(false);
    }

    void _title23_MethodON()
    {
        title23.SetActive(true);
    }

    void _title23_MethodOFF()
    {
        title23.SetActive(false);
    }
    void _title24_MethodON()
    {
        title24.SetActive(true);
    }

    void _title24_MethodOFF()
    {
        title24.SetActive(false);
    }

    void _title25_MethodON()
    {
        title25.SetActive(true);
    }

    void _title25_MethodOFF()
    {
        title25.SetActive(false);
    }




    void _down1_MethodON()
    {
        down1.SetActive(true);
    }

    void _down1_MethodOFF()
    {
        down1.SetActive(false);
    }

    void _down2_MethodON()
    {
        down2.SetActive(true);
    }

    void _down2_MethodOFF()
    {
        down2.SetActive(false);
    }

    void _down3_MethodON()
    {
        down3.SetActive(true);
    }

    void _down3_MethodOFF()
    {
        down3.SetActive(false);
    }


    void _down4_MethodON()
    {
        down4.SetActive(true);
    }

    void _down4_MethodOFF()
    {
        down4.SetActive(false);
    }


    void _down5_MethodON()
    {
        down5.SetActive(true);
    }

    void _down5_MethodOFF()
    {
        down5.SetActive(false);
    }

    void _down6_MethodON()
    {
        down6.SetActive(true);
    }

    void _down6_MethodOFF()
    {
        down6.SetActive(false);
    }

    void _down7_MethodON()
    {
        down7.SetActive(true);
    }

    void _down7_MethodOFF()
    {
        down7.SetActive(false);
    }

    void _down8_MethodON()
    {
        down8.SetActive(true);
    }

    void _down8_MethodOFF()
    {
        down8.SetActive(false);
    }
    void _down9_MethodON()
    {
        down9.SetActive(true);
    }

    void _down9_MethodOFF()
    {
        down9.SetActive(false);
    }










    // Line on/off

    //
    void Acids_and_Bases_React_with_each_other_MethodOFF()
    {
        Acids_and_Bases_React_with_each_other.SetActive(false);
    }
    //
    void Acids_and_Bases_React_with_each_other_MethodON()
    {
        Acids_and_Bases_React_with_each_other.SetActive(true);
    }
    //
    void Base_in_a_Water_Solution_MethodOFF()
    {
        Base_in_a_Water_Solution.SetActive(false);
    }
    //
    void Base_in_a_Water_Solution_MethodON()
    {
        Base_in_a_Water_Solution.SetActive(true);
    }
    //
    void Carbonates_and_Metal_Hydrogencarbonates_MethodOFF()
    {
        Carbonates_and_Metal_Hydrogencarbonates.SetActive(false);
    }
    //
    void Carbonates_and_Metal_Hydrogencarbonates_MethodON()
    {
        Carbonates_and_Metal_Hydrogencarbonates.SetActive(true);
    }
    //
    void curd1_MethodOFF()
    {
        curd1.SetActive(false);
    }
    //
    void curd1_MethodON()
    {
        curd1.SetActive(true);
    }
    //
    void dilution_MethodOFF()
    {
        dilution.SetActive(false);
    }
    //
    void dilution_MethodON()
    {
        dilution.SetActive(true);
    }
    //
    void Metallic_Oxides_with_Acids_MethodOFF()
    {
        Metallic_Oxides_with_Acids.SetActive(false);
    }
    //
    void Metallic_Oxides_with_Acids_MethodON()
    {
        Metallic_Oxides_with_Acids.SetActive(true);
    }
    //
    void Methyl_orange_MethodOFF()
    {
        Methyl_orange.SetActive(false);
    }
    //
    void Methyl_orange_MethodON()
    {
        Methyl_orange.SetActive(true);
    }
    //
    void oifactory_MethodOFF()
    {
        oifactory.SetActive(false);
    }
    //
    void oifactory_MethodON()
    {
        oifactory.SetActive(true);
    }
    //
    void phenolphthalein_MethodOFF()
    {
        phenolphthalein.SetActive(false);
    }
    //
    void phenolphthalein_MethodON()
    {
        phenolphthalein.SetActive(true);
    }
    //
    void react_metals_MethodOFF()
    {
        react_metals.SetActive(false);
    }
    //
    void react_metals_MethodON()
    {
        react_metals.SetActive(true);
    }
    //
    void red_and_blue_litmus_MethodOFF()
    {
        red_and_blue_litmus.SetActive(false);
    }
    //
    void red_and_blue_litmus_MethodON()
    {
        red_and_blue_litmus.SetActive(true);
    }
    //
    void Red_cabbage_MethodOFF()
    {
        Red_cabbage.SetActive(false);
    }
    //
    void Red_cabbage_MethodON()
    {
        Red_cabbage.SetActive(true);
    }
    //
    void turmeric_powder_MethodOFF()
    {
        turmeric_powder.SetActive(false);
    }
    //
    void turmeric_powder_MethodON()
    {
        turmeric_powder.SetActive(true);
    }
    //
    void vanilla_MethodOFF()
    {
        vanilla.SetActive(false);
    }
    //
    void vanilla_MethodON()
    {
        vanilla.SetActive(true);
    }
    //
    void water_conducts_electricity_MethodOFF()
    {
        water_conducts_electricity.SetActive(false);
    }
    //
    void water_conducts_electricity_MethodON()
    {
        water_conducts_electricity.SetActive(true);
    }
    //
    void lemon_MethodOFF()
    {
        lemon.SetActive(false);
    }
    //
    void lemon_MethodON()
    {
        lemon.SetActive(true);
    }
    //
    void vinegar_MethodOFF()
    {
        vinegar.SetActive(false);
    }
    //
    void vinegar_MethodON()
    {
        vinegar.SetActive(true);
    }
    //
    void hair_MethodOFF()
    {
        hair.SetActive(false);
    }
    //
    void hair_MethodON()
    {
        hair.SetActive(true);
    }
    //
    void toothpast_MethodOFF()
    {
        toothpast.SetActive(false);
    }
    //
    void toothpast_MethodON()
    {
        toothpast.SetActive(true);
    }
    //
    void dev_MethodOFF()
    {
        dev.SetActive(false);
    }
    //
    void dev_MethodON()
    {
        dev.SetActive(true);
    }
    //
    void red_MethodOFF()
    {
        red.SetActive(false);
    }
    //
    void red_MethodON()
    {
        red.SetActive(true);
    }
    //

    void change_litmus_paper_MethodOFF()
    {
        change_litmus_paper.SetActive(false);
    }
    //
    void change_litmus_paper_MethodON()
    {
        change_litmus_paper.SetActive(true);
    }
    //


    void acid_text_MethodOFF()
    {
        acid_text.SetActive(false);
    }
    //
    void acid_text_MethodON()
    {
        acid_text.SetActive(true);
    }
    //



    void acid_formula_MethodOFF()
    {
        acid_formula.SetActive(false);
    }
    //
    void acid_formula_MethodON()
    {
        acid_formula.SetActive(true);
    }
    //


    void base_text_MethodOFF()
    {
        base_text.SetActive(false);
    }
    //
    void base_text_MethodON()
    {
        base_text.SetActive(true);
    }
    //

    void base_formula_MethodOFF()
    {
        base_formula.SetActive(false);
    }
    //
    void base_formula_MethodON()
    {
        base_formula.SetActive(true);
    }
    //

    void metalcarb_acid_MethodOFF()
    {
        metalcarb_acid.SetActive(false);
    }
    //
    void metalcarb_acid_MethodON()
    {
        metalcarb_acid.SetActive(true);
    }
    //

    void metalcarb_acid_f_MethodOFF()
    {
        metalcarb_acid_f.SetActive(false);
    }
    //
    void metalcarb_acid_f_MethodON()
    {
        metalcarb_acid_f.SetActive(true);
    }
    //

    void phe_MethodOFF()
    {
        phe.SetActive(false);
    }
    //
    void phe_MethodON()
    {
        phe.SetActive(true);
    }
    //



    void hcl_MethodOFF()
    {
        hcl.SetActive(false);
    }
    //
    void hcl_MethodON()
    {
        hcl.SetActive(true);
    }
    //

    void naoh_MethodOFF()
    {
        naoh.SetActive(false);
    }
    //
    void naoh_MethodON()
    {
        naoh.SetActive(true);
    }
    //


    void acid_base_text_MethodOFF()
    {
        acid_base_text.SetActive(false);
    }
    //
    void acid_base_text_MethodON()
    {
        acid_base_text.SetActive(true);
    }
    //


    void acid_base_f_MethodOFF()
    {
        acid_base_f.SetActive(false);
    }
    //
    void acid_base_f_MethodON()
    {
        acid_base_f.SetActive(true);
    }
    //

    void metalox_f_MethodOFF()
    {
        metalox_f.SetActive(false);
    }
    //
    void metalox_f_MethodON()
    {
        metalox_f.SetActive(true);
    }
    //


    void metalox_text_MethodOFF()
    {
        metalox_text.SetActive(false);
    }
    //
    void metalox_text_MethodON()
    {
        metalox_text.SetActive(true);
    }
    //


    void phi_MethodOFF()
    {
        phi.SetActive(false);
    }
    //
    void phi_MethodON()
    {
        phi.SetActive(true);
    }
    //

    void v_MethodOFF()
    {
        v.SetActive(false);
    }
    //
    void v_MethodON()
    {
        v.SetActive(true);
    }
    //


    void audio_text_MethodOFF()
    {
        audio_text.SetActive(false);
    }
    //
    void audio_text_MethodON()
    {
        audio_text.SetActive(true);
    }
    //


    void acidic_MethodOFF()
    {
        acidic.SetActive(false);
    }
    //
    void acidic_MethodON()
    {
        acidic.SetActive(true);
    }
    //
    void basic_MethodOFF()
    {
        basic.SetActive(false);
    }
    //
    void basic_MethodON()
    {
        basic.SetActive(true);
    }
    //
    void strong_acid_MethodOFF()
    {
        strong_acid.SetActive(false);
    }
    //
    void strong_acid_MethodON()
    {
        strong_acid.SetActive(true);
    }
    //
    void weak_acid_MethodOFF()
    {
        weak_acid.SetActive(false);
    }
    //
    void weak_acid_MethodON()
    {
        weak_acid.SetActive(true);
    }
    //
    void strong_base_MethodOFF()
    {
        strong_base.SetActive(false);
    }
    //
    void strong_base_MethodON()
    {
        strong_base.SetActive(true);
    }
    //
    void weak_base_MethodOFF()
    {
        weak_base.SetActive(false);
    }
    //
    void weak_base_MethodON()
    {
        weak_base.SetActive(true);
    }
    //
    void acidity_text_MethodOFF()
    {
        acidity_text.SetActive(false);
    }
    //
    void acidity_text_MethodON()
    {
        acidity_text.SetActive(true);
    }
    //
    void brush_boy_MethodOFF()
    {
        brush_boy.SetActive(false);
    }
    //
    void brush_boy_MethodON()
    {
        brush_boy.SetActive(true);
    }
    //
    void recrys_formula_MethodOFF()
    {
        recrys_formula.SetActive(false);
    }
    //
    void recrys_formula_MethodON()
    {
        recrys_formula.SetActive(true);
    }
    //
    void bleaching_formula_MethodOFF()
    {
        bleaching_formula.SetActive(false);
    }
    //
    void bleaching_formula_MethodON()
    {
        bleaching_formula.SetActive(true);
    }
    //
    void k_text_MethodOFF()
    {
        k_text.SetActive(false);
    }
    //
    void k_text_MethodON()
    {
        k_text.SetActive(true);
    }
    //
    void plaster_formula_MethodOFF()
    {
        plaster_formula.SetActive(false);
    }
    //
    void plaster_formula_MethodON()
    {
        plaster_formula.SetActive(true);
    }
    //

    void ab_text_MethodOFF()
    {
        ab_text.SetActive(false);
    }
    //
    void ab_text_MethodON()
    {
        ab_text.SetActive(true);
    }
    //
    void hibiscus_MethodOFF()
    {
        hibiscus.SetActive(false);
    }
    //
    void hibiscus_MethodON()
    {
        hibiscus.SetActive(true);
    }
    //
    void cloth_MethodOFF()
    {
        cloth.SetActive(false);
    }
    //
    void cloth_MethodON()
    {
        cloth.SetActive(true);
    }
    //
    void h2gas_MethodOFF()
    {
        h2gas.SetActive(false);
    }
    //
    void h2gas_MethodON()
    {
        h2gas.SetActive(true);
    }
    //
    void cl2gas_MethodOFF()
    {
        cl2gas.SetActive(false);
    }
    //
    void cl2gas_MethodON()
    {
        cl2gas.SetActive(true);
    }
    //
    void naoh2_MethodOFF()
    {
        naoh2.SetActive(false);
    }
    //
    void naoh2_MethodON()
    {
        naoh2.SetActive(true);
    }
    //



    // Audio

    void _voice1_audioMethod()
    {
        myAudio.clip = voice1;
        myAudio.Play();
    }

    //
    void _voice2_audioMethod()
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
        myAudio.clip = voice4;
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
        myAudio.clip = voice56;
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
        myAudio.clip = voice70;
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


    // Animations

    void v_aimAnimmethod()
    {

        anim = v.GetComponent<Animator>();
        anim.Play("v_animation");
    }
    void litmus_aimAnimmethod()
    {

        anim = litmus.GetComponent<Animator>();
        anim.Play("litmus_animation");
    }

    void mythl_aimAnimmethod()
    {

        anim = mythl.GetComponent<Animator>();
        anim.Play("mythl_change_animation");
    }

    void pheno_aimAnimmethod()
    {

        anim = pheno.GetComponent<Animator>();
        anim.Play("pheno_animation");
    }

    void r_litmus_aimAnimmethod()
    {

        anim = r_litmus.GetComponent<Animator>();
        anim.Play("r_litmus_animation");
    }

    void r_mythl_aimAnimmethod()
    {

        anim = r_mythl.GetComponent<Animator>();
        anim.Play("r_mythl_animation");
    }
    void r_pheno_aimAnimmethod()
    {

        anim = r_pheno.GetComponent<Animator>();
        anim.Play("r_pheno_animation");
    }

    void refrig_aimAnimmethod()
    {

        anim = refrig.GetComponent<Animator>();
        anim.Play("refrig_ani");
    }


    void paper_aimAnimmethod()
    {

        anim = paper.GetComponent<Animator>();
        anim.Play("paper_ani");
    }

    void react_metals_aimAnimmethod()
    {

        anim = react_metals.GetComponent<Animator>();
        anim.Play("react_metals_ani");
    }

    void base_metal_aimAnimmethod()
    {

        anim = base_metal.GetComponent<Animator>();
        anim.Play("base_metal_ani");
    }

    void metalox_aimAnimmethod()
    {

        anim = metalox.GetComponent<Animator>();
        anim.Play("metalox_acid_ani");
    }


    void ph_aimAnimmethod()
    {

        anim = phi.GetComponent<Animator>();
        anim.Play("phenolphthalein_animation");
    }

    void ab_aimAnimmethod()
    {

        anim = ab.GetComponent<Animator>();
        anim.Play("ab_animation");
    }


    void rightaw_aimAnimmethod()
    {

        anim = rightaw.GetComponent<Animator>();
        anim.Play("right_atow_ani");
    }

    void rub_aimAnimmethod()
    {

        anim = rub.GetComponent<Animator>();
        anim.Play("rubbing_ani");
    }


    public TextMeshProUGUI phValueNumberText;
    private int currentInd = 0;
    private bool phMiniGameStarted = false;
    private bool saltsMiniGameStarted = false;
    private bool lastMiniGameStarted = false;
    private bool canPourNextLiq = false;
    public TargetController phMiniGame;
    public TargetController saltsMiniGame;
    public TargetController lastMiniGame;
    public List<string> phValues;
    public List<string> salts;
    public List<string> lastbeakers;
    public List<string> saltsQuestions;
    public int beakerCount = 0;
    public GameObject acbQuestions;
    public GameObject inQuestions;
    public List<Button> absQuestionBtn = new List<Button>();
    public List<Button> inQuestionBtn = new List<Button>();
    private TargetController currentMiniGame;
    public Camera cam;
    public LayerMask layerMask;

    void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        } 


        if (Input.GetMouseButtonDown(0) && !phMiniGameStarted && !saltsMiniGameStarted && !lastMiniGameStarted)
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            //acid base and salt
            if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layerMask))
            {
                if (raycastHit2.collider != null)
                {
                    Debug.Log("Exp");
                    Animator anim = raycastHit2.collider.transform.parent.GetComponent<Animator>();
                    ExpirementAnim(anim);
                }
            }
        }

        if (phMiniGameStarted)
        {
            phValueNumberText.text = phValues[currentInd];

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                //acid base and salt
                if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layerMask))
                {
                    if (raycastHit2.collider != null)
                    {
                        if (raycastHit2.collider.gameObject.name == phValues[currentInd])
                        {
                            currentInd++;

                            if (currentInd == 5)
                            {
                                currentInd = 0;
                                phMiniGame.EndMiniGame();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                                phMiniGameStarted = false;



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

        if (saltsMiniGameStarted)
        {
            phValueNumberText.text = saltsQuestions[currentInd];

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                //acid base and salt
                if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layerMask))
                {
                    if (raycastHit2.collider != null)
                    {
                        if (raycastHit2.collider.gameObject.name == salts[currentInd])
                        {
                            currentInd++;
                            if (currentInd == 6)
                            {
                                currentInd = 0;
                                saltsMiniGame.EndMiniGame();
                                saltsMiniGameStarted = false;
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

        if (lastMiniGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                //acid base and salt
                if (Physics.Raycast(ray, out RaycastHit raycastHit2, 999f, layerMask))
                {
                    if (raycastHit2.collider != null)
                    {
                        if (raycastHit2.collider.gameObject.name == lastbeakers[currentInd])
                        {
                            Animator anim = raycastHit2.collider.gameObject.GetComponent<Animator>();
                            anim.SetTrigger("Trigger");
                            currentInd++;
                            if (currentInd == 3)
                            {
                                currentInd = 0;
                                lastMiniGame.EndMiniGame();
                                lastMiniGameStarted = false;
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

    IEnumerator QuestionsPopUp()
    {
        yield return new WaitForSeconds(2);
        acbQuestions.SetActive(true);
    }

    public void PlayParticleEffect(ParticleSystem particleSystem)
    {
        particleSystem.Play();
    }

    public void PlayAnimTrigg(Animator anim)
    {
        anim.SetTrigger("Trigger"); // Replace "TriggerName" with the actual name of your trigger
    }

    public void Expiriment(Animator anim)
    {
        anim.SetTrigger("Trigger");

        StartCoroutine(QuestionsPopUp());


        TargetController controller = anim.gameObject.GetComponent<TargetController>();

        currentMiniGame = controller;

        int correctIndex = int.Parse(controller.checkName);

        for (int i = 0; i < absQuestionBtn.Count; i++)
        {
            absQuestionBtn[i].onClick.RemoveAllListeners();
            if (i == correctIndex)
            {
                absQuestionBtn[i].onClick.AddListener(CorrectAnswer);
            }
            else
            {
                absQuestionBtn[i].onClick.AddListener(WrongAnswer);
            }
        }
        for (int i = 0; i < inQuestionBtn.Count; i++)
        {
            inQuestionBtn[i].onClick.RemoveAllListeners();
            if (i == controller.rotationCount)
            {
                inQuestionBtn[i].onClick.AddListener(CorrectInAnswer);
            }
            else
            {
                inQuestionBtn[i].onClick.AddListener(WrongAnswer);
            }
        }

    }

    public void CorrectAnswer()
    {
        acbQuestions.SetActive(false);
        inQuestions.SetActive(true);
    }

    public void CorrectInAnswer()
    {
        inQuestions.SetActive(false);

        beakerCount++;
        currentMiniGame.EndMiniGame();

        if (beakerCount == 10)
        {
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void PHMiniGameStart()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        phMiniGameStarted = true;
    }
    public void SaltsMiniGameStart()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        saltsMiniGameStarted = true;
    }

    public void LastMiniGameStart()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        lastMiniGameStarted = true;
    }
    public GameObject dail1;
    public List<TargetController> dailers = new List<TargetController>();
    public void CheckForDailer(TargetController rotatable)
    {
        rotatable.rotationCount--;

        if (rotatable.rotationCount == 0)
        {
            rotatable.hasRotated = true;
            rotatable.gameObject.tag = "Untagged";
        }

        foreach (var controller in dailers)
        {
            if (!controller.hasRotated)
            {
                return;
            }
        }
        dail1.SetActive(true);
        StepComplete();
        
    }

    public GameObject dail2;
    public List<TargetController> dailers1 = new List<TargetController>();
    public void CheckForDailer1(TargetController rotatable)
    {
        rotatable.rotationCount--;

        if (rotatable.rotationCount == 0)
        {
            rotatable.hasRotated = true;
            rotatable.gameObject.tag = "Untagged";
        }

        foreach (var controller in dailers1)
        {
            if (!controller.hasRotated)
            {
                return;
            }
        }
        dail2.SetActive(true);
        StepComplete();

    }

    public GameObject dail3;
    public List<TargetController> dailers2 = new List<TargetController>();
    public void CheckForDailer2(TargetController rotatable)
    {
        rotatable.rotationCount--;

        if (rotatable.rotationCount == 0)
        {
            rotatable.hasRotated = true;
            rotatable.gameObject.tag = "Untagged";
        }

        foreach (var controller in dailers2)
        {
            if (!controller.hasRotated)
            {
                return;
            }
        }
        dail3.SetActive(true);
        StepComplete();

    }

    public GameObject dail4;
    public List<TargetController> dailers3 = new List<TargetController>();
    public void CheckForDailer3(TargetController rotatable)
    {
        rotatable.rotationCount--;

        if (rotatable.rotationCount == 0)
        {
            rotatable.hasRotated = true;
            rotatable.gameObject.tag = "Untagged";
        }

        foreach (var controller in dailers3)
        {
            if (!controller.hasRotated)
            {
                return;
            }
        }
        dail4.SetActive(true);
        StepComplete();

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
    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void WrongAnswer()
    {
        Debug.Log("Mission Failed");
        MissionFailed();
        
    }

    private int index = 0;
    public GameObject bulbTourch;
    public void level8()
    {
        index++;
        if(index == 3)
        {
            bulbTourch.SetActive(true);
            StepComplete();
        }

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

    public void ExpirementAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
        StartCoroutine(MissionStart(anim));
    }

    IEnumerator MissionStart(Animator anim)
    {
        TargetController targetController = anim.transform.GetComponentInChildren<TargetController>();
        yield return new WaitForSeconds(3);
        targetController.subObject.gameObject.SetActive(true);
    }

    IEnumerator DelayMiniGameEnd(TargetController miniGame)
    {
        yield return new WaitForSeconds(3);
        CorrectAnswer(miniGame);
    }

    public void CorrectAnswer(TargetController miniGame)
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        miniGame.subObject.gameObject.SetActive(false);
        miniGame.EndMiniGame();
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

}
