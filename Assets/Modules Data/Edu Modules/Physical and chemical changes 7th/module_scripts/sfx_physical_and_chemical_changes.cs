using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_physical_and_chemical_changes : MonoBehaviour
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

        //SetWayPoint(wayPoint1); 

    }

    public Camera cam;

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

    public void PlayAnimTrigg(Animator anim)
    {
        anim.SetTrigger("Trigger"); // Replace "TriggerName" with the actual name of your trigger
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    private int index = 0;
    public void Lv1()
    {
        index++;
        

        if (index == 5)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }


    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject ice_cream;
    public GameObject ice_melting;



    public GameObject title_1;
    public GameObject title_2;
    public GameObject title_3;
    public GameObject title_4;
    public GameObject title_5;
    public GameObject title_6;
    public GameObject title_7;
    public GameObject title_8;
    public GameObject title_9;
    public GameObject title_10;
    public GameObject title_11;
    public GameObject title_12;
    public GameObject title_13;
    public GameObject title_14;
    public GameObject title_15;
    public GameObject title_16;
    public GameObject des_1;
    public GameObject des_2;
    public GameObject des_3;
    public GameObject des_4;
    public GameObject des_5;


    // Exp - Animations
    [Header("Explanation anims")] 
    private Animator anim;

    public GameObject refrigerator;
    public GameObject copper_sulfate;
    public GameObject venigar;


    // Exp - Audio
    [Header("Audio files")]   
    public AudioSource myAudio;

    public AudioClip voice_1;
    public AudioClip voice_2;
    public AudioClip voice_3;
    public AudioClip voice_4;
    public AudioClip voice_5;
    public AudioClip voice_6;
    public AudioClip voice_7;
    public AudioClip voice_8;
    public AudioClip voice_9;
    public AudioClip voice_10;
    public AudioClip voice_11;
    public AudioClip voice_12;
    public AudioClip voice_13;
    public AudioClip voice_14;
    public AudioClip voice_15;
    public AudioClip voice_16;
    public AudioClip voice_17;
    public AudioClip voice_18;
    public AudioClip voice_19;
    public AudioClip voice_20;
    public AudioClip voice_21;
    public AudioClip voice_22;
    public AudioClip voice_23;
    public AudioClip voice_24;
    public AudioClip voice_25;
    public AudioClip voice_26;
    public AudioClip voice_27;
    public AudioClip voice_27_1;
    public AudioClip voice_28;
    public AudioClip voice_29;
    public AudioClip voice_30;
    public AudioClip voice_31;
    public AudioClip voice_32;
    public AudioClip voice_33;
    public AudioClip voice_34;
    public AudioClip voice_35;
    public AudioClip voice_36;
    public AudioClip voice_36_1;
    public AudioClip voice_37;
    public AudioClip voice_37_1;
    public AudioClip voice_38;
    public AudioClip voice_38_1;
    public AudioClip voice_39;
    public AudioClip voice_39_1;
    public AudioClip voice_40;
    public AudioClip voice_40_1;
    public AudioClip voice_41;














    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camera_anim", 0, targetNormalizedTime);
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

    //
    void title_1_MethodON()
    {
        title_1.SetActive(true);
    }
    //
    void title_2_MethodON()
    {
        title_2.SetActive(true);
    }
    //
    void title_3_MethodON()
    {
        title_3.SetActive(true);
    }
    //
    void title_4_MethodON()
    {
        title_4.SetActive(true);
    }
    //
    void title_5_MethodON()
    {
        title_5.SetActive(true);
    }
    //
    void title_6_MethodON()
    {
        title_6.SetActive(true);
    }
    //
    void title_7_MethodON()
    {
        title_7.SetActive(true);
    }
    //
    void title_8_MethodON()
    {
        title_8.SetActive(true);
    }
    //
    //
    void title_9_MethodON()
    {
        title_9.SetActive(true);
    }
    //
    void title_10_MethodON()
    {
        title_10.SetActive(true);
    }
    //
    void title_11_MethodON()
    {
        title_11.SetActive(true);
    }
    //
    void title_12_MethodON()
    {
        title_12.SetActive(true);
    }
    //
    void title_13_MethodON()
    {
        title_13.SetActive(true);
    }
    //
    void title_14_MethodON()
    {
        title_14.SetActive(true);
    }
    //
    void title_15_MethodON()
    {
        title_15.SetActive(true);
    }
    //
    void title_16_MethodON()
    {
        title_16.SetActive(true);
    }























    void _refrigeratorAnimmethod()
    {

        anim = refrigerator.GetComponent<Animator>();
        anim.Play("RENAME TO ANIM FILE");
    }





    void _copper_sulfateAnimmethod()
    {

        anim = copper_sulfate.GetComponent<Animator>();
        anim.Play("copper sulfate");
    }





    void _venigarAnimmethod()
    {

        anim = venigar.GetComponent<Animator>();
        anim.Play("venigar anim");
    }










    // Line on/off

    void _icecreammethod()
    {
        ice_cream.SetActive(true);
    }
    //

    void _icemeltingmethod()
    {
        ice_melting.SetActive(true);
    }
    //

    void _refrigeratormethod()
    {
        refrigerator.SetActive(true);
    }
    //






























    //Audio play

    void voice1_method()
    {
        myAudio.clip = voice_1;
        myAudio.Play();

    }
    //
    void voice2_method()
    {
        myAudio.clip = voice_2;
        myAudio.Play();

    }
    //
    void voice3_method()
    {
        myAudio.clip = voice_3;
        myAudio.Play();

    }
    //
    void voice4_method()
    {
        myAudio.clip = voice_4;
        myAudio.Play();

    }
    //
    void voice5_method()
    {
        myAudio.clip = voice_5;
        myAudio.Play();

    }
    //
    void voice6_method()
    {
        myAudio.clip = voice_6;
        myAudio.Play();

    }
    //
    void voice7_method()
    {
        myAudio.clip = voice_7;
        myAudio.Play();

    }
    //
    void voice8_method()
    {
        myAudio.clip = voice_8;
        myAudio.Play();

    }
    //
    void voice9_method()
    {
        myAudio.clip = voice_9;
        myAudio.Play();

    }
    //
    void voice10_method()
    {
        myAudio.clip = voice_10;
        myAudio.Play();

    }
    //
    void voice11_method()
    {
        myAudio.clip = voice_11;
        myAudio.Play();

    }
    //
    void voice12_method()
    {
        myAudio.clip = voice_12;
        myAudio.Play();

    }
    //

    void voice13_method()
    {
        myAudio.clip = voice_13;
        myAudio.Play();

    }
    //
    void voice14_method()
    {
        myAudio.clip = voice_14;
        myAudio.Play();

    }
    //
    void voice15_method()
    {
        myAudio.clip = voice_15;
        myAudio.Play();

    }
    //
    void voice16_method()
    {
        myAudio.clip = voice_16;
        myAudio.Play();

    }
    //
    void voice17_method()
    {
        myAudio.clip = voice_17;
        myAudio.Play();

    }
    //
    void voice18_method()
    {
        myAudio.clip = voice_18;
        myAudio.Play();

    }
    //
    void voice19_method()
    {
        myAudio.clip = voice_19;
        myAudio.Play();

    }
    //
    void voice20_method()
    {
        myAudio.clip = voice_20;
        myAudio.Play();

    }
    //
    void voice21_method()
    {
        myAudio.clip = voice_21;
        myAudio.Play();

    }
    //
    void voice22_method()
    {
        myAudio.clip = voice_22;
        myAudio.Play();

    }
    //
    void voice23_method()
    {
        myAudio.clip = voice_23;
        myAudio.Play();

    }
    //
    void voice24_method()
    {
        myAudio.clip = voice_24;
        myAudio.Play();

    }
    //
    void voice25_method()
    {
        myAudio.clip = voice_25;
        myAudio.Play();

    }
    //
    void voice26_method()
    {
        myAudio.clip = voice_26;
        myAudio.Play();

    }
    //
    void voice27_method()
    {
        myAudio.clip = voice_27;
        myAudio.Play();

    }
    //
    void voice27_1_method()
    {
        myAudio.clip = voice_27_1;
        myAudio.Play();

    }
    //
    void voice28_method()
    {
        myAudio.clip = voice_28;
        myAudio.Play();

    }
    //
    void voice29_method()
    {
        myAudio.clip = voice_29;
        myAudio.Play();

    }
    //
    void voice30_method()
    {
        myAudio.clip = voice_30;
        myAudio.Play();

    }
    //
    void voice31_method()
    {
        myAudio.clip = voice_31;
        myAudio.Play();

    }
    //
    void voice32_method()
    {
        myAudio.clip = voice_32;
        myAudio.Play();

    }
    //
    void voice33_method()
    {
        myAudio.clip = voice_33;
        myAudio.Play();

    }
    //
    void voice34_method()
    {
        myAudio.clip = voice_34;
        myAudio.Play();

    }
    //
    void voice35_method()
    {
        myAudio.clip = voice_35;
        myAudio.Play();

    }
    //
    void voice36_method()
    {
        myAudio.clip = voice_36;
        myAudio.Play();

    }
    //
    void voice36_1_method()
    {
        myAudio.clip = voice_36_1;
        myAudio.Play();

    }
    //
    void voice37_method()
    {
        myAudio.clip = voice_37;
        myAudio.Play();

    }
    //
    void voice37_1_method()
    {
        myAudio.clip = voice_37_1;
        myAudio.Play();

    }
    //
    void voice38_method()
    {
        myAudio.clip = voice_38;
        myAudio.Play();

    }
    //
    void voice38_1_method()
    {
        myAudio.clip = voice_38_1;
        myAudio.Play();

    }
    //
    void voice39_method()
    {
        myAudio.clip = voice_39;
        myAudio.Play();

    }
    //
    void voice39_1_method()
    {
        myAudio.clip = voice_39_1;
        myAudio.Play();

    }
    //
    void voice40_method()
    {
        myAudio.clip = voice_40;
        myAudio.Play();

    }
    //
    void voice40_1_method()
    {
        myAudio.clip = voice_40_1;
        myAudio.Play();

    }
    //
    void voice41_method()
    {
        myAudio.clip = voice_41;
        myAudio.Play();

    }
    //
 



















}
