using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityStandardAssets.Water;

public class sfx_getting_know_plants : MonoBehaviour
{
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
    public GameObject lv1;

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

        level();
    }

    //on and off
    [Header("Explanation Assets")]


    public GameObject carrot;
    public GameObject cholophyll;
    public GameObject datura;
    public GameObject diedplant;
    public GameObject fruit;
    public GameObject leaf;
    public GameObject o2;
    public GameObject photosynthesis;
    public GameObject plant;
    public GameObject radish;
    public GameObject root;
    public GameObject soil;
    public GameObject tablit;
    public GameObject veg;
    public GameObject weedplant;
    public GameObject plant1;
    public GameObject blueliquid;
    public GameObject redliquid;
    public GameObject colourplant;
    public GameObject getting_to_know_plantst;
    public GameObject Types_of_plantst;
    public GameObject Parts_of_plantt;
    public GameObject rootb;
    public GameObject Roott;
    public GameObject importance_of_rootst;
    public GameObject Types_of_rootst;
    public GameObject tap_rootst;
    public GameObject small_roots;
    public GameObject Fibrous_rootst;
    public GameObject Stemt;
    public GameObject stem_is_dividedb;
    public GameObject Functions_of_stemt;
    public GameObject activityt;
    public GameObject Leaft;
    public GameObject Transpirationt;
    public GameObject Photosynthesist;
    public GameObject Chloroplast_and_chlorophyllt;
    public GameObject Flowerst;
    public GameObject pollen_sacsb;
    public GameObject Stigmat;
    public GameObject Stylet;
    public GameObject ovaryt;



    // Exp - Animations

   

    [Header("Explanation anims")]



    public GameObject root_h2o_anim;
    public GameObject water_tube_anim;




    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip gettingknowplantsau;
    public AudioClip theyveryau;
    public AudioClip plantsprovedaau;
    public AudioClip typesofplantsau;
    public AudioClip plantswithau;
    public AudioClip someplantsau;
    public AudioClip someplantsareau;
    public AudioClip partofplantsau;
    public AudioClip theyrootau;
    public AudioClip letuslookaU;
    public AudioClip rootau;
    public AudioClip rootaboutau;
    public AudioClip theyalsoaau;
    public AudioClip importansofrootau;
    public AudioClip oneau;
    public AudioClip twoau;
    public AudioClip threeau;
    public AudioClip fourau;
    public AudioClip fiveau;
    public AudioClip sixau;
    public AudioClip sevenau;
    public AudioClip eightau;
    public AudioClip nineau;
    public AudioClip tenau;
    public AudioClip typesofrootsau;
    public AudioClip tapandfiberau;
    public AudioClip taprootau;
    public AudioClip fiberau;
    public AudioClip stemau;
    public AudioClip thestemau;
    public AudioClip functionofstemau;
    public AudioClip thestemconductsau;
    public AudioClip theyvery;
    public AudioClip activityau;
    public AudioClip listau;
    public AudioClip leafau;
    public AudioClip thestalkau;
    public AudioClip thethickau;
    public AudioClip veinsau;
    public AudioClip transpirationau;
    public AudioClip thisisdoneau;
    public AudioClip photosynthesisau;
    public AudioClip howeverplantsau;
    public AudioClip theleavesau;
    public AudioClip chloroplastandchorophyllau;
    public AudioClip foodfactoriesau;
    public AudioClip flowerau;
    public AudioClip flowersirrespectiveau;
    public AudioClip innerpartau;
    public AudioClip petalsau;
    public AudioClip thesteckau;
    public AudioClip thefemaleau;
    public AudioClip thepistolau;
    public AudioClip stigmaau;
    public AudioClip styleau;
    public AudioClip ovaryau;

    public GameObject explanationScene;
    public GameObject gamePlayScene;

    public Transform scifilabSpawnPoint;
    public Transform player;











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














    // Line on/off

    void _carrot_methodon()
    {
        carrot.SetActive(true);
    }

    void _carrot_methodoff()
    {
        carrot.SetActive(false);
    }

    //

    void _chlorophyll_methodon()
    {
        cholophyll.SetActive(true);
    }

    void _chlorophyll_methodoff()
    {
        cholophyll.SetActive(false);
    }

    //

    void _datura_methodon()
    {
        datura.SetActive(true);
    }

    void _datura_methodoff()
    {
        datura.SetActive(false);
    }

    //

    void _fruits_methodon()
    {
        fruit.SetActive(true);
    }

    void _fruits_methodoff()
    {
        fruit.SetActive(false);
    }

    //

    void _leaf_methodon()
    {
        leaf.SetActive(true);
    }

    void _leaf_methodoff()
    {
        leaf.SetActive(false);
    }

    //

    void _died_plant_methodon()
    {
        diedplant.SetActive(true);
    }

    void _died_plant_methodoff()
    {
        diedplant.SetActive(false);
    }

    //

    void _o2_methodon()
    {
        o2.SetActive(true);
    }

    void _o2_methodoff()
    {
        o2.SetActive(false);
    }

    //

    void _photosynthesis_methodon()
    {
        photosynthesis.SetActive(true);
    }

    void _photosynthesis_methodoff()
    {
        photosynthesis.SetActive(false);
    }

    //

    void _plant_methodon()
    {
        plant.SetActive(true);
    }

    void _plant_methodoff()
    {
        plant.SetActive(false);
    }

    //

    void _radish_methodon()
    {
        radish.SetActive(true);
    }

    void _radish_methodoff()
    {
        radish.SetActive(false);
    }

    //

    void _root_methodon()
    {
        root.SetActive(true);
    }

    void _root_methodoff()
    {
        root.SetActive(false);
    }

    //

    void _soil_methodon()
    {
        soil.SetActive(true);
    }

    void _soil_methodoff()
    {
        soil.SetActive(false);
    }

    //

    void _tablet_methodon()
    {
        tablit.SetActive(true);
    }

    void _tablet_methodoff()
    {
        tablit.SetActive(false);
    }

    //

    void _veg_methodon()
    {
        veg.SetActive(true);
    }

    void _veg_methodoff()
    {
        veg.SetActive(false);
    }

    //

    void _weed_plant_methodon()
    {
        weedplant.SetActive(true);
    }

    void _weed_plant_methodoff()
    {
        weedplant.SetActive(false);
    }

    //

    void _plant1_methodon()
    {
        plant1.SetActive(true);
    }

    void _plant1_methodoff()
    {
        plant1.SetActive(false);
    }

    //

    void _blue_liqvid_methodon()
    {
        blueliquid.SetActive(true);
    }

    void _blue_liquid_methodoff()
    {
        blueliquid.SetActive(false);
    }

    //

    void _red_liquid_methodon()
    {
        redliquid.SetActive(true);
    }

    void _red_liquid_methodoff()
    {
        redliquid.SetActive(false);
    }

    //

    void _colour_plant_methodon()
    {
        colourplant.SetActive(true);
    }

    void _colour_plant_methodoff()
    {
        colourplant.SetActive(false);
    }

    //

    void _getting_to_knowplants_methodon()
    {
        getting_to_know_plantst.SetActive(true);
    }

    //

    void _Types_of_plants_methodon()
    {
        Types_of_plantst.SetActive(true);
    }

    //

    void _Parts_of_plant_methodon()
    {
        Parts_of_plantt.SetActive(true);
    }

    //

    void _rootb_methodon()
    {
        rootb.SetActive(true);
    }

    //

    void _roott_methodon()
    {
        Roott.SetActive(true);
    }

    //

    void _importance_of_roots_methodon()
    {
        importance_of_rootst.SetActive(true);
    }

    //

    void _Types_of_roots_methodon()
    {
        Types_of_plantst.SetActive(true);
    }

    //

    void _tap_roots_methodon()
    {
        tap_rootst.SetActive(true);
    }

    //

    void _small_roots_methodon()
    {
        small_roots.SetActive(true);
    }

    //

    void _Fibrous_roots_methodon()
    {
        Fibrous_rootst.SetActive(true);
    }

    //

    void _stem_methodon()
    {
        Stemt.SetActive(true);
    }

    //

    void _stem_is_divided_methodon()
    {
        stem_is_dividedb.SetActive(true);
    }

    //

    void _Functions_of_stem_methodon()
    {
        Functions_of_stemt.SetActive(true);
       
    }

    //

    void _Functions_of_stem_methodoff()
    {
        Functions_of_stemt.SetActive(false);

    }

    void ChangeToGameplay()
    {
        explanationScene.SetActive(false);
        gamePlayScene.SetActive(true);
    }

    //

    void _activity_methodon()
    {
        activityt.SetActive(true);
    }

    //

    void _activity_methodoff()
    {
        activityt.SetActive(false);
    }

    //

    void _Leaf_methodon()
    {
        Leaft.SetActive(true);
    }

    //

    void _Leaf_methodoff()
    {
        Leaft.SetActive(false);
    }

    //

    void _Transpiration_methodon()
    {
        Transpirationt.SetActive(true);
    }

    //

    void _Transpiration_methodoff()
    {
        Transpirationt.SetActive(false);
    }

    //

    void _Photosynthesis_methodon()
    {
        Photosynthesist.SetActive(true);
    }

    //

    void _Photosynthesis_methodoff()
    {
        Photosynthesist.SetActive(false);
    }

    //

    void _Chloroplast_and_chlorophyll_methodon()
    {
        Chloroplast_and_chlorophyllt.SetActive(true);
    }

    //

    void _Chloroplast_and_chlorophyll_methodoff()
    {
        Chloroplast_and_chlorophyllt.SetActive(false);
    }

    //

    void _Flowers_methodon()
    {
        Flowerst.SetActive(true);
    }

    //

    void _Flowers_methodoff()
    {
        Flowerst.SetActive(false);
    }

    //

    void _pollen_sacs_methodon()
    {
        pollen_sacsb.SetActive(true);
    }

    //

    void _Stigma_methodon()
    {
        Stigmat.SetActive(true);
    }

    //

    void _Stigma_methodoff()
    {
        Stigmat.SetActive(false);
    }

    //

    void _style_methodon()
    {
        Stylet.SetActive(true);
    }

    //

    void _style_methodoff()
    {
        Stylet.SetActive(false);
    }

    //

    void _ovary_methodon()
    {
        ovaryt.SetActive(true);
    }

    //

    void _ovary_methodoff()
    {
        ovaryt.SetActive(false);
    }

    //



    // Animations




    void _root_animationAnimmethod()
    {

        anim = root_h2o_anim.GetComponent<Animator>();
        anim.Play("root h20  anim");
    }
    void _water_animationAnimmethod()
    {

        anim = water_tube_anim.GetComponent<Animator>();
        anim.Play("water tube anim");
    }
















    //Audio play

    void gettingknowplants_method()
    {
        myAudio.clip = gettingknowplantsau;
        myAudio.Play();
    }

    //

    void theyvery_method()
    {
        myAudio.clip = theyveryau;
        myAudio.Play();
    }

    //

    void _plantsprvedaau_method()
    {
        myAudio.clip = plantsprovedaau;
        myAudio.Play();
    }

    //

    void _typesofplantsau_method()
    {
        myAudio.clip = typesofplantsau;
        myAudio.Play();
    }

    //

    void _plantswithau_method()
    {
        myAudio.clip = plantswithau;
        myAudio.Play();
    }

    //

    void _someplantsau_method()
    {
        myAudio.clip = someplantsau;
        myAudio.Play();
    }

    //

    void _someplantsareau_method()
    {
        myAudio.clip = someplantsareau;
        myAudio.Play();
    }

    //

    void _partofplantsau_method()
    {
        myAudio.clip = partofplantsau;
        myAudio.Play();
    }

    //

    void _theyrootau_method()
    {
        myAudio.clip = theyrootau;
        myAudio.Play();
    }

    //

    void _letuslookau_method()
    {
        myAudio.clip = letuslookaU;
        myAudio.Play();
    }

    //

    void _rootau_method()
    {
        myAudio.clip = rootau;
        myAudio.Play();
    }

    //

    void _rootaboutau_method()
    {
        myAudio.clip = rootaboutau;
        myAudio.Play();
    }

    //

    void _theyalsoau_method()
    {
        myAudio.clip = theyalsoaau;
        myAudio.Play();
    }

    //

    void _importansofrootau_method()
    {
        myAudio.clip = importansofrootau;
        myAudio.Play();
    }

    //

    void _oneau_method()
    {
        myAudio.clip = oneau;
        myAudio.Play();
    }

    //

    void _twoau_method()
    {
        myAudio.clip = twoau;
        myAudio.Play();
    }

    //

    void _threeau_method()
    {
        myAudio.clip = threeau;
        myAudio.Play();
    }

    //

    void _foureau_method()
    {
        myAudio.clip = fourau;
        myAudio.Play();
    }

    //

    void _fiveau_method()
    {
        myAudio.clip = fiveau;
        myAudio.Play();
    }

    //

    void _sixau_method()
    {
        myAudio.clip = sixau;
        myAudio.Play();
    }

    //


    void _sevenau_method()
    {
        myAudio.clip = sevenau;
        myAudio.Play();
    }

    //

    void _eightau_method()
    {
        myAudio.clip = eightau;
        myAudio.Play();
    }

    //

    void _nineau_method()
    {
        myAudio.clip = nineau;
        myAudio.Play();
    }

    //


    void _tenau_method()
    {
        myAudio.clip = tenau;
        myAudio.Play();
    }

    //

    void _typesofrootsau_method()
    {
        myAudio.clip = typesofrootsau;
        myAudio.Play();
    }

    //

    void _tapandfiberau_method()
    {
        myAudio.clip = tapandfiberau;
        myAudio.Play();
    }

    //


    void _taprootau_method()
    {
        myAudio.clip = taprootau;
        myAudio.Play();
    }

    //


    void _fiberau_method()
    {
        myAudio.clip = fiberau;
        myAudio.Play();
    }

    //

    void _stemau_method()
    {
        myAudio.clip = stemau;
        myAudio.Play();
    }

    //

    void _thestemau_method()
    {
        myAudio.clip = thestemau;
        myAudio.Play();
    }

    //

    void _function_of_stem_method()
    {
        myAudio.clip = functionofstemau;
        myAudio.Play();
    }

    //

    void _the_stem_conducts_method()
    {
        myAudio.clip = functionofstemau;
        myAudio.Play();
    }

    //

    void _activityau_method()
    {
        myAudio.clip = activityau;
        myAudio.Play();
    }

    //

    void _leafau_method()
    {
        myAudio.clip = leafau;
        myAudio.Play();
    }

    //

    void _the_stalk_au_method()
    {
        myAudio.clip = thestalkau;
        myAudio.Play();
    }

    //

    void _the_rhick_au_method()
    {
        myAudio.clip = thethickau;
        myAudio.Play();
    }

    //

    void _veinsau_method()
    {
        myAudio.clip = veinsau;
        myAudio.Play();
    }

    //

    void _transdpirationau_method()
    {
        myAudio.clip = transpirationau;
        myAudio.Play();
    }

    //

    void _this_is_doneau_method()
    {
        myAudio.clip = thisisdoneau;
        myAudio.Play();
    }

    //

    void _photosynthesis_au_method()
    {
        myAudio.clip = photosynthesisau;
        myAudio.Play();
    }

    //

    void _how_ever_plant_au_method()
    {
        myAudio.clip = howeverplantsau;
        myAudio.Play();
    }

    //

    void _the_leaves_au_method()
    {
        myAudio.clip = theleavesau;
        myAudio.Play();
    }

    //

    void _chloroplastandchlorophyll_au_method()
    {
        myAudio.clip = chloroplastandchorophyllau;
        myAudio.Play();
    }

    //

    void _food_factories_au_method()
    {
        myAudio.clip = foodfactoriesau;
        myAudio.Play();
    }

    //

    void _flowers_au_method()
    {
        myAudio.clip = flowerau;
        myAudio.Play();
    }

    //

    void _flower_au_method()
    {
        myAudio.clip = flowerau;
        myAudio.Play();
    }

    //

    void _flower_irrespective_au_method()
    {
        myAudio.clip = flowersirrespectiveau;
        myAudio.Play();
    }

    //

    void _inner_parts_au_method()
    {
        myAudio.clip = innerpartau;
        myAudio.Play();
    }

    //

    void _petals_au_method()
    {
        myAudio.clip = petalsau;
        myAudio.Play();
    }

    //

    void _the_stack_au_method()
    {
        myAudio.clip = thesteckau;
        myAudio.Play();
    }

    //

    void _the_female_au_method()
    {
        myAudio.clip = thefemaleau;
        myAudio.Play();
    }

    //

    void _the_pistolyau_method()
    {
        myAudio.clip = thepistolau;
        myAudio.Play();
    }

    //

    void _stigma_au_method()
    {
        myAudio.clip = stigmaau;
        myAudio.Play();
    }

    //

    void _styleau_method()
    {
        myAudio.clip = styleau;
        myAudio.Play();
    }

    //














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

















    void _ovary_au_method()
    {
        myAudio.clip = ovaryau;
        myAudio.Play();
    }
    
    public void CheckForHearbsAndShrubs()
    {
        int hearbsCount = 0;
        int shrubsCount = 0;

        for(int i  = 0; i < InventoryManager.Instance.items.Count; i++)
        {
            if (InventoryManager.Instance.items[i] != null && InventoryManager.Instance.items[i].item.name == "Herb")
            {
                hearbsCount += InventoryManager.Instance.items[i].count;
            }
            else if(InventoryManager.Instance.items[i] != null && InventoryManager.Instance.items[i].item.name == "Shrub")
            {
                shrubsCount += InventoryManager.Instance.items[i].count;
            }
        }

        if(hearbsCount <= 4 && shrubsCount <= 2)
        {
            if (InventoryManager.Instance.player != null)
            {
                InventoryManager.Instance.player.ChangePosition(scifilabSpawnPoint);
            }
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    private int pipeCount = 8;
    private int currentPipeCount = 0;
    public GameObject timeCrystal;
   
    public void CheckRotatblePipes()
    {
        currentPipeCount++;
        Debug.Log("PIPE ROTATED " + currentPipeCount);
        if(currentPipeCount == pipeCount)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            timeCrystal.SetActive(true);
        }
    }

    public List<TriggersManager> placementTables;
    public List<BoxCollider> pipes;

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void ChangePlayerPos(Transform point)
    {
        StartCoroutine(DelayChangePlayerPos(point));

    }

    IEnumerator DelayChangePlayerPos(Transform point)
    {
        yield return new WaitForSeconds(0.1f);
        //InventoryManager.Instance.inventryStatic.SetActive(false);
        InventoryManager.Instance.player.ChangePosition(point);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }


    public void CheckForHerbAndShrubPlacement()
    {
        foreach (TriggersManager triggersManager in placementTables)
        {
            if (!triggersManager.hasFilled) return;
        }
       
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        foreach (BoxCollider collider in pipes)
        {
            collider.isTrigger = true;
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
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    bool canExit = false;
    public void ReleaseO2()
    {
        canExit = true;
    }

    public Animator raddishAnim;
    public void TimeCrystal()
    {
        raddishAnim.SetTrigger("Grow");
        o2Obj.isTrigger = true;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Transform leafSpawnPoint;
    public void SciFiLabExit()
    {
        if(canExit)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            InventoryManager.Instance.player.ChangePosition(leafSpawnPoint);
        }
    }

    public void PushPlayerUpwards()
    {
        InventoryManager.Instance.player.gameObject.GetComponent<ThirdPersonController>().PushUp();
    }

    public void MiniGameStart()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
    }

    public void MiniGameEnd()
    {
        InventoryManager.Instance.player.gameObject.SetActive(true);
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

