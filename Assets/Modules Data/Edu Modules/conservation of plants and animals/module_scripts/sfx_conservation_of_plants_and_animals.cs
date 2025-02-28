using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_conservation_of_plants_and_animals : MonoBehaviour
{


    //Titles
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
        InitializeFromCheckpoint();
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



    public GameObject down1;





    // ON - OFF gameobjects
    [Header("Explanation Assets")]




    public GameObject house;

    public GameObject chinkara;









    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject man1;

    public GameObject man2;

    public GameObject birds;

    public GameObject global_warming;




















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

    public AudioClip para39;

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



    void _down1_MethodON()
    {
        down1.SetActive(true);
    }

    void _down1_MethodOFF()
    {
        down1.SetActive(false);
    }








    void house_MethodON()
    {
        house.SetActive(true);
    }

    void house_MethodOFF()
    {
        house.SetActive(false);
    }




    void chinkara_MethodON()
    {
        chinkara.SetActive(true);
    }

    void chinkara_MethodOFF()
    {
        chinkara.SetActive(false);
    }

































    void _man1_MethodON()
    {
        man1.SetActive(true);
    }

    void _man1_MethodOFF()
    {
        man1.SetActive(false);
    }




    void _man2_MethodON()
    {
        man2.SetActive(true);
    }

    void _man2_MethodOFF()
    {
        man2.SetActive(false);
    }












    //Animation Play

    void man1_anim_Method()
    {
        anim = man1.GetComponent<Animator>();
        anim.Play("man1_animation");
    }




    void man2_anim_Method()
    {
        anim = man2.GetComponent<Animator>();
        anim.Play("man2_animation");
    }


    void birds_anim_Method()
    {
        anim = birds.GetComponent<Animator>();
        anim.Play("birds_animation");
    }


    void global_warming_anim_Method()
    {
        anim = global_warming.GetComponent<Animator>();
        anim.Play("global_warming_animation");
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



    //

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

    void para39_method()
    {
        myAudio.clip = para39;
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


    public Npc lion;
    public void SetTargetToLion(Transform target)
    {
        lion.moveposition = null;
        lion.moveposition = target;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Npc bear;
    public void SetTargetToBear(Transform target)
    {
        bear.moveposition = target;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Npc croc;
    public void SetTargetToCroc(Transform target)
    {
        croc.moveposition = target;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void SetTargetAsPlayer(Npc npc)
    {
        npc.moveposition = InventoryManager.Instance.player.gameObject.transform;
    }

    public TargetController wastebinMiniGame;
    private int currentIndex = 0;
    public void DropWaste()
    {
        currentIndex++;

        if (currentIndex == 6)
        {
            wastebinMiniGame.EndMiniGame();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    private int ind = 0;
    public TargetController lv2MiniGame;
    public void Lv1MiniGame(GameObject colloider)
    {
        colloider.tag = "Finish";
        ind++;

        if (ind == 7)
        {
            ind = 0;
            lv2MiniGame.EndMiniGame();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void Lv3MiniGame()
    {
        
        ind++;

        if (ind == 3)
        {
            ind = 0;
            
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    public void TurnOnTrees(GameObject trees)
    {
        trees.SetActive(true);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void AnimalsToVehicle(Animator anim)
    {
        anim.SetTrigger("Trigger");
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
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



































}
