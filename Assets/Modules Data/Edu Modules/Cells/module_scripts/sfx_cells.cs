using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using TMPro;

public class sfx_cells : MonoBehaviour
{

    #region SubtitleFunction
    public List<Subtitle> subtitles = new List<Subtitle>();
    public TextMeshProUGUI subtitleText;
    public GameObject subtitleObject;
    private int subtitleIndex = 0;
    private float currentDelay;

    private void Start()
    {
        if (subtitles.Count > 0)
        {
            currentDelay = subtitles[subtitleIndex].delay;
            Invoke("StartNextSubtitle", currentDelay);
        }
        else
        {
            Debug.LogError("No subtitles found in the list!");
        }
    }

    private void StartNextSubtitle()
    {
        if (!subtitleObject.activeInHierarchy)
        {
            subtitleObject.SetActive(true);
        }

        subtitleText.text = subtitles[subtitleIndex].subTitleText;

        if (subtitleIndex < subtitles.Count - 1)
        {
            subtitleIndex++;
            currentDelay = subtitles[subtitleIndex].delay;
            // Schedule the next subtitle after currentDelay seconds
            Invoke("StartNextSubtitle", currentDelay);
        }
        else
        {
            Debug.Log("All subtitles shown.");
        }
    }
    #endregion




    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

    private GameObject JustInstantiatedNoPlayerCanvas;

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
        JustInstantiatedNoPlayerCanvas.SetActive(true);

