using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sfx_Heart : MonoBehaviour
{
    [Header("Exp Assets")]
    public GameObject Skeleton;
    public GameObject CardiacMuscle;
    public GameObject ClosedHeart;
    public GameObject OpenHeart;
    public GameObject Fist;



    [Header("Lines & Texts")]
    public GameObject Line_pulVal; 
    public GameObject Line_MitralVal; 
    public GameObject Line_TrisucpidVal; 
    public GameObject Line_AorticVal; 
    public GameObject Line_Septum; 
    public GameObject Line_Chambers;  
    public GameObject Line_Aota; 
    public GameObject Line_endo; 
    public GameObject Line_myo; 
    public GameObject Line_epi; 
    public GameObject Line_sternum;  
    public GameObject Arrow1;
    public GameObject Arrow2;
    public GameObject Arrow3;
    public GameObject Arrow4;

    [Header("cutscene mp3")]
    public AudioSource myAudio;

    public AudioClip title;
    public AudioClip exp1;
    public AudioClip exp2;
    public AudioClip exp3;
    public AudioClip SOH_title;
    public AudioClip SOH_exp1;
    public AudioClip SOH_exp2; 
    public AudioClip Valve_title;
    public AudioClip Valve_exp1; 
    public AudioClip Valve_4; 
    public AudioClip Valve_tricuspid; 
    public AudioClip Valve_aortic; 
    public AudioClip Valve_mitral;
    public AudioClip Valve_pulmonary; 
    public AudioClip CM_title;
    public AudioClip CM_exp1;
    public AudioClip CM_types;
    public AudioClip FOH_title;
    public AudioClip FOH_exp1;
    public AudioClip FOH_exp2;
    public AudioClip FOH_exp3;
    public AudioClip FOH_exp4;
    public AudioClip FOH_exp5;
    public AudioClip FOH_exp6;
    public AudioClip FOH_exp7;
    public AudioClip CD_title;
    public AudioClip CD_exp1;
    public AudioClip CD_exp2;
    public AudioClip CD_exp3;
    

    //=============== TOGGLES ===============//
    
    void Fist_ON(){
        Fist.SetActive(true);
    } 
    void Fist_OFF(){
        Fist.SetActive(false);
    } 

    void Skeleton_ON(){
        Skeleton.SetActive(true);
    }
    
    void Skeleton_OFF(){
        Skeleton.SetActive(false);
    }

    void Heart_ON(){
         ClosedHeart.SetActive(true);
    }
    void Heart_OFF(){
         ClosedHeart.SetActive(false);
    }
    void OpenHeart_ON(){
        OpenHeart.SetActive(true);
    }
    void OpenHeart_OFF(){
        OpenHeart.SetActive(false);
    }

    void CardiaMuscle_ON(){
        CardiacMuscle.SetActive(true);
    }
    void CardiaMuscle_OFF(){
        CardiacMuscle.SetActive(false);
    }

    //========================  VOICES =====================//
    
        void _title_Method()
    {
        myAudio.clip = title;
        myAudio.Play();
    }
        void _exp1_Method()
    {
        myAudio.clip = exp1;
        myAudio.Play();
    }
        void _exp2_Method()
    {
        myAudio.clip = exp2;
        myAudio.Play();
    }
        void _exp3_Method()
    {
        myAudio.clip = exp3;
        myAudio.Play();
        //line
        Line_sternum.SetActive(true);
    }
        void _SOH_title_Method()
    {
        myAudio.clip = SOH_title;
        myAudio.Play();
    }
        void _SOH_exp1_Method()
    {
        myAudio.clip = SOH_exp1;
        myAudio.Play();
        //line
        Line_Chambers.SetActive(true);
        Line_Aota.SetActive(true);
    }
        void _SOH_exp2_Method()
    {
        myAudio.clip = SOH_exp2; 
        myAudio.Play();
        //line
        Line_Septum.SetActive(true);
    }
        void _Valve_title_Method()
    {
        myAudio.clip = Valve_title;
        myAudio.Play();
    }
        void _Valve_exp1_Method()
    {
        myAudio.clip = Valve_exp1; 
        myAudio.Play();
    }
        void _Valve_4_Method()
    {
        myAudio.clip = Valve_4; 
        myAudio.Play();
    }
        void _Valve_tricuspid_Method()
    {
        myAudio.clip = Valve_tricuspid; 
        myAudio.Play();
        //line
        Line_TrisucpidVal.SetActive(true);
    }
        void _Valve_aortic_Method()
    {
        myAudio.clip = Valve_aortic; 
        myAudio.Play();
        //line
        Line_AorticVal.SetActive(true);
    }
        void _Valve_mitral_Method()
    {
        myAudio.clip = Valve_mitral; 
        myAudio.Play();
        //line
        Line_MitralVal.SetActive(true);
    }
        void _Valve_pulmonary_Method()
    {
        myAudio.clip = Valve_pulmonary; 
        myAudio.Play();
        //line
        Line_pulVal.SetActive(true);
    }
        void _CM_title_Method()
    {
        myAudio.clip = CM_title;
        myAudio.Play();
    }
        void _CM_exp_Method()
    {
        myAudio.clip = CM_exp1;
        myAudio.Play();
    }
        void _CM_types_Method()
    {
        myAudio.clip = CM_types;
        myAudio.Play();
        //line
        Line_epi.SetActive(true);
    }
        void _FOH_title_Method()
    {
        myAudio.clip = FOH_title;
        myAudio.Play();
        //line
        Line_epi.SetActive(false);
        Line_endo.SetActive(false);
        Line_myo.SetActive(false);
    }
        void _FOH_exp1_Method()
    {
        myAudio.clip = FOH_exp1;
        myAudio.Play();
    }
        void _FOH_exp2_Method()
    {
        myAudio.clip = FOH_exp2;
        myAudio.Play();
    }
        void _FOH_exp3_Method()
    {
        myAudio.clip = FOH_exp3;
        myAudio.Play();
    }
        void _FOH_exp4_Method()
    {
        myAudio.clip = FOH_exp4;
        myAudio.Play();
    }
        void _FOH_exp5_Method()
    {
        myAudio.clip = FOH_exp5;
        myAudio.Play();
    }
        void _FOH_exp6_Method()
    {
        myAudio.clip = FOH_exp6;
        myAudio.Play();
    }
        void _FOH_exp7_Method()
    {
        myAudio.clip = FOH_exp7;
        myAudio.Play();
    }
        void _CD_title_Method()
    {
        myAudio.clip = CD_title;
        myAudio.Play();
    }
        void _CD_exp1_Method()
    {
        myAudio.clip = CD_exp1;
        myAudio.Play();
    }
        void _CD_exp2_Method()
    {
        myAudio.clip = CD_exp2;
        myAudio.Play();
    }
        void _CD_exp3_Method()
    {
        myAudio.clip = CD_exp3;
        myAudio.Play();
    }


    // Other voice delayed line methods
        void arrow1(){
            Arrow1.SetActive(true);
        }
        void arrow2(){
            Arrow2.SetActive(true); 
        }
        void arrow3(){
            Arrow3.SetActive(true); 
        }
        void arrow4(){
            Arrow4.SetActive(true); 
        } 


        //GoTo GamePlay
        public void GoToGamePlay(){
            //Load objective into static variable
            StaticVariables.Human_Objective = "Heart";
            //open game scene 
            GameObject loader = GameObject.Find("Sceneloader Canvas");
            loader.GetComponent<SceneLoader>().LoadScene(4);
            //SceneManager.LoadScene("HumanBody"); 
    }


}
