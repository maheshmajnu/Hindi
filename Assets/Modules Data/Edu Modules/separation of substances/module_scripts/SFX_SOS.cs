using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_SOS : MonoBehaviour
{
    public Transform wayPoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypoint.player = InventoryManager.Instance.player.transform;
        waypointCanvas.SetActive(true);
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
        SetWayPoint(wayPoint1);
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
    public GameObject title_11;
    public GameObject title_12;
    public GameObject title_13;
    public GameObject title_14;
    public GameObject title_15;
    public GameObject Description_1;
    public GameObject Description_2;
    public GameObject Description_3;
    public GameObject Description_4;
    public GameObject Description_5;
    public GameObject Description_6;
    public GameObject Description_7;
    public GameObject Description_8;
    public GameObject Description_9;
    public GameObject Description_10;
    public GameObject Description_11;
    public GameObject Description_12;
    public GameObject middle_lines_1;
    public GameObject middle_lines_2;
    public GameObject middle_lines_3;
    public GameObject middle_lines_4;
    public GameObject tea;
    public GameObject purewater;
    public GameObject particles_in_beaker;
    public GameObject water;
    public GameObject part_1;
    public GameObject part_1_1;
    public GameObject part_2;
    public GameObject small_particles;



    // Exp - Animations
    private Animator anim;
    [Header("Explanation anims")]

    public GameObject hand_picking;
    public GameObject sieveing;
    public GameObject Winnowing; 
    public GameObject Threshing;
    public GameObject churning;
    public GameObject filtration;
    public GameObject beaker;
    public GameObject beaker_3;
    public GameObject tea_pouring;
    public GameObject ice_melting;
    public GameObject dust;
    public GameObject Stirrer;
  







    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip in_1;
    public AudioClip in_2;
    public AudioClip in_3;
    public AudioClip com_1;
    public AudioClip com_2;
    public AudioClip com_3;
    public AudioClip mos_1;
    public AudioClip hp_1;
    public AudioClip hp_2;
    public AudioClip hp_3;
    public AudioClip hp_4;
    public AudioClip cn_1;
    public AudioClip cn_2;
    public AudioClip cn_3;
    public AudioClip cn_4;
    public AudioClip cn_5;
    public AudioClip th_1;
    public AudioClip th_2;
    public AudioClip th_3;
    public AudioClip th_4;
    public AudioClip th_5;
    public AudioClip th_6;
    public AudioClip wn_1;
    public AudioClip wn_2;
    public AudioClip wn_3;
    public AudioClip wn_4;
    public AudioClip wn_5;
    public AudioClip si_1;
    public AudioClip si_2;
    public AudioClip si_3;
    public AudioClip si_4;
    public AudioClip si_5;
    public AudioClip se_dec_1;
    public AudioClip se_dec_2;
    public AudioClip se_dec_3;
    public AudioClip se_dec_4;
    public AudioClip se_dec_5;
    public AudioClip fr_1;
    public AudioClip fr_2;
    public AudioClip fr_3;
    public AudioClip fr_4;
    public AudioClip sep_1;
    public AudioClip sep_2;
    public AudioClip sep_3;
    public AudioClip sep_4;
    public AudioClip sep_5;
    public AudioClip sep_6;
    public AudioClip sep_7;
    public AudioClip sep_8;
    public AudioClip sep_9;
    public AudioClip sep_10;
    public AudioClip sep_11;
    public AudioClip sep_12;
    public AudioClip sep_13;
    public AudioClip sep_14;
    public AudioClip sep_15;
    public AudioClip sep_16;
    public AudioClip s_us_1;
    public AudioClip s_us_2;
    public AudioClip s_us_3;
    public AudioClip s_us_4;
    public AudioClip s_us_5;
    public AudioClip s_us_6;
    public AudioClip s_us_7;
    public AudioClip s_us_8;
    public AudioClip s_us_9;













    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //
    void _part_1_MethodOFF()
    {
        part_1.SetActive(false);
    }
    //
    void _part_1_1_MethodOFF()
    {
        part_1_1.SetActive(false);
    }
    //
    void _part_2_MethodON()
    {
        part_2.SetActive(true);
    }
    //
    void _title_1_MethodON()
    {
        title_1.SetActive(true);
    }

    void _title_1_MethodOFF()
    {
        title_1.SetActive(false);
    }

    //
    void _title_2_MethodON()
    {
        title_2.SetActive(true);
    }

    void _title_2_MethodOFF()
    {
        title_2.SetActive(false);
    }
    //
    void _title_3_MethodON()
    {
        title_3.SetActive(true);
    }
    void _title_3_MethodOFF()
    {
        title_3.SetActive(false);
    }
    //
    void _title_4_MethodON()
    {
        title_4.SetActive(true);
    }
    void _title_4_MethodOFF()
    {
        title_4.SetActive(false);
    }
    //
    void _title_5_MethodON()
    {
        title_5.SetActive(true);
    }
    void _title_5_MethodOff()
    {
        title_5.SetActive(false);
    }
    //
    void _title_6_MethodON()
    {
        title_6.SetActive(true);
    }
    void _title_6_MethodOFF()
    {
        title_6.SetActive(false);
    }
    //
    void _title_7_MethodON()
    {
        title_7.SetActive(true);
    }
    void _title_7_MethodOFF()
    {
        title_7.SetActive(false);
    }
    //
    void _title_8_MethodON()
    {
        title_8.SetActive(true);
    }
    void _title_8_MethodOFF()
    {
        title_8.SetActive(false);
    }
    //
    void _title_9_MethodON()
    {
        title_9.SetActive(true);
    }
    void _title_9_MethodOFF()
    {
        title_9.SetActive(false);
    }
    //
    void _title_10_MethodON()
    {
        title_10.SetActive(true);
    }
    void _title_10_MethodOFF()
    {
        title_10.SetActive(false);
    }
    //
    void _title_11_MethodON()
    {
        title_11.SetActive(true);
    }
    void _title_11_MethodOFF()
    {
        title_11.SetActive(false);
    }
    //
    void _title_12_MethodON()
    {
        title_12.SetActive(true);
    }
    void _title_12_MethodOFF()
    {
        title_12.SetActive(false);
    }
    //
    void _title_13_MethodON()
    {
        title_13.SetActive(true);
    }
    void _title_13_MethodOFF()
    {
        title_13.SetActive(false);
    }
    //
    void _title_14_MethodON()
    {
        title_14.SetActive(true);
    }
    void _title_14_MethodOFF()
    {
        title_14.SetActive(false);
    }
    //
    void _title_15_MethodON()
    {
        title_15.SetActive(true);
    }
    void _title_15_MethodOFF()
    {
        title_15.SetActive(false);
    }
    //
    void _Description_1_MethodON()
    {
        Description_1.SetActive(true);
    }

    void _Description_1_MethodOFF()
    {
        Description_1.SetActive(false);
    }



    //
    void _Description_2_MethodON()
    {
        Description_2.SetActive(true);
    }

    void _Description_2_MethodOFF()
    {
        Description_2.SetActive(false);
    }







    //
    void _Description_3_MethodON()
    {
        Description_3.SetActive(true);
    }

    void _Description_3_MethodOFF()
    {
        Description_3.SetActive(false);
    }

    //
    void _Description_4_MethodON()
    {
        Description_4.SetActive(true);
    }

    void _Description_4_MethodOFF()
    {
        Description_4.SetActive(false);
    }


    //
    void _Description_5_MethodON()
    {
        Description_5.SetActive(true);
    }

    void _Description_5_MethodOFF()
    {
        Description_5.SetActive(false);
    }



    //
    void _Description_6_MethodON()
    {
        Description_6.SetActive(true);
    }

    void _Description_6_MethodOFF()
    {
        Description_6.SetActive(false);
    }





    //
    void _Description_7_MethodON()
    {
        Description_7.SetActive(true);
    }

    void _Description_7_MethodOFF()
    {
        Description_7.SetActive(false);
    }






    //
    void _Description_8_MethodON()
    {
        Description_8.SetActive(true);
    }

    void _Description_8_MethodOFF()
    {
        Description_8.SetActive(false);
    }





    //
    void _Description_9_MethodON()
    {
        Description_9.SetActive(true);
    }

    void _Description_9_MethodOFF()
    {
        Description_9.SetActive(false);
    }



    //
    void _Description_10_MethodON()
    {
        Description_10.SetActive(true);
    }

    void _Description_10_MethodOFF()
    {
        Description_10.SetActive(false);
    }





    //
    void _Description_11_MethodON()
    {
        Description_11.SetActive(true);
    }


    void _Description_11_MethodOFF()
    {
        Description_11.SetActive(false);
    }










    //
    void _Description_12_MethodON()
    {
        Description_12.SetActive(true);
    }

    void _Description_12_MethodOFF()
    {
        Description_12.SetActive(false);
    }






    //
    void _middle_lines_1_MethodON()
    {
        middle_lines_1.SetActive(true);
    }

    void _middle_lines_1_MethodOFF()
    {
        middle_lines_1.SetActive(false);
    }



    //
    void _middle_lines_2_MethodON()
    {
        middle_lines_2.SetActive(true);
    }

    void _middle_lines_2_MethodOFF()
    {
        middle_lines_2.SetActive(false);
    }




    //
    void _middle_lines_3_MethodON()
    {
        middle_lines_3.SetActive(true);
    }

    void _middle_lines_3_MethodOFF()
    {
        middle_lines_3.SetActive(false);
    }






    //
    void _middle_lines_4_MethodON()
    {
        middle_lines_4.SetActive(true);
    }

    void _middle_lines_4_MethodOFF()
    {
        middle_lines_4.SetActive(false);
    }







    //
    void _hand_MethodON()
    {
        hand_picking.SetActive(true);

    }

    void _hand_MethodOFF()
    {
        hand_picking.SetActive(false);
    }
    //
    void _th_MethodON()
    {
        Threshing.SetActive(true);

    }

    void _th_MethodOFF()
    {
        Threshing.SetActive(false);
    }
    //
    void _si_MethodON()
    {
        sieveing.SetActive(true);

    }

    void _si_MethodOFF()
    {
        sieveing.SetActive(false);
    }
    //
    void _wn_MethodON()
    {
        Winnowing.SetActive(true);

    }

    void _wn_MethodOFF()
    {
        Winnowing.SetActive(false);
    }
    //
    void _cn_MethodON()
    {
        churning.SetActive(true);

    }

    void _cn_MethodOFF()
    {
        churning.SetActive(false);
    }
    //
    void _tea_MethodON()
    {
        tea.SetActive(true);

    }

    void _tea_MethodOFF()
    {
        tea.SetActive(false);
    }
    //
    void _tea_2_MethodON()
    {
        tea.SetActive(true);

    }

    void _tea_2_MethodOFF()
    {
        tea.SetActive(false);
    }
    //
    void _pure_water_MethodON()
    {
        purewater.SetActive(true);

    }

    void _pure_water_MethodOFF()
    {
        purewater.SetActive(false);
    }
    //
    void _water_MethodON()
    {
        water.SetActive(true);

    }

    void _water_MethodOFF()
    {
        water.SetActive(false);
    }
    //
    void _dust_MethodON()
    {
        dust.SetActive(true);

    }

    void _dust_MethodOFF()
    {
        dust.SetActive(false);
    }
    //
    void _small_particles_MethodON()
    {
        small_particles.SetActive(true);

    }

    void _small_particles_MethodOFF()
    {
        small_particles.SetActive(false);
    }
    //




    //
    void _hand_picking_1_Animmethod()
    {

        anim = hand_picking.GetComponent<Animator>();
        anim.Play("hand_picking_1");
    }
    //
    void _hand_picking_2_Animmethod()
    {

        anim = hand_picking.GetComponent<Animator>();
        anim.Play("hand_pickning_2");
    }
    //
    void _sieveing_Animmethod()
    {

        anim = sieveing.GetComponent<Animator>();
        anim.Play("sieving");
    }
    //
    void _Winnowing_Animmethod()
    {

        anim = Winnowing.GetComponent<Animator>();
        anim.Play("Winnowing_anim_");
    }
    //
    void _Threshing_Animmethod()
    {

        anim = Threshing.GetComponent<Animator>();
        anim.Play("threshing_anim");
    }
    //
    void _churning_Animmethod()
    {

        anim = Threshing.GetComponent<Animator>();
        anim.Play("churning");
    }
    //
    void _beaker_pouring_Animmethod()
    {

        anim = beaker.GetComponent<Animator>();
        anim.Play("beaker_pouring_anim");
    }
    //
    void _beaker_pouring_2_Animmethod()
    {

        anim = beaker.GetComponent<Animator>();
        anim.Play("beaker_pouring_anim_2");
    }
    //
    void _beaker_filtration_Animmethod()
    {

        anim = beaker_3.GetComponent<Animator>();
        anim.Play("beaker_filtration_anim");
    }
    //
    void _filtration_Animmethod()
    {

        anim = filtration.GetComponent<Animator>();
        anim.Play("filtration_anim");
    }
    //
    void _tea_pouring_Animmethod()
    {

        anim = tea_pouring.GetComponent<Animator>();
        anim.Play("tea_pouring_anim");
    }
    //
    void _tea_pouring_2_Animmethod()
    {

        anim = tea_pouring.GetComponent<Animator>();
        anim.Play("tea_pouring_anim");
    }
    //
    void _ice_melting_Animmethod()
    {

        anim = ice_melting.GetComponent<Animator>();
        anim.Play("ice_melting_anim");
    }
    //
    void _dust_Animmethod()
    {

        anim = dust.GetComponent<Animator>();
        anim.Play("dust_falling_anim");
    }
    //
    void _mixing_anim_Animmethod()
    {

        anim = Stirrer.GetComponent<Animator>();
        anim.Play("mixing_anim");
    }
    //



    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camera", 0, targetNormalizedTime);
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



















    void _in_1_audioMethod()
    {
        myAudio.clip = in_1;
        myAudio.Play();
    }
    //
    void _in_2_audioMethod()
    {
        myAudio.clip = in_2;
        myAudio.Play();
    }
    //
    void _in_3_audioMethod()
    {
        myAudio.clip = in_3;
        myAudio.Play();
    }
    //
    void _com_1_audioMethod()
    {
        myAudio.clip = com_1;
        myAudio.Play();
    }
    //
    void _com_2_audioMethod()
    {
        myAudio.clip = com_2;
        myAudio.Play();
    }
    //
    void com_3_audioMethod()
    {
        myAudio.clip = com_3;
        myAudio.Play();
    }
    //
    void mos_1_audioMethod()
    {
        myAudio.clip = mos_1;
        myAudio.Play();
    }
    //
    void _hp_1_audioMethod()
    {
        myAudio.clip = hp_1;
        myAudio.Play();
    }
    //
    void _hp_2_audioMethod()
    {
        myAudio.clip = hp_2;
        myAudio.Play();
    }
    //
    void _hp_3_audioMethod()
    {
        myAudio.clip = hp_3;
        myAudio.Play();
    }
    //
    void _hp_4_audioMethod()
    {
        myAudio.clip = hp_4;
        myAudio.Play();
    }
    //
    void _cn_1_audioMethod()
    {
        myAudio.clip = cn_1;
        myAudio.Play();
    }
    //
    void _cn_2_audioMethod()
    {
        myAudio.clip = cn_2;
        myAudio.Play();
    }
    //
    void _cn_3_audioMethod()
    {
        myAudio.clip = cn_3;
        myAudio.Play();
    }
    //
    void _cn_4_audioMethod()
    {
        myAudio.clip = cn_4;
        myAudio.Play();
    }
    //
    void _cn_5_audioMethod()
    {
        myAudio.clip = cn_5;
        myAudio.Play();
    }
    //
    void _th_1_audioMethod()
    {
        myAudio.clip = th_1;
        myAudio.Play();
    }
    //
    void _th_2_audioMethod()
    {
        myAudio.clip = th_2;
        myAudio.Play();
    }
    //
    void _th_3_audioMethod()
    {
        myAudio.clip = th_3;
        myAudio.Play();
    }
    //
    void _th_4_audioMethod()
    {
        myAudio.clip = th_4;
        myAudio.Play();
    }
    //
    void _th_5_audioMethod()
    {
        myAudio.clip = th_5;
        myAudio.Play();
    }
    //
    void _th_6_audioMethod()
    {
        myAudio.clip = th_6;
        myAudio.Play();
    }
    //
    void _wn_1_audioMethod()
    {
        myAudio.clip = wn_1;
        myAudio.Play();
    }
    //
    void _wn_2_audioMethod()
    {
        myAudio.clip = wn_2;
        myAudio.Play();
    }
    //
    void _wn_3_audioMethod()
    {
        myAudio.clip = wn_3;
        myAudio.Play();
    }
    //
    void _wn_4_audioMethod()
    {
        myAudio.clip = wn_4;
        myAudio.Play();
    }
    //
    void _wn_5_audioMethod()
    {
        myAudio.clip = wn_5;
        myAudio.Play();
    }
    //
    void _si_1_audioMethod()
    {
        myAudio.clip = si_1;
        myAudio.Play();
    }
    //
    void _si_2_audioMethod()
    {
        myAudio.clip = si_2;
        myAudio.Play();
    }
    //
    void _si_3_audioMethod()
    {
        myAudio.clip = si_3;
        myAudio.Play();
    }
    //
    void _si_4_audioMethod()
    {
        myAudio.clip = si_4;
        myAudio.Play();
    }
    //
    void _si_5_audioMethod()
    {
        myAudio.clip = si_5;
        myAudio.Play();
    }
    //
    void _se_dec_1_audioMethod()
    {
        myAudio.clip = se_dec_1;
        myAudio.Play();
    }
    //
    void _se_dec_2_audioMethod()
    {
        myAudio.clip = se_dec_2;
        myAudio.Play();
    }
    //
    void _se_dec_3_audioMethod()
    {
        myAudio.clip = se_dec_3;
        myAudio.Play();
    }
    //
    void _se_dec_4_audioMethod()
    {
        myAudio.clip = se_dec_4;
        myAudio.Play();
    }
    //
    void _se_dec_5_audioMethod()
    {
        myAudio.clip = se_dec_5;
        myAudio.Play();
    }
    //
    void _fr_1_audioMethod()
    {
        myAudio.clip = fr_1;
        myAudio.Play();
    }
    //
    void _fr_2_audioMethod()
    {
        myAudio.clip = fr_2;
        myAudio.Play();
    }
    //
    void _fr_3_audioMethod()
    {
        myAudio.clip = fr_3;
        myAudio.Play();
    }
    //
    void _fr_4_audioMethod()
    {
        myAudio.clip = fr_4;
        myAudio.Play();
    }
    //
    void _sep_1_audioMethod()
    {
        myAudio.clip = sep_1;
        myAudio.Play();
    }
    //
    void _sep_2_audioMethod()
    {
        myAudio.clip = sep_2;
        myAudio.Play();
    }
    //
    void _sep_3_audioMethod()
    {
        myAudio.clip = sep_3;
        myAudio.Play();
    }
    //
    void _sep_4_audioMethod()
    {
        myAudio.clip = sep_4;
        myAudio.Play();
    }
    //
    void _sep_5_audioMethod()
    {
        myAudio.clip = sep_5;
        myAudio.Play();
    }
    //
    void _sep_6_audioMethod()
    {
        myAudio.clip = sep_6;
        myAudio.Play();
    }
    //
    void _sep_7_audioMethod()
    {
        myAudio.clip = sep_7;
        myAudio.Play();
    }
    //
    void _sep_8_audioMethod()
    {
        myAudio.clip = sep_8;
        myAudio.Play();
    }
    //
    void _sep_9_audioMethod()
    {
        myAudio.clip = sep_9;
        myAudio.Play();
    }
    //
    void _sep_10_audioMethod()
    {
        myAudio.clip = sep_10;
        myAudio.Play();
    }
    //
    void _sep_11_audioMethod()
    {
        myAudio.clip = sep_11;
        myAudio.Play();
    }
    //
    void _sep_12_audioMethod()
    {
        myAudio.clip = sep_12;
        myAudio.Play();
    }
    //
    void _sep_13_audioMethod()
    {
        myAudio.clip = sep_13;
        myAudio.Play();
    }
    //
    void _sep_14_audioMethod()
    {
        myAudio.clip = sep_14;
        myAudio.Play();
    }
    //
    void _sep_15_audioMethod()
    {
        myAudio.clip = sep_15;
        myAudio.Play();
    }
    //
    void _sep_16_audioMethod()
    {
        myAudio.clip = sep_16;
        myAudio.Play();
    }
    //
    void _s_us_1_audioMethod()
    {
        myAudio.clip = s_us_1;
        myAudio.Play();
    }
    //
    void _s_us_2_audioMethod()
    {
        myAudio.clip = s_us_2;
        myAudio.Play();
    }
    //
    void _s_us_3_audioMethod()
    {
        myAudio.clip = s_us_3;
        myAudio.Play();
    }
    //
    void _s_us_4_audioMethod()
    {
        myAudio.clip = s_us_4;
        myAudio.Play();
    }
    //
    void _s_us_5_audioMethod()
    {
        myAudio.clip = s_us_5;
        myAudio.Play();
    }
    //
    void _s_us_6_audioMethod()
    {
        myAudio.clip = s_us_6;
        myAudio.Play();
    }
    //
    void _s_us_7_audioMethod()
    {
        myAudio.clip = s_us_7;
        myAudio.Play();
    }
    //
    void _s_us_8_audioMethod()
    {
        myAudio.clip = s_us_8;
        myAudio.Play();
    }
    //
    void _s_us_9_audioMethod()
    {
        myAudio.clip = s_us_9;
        myAudio.Play();
    }
    //

    private int appleCount = 0;
    public void ApplePickup()
    {
        appleCount++;
        if(appleCount == 5)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
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

    private int maxApple = 6;
    private int currApple = 0;
    public TargetController appleMiniGame;
    public void AppleDrop()
    {
        currApple++;

        if (currApple == maxApple)
        {
            appleMiniGame.EndMiniGame();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public Animator butterChunking;
    public Animator sievingANimation;
    public Animator winnowingAnim;
    private int maxChunking = 5;
    private int currChunking = 0;
    public TargetController butterMiniGame;
    public TargetController sieveMiniGame;
    public TargetController winnowingMiniGame;
    public GameObject butterBowl;
    public GameObject sieve;
    public GameObject groundSieve;

    public void StartSeive()
    {
        sieve.SetActive(true);
        groundSieve.SetActive(false);
    }
    public void ChunkingAnimation()
    {
        butterChunking.SetTrigger("Butter");
        currChunking++;

        if (currChunking == maxChunking)
        {
            butterMiniGame.EndMiniGame();
            butterBowl.SetActive(true);
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            currChunking = 0;
        }
    }

    public void SievingAnimation()
    {
        sievingANimation.SetTrigger("Butter");
        currChunking++;

        if (currChunking == maxChunking)
        {
            sieveMiniGame.EndMiniGame();
            groundSieve.SetActive(true);
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            currChunking = 0;
        }
    }

    public void WinnowingAnimation()
    {
        winnowingAnim.SetTrigger("Butter");
        currChunking++;

        if (currChunking == maxChunking)
        {
            winnowingMiniGame.EndMiniGame();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            currChunking = 0;
        }
    }

    public void WheatBundlePickup()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void ButterFiler()
    {
        butterBowl.transform.DOMoveZ(butterBowl.transform.position.z - 2, 2f);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public GameObject brownWater;
    public GameObject cleanWater;
    public TargetController waterPurifyMiniGame;
    public void PurificationOfWater()
    {
        StartCoroutine(WaterPurify());
    }

    IEnumerator WaterPurify()
    {
        yield return new WaitForSeconds(5);
        brownWater.SetActive(false);
        cleanWater.SetActive(true);
        yield return new WaitForSeconds(2);
        waterPurifyMiniGame.EndMiniGame();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void OngameObject(GameObject obj)
    {
        StartCoroutine(DelayOngameObject(obj));
    }

    IEnumerator DelayOngameObject(GameObject obj)
    {
        yield return new WaitForSeconds(10);
        obj.SetActive(true);
    }

    public void OffgameObject(GameObject obj)
    {
        StartCoroutine(DelayOffgameObject(obj));
    }

    IEnumerator DelayOffgameObject(GameObject obj)
    {
        yield return new WaitForSeconds(10);
        obj.SetActive(false);
    }

    public void WaterOnFreezer()
    {
        Debug.Log("MissionFailed");
    }

    public void WaterOnPond()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void StepComp()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void GetRiceGrains(GameObject objs)
    {
        objs.SetActive(true);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }


}
