using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_heat_c7 : MonoBehaviour
{
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
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]

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
    public GameObject des_1;
    public GameObject des_2;
    public GameObject des_3;
    public GameObject des_4;
    public GameObject des_5;
    public GameObject des_6;
    public GameObject des_7;
    public GameObject des_8;
    public GameObject sun;
    public GameObject aluminium_sheet;
    public GameObject boy_p1;
    public GameObject boy_p2;
    public GameObject text_1;
    public GameObject text_2;
    public GameObject text_3;
    public GameObject text_4;
    public GameObject text_5;
    public GameObject text_6;
    public GameObject text_7;
    public GameObject text_8;
    public GameObject text_9;
    public GameObject bb;
    public GameObject machine;
    public GameObject milk;
    public GameObject water;
    public GameObject temp3;
    public GameObject foh;
    public GameObject m;
    public GameObject fire;
    public GameObject paper_fire;
    public GameObject cloud;


    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject earth;
    public GameObject arrow_anim;
    public GameObject rubbing_anim;
    public GameObject car_anim;
    public GameObject re_temp_cl_temp_anim;
    public GameObject re_temp_lab_temp_anim;
    public GameObject ice_anim;
    public GameObject mag_anim;


    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip h_1;
    public AudioClip h_2;
    public AudioClip soh_1;
    public AudioClip soh_2;
    public AudioClip soh_3;
    public AudioClip soh_4;
    public AudioClip soh_5;
    public AudioClip soh_6;
    public AudioClip fohe_1;
    public AudioClip fohe_2;
    public AudioClip dohf_1;
    public AudioClip dohf_2;
    public AudioClip temp_1;
    public AudioClip temp_2;
    public AudioClip temp_3;
    public AudioClip db_hat_1;
    public AudioClip db_hat_2;
    public AudioClip cl_temp_1;
    public AudioClip cl_temp_2;
    public AudioClip re__temp_cl_temp_1;
    public AudioClip re__temp_cl_temp_2;
    public AudioClip lab_temp_1;
    public AudioClip lab_temp_2;
    public AudioClip re__temp_lab_temp_1;
    public AudioClip re__temp_lab_temp_2;














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

  


    //
    void _Goto_menuMethodON()
    {
        Debug.Log("Text: " );
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }
    //

    //
    void _title_1_MethodON()
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
    void _title_6_MethodON()
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
    void _des_1_MethodON()
    {
        des_1.SetActive(true);
    }
    //
    void _des_2_MethodON()
    {
        des_2.SetActive(true);
    }
    //
    void _des_3_MethodON()
    {
        des_3.SetActive(true);
    }
    //
    void _des_4_MethodON()
    {
        des_4.SetActive(true);
    }
    //
    void _des_5_MethodON()
    {
        des_5.SetActive(true);
    }
    void _des_6_MethodON()
    {
        des_6.SetActive(true);
    }
    void _des_7_MethodON()
    {
        des_7.SetActive(true);
    }
    //
    void sun_MethodON()
    {
        sun.SetActive(true);
    }

    void sun_MethodOFF()
    {
        sun.SetActive(false);
    }
    //
    void arrow_anim_MethodON()
    {
        arrow_anim.SetActive(true);
    }

    void arrow_anim_MethodOFF()
    {
        arrow_anim.SetActive(false);
    }
    //    
    void aluminium_sheet_MethodON()
    {
        aluminium_sheet.SetActive(true);
    }

    void aluminium_sheet_MethodOFF()
    {
        aluminium_sheet.SetActive(false);
    }
    //  
    void boy_p1_MethodON()
    {
        boy_p1.SetActive(true);
    }

    void boy_p1_MethodOFF()
    {
        boy_p1.SetActive(false);
    }
    //
    void boy_p2_MethodON()
    {
        boy_p2.SetActive(true);
    }

    void boy_p2_MethodOFF()
    {
        boy_p2.SetActive(false);
    }
    //    
    void _text_1_MethodON()
    {
        text_1.SetActive(true);
    }
    //
    void _text_2_MethodON()
    {
        text_2.SetActive(true);
    }
    //
    void _text_3_MethodON()
    {
       text_3.SetActive(true);
    }
    //
    void _text_4_MethodON()
    {
        text_4.SetActive(true);
    }
    //
    void _text_5_MethodON()
    {
        text_5.SetActive(true);
    }
    //
    void _text_6_MethodON()
    {
        text_6.SetActive(true);
    }
    //
    void _text_7_MethodON()
    {
        text_7.SetActive(true);
    }
    //
    void _text_8_MethodON()
    {
        text_8.SetActive(true);
    }
    //
    void _text_9_MethodON()
    {
        text_9.SetActive(true);
    }
    //
    void _bb_MethodON()
    {
        bb.SetActive(true);
    }

    void _bb_MethodOFF()
    {
        bb.SetActive(false);
    }
    //
    void _re_temp_cl_temp_anim_MethodON()
    {
        re_temp_cl_temp_anim.SetActive(true);
    }

    void _re_temp_cl_temp_anim_MethodOFF()
    {
        re_temp_cl_temp_anim.SetActive(false);
    }
    //
    void _re_temp_lab_temp_anim_MethodON()
    {
        re_temp_lab_temp_anim.SetActive(true);
    }

    void _re_temp_lab_temp_anim_MethodOFF()
    {
        re_temp_lab_temp_anim.SetActive(false);
    }
    //
    void _machine_MethodON()
    {
        machine.SetActive(true);
    }

    void _machine_MethodOFF()
    {
        machine.SetActive(false);
    }
    //
    void _wm_MethodON()
    {
        milk.SetActive(true);
        water.SetActive(true);
    }

    void _wm_MethodOFF()
    {
        milk.SetActive(false);
        water.SetActive(false);
    }
    //
    void _temp3_MethodON()
    {
        temp3.SetActive(true);
       
    }

    void _temp3_MethodOFF()
    {
        temp3.SetActive(false);
        
    }
    //
    void _foh_MethodON()
    {
        foh.SetActive(true);

    }

    void _foh_MethodOFF()
    {
        foh.SetActive(false);

    }
    //
    void _m_MethodON()
    {
        m.SetActive(true);

    }

    void _m_MethodOFF()
    {
        m.SetActive(false);

    }
    //
    void _fire_MethodON()
    {
        fire.SetActive(true);

    }

    void _fire_MethodOFF()
    {
        fire.SetActive(false);

    }
    //
    void _paper_fire_MethodON()
    {
        paper_fire.SetActive(true);

    }

    void _paper_fire_MethodOFF()
    {
        paper_fire.SetActive(false);

    }
    //
    void _cloud_MethodON()
    {
        cloud.SetActive(true);

    }

    void _cloud_MethodOFF()
    {
        cloud.SetActive(false);

    }
    //




    //
    void _earth_Animmethod()
    {

        anim = earth.GetComponent<Animator>();
        anim.Play("earth_anim");
    }
    //
    void _arrow_anim_Animmethod()
    {

        anim = arrow_anim.GetComponent<Animator>();
        anim.Play("arrow_anim");
    }
    //
    void _rubbing_anim_Animmethod()
    {

        anim = rubbing_anim.GetComponent<Animator>();
        anim.Play("rubbing_anim");
    }
    //
    void _car_anim_Animmethod()
    {

        anim = car_anim.GetComponent<Animator>();
        anim.Play("car_anim");
    }
    //
    void _re_temp_cl_temp_anim_Animmethod()
    {

        anim = re_temp_cl_temp_anim.GetComponent<Animator>();
        anim.Play("re_temp_cl_temp_2_anim");
    }
    //
    void _re_temp_lab_temp_anim_Animmethod()
    {

        anim = re_temp_lab_temp_anim.GetComponent<Animator>();
        anim.Play("re_temp_lab_temp_anim");
    }
    //
    void _ice_anim_Animmethod()
    {

        anim = ice_anim.GetComponent<Animator>();
        anim.Play("ice_anim");
    }
    //
    void _mag_anim_anim_Animmethod()
    {

        anim = mag_anim.GetComponent<Animator>();
        anim.Play("mag_anim");
    }
    //



    //
    void _h_1_audioMethod()

    {
        myAudio.clip = h_1;
        myAudio.Play();
    }
    //
    void _h_2_audioMethod()

    {
        myAudio.clip = h_2;
        myAudio.Play();
    }
    //
    void _soh_1_audioMethod()

    {
        myAudio.clip = soh_1;
        myAudio.Play();
    }
    //
    void _soh_2_audioMethod()

    {
        myAudio.clip = soh_2;
        myAudio.Play();
    }
    //
    void _soh_3_audioMethod()

    {
        myAudio.clip = soh_3;
        myAudio.Play();
    }
    //
    void _soh_4_audioMethod()

    {
        myAudio.clip = soh_4;
        myAudio.Play();
    }
    //
    void _soh_5_audioMethod()

    {
        myAudio.clip = soh_5;
        myAudio.Play();
    }
    //
    void _soh_6_audioMethod()

    {
        myAudio.clip = soh_6;
        myAudio.Play();
    }
    //
    void _fohe_1_audioMethod()

    {
        myAudio.clip = fohe_1;
        myAudio.Play();
    }
    //
    void _fohe_2_audioMethod()

    {
        myAudio.clip = fohe_2;
        myAudio.Play();
    }
    //
    void _dohf_1_audioMethod()

    {
        myAudio.clip = dohf_1;
        myAudio.Play();
    }
    //
    void _dohf_2_audioMethod()

    {
        myAudio.clip = dohf_2;
        myAudio.Play();
    }
    //
    void _temp_1_audioMethod()

    {
        myAudio.clip = temp_1;
        myAudio.Play();
    }
    //
    void _temp_2_audioMethod()

    {
        myAudio.clip = temp_2;
        myAudio.Play();
    }
    //
    void _temp_3_audioMethod()

    {
        myAudio.clip = temp_3;
        myAudio.Play();
    }
    //
    void _db_hat_1_audioMethod()

    {
        myAudio.clip = db_hat_1;
        myAudio.Play();
    }
    //
    void _db_hat_2_audioMethod()

    {
        myAudio.clip = db_hat_2;
        myAudio.Play();
    }
    //
    void _cl_temp_1_audioMethod()

    {
        myAudio.clip = cl_temp_1;
        myAudio.Play();
    }
    //
    void _cl_temp_2_audioMethod()

    {
        myAudio.clip = cl_temp_2;
        myAudio.Play();
    }
    //
    void _re_temp_cl_temp_1_audioMethod()

    {
        myAudio.clip = re__temp_cl_temp_1;
        myAudio.Play();
    }
    //
    void _re_temp_cl_temp_2_audioMethod()

    {
        myAudio.clip = re__temp_cl_temp_2;
        myAudio.Play();
    }
    //
    void _lab_temp_1_audioMethod()

    {
        myAudio.clip = lab_temp_1;
        myAudio.Play();
    }
    //
    void _lab_temp_2_audioMethod()

    {
        myAudio.clip = lab_temp_2;
        myAudio.Play();
    }
    //
    void _re_temp_lab_temp_1_audioMethod()

    {
        myAudio.clip = re__temp_lab_temp_1;
        myAudio.Play();
    }
    //
    void _re_temp_lab_temp_2_audioMethod()

    {
        myAudio.clip = re__temp_lab_temp_2;
        myAudio.Play();
    }

    private bool collectedGlove;
    private bool collectedSolar;
    private bool collectedLever;

    public void CollectedGlove()
    {
        collectedGlove = true;
    }

    public GameObject lever;
    public void CollectedLever()
    {
        if (!collectedGlove)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
        }
        else
        {
            lever.SetActive(true);
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            collectedLever = true;
        }
    }

    public void CoillectedSolar()
    {
        collectedSolar = true;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void OpenDoor(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }


    public void  StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Collider leverCol;
    public void OpenDoor1(Animator anim)
    {
        if(collectedLever)
        {
            anim.SetTrigger("Trigger");
            leverCol.enabled = false;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
        else
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
        }
    }

    public void SetUpSolarPanel(GameObject setUpPanel)
    {
        setUpPanel.SetActive(true);
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }
}
