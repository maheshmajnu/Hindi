using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_transpiration_in_plants : MonoBehaviour
{


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

    public Camera cam;
    public LayerMask layerMask;
    private bool canChoose = true;
    private void Update()
    {
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
                        StepComplete();
                        //TargetController miniGame = raycastHit.collider.gameObject.GetComponent<TargetController>();
                        //miniGame.EndMiniGame();
                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }
    }

    public void CanChooseTrue()
    {
        canChoose = true;
    }

    private int pipeCount = 8;
    private int currentPipeCount = 0;
    public GameObject timeCrystal;

    public void CheckRotatblePipes()
    {
        currentPipeCount++;
        Debug.Log("PIPE ROTATED " + currentPipeCount);
        if (currentPipeCount == pipeCount)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            timeCrystal.SetActive(true);
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

    private BoxCollider doorCol;
    public void ReleaseO2()
    {
        MissionFailed();
    }

    public void CrystalObjUse(Animator anim)
    {
        anim.SetTrigger("Grow");
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    private int ind = 0;
    public GameObject root;
    public void RootsDrop(GameObject obj)
    {
        obj.SetActive(false);
        ind++;
        root.gameObject.tag = "Finish";
        if(ind == 4)
        {
            StepComplete();
            canChoose = true;
        }
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    public void MiniGameStart()
    {
        InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().FadeOut();
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
        canChoose = false;
    }

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void OnlyStepComp()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }



    //Titles




    public GameObject title1;


    public GameObject title2;

    public GameObject title3;

    public GameObject title4;


    public GameObject title5;





    public GameObject down1;

    public GameObject down2;

    public GameObject down3;

    public GameObject down4;

    public GameObject down5;









    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject character;



    public GameObject planking_kid;


    public GameObject fruits1;



    public GameObject gardener_female;


    public GameObject watering_female_with_watercan;







































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
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject planking_kid1;


    public GameObject gardener_female1;


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



















    void _planking_kid1_MethodON()
    {
        planking_kid1.SetActive(true);
    }

    void _planking_kid1_MethodOFF()
    {
        planking_kid1.SetActive(false);
    }






    void _fruits1_MethodON()
    {
        fruits1.SetActive(true);
    }

    void _fruits1_MethodOFF()
    {
        fruits1.SetActive(false);
    }





    void _gardener_female1_MethodON()
    {
        gardener_female1.SetActive(true);
    }

    void _gardener_female1_MethodOFF()
    {
        gardener_female1.SetActive(false);
    }





    void _watering_female_with_wateringcan_MethodON()
    {
        watering_female_with_watercan.SetActive(true);
    }

    void _watering_female_with_wateringcan_MethodOFF()
    {
        watering_female_with_watercan.SetActive(false);
    }






























    //Animation Play

    void _planking_kidanim_Method()
    {
        anim = planking_kid.GetComponent<Animator>();
        anim.Play("planking_kid");
    }





    void _gardener_female1anim_Method()
    {
        anim = gardener_female1.GetComponent<Animator>();
        anim.Play("gardener_female_animation");
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





























































}
