using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx_Spacelife : MonoBehaviour
{

    public int speedConstant=0;

    [SerializeField]
    private GameObject Mercury;
    [SerializeField]
    private GameObject Venus;
    [SerializeField]
    private GameObject Earth;
    [SerializeField]
    private GameObject Mars;
    [SerializeField]
    private GameObject Jupiter;
    [SerializeField]
    private GameObject Satrun;
    [SerializeField]
    private GameObject Uranus;
    [SerializeField]
    private GameObject Pluto;

    private bool explation;
    [SerializeField]
    private GameObject ExpCam_Origin;
    [SerializeField]
    private GameObject ExpCam;
    private GameObject targetPlanet;
    [SerializeField]
    private GameObject PlanetLables;

    [Header("cutscene mp3")]
    public AudioSource myAudio;

    public AudioClip Mercury_mp3;
    public AudioClip Venus_mp3;
    public AudioClip Earth_mp3;
    public AudioClip Mars_mp3;
    public AudioClip Jupiter_mp3;
    public AudioClip Satrun_mp3;
    public AudioClip Uranus_mp3;
    public AudioClip Pluto_mp3;


    
    void Start()
    { 
            StartCoroutine(TurnOnAfterTime());
    }

//bug fix for orbit convex trigger OnEnter trigger byDefault ON
    IEnumerator TurnOnAfterTime()
    { 
        yield return new WaitForSeconds(0.5f); //waits time T seconds
        gameObject.GetComponent<SPACE_PlayerMove>().runSpeed = 50f;
        gameObject.GetComponent<SPACE_PlayerMove>().OrbitEnterSpeed();
        yield break;
    }

    void FixedUpdate(){

        if(explation){ 
            PlanetLables.SetActive(false);
            ExpCam.SetActive(true);
            CamAnimation();
        }

    }
 

        void CamAnimation(){

        ExpCam.transform.LookAt(targetPlanet.transform);

       Vector3 a =  ExpCam.transform.position;
       Vector3 b =  ExpCam_Origin.transform.position; 
       ExpCam.transform.position = Vector3.MoveTowards(a,b,2.8f);

       if(ExpCam.transform.position == b) {
        explation = false;
        ExpCam.transform.position = ExpCam_Origin.transform.position;
        ExpCam.SetActive(false);PlanetLables.SetActive(true);
       }
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {  
            if (other.gameObject.name == "SpeedLow") {
                speedConstant=1;
                //decrease rocket speed
                SlowDownRocket();
            }
            

 /* Mercury START */              
            else if (other.gameObject.name == "Mercury Trigger") //heart-High BP Table trigger name is Heart
            {   
                //audio play 
                myAudio.clip = Mercury_mp3;
                myAudio.Play();

                targetPlanet = Mercury;
                ExpCam.transform.position = targetPlanet.transform.position + new Vector3(0f,10f,-50f);
                explation =true;
                //for alphaV1 destroy this trigger
                Destroy(other.gameObject);
            }  
 /* Mercury END */ 
  /* Venus START */              
            else if (other.gameObject.name == "Venus Trigger") //heart-High BP Table trigger name is Heart
            {   
                //audio play 
                myAudio.clip = Venus_mp3;
                myAudio.Play();

                targetPlanet = Venus;
                ExpCam.transform.position = targetPlanet.transform.position + new Vector3(0f,10f,-50f);
                explation =true;
                //for alphaV1 destroy this trigger
                Destroy(other.gameObject);
            }  
 /* Venus END */ 
  /* Earth START */              
            else if (other.gameObject.name == "Earth Trigger") //heart-High BP Table trigger name is Heart
            {   
                //audio play 
                myAudio.clip = Earth_mp3;
                myAudio.Play();

                targetPlanet = Earth;
                ExpCam.transform.position = targetPlanet.transform.position + new Vector3(0f,10f,-50f);
                explation =true;
                //for alphaV1 destroy this trigger
                Destroy(other.gameObject);
            }  
 /* Earth END */ 
 /* Mars START */              
            else if (other.gameObject.name == "Mars Trigger") //heart-High BP Table trigger name is Heart
            {   
                //audio play 
                myAudio.clip = Mars_mp3;
                myAudio.Play();

                targetPlanet = Mars;
                ExpCam.transform.position = targetPlanet.transform.position + new Vector3(0f,10f,-50f);
                explation =true;
                //for alphaV1 destroy this trigger
                Destroy(other.gameObject);
            }  
 /* Mars END */  
  /* Jupiter START */              
            else if (other.gameObject.name == "Jupiter Trigger") //heart-High BP Table trigger name is Heart
            {   
                //audio play 
                myAudio.clip = Jupiter_mp3;
                myAudio.Play();

                targetPlanet = Jupiter;
                ExpCam.transform.position = targetPlanet.transform.position + new Vector3(0f,10f,-50f);
                explation =true;
                //for alphaV1 destroy this trigger
                Destroy(other.gameObject);
            }  
 /* Jupiter END */ 
  /* Saturn START */              
            else if (other.gameObject.name == "Saturn Trigger") //heart-High BP Table trigger name is Heart
            {   
                //audio play 
                myAudio.clip = Satrun_mp3;
                myAudio.Play();

                targetPlanet = Satrun;
                ExpCam.transform.position = targetPlanet.transform.position + new Vector3(0f,10f,-50f);
                explation =true;
                //for alphaV1 destroy this trigger
                Destroy(other.gameObject);
            }  
 /* Saturn END */ 
  /* Uranus START */              
            else if (other.gameObject.name == "Uranus Trigger") //heart-High BP Table trigger name is Heart
            {   
                //audio play 
                myAudio.clip = Uranus_mp3;
                myAudio.Play();

                targetPlanet = Uranus;
                ExpCam.transform.position = targetPlanet.transform.position + new Vector3(0f,10f,-50f);
                explation =true;
                //for alphaV1 destroy this trigger
                Destroy(other.gameObject);
            }  
 /* Uranus END */ 
  /* Pluto START */              
            else if (other.gameObject.name == "Pluto Trigger") //heart-High BP Table trigger name is Heart
            {   
                //audio play 
                myAudio.clip = Pluto_mp3;
                myAudio.Play();
                
                targetPlanet = Pluto;
                ExpCam.transform.position = targetPlanet.transform.position + new Vector3(0f,10f,-50f);
                explation =true;
                //for alphaV1 destroy this trigger
                Destroy(other.gameObject);
            }  
 /* Pluto END */     
        }
    }  

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "SpeedHigh") {
                if(speedConstant == 1) {
                    //increase rocket speed
                    SpeedUpRocket();
                    //reset const
                    speedConstant=0;
                }
        }
    }

    void SpeedUpRocket(){ 
        gameObject.GetComponent<SPACE_PlayerMove>().runSpeed = 300f;
        gameObject.GetComponent<SPACE_PlayerMove>().OrbitEnterSpeed();
    }
    void SlowDownRocket(){ 
        gameObject.GetComponent<SPACE_PlayerMove>().runSpeed = 50f;
        gameObject.GetComponent<SPACE_PlayerMove>().OrbitEnterSpeed();
    }
}
