using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sfx_dcmotor : MonoBehaviour
{

    // public AudioClip otherClip;


    [Header("Lines & Texts")]
    public GameObject Linestator;
    public GameObject Lineshaft;
    public GameObject LineMagneticfield;
    public GameObject LineCurrent;
    public GameObject LineForce;
    public GameObject Linearmature;
    public GameObject Linewinding;
    public GameObject Linepoleshoe;
    public GameObject Linecommutator;
    public GameObject Linebrush;
    public GameObject Directions;
    public GameObject ArmatureCurrentDirections;
    public GameObject ArmatureMagneticDirections;
    public GameObject Charges;

    private Animator anim;

    [Header("Animations")]
    public GameObject Stator;
    public GameObject Shaft;
    public GameObject Brush;
    public GameObject Commutator;
    public GameObject PoleShoe;
    public GameObject Rotor;
    public GameObject Winding;
    public GameObject ArmatureCloseup;

    public GameObject SolarPanelScript;


    [Header("cutscene mp3")]
    public AudioSource myAudio;

    public AudioClip definition;
    public AudioClip motorprinciple;
    public AudioClip northandsouthpole;
    public AudioClip positivetonegative;
    public AudioClip fleminglefthand;
    public AudioClip fleminglefthandExp;
    public AudioClip stator;
    public AudioClip shaft;
    public AudioClip rotor;
    public AudioClip armature;
    public AudioClip winding;
    public AudioClip poleshoe;
    public AudioClip commutator;
    public AudioClip brush;
    public AudioClip FlemingInMotor;
    public AudioClip FlemingInMotorExplanation;

    

    //public GameObject lights;



    // Start is called before the first frame update
    // void Start()
    //{

    //restartClass
    //  Button btn1 = ClassButton.GetComponent<Button>();
    //btn1.onClick.AddListener(RestartClass);

    //goto gameplay
    //Button btn = GameplayButton.GetComponent<Button>();
    //btn.onClick.AddListener(TaskOnClick);

    //restartMain
    //Button btn2 = RestartButton.GetComponent<Button>();
    //btn2.onClick.AddListener(RestartMainMenu);


    //}



    /* void TaskOnClick()
     {
         _Switch.SetActive(true);
         _MainCamera.SetActive(true);
         _all.SetActive(true);
         _SwitchtoClass.SetActive(true);
         //turnoff bladehead, camera
         _BladeHead.SetActive(false);
         _Camera.SetActive(false);
         _gameplay.SetActive(false);

         //mobile controller true
         MobileControlsUI.SetActive(true);

     }*/

    // Update is called once per frame
    void Update()
    {

    }

     //======================== Audios ======================

    void _definitonmethod()
    {
        myAudio.clip = definition;
        myAudio.Play();
    }

    void _motorprinciplemethod()
    {
        myAudio.clip = motorprinciple;
        myAudio.Play();
    }

    void northandsouthpolemethod()
    {
        myAudio.clip = northandsouthpole;
        myAudio.Play();
    }
    void positivetonegativemethod()
    {
        myAudio.clip = positivetonegative;
        myAudio.Play();
    }
    void fleminglefthandmethod()
    {
        myAudio.clip = fleminglefthand;
        myAudio.Play();
    }
        void fleminglefthandExp_method()
    {
        myAudio.clip = fleminglefthandExp;
        myAudio.Play();
    } 
    void _FlemingInMotormethod()
    {
        myAudio.clip = FlemingInMotor;
        myAudio.Play();
    }

     
    void _FlemingInMotorExplanationmethod()
    {
        myAudio.clip = FlemingInMotorExplanation;
        myAudio.Play();
    }





    void statormethod()
    {
        myAudio.clip = stator;
        myAudio.Play();

    }
      void shaftmethod()
    {
        myAudio.clip = shaft;
        myAudio.Play();
    }
    void rotormethod()
    {
        myAudio.clip = rotor;
        myAudio.Play();

    }
    void armaturemethod()
    {
        myAudio.clip = armature;
        myAudio.Play();
    }
    void windingmethod()
    {
        myAudio.clip = winding;
        myAudio.Play();
    }
    void poleshoemethod()
    {
        myAudio.clip = poleshoe;
        myAudio.Play();
    }
    void commutatormethod()
    {
        myAudio.clip = commutator;
        myAudio.Play();
    }
    void brushmethod()
    {
        myAudio.clip = brush;
        myAudio.Play();
    }

    //======================== Animation s======================

    void StatorAnimmethod()
    {

        anim = Stator.GetComponent<Animator>();
        anim.Play("stator _stator");
    }

    void StatorSideAnimmethod()
    {

        anim = Stator.GetComponent<Animator>();
        anim.Play("Stator Side Animation");
    }

    void ShaftAnimmethod()
    {
        
        anim = Shaft.GetComponent<Animator>();
        anim.Play("shaft_shaft");

    }

    void BrushAnimmethod()
    {

        anim = Brush.GetComponent<Animator>();
        anim.Play("brush");

    }

    void CommutatorAnimmethod()
    {

        anim = Commutator.GetComponent<Animator>();
        anim.Play("commutator_commutatorAction");

    }

    void PoleShoeAnimmethod()
    {

        anim = PoleShoe.GetComponent<Animator>();
        anim.Play("pole_pole shoe");

    }

    void RotorAnimmethod()
    {

        anim = Rotor.GetComponent<Animator>();
        anim.Play("rotor_actions_rotor_animations");

    }

    void WindingAnimmethod()
    {

        anim = Winding.GetComponent<Animator>();
        anim.Play("field winding_winding");

    }

    void ArmatureCloseupAnimmethod()
    {

        anim = ArmatureCloseup.GetComponent<Animator>();
        anim.Play("Armature closeup");

    }


  

    void _LineFlemingmethodon()
    {
        LineMagneticfield.SetActive(true);
        LineCurrent.SetActive(true);
        LineForce.SetActive(true);
    }
    void _LineFlemingmethodoff()
    {
        LineMagneticfield.SetActive(false);
        LineCurrent.SetActive(false);
        LineForce.SetActive(false);
    }

    void _Linestatormethodon()
    {
        Linestator.SetActive(true);
    }
    void _Linestatormethodoff()
    {
        Linestator.SetActive(false);
    }
    void _Lineshaftmethodon()
    {
        Lineshaft.SetActive(true);
    }
    void _Lineshaftmethodoff()
    {
        Lineshaft.SetActive(false);
    }
    
    void _Linearmaturemethodon()
    {
        Linearmature.SetActive(true);
    }
    void _Linearmaturemethodoff()
    {
        Linearmature.SetActive(false);
    }
    void _Linewindingmethodon()
    {
        Linewinding.SetActive(true);
    }
    void _Linewindingmethodoff()
    {
        Linewinding.SetActive(false);
    }
    void _Linepoleshoemethodon()
    {
        Linepoleshoe.SetActive(true);
    }
    void _Linepoleshoemethodoff()
    {
        Linepoleshoe.SetActive(false);
    }
    void _Linecommutatormethodon()
    {
        Linecommutator.SetActive(true);
    }
    void _Linecommutatormethodoff()
    {
        Linecommutator.SetActive(false);
    }
    void _Linebrushmethodon()
    {
        Linebrush.SetActive(true);
    }
    void _Linebrushmethodoff()
    {
        Linebrush.SetActive(false);
    }

    void _Directionsmethodon()
    {
        Directions.SetActive(true);
    }
    void _Directionsmethodoff()
    {
        Directions.SetActive(false);
    }

    void _ArmatureCurrentDirectionsmethodon()
    {
        ArmatureCurrentDirections.SetActive(true);
        ArmatureMagneticDirections.SetActive(true);
        Charges.SetActive(true);

    }
    void _ArmatureCurrentDirectionsmethodoff()
    {
        ArmatureCurrentDirections.SetActive(false);
        ArmatureMagneticDirections.SetActive(false);
        Charges.SetActive(false);
    }


    void GotoSolarGameplay(){
        SolarPanelScript.GetComponent<sfx_Solarenergy>()._ResetnInitialize();
    }


}
