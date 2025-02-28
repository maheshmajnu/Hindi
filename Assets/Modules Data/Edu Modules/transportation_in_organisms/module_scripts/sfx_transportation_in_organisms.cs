using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sfx_transportation_in_organisms : MonoBehaviour
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
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();

        miniGames[miniGameIndex].Output();
        canChoose = true;
    }

    public Camera cam;
    public LayerMask layerMask;
    public List<TargetController> miniGames = new List<TargetController>();
    public List<GameObject> questions = new List<GameObject>();
    public List<string> correctObjs = new List<string>();
    private int correctObjIndex = 0;
    private int miniGameIndex = 0;
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
                    if (correctObjIndex < correctObjs.Count &&  raycastHit.collider.gameObject.name == correctObjs[correctObjIndex])
                    {
                        correctObjIndex++;
                        miniGames[miniGameIndex].defaultEvent.Invoke();
                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }
    }

    private int ind = 0;
    public void Lev3Correct()
    {
        ind++;
        if (ind == 2)
        {
            ind = 0;
            MoveToNextMiniGame();
            TurnOffRaycast();
            StepComplete();
            return;
        }
        StepComplete();
    }

    public void TurnOnGOWithDelay(GameObject obj)
    {
        StartCoroutine(ObjectTurnOnDelay(obj));
    }

    IEnumerator ObjectTurnOnDelay(GameObject obj)
    {
        foreach (GameObject objec in questions)
        {
            objec.SetActive(false);
        }
        yield return new WaitForSeconds(2);
        obj.SetActive(true);
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

    public void PlayAnimBool(Animator anim)
    {
        anim.SetBool("Bool", true);
    }

    public void AnimBack(Animator anim)
    {
        foreach (GameObject obj in questions)
        {
            obj.SetActive(false);
        }

        anim.SetBool("Bool", false);
    }


    public void TurnOnMeshRendererLv1(MeshRenderer mr)
    {
        ind++;
        mr.enabled = true;

        if (ind == 7)
        {
            ind = 0;
            MoveToNextMiniGame();
            StepComplete();
        }
    }

    public void TurnOnMeshRendererLv2(MeshRenderer mr)
    {
        ind++;
        mr.enabled = true;

        if (ind == 6)
        {
            ind = 0;
            MoveToNextMiniGame();
            StepComplete();
        }
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
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

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject title1;


    public GameObject title2;

    public GameObject title3;

    public GameObject title4;


    public GameObject title5;


    public GameObject title6;

    public GameObject title7;

    public GameObject title8;




    public GameObject down1;
   


    public GameObject down3;

    public GameObject down4;

    public GameObject down5;

    public GameObject down6;

    public GameObject down7;

    public GameObject down8;





    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    



    public GameObject veins;

    public GameObject arteries;








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



    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject blood_veseel_animation;

    public GameObject blood_vessel_capillaries;

    public GameObject heart;

    public GameObject Venous;

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













    void _down1_MethodON()
    {
        down1.SetActive(true);
    }

    void _down1_MethodOFF()
    {
        down1.SetActive(false);
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
















    void veins_MethodON()
    {
        veins.SetActive(true);
    }

    void veins_MethodOFF()
    {
        veins.SetActive(false);
    }




    void arteries_MethodON()
    {
        arteries.SetActive(true);
    }

    void arteries_MethodOFF()
    {
       arteries.SetActive(false);
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










    //Animation Play

    void blood_veseel_animationanim_Method()
    {
        anim = blood_veseel_animation.GetComponent<Animator>();
        anim.Play("blood_vessel_animation");
    }



    void blood_vessel_capillariesanim_Method()
    {
        anim = blood_vessel_capillaries.GetComponent<Animator>();
        anim.Play("blood_vessel_capillaries_animation");
    }


    void heart_anim_Method()
    {
        anim = heart.GetComponent<Animator>();
        anim.Play("heart_animation");
    }



    



    void Venousanim_Method()
    {
        anim = Venous.GetComponent<Animator>();
        anim.Play("venous_animation");
    }













}
