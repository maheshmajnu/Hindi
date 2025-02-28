using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class sfx_Newtonssecondlaw : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Explanation anims")]
    public GameObject SkipBtn;

    public GameObject Cube_Fast;
    public GameObject Push_More_Force;
    public GameObject Cube_Slow;
    public GameObject Push_Less_Force;

    [Header("Explanation GameObjects")]
    public GameObject ExpCam;

    [Header("Audio files")]
    public AudioSource myAudio;
    public AudioClip Newtonssecondlawintro_audio;
    public AudioClip procedure_audio;
    

    [Header("Lines&texts")]
    public GameObject More_Text;
    public GameObject Name_A;
    public GameObject Less_Text;
    public GameObject Name_B;

    [Header("Gameplay Anims")]
    private Animator anim;
    public GameObject Baseball;
    public GameObject Basebat;
    public GameObject BaseballPlayer;

    [Header("Gameplay GameObjects")]
    public GameObject GameplayCam;
    public GameObject HighShotBtn;
    public GameObject LowShotBtn;
    private GameObject JustInstantiatedNoPlayerCanvas;


    private string[] miniObjectives = { "Hit Ball with Less Force.", "Hit Ball with More Force" }; private int array_i = 1;

    [Header("Gameplay-canvas")]
    public GameObject Objective_canvas;
    public GameObject UI_assets;
    public TextMeshProUGUI Objective;
    public TextMeshProUGUI Steps;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;



    private void Awake()
    {
        //==================== INSERT NO-PLAYER Menu  ====================//
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/NotDefaultPlayer_Menu", typeof(GameObject));  // Load No-Player Menu
        JustInstantiatedNoPlayerCanvas = (GameObject)GameObject.Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate No-Player Menu   {  Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);   }
        JustInstantiatedNoPlayerCanvas.SetActive(false);
        //================================================================//
    }




    //animation Play
    void _PushMethod()
    {
        anim = Cube_Fast.GetComponent<Animator>();
        anim.Play("CubeFast");

        anim = Push_More_Force.GetComponent<Animator>();
        anim.Play("Pushmoreforce");

        anim = Cube_Slow.GetComponent<Animator>();
        anim.Play("CubeSlow");

        anim = Push_Less_Force.GetComponent<Animator>();
        anim.Play("Pushlessforce");

    }


    void Audio_intro_method()
    {
        myAudio.clip = Newtonssecondlawintro_audio;
        myAudio.Play();
    }

    void _procedureaudio()
    {
        myAudio.clip = procedure_audio;
        myAudio.Play();
    }

     
    void _Textmethod()
    {
        //Animating label
        More_Text.SetActive(true);
        Name_A.SetActive(true);
        Less_Text.SetActive(true);
        Name_B.SetActive(true);
    }





    //=======================================================//
    //                      GAMEPLAY                        //
    //======================================================//

    public void _ResetnInitialize()
    {
        //=======stop ongoing explanatory audio=====//
        myAudio.Stop();
        // Hide skip button
        SkipBtn.SetActive(false);
        //==========================================// 
        anim = ExpCam.GetComponent<Animator>();
        anim.Play("empty");

        JustInstantiatedNoPlayerCanvas.SetActive(true);

        GameplayCam.SetActive(true);
        HighShotBtn.SetActive(true);
        LowShotBtn.SetActive(true);




        //============= POPULATE OBJECTIVE CANVAS TEXT FOR GAMEPLAY ============//
        //Show objective canvas
        Objective_canvas.SetActive(true);
        //Objective = GameObject.Find("Main_Objective").GetComponent<TextMeshProUGUI>();
        Objective.text = "Objective: Use Force to Hit the Baseball."; 
        //Steps = GameObject.Find("Steps").GetComponent<TextMeshProUGUI>();
        Steps.text = miniObjectives[0];
        //==========================================================//
    }



    //highshot playeranim
    public void PlayerHighShot()
    {
        anim = BaseballPlayer.GetComponent<Animator>();
        anim.Play("BaseBallPlayer_BatSpeed_100");

        anim = GameplayCam.GetComponent<Animator>();
        anim.Play("Camera_CameraActionHalf");
        if (array_i == 1)
        {
            NextObjective();
        }
    }
    //lowshot playeranim
    public void PlayerLowShot()
    {
        anim = BaseballPlayer.GetComponent<Animator>();
        anim.Play("BaseBallPlayer_BatSpeed_50");

        anim = GameplayCam.GetComponent<Animator>();
        anim.Play("Camera_CameraActionFull");
         
        if(array_i == 2)
        {
            LastObjective();
        }
    }




    public void PlayerMissShot()
    {
        anim = Baseball.GetComponent<Animator>();
        anim.SetTrigger("nohit");
        anim.ResetTrigger("low"); anim.ResetTrigger("high");
    }


    void NextObjective()
    {
        //objective checkbox toggle
        GreenCheckBox.SetActive(true);
        Steps.color = Color.green; 
        //wait and replace with new objective
        Invoke("CallMeWithWait", 3f);
    }
    void CallMeWithWait()
    {
        Steps.text = miniObjectives[array_i];
        GreenCheckBox.SetActive(false);
        Steps.color = Color.white;
        array_i++;
    }


    void LastObjective()
    {
        GreenCheckBox.SetActive(true);
        Steps.color = Color.green;
        Objective.color = Color.green;

        Invoke("missionPassed", 2f);

    }

    void missionPassed()
    {
        Cursor.visible = true;
        GameObject missionCompletePrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/Mission Passed", typeof(GameObject));  // Load Player
        Instantiate(missionCompletePrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate mission-Complete canvas 
    }
}


