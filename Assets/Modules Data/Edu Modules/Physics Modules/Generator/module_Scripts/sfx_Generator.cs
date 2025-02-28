using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sfx_Generator : MonoBehaviour
{ 

    // public AudioClip otherClip;


    [Header("Lines & Texts")]
    public GameObject LineStator;
    public GameObject LinePoleshoe;
    public GameObject LineShaft;
    public GameObject LineArmature;
    public GameObject LineWinding;
    public GameObject LineCommutator;
    public GameObject LineBrushes;
    public GameObject LineCopperwire;
    public GameObject LineMagnets;
    public GameObject LineCurrent;
    public GameObject LineMagneticField;
    public GameObject LineForce;


    [Header("cutscene mp3")]
    public AudioSource myAudio;

    public AudioClip s_Intro;
    public AudioClip s_Definition;
    public AudioClip s_Stator;
    public AudioClip s_Poleshoe;
    public AudioClip s_Shaft;
    public AudioClip s_Armature;
    public AudioClip s_Rotor;
    public AudioClip s_Fieldwinding;
    public AudioClip s_Commutator;
    public AudioClip s_Brushes;
    public AudioClip s_Flemingsrule;
    public AudioClip s_Principle;

    private Animator anim;

    [Header("Explanation anims")]

    public GameObject Stator;
    public GameObject Poleshoe;
    public GameObject Shaft;
    public GameObject Armature;
    public GameObject Commutator;
    public GameObject Brushes;
    public GameObject Copperwire;



    [Header("Gameplay assets")]
    private GameObject mainPlayer;
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public Camera MainCam;


    public string trigname;
    public string Interactable_ColliderName;

    [Header("Gameplay-Triggers")]
    //triggers and objects
    public GameObject Motor_boat;
    public GameObject solarpanel;

    // [Header("Lines&texts")]
    private string[] miniObjectives = { "Find the Motor.", "Find the Solar Panel.", "Power Up The Boat", "Push Boat onto Sea" }; private int array_i = 1;

    [Header("Gameplay-canvas")]
    public GameObject Objective_canvas;
    public GameObject UI_assets;
    //public TextMeshProUGUI Objective;
    //public TextMeshProUGUI Steps;
   //public TextMeshProUGUI color;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;


    public AudioClip game_bgm;
    public GameObject ExpCam;



    public Button GameplayButton;
    public Button ClassButton;
    public Button RestartButton;
    public GameObject MobileControlsUI;

    public GameObject HydroElectricityScript;






    // Update is called once per frame
    void Update()
    {

    }

    void _Intromethod()
    {
        myAudio.clip = s_Intro;
        myAudio.Play();
    }

    void _Definitionmethod()
    {
        myAudio.clip = s_Definition;
        myAudio.Play();
    }

    void _Statormethod()
    {
        myAudio.clip = s_Stator;
        myAudio.Play();

    }

    void _Shaftmethod()
    {
        myAudio.clip = s_Shaft;
        myAudio.Play();
    }

    void _Rotormethod()
    {
        myAudio.clip = s_Rotor;
        myAudio.Play();
    }

    void _Armaturemethod()
    {
        myAudio.clip = s_Armature;
        myAudio.Play();
    }

    void _Fieldwindingmethod()
    {
        myAudio.clip = s_Fieldwinding;
        myAudio.Play();
    }


    void _Poleshoemethod()
    {
        myAudio.clip = s_Poleshoe;
        myAudio.Play();

    }



    void _Commutatormethod()
    {
        myAudio.clip = s_Commutator;
        myAudio.Play();

    }

    void _Brushesmethod()
    {
        myAudio.clip = s_Brushes;
        myAudio.Play();
    }

    void _Flemingsrulemethod()
    {
        myAudio.clip = s_Flemingsrule;
        myAudio.Play();
    }

    void _Principlemethod()
    {
        myAudio.clip = s_Principle;
        myAudio.Play();
    }





    void _LineFaradaymethodon()
    {
        LineCurrent.SetActive(true);
        LineMagneticField.SetActive(true);
        LineForce.SetActive(true);
    }
    void _LineFaradaymethodoff()
    {
        LineCurrent.SetActive(false);
        LineMagneticField.SetActive(false);
        LineForce.SetActive(false);
    }

    void _LineStatormethodon()
    {
        LineStator.SetActive(true);
    }
    void _LineStatormethodoff()
    {
        LineStator.SetActive(false);
    }

    void _LinePoleshoemethodon()
    {
        LinePoleshoe.SetActive(true);
    }
    void _LinePoleshoemethodoff()
    {
        LinePoleshoe.SetActive(false);
    }

    void _LineShaftmethodon()
    {
        LineShaft.SetActive(true);
    }
    void _LineShaftmethodoff()
    {
        LineShaft.SetActive(false);
    }

    void _LineArmaturemethodon()
    {
        LineArmature.SetActive(true);
    }
    void _LineArmaturemethodoff()
    {
        LineArmature.SetActive(false);
    }


    void _LineWindingmethodon()
    {
        LineWinding.SetActive(true);
    }
    void _LineWindingmethodoff()
    {
        LineWinding.SetActive(false);
    }


    void _LineCommutatormethodon()
    {
        LineCommutator.SetActive(true);
    }
    void _LineCommutatormethodoff()
    {
        LineCommutator.SetActive(false);
    }

    void _LineBrushesmethodon()
    {
        LineBrushes.SetActive(true);
    }
    void _LineBrushesmethodoff()
    {
        LineBrushes.SetActive(false);
    }

    void _LineCopperwiremethodon()
    {
        LineCopperwire.SetActive(true);
    }
    void _LineCopperwiremethodoff()
    {
        LineCopperwire.SetActive(false);
    }


    void _StatoranimatedMethod()
    {
        anim = Stator.GetComponent<Animator>();
        anim.Play("Stator");
    }

    void _PoleshoeanimatedMethod()
    {
        anim = Poleshoe.GetComponent<Animator>();
        anim.Play("Poleshoe");
    }

    void _ShaftanimatedMethod()
    {
        anim = Shaft.GetComponent<Animator>();
        anim.Play("Shaft");
    }

    void _ArmatureanimatedMethod()
    {
        anim = Armature.GetComponent<Animator>();
        anim.Play("Armature");
    }

    void _CommutatoranimatedMethod()
    {
        anim = Commutator.GetComponent<Animator>();
        anim.Play("Commutator");
    }

    void _BrushesanimatedMethod()
    {
        anim = Brushes.GetComponent<Animator>();
        anim.Play("Brushess");
    }

    void _CopperwireanimatedMethod()
    {
        anim = Copperwire.GetComponent<Animator>();
        anim.Play("Copperwire");
    }



    void GotoHydroGameplay()
    {
        HydroElectricityScript.GetComponent<sfx>().Reset_n_Initialize();
    }


    

}
