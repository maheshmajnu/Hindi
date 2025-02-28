using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thiedlaw : MonoBehaviour
{

    [Header("Audio files")]
    public AudioSource myAudio;
    public AudioClip title;
    public AudioClip exp1;
    public AudioClip exp2;
    public AudioClip ex1_exp1;
    public AudioClip ex1_exp2;
    public AudioClip ex2_exp; 

    [Header("Explanation GameObjects")]
    public GameObject titlePhoto;
    public GameObject Statement;

    
    public GameObject Scene_exp1;
    public GameObject Scene_exp2;
    public GameObject Scene_CamExp2;


    [Header("Animations")]
    private Animator anim; 
    public GameObject gunner;
   


     void _TitleMethod()
    {
        myAudio.clip = title;
        myAudio.Play();
    }

     void _exp1Method()
    {
        myAudio.clip = exp1;
        myAudio.Play();
    }

    void _exp2Method()
    {
        titlePhoto.SetActive(false);
        myAudio.clip = exp2;
        myAudio.Play();
        Statement.SetActive(true);
    } 

    void _ex1_exp1Method()
    {
        myAudio.clip = ex1_exp1;
        myAudio.Play();
        //gunner
        anim = gunner.GetComponent<Animator>();
        anim.Play("Gun Shoot");
      
    }

    void _ex1_exp2Method()
    {
        myAudio.clip = ex1_exp2;
        myAudio.Play();
    }

    void _ex2_expMethod()
    {
        myAudio.clip = ex2_exp;
        myAudio.Play();
    }

    void _Start_Explanatory2(){
      Scene_exp1.SetActive(false);
      Scene_exp2.SetActive(true);
      Scene_CamExp2.SetActive(true);
    }
 
}
