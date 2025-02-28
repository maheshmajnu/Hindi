 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_sortingofgroups_6class : MonoBehaviour
{



    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;


    public Transform waypoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypointCanvas.SetActive(true);
        waypoint.player = InventoryManager.Instance.player.transform;
        waypoint.target = target;
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
        SetWayPoint(waypoint1);
    }

    //Titles




    public GameObject title1;

    public GameObject title2;

    public GameObject title3;


    public GameObject title4;


    public GameObject title5;








    public GameObject down1;













    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject character;

    

    public GameObject fruits;

    public GameObject scooter;

    public GameObject car;


    public GameObject bus;


    public GameObject bag;

    public GameObject juiceglass;

    public GameObject ball;
















    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject person1;


    public GameObject person2;
















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






    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("cam anim", 0, targetNormalizedTime);
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

















    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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













    void _down1_MethodON()
    {
        down1.SetActive(true);
    }

    void _down1_MethodOFF()
    {
        down1.SetActive(false);
    }























    void _fruits_MethodON()
    {
        fruits.SetActive(true);
    }

    void _fruits_MethodOFF()
    {
        fruits.SetActive(false);
    }





    void _scooter_MethodON()
    {
        scooter.SetActive(true);
    }

    void _scooter_MethodOFF()
    {
        scooter.SetActive(false);
    }




    void _car_MethodON()
    {
        car.SetActive(true);
    }

    void _car_MethodOFF()
    {
        car.SetActive(false);
    }



    void _bus_MethodON()
    {
        bus.SetActive(true);
    }

    void _bus_MethodOFF()
    {
        bus.SetActive(false);
    }




    void _bag_MethodON()
    {
        bag.SetActive(true);
    }

    void _bag_MethodOFF()
    {
        bag.SetActive(false);
    }



    void _person1_MethodON()
    {
        person1.SetActive(true);
    }

    void _person1_MethodOFF()
    {
        person1.SetActive(false);
    }


    void _person2_MethodON()
    {
        person2.SetActive(true);
    }

    void _person2_MethodOFF()
    {
        person2.SetActive(false);
    }


    void _juiceglass_MethodON()
    {
        juiceglass.SetActive(true);
    }

    void _juiceglass_MethodOFF()
    {
        juiceglass.SetActive(false);
    }


    void _ball_MethodON()
    {
        ball.SetActive(true);
    }

    void _ball_MethodOFF()
    {
        ball.SetActive(false);
    }













    //Animation Play

    void _person1animMethod()
    {
        anim = person1.GetComponent<Animator>();
        anim.Play("person1");
    }



    //
    void _person2Method()
    {
        anim = person2.GetComponent<Animator>();
        anim.Play("gate person2 sliderAction");
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


    public int index = 0;
    public void SortingShapes()
    {
        index++;
        if(index == 7)
        {
            index = 0;
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }
   
    public void SortRawAndProducts(TargetController level2MiniGame)
    {
        index++;
        if(index == 5)
        {
            index = 0;
            level2MiniGame.EndMiniGame();
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public List<string> objectibe3 = new List<string>();
    public void SortStates(string state)
    {
        
        if(state == objectibe3[index])
        {
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
            index++;

            
            if (index >= objectibe3.Count)
            {
                index = 0; 
            }
        }
        else
        {
            MissionFailed();
        }
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    public void SortItems()
    {
        index++;
        Debug.Log("Placed");
        if (index == 3)
        {
            index = 0;
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
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
