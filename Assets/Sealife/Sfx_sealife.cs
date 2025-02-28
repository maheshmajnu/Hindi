using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx_sealife : MonoBehaviour
{
    private GameObject Cam;

    public GameObject Whale_cam;
    public GameObject Dolphin_cam;
    public GameObject Turtle_cam;
    public GameObject StarFish_cam;

    private string trigname="";

    [Header("cutscene mp3")]
    public AudioSource myAudio;

    public AudioClip whale_audio;
    public AudioClip dolphin_audio;
    public AudioClip turtle_audio;
    public AudioClip starfish_audio;

    public GameObject Skip_Canvas;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DoTriggerAction();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            trigname = other.gameObject.name;
        }
    }
    public void OnTriggerExit(Collider other)
    { 
            trigname = ""; 
    }


    void DoTriggerAction()
    { 
            if (trigname == "Whale")
            {
                if (Input.GetKeyDown(KeyCode.E) || gameObject.GetComponent<PlayerMove>().InteractIspressed)
                {
                gameObject.GetComponent<PlayerMove>().InteractIspressed = false;
                Skip_Canvas.SetActive(true); Cursor.visible = true;
                    Cam = Whale_cam;
                    Whale_cam.SetActive(true);
                    myAudio.clip = whale_audio;
                    myAudio.Play();
                    //Invoke Wait
                    Invoke("CallMeWithWait", 41f);
                }
            }
            else if (trigname == "Dolphin")
            {
                if (Input.GetKeyDown(KeyCode.E) || gameObject.GetComponent<PlayerMove>().InteractIspressed)
                {
                gameObject.GetComponent<PlayerMove>().InteractIspressed = false;
                Skip_Canvas.SetActive(true); Cursor.visible = true;
                    Cam = Dolphin_cam;
                    Dolphin_cam.SetActive(true);
                    myAudio.clip = dolphin_audio;
                    myAudio.Play();
                    //Invoke Wait
                    Invoke("CallMeWithWait", 51f);
                }
            }
            else if (trigname == "Turtle")
            {
                if (Input.GetKeyDown(KeyCode.E) || gameObject.GetComponent<PlayerMove>().InteractIspressed)
                {
                gameObject.GetComponent<PlayerMove>().InteractIspressed=false;
                    Skip_Canvas.SetActive(true); Cursor.visible = true;
                    Debug.Log("turtle");
                    Cam = Turtle_cam;
                    Turtle_cam.SetActive(true);
                    myAudio.clip = turtle_audio;
                    myAudio.Play();
                    //Invoke Wait
                     Invoke("CallMeWithWait", 42f);
                }
            }
            else if (trigname == "StarFish")
            {
                if (Input.GetKeyDown(KeyCode.E) || gameObject.GetComponent<PlayerMove>().InteractIspressed)
                {
                gameObject.GetComponent<PlayerMove>().InteractIspressed = false;
                    Skip_Canvas.SetActive(true); Cursor.visible = true;
                    Cam = StarFish_cam;
                    StarFish_cam.SetActive(true);
                    myAudio.clip = starfish_audio;
                    myAudio.Play();
                    //Invoke Wait
                    Invoke("CallMeWithWait", 51f);
                }
            } 
    }



    void CallMeWithWait()
    {
        // Disable active
        Cam.SetActive(false);
        Skip_Canvas.SetActive(false);
    }

    public void skipAudio(){
        Cam.SetActive(false);
        myAudio.Stop();Cursor.visible = false;
    }
}
