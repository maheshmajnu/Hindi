using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sfx_acids_bases_salts : MonoBehaviour
{
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    //scripts
    [Header("Script GO")]
    public GameObject liquidInLemonWater;
    public GameObject liquidInWater;
    public GameObject liquidInSoapWater;
    public GameObject boilingpinkwater;
    public GameObject liquidInNaOH;
    public GameObject liquidInHCL;
    public GameObject liquidInstrainer;




    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject sugar_bowl;
    public GameObject lemon;
    public GameObject lemonslices;
    public GameObject bittergourd;
    public GameObject exp_juice_glass;
    public GameObject tomato;
    public GameObject grapes;
    public GameObject apple;
    public GameObject strawberry;
    public GameObject banana;
    public GameObject bee_exp;
    public GameObject orange_with_fruits;
    public GameObject vinegar;
    public GameObject orange;
    public GameObject pizza;
    public GameObject curd;
    public GameObject detergent;
    public GameObject sanitizer;
    public GameObject milk_of_magnesia;
    public GameObject baking_soda_bowl;
    public GameObject cake;
    public GameObject bread_slices;
    public GameObject stovestanddisp;
    public GameObject beaker1disp;
    public GameObject beaker2disp;
    public GameObject beaker3disp;
    public GameObject glassbottledisp;
    public GameObject juiceglassdisp;
    public GameObject smallglass1disp;
    public GameObject smallglass2disp;
    public GameObject smallglass3disp;
    public GameObject petalsdisp;
    public GameObject bunsenburner;
    public GameObject stovestand_boiling;
    public GameObject beaker_boiling;
    public GameObject petalsboiling;
    public GameObject glassbottle_straining;
    public GameObject tea_strainer;
    public GameObject glassbottle_pouring_exp;
    public GameObject glass_litmus;
    public GameObject straining_exp;
    public GameObject boilingwater_exp;
    public GameObject bakingsoda_exp;
    public GameObject vinegar_exp;
    public GameObject bee_sting;
    public GameObject sting_redbump;
    public GameObject brinjal;
    public GameObject chalkbox;
    public GameObject bakinglitmus;
    public GameObject vinegarlitmus;
    public GameObject glass_pouring_eff;
    public GameObject vinegar_pouring_eff;
    public GameObject sanitization;
    public GameObject acid_effect;
    public GameObject vinegar_effect;
    public GameObject introductionp;
    public GameObject chemist_robert_boylep;
    public GameObject abs_downp;
    public GameObject acidsp;
    public GameObject sour_tastep;
    public GameObject acid_originp;
    public GameObject sting_of_beep;
    public GameObject citric_acidp;
    public GameObject dilute_acetic_acidp;
    public GameObject absorbic_acidp;
    public GameObject lactic_acidp;
    public GameObject basesp;
    public GameObject alkali_originp;
    public GameObject milk_of_magnesiap;
    public GameObject manganese_hydroxidep;
    public GameObject bakingsodap;
    public GameObject sodium_hydrogen_carbonatep;
    public GameObject chalkp;
    public GameObject calciumcarbonatep;
    public GameObject indicatorsp;
    public GameObject indicators_demop;
    public GameObject lemonjuice_acidicp;
    public GameObject water_neutralp;
    public GameObject soap_solution_basicp;
    public GameObject neutral_solutionp;
    public GameObject litmusp;
    public GameObject blue_alkalinep;
    public GameObject red_vinegar_acidp;
    public GameObject saltsp;
    public GameObject beestingp;
    public GameObject acid_base_combop;
    public GameObject universal_indicatorp;
    public GameObject pink_colorp;
    public GameObject purple_colorp;
    public GameObject green_solutionp;
    public GameObject neuralisation_reactionp;
    public GameObject antacidp;
    public GameObject organic_compoundsp;
    public GameObject slaked_limep;
    public GameObject color_c;
    public GameObject powder;





    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("Camera animation", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }
    }

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






















    // Exp - Animations

    private Animator anim;
    [Header("Explanation anims")]
    public GameObject chalkpiece;
    public GameObject petals_boiling;
    public GameObject straining_beaker;
    public GameObject glass_pouring_bottle;
    public GameObject plate;
    public GameObject stirrer_bakingsoda;
    public GameObject funnel;
    public GameObject stirrer_vinegar;
    public GameObject hand_cross;
    public GameObject honeybee_sting;
    public GameObject stirrer_for_hcl;
    public GameObject stirrer_for_naoh;
    public GameObject upper_layer;
    public GameObject food;



    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip introduction;
    public AudioClip chemist_audio;
    public AudioClip acid_intro;
    public AudioClip bee;
    public AudioClip citrus_fruit;
    public AudioClip vinegar_v;
    public AudioClip absorbic_acid;
    public AudioClip curd_v;
    public AudioClip bases_intro;
    public AudioClip milk_of_magnesia_v;
    public AudioClip bakingsoda;
    public AudioClip chalk;
    public AudioClip indicator_intro;
    public AudioClip indicator_parts_exp;
    public AudioClip boiling;
    public AudioClip petals;
    public AudioClip strainer;
    public AudioClip three_beakers;
    public AudioClip lemon_juice_water;
    public AudioClip indicator_explanation;
    public AudioClip water;
    public AudioClip litmus;
    public AudioClip litmus_redtoblue;
    public AudioClip litmus_bluetored;
    public AudioClip bee_sting_v;
    public AudioClip salt_intro;
    public AudioClip neuralization_1;
    public AudioClip neuralization_2;
    public AudioClip neuralization_3;
    public AudioClip neuralization_4;
    public AudioClip neuralization_5;
    public AudioClip neuralization_6;
    public AudioClip neuralization_7;
    public AudioClip neuralization_8;
    public AudioClip soap_sanitizer;
    public AudioClip stomach;
    public AudioClip fertilizer;
    public AudioClip factory;
    public AudioClip fruitsA;
    public AudioClip fruitsB;


    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
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
    }


    // Exp objects


    void _Color_CMethodON()
    {
        color_c.SetActive(true);
    }

    void _Color_CMethodOFF()
    {
        color_c.SetActive(false);
    }

    //


    void _powder_CMethodON()
    {
        powder.SetActive(true);
    }

    void _powder_CMethodOFF()
    {
        powder.SetActive(false);
    }

    //

    void _IntroductionPMethodON()
    {
        introductionp.SetActive(true);
    }

    void _IntroductionPMethodOFF()
    {
        introductionp.SetActive(false);
    }

    //
    void _Chemist_robert_boylePMethodON()
    {
        chemist_robert_boylep.SetActive(true);
    }

    void _Chemist_robert_boylePMethodOFF()
    {
        chemist_robert_boylep.SetActive(false);
    }

    //
    void _Abs_downPMethodON()
    {
        abs_downp.SetActive(true);
    }

    void _Abs_downPMethodOFF()
    {
        abs_downp.SetActive(false);
    }

    //
    void _AcidsPMethodON()
    {
        acidsp.SetActive(true);
    }

    void _AcidsPMethodOFF()
    {
        acidsp.SetActive(false);
    }

    //
    void _Sour_tastePMethodON()
    {
        sour_tastep.SetActive(true);
    }

    void _Sour_tastePMethodOFF()
    {
        sour_tastep.SetActive(false);
    }

    //
    void _Acid_originPMethodON()
    {
        acid_originp.SetActive(true);
    }

    void _Acid_originPMethodOFF()
    {
        acid_originp.SetActive(false);
    }

    //
    void _Sting_of_beePMethodON()
    {
        sting_of_beep.SetActive(true);
    }

    void _Sting_of_beePMethodOFF()
    {
        sting_of_beep.SetActive(false);
    }

    //
    void _Citric_acidPMethodON()
    {
        citric_acidp.SetActive(true);
    }

    void _Citric_acidPMethodOFF()
    {
        citric_acidp.SetActive(false);
    }

    //
    void _Dilute_acetic_acidPMethodON()
    {
        dilute_acetic_acidp.SetActive(true);
    }

    void _Dilute_acetic_acidPMethodOFF()
    {
        dilute_acetic_acidp.SetActive(false);
    }

    //
    void _Absorbic_acidPMethodON()
    {
        absorbic_acidp.SetActive(true);
    }

    void _Absorbic_acidPMethodOFF()
    {
        absorbic_acidp.SetActive(false);
    }

    //
    void _Lactic_acidPMethodON()
    {
        lactic_acidp.SetActive(true);
    }

    void _Lactic_acidPMethodOFF()
    {
        lactic_acidp.SetActive(false);
    }

    //
    void _BasesPMethodON()
    {
        basesp.SetActive(true);
    }

    void _BasesPMethodOFF()
    {
        basesp.SetActive(false);
    }

    //
    void _Alkali_originPMethodON()
    {
        alkali_originp.SetActive(true);
    }

    void _Alkali_originPMethodOFF()
    {
        alkali_originp.SetActive(false);
    }

    //
    void _Milk_of_magnesiaPMethodON()
    {
        milk_of_magnesiap.SetActive(true);
    }

    void _Milk_of_magnesiaPMethodOFF()
    {
        milk_of_magnesiap.SetActive(false);
    }

    //
    void _Manganese_hydroxidePMethodON()
    {
        manganese_hydroxidep.SetActive(true);
    }

    void _Manganese_hydroxidePMethodOFF()
    {
        manganese_hydroxidep.SetActive(false);
    }

    //
    void _BakingsodaPMethodON()
    {
        bakingsodap.SetActive(true);
    }

    void _BakingsodaPMethodOFF()
    {
        bakingsodap.SetActive(false);
    }

    //
    void _Sodium_hydrogen_carbonatePMethodON()
    {
        sodium_hydrogen_carbonatep.SetActive(true);
    }

    void _Sodium_hydrogen_carbonatePMethodOFF()
    {
        sodium_hydrogen_carbonatep.SetActive(false);
    }

    //
    void _ChalkPMethodON()
    {
        chalkp.SetActive(true);
    }

    void _ChalkPMethodOFF()
    {
        chalkp.SetActive(false);
    }

    //
    void _CalciumcarbonatePMethodON()
    {
        calciumcarbonatep.SetActive(true);
    }

    void _CalciumcarbonatePMethodOFF()
    {
        calciumcarbonatep.SetActive(false);
    }

    //
    void _IndicatorsPMethodON()
    {
        indicatorsp.SetActive(true);
    }

    void _IndicatorsPMethodOFF()
    {
        indicatorsp.SetActive(false);
    }

    //
    void _Indicators_demoPMethodON()
    {
        indicators_demop.SetActive(true);
    }

    void _Indicators_demoPMethodOFF()
    {
        indicators_demop.SetActive(false);
    }

    //
    void _Lemon_juice_acidPMethodON()
    {
        lemonjuice_acidicp.SetActive(true);
    }

    void _Lemon_juice_acidPMethodOFF()
    {
        lemonjuice_acidicp.SetActive(false);
    }

    //
    void _Water_neutralPMethodON()
    {
        water_neutralp.SetActive(true);
    }

    void _Water_neutralPMethodOFF()
    {
        water_neutralp.SetActive(false);
    }

    //
    void _Soap_solution_basicPMethodON()
    {
        soap_solution_basicp.SetActive(true);
    }

    void _Soap_solution_basicPMethodOFF()
    {
        soap_solution_basicp.SetActive(false);
    }

    //
    void _Neutral_solutionPMethodON()
    {
        neutral_solutionp.SetActive(true);
    }

    void _Neutral_solutionPMethodOFF()
    {
        neutral_solutionp.SetActive(false);
    }

    //
    void _LitmusPMethodON()
    {
        litmusp.SetActive(true);
    }

    void _LitmusPMethodOFF()
    {
        litmusp.SetActive(false);
    }

    //
    void _Blue_alkalinePMethodON()
    {
        blue_alkalinep.SetActive(true);
    }

    void _Blue_alkalinePMethodOFF()
    {
        blue_alkalinep.SetActive(false);
    }

    //
    void _Red_vinegar_acidPMethodON()
    {
        red_vinegar_acidp.SetActive(true);
    }

    void _Red_vinegar_acidPMethodOFF()
    {
        red_vinegar_acidp.SetActive(false);
    }

    //
    void _SaltsPMethodON()
    {
        saltsp.SetActive(true);
    }

    void _SaltsPMethodOFF()
    {
        saltsp.SetActive(false);
    }

    //
    void _BeestingPMethodON()
    {
        beestingp.SetActive(true);
    }

    void _BeestingPMethodOFF()
    {
        beestingp.SetActive(false);
    }

    //
    void _Acid_base_comboPMethodON()
    {
        acid_base_combop.SetActive(true);
    }

    void _Acid_base_comboPMethodOFF()
    {
        acid_base_combop.SetActive(false);
    }

    //
    void _Universal_indicatorPMethodON()
    {
        universal_indicatorp.SetActive(true);
    }

    void _Universal_indicatorPMethodOFF()
    {
        universal_indicatorp.SetActive(false);
    }

    //
    void _Pink_colorPMethodON()
    {
        pink_colorp.SetActive(true);
    }

    void _Pink_colorPMethodOFF()
    {
        pink_colorp.SetActive(false);
    }

    //

    void _Purple_colorPMethodON()
    {
        purple_colorp.SetActive(true);
    }

    void _Purple_colorPMethodOFF()
    {
        purple_colorp.SetActive(false);
    }

    //

    void _Green_solutionPMethodON()
    {
        green_solutionp.SetActive(true);
    }

    void _Green_solutionPMethodOFF()
    {
        green_solutionp.SetActive(false);
    }

    //

    void _Neuralisation_reactionPMethodON()
    {
        neuralisation_reactionp.SetActive(true);
    }

    void _Neuralisation_reactionPMethodOFF()
    {
        neuralisation_reactionp.SetActive(false);
    }

    //

    void _AntacidPMethodON()
    {
        antacidp.SetActive(true);
    }

    void _AntacidPMethodOFF()
    {
        antacidp.SetActive(false);
    }

    //
    void _Organic_compoundsPMethodON()
    {
        organic_compoundsp.SetActive(true);
    }

    void _Organic_compoundsPMethodOFF()
    {
        organic_compoundsp.SetActive(false);
    }

    //

    void _Slaked_limePMethodON()
    {
        slaked_limep.SetActive(true);
    }

    void _Slaked_limePMethodOFF()
    {
        slaked_limep.SetActive(false);
    }

    //














    void _Vinegar_effMethodON()
    {
        vinegar_effect.SetActive(true);
    }

    void _Vinegar_effMethodOFF()
    {
        vinegar_effect.SetActive(false);
    }

    //

    void _SanitizationMethodON()
    {
        sanitization.SetActive(true);
    }

    void _SanitizationMethodOFF()
    {
        sanitization.SetActive(false);
    }

    //

    void _Acid_effectMethodON()
    {
        acid_effect.SetActive(true);
    }

    void _Acid_effectMethodOFF()
    {
        acid_effect.SetActive(false);
    }

    //


    void _Vinegar_pouring_effMethodON()
    {
        vinegar_pouring_eff.SetActive(true);
    }

    void _Vinegar_pouring_effMethodOFF()
    {
        vinegar_pouring_eff.SetActive(false);
    }

    //

    void _Pouring_exp_effMethodON()
    {
        glass_pouring_eff.SetActive(true);
    }

    void _Pouriing_exp_effMethodOFF()
    {
        glass_pouring_eff.SetActive(false);
    }

    //

    void _Baking_litmusMethodON()
    {
        bakinglitmus.SetActive(true);
    }

    void _Baking_litmusMethodOFF()
    {
        bakinglitmus.SetActive(false);
    }

    //

    void _Vinegar_litmusMethodON()
    {
        vinegarlitmus.SetActive(true);
    }

    void _Vinegar_litmusMethodOFF()
    {
        vinegarlitmus.SetActive(false);
    }

    //


    void _BrinjalMethodON()
    {
        brinjal.SetActive(true);
    }

    void _BrinjalMethodOFF()
    {
        brinjal.SetActive(false);
    }

    //

    void _Sugar_bowlMethodON()
    {
        sugar_bowl.SetActive(true);
    }

    void _Sugar_bowlMethodOFF()
    {
        sugar_bowl.SetActive(false);
    }

    //

    void _Chalk_boxMethodON()
    {
        chalkbox.SetActive(true);
    }

    void _Chalk_boxMethodOFF()
    {
        chalkbox.SetActive(false);
    }

    //

    void _LemonMethodON()
    {
        lemon.SetActive(true);
    }

    void _LemonMethodOFF()
    {
        lemon.SetActive(false);
    }


    //

    void _Lemon_slicesMethodON()
    {
        lemonslices.SetActive(true);
    }

    void _Lemon_slicesMethodOFF()
    {
        lemonslices.SetActive(false);
    }

    //

    void _BittergourdMethodON()
    {
        bittergourd.SetActive(true);
    }

    void _BittergourdMethodOFF()
    {
        bittergourd.SetActive(false);
    }


    //

    void _Exp_juice_glassMethodON()
    {
        exp_juice_glass.SetActive(true);
    }

    void _Exp_juice_glassMethodOFF()
    {
        exp_juice_glass.SetActive(false);
    }

    //

    void _TomatoMethodON()
    {
        tomato.SetActive(true);
    }

    void _TomatoMethodOFF()
    {
        tomato.SetActive(false);
    }


    //

    void _GrapesMethodON()
    {
        grapes.SetActive(true);
    }

    void _GrapesMethodOFF()
    {
        grapes.SetActive(false);
    }

    //

    void _AppleMethodON()
    {
        apple.SetActive(true);
    }

    void _AppleMethodOFF()
    {
        apple.SetActive(false);
    }

    //

    void _StrawberryMethodON()
    {
        strawberry.SetActive(true);
    }

    void _StrawberryMethodOFF()
    {
        strawberry.SetActive(false);
    }


    //

    void _BananaMethodON()
    {
        banana.SetActive(true);
    }

    void _BananaMethodOFF()
    {
        banana.SetActive(false);
    }


    //

    void _bee_expMethodON()
    {
        bee_exp.SetActive(true);
    }

    void _bee_expMethodOFF()
    {
        bee_exp.SetActive(false);
    }


    //

    void _VinegarMethodON()
    {
        vinegar.SetActive(true);
    }

    void _VinegarMethodOFF()
    {
        vinegar.SetActive(false);
    }


    //

    void _OrangeMethodON()
    {
        orange.SetActive(true);
    }

    void _OrangeMethodOFF()
    {
        orange.SetActive(false);
    }

    //

    void _PizzaMethodON()
    {
        pizza.SetActive(true);
    }

    void _PizzaMethodOFF()
    {
        pizza.SetActive(false);
    }

    //

    void _CurdMethodON()
    {
        curd.SetActive(true);
    }

    void _CurdMethodOFF()
    {
        curd.SetActive(false);
    }

    //

    void _DetergentMethodON()
    {
        detergent.SetActive(true);
    }

    void _DetergentMethodOFF()
    {
        detergent.SetActive(false);
    }

    //

    void _SanitizerMethodON()
    {
        sanitizer.SetActive(true);
    }

    void _SanitizerMethodOFF()
    {
        sanitizer.SetActive(false);
    }

    //

    void _Milk_of_magnesiaMethodON()
    {
        milk_of_magnesia.SetActive(true);
    }

    void _Milk_of_magnesiaMethodOFF()
    {
        milk_of_magnesia.SetActive(false);
    }

    //

    void _Baking_soda_bowlMethodON()
    {
        baking_soda_bowl.SetActive(true);
    }

    void _Baking_soda_bowlMethodOFF()
    {
        baking_soda_bowl.SetActive(false);
    }

    //

    void _CakeMethodON()
    {
        cake.SetActive(true);
    }

    void _CakeMethodOFF()
    {
        cake.SetActive(false);
    }

    //

    void _Bread_slicesMethodON()
    {
        bread_slices.SetActive(true);
    }

    void _Bread_slicesMethodOFF()
    {
        bread_slices.SetActive(false);
    }

    //

    void _Stovestand_dispMethodON()
    {
        stovestanddisp.SetActive(true);
    }

    void _Stovestand_dispMethodOFF()
    {
        stovestanddisp.SetActive(false);
    }

    //

    void _Beaker1_dispMethodON()
    {
        beaker1disp.SetActive(true);
    }

    void _Beaker1_dispMethodOFF()
    {
        beaker1disp.SetActive(false);
    }

    //

    void _Beaker2_dispMethodON()
    {
        beaker2disp.SetActive(true);
    }

    void _Beaker2_dispMethodOFF()
    {
        beaker2disp.SetActive(false);
    }

    //

    void _Beaker3_dispMethodON()
    {
        beaker3disp.SetActive(true);
    }

    void _Beaker3_dispMethodOFF()
    {
        beaker3disp.SetActive(false);
    }

    //

    void _Glassbottle_dispMethodON()
    {
        glassbottledisp.SetActive(true);
    }

    void _Glassbottle_dispMethodOFF()
    {
        glassbottledisp.SetActive(false);
    }

    //

    void _Juiceglass_dispMethodON()
    {
        juiceglassdisp.SetActive(true);
    }

    void _Juiceglass_dispMethodOFF()
    {
        juiceglassdisp.SetActive(false);
    }

    //

    void _Smallglass1_dispMethodON()
    {
        smallglass1disp.SetActive(true);
    }

    void _Smallglass1_dispMethodOFF()
    {
        smallglass1disp.SetActive(false);
    }

    //

    void _Smallglass2_dispMethodON()
    {
        smallglass2disp.SetActive(true);
    }

    void _Smallglass2_dispMethodOFF()
    {
        smallglass2disp.SetActive(false);
    }

    //

    void _Smallglass3_dispMethodON()
    {
        smallglass3disp.SetActive(true);
    }

    void _Smallglass3_dispMethodOFF()
    {
        smallglass3disp.SetActive(false);
    }

    //

    void _Petals_dispMethodON()
    {
        petalsdisp.SetActive(true);
    }

    void _Petals_dispMethodOFF()
    {
        petalsdisp.SetActive(false);
    }

    //

    void _BunsenburnerMethodON()
    {
        bunsenburner.SetActive(true);
    }

    void _BunsenburnerMethodOFF()
    {
        bunsenburner.SetActive(false);
    }
    //

    void _Stovestand_boilingMethodON()
    {
        stovestand_boiling.SetActive(true);
    }

    void _Stovestand_boilingMethodOFF()
    {
        stovestand_boiling.SetActive(false);
    }

    //

    void _Beaker_boilingMethodON()
    {
        beaker_boiling.SetActive(true);
    }

    void _Beaker_boilingMethodOFF()
    {
        beaker_boiling.SetActive(false);
    }


    //

    void _Petals_boilingMethodON()
    {
        petalsboiling.SetActive(true);
    }

    void _Petals_boilingMethodOFF()
    {
        petalsboiling.SetActive(false);
    }

    //

    void _Glassbottle_strainingMethodON()
    {
        glassbottle_straining.SetActive(true);
    }

    void _Glassbottle_strainingMethodOFF()
    {
        glassbottle_straining.SetActive(false);
    }

    //

    void _Tea_strainerMethodON()
    {
        tea_strainer.SetActive(true);
    }

    void _Tea_strainerMethodOFF()
    {
        tea_strainer.SetActive(false);
    }

    //

    void _Orange_with_fruitsMethodON()
    {
        orange_with_fruits.SetActive(true);
    }

    void _Orange_with_fruitsMethodOFF()
    {
        orange_with_fruits.SetActive(false);
    }

    //

    void _Glass_pouring_expMethodON()
    {
        glassbottle_pouring_exp.SetActive(true);
    }

    void _Glass_pouring_expMethodOFF()
    {
        glassbottle_pouring_exp.SetActive(false);
    }

    //

    void _Glass_litmusMethodON()
    {
        glass_litmus.SetActive(true);
    }

    void _Glass_litmusMethodOFF()
    {
        glass_litmus.SetActive(false);
    }

    //

    void _Straining_expMethodON()
    {
        straining_exp.SetActive(true);
    }

    void _Straining_expMethodOFF()
    {
        straining_exp.SetActive(false);
    }

    //

    void _Boilingwater_expMethodON()
    {
        boilingwater_exp.SetActive(true);
    }

    void _Boilingwater_expMethodOFF()
    {
        boilingwater_exp.SetActive(false);
    }

    //

    void _Bakingsoda_expMethodON()
    {
        bakingsoda_exp.SetActive(true);
    }

    void _BakingsodaMethodOFF()
    {
        bakingsoda_exp.SetActive(false);
    }

    //

    void _Vinegar_expMethodON()
    {
        vinegar_exp.SetActive(true);
    }

    void _Vinegar_expMethodOFF()
    {
        vinegar_exp.SetActive(false);
    }

    //

    void _Bee_stingMethodON()
    {
        bee_sting.SetActive(true);
    }

    void _Bee_stingMethodOFF()
    {
        bee_sting.SetActive(false);
    }

    //

    void _Sting_redbumpMethodON()
    {
        sting_redbump.SetActive(true);
    }

    void _Sting_redbumpMethodOFF()
    {
        sting_redbump.SetActive(false);
    }

    //


















    //

    void _Fruits1_audioMethod()
    {
        myAudio.clip = fruitsA;
        myAudio.Play();
    }

    //

    void _Fruits2_audioMethod()
    {
        myAudio.clip = fruitsB;
        myAudio.Play();
    }

    //

    void _Stomach_audioMethod()
    {
        myAudio.clip = stomach;
        myAudio.Play();
    }

    //
    void _Fertilizer_audioMethod()
    {
        myAudio.clip = fertilizer;
        myAudio.Play();
    }

    //
    void _Factory_audioMethod()
    {
        myAudio.clip = factory;
        myAudio.Play();
    }

    //

    void _Soap_sanitizer_audioMethod()
    {
        myAudio.clip = soap_sanitizer;
        myAudio.Play();
    }

    //

    void _Introduction_audioMethod()
    {
        myAudio.clip = introduction;
        myAudio.Play();
    }

    //

    void _Chemist_audio_audioMethod()
    {
        myAudio.clip = chemist_audio;
        myAudio.Play();
    }

    //

    void _Acid_intro_audioMethod()
    {
        myAudio.clip = acid_intro;
        myAudio.Play();
    }

    //

    void _Bee_audioMethod()
    {
        myAudio.clip = bee;
        myAudio.Play();
    }

    //

    void _Citrus_fruit_audioMethod()
    {
        myAudio.clip = citrus_fruit;
        myAudio.Play();
    }


    //

    void _Vinegar_audioMethod()
    {
        myAudio.clip = vinegar_v;
        myAudio.Play();
    }

    //

    void _Absorbic_acid_audioMethod()
    {
        myAudio.clip = absorbic_acid;
        myAudio.Play();
    }

    //

    void _Curd_audioMethod()
    {
        myAudio.clip = curd_v;
        myAudio.Play();
    }

    //

    void _Bases_intro_audioMethod()
    {
        myAudio.clip = bases_intro;
        myAudio.Play();
    }


    //

    void _Milk_of_magnesia_audioMethod()
    {
        myAudio.clip = milk_of_magnesia_v;
        myAudio.Play();
    }

    //

    void _Baakingsoda_audioMethod()
    {
        myAudio.clip = bakingsoda;
        myAudio.Play();
    }

    //



    void _Chalk_audioMethod()
    {
        myAudio.clip = chalk;
        myAudio.Play();
    }

    //

    void _Indicator_intro_audioMethod()
    {
        myAudio.clip = indicator_intro;
        myAudio.Play();
    }

    //

    void _Indicator_parts_exp_audioMethod()
    {
        myAudio.clip = indicator_parts_exp;
        myAudio.Play();
    }


    //
    void _Boiling_audioMethod()
    {
        myAudio.clip = boiling;
        myAudio.Play();
    }

    //

    void _Petals_audioMethod()
    {
        myAudio.clip = petals;
        myAudio.Play();
    }

    //

    void _Strainer_audioMethod()
    {
        myAudio.clip = strainer;
        myAudio.Play();
    }

    //

    void _Three_beakers_audioMethod()
    {
        myAudio.clip = three_beakers;
        myAudio.Play();
    }

    //

    void _Lemon_juice_water_audioMethod()
    {
        myAudio.clip = lemon_juice_water;
        myAudio.Play();
    }

    //

    void _Indicator_explanation_audioMethod()
    {
        myAudio.clip = indicator_explanation;
        myAudio.Play();
    }

    //

    void _Water_audioMethod()
    {
        myAudio.clip = water;
        myAudio.Play();
    }

    //

    void _Litmus_audioMethod()
    {
        myAudio.clip = litmus;
        myAudio.Play();
    }

    //

    void _Litmus_redtoblue_audioMethod()
    {
        myAudio.clip = litmus_redtoblue;
        myAudio.Play();
    }


    //

    void _Litmus_bluetored_audioMethod()
    {
        myAudio.clip = litmus_bluetored;
        myAudio.Play();
    }

    //

    void _Bee_sting_audioMethod()
    {
        myAudio.clip = bee_sting_v;
        myAudio.Play();
    }

    //

    void _Salt_intro_audioMethod()
    {
        myAudio.clip = salt_intro;
        myAudio.Play();
    }

    //

    void _Neuraalization_1_audioMethod()
    {
        myAudio.clip = neuralization_1;
        myAudio.Play();
    }

    //

    void _Neuraalization_2_audioMethod()
    {
        myAudio.clip = neuralization_2;
        myAudio.Play();
    }
    //

    void _Neuraalization_3_audioMethod()
    {
        myAudio.clip = neuralization_3;
        myAudio.Play();
    }
    //

    void _Neuraalization_4_audioMethod()
    {
        myAudio.clip = neuralization_4;
        myAudio.Play();
    }
    //

    void _Neuraalization_5_audioMethod()
    {
        myAudio.clip = neuralization_5;
        myAudio.Play();
    }
    //

    void _Neuraalization_6_audioMethod()
    {
        myAudio.clip = neuralization_6;
        myAudio.Play();
    }
    //

    void _Neuraalization_7_audioMethod()
    {
        myAudio.clip = neuralization_7;
        myAudio.Play();
    }
    //

    void _Neuraalization_8_audioMethod()
    {
        myAudio.clip = neuralization_8;
        myAudio.Play();
    }
    //




    //Scripts


    void liquid_in_lemon_juiceMethodON()

    {
        liquidInLemonWater.GetComponent<LiquidLevelDown>().enabled = true;
    }

    //

    void liquid_in_WaterMethodON()

    {
        liquidInWater.GetComponent<LiquidLevelDown>().enabled = true;
    }

    //

    void liquid_in_SoapWaterMethodON()

    {
        liquidInSoapWater.GetComponent<LiquidLevelDown>().enabled = true;
    }

    //

    void Boiling_Pink_waterMethodON()

    {
        boilingpinkwater.GetComponent<ColorLerp>().enabled = true;
    }

    //

    void NaOH_colorchangeMethodON()

    {
        liquidInNaOH.GetComponent<ColorLerp>().enabled = true;
    }

    //

    void HCL_colorchangeMethodON()

    {
        liquidInHCL.GetComponent<ColorLerp>().enabled = true;
    }

    //

    void StrainingwaterMethodON()

    {
        liquidInstrainer.GetComponent<LiquidLevelDown>().enabled = true;
    }

    //











    // anims


    void _Chalk_animationAnimmethod()
    {

        anim = chalkpiece.GetComponent<Animator>();
        anim.Play("Chalkpiece animation");
    }


    //

    void _Petals_boiling_animationAnimmethod()
    {

        anim = petals_boiling.GetComponent<Animator>();
        anim.Play("Petals boiling animation");
    }


    //

    void _Straining_beaker_animationAnimmethod()
    {

        anim = straining_beaker.GetComponent<Animator>();
        anim.Play("Straining beaker animation");
    }


    //

    void _Glass_pouring_animationAnimmethod()
    {

        anim = glass_pouring_bottle.GetComponent<Animator>();
        anim.Play("Pouring glass animation");
    }


    //


    void _Plate_animationAnimmethod()
    {

        anim = plate.GetComponent<Animator>();
        anim.Play("Baking soda pouring plate animation");
    }


    //

    void _Stirrer_bakingsoda_animationAnimmethod()
    {

        anim = stirrer_bakingsoda.GetComponent<Animator>();
        anim.Play("Stirrer for baking soda");
    }


    //

    void _Funnel_animationAnimmethod()
    {

        anim = funnel.GetComponent<Animator>();
        anim.Play("Funnel vinegar animation");
    }


    //

    //

    void _Stirrer_vinegar_animationAnimmethod()
    {

        anim = stirrer_vinegar.GetComponent<Animator>();
        anim.Play("Stirrer for vinegar");
    }

    //

    void _Hand_cross_animationAnimmethod()
    {

        anim = hand_cross.GetComponent<Animator>();
        anim.Play("Hand cross animation");
    }

    //

    void _Bee_sting_animationAnimmethod()
    {

        anim = honeybee_sting.GetComponent<Animator>();
        anim.Play("Bee animation");
    }

    //


    void _Hcl_stirrer_animationAnimmethod()
    {

        anim = stirrer_for_hcl.GetComponent<Animator>();
        anim.Play("Stirrer for HCL");
    }

    //

    void _NaOH_stirrer_animationAnimmethod()
    {

        anim = stirrer_for_naoh.GetComponent<Animator>();
        anim.Play("Stirrer for NaOH");
    }

    //

    void _Upperlayer_animationAnimmethod()
    {

        anim = upper_layer.GetComponent<Animator>();
        anim.Play("Upper body layer");
    }

    //

    void _Food_aimationAnimmethod()
    {

        anim = food.GetComponent<Animator>();
        anim.Play("Food animation");
    }

    //

    public void MiniGameStart()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MiniGameEnd()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(true);
    }

    public int beakerCount = 0;
    public GameObject questions;
    public List<Button> questionBtn = new List<Button>();
    private TargetController currentMiniGame;
    public void DipLitmusPaper(TargetController beaker)
    {
        int correctIndex = int.Parse(beaker.checkName);

        for (int i = 0; i < questionBtn.Count; i++)
        {
            questionBtn[i].onClick.RemoveAllListeners();

            if (i == correctIndex)
            {
                questionBtn[i].onClick.AddListener(CorrectAnswer);
            }
            else
            {
                questionBtn[i].onClick.AddListener(WrongAnswer);
            }
        }

        Transform litmus = beaker.subObject;
        currentMiniGame = beaker;

        litmus.DOLocalMoveY(0.03f, 1f).OnComplete(() =>
        {
            Material mat = new Material(litmus.gameObject.GetComponent<MeshRenderer>().material);
            if (litmus.gameObject.name == "A")
            {
                mat.color = Color.red;
            }
            else if (litmus.gameObject.name == "B")
            {
                mat.color = Color.blue;
            }
            else if (litmus.gameObject.name == "N")
            {
                mat.color = new Color(255, 0, 139, 1);
            }

            litmus.gameObject.GetComponent<MeshRenderer>().material = mat;

            litmus.DOLocalMoveY(0.3f, 1f).OnComplete(() =>
            {
                questions.SetActive(true);
            });
        });
    }

    public void CorrectAnswer()
    {
        questions.SetActive(false);

        beakerCount++;
        currentMiniGame.EndMiniGame();

        if (beakerCount == 5)
        {
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void WrongAnswer()
    {
        questions.SetActive(false);

        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    public int dropCount = 0;
    public void DropObj(TargetController cont)
    {
        dropCount++;

        if (dropCount == 6)
        {
            cont.EndMiniGame();
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public GameObject beeSting;
    public GameObject creamOnTable;
    public GameObject creamOnScreen;
    public void BeeAnim(Animator anim)
    {
        StartCoroutine(BeeAnimation(anim));
    }

    IEnumerator BeeAnimation(Animator anim)
    {
        anim.SetTrigger("Trigger");
        yield return new WaitForSeconds(1);
        beeSting.SetActive(true);
        yield return new WaitForSeconds(1);
        beeMiniGame = true;
    }

    bool beeMiniGame = false;
    public LayerMask layerMask;
    public Camera cam;
    private void Update()
    {
        if(beeMiniGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
                {
                    if (raycastHit.collider != null)
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        if (raycastHit.collider.gameObject.name == "Cream3")
                        {
                            CorrectCreamSelected(creamAnim);
                            beeMiniGame = false;
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

    public Animator creamAnim;
    public void CorrectCreamSelected(Animator anim)
    {
        StartCoroutine(CorrectCream(anim));
    }

    IEnumerator CorrectCream(Animator anim)
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Trigger");
        yield return new WaitForSeconds(2);
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void WrongCream()
    {
        creamOnTable.SetActive(true);
        creamOnScreen.SetActive(false);
        beeSting.transform.localScale = new Vector3(2, 2, 2);
    }





















}
