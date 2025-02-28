using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Carbon_and_its_compounds_Pt2_10th : MonoBehaviour
{

    public Transform waypoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypointCanvas.SetActive(true);
        waypoint.player = InventoryManager.Instance.player.transform;
        waypoint.target = target;
    }

    //public List<Subtitle> subtitles = new List<Subtitle>();
    //private int subtitleIndex = 0;
    //public TextMeshProUGUI subtitleText;

    //private void Start()
    //{
    //    InvokeRepeating("StartSubtitle", subtitles[subtitleIndex].delay, subtitles[subtitleIndex].delay);
    //}

    //public void StartSubtitle()
    //{
    //    subtitleText.text = subtitles[subtitleIndex].subTitleText;

    //    if(subtitleIndex < subtitles.Count)
    //    {
    //        CancelInvoke("StartSubtitle");
    //    }
    //    else
    //    {
    //        subtitleIndex++;
    //    }
    //}


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
        SetWayPoint(waypoint1);
    }



    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject hatertotext;
    public GameObject propanesfxtext;
    public GameObject Fire;
    public GameObject satFire;
    public GameObject unsatFire;
    public GameObject sootyFire;
    public GameObject sulfurFire;
    public GameObject Gasestext;
    public GameObject drinking;
    public GameObject droppers;
    public GameObject dropper;
    public GameObject soapsol;
    public GameObject soapanim;
    public GameObject OnOff;
    public GameObject soap;
    public GameObject ch3;
    public GameObject mp;
    public GameObject glacial;
    public GameObject weak;
    public GameObject esterR;
    public GameObject perfume;
    public GameObject soapchain;
    public GameObject withbase;
    public GameObject carbonates;
    public GameObject sodiumacetate;




    //Titles



    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject T6;
    public GameObject T7;
    public GameObject T8;
    public GameObject T9;
    public GameObject T10;
    public GameObject T11;
    public GameObject T12;
    public GameObject T13;
    public GameObject T14;
    public GameObject T15;
    public GameObject T16;







    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;
    public GameObject D6;
    public GameObject D7;
    public GameObject D8;
    public GameObject D9;
    public GameObject D10;
    public GameObject D11;


    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("Cam anim", 0, targetNormalizedTime);
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

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject co2_anim;
    public GameObject oxy_anim;
    public GameObject oxi_anim;
    public GameObject tt_anim;
    public GameObject soap_anim;
    public GameObject droppers_anim;
    public GameObject tubes_anim;









    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip audio_E1;
    public AudioClip audio_E2;
    public AudioClip audio_E3;
    public AudioClip audio_1b;
    public AudioClip audio_2b;
    public AudioClip audio_3b;
    public AudioClip audio_4b;
    public AudioClip audio_5b;
    public AudioClip audio_6b;
    public AudioClip audio_7b;
    public AudioClip audio_8b;
    public AudioClip audio_9b;
    public AudioClip audio_10b;
    public AudioClip audio_11b;
    public AudioClip audio_12b;
    public AudioClip audio_13b;
    public AudioClip audio_14b;
    public AudioClip audio_15b;
    public AudioClip audio_16b;
    public AudioClip audio_17b;
    public AudioClip audio_18b;
    public AudioClip audio_19b;
    public AudioClip audio_20b;
    public AudioClip audio_21b;
    public AudioClip audio_22b;
    public AudioClip audio_23b;
    public AudioClip audio_24b;
    public AudioClip audio_25b;
    public AudioClip audio_26b;
    public AudioClip audio_27b;
    public AudioClip audio_28b;
    public AudioClip audio_29b;
    public AudioClip audio_30b;
    public AudioClip audio_31b;
    public AudioClip audio_32b;
    public AudioClip audio_33b;
    public AudioClip audio_34b;
    public AudioClip audio_35b;
    public AudioClip audio_36b;
    public AudioClip audio_37b;
    public AudioClip audio_38b;
    public AudioClip audio_39b;
    public AudioClip audio_40b;
    public AudioClip audio_41b;
    public AudioClip audio_42b;
    public AudioClip audio_43b;
    public AudioClip audio_44b;
    public AudioClip audio_45b;
    public AudioClip audio_46b;
    public AudioClip audio_47b;
    public AudioClip audio_48b;
    public AudioClip audio_49b;
    public AudioClip audio_50b;
    public AudioClip audio_51b;
    public AudioClip audio_52b;
    public AudioClip audio_53b;
    public AudioClip audio_54b;
    public AudioClip audio_55b;
    public AudioClip audio_56b;
    public AudioClip audio_57b;
    public AudioClip audio_58b;
    public AudioClip audio_59b;
    public AudioClip audio_60b;
    public AudioClip audio_61b;
    public AudioClip audio_62b;
    public AudioClip audio_63b;
    public AudioClip audio_64b;
    public AudioClip audio_65b;
    public AudioClip audio_66b;
    public AudioClip audio_67b;
    public AudioClip audio_68b;
    public AudioClip audio_69b;
    public AudioClip audio_70b;
    public AudioClip audio_71b;
    public AudioClip audio_72b;
    public AudioClip audio_73b;
    public AudioClip audio_74b;
    public AudioClip audio_75b;
    public AudioClip audio_76b;
    public AudioClip audio_77b;
    public AudioClip audio_78b;
    public AudioClip audio_79b;
    public AudioClip audio_80b;
    public AudioClip audio_81b;
    public AudioClip audio_82b;
    public AudioClip audio_83b;
    public AudioClip audio_84b;
    public AudioClip audio_85b;
    public AudioClip audio_86b;
    public AudioClip audio_87b;
    public AudioClip audio_88b;
    public AudioClip audio_89b;
    public AudioClip audio_90b;
    public AudioClip audio_91b;
    public AudioClip audio_92b;
    public AudioClip audio_93b;
    public AudioClip audio_94b;
    public AudioClip audio_95b;
    public AudioClip audio_96b;
    public AudioClip audio_97b;
    public AudioClip audio_98b;
    public AudioClip audio_99b;
    public AudioClip audio_100b;
    public AudioClip audio_101b;
    public AudioClip audio_102b;
    public AudioClip audio_103b;
    public AudioClip audio_104b;
    public AudioClip audio_105b;
    public AudioClip audio_106b;
    public AudioClip audio_107b;
    public AudioClip audio_108b;
    public AudioClip audio_109b;
    public AudioClip audio_110b;
    public AudioClip audio_111b;
    public AudioClip audio_112b;
    public AudioClip audio_113b;
    public AudioClip audio_114b;
    public AudioClip audio_115b;
    public AudioClip audio_116b;
    public AudioClip audio_117b;
    public AudioClip audio_118b;
    public AudioClip audio_119b;
    public AudioClip audio_120b;
    public AudioClip audio_121b;
    public AudioClip audio_122b;
    public AudioClip audio_123b;
    public AudioClip audio_124b;
    public AudioClip audio_125b;
    public AudioClip audio_126b;
    public AudioClip audio_127b;
    public AudioClip audio_128b;
    public AudioClip audio_129b;
    public AudioClip audio_130b;
    public AudioClip audio_131b;
    public AudioClip audio_132b;
    public AudioClip audio_133b;















    // Method ON/OFF






    void _sodiumacetate_MethodON()
    {
        sodiumacetate.SetActive(true);
    }
    void _sodiumacetate_MethodoOFF()
    {
        sodiumacetate.SetActive(false);
    }
    //

    void _carbonates_MethodON()
    {
        carbonates.SetActive(true);
    }
    void _carbonates_MethodoOFF()
    {
        carbonates.SetActive(false);
    }
    //

    void _withbase_MethodON()
    {
        withbase.SetActive(true);
    }
    void _withbase_MethodoOFF()
    {
        withbase.SetActive(false);
    }
    //

    void _soapchain_MethodON()
    {
        soapchain.SetActive(true);
    }
    void _soapchain_MethodoOFF()
    {
        soapchain.SetActive(false);
    }
    //

    void _perfume_MethodON()
    {
        perfume.SetActive(true);
    }
    void _perfume_MethodoOFF()
    {
        perfume.SetActive(false);
    }
    //


    void _esterR_MethodON()
    {
        esterR.SetActive(true);
    }
    void _esterR_MethodoOFF()
    {
        esterR.SetActive(false);
    }
    //

    void _weak_MethodON()
    {
        weak.SetActive(true);
    }
    void _weak_MethodoOFF()
    {
        weak.SetActive(false);
    }
    //

    void _glacial_MethodON()
    {
        glacial.SetActive(true);
    }
    void _glacial_MethodoOFF()
    {
        glacial.SetActive(false);
    }
    //

    void _mp_MethodON()
    {
        mp.SetActive(true);
    }
    void _mp_MethodoOFF()
    {
        mp.SetActive(false);
    }
    //

    void _ch3_MethodON()
    {
        ch3.SetActive(true);
    }
    void _ch3_MethodoOFF()
    {
        ch3.SetActive(false);
    }
    //

    void _soap_MethodON()
    {
        soap.SetActive(true);
    }
    void _soap_MethodoOFF()
    {
        soap.SetActive(false);
    }
    //

    void _OnOff_MethodON()
    {
        OnOff.SetActive(true);
    }
    void _OnOff_MethodoOFF()
    {
        OnOff.SetActive(false);
    }
    //

    void _soapanim_MethodON()
    {
        soapanim.SetActive(true);
    }
    void _soapanim_MethodoOFF()
    {
        soapanim.SetActive(false);
    }
    //

    void _droppers_MethodON()
    {
        droppers.SetActive(true);
    }
    void _droppers_MethodoOFF()
    {
        droppers.SetActive(false);
    }
    //
    void _dropper_MethodON()
    {
        dropper.SetActive(true);
    }
    void _dropper_MethodoOFF()
    {
        dropper.SetActive(false);
    }
    //
    void _soapsol_MethodON()
    {
        soapsol.SetActive(true);
    }
    void _soapsol_MethodoOFF()
    {
        soapsol.SetActive(false);
    }
    //

    void _drinking_MethodON()
    {
        drinking.SetActive(true);
    }
    void _drinking_MethodoOFF()
    {
        drinking.SetActive(false);
    }
    //
    void _hatertotext_MethodON()
    {
        hatertotext.SetActive(true);
    }
    void _hatertotext_MethodoOFF()
    {
        hatertotext.SetActive(false);
    }
    //
    void _propanesfxtext_MethodON()
    {
        propanesfxtext.SetActive(true);
    }
    void _propanesfxtext_MethodoOFF()
    {
        propanesfxtext.SetActive(false);
    }
    //
    void _Fire_MethodON()
    {
        Fire.SetActive(true);
    }
    void _Fire_MethodoOFF()
    {
        Fire.SetActive(false);
    }
    //
    void _satFire_MethodON()
    {
        satFire.SetActive(true);
    }
    void _satFire_MethodoOFF()
    {
        satFire.SetActive(false);
    }
    //
    void _unsatFire_MethodON()
    {
        unsatFire.SetActive(true);
    }
    void _unsatFire_MethodoOFF()
    {
        unsatFire.SetActive(false);
    }
    //
    void _sootyFire_MethodON()
    {
        sootyFire.SetActive(true);
    }
    void _sootyFire_MethodoOFF()
    {
        sootyFire.SetActive(false);
    }
    //
    void _sulfurFire_MethodON()
    {
        sulfurFire.SetActive(true);
    }
    void _sulfurFire_MethodoOFF()
    {
        sulfurFire.SetActive(false);
    }
    //
    void _Gasestext_MethodON()
    {
        Gasestext.SetActive(true);
    }
    void _Gasestext_MethodoOFF()
    {
        Gasestext.SetActive(false);
    }




























    // Animations




    void _co2_anim_animAnimmethod()
    {

        anim = co2_anim.GetComponent<Animator>();
        anim.Play("Co2 burning anim");
    }

    void _oxy_anim_animAnimmethod()
    {

        anim = oxy_anim.GetComponent<Animator>();
        anim.Play("Oxy anim");
    }

    void _oxi_anim_animAnimmethod()
    {

        anim = oxi_anim.GetComponent<Animator>();
        anim.Play("Oxy anim");
    }

    void _tt_anim_animAnimmethod()
    {

        anim = tt_anim.GetComponent<Animator>();
        anim.Play("Testubes anim");
    }

    void _Soap_anim_animAnimmethod()
    {

        anim = soap_anim.GetComponent<Animator>();
        anim.Play("Soap anim");
    }

    void _Droppers_anim_animAnimmethod()
    {

        anim = droppers_anim.GetComponent<Animator>();
        anim.Play("Droppers anim");
    }

    void _Tubes_anim_animAnimmethod()
    {

        anim = tubes_anim.GetComponent<Animator>();
        anim.Play("Tubes anim");
    }










    // Descriptions


    //
    void _D1_MethodON()
    {
        D1.SetActive(true);
    }
    void _D1_MethodoOFF()
    {
        D1.SetActive(false);
    }
    //
    void _D2_MethodON()
    {
        D2.SetActive(true);
    }
    void _D2_MethodoOFF()
    {
        D2.SetActive(false);
    }
    //
    void _D3_MethodON()
    {
        D3.SetActive(true);
    }
    void _D3_MethodoOFF()
    {
        D3.SetActive(false);
    }
    //
    void _D4_MethodON()
    {
        D4.SetActive(true);
    }
    void _D4_MethodoOFF()
    {
        D4.SetActive(false);
    }
    //
    void _D5_MethodON()
    {
        D5.SetActive(true);
    }
    void _D5_MethodoOFF()
    {
        D5.SetActive(false);
    }
    //
    void _D6_MethodON()
    {
        D6.SetActive(true);
    }
    void _D6_MethodoOFF()
    {
        D6.SetActive(false);
    }
    //
    void _D7_MethodON()
    {
        D7.SetActive(true);
    }
    void _D7_MethodoOFF()
    {
        D7.SetActive(false);
    }
    //
    void _D8_MethodON()
    {
        D8.SetActive(true);
    }
    void _D8_MethodoOFF()
    {
        D8.SetActive(false);
    }
    //
    void _D9_MethodON()
    {
        D9.SetActive(true);
    }
    void _D9_MethodoOFF()
    {
        D9.SetActive(false);
    }
    //
    void _D10_MethodON()
    {
        D10.SetActive(true);
    }
    void _D10_MethodoOFF()
    {
        D10.SetActive(false);
    }
    //
    void _D11_MethodON()
    {
        D11.SetActive(true);
    }
    void _D11_MethodoOFF()
    {
        D11.SetActive(false);
    }











    //Titles




    //
    void _T1_MethodON()
    {
        T1.SetActive(true);
    }
    void _T1_MethodoOFF()
    {
        T1.SetActive(false);
    }
    //
    void _T2_MethodON()
    {
        T2.SetActive(true);
    }
    void _T2_MethodoOFF()
    {
        T2.SetActive(false);
    }
    //
    void _T3_MethodON()
    {
        T3.SetActive(true);
    }
    void _T3_MethodoOFF()
    {
        T3.SetActive(false);
    }
    //
    void _T4_MethodON()
    {
        T4.SetActive(true);
    }
    void _T4_MethodoOFF()
    {
        T4.SetActive(false);
    }
    //
    void _T5_MethodON()
    {
        T5.SetActive(true);
    }
    void _T5_MethodoOFF()
    {
        T5.SetActive(false);
    }
    //
    void _T6_MethodON()
    {
        T6.SetActive(true);
    }
    void _T6_MethodoOFF()
    {
        T6.SetActive(false);
    }
    //
    void _T7_MethodON()
    {
        T7.SetActive(true);
    }
    void _T7_MethodoOFF()
    {
        T7.SetActive(false);
    }
    //
    void _T8_MethodON()
    {
        T8.SetActive(true);
    }
    void _T8_MethodoOFF()
    {
        T8.SetActive(false);
    }
    //
    void _T9_MethodON()
    {
        T9.SetActive(true);
    }
    void _T9_MethodoOFF()
    {
        T9.SetActive(false);
    }
    //
    void _T10_MethodON()
    {
        T10.SetActive(true);
    }
    void _T10_MethodoOFF()
    {
        T10.SetActive(false);
    }
    //
    void _T11_MethodON()
    {
        T11.SetActive(true);
    }
    void _T11_MethodoOFF()
    {
        T11.SetActive(false);
    }
    //
    void _T12_MethodON()
    {
        T12.SetActive(true);
    }
    void _T12_MethodoOFF()
    {
        T12.SetActive(false);
    }
    //
    void _T13_MethodON()
    {
        T13.SetActive(true);
    }
    void _T13_MethodoOFF()
    {
        T13.SetActive(false);
    }
    //
    void _T14_MethodON()
    {
        T14.SetActive(true);
    }
    void _T14_MethodoOFF()
    {
        T14.SetActive(false);
    }
    //
    void _T15_MethodON()
    {
        T15.SetActive(true);
    }
    void _T15_MethodoOFF()
    {
        T15.SetActive(false);
    }
    //
    void _T16_MethodON()
    {
        T16.SetActive(true);
    }
    void _T16_MethodoOFF()
    {
        T16.SetActive(false);
    }

















    // Audio












    void _audio_E1_audioMethod()
    {
        myAudio.clip = audio_E1;
        myAudio.Play();
    }
    void _audio_E2_audioMethod()
    {
        myAudio.clip = audio_E2;
        myAudio.Play();
    }
    void _audio_E3_audioMethod()
    {
        myAudio.clip = audio_E3;
        myAudio.Play();
    }
    void _audio_1b_audioMethod()
    {
        myAudio.clip = audio_1b;
        myAudio.Play();
    }
    void _audio_2b_audioMethod()
    {
        myAudio.clip = audio_2b;
        myAudio.Play();
    }
    void _audio_3b_audioMethod()
    {
        myAudio.clip = audio_3b;
        myAudio.Play();
    }
    void _audio_4b_audioMethod()
    {
        myAudio.clip = audio_4b;
        myAudio.Play();
    }
    void _audio_5b_audioMethod()
    {
        myAudio.clip = audio_5b;
        myAudio.Play();
    }
    void _audio_6b_audioMethod()
    {
        myAudio.clip = audio_6b;
        myAudio.Play();
    }
    void _audio_7b_audioMethod()
    {
        myAudio.clip = audio_7b;
        myAudio.Play();
    }
    void _audio_8b_audioMethod()
    {
        myAudio.clip = audio_8b;
        myAudio.Play();
    }
    void _audio_9b_audioMethod()
    {
        myAudio.clip = audio_9b;
        myAudio.Play();
    }
    void _audio_10b_audioMethod()
    {
        myAudio.clip = audio_10b;
        myAudio.Play();
    }

    void _audio_11b_audioMethod()
    {
        myAudio.clip = audio_11b;
        myAudio.Play();
    }
    void _audio_12b_audioMethod()
    {
        myAudio.clip = audio_12b;
        myAudio.Play();
    }
    void _audio_13b_audioMethod()
    {
        myAudio.clip = audio_13b;
        myAudio.Play();
    }
    void _audio_14b_audioMethod()
    {
        myAudio.clip = audio_14b;
        myAudio.Play();
    }
    void _audio_15b_audioMethod()
    {
        myAudio.clip = audio_15b;
        myAudio.Play();
    }
    void _audio_16b_audioMethod()
    {
        myAudio.clip = audio_16b;
        myAudio.Play();
    }
    void _audio_17b_audioMethod()
    {
        myAudio.clip = audio_17b;
        myAudio.Play();
    }
    void _audio_18b_audioMethod()
    {
        myAudio.clip = audio_18b;
        myAudio.Play();
    }
    void _audio_19b_audioMethod()
    {
        myAudio.clip = audio_19b;
        myAudio.Play();
    }
    void _audio_20b_audioMethod()
    {
        myAudio.clip = audio_20b;
        myAudio.Play();
    }
    void _audio_21b_audioMethod()
    {
        myAudio.clip = audio_21b;
        myAudio.Play();
    }
    void _audio_22b_audioMethod()
    {
        myAudio.clip = audio_22b;
        myAudio.Play();
    }
    void _audio_23b_audioMethod()
    {
        myAudio.clip = audio_23b;
        myAudio.Play();
    }
    void _audio_24b_audioMethod()
    {
        myAudio.clip = audio_24b;
        myAudio.Play();
    }
    void _audio_25b_audioMethod()
    {
        myAudio.clip = audio_25b;
        myAudio.Play();
    }
    void _audio_26b_audioMethod()
    {
        myAudio.clip = audio_26b;
        myAudio.Play();
    }
    void _audio_27b_audioMethod()
    {
        myAudio.clip = audio_27b;
        myAudio.Play();
    }
    void _audio_28b_audioMethod()
    {
        myAudio.clip = audio_28b;
        myAudio.Play();
    }
    void _audio_29b_audioMethod()
    {
        myAudio.clip = audio_29b;
        myAudio.Play();
    }
    void _audio_30b_audioMethod()
    {
        myAudio.clip = audio_30b;
        myAudio.Play();
    }
    void _audio_31b_audioMethod()
    {
        myAudio.clip = audio_31b;
        myAudio.Play();
    }
    void _audio_32b_audioMethod()
    {
        myAudio.clip = audio_32b;
        myAudio.Play();
    }
    void _audio_33b_audioMethod()
    {
        myAudio.clip = audio_33b;
        myAudio.Play();
    }
    void _audio_34b_audioMethod()
    {
        myAudio.clip = audio_34b;
        myAudio.Play();
    }
    void _audio_35b_audioMethod()
    {
        myAudio.clip = audio_35b;
        myAudio.Play();
    }
    void _audio_36b_audioMethod()
    {
        myAudio.clip = audio_36b;
        myAudio.Play();
    }
    void _audio_37b_audioMethod()
    {
        myAudio.clip = audio_37b;
        myAudio.Play();
    }
    void _audio_38b_audioMethod()
    {
        myAudio.clip = audio_38b;
        myAudio.Play();
    }
    void _audio_39b_audioMethod()
    {
        myAudio.clip = audio_39b;
        myAudio.Play();
    }
    void _audio_40b_audioMethod()
    {
        myAudio.clip = audio_40b;
        myAudio.Play();
    }
    void _audio_41b_audioMethod()
    {
        myAudio.clip = audio_41b;
        myAudio.Play();
    }
    void _audio_42b_audioMethod()
    {
        myAudio.clip = audio_42b;
        myAudio.Play();
    }
    void _audio_43b_audioMethod()
    {
        myAudio.clip = audio_43b;
        myAudio.Play();
    }
    void _audio_44b_audioMethod()
    {
        myAudio.clip = audio_44b;
        myAudio.Play();
    }
    void _audio_45b_audioMethod()
    {
        myAudio.clip = audio_45b;
        myAudio.Play();
    }
    void _audio_46b_audioMethod()
    {
        myAudio.clip = audio_46b;
        myAudio.Play();
    }
    void _audio_47b_audioMethod()
    {
        myAudio.clip = audio_47b;
        myAudio.Play();
    }
    void _audio_48b_audioMethod()
    {
        myAudio.clip = audio_48b;
        myAudio.Play();
    }
    void _audio_49b_audioMethod()
    {
        myAudio.clip = audio_49b;
        myAudio.Play();
    }
    void _audio_50b_audioMethod()
    {
        myAudio.clip = audio_50b;
        myAudio.Play();
    }
    void _audio_51b_audioMethod()
    {
        myAudio.clip = audio_51b;
        myAudio.Play();
    }
    void _audio_52b_audioMethod()
    {
        myAudio.clip = audio_52b;
        myAudio.Play();
    }
    void _audio_53b_audioMethod()
    {
        myAudio.clip = audio_53b;
        myAudio.Play();
    }
    void _audio_54b_audioMethod()
    {
        myAudio.clip = audio_54b;
        myAudio.Play();
    }
    void _audio_55b_audioMethod()
    {
        myAudio.clip = audio_55b;
        myAudio.Play();
    }
    void _audio_56b_audioMethod()
    {
        myAudio.clip = audio_56b;
        myAudio.Play();
    }
    void _audio_57b_audioMethod()
    {
        myAudio.clip = audio_57b;
        myAudio.Play();
    }
    void _audio_58b_audioMethod()
    {
        myAudio.clip = audio_58b;
        myAudio.Play();
    }
    void _audio_59b_audioMethod()
    {
        myAudio.clip = audio_59b;
        myAudio.Play();
    }
    void _audio_60b_audioMethod()
    {
        myAudio.clip = audio_60b;
        myAudio.Play();
    }
    void _audio_61b_audioMethod()
    {
        myAudio.clip = audio_61b;
        myAudio.Play();
    }
    void _audio_62b_audioMethod()
    {
        myAudio.clip = audio_62b;
        myAudio.Play();
    }
    void _audio_63b_audioMethod()
    {
        myAudio.clip = audio_63b;
        myAudio.Play();
    }
    void _audio_64b_audioMethod()
    {
        myAudio.clip = audio_64b;
        myAudio.Play();
    }
    void _audio_65b_audioMethod()
    {
        myAudio.clip = audio_65b;
        myAudio.Play();
    }
    void _audio_66b_audioMethod()
    {
        myAudio.clip = audio_66b;
        myAudio.Play();
    }
    void _audio_67b_audioMethod()
    {
        myAudio.clip = audio_67b;
        myAudio.Play();
    }
    void _audio_68b_audioMethod()
    {
        myAudio.clip = audio_68b;
        myAudio.Play();
    }
    void _audio_69b_audioMethod()
    {
        myAudio.clip = audio_69b;
        myAudio.Play();
    }
    void _audio_70b_audioMethod()
    {
        myAudio.clip = audio_70b;
        myAudio.Play();
    }
    void _audio_71b_audioMethod()
    {
        myAudio.clip = audio_71b;
        myAudio.Play();
    }
    void _audio_72b_audioMethod()
    {
        myAudio.clip = audio_72b;
        myAudio.Play();
    }
    void _audio_73b_audioMethod()
    {
        myAudio.clip = audio_73b;
        myAudio.Play();
    }
    void _audio_74b_audioMethod()
    {
        myAudio.clip = audio_74b;
        myAudio.Play();
    }
    void _audio_75b_audioMethod()
    {
        myAudio.clip = audio_75b;
        myAudio.Play();
    }
    void _audio_76b_audioMethod()
    {
        myAudio.clip = audio_76b;
        myAudio.Play();
    }
    void _audio_77b_audioMethod()
    {
        myAudio.clip = audio_77b;
        myAudio.Play();
    }
    void _audio_78b_audioMethod()
    {
        myAudio.clip = audio_78b;
        myAudio.Play();
    }
    void _audio_79b_audioMethod()
    {
        myAudio.clip = audio_79b;
        myAudio.Play();
    }
    void _audio_80b_audioMethod()
    {
        myAudio.clip = audio_80b;
        myAudio.Play();
    }
    void _audio_81b_audioMethod()
    {
        myAudio.clip = audio_81b;
        myAudio.Play();
    }
    void _audio_82b_audioMethod()
    {
        myAudio.clip = audio_82b;
        myAudio.Play();
    }
    void _audio_83b_audioMethod()
    {
        myAudio.clip = audio_83b;
        myAudio.Play();
    }
    void _audio_84b_audioMethod()
    {
        myAudio.clip = audio_84b;
        myAudio.Play();
    }
    void _audio_85b_audioMethod()
    {
        myAudio.clip = audio_85b;
        myAudio.Play();
    }
    void _audio_86b_audioMethod()
    {
        myAudio.clip = audio_86b;
        myAudio.Play();
    }
    void _audio_87b_audioMethod()
    {
        myAudio.clip = audio_87b;
        myAudio.Play();
    }
    void _audio_88b_audioMethod()
    {
        myAudio.clip = audio_88b;
        myAudio.Play();
    }
    void _audio_89b_audioMethod()
    {
        myAudio.clip = audio_89b;
        myAudio.Play();
    }
    void _audio_90b_audioMethod()
    {
        myAudio.clip = audio_90b;
        myAudio.Play();
    }
    void _audio_91b_audioMethod()
    {
        myAudio.clip = audio_91b;
        myAudio.Play();
    }
    void _audio_92b_audioMethod()
    {
        myAudio.clip = audio_92b;
        myAudio.Play();
    }
    void _audio_93b_audioMethod()
    {
        myAudio.clip = audio_93b;
        myAudio.Play();
    }
    void _audio_94b_audioMethod()
    {
        myAudio.clip = audio_94b;
        myAudio.Play();
    }
    void _audio_95b_audioMethod()
    {
        myAudio.clip = audio_95b;
        myAudio.Play();
    }
    void _audio_96b_audioMethod()
    {
        myAudio.clip = audio_96b;
        myAudio.Play();
    }
    void _audio_97b_audioMethod()
    {
        myAudio.clip = audio_97b;
        myAudio.Play();
    }
    void _audio_98b_audioMethod()
    {
        myAudio.clip = audio_98b;
        myAudio.Play();
    }
    void _audio_99b_audioMethod()
    {
        myAudio.clip = audio_99b;
        myAudio.Play();
    }
    void _audio_100b_audioMethod()
    {
        myAudio.clip = audio_100b;
        myAudio.Play();
    }
    void _audio_101b_audioMethod()
    {
        myAudio.clip = audio_101b;
        myAudio.Play();
    }
    void _audio_102b_audioMethod()
    {
        myAudio.clip = audio_102b;
        myAudio.Play();
    }
    void _audio_103b_audioMethod()
    {
        myAudio.clip = audio_103b;
        myAudio.Play();
    }
    void _audio_104b_audioMethod()
    {
        myAudio.clip = audio_104b;
        myAudio.Play();
    }
    void _audio_105b_audioMethod()
    {
        myAudio.clip = audio_105b;
        myAudio.Play();
    }
    void _audio_106b_audioMethod()
    {
        myAudio.clip = audio_106b;
        myAudio.Play();
    }
    void _audio_107b_audioMethod()
    {
        myAudio.clip = audio_107b;
        myAudio.Play();
    }
    void _audio_108b_audioMethod()
    {
        myAudio.clip = audio_108b;
        myAudio.Play();
    }
    void _audio_109b_audioMethod()
    {
        myAudio.clip = audio_109b;
        myAudio.Play();
    }
    void _audio_110b_audioMethod()
    {
        myAudio.clip = audio_110b;
        myAudio.Play();
    }
    void _audio_111b_audioMethod()
    {
        myAudio.clip = audio_111b;
        myAudio.Play();
    }
    void _audio_112b_audioMethod()
    {
        myAudio.clip = audio_112b;
        myAudio.Play();
    }
    void _audio_113b_audioMethod()
    {
        myAudio.clip = audio_113b;
        myAudio.Play();
    }
    void _audio_114b_audioMethod()
    {
        myAudio.clip = audio_114b;
        myAudio.Play();
    }
    void _audio_115b_audioMethod()
    {
        myAudio.clip = audio_115b;
        myAudio.Play();
    }
    void _audio_116b_audioMethod()
    {
        myAudio.clip = audio_116b;
        myAudio.Play();
    }
    void _audio_117b_audioMethod()
    {
        myAudio.clip = audio_117b;
        myAudio.Play();
    }
    void _audio_118b_audioMethod()
    {
        myAudio.clip = audio_118b;
        myAudio.Play();
    }
    void _audio_119b_audioMethod()
    {
        myAudio.clip = audio_119b;
        myAudio.Play();
    }
    void _audio_120b_audioMethod()
    {
        myAudio.clip = audio_120b;
        myAudio.Play();
    }
    void _audio_121b_audioMethod()
    {
        myAudio.clip = audio_121b;
        myAudio.Play();
    }
    void _audio_122b_audioMethod()
    {
        myAudio.clip = audio_122b;
        myAudio.Play();
    }
    void _audio_123b_audioMethod()
    {
        myAudio.clip = audio_123b;
        myAudio.Play();
    }
    void _audio_124b_audioMethod()
    {
        myAudio.clip = audio_124b;
        myAudio.Play();
    }
    void _audio_125b_audioMethod()
    {
        myAudio.clip = audio_125b;
        myAudio.Play();
    }
    void _audio_126b_audioMethod()
    {
        myAudio.clip = audio_126b;
        myAudio.Play();
    }
    void _audio_127b_audioMethod()
    {
        myAudio.clip = audio_127b;
        myAudio.Play();
    }
    void _audio_128b_audioMethod()
    {
        myAudio.clip = audio_128b;
        myAudio.Play();
    }
    void _audio_129b_audioMethod()
    {
        myAudio.clip = audio_129b;
        myAudio.Play();
    }
    void _audio_130b_audioMethod()
    {
        myAudio.clip = audio_130b;
        myAudio.Play();
    }
    void _audio_131b_audioMethod()
    {
        myAudio.clip = audio_131b;
        myAudio.Play();
    }
    void _audio_132b_audioMethod()
    {
        myAudio.clip = audio_132b;
        myAudio.Play();
    }
    void _audio_133b_audioMethod()
    {
        myAudio.clip = audio_133b;
        myAudio.Play();
    }

    public void MiniGameStart()
    {
        waypointCanvas.SetActive(false);
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

    public int index = 0;
    public TargetController level1minigame;
    public void TurnOnMesh(DragAndDrop meshRenderer)
    {
        index++;
       meshRenderer.triggeredObj.GetComponent<MeshRenderer>().enabled = true;
        if(index == 2)
        {
            index = 0;
            level1minigame.EndMiniGame();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    public List<GameObject> questions1list = new List<GameObject>();
    public List<GameObject> exp1list = new List<GameObject>();
    public TargetController level1QuestionsMiniGame;
    public void Questions1()
    {
        foreach(GameObject ques in questions1list)
        {
            ques.SetActive(false);
        }
        foreach (GameObject obj in exp1list)
        {
            obj.SetActive(false);
        }

        exp1list[index].SetActive(true);
        StartCoroutine(QuestionsDelay1());
    }

    IEnumerator QuestionsDelay1()
    {
        yield return new WaitForSeconds(1);
        questions1list[index].SetActive(true);
    }

    public Animator door1Anim;
    public Animator door2Anim;

    public void CorrectAnswer()
    {
        index++;
        if (index == 5)
        {
            index = 0;
            level1QuestionsMiniGame.EndMiniGame();
            door1Anim.SetTrigger("Trigger");
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            foreach (GameObject ques in questions1list)
            {
                ques.SetActive(false);
            }
            return;
        }
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        Questions1();

       
    }

    public List<GameObject> questions2list = new List<GameObject>();
    public List<GameObject> exp2list = new List<GameObject>();
    public TargetController level2QuestionsMiniGame;
    public void Questions2()
    {
        foreach (GameObject ques in questions2list)
        {
            ques.SetActive(false);
        }
        foreach (GameObject obj in exp2list)
        {
            obj.SetActive(false);
        }

        exp2list[index].SetActive(true);
        StartCoroutine(QuestionsDelay2());
    }

    IEnumerator QuestionsDelay2()
    {
        yield return new WaitForSeconds(1);
        questions2list[index].SetActive(true);
    }

    public void CorrectAnswer2()
    {
        index++;
        if (index == 6)
        {
            index = 0;
            level2QuestionsMiniGame.EndMiniGame();
            door2Anim.SetTrigger("Trigger");
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            foreach (GameObject ques in questions2list)
            {
                ques.SetActive(false);
            }
            return;
        }
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        Questions2();

    }

    public void DropObjLevel2(TargetController miniGame)
    {
        index++;
        if(index == 3)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            miniGame.EndMiniGame();
        }
    }

    public void DropObj3(TargetController MINIgANME)
    {
        index++;
        if(index == 5)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            index = 0;
            MINIgANME.EndMiniGame();
        }
    }













}