        miniGames[miniGameIndex].Output();
    }


    public Camera cam;
    public LayerMask layerMask;
    public List<GameObject> questions = new List<GameObject> ();
    public List<TargetController> miniGames = new List<TargetController>();
    private int miniGameIndex = 0;
    private bool canChoose = true;

    public bool isLastMiniGame = false;
    int ind = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canChoose && !isLastMiniGame)
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
                        miniGames[miniGameIndex].defaultEvent.Invoke();
                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && isLastMiniGame)
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
                        ind++;
                        if(ind == 4)
                        {
                           StepComplete();
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

    public void Level4MiniGame()
    {
        isLastMiniGame = true;
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public List<GameObject> shapeObjs = new List<GameObject>();
    private int shapeIndex = 0;
    public void Lev1P2ShapeSelect()
    {
        foreach (GameObject obj in shapeObjs)
        {
            obj.name = "a";
        }

        shapeIndex++;
        

        if(shapeIndex == 3)
        {
            MoveToNextMiniGame();
            StepComplete();
            return;
        }

        shapeObjs[shapeIndex].name = "Correct";
        StepComplete();

    }

    public void PlayAnim(Animator anim)
    {
        anim.SetBool("Bool", true);
    }

    public void Level3CorrectAnswer(Animator anim)
    {
        foreach(GameObject obj in questions)
        {
            obj.SetActive(false);
        }

        anim.SetBool("Bool", false);
    }

    public void QuestionPopUp(GameObject go)
    {
        StartCoroutine(PopUpDelay(go));
    }

    IEnumerator PopUpDelay(GameObject go)
    {
        yield return new WaitForSeconds(1);
        go.SetActive(true);
    }

    public Transform cameraTransform;
    public void ChangeCamHolder(Transform holder)
    {
        cameraTransform.position = holder.position;
        cameraTransform.rotation = holder.rotation;
    }

    public void TurnOffRaycast()
    {
        canChoose = false;
    }

    public void MoveToNextMiniGame()
    {
        foreach (GameObject obj in questions)
        {
            obj.SetActive(false);
        }

        canChoose = true;
        miniGames[miniGameIndex].EndMiniGame();
        miniGameIndex++;
        miniGames[miniGameIndex].Output();
    }

    public void TurnOnObject(GameObject obj)
    {
        canChoose = false;
        obj.SetActive(true);
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
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

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public Light myLight;



    public GameObject robert_hooke_pic;
    public GameObject robert_hooke_cells;
    public GameObject hen_eggg;
    public GameObject compound_microscope;
    public GameObject electron_microscope;
    public GameObject pcell_demo;
    public GameObject TO_BB;
    public GameObject uni;
    public GameObject bacteria_A;
    public GameObject incubator;
    public GameObject mushroom;
    public GameObject digestive_system;
    public GameObject tissue;
    public GameObject uni_cellshapes;
    public GameObject uni_amoeba_anim;
    public GameObject rbc_final;
    public GameObject cell_defP;
    public GameObject discovery_of_cellsP;
    public GameObject robertHookeP;
    public GameObject roberthookecellsP;
    public GameObject shape_sizeP;
    public GameObject vary_sizeP;
    public GameObject number_of_cellsP;
    public GameObject types_of_cellsP;
    public GameObject Unicellular_organisms;
    public GameObject unicellular_organismsP;
    public GameObject unicellular_organisms_expP;
    public GameObject unicellular_organisms_exmpP;
    public GameObject multicellular_organismsP;
    public GameObject multicellular_organisms_expP;
    public GameObject multicellular_organisms_exmpP;
    public GameObject trillion_billonP;
    public GameObject shape_of_cellsP;
    public GameObject cell_membraneP;
    public GameObject psudopodia_panelP;
    public GameObject size_of_cellsP;
    public GameObject micrometerP;
    public GameObject centimeterP;
    public GameObject cell_structureP;
    public GameObject cell_membrane_topP;
    public GameObject cell_membrane_compoP;
    public GameObject plasma_membraneP;
    public GameObject Osmosis_or_diffusionp;
    public GameObject Cell_wallP;
    public GameObject CytoplasmP;
    public GameObject OrganellesP;
    public GameObject Organelles_exmpsP;
    public GameObject NucleasP;
    public GameObject Nuclear_membraneP;
    public GameObject NucleolusP;
    public GameObject ChromosomesP;
    public GameObject genesP;
    public GameObject nucleas_is_mainP;
    public GameObject cell_type_nucP;
    public GameObject prokaryoticP;
    public GameObject prokaryotesP;
    public GameObject eukaryoticP;
    public GameObject eukaryotesP;
    public GameObject ProtoplasmP;
    public GameObject other_partsP;
    public GameObject protoplasm_downP;
    public GameObject VacuoleP;
    public GameObject PlastidsP;
    public GameObject Green_PlastidsP;
    public GameObject DifferencesP;
    public GameObject liver;
    public GameObject animal_cellB;
    public GameObject compound_microscopeB;
    public GameObject animal_cellC;
    public GameObject dropper;
    public GameObject needle;
    public GameObject onion_slice;
    public GameObject watch_slide;
    public GameObject glass_slide_top;
    public GameObject glass_slide_bottom;
    public GameObject glass_slides_combined;
    public GameObject small_onion_slice;
    public GameObject common_parts;
    public GameObject fat_cellP;
    public GameObject rbc_P;
    public GameObject muscle_cellP;
    public GameObject neuronP;
    public GameObject chromosome;
    public GameObject drip_eff;
    public GameObject iodine_small;
    public GameObject iodine_large;
    public GameObject water_drop;
    public GameObject ui_button;




    // Exp - Animations


    private Animator anim;
    [Space(30)] // 10 pixels of spacing here.
    [Header("Explanation anims")]
    

    public GameObject bacteria_anim;
    public GameObject upperlayer_anim;
    public GameObject amoeba_animation;
    public GameObject needle_anim;
    public GameObject slide_anim;
    public GameObject onion_anim;















    // Exp - Audio
    [Space(30)] // 10 pixels of spacing here.
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip all_organisms;
    public AudioClip cell;
    public AudioClip roberyt_hooke;
    public AudioClip honey_comb;
    public AudioClip robert_cells;
    public AudioClip microscope;
    public AudioClip different_microsopes;
    public AudioClip hen_egg;
    public AudioClip million_organisms;
    public AudioClip organs_shapes;
    public AudioClip types_of_orgamsims;
    public AudioClip unicellular;
    public AudioClip unicelular_example;
    public AudioClip unicellular_bacteria;
    public AudioClip multicellular;
    public AudioClip multicellular_examples;
    public AudioClip multicell_tissues;
    public AudioClip tissues;
    public AudioClip diff_organ_functions;
    public AudioClip trillion_cells;
    public AudioClip cell_shapes;
    public AudioClip cell_membrane;
    public AudioClip amoeba_exmp;
    public AudioClip amoeba_projections;
    public AudioClip psudopodia;
    public AudioClip white_amoeba;
    public AudioClip round_fatcells;
    public AudioClip spherical_redb_cells;
    public AudioClip pointed_muscle_cells;
    public AudioClip neurons;
    public AudioClip cell_size;
    public AudioClip tiny_cells;
    public AudioClip large_cells;
    public AudioClip microscopic_cells;
    public AudioClip bacteria_cell;
    public AudioClip ostrich_egg;
    public AudioClip cell_function_matters;
    public AudioClip an_organism;
    public AudioClip for_example;
    public AudioClip organ_similar_tissues;
    public AudioClip tissues_are_made;
    public AudioClip cell_basic_structure;
    public AudioClip basic_components;
    public AudioClip nucleas_cytoplasm;
    public AudioClip cellmembrane_gives;
    public AudioClip plasma_membrane;
    public AudioClip in_plant_cells;
    public AudioClip cell_walls;
    public AudioClip we_can_easily;
    public AudioClip onions_1;
    public AudioClip onions_2;
    public AudioClip onions_3;
    public AudioClip onions_4;
    public AudioClip onions_5;
    public AudioClip onions_6;
    public AudioClip cytoplasm_organelles;
    public AudioClip cytoplasm_exmps;
    public AudioClip nucleas_important;
    public AudioClip it_can_be_easily;
    public AudioClip nucleas_seperated;
    public AudioClip porous_membrane;
    public AudioClip nucleolus;
    public AudioClip chromosomes;
    public AudioClip genes;
    public AudioClip brown_eyes;
    public AudioClip diff_gene_combo;
    public AudioClip nucleas_is_main;
    public AudioClip in_contrast_to;
    public AudioClip prokaryotic_cells;
    public AudioClip prokaryotes_cells;
    public AudioClip prokaryotes_exmps;
    public AudioClip eukaryotic_cells;
    public AudioClip eukaryotes_cells;
    public AudioClip protoplasm;
    public AudioClip vacuoles;
    public AudioClip plastids;
    public AudioClip green_plastids;
    public AudioClip provide_green_color;
    public AudioClip plant_animal_1;
    public AudioClip plant_animal_2;
    public AudioClip plant_animal_3;
    public AudioClip plant_animal_4;

    // Update is called once per fram

    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }


    //camera_explanatory play pause

    void _camExp_Pause()
    {
        gameObject.GetComponent<Animator>().speed = 0f;
    }
    //



    //  On/Off



    // Light


    void _animal_cell_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-30.3899994f, 1.19599998f, 49.7400017f);
    }

    void _bacteriaA_light_positionMethodON()
    {
        myLight.intensity = 0.5f;
        myLight.transform.position = new Vector3(-37.1389999f, 3.54220009f, 58.2859993f);
    }

    void _bacteria_exmp_light_positionMethodON()
    {
        myLight.intensity = 0.5f;
        myLight.transform.position = new Vector3(-37.2509995f, 4.28800011f, 58.257f);
    }

    void _Bacteria_exp_light_positionMethodON()
    {
        myLight.intensity = 3f;
        myLight.transform.position = new Vector3(-34.2459602f, 1.92200005f, 65.7259979f);
    }

    void _Muscle_tissues_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-31.0551014f, 1.10800004f, 49.2784195f);
    }

    void _Shape_of_cells_light_positionMethodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(-33.2649994f, 1.86399996f, 40.9199982f);
    }

    void _Amoeba_whitecell_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-34.0781326f, 6.34299994f, 43.6377563f);
    }

    void _FatCell_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-35.1990013f, 6.15999985f, 42.1699982f);
    }

    void _RedBloodCell_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-36.1599998f, 6.15999985f, 45.2719994f);
    }

    void _MuscleCell_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-33.1010017f, 6.15999985f, 41.848999f);
    }

    void _Neuron_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-33.2340012f, 6.07999992f, 45.4140015f);
    }

    void _Size_of_cells_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-30.4911919f, 1.30309999f, 49.95895f);
    }

    void _Bacteria_final_light_positionMethodON()
    {
        myLight.intensity = 1.5f;
        myLight.transform.position = new Vector3(-34.382f, 1.92900002f, 65.6419983f);
    }

    void _RBC_final_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-36.1599998f, 6.15999985f, 45.2719994f);
    }

    void _Tissue_final_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-33.1010017f, 6.15999985f, 41.848999f);
    }

    void _2Cells_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-30.8431931f, 1.58000004f, 49.8839989f);
    }

    void _Plant_and_Animal_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-15.7684927f, 4.97700024f, 36.7203522f);
    }

    void _Chromosome_light_positionMethodON()
    {
        myLight.intensity = 1f;
        myLight.transform.position = new Vector3(-30.0068779f, 1.07599998f, 50.6321754f);
    }





    // jump to point buttons

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
























    // Objects


    void _ui_button_MethodON()
    {
        ui_button.SetActive(true);
    }


    void _WaterdropMethodON()
    {
        water_drop.SetActive(true);
    }

    void _WaterdropMethodOFF()
    {
        water_drop.SetActive(false);
    }


    void _Iodine_smallMethodON()
    {
        iodine_small.SetActive(true);
    }

    void _Iodine_smallMethodOFF()
    {
        iodine_small.SetActive(false);
    }


    void _Unicellular_organismsMethodON()
    {
        Unicellular_organisms.SetActive(true);
    }

    void _Unicellular_organismsMethodOFF()
    {
        Unicellular_organisms.SetActive(false);
    }

    //

    void _Iodine_largeMethodON()
    {
        iodine_large.SetActive(true);
    }

    void _Iodine_largeMethodOFF()
    {
        iodine_large.SetActive(false);
    }

    //

    void _Drip_effMethodON()
    {
        drip_eff.SetActive(true);
    }

    void _Drip_effMethodOFF()
    {
        drip_eff.SetActive(false);
    }

    //

    void _Organelle_exmpsPMethodON()
    {
        Organelles_exmpsP.SetActive(true);
    }

    void _Organelle_exmpsPMethodOFF()
    {
        Organelles_exmpsP.SetActive(false);
    }

    //

    void _Cell_membrane_compoPMethodON()
    {
        cell_membrane_compoP.SetActive(true);
    }

    void _Cell_membrane_compoPMethodOFF()
    {
        cell_membrane_compoP.SetActive(false);
    }

    //

    void _ChromosomeMethodON()
    {
        chromosome.SetActive(true);
    }

    void _ChromosomeMethodOFF()
    {
        chromosome.SetActive(false);
    }

    //

    void _NeuronPMethodON()
    {
        neuronP.SetActive(true);
    }

    void _NeuronPMethodOFF()
    {
        neuronP.SetActive(false);
    }

    //


    void _MuscleCellPMethodON()
    {
        muscle_cellP.SetActive(true);
    }

    void _MuscleCellPMethodOFF()
    {
        muscle_cellP.SetActive(false);
    }

    //


    void _RbcPMethodON()
    {
        rbc_P.SetActive(true);
    }

    void _RbcPMethodOFF()
    {
        rbc_P.SetActive(false);
    }

    //

    void _FatCellPMethodON()
    {
        fat_cellP.SetActive(true);
    }

    void _FatCellPMethodOFF()
    {
        fat_cellP.SetActive(false);
    }

    //

    void _Common_partsMethodON()
    {
        common_parts.SetActive(true);
    }

    void _Common_partsMethodOFF()
    {
        common_parts.SetActive(false);
    }

    //

    void _Small_onionMethodON()
    {
        small_onion_slice.SetActive(true);
    }

    void _Small_onionMethodOFF()
    {
        small_onion_slice.SetActive(false);
    }

    //

    void _GlassSlide_comboMethodON()
    {
        glass_slides_combined.SetActive(true);
    }

    void _GlassSlide_comboMethodOFF()
    {
        glass_slides_combined.SetActive(false);
    }

    //

    void _GlassSlide_topMethodON()
    {
        glass_slide_top.SetActive(true);
    }

    void _GlassSlide_topMethodOFF()
    {
        glass_slide_top.SetActive(false);
    }

    //

    void _GlassSlide_bottomMethodON()
    {
        glass_slide_bottom.SetActive(true);
    }

    void _OnionSlice_bottomMethodOFF()
    {
        glass_slide_bottom.SetActive(false);
    }

    //

    void _OnionSliceMethodON()
    {
        onion_slice.SetActive(true);
    }

    void _OnionSliceMethodOFF()
    {
        onion_slice.SetActive(false);
    }

    //

    void _WatchSlideMethodON()
    {
        watch_slide.SetActive(true);
    }

    void _WatchSlideMethodOFF()
    {
        watch_slide.SetActive(false);
    }

    //

    void _DropperMethodON()
    {
        dropper.SetActive(true);
    }

    void _DropperMethodOFF()
    {
        dropper.SetActive(false);
    }

    //

    void _NeedleMethodON()
    {
        needle.SetActive(true);
    }

    void _NeedleMethodOFF()
    {
        needle.SetActive(false);
    }

    //

    void _Animal_cellCMethodON()
    {
        animal_cellC.SetActive(true);
    }

    void _Animal_cellCMethodOFF()
    {
        animal_cellC.SetActive(false);
    }

    //

    void _Compound_microscopeBMethodON()
    {
        compound_microscopeB.SetActive(true);
    }

    void _Compound_microscopeBMethodOFF()
    {
        compound_microscopeB.SetActive(false);
    }

    //


    void _Animal_cellBMethodON()
    {
        animal_cellB.SetActive(true);
    }

    void _Animal_cellMethodOFF()
    {
        animal_cellB.SetActive(false);
    }

    //

    void _LiverMethodON()
    {
        liver.SetActive(true);
    }

    void _LiverMethodOFF()
    {
        liver.SetActive(false);
    }

    //

    void _Robert_hooke_picMethodON()
    {
        robert_hooke_pic.SetActive(true);
    }

    void _Robert_hooke_picMethodOFF()
    {
        robert_hooke_pic.SetActive(false);
    }

    //

    void _Robert_hooke_CellsMethodON()
    {
        robert_hooke_cells.SetActive(true);
    }

    void _Robert_hooke_CellsMethodOFF()
    {
        robert_hooke_cells.SetActive(false);
    }

    //

    void _Hen_eggMethodON()
    {
        hen_eggg.SetActive(true);
    }

    void _Hen_eggMethodOFF()
    {
        hen_eggg.SetActive(false);

    }

    //

    void _Compound_microscopeMethodON()
    {
        compound_microscope.SetActive(true);
    }

    void _Compound_microscopeMethodOFF()
    {
        compound_microscope.SetActive(false);

    }

    //

    void _Electron_microscopeMethodON()
    {
        electron_microscope.SetActive(true);
    }

    void _Electron_microscopeMethodOFF()
    {
        electron_microscope.SetActive(false);

    }

    //

    void _Pcell_demoMethodON()
    {
        pcell_demo.SetActive(true);
    }

    void _Pcell_demoMethodOFF()
    {
        pcell_demo.SetActive(false);

    }

    //


    void _TO_BBMethodON()
    {
        TO_BB.SetActive(true);
    }

    void _TO_BBMethodOFF()
    {
        TO_BB.SetActive(false);

    }

    //

    void _UniMethodON()
    {
        uni.SetActive(true);
    }

    void _UniMethodOFF()
    {
        uni.SetActive(false);

    }

    //

    void _BacteriaAMethodON()
    {
        bacteria_A.SetActive(true);
    }

    void _BacteriaAMethodOFF()
    {
        bacteria_A.SetActive(false);

    }

    //

    void _IncubatorMethodON()
    {
        incubator.SetActive(true);
    }

    void _IncubatorMethodOFF()
    {
        incubator.SetActive(false);

    }

    //

    void _MushroomMethodON()
    {
        mushroom.SetActive(true);
    }

    void _MushroomMethodOFF()
    {
        mushroom.SetActive(false);

    }

    //

    void _Digestive_systemMethodON()
    {
        digestive_system.SetActive(true);
    }

    void _Digestive_systemMethodOFF()
    {
        digestive_system.SetActive(false);

    }

    //

    void _TissuesMethodON()
    {
        tissue.SetActive(true);
    }

    void _TissuesMethodOFF()
    {
        tissue.SetActive(false);

    }

    //

    void _Uni_cellshapesMethodON()
    {
        uni_cellshapes.SetActive(true);
    }

    void _Uni_cellshapesMethodOFF()
    {
        uni_cellshapes.SetActive(false);

    }

    //

    void _Uni_ameoba_animMethodON()
    {
        uni_amoeba_anim.SetActive(true);
    }

    void _Uni_amoeba_animMethodOFF()
    {
        uni_amoeba_anim.SetActive(false);

    }

    //

    void _Rbc_finalMethodON()
    {
        rbc_final.SetActive(true);
    }

    void _Rbc_finalMethodOFF()
    {
        rbc_final.SetActive(false);

    }






    void _Cell_structurePMethodON()
    {
        cell_structureP.SetActive(true);
    }

    void _Cell_structurePMethodOFF()
    {
        cell_structureP.SetActive(false);
    }

    //
    void _Cell_membrane_topPMethodON()
    {
        cell_membrane_topP.SetActive(true);
    }

    void _Cell_membrane_topPMethodOFF()
    {
        cell_membrane_topP.SetActive(false);
    }

    //
    void _Plasma_membranePMethodON()
    {
        plasma_membraneP.SetActive(true);
    }

    void _Plasma_membranePMethodOFF()
    {
        plasma_membraneP.SetActive(false);
    }

    //
    void _Osmosis_or_diffusionPMethodON()
    {
        Osmosis_or_diffusionp.SetActive(true);
    }

    void _Osmosis_or_diffusionPMethodOFF()
    {
        Osmosis_or_diffusionp.SetActive(false);
    }

    //
    void _Cell_wallPMethodON()
    {
        Cell_wallP.SetActive(true);
    }

    void _Cell_wallPMethodOFF()
    {
        Cell_wallP.SetActive(false);
    }

    //
    void _CytoplasmPMethodON()
    {
        CytoplasmP.SetActive(true);
    }

    void _CytoplasmPMethodOFF()
    {
        CytoplasmP.SetActive(false);
    }

    //
    void _OrganellesPMethodON()
    {
        OrganellesP.SetActive(true);
    }

    void _OrganellesPMethodOFF()
    {
        OrganellesP.SetActive(false);
    }

    //
    void _NucleasPMethodON()
    {
        NucleasP.SetActive(true);
    }

    void _NucleasPMethodOFF()
    {
        NucleasP.SetActive(false);
    }

    //
    void _Nuclear_membranePMethodON()
    {
        Nuclear_membraneP.SetActive(true);
    }

    void _Nuclear_membranePMethodOFF()
    {
        Nuclear_membraneP.SetActive(false);
    }

    //
    void _NucleolusPMethodON()
    {
        NucleolusP.SetActive(true);
    }

    void _NucleolusPMethodOFF()
    {
        NucleolusP.SetActive(false);
    }

    //
    void _ChromosomesPMethodON()
    {
        ChromosomesP.SetActive(true);
    }

    void _ChromosomesPMethodOFF()
    {
        ChromosomesP.SetActive(false);
    }

    //
    void _GenesPMethodON()
    {
        genesP.SetActive(true);
    }

    void _GenesPMethodOFF()
    {
        genesP.SetActive(false);
    }
    //
    void _Nucleas_is_mainPPMethodON()
    {
        nucleas_is_mainP.SetActive(true);
    }

    void _Nucleas_is_mainPMethodOFF()
    {
        nucleas_is_mainP.SetActive(false);
    }
    //

    void _Types_of_cellsNucleasPMethodON()
    {
        cell_type_nucP.SetActive(true);
    }

    void _Types_of_cellsNucleasPMethodOFF()
    {
        cell_type_nucP.SetActive(false);
    }
    //


    void _ProkaryoticPMethodON()
    {
        prokaryoticP.SetActive(true);
    }

    void _ProkaryoticPMethodOFF()
    {
        prokaryoticP.SetActive(false);
    }

    //
    void _ProkaryotesPMethodON()
    {
        prokaryotesP.SetActive(true);
    }

    void _ProkaryotesPMethodOFF()
    {
        prokaryotesP.SetActive(false);
    }

    //
    void _EukaryoticPMethodON()
    {
        eukaryoticP.SetActive(true);
    }

    void _EukaryoticPMethodOFF()
    {
        eukaryoticP.SetActive(false);
    }

    //
    void _EukaryotesPMethodON()
    {
        eukaryotesP.SetActive(true);
    }

    void _EukaryotesPMethodOFF()
    {
        eukaryotesP.SetActive(false);
    }

    //
    void _ProtoplasmPMethodON()
    {
        ProtoplasmP.SetActive(true);
    }

    void _ProtoplasmPMethodOFF()
    {
        ProtoplasmP.SetActive(false);
    }
    //
    void _Other_parts_of_cellPMethodON()
    {
        other_partsP.SetActive(true);
    }

    void _Other_parts_of_cellPMethodOFF()
    {
        other_partsP.SetActive(false);
    }

    //
    void _Protoplasm_downPMethodON()
    {
        protoplasm_downP.SetActive(true);
    }

    void _Protoplasm_downPMethodOFF()
    {
        protoplasm_downP.SetActive(false);
    }

    //
    void _VacuolePMethodON()
    {
        VacuoleP.SetActive(true);
    }

    void _VacuolePMethodOFF()
    {
        VacuoleP.SetActive(false);
    }

    //
    void _PlastidsPMethodON()
    {
        PlastidsP.SetActive(true);
    }

    void _PlastidsPMethodOFF()
    {
        PlastidsP.SetActive(false);
    }

    //
    void _Green_PlastidsPMethodON()
    {
        Green_PlastidsP.SetActive(true);
    }

    void _Green_PlastidsPMethodOFF()
    {
        Green_PlastidsP.SetActive(false);
    }

    //
    void _DifferencesPMethodON()
    {
        DifferencesP.SetActive(true);
    }

    void _DifferencesPMethodOFF()
    {
        DifferencesP.SetActive(false);
    }

    //


















    void _Cell_defPMethodON()
    {
        cell_defP.SetActive(true);
    }

    void _Cell_defPMethodOFF()
    {
        cell_defP.SetActive(false);
    }

    //

    void _Discovery_ofCellsPMethodON()
    {
        discovery_of_cellsP.SetActive(true);
    }

    void _Discovery_ofCellsPMethodOFF()
    {
        discovery_of_cellsP.SetActive(false);
    }

    //

    void _RobertHookePMethodON()
    {
        robertHookeP.SetActive(true);
    }

    void _RobertHookePPMethodOFF()
    {
        robertHookeP.SetActive(false);
    }

    //

    void _RobertHooke_cellsPMethodON()
    {
        roberthookecellsP.SetActive(true);
    }

    void _RobertHooke_cellsPMethodOFF()
    {
        roberthookecellsP.SetActive(false);
    }

    //

    void _Shape_sizePMethodON()
    {
        shape_sizeP.SetActive(true);
    }

    void _Shape_sizePMethodOFF()
    {
        shape_sizeP.SetActive(false);
    }

    //

    void _Vary_sizePMethodON()
    {
        vary_sizeP.SetActive(true);
    }

    void _Vary_sizePMethodOFF()
    {
        vary_sizeP.SetActive(false);
    }

    //

    void _Number_of_cellsPMethodON()
    {
        number_of_cellsP.SetActive(true);
    }

    void _Number_of_cellsPMethodOFF()
    {
        number_of_cellsP.SetActive(false);
    }

    //

    void _Types_of_cellsPMethodON()
    {
        types_of_cellsP.SetActive(true);
    }

    void _Types_of_cellsPMethodOFF()
    {
        types_of_cellsP.SetActive(false);
    }

    //

    void _UnicellularOrganismsPMethodON()
    {
        unicellular_organismsP.SetActive(true);
    }

    void _UnicellularOrganismsPMethodOFF()
    {
        unicellular_organismsP.SetActive(false);
    }

    //

    void _Unicellular_expPMethodON()
    {
        unicellular_organisms_expP.SetActive(true);
    }

    void _Unicellular_expPMethodOFF()
    {
        unicellular_organisms_expP.SetActive(false);
    }

    //

    void _Unicellular_exmpPMethodON()
    {
        unicellular_organisms_exmpP.SetActive(true);
    }

    void _Unicellular_exmpPMethodOFF()
    {
        unicellular_organisms_exmpP.SetActive(false);
    }

    //

    void _MulticellularOrganismsPMethodON()
    {
        multicellular_organismsP.SetActive(true);
    }

    void _MulticellularOrganismsPMethodOFF()
    {
        multicellular_organismsP.SetActive(false);
    }

    //

    void _Multicellular_expPMethodON()
    {
        multicellular_organisms_expP.SetActive(true);
    }

    void _Multicellular_expPMethodOFF()
    {
        multicellular_organisms_expP.SetActive(false);
    }

    //

    void _Multicellular_exmpPMethodON()
    {
        multicellular_organisms_exmpP.SetActive(true);
    }

    void _Multicellular_exmpPMethodOFF()
    {
        multicellular_organisms_exmpP.SetActive(false);
    }

    //

    //

    void _Trillion_billionPMethodON()
    {
        trillion_billonP.SetActive(true);
    }

    void _Trillion_billionPMethodOFF()
    {
        trillion_billonP.SetActive(false);
    }

    //

    //

    void _Shape_of_cellsPMethodON()
    {
        shape_of_cellsP.SetActive(true);
    }

    void _Shape_of_cellsPMethodOFF()
    {
        shape_of_cellsP.SetActive(false);
    }

    //

    //

    void _Cell_membranePMethodON()
    {
        cell_membraneP.SetActive(true);
    }

    void _Cell_membranePMethodOFF()
    {
        cell_membraneP.SetActive(false);
    }

    //

    void _PsudopodiaPMethodON()
    {
        psudopodia_panelP.SetActive(true);
    }

    void _PsudopodiaPMethodOFF()
    {
        psudopodia_panelP.SetActive(false);
    }

    //

    void _Size_of_cellsPMethodON()
    {
        size_of_cellsP.SetActive(true);
    }

    void _Size_of_cellsPMethodOFF()
    {
        size_of_cellsP.SetActive(false);
    }

    //

    void _MicrometerPMethodON()
    {
        micrometerP.SetActive(true);
    }

    void _MicrometerPMethodOFF()
    {
        micrometerP.SetActive(false);
    }

    //

    void _CentimeterPMethodON()
    {
        centimeterP.SetActive(true);
    }

    void _CentimeterPMethodOFF()
    {
        centimeterP.SetActive(false);
    }

    //






    //Audios

    //

    // voice 2

    void _1An_organism_audioMethod()
    {
        myAudio.clip = an_organism;
        myAudio.Play();
    }

    //

    void _2For_example_audioMethod()
    {
        myAudio.clip = for_example;
        myAudio.Play();
    }

    //
    void _3Organ_similar_tissue_audioMethod()
    {
        myAudio.clip = organ_similar_tissues;
        myAudio.Play();
    }

    //
    void _4Tissues_are_made_audioMethod()
    {
        myAudio.clip = tissues_are_made;
        myAudio.Play();
    }

    //
    void _5Cell_basic_structure_audioMethod()
    {
        myAudio.clip = cell_basic_structure;
        myAudio.Play();
    }

    //
    void _6Basic_components_audioMethod()
    {
        myAudio.clip = basic_components;
        myAudio.Play();
    }

    //
    void _7Nucleas_cytoplasm_audioMethod()
    {
        myAudio.clip = nucleas_cytoplasm;
        myAudio.Play();
    }

    //
    void _8Cellmembrane_gives_audioMethod()
    {
        myAudio.clip = cellmembrane_gives;
        myAudio.Play();
    }

    //
    void _9Plasma_membrane_audioMethod()
    {
        myAudio.clip = plasma_membrane;
        myAudio.Play();
    }

    //
    void _10In_plant_cells_audioMethod()
    {
        myAudio.clip = in_plant_cells;
        myAudio.Play();
    }
    //
    void _11Cellwalls_surrounding_audioMethod()
    {
        myAudio.clip = cell_walls;
        myAudio.Play();
    }
    //
    void _12We_can_easily_audioMethod()
    {
        myAudio.clip = we_can_easily;
        myAudio.Play();
    }
    //
    void _13Onion1_audioMethod()
    {
        myAudio.clip = onions_1;
        myAudio.Play();
    }
    //
    void _14Onion2_audioMethod()
    {
        myAudio.clip = onions_2;
        myAudio.Play();
    }
    //
    void _15Onion3_audioMethod()
    {
        myAudio.clip = onions_3;
        myAudio.Play();
    }
    //
    void _16Onion4_audioMethod()
    {
        myAudio.clip = onions_4;
        myAudio.Play();
    }
    //
    void _17Onion5_audioMethod()
    {
        myAudio.clip = onions_5;
        myAudio.Play();
    }
    //
    void _18Onion6_audioMethod()
    {
        myAudio.clip = onions_6;
        myAudio.Play();
    }
    //
    void _19Cytoplasm_organilles_audioMethod()
    {
        myAudio.clip = cytoplasm_organelles;
        myAudio.Play();
    }
    //
    void _20Cytoplasm_examples_audioMethod()
    {
        myAudio.clip = cytoplasm_exmps;
        myAudio.Play();
    }
    //
    void _21Nucleas_important_audioMethod()
    {
        myAudio.clip = nucleas_important;
        myAudio.Play();
    }
    //
    void _22_It_can_be_audioMethod()
    {
        myAudio.clip = it_can_be_easily;
        myAudio.Play();
    }
    //
    void _23Nucleas_seperated_audioMethod()
    {
        myAudio.clip = nucleas_seperated;
        myAudio.Play();
    }
    //
    void _24Porous_membrane_audioMethod()
    {
        myAudio.clip = porous_membrane;
        myAudio.Play();
    }
    //
    void _25Nucleolus_audioMethod()
    {
        myAudio.clip = nucleolus;
        myAudio.Play();
    }
    //
    void _26Chromosomes_audioMethod()
    {
        myAudio.clip = chromosomes;
        myAudio.Play();
    }
    //
    void _27Genes_audioMethod()
    {
        myAudio.clip = genes;
        myAudio.Play();
    }
    //
    void _28Brown_eyes_audioMethod()
    {
        myAudio.clip = brown_eyes;
        myAudio.Play();
    }
    //
    void _29Diff_gene_combo_audioMethod()
    {
        myAudio.clip = diff_gene_combo;
        myAudio.Play();
    }
    //
    void _30Nucleas_is_main_audioMethod()
    {
        myAudio.clip = nucleas_is_main;
        myAudio.Play();
    }
    //
    void _31In_Contrast_to_audioMethod()
    {
        myAudio.clip = in_contrast_to;
        myAudio.Play();
    }
    //
    void _32Prokaryotic_cells_audioMethod()
    {
        myAudio.clip = prokaryotic_cells;
        myAudio.Play();
    }
    //
    void _33Prokartotes_audioMethod()
    {
        myAudio.clip = prokaryotes_cells;
        myAudio.Play();
    }
    //
    void _34Prokaryotic_exmps_audioMethod()
    {
        myAudio.clip = prokaryotes_exmps;
        myAudio.Play();
    }
    //
    void _35Eukaryotic_cells_audioMethod()
    {
        myAudio.clip = eukaryotic_cells;
        myAudio.Play();
    }
    //
    void _36Eukaryotes_audioMethod()
    {
        myAudio.clip = eukaryotes_cells;
        myAudio.Play();
    }
    //
    void _37Protoplasm_audioMethod()
    {
        myAudio.clip = protoplasm;
        myAudio.Play();
    }
    //
    void _38Vacuoles_audioMethod()
    {
        myAudio.clip = vacuoles;
        myAudio.Play();
    }
    //
    void _39Plastids_audioMethod()
    {
        myAudio.clip = plastids;
        myAudio.Play();
    }
    //
    void _40Green_plastids_audioMethod()
    {
        myAudio.clip = green_plastids;
        myAudio.Play();
    }
    //
    void _41Provide_green_color_audioMethod()
    {
        myAudio.clip = provide_green_color;
        myAudio.Play();
    }
    //
    void _42Plant_animal1_audioMethod()
    {
        myAudio.clip = plant_animal_1;
        myAudio.Play();
    }
    //
    void _43Plant_animal2_audioMethod()
    {
        myAudio.clip = plant_animal_2;
        myAudio.Play();
    }
    //
    void _44Plant_animal3_audioMethod()
    {
        myAudio.clip = plant_animal_3;
        myAudio.Play();
    }
    //
    void _45Plant_animal4_audioMethod()
    {
        myAudio.clip = plant_animal_4;
        myAudio.Play();
    }

















    // voice 1

    //
    void _1All_organisms_audioMethod()
    {
        myAudio.clip = all_organisms;
        myAudio.Play();
    }

    //

    void _2Cell_audioMethod()
    {
        myAudio.clip = cell;
        myAudio.Play();
    }


    //

    void _3Robert_hooke_audioMethod()
    {
        myAudio.clip = roberyt_hooke;
        myAudio.Play();
    }

    //

    void _4Honeycomb_audioMethod()
    {
        myAudio.clip = honey_comb;
        myAudio.Play();
    }

    //

    void _5robert_cells_audioMethod()
    {
        myAudio.clip = robert_cells;
        myAudio.Play();
    }

    //

    void _6Microscope_audioMethod()
    {
        myAudio.clip = microscope;
        myAudio.Play();
    }

    //

    void _7Different_microscopes_audioMethod()
    {
        myAudio.clip = different_microsopes;
        myAudio.Play();
    }

    //

    void _8Hen_egg_audioMethod()
    {
        myAudio.clip = hen_egg;
        myAudio.Play();
    }

    //

    void _9Million_organisms_audioMethod()
    {
        myAudio.clip = million_organisms;
        myAudio.Play();
    }

    //

    void _10Organs_shapes_audioMethod()
    {
        myAudio.clip = organs_shapes;
        myAudio.Play();
    }

    //

    void _11Types_of_organisms_audioMethod()
    {
        myAudio.clip = types_of_orgamsims;
        myAudio.Play();
    }

    //

    void _12Unicellular_audioMethod()
    {
        myAudio.clip = unicellular;
        myAudio.Play();
    }

    //

    void _13Unicellular_example_audioMethod()
    {
        myAudio.clip = unicelular_example;
        myAudio.Play();
    }

    //

    void _14Unicellular_bacteria_audioMethod()
    {
        myAudio.clip = unicellular_bacteria;
        myAudio.Play();
    }

    //

    void _15Multicellular_audioMethod()
    {
        myAudio.clip = multicellular;
        myAudio.Play();
    }

    //

    void _16Multicellular_examples_audioMethod()
    {
        myAudio.clip = multicellular_examples;
        myAudio.Play();
    }

    //

    void _17Multicell_Tissues_audioMethod()
    {
        myAudio.clip = multicell_tissues;
        myAudio.Play();
    }

    //

    void _18Tissues_audioMethod()
    {
        myAudio.clip = tissues;
        myAudio.Play();
    }

    //

    void _19Diff_organ_functions_audioMethod()
    {
        myAudio.clip = diff_organ_functions;
        myAudio.Play();
    }

    //

    void _20Trillion_cells_audioMethod()
    {
        myAudio.clip = trillion_cells;
        myAudio.Play();
    }

    //

    void _21Cell_shapes_audioMethod()
    {
        myAudio.clip = cell_shapes;
        myAudio.Play();
    }

    //

    void _22Cell_membrane_audioMethod()
    {
        myAudio.clip = cell_membrane;
        myAudio.Play();
    }

    //

    void _23Amoeba_example_audioMethod()
    {
        myAudio.clip = amoeba_exmp;
        myAudio.Play();
    }

    //

    void _24Amoeba_projections_audioMethod()
    {
        myAudio.clip = amoeba_projections;
        myAudio.Play();
    }

    //

    void _25Psudopodia_audioMethod()
    {
        myAudio.clip = psudopodia;
        myAudio.Play();
    }

    //

    void _26White_amoeba_audioMethod()
    {
        myAudio.clip = white_amoeba;
        myAudio.Play();
    }
    //

    void _27Round_fatcells_audioMethod()
    {
        myAudio.clip = round_fatcells;
        myAudio.Play();
    }

    //

    void _28Speherical_redb_cells_audioMethod()
    {
        myAudio.clip = spherical_redb_cells;
        myAudio.Play();
    }

    //

    void _29Pointed_muscle_cells_audioMethod()
    {
        myAudio.clip = pointed_muscle_cells;
        myAudio.Play();
    }

    //

    void _30Neurons_audioMethod()
    {
        myAudio.clip = neurons;
        myAudio.Play();
    }

    //

    void _31Cell_size_audioMethod()
    {
        myAudio.clip = cell_size;
        myAudio.Play();
    }

    //

    void _32Tiny_cells_audioMethod()
    {
        myAudio.clip = tiny_cells;
        myAudio.Play();
    }

    //

    void _33Large_cells_audioMethod()
    {
        myAudio.clip = large_cells;
        myAudio.Play();
    }

    //

    void _34Microscopic_cell_audioMethod()
    {
        myAudio.clip = microscopic_cells;
        myAudio.Play();
    }

    //

    void _35Bacteria_cell_audioMethod()
    {
        myAudio.clip = bacteria_cell;
        myAudio.Play();
    }

    //

    void _36Ostrich_egg_audioMethod()
    {
        myAudio.clip = ostrich_egg;
        myAudio.Play();
    }

    //

    void _37Cell_function_matters_audioMethod()
    {
        myAudio.clip = cell_function_matters;
        myAudio.Play();
    }
































    // Animations


    void _bacteria_animationAnimmethod()
    {

        anim = bacteria_anim.GetComponent<Animator>();
        anim.Play("Bacteria animation");
    }

    //

    void _UpperlayeranimationAnimmethod()
    {

        anim = upperlayer_anim.GetComponent<Animator>();
        anim.Play("Upper layer animation");
    }

    //

    void _Amoeba_animationAnimmethod()
    {

        anim = amoeba_animation.GetComponent<Animator>();
        anim.Play("Amoeba anim");
    }

    //

    void _Needle_animationAnimmethod()
    {

        anim = needle_anim.GetComponent<Animator>();
        anim.Play("Needle anim");
    }

    //

    void _Slide_animationAnimmethod()
    {

        anim = slide_anim.GetComponent<Animator>();
        anim.Play("Slide animation");
    }

    //

    void _Onion_animationAnimmethod()
    {

        anim = onion_anim.GetComponent<Animator>();
        anim.Play("Onion anim");
    }

}
