using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sxf_crop_production_and_management_8cl : MonoBehaviour
{
    public Transform wayPoint1;
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
            animator.Play("camera_animation", 0, targetNormalizedTime);
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
            case 1: Level4(); break;
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

    public void Level4()
    {
        SaveProgress(1, 0, 3);
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

        SetWayPoint(wayPoint1);
        level();

    }

    //on and off
    [Header("Explanation Assets")]


    public GameObject introtion_t;
    public GameObject type_of_crops_t;
    public GameObject same_type_of_plants_d;
    public GameObject rabi_crops_t;
    public GameObject winter_season_d;
    public GameObject kharif_crops_t;
    public GameObject rainy_season_d;
    public GameObject agricultural_practices_t;
    public GameObject plowing_t;
    public GameObject the_process_of_d;
    public GameObject plowshare_d;
    public GameObject plowshaft_d;
    public GameObject hoe_d;
    public GameObject tractors_d;
    public GameObject cultivator_d;
    public GameObject sowing_t;
    public GameObject funnel_shaped_tools_d;
    public GameObject seed_drills_d;
    public GameObject manure_t;
    public GameObject manure_is_an_d;
    public GameObject manuring_d;
    public GameObject advantages_of_manure_t;
    public GameObject one_d;
    public GameObject two_d;
    public GameObject three_d;
    public GameObject four_d;
    public GameObject fertilizers_t;
    public GameObject potassium_d;
    public GameObject urea_d;
    public GameObject difference_between_t;
    public GameObject crops_rotalion_t;
    public GameObject the_grouth_of_d;
    public GameObject leguminous_crops_d;
    public GameObject rhizo_bium_d;




    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;


    public AudioClip intro_au;
    public AudioClip likeus_au;
    public AudioClip foodhas_au;
    public AudioClip types_of_crops_au;
    public AudioClip can_we_call_au;
    public AudioClip if_you_au;
    public AudioClip they_are_au;
    public AudioClip rabi_au;
    public AudioClip kharif_au;
    public AudioClip agricutural_au;
    public AudioClip ploeing_au;
    public AudioClip the_loosen_au;
    public AudioClip these_nutrtes_au;
    public AudioClip the_other_au;
    public AudioClip another_au;
    public AudioClip you_must_au;
    public AudioClip sowing_au;
    public AudioClip have_you_au;
    public AudioClip this_is_good_au;
    public AudioClip he_is_filling_au;
    public AudioClip nowadays_au;
    public AudioClip manure_au;
    public AudioClip soil_fertilty_au;
    public AudioClip advantanges_au;
    public AudioClip feltiliers_au;
    public AudioClip feltiliers_are_au;
    public AudioClip the_useof_au;
    public AudioClip crop_rotantion_au;
    public AudioClip when_the_au;
    public AudioClip you_will_au;
    public AudioClip this_planting_au;
















    // Start is called before the first frame update
  

    private bool collectedSeed = false;
    private bool collectedWatermelons = false;
    // Update is called once per frame
    void Update()
    {
        if (shouldSkipLevel1)
        {
            lv1.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!collectedSeed && !collectedWatermelons)
            {
                StartCoroutine(CheckForItems());
            }
        }
    }

    IEnumerator CheckForItems()
    {
        yield return new WaitForSeconds(1);
        if (InventoryManager.Instance.items.Count != 0)
        {
            foreach (Item itm in InventoryManager.Instance.items)
            {
                if (itm.item.itemName == "Seed" && !collectedSeed)
                {

                    collectedSeed = true;
                    break;
                }
            }
        }
    }


    // Line on/off

    //
    void introtion_methodON()
    {
        introtion_t.SetActive(true);
    }

    void introtion_methodOFF()
    {
        introtion_t.SetActive(false);
    }


    //
    void type_of_crops_methodON()
    {
        type_of_crops_t.SetActive(true);
    }

    void type_of_crops_methodOFF()
    {
        type_of_crops_t.SetActive(false);
    }





    //
    void same_type_of_plants_methodON()
    {
        same_type_of_plants_d.SetActive(true);
    }


    void same_type_of_plants_methodOFF()
    {
        same_type_of_plants_d.SetActive(false);
    }





    //
    void rabi_crops_methodON()
    {
        rabi_crops_t.SetActive(true);
    }


    void rabi_crops_methodOFF()
    {
        rabi_crops_t.SetActive(false);
    }








    //
    void winter_season_methodON()
    {
        winter_season_d.SetActive(true);
    }


    void winter_season_methodOFF()
    {
        winter_season_d.SetActive(false);
    }






    //
    void kharif_crops_methodON()
    {
        kharif_crops_t.SetActive(true);
    }

    void kharif_crops_methodOFF()
    {
        kharif_crops_t.SetActive(false);
    }







    //
    void rainy_season_methodON()
    {
        rainy_season_d.SetActive(true);
    }

    void rainy_season_methodOFF()
    {
        rainy_season_d.SetActive(false);
    }






    //
    void agricutural_practices_methodON()
    {
        agricultural_practices_t.SetActive(true);
    }

    void agricutural_practices_methodOFF()
    {
        agricultural_practices_t.SetActive(false);
    }






    //
    void plowing_methodON()
    {
        plowing_t.SetActive(true);
    }


    void plowing_methodOF()
    {
        plowing_t.SetActive(false);
    }





    //
    void the_process_of_methodON()
    {
        the_process_of_d.SetActive(true);
    }

    void the_process_of_methodOFF()
    {
        the_process_of_d.SetActive(false);
    }







    //
    void plowshare_methodON()
    {
        plowshare_d.SetActive(true);
    }

    void plowshare_methodOFF()
    {
        plowshare_d.SetActive(false);
    }






    //
    void plowshaft_methodON()
    {
        plowshaft_d.SetActive(true);
    }


    void plowshaft_methodOFF()
    {
        plowshaft_d.SetActive(false);
    }







    //
    void hoe_methodON()
    {
        hoe_d.SetActive(true);
    }

    void hoe_methodOFF()
    {
        hoe_d.SetActive(false);
    }







    //
    void tractors_methodON()
    {
        tractors_d.SetActive(true);
    }


    void tractors_methodOFF()
    {
        tractors_d.SetActive(false);
    }






    //
    void cultivator_methodON()
    {
        cultivator_d.SetActive(true);
    }


    void cultivator_methodOF()
    {
        cultivator_d.SetActive(false);
    }







    //
    void sowing_methodON()
    {
        sowing_t.SetActive(true);
    }


    void sowing_methodOF()
    {
        sowing_t.SetActive(false);
    }








    //
    void funnel_shaped_tools_methodON()
    {
        funnel_shaped_tools_d.SetActive(true);
    }


    void funnel_shaped_tools_methodOFF()
    {
        funnel_shaped_tools_d.SetActive(false);
    }







    //
    void seed_drills_methodON()
    {
        seed_drills_d.SetActive(true);
    }

    void seed_drills_methodOFF()
    {
        seed_drills_d.SetActive(false);
    }








    //
    void manure_methodON()
    {
        manure_t.SetActive(true);
    }


    void manure_methodOFF()
    {
        manure_t.SetActive(false);
    }






    //
    void manure_is_an_methodON()
    {
        manure_is_an_d.SetActive(true);
    }


    void manure_is_an_methodOFF()
    {
        manure_is_an_d.SetActive(false);
    }






    //
    void manuring_methodON()
    {
        manuring_d.SetActive(true);
    }



    void manuring_methodOFF()
    {
        manuring_d.SetActive(false);
    }





    //
    void advantages_of_manure_methodON()
    {
        advantages_of_manure_t.SetActive(true);
    }

    void advantages_of_manure_methodOFF()
    {
        advantages_of_manure_t.SetActive(false);
    }









    //
    void one_methodON()
    {
        one_d.SetActive(true);
    }

    void one_methodOFF()
    {
        one_d.SetActive(false);
    }







    //
    void two_methodON()
    {
        two_d.SetActive(true);
    }


    void two_methodOFF()
    {
        two_d.SetActive(false);
    }






    //
    void three_methodON()
    {
        three_d.SetActive(true);
    }



    void three_methodOFF()
    {
        three_d.SetActive(false);
    }








    //
    void four_methodON()
    {
        four_d.SetActive(true);
    }

    void four_methodOFF()
    {
        four_d.SetActive(false);
    }









    //
    void fertilizer_methodON()
    {
        fertilizers_t.SetActive(true);
    }

    void fertilizer_methodOFF()
    {
        fertilizers_t.SetActive(false);
    }







    //
    void potassium_methodON()
    {
        potassium_d.SetActive(true);
    }

    void potassium_methodOFF()
    {
        potassium_d.SetActive(false);
    }









    //
    void urea_methodON()
    {
        urea_d.SetActive(true);
    }



    void urea_methodOFF()
    {
        urea_d.SetActive(false);
    }







    //
    void difference_between_methodON()
    {
        difference_between_t.SetActive(true);
    }


    void difference_between_methodOFF()
    {
        difference_between_t.SetActive(false);
    }







    //
    void crops_rotalion_methodON()
    {
        crops_rotalion_t.SetActive(true);
    }


    void crops_rotalion_methodOFF()
    {
        crops_rotalion_t.SetActive(false);
    }







    //
    void the_groth_of_methodON()
    {
        the_grouth_of_d.SetActive(true);
    }


    void the_groth_of_methodOFF()
    {
        the_grouth_of_d.SetActive(false);
    }






    //
    void leguminous_methodON()
    {
        leguminous_crops_d.SetActive(true);
    }

    void leguminous_methodOFF()
    {
        leguminous_crops_d.SetActive(false);
    }







    //
    void rhizo_bium_methodON()
    {
        rhizo_bium_d.SetActive(true);
    }

    void rhizo_bium_methodOFF()
    {
        rhizo_bium_d.SetActive(false);
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
























    //Audio play

    void intro_au_method()
    {
        myAudio.clip = intro_au;
        myAudio.Play();
    }

    //

    void likeus_au_method()
    {
        myAudio.clip = likeus_au;
        myAudio.Play();
    }

    //

    void foodhas_au_method()
    {
        myAudio.clip = foodhas_au;
        myAudio.Play();
    }

    //

    void types_of_crops_au_method()
    {
        myAudio.clip = types_of_crops_au;
        myAudio.Play();
    }

    //

    void can_we_call_au_method()
    {
        myAudio.clip = can_we_call_au;
        myAudio.Play();
    }

    //

    void if_you_au_method()
    {
        myAudio.clip = if_you_au;
        myAudio.Play();
    }

    //

    void they_are_au_method()
    {
        myAudio.clip = they_are_au;
        myAudio.Play();
    }

    //

    void rabi_au_method()
    {
        myAudio.clip = rabi_au;
        myAudio.Play();
    }

    //

    void kharif_au_method()
    {
        myAudio.clip = kharif_au;
        myAudio.Play();
    }

    //

    void agricutural_au_method()
    {
        myAudio.clip = agricutural_au;
        myAudio.Play();
    }

    //

    void plowing_au_method()
    {
        myAudio.clip = ploeing_au;
        myAudio.Play();
    }

    //

    void the_loosen_au_method()
    {
        myAudio.clip = the_loosen_au;
        myAudio.Play();
    }

    //

    void these_nutrtes_au_method()
    {
        myAudio.clip = these_nutrtes_au;
        myAudio.Play();
    }

    //

    void the_other_au_method()
    {
        myAudio.clip = the_other_au;
        myAudio.Play();
    }

    //

    void another_au_method()
    {
        myAudio.clip = another_au;
        myAudio.Play();
    }

    //

    void you_must_au_method()
    {
        myAudio.clip = you_must_au;
        myAudio.Play();
    }

    //

    void sowing_au_method()
    {
        myAudio.clip = sowing_au;
        myAudio.Play();
    }

    //

    void have_you_au_method()
    {
        myAudio.clip = have_you_au;
        myAudio.Play();
    }

    //

    void this_is_good_au_method()
    {
        myAudio.clip = this_is_good_au;
        myAudio.Play();
    }

    //


    void he_is_filling_au_method()
    {
        myAudio.clip = he_is_filling_au;
        myAudio.Play();
    }

    //

    void now_day_au_method()
    {
        myAudio.clip = nowadays_au;
        myAudio.Play();
    }

    //

    void manure_au_method()
    {
        myAudio.clip = manure_au;
        myAudio.Play();
    }

    //

    void soil_fertilty_au_method()
    {
        myAudio.clip = soil_fertilty_au;
        myAudio.Play();
    }

    //

    void advantanges_au_method()
    {
        myAudio.clip = advantanges_au;
        myAudio.Play();
    }

    //

    void fertilizers_au_method()
    {
        myAudio.clip = feltiliers_au;
        myAudio.Play();
    }

    //

    void fertilizer_are_au_method()
    {
        myAudio.clip = feltiliers_are_au;
        myAudio.Play();
    }

    //

    void the_uses_of_au_method()
    {
        myAudio.clip = the_useof_au;
        myAudio.Play();
    }

    //
    void crop_rotantion_au_method()
    {
        myAudio.clip = crop_rotantion_au;
        myAudio.Play();
    }

    //

    void when_the_au_method()
    {
        myAudio.clip = when_the_au;
        myAudio.Play();
    }

    //

    void you_will_au_method()
    {
        myAudio.clip = you_must_au;
        myAudio.Play();
    }

    //

    void thes_planting_au_method()
    {
        myAudio.clip = this_planting_au;
        myAudio.Play();
    }

    int watermelonCount = 0;
    public void CollectWaterMelons()
    {
        watermelonCount++;

        if(watermelonCount == 4)
        {
            StepComp();
        }

    }

    //
    public void PlayAnim(Animator anim)
    {
        //InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        //InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        anim.SetTrigger("Trigger");
    }

    public Transform tractorCamHolder;

    public void Tractor(Animator tracAnim)
    {
        StepComp();
        tracAnim.SetTrigger("Trigger");
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        transform.SetParent(tractorCamHolder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        StartCoroutine(DelayTractorEnd());
    }

    IEnumerator DelayTractorEnd()
    {
        yield return new WaitForSeconds(8);
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(true);
        transform.SetParent(null);
    }

    public GameObject waterField;
    public void WaterField(BoxCollider motor)
    {
        motor.enabled = false;
        waterField.SetActive(true);
        StepComp();
    }

    public GameObject waterCattle;
    public void WaterCattle(BoxCollider pipe)
    {
        pipe.enabled = false;
        //waterCattle.SetActive(true);
        StepComp();
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

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
        
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
        StepComp();
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
                StepComp();
            }
        }

    }

    public GameObject chicken;
    public GameObject mutton;
    public void Chicken(GameObject obj)
    {
        obj.SetActive(false);
        chicken.SetActive(true);
    }

    public void Mutton(GameObject obj)
    {
        obj.SetActive(false);
        mutton.SetActive(true);
    }

    public void StepComp()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }





}
