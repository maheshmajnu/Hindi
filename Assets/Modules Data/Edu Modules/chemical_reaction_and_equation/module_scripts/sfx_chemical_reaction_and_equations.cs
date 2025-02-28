using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_chemical_reaction_and_equations : MonoBehaviour
{


    //Titles

    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

    public GameObject checkpointManager;
    private static bool isSceneReloaded = false;

    public ObjectiveController objectiveController;
    public GameObject findplayer;
    public MenuSystem menu;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("New Animation", 0, targetNormalizedTime);
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
            //case 1: Level6(); break;
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

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }


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





    public GameObject down1;


    public GameObject down2;


    public GameObject down3;


    public GameObject down4;


    public GameObject down5;

    public GameObject down6;

    public GameObject down7;


    public GameObject down8;

    public GameObject down9;




























    // Exp - Audio
    [Header("Audio files")]


    public AudioSource myAudio;

    public AudioClip para1;

    public AudioClip para2;

    public AudioClip para3;

    public AudioClip para4;

    public AudioClip para5;

    public AudioClip para6;

    public AudioClip para7;

    public AudioClip para8;

    public AudioClip para9;

    public AudioClip para10;

    public AudioClip para11;

    public AudioClip para12;

    public AudioClip para13;

    public AudioClip para14;

    public AudioClip para15;

    public AudioClip para16;

    public AudioClip para17;

    public AudioClip para18;

    public AudioClip para19;

    public AudioClip para20;

    public AudioClip para21;

    public AudioClip para22;

    public AudioClip para23;

    public AudioClip para24;

    public AudioClip para25;

    public AudioClip para26;

    public AudioClip para27;

    public AudioClip para28;

    public AudioClip para29;

    public AudioClip para30;

    public AudioClip para31;

    public AudioClip para32;

    public AudioClip para33;

    public AudioClip para34;

    public AudioClip para35;

    public AudioClip para36;

    public AudioClip para37;

    public AudioClip para38;



    public AudioClip para40;

    public AudioClip para41;

    public AudioClip para42;

    public AudioClip para43;

    public AudioClip para44;

    public AudioClip para45;

    public AudioClip para46;

    public AudioClip para47;

    public AudioClip para48;

    public AudioClip para49;

    public AudioClip para50;

    public AudioClip para51;

    public AudioClip para52;

    public AudioClip para53;

    public AudioClip para54;

    public AudioClip para55;

    public AudioClip para56;

    public AudioClip para57;

    public AudioClip para58;

    public AudioClip para59;

    public AudioClip para60;

    public AudioClip para61;

    public AudioClip para62;

    public AudioClip para63;

    public AudioClip para64;

    public AudioClip para65;

    public AudioClip para66;

    public AudioClip para67;

    public AudioClip para68;

    public AudioClip para69;

    public AudioClip para70;

    public AudioClip para71;

    public AudioClip para72;

    public AudioClip para73;

    public AudioClip para74;

    public AudioClip para75;

    public AudioClip para76;



















    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject character;



    public GameObject zinc_granules_experiment;


    public GameObject boxes;


    public GameObject fe_line;


    public GameObject h_line;

    public GameObject o_line;


    public GameObject reactants;


    public GameObject product;


    public GameObject zncl_formula;

    public GameObject tin;

    public GameObject ChipsPizza;

    public GameObject Lhs;

    public GameObject Rhs;

    public GameObject coff;

    public GameObject hcoff;

    public GameObject fcoff;

    public GameObject factorygas_formula;

    public GameObject cc_formula;

    public GameObject l_formula;

    public GameObject displac_formula;

    public GameObject zinc_experiment;


    public GameObject reduction_experiment;

    public GameObject oxidadtion_experiment;
    public GameObject dou_formula;

    public GameObject oxi_formula;
    public GameObject reduction_formula;



    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject magnesium_ribbon_experimnet;


    public GameObject potas_lead_experiment;


    public GameObject nc_experiment;

    public GameObject Dis_experi;


    public GameObject cal_experiment;

    public GameObject xon_experiment;



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

    void para1_method()
    {
        myAudio.clip = para1;
        myAudio.Play();
    }

    //

    void para2_method()
    {
        myAudio.clip = para2;
        myAudio.Play();
    }

    //

    void para3_method()
    {
        myAudio.clip = para3;
        myAudio.Play();
    }


    //

    void para4_method()
    {
        myAudio.clip = para4;
        myAudio.Play();
    }


    //

    void para5_method()
    {
        myAudio.clip = para5;
        myAudio.Play();
    }



    //

    void para6_method()
    {
        myAudio.clip = para6;
        myAudio.Play();
    }





    //

    void para7_method()
    {
        myAudio.clip = para7;
        myAudio.Play();
    }


    //

    void para8_method()
    {
        myAudio.clip = para8;
        myAudio.Play();
    }



    //

    void para9_method()
    {
        myAudio.clip = para9;
        myAudio.Play();
    }



    //

    void para10_method()
    {
        myAudio.clip = para10;
        myAudio.Play();
    }


    //

    void para11_method()
    {
        myAudio.clip = para11;
        myAudio.Play();
    }


    //

    void para12_method()
    {
        myAudio.clip = para12;
        myAudio.Play();
    }


    //

    void para13_method()
    {
        myAudio.clip = para13;
        myAudio.Play();
    }


    //

    void para14_method()
    {
        myAudio.clip = para14;
        myAudio.Play();
    }


    //

    void para15_method()
    {
        myAudio.clip = para15;
        myAudio.Play();
    }



    //

    void para16_method()
    {
        myAudio.clip = para16;
        myAudio.Play();
    }



    //

    void para17_method()
    {
        myAudio.clip = para17;
        myAudio.Play();
    }




    //

    void para18_method()
    {
        myAudio.clip = para18;
        myAudio.Play();
    }





    //

    void para19_method()
    {
        myAudio.clip = para19;
        myAudio.Play();
    }



    //

    void para20_method()
    {
        myAudio.clip = para20;
        myAudio.Play();
    }



    //

    void para21_method()
    {
        myAudio.clip = para21;
        myAudio.Play();
    }


    //

    void para22_method()
    {
        myAudio.clip = para22;
        myAudio.Play();
    }



    //

    void para23_method()
    {
        myAudio.clip = para23;
        myAudio.Play();
    }



    //

    void para24_method()
    {
        myAudio.clip = para24;
        myAudio.Play();
    }



    //

    void para25_method()
    {
        myAudio.clip = para25;
        myAudio.Play();
    }




    //

    void para26_method()
    {
        myAudio.clip = para26;
        myAudio.Play();
    }




    //

    void para27_method()
    {
        myAudio.clip = para27;
        myAudio.Play();
    }





    //

    void para28_method()
    {
        myAudio.clip = para28;
        myAudio.Play();
    }


    //

    void para29_method()
    {
        myAudio.clip = para29;
        myAudio.Play();
    }



    //

    void para30_method()
    {
        myAudio.clip = para30;
        myAudio.Play();
    }



    //

    void para31_method()
    {
        myAudio.clip = para31;
        myAudio.Play();
    }




    //

    void para32_method()
    {
        myAudio.clip = para32;
        myAudio.Play();
    }



    //

    void para33_method()
    {
        myAudio.clip = para33;
        myAudio.Play();
    }


    //

    void para34_method()
    {
        myAudio.clip = para34;
        myAudio.Play();
    }




    //Audio play

    void para35_method()
    {
        myAudio.clip = para35;
        myAudio.Play();
    }

    //

    void para36_method()
    {
        myAudio.clip = para36;
        myAudio.Play();
    }

    //

    void para37_method()
    {
        myAudio.clip = para37;
        myAudio.Play();
    }


    //

    void para38_method()
    {
        myAudio.clip = para38;
        myAudio.Play();
    }





    //

    void para40_method()
    {
        myAudio.clip = para40;
        myAudio.Play();
    }





    //

    void para41_method()
    {
        myAudio.clip = para41;
        myAudio.Play();
    }


    //

    void para42_method()
    {
        myAudio.clip = para42;
        myAudio.Play();
    }



    //

    void para43_method()
    {
        myAudio.clip = para43;
        myAudio.Play();
    }



    //

    void para44_method()
    {
        myAudio.clip = para44;
        myAudio.Play();
    }


    //

    void para45_method()
    {
        myAudio.clip = para45;
        myAudio.Play();
    }


    //

    void para46_method()
    {
        myAudio.clip = para46;
        myAudio.Play();
    }


    //

    void para47_method()
    {
        myAudio.clip = para47;
        myAudio.Play();
    }


    //

    void para48_method()
    {
        myAudio.clip = para48;
        myAudio.Play();
    }


    //

    void para49_method()
    {
        myAudio.clip = para49;
        myAudio.Play();
    }



    //

    void para50_method()
    {
        myAudio.clip = para50;
        myAudio.Play();
    }



    //

    void para51_method()
    {
        myAudio.clip = para51;
        myAudio.Play();
    }




    //

    void para52_method()
    {
        myAudio.clip = para52;
        myAudio.Play();
    }





    //

    void para53_method()
    {
        myAudio.clip = para53;
        myAudio.Play();
    }



    //

    void para54_method()
    {
        myAudio.clip = para54;
        myAudio.Play();
    }



    //

    void para55_method()
    {
        myAudio.clip = para55;
        myAudio.Play();
    }


    //

    void para56_method()
    {
        myAudio.clip = para56;
        myAudio.Play();
    }



    //

    void para57_method()
    {
        myAudio.clip = para57;
        myAudio.Play();
    }



    //

    void para58_method()
    {
        myAudio.clip = para58;
        myAudio.Play();
    }



    //

    void para59_method()
    {
        myAudio.clip = para59;
        myAudio.Play();
    }




    //

    void para60_method()
    {
        myAudio.clip = para60;
        myAudio.Play();
    }




    //

    void para61_method()
    {
        myAudio.clip = para61;
        myAudio.Play();
    }





    //

    void para62_method()
    {
        myAudio.clip = para62;
        myAudio.Play();
    }


    //

    void para63_method()
    {
        myAudio.clip = para63;
        myAudio.Play();
    }



    //

    void para64_method()
    {
        myAudio.clip = para64;
        myAudio.Play();
    }



    //

    void para65_method()
    {
        myAudio.clip = para65;
        myAudio.Play();
    }




    //

    void para66_method()
    {
        myAudio.clip = para66;
        myAudio.Play();
    }



    //

    void para67_method()
    {
        myAudio.clip = para67;
        myAudio.Play();
    }


    //

    void para68_method()
    {
        myAudio.clip = para68;
        myAudio.Play();
    }



    //

    void para69_method()
    {
        myAudio.clip = para69;
        myAudio.Play();
    }





    //

    void para70_method()
    {
        myAudio.clip = para70;
        myAudio.Play();
    }


    //

    void para71_method()
    {
        myAudio.clip = para71;
        myAudio.Play();
    }



    //

    void para72_method()
    {
        myAudio.clip = para72;
        myAudio.Play();
    }



    //

    void para73_method()
    {
        myAudio.clip = para73;
        myAudio.Play();
    }




    //

    void para74_method()
    {
        myAudio.clip = para74;
        myAudio.Play();
    }



    //

    void para75_method()
    {
        myAudio.clip = para75;
        myAudio.Play();
    }


    //

    void para76_method()
    {
        myAudio.clip = para76;
        myAudio.Play();
    }




























    void _zinc_granules_experiment_MethodON()
    {
        zinc_granules_experiment.SetActive(true);
    }

    void _zinc_granules_experiment_MethodOFF()
    {
        zinc_granules_experiment.SetActive(false);
    }










    void _reduction_experiment_MethodON()
    {
        reduction_experiment.SetActive(true);
    }

    void _reduction_experiment_MethodOFF()
    {
        reduction_experiment.SetActive(false);
    }





    void _oxidadtion_experiment_MethodON()
    {
        oxidadtion_experiment.SetActive(true);
    }

    void _oxidadtion_experiment_MethodOFF()
    {
        oxidadtion_experiment.SetActive(false);
    }




    void _boxes_MethodON()
    {
        boxes.SetActive(true);
    }

    void _boxes_MethodOFF()
    {
        boxes.SetActive(false);
    }







    void _fe_line_MethodON()
    {
        fe_line.SetActive(true);
    }

    void _fe_line_MethodOFF()
    {
        fe_line.SetActive(false);
    }







    void _h_line_MethodON()
    {
        h_line.SetActive(true);
    }

    void _h_line_MethodOFF()
    {
        h_line.SetActive(false);
    }




    void _o_line_MethodON()
    {
        o_line.SetActive(true);
    }

    void _o_line_MethodOFF()
    {
        o_line.SetActive(false);
    }




    void _reactants_MethodON()
    {
        reactants.SetActive(true);
    }

    void _reactants_MethodOFF()
    {
        reactants.SetActive(false);
    }



    void _product_MethodON()
    {
        product.SetActive(true);
    }

    void _product_MethodOFF()
    {
        product.SetActive(false);
    }


    void _zncl_formula_MethodON()
    {
        zncl_formula.SetActive(true);
    }

    void _zncl_formula_MethodOFF()
    {
        zncl_formula.SetActive(false);
    }



    void _tin_MethodON()
    {
        tin.SetActive(true);
    }

    void _tin_MethodOFF()
    {
        tin.SetActive(false);
    }


    void _ChipsPizza_MethodON()
    {
        ChipsPizza.SetActive(true);
    }

    void _ChipsPizza_MethodOFF()
    {
        ChipsPizza.SetActive(false);
    }


    void _Lhs_MethodON()
    {
        Lhs.SetActive(true);
    }

    void _Lhs_MethodOFF()
    {
        Lhs.SetActive(false);
    }



    void _Rhs_MethodON()
    {
        Rhs.SetActive(true);
    }

    void _Rhs_MethodOFF()
    {
        Rhs.SetActive(false);
    }


    void _coff_MethodON()
    {
        coff.SetActive(true);
    }

    void _coff_MethodOFF()
    {
        coff.SetActive(false);
    }




    void _hcoff_MethodON()
    {
        hcoff.SetActive(true);
    }

    void _hcoff_MethodOFF()
    {
        hcoff.SetActive(false);
    }



    void _fcoff_MethodON()
    {
        fcoff.SetActive(true);
    }

    void _fcoff_MethodOFF()
    {
        fcoff.SetActive(false);
    }



    void _factorygas_formula_MethodON()
    {
        factorygas_formula.SetActive(true);
    }

    void _factorygas_formula_MethodOFF()
    {
        factorygas_formula.SetActive(false);
    }


    void _cc_formula_MethodON()
    {
        cc_formula.SetActive(true);
    }

    void _cc_formula_MethodOFF()
    {
        cc_formula.SetActive(false);
    }



    void _l_formula_MethodON()
    {
        l_formula.SetActive(true);
    }

    void _l_formula_MethodOFF()
    {
        l_formula.SetActive(false);
    }




    void _displac_formula_MethodON()
    {
        displac_formula.SetActive(true);
    }

    void _displac_formula_MethodOFF()
    {
        displac_formula.SetActive(false);
    }




    void _zinc_experiment_MethodON()
    {
        zinc_experiment.SetActive(true);
    }

    void _zinc_experiment_MethodOFF()
    {
        zinc_experiment.SetActive(false);
    }



    void _dou_formula_MethodON()
    {
        dou_formula.SetActive(true);
    }

    void _dou_formula_MethodOFF()
    {
        dou_formula.SetActive(false);
    }


    void _oxi_formula_MethodON()
    {
        oxi_formula.SetActive(true);
    }

    void _oxi_formula_MethodOFF()
    {
        oxi_formula.SetActive(false);
    }

    void reduction_formula_MethodON()
    {
        reduction_formula.SetActive(true);
    }

    void reduction_formula_MethodOFF()
    {
        reduction_formula.SetActive(false);
    }






    //Animation Play

    void magnesium_ribbon_experimnet_anim_Method()
    {
        anim = magnesium_ribbon_experimnet.GetComponent<Animator>();
        anim.Play("magnesium_ribbon_animation");


    }




    void potas_lead_experiment_anim_Method()
    {
        anim = potas_lead_experiment.GetComponent<Animator>();
        anim.Play("potas_lead_animation");

    }

    void nc_experiment_anim_Method()
    {
        anim = nc_experiment.GetComponent<Animator>();
        anim.Play("zinc_granules_animation");

    }

    void Dis_experi_anim_Method()
    {
        anim = Dis_experi.GetComponent<Animator>();
        anim.Play("Displacement reaction_animation");

    }


    void cal_experiment_anim_Method()
    {
        anim = cal_experiment.GetComponent<Animator>();
        anim.Play("cal_animation ");

    }
    void xon_experiment_anim_Method()
    {
        anim = xon_experiment.GetComponent<Animator>();
        anim.Play("xon_animation ");

    }

    private int index;

    public void OpenDoor()
    {
        index++;
        
        if(index == 8)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }
}
















