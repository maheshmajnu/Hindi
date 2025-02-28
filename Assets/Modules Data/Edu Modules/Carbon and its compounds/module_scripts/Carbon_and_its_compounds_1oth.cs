using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Carbon_and_its_compounds_1oth : MonoBehaviour
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


        

        InventoryManager.Instance.GetComponent<GamePlayManager>().InitializeScene();
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        

        

    }

    public Camera cam;

    #region SubtitleFunction
    public List<Subtitle> subtitles = new List<Subtitle>();
    public TextMeshProUGUI subtitleText;
    public GameObject subtitleObject;
    private int subtitleIndex = 0;
    private float currentDelay;

    private void Start()
    {
        if (subtitles.Count > 0)
        {
            currentDelay = subtitles[subtitleIndex].delay;
            Invoke("StartNextSubtitle", currentDelay);
        }
        else
        {
            Debug.LogError("No subtitles found in the list!");
        }
    }

    private void StartNextSubtitle()
    {
        if (!subtitleObject.activeInHierarchy)
        {
            subtitleObject.SetActive(true);
        }

        subtitleText.text = subtitles[subtitleIndex].subTitleText;

        if (subtitleIndex < subtitles.Count - 1)
        {
            subtitleIndex++;
            currentDelay = subtitles[subtitleIndex].delay;
            // Schedule the next subtitle after currentDelay seconds
            Invoke("StartNextSubtitle", currentDelay);
        }
        else
        {
            Debug.Log("All subtitles shown.");
        }
    }
    #endregion


    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject ccm_text;
    public GameObject carbon_text;
    public GameObject neon_text;
    public GameObject sodium_text;
    public GameObject sodiumlose;
    public GameObject hydrogen;
    public GameObject heliumgrp;
    public GameObject h2dc;
    public GameObject h2cov;
    public GameObject chlorinedet;
    public GameObject hydrotext;
    public GameObject carbon;
    public GameObject hyd1;
    public GameObject hyd3;
    public GameObject diatext;
    public GameObject gratext;
    public GameObject carbonform;
    public GameObject PCtext;
    public GameObject graphiteHover;
    public GameObject diamond;
    public GameObject ethanecom;
    public GameObject carbonsep;
    public GameObject carbonbond;
    public GameObject hydrobonds;
    public GameObject carbonsep1;
    public GameObject carbonbond1;
    public GameObject hydrobonds1;
    public GameObject hydrogens;
    public GameObject ethenecarbs;
    public GameObject ethenehydrs;
    public GameObject ethenedbond;
    public GameObject ethynetext;
    public GameObject ethynetext2;
    public GameObject coming;
    public GameObject going;
    public GameObject chlorinetext;
    public GameObject nitrogentext;
    public GameObject onevalence;
    public GameObject elementswith;


    //Titles


    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject T6;
    public GameObject T7;







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
    public GameObject D12;











    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject graphite_anim;
    public GameObject diamond_anim;
    public GameObject coming_anim;
    public GameObject going_anim;









    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;


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




    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("Camera anim", 0, targetNormalizedTime);
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



    public void GotoMainMenu()
    {

        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }









    // Update is called once per frame
    void Update()
    {

    }







    // Animations


    void PlayAudio(AudioClip clip)
    {
        myAudio.clip = clip;
        myAudio.Play();
    }



    void _graphite_animAnimmethod()
    {

        anim = graphite_anim.GetComponent<Animator>();
        anim.Play("Graphite anim");
    }

    void _diamond_animAnimmethod()
    {

        anim = diamond_anim.GetComponent<Animator>();
        anim.Play("Diamond anim");
    }

    void _coming_animAnimmethod()
    {

        anim = coming_anim.GetComponent<Animator>();
        anim.Play("Carbon coming");
    }

    void _going_animAnimmethod()
    {

        anim = going_anim.GetComponent<Animator>();
        anim.Play("Carbon going");
    }



















    // Method ON/OFF




    void _coming_MethodON()
    {
        coming.SetActive(true);
    }
    void _coming_MethodoOFF()
    {
        coming.SetActive(false);
    }

    void _going_MethodON()
    {
        going.SetActive(true);
    }
    void _going_MethodoOFF()
    {
        going.SetActive(false);
    }

    void _chlorinetext_MethodON()
    {
        chlorinetext.SetActive(true);
    }
    void _chlorinetext_MethodoOFF()
    {
        chlorinetext.SetActive(false);
    }

    void _nitrogentext_MethodON()
    {
        nitrogentext.SetActive(true);
    }
    void _nitrogentext_MethodoOFF()
    {
        nitrogentext.SetActive(false);
    }

    void _onevalence_MethodON()
    {
        onevalence.SetActive(true);
    }
    void _onevalence_MethodoOFF()
    {
        onevalence.SetActive(false);
    }

    void _elementswith_MethodON()
    {
        elementswith.SetActive(true);
    }
    void _elementswith_MethodoOFF()
    {
        elementswith.SetActive(false);
    }


















    void _ethenecarbs_MethodON()
    {
        ethenecarbs.SetActive(true);
    }
    void _ethenecarbs_MethodoOFF()
    {
        ethenecarbs.SetActive(false);
    }

    void _ethenehydrs_MethodON()
    {
        ethenehydrs.SetActive(true);
    }
    void _ethenehydrs_MethodoOFF()
    {
        ethenehydrs.SetActive(false);
    }

    void _ethenedbond_MethodON()
    {
        ethenedbond.SetActive(true);
    }
    void _ethenedbond_MethodoOFF()
    {
        ethenedbond.SetActive(false);
    }

    void _ethynetext_MethodON()
    {
        ethynetext.SetActive(true);
    }
    void _ethynetext_MethodoOFF()
    {
        ethynetext.SetActive(false);
    }

    void _ethynetext2_MethodON()
    {
        ethynetext2.SetActive(true);
    }
    void _ethynetext2_MethodoOFF()
    {
        ethynetext2.SetActive(false);
    }










    void _ethanecom_MethodON()
    {
        ethanecom.SetActive(true);
    }
    void _ethanecom_MethodoOFF()
    {
        ethanecom.SetActive(false);
    }

    void _carbonsep_MethodON()
    {
        carbonsep.SetActive(true);
    }
    void _carbonsep_MethodoOFF()
    {
        carbonsep.SetActive(false);
    }

    void _carbonbond_MethodON()
    {
        carbonbond.SetActive(true);
    }
    void _carbonbond_MethodoOFF()
    {
        carbonbond.SetActive(false);
    }

    void _hydrobonds_MethodON()
    {
        hydrobonds.SetActive(true);
    }
    void _hydrobonds_MethodoOFF()
    {
        hydrobonds.SetActive(false);
    }


    void _carbonsep1_MethodON()
    {
        carbonsep1.SetActive(true);
    }
    void _carbonsep1_MethodoOFF()
    {
        carbonsep1.SetActive(false);
    }

    void _carbonbond1_MethodON()
    {
        carbonbond1.SetActive(true);
    }
    void _carbonbond1_MethodoOFF()
    {
        carbonbond1.SetActive(false);
    }

    void _hydrobonds1_MethodON()
    {
        hydrobonds1.SetActive(true);
    }
    void _hydrobonds1_MethodoOFF()
    {
        hydrobonds1.SetActive(false);
    }

    void _hydrogens_MethodON()
    {
        hydrogens.SetActive(true);
    }
    void _hydrogens_MethodoOFF()
    {
        hydrogens.SetActive(false);
    }






















    void _ccm_text_MethodON()
    {
        ccm_text.SetActive(true);
    }
    void _ccm_text_MethodoOFF()
    {
        ccm_text.SetActive(false);
    }


    void _carbon_text_MethodON()
    {
        carbon_text.SetActive(true);
    }
    void _carbon_text_MethodoOFF()
    {
        carbon_text.SetActive(false);
    }


    void _neon_text_MethodON()
    {
        neon_text.SetActive(true);
    }
    void _neon_text_MethodoOFF()
    {
        neon_text.SetActive(false);
    }


    void _sodium_text_MethodON()
    {
        sodium_text.SetActive(true);
    }
    void _sodium_text_MethodoOFF()
    {
        sodium_text.SetActive(false);
    }


    void _sodiumlose_MethodON()
    {
        sodiumlose.SetActive(true);
    }
    void _sodiumlose_MethodoOFF()
    {
        sodiumlose.SetActive(false);
    }


    void _hydrogen_MethodON()
    {
        hydrogen.SetActive(true);
    }
    void _hydrogen_MethodoOFF()
    {
        hydrogen.SetActive(false);
    }

    void _heliumgrp_MethodON()
    {
        heliumgrp.SetActive(true);
    }
    void _heliumgrp_MethodoOFF()
    {
        heliumgrp.SetActive(false);
    }

    void _h2dc_MethodON()
    {
        h2dc.SetActive(true);
    }
    void _h2dc_MethodoOFF()
    {
        h2dc.SetActive(false);
    }

    void _h2cov_MethodON()
    {
        h2cov.SetActive(true);
    }
    void _h2cov_MethodoOFF()
    {
        h2cov.SetActive(false);
    }

    void _chlorinedet_MethodON()
    {
        chlorinedet.SetActive(true);
    }
    void _chlorinedet_MethodoOFF()
    {
        chlorinedet.SetActive(false);
    }

    void _hydrotext_MethodON()
    {
        hydrotext.SetActive(true);
    }
    void _hydrotext_MethodoOFF()
    {
        hydrotext.SetActive(false);
    }

    void _carbon_MethodON()
    {
        carbon.SetActive(true);
    }
    void _carbon_MethodoOFF()
    {
        carbon.SetActive(false);
    }

    void _hyd1_MethodON()
    {
        hyd1.SetActive(true);
    }
    void _hyd1_MethodoOFF()
    {
        hyd1.SetActive(false);
    }

    void _hyd3_MethodON()
    {
        hyd3.SetActive(true);
    }
    void _hyd3_MethodoOFF()
    {
        hyd3.SetActive(false);
    }

    void _diatext_MethodON()
    {
        diatext.SetActive(true);
    }
    void _diatext_MethodoOFF()
    {
        diatext.SetActive(false);
    }

    void _gratext_MethodON()
    {
        gratext.SetActive(true);
    }
    void _gratext_MethodoOFF()
    {
        gratext.SetActive(false);
    }

    void _carbonform_MethodON()
    {
        carbonform.SetActive(true);
    }
    void _carbonform_MethodoOFF()
    {
        carbonform.SetActive(false);
    }

    void _graphiteHover_MethodON()
    {
        graphiteHover.SetActive(true);
    }
    void _graphiteHover_MethodoOFF()
    {
        graphiteHover.SetActive(false);
    }

    void _diamond_MethodON()
    {
        diamond.SetActive(true);
    }
    void _diamond_MethodoOFF()
    {
        diamond.SetActive(false);
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
    //
    void _D12_MethodON()
    {
        D12.SetActive(true);
    }
    void _D12_MethodoOFF()
    {
        D12.SetActive(false);
    }






































    // Audio











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


































}
