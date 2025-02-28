using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_life_processes_10_class_part2 : MonoBehaviour
{


    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    private Animator anim;


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



    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;


    public AudioClip audio51;
    public AudioClip audio52;
    public AudioClip audio53;
    public AudioClip audio54;
    public AudioClip audio55;
    public AudioClip audio56;
    public AudioClip audio57;
    public AudioClip audio58;
    public AudioClip audio59;
    public AudioClip audio59A;


    public AudioClip audio60;




    public AudioClip audio61;
    public AudioClip audio62;
    public AudioClip audio63;
    public AudioClip audio64;
    public AudioClip audio65;
    public AudioClip audio66;
    public AudioClip audio67;
    public AudioClip audio68;
    public AudioClip audio69;
    public AudioClip audio70;





    public AudioClip audio71;
    public AudioClip audio72;
    public AudioClip audio73;
    public AudioClip audio74;
    public AudioClip audio75;

    public AudioClip audio76;
    public AudioClip audio77;
    public AudioClip audio78;
    public AudioClip audio79;
    public AudioClip audio80;
    public AudioClip audio81;
    public AudioClip audio82;
    public AudioClip audio83;
    public AudioClip audio84;





    // Titles



    public GameObject TransportationT;
    public GameObject heart_structureT;
    public GameObject amphibians_and_reptilesT;
    public GameObject fishesT;
    public GameObject dobule_circuit_circulationT;
    public GameObject blood_vesselsT;
    public GameObject Blood_pressureT;
    public GameObject damaged_blood_vesselsT;
    public GameObject Lymphatic_systemT;
    public GameObject Transport_of_foodT;
    public GameObject ExcretionT;
    public GameObject different_organismsT;
    public GameObject human_beingsT;
    public GameObject Human_excretoryT;
    public GameObject Infections_in_kidneysT;
    public GameObject Excretion_In_plantsT;
    public GameObject Transportation_In_PlantsT;
    public GameObject Transport_of_waterT;




    public GameObject arteriesD;
    public GameObject pressureD;
    public GameObject phloemD;
    public GameObject translocationD;
    public GameObject urethraD;
    public GameObject kidneyD;
    public GameObject glomerulusD;
    public GameObject bowman_s_capsuleD;
    public GameObject creatinineD;
    public GameObject amino_acidsD;
    public GameObject dialysisD;








    // Text



    public GameObject right_side_chambers;
    public GameObject Right_ventricle;
    public GameObject Right_atrium;
    public GameObject Left_atrium;
    public GameObject Left_side_chambers;
    public GameObject left_ventricle;


    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camara Animation", 0, targetNormalizedTime);
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










    void _audio51_audioMethod()

    {
        myAudio.clip = audio51;
        myAudio.Play();
    }







    void _audio52_audioMethod()

    {
        myAudio.clip = audio52;
        myAudio.Play();
    }



    void _audio53_audioMethod()

    {
        myAudio.clip = audio53;
        myAudio.Play();
    }



    void _audio54_audioMethod()

    {
        myAudio.clip = audio54;
        myAudio.Play();
    }



    void _audio55_audioMethod()

    {
        myAudio.clip = audio55;
        myAudio.Play();
    }



    void _audio56_audioMethod()

    {
        myAudio.clip = audio56;
        myAudio.Play();
    }



    void _audio57_audioMethod()

    {
        myAudio.clip = audio57;
        myAudio.Play();
    }



    void _audio58_audioMethod()

    {
        myAudio.clip = audio58;
        myAudio.Play();
    }



    void _audio59_audioMethod()

    {
        myAudio.clip = audio59;
        myAudio.Play();
    }




    void _audio59A_audioMethod()

    {
        myAudio.clip = audio59A;
        myAudio.Play();
    }
















    void _audio60_audioMethod()

    {
        myAudio.clip = audio60;
        myAudio.Play();
    }






    void _audio61_audioMethod()

    {
        myAudio.clip = audio61;
        myAudio.Play();
    }

    void _audio62_audioMethod()

    {
        myAudio.clip = audio62;
        myAudio.Play();
    }

    void _audio63_audioMethod()

    {
        myAudio.clip = audio63;
        myAudio.Play();
    }

    void _audio64_audioMethod()

    {
        myAudio.clip = audio64;
        myAudio.Play();
    }

    void _audio65_audioMethod()

    {
        myAudio.clip = audio65;
        myAudio.Play();
    }

    void _audio66_audioMethod()

    {
        myAudio.clip = audio66;
        myAudio.Play();
    }

    void _audio67_audioMethod()

    {
        myAudio.clip = audio67;
        myAudio.Play();
    }

    void _audio68_audioMethod()

    {
        myAudio.clip = audio68;
        myAudio.Play();
    }

    void _audio69_audioMethod()

    {
        myAudio.clip = audio69;
        myAudio.Play();
    }

    void _audio70_audioMethod()

    {
        myAudio.clip = audio70;
        myAudio.Play();
    }

    void _audio71_audioMethod()

    {
        myAudio.clip = audio71;
        myAudio.Play();
    }

    void _audio72_audioMethod()

    {
        myAudio.clip = audio72;
        myAudio.Play();
    }

    void _audio73_audioMethod()

    {
        myAudio.clip = audio73;
        myAudio.Play();
    }

    void _audio74_audioMethod()

    {
        myAudio.clip = audio74;
        myAudio.Play();
    }

    void _audio75_audioMethod()

    {
        myAudio.clip = audio75;
        myAudio.Play();
    }











    void _audio76_audioMethod()

    {
        myAudio.clip = audio76;
        myAudio.Play();
    }
    void _audio77_audioMethod()

    {
        myAudio.clip = audio77;
        myAudio.Play();
    }
    void _audio78_audioMethod()

    {
        myAudio.clip = audio78;
        myAudio.Play();

    }





    void _audio79_audioMethod()

    {
        myAudio.clip = audio79;
        myAudio.Play();
    }




    void _audio80_audioMethod()

    {
        myAudio.clip = audio80;
        myAudio.Play();
    }





















    void _audio81_audioMethod()

    {
        myAudio.clip = audio81;
        myAudio.Play();
    }
    void _audio82_audioMethod()

    {
        myAudio.clip = audio82;
        myAudio.Play();
    }
    void _audio83_audioMethod()

    {
        myAudio.clip = audio83;
        myAudio.Play();
    }


    void _audio84_audioMethod()

    {
        myAudio.clip = audio84;
        myAudio.Play();
    }











    // Titles


    void _TransportationTMethodON()
    {
        TransportationT.SetActive(true);
    }

    void _TransportationTMethodOFF()
    {
        TransportationT.SetActive(false);
    }






    void _heart_structureTMethodON()
    {
        heart_structureT.SetActive(true);
    }

    void _heart_structureTMethodOFF()
    {
        heart_structureT.SetActive(false);
    }






   


    void _amphibians_and_reptilesTMethodON()
    {
        amphibians_and_reptilesT.SetActive(true);
    }

    void _amphibians_and_reptilesTMethodOFF()
    {
        amphibians_and_reptilesT.SetActive(false);
    }




    void _fishesTMethodON()
    {
        fishesT.SetActive(true);
    }

    void _fishesTMethodOFF()
    {
        fishesT.SetActive(false);
    }







    void _dobule_circuit_circulationTMethodON()
    {
        dobule_circuit_circulationT.SetActive(true);
    }

    void _dobule_circuit_circulationTMethodOFF()
    {
        dobule_circuit_circulationT.SetActive(false);
    }






    void _blood_vesselsTMethodON()
    {
        blood_vesselsT.SetActive(true);
    }

    void _blood_vesselsTMethodOFF()
    {
        blood_vesselsT.SetActive(false);
    }




    void _Blood_pressureTMethodON()
    {
        Blood_pressureT.SetActive(true);
    }

    void _Blood_pressureTMethodOFF()
    {
        Blood_pressureT.SetActive(false);
    }






    void _damaged_blood_vesselsTMethodON()
    {
        damaged_blood_vesselsT.SetActive(true);
    }

    void _damaged_blood_vesselsTMethodOFF()
    {
        damaged_blood_vesselsT.SetActive(false);
    }



    void _Lymphatic_systemTMethodON()
    {
        Lymphatic_systemT.SetActive(true);
    }

    void _Lymphatic_systemTMethodOFF()
    {
        Lymphatic_systemT.SetActive(false);
    }



    void _Transport_of_foodTMethodON()
    {
        Transport_of_foodT.SetActive(true);
    }

    void _Transport_of_foodTMethodOFF()
    {
        Transport_of_foodT.SetActive(false);
    }




    void _ExcretionTMethodON()
    {
        ExcretionT.SetActive(true);
    }

    void _ExcretionTMethodOFF()
    {
        ExcretionT.SetActive(false);
    }


    void _different_organismsTMethodON()
    {
        different_organismsT.SetActive(true);
    }

    void _different_organismsTMethodOFF()
    {
        different_organismsT.SetActive(false);
    }


    void _human_beingsTMethodON()
    {
        human_beingsT.SetActive(true);
    }

    void _human_beingsTMethodOFF()
    {
        human_beingsT.SetActive(false);
    }


    void _Human_excretoryTMethodON()
    {
        Human_excretoryT.SetActive(true);
    }

    void _Human_excretoryTMethodOFF()
    {
        Human_excretoryT.SetActive(false);
    }


    void _Infections_in_kidneysTMethodON()
    {
        Infections_in_kidneysT.SetActive(true);
    }

    void _Infections_in_kidneysTMethodOFF()
    {
        Infections_in_kidneysT.SetActive(false);
    }



    void _Excretion_In_plantsTMethodON()
    {
        Excretion_In_plantsT.SetActive(true);
    }

    void _Excretion_In_plantsTMethodOFF()
    {
        Excretion_In_plantsT.SetActive(false);
    }

    void _Transportation_In_PlantsTMethodON()
    {
        Transportation_In_PlantsT.SetActive(true);
    }

    void _Transportation_In_PlantsTMethodOFF()
    {
        Transportation_In_PlantsT.SetActive(false);
    }

    void _Transport_of_waterTMethodON()
    {
        Transport_of_waterT.SetActive(true);
    }

    void _Transport_of_waterTMethodOFF()
    {
        Transport_of_waterT.SetActive(false);
    }














    void _arteriesDMethodON()
    {
        arteriesD.SetActive(true);
    }

    void _arteriesDMethodOFF()
    {
        arteriesD.SetActive(false);
    }



    void _pressureDMethodON()
    {
        pressureD.SetActive(true);
    }

    void _pressureDMethodOFF()
    {
        pressureD.SetActive(false);
    }



    void _phloemDMethodON()
    {
        phloemD.SetActive(true);
    }

    void _phloemDMethodOFF()
    {
        phloemD.SetActive(false);
    }



    void _translocationDMethodON()
    {
        translocationD.SetActive(true);
    }

    void _translocationDMethodOFF()
    {
        translocationD.SetActive(false);
    }



    void _urethraDMethodON()
    {
        urethraD.SetActive(true);
    }

    void _urethraDMethodOFF()
    {
        urethraD.SetActive(false);
    }



    void _kidneyDMethodON()
    {
        kidneyD.SetActive(true);
    }

    void _kidneyDMethodOFF()
    {
        kidneyD.SetActive(false);
    }



    void _glomerulusDMethodON()
    {
        glomerulusD.SetActive(true);
    }

    void _glomerulusDMethodOFF()
    {
        glomerulusD.SetActive(false);
    }






    void _bowman_s_capsuleDMethodON()
    {
        bowman_s_capsuleD.SetActive(true);
    }

    void _bowman_s_capsuleDMethodOFF()
    {
        bowman_s_capsuleD.SetActive(false);
    }



    void _creatinineDMethodON()
    {
        creatinineD.SetActive(true);
    }

    void _creatinineDMethodOFF()
    {
        creatinineD.SetActive(false);
    }



    void _amino_acidsDMethodON()
    {
        amino_acidsD.SetActive(true);
    }

    void _amino_acidsDMethodOFF()
    {
        amino_acidsD.SetActive(false);
    }



    void _dialysisDMethodON()
    {
        dialysisD.SetActive(true);
    }

    void _dialysisDMethodOFF()
    {
        dialysisD.SetActive(false);
    }



















    // Text


    void _right_side_chambersMethodON()
    {
        right_side_chambers.SetActive(true);
    }

    void _right_side_chambersMethodOFF()
    {
        right_side_chambers.SetActive(false);
    }





    void _Right_ventricleMethodON()
    {
        Right_ventricle.SetActive(true);
    }

    void _Right_ventricleMethodOFF()
    {
        Right_ventricle.SetActive(false);
    }


    void _Right_atriumMethodON()
    {
        Right_atrium.SetActive(true);
    }

    void _Right_atriumMethodOFF()
    {
        Right_atrium.SetActive(false);
    }


    void _Left_atriumMethodON()
    {
        Left_atrium.SetActive(true);
    }

    void _Left_atriumMethodOFF()
    {
        Left_atrium.SetActive(false);
    }


    void _Left_side_chambersMethodON()
    {
        Left_side_chambers.SetActive(true);
    }

    void _Left_side_chambersMethodOFF()
    {
        Left_side_chambers.SetActive(false);
    }


    void _left_ventricleMethodON()
    {
        left_ventricle.SetActive(true);
    }

    void _left_ventricleMethodOFF()
    {
        left_ventricle.SetActive(false);
    }




    public void GotoMainMenu()
    {
        
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }
































}
