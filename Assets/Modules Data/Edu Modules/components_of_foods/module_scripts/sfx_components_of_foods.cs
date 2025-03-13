using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_components_of_foods : MonoBehaviour
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



    public GameObject down1;

    public GameObject down2;

    public GameObject down3;

    public GameObject down4;

    public GameObject down5;

    public GameObject down6;

    public GameObject down7;

    public GameObject down8;

    public GameObject down9;

    public GameObject down10;

    public GameObject down11;

    public GameObject down12;

    public GameObject down13;






    // ON - OFF gameobjects
    [Header("Explanation Assets")]




    public GameObject appam;

    public GameObject fish;


    public GameObject chapathi;


    public GameObject daal;

    public GameObject oil_tissue;

    public GameObject paper;

    public GameObject sambar;

    public GameObject dropper;

    public GameObject boy;


    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("New Animation", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }
    }

    private bool isMuted = false;

    public void ToggleAudio()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
        Debug.Log("Audio Muted: " + isMuted);
    }
    public void _Jump_To1(float value)
    {
        ToggleAudio();
        RestartSceneWithKeyframe(value);
    }

    private void RestartSceneWithKeyframe(float normalizedTime)
    {
        targetNormalizedTime = normalizedTime; // Store the keyframe to jump to
                                               //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(1);
    }



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

    public AudioClip para77;

    public AudioClip para78;

    public AudioClip para79;

    public AudioClip para80;

    public AudioClip para81;

    public AudioClip para82;

    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject test_tube;

    public GameObject fat_tissue;


    public GameObject blood;


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



    void _down10_MethodON()
    {
        down10.SetActive(true);
    }

    void _down10_MethodOFF()
    {
        down10.SetActive(false);
    }




    void _down11_MethodON()
    {
        down11.SetActive(true);
    }

    void _down11_MethodOFF()
    {
        down11.SetActive(false);
    }




    void _down12_MethodON()
    {
        down12.SetActive(true);
    }

    void _down12_MethodOFF()
    {
        down12.SetActive(false);
    }



    void _down13_MethodON()
    {
        down13.SetActive(true);
    }

    void _down13_MethodOFF()
    {
        down13.SetActive(false);
    }













    void appam_MethodON()
    {
        appam.SetActive(true);
    }

    void appam_MethodOFF()
    {
        appam.SetActive(false);
    }




    void fish_MethodON()
    {
        fish.SetActive(true);
    }

    void fish_MethodOFF()
    {
        fish.SetActive(false);
    }




    void chapathi_MethodON()
    {
        chapathi.SetActive(true);
    }

    void chapathi_MethodOFF()
    {
        chapathi.SetActive(false);
    }



    void daal_MethodON()
    {
        daal.SetActive(true);
    }

    void daal_MethodOFF()
    {
        daal.SetActive(false);
    }



    void oil_tissue_MethodON()
    {
        oil_tissue.SetActive(true);
    }

    void oil_tissue_MethodOFF()
    {
        oil_tissue.SetActive(false);
    }



    void paper_MethodON()
    {
        paper.SetActive(true);
    }

    void paper_MethodOFF()
    {
        paper.SetActive(false);
    }



    void sambar_MethodON()
    {
        sambar.SetActive(true);
    }

    void sambar_MethodOFF()
    {
        sambar.SetActive(false);
    }


    void dropper_MethodON()
    {
        dropper.SetActive(true);
    }

    void dropper_MethodOFF()
    {
        dropper.SetActive(false);
    }



    void boy_MethodON()
    {
        boy.SetActive(true);
    }

    void boy_MethodOFF()
    {
        boy.SetActive(false);
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



    //

    void para77_method()
    {
        myAudio.clip = para77;
        myAudio.Play();
    }



    //

    void para78_method()
    {
        myAudio.clip = para78;
        myAudio.Play();
    }



    //

    void para79_method()
    {
        myAudio.clip = para79;
        myAudio.Play();
    }



    //

    void para80_method()
    {
        myAudio.clip = para80;
        myAudio.Play();
    }


    //

    void para81_method()
    {
        myAudio.clip = para81;
        myAudio.Play();
    }



    //

    void para82_method()
    {
        myAudio.clip = para82;
        myAudio.Play();
    }


































    //Animation Play

    void test_tube_anim_Method()
    {
        anim = test_tube.GetComponent<Animator>();
        anim.Play("test_tube_animation");
    }




    void fat_tissue_anim_Method()
    {
        anim = fat_tissue.GetComponent<Animator>();
        anim.Play("fat_animation");
    }



    void blood_anim_Method()
    {
        anim = blood.GetComponent<Animator>();
        anim.Play("blood_veseel_animation");
    }

    public Animator carAnim;
    public List<string> delivirables = new List<string>();
    public List<string> testable = new List<string>();
    private int currentDelIndx = 0;
    private bool exp1Completed = false;
    private bool exp2Completed = false;
    private void Start()
    {
        CarAnimation(carAnim);
    }

    public void CarAnimation(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public Collider opt1BoxLevel1;
    public void ValidateDelivarable(ItemObject item)
    {
        if (item.item.itemName == delivirables[currentDelIndx])
        {
            currentDelIndx++;
            if (currentDelIndx == delivirables.Count) currentDelIndx = 0;
            CarAnimation(carAnim);
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            StartCoroutine(TurnOnColider());
        }
    }

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    IEnumerator TurnOnColider()
    {
        yield return new WaitForSeconds(1);
        opt1BoxLevel1.enabled = true;
    }

    public GameObject potatoMiniGame;
    private TargetController currentMiniGame;
    public void PotatoMiniGame(TargetController controller)
    {
        currentMiniGame = controller;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        controller.Output();
        potatoMiniGame.SetActive(true);
        miniGameStarted = true;
    }
    public GameObject meatMinGam;
    public void MeatMiniGame(TargetController controller)
    {
        currentMiniGame = controller;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        controller.Output();
        potatoMiniGame.SetActive(true);
        miniGameStarted = true;
        meatMiniGameStarted = true;
    }
    public GameObject cheeseMinGam;
    public void CheeseMiniGame(TargetController controller)
    {
        currentMiniGame = controller;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        controller.Output();
        cheeseMinGam.SetActive(true);
        meatMiniGameStarted = false;
        miniGameStarted = true;
    }

    public List<GameObject> tasks = new List<GameObject>();
    public void MiniGameExit(GameObject nextTask)
    {
        StartCoroutine(MiniGameEndDelay(nextTask));
        meatMiniGameStarted = false;
        miniGameStarted = false;
    }

    IEnumerator MiniGameEndDelay(GameObject nextTask)
    {
        yield return new WaitForSeconds(4);
        currentMiniGame.EndMiniGame();
        foreach (GameObject task in tasks)
        {
            task.SetActive(false);
        }
        nextTask.SetActive(true);        
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }


    public Camera cam;
    bool miniGameStarted;
    bool meatMiniGameStarted;
    public LayerMask layerMask;
    public List<string> items;
    public int currentIndex;
    private void Update()
    {
        if (miniGameStarted)
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
                        if (raycastHit.collider.gameObject.name == items[currentIndex])
                        {
                            currentIndex++;
                            if(!meatMiniGameStarted)
                            {
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                            }
                            else
                            {
                                ItemObject itmObj = raycastHit.collider.gameObject.GetComponent<ItemObject>();
                                itmObj.optionOneEvent.Invoke();
                            }

                            if(currentIndex == 13)
                            {
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                                meatMiniGameStarted = false;
                            }

                            if (currentIndex == items.Count)
                            {
                                miniGameStarted = false;
                                currentIndex = 0;
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

    public void OpenDoor(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void DoctorMiniGameStart(GameObject patient1)
    {
        patient1.SetActive(true);
        miniGameStarted = true;
        meatMiniGameStarted = true;
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        miniGameStarted = false;
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
