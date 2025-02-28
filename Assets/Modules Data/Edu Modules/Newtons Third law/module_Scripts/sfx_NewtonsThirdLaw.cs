using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class sfx_NewtonsThirdLaw : MonoBehaviour
{

    
    public GameObject Scene_CamExp1;

    [Header("Particlesystems")]
    public GameObject Thrust1_particle;
    public GameObject Thrust2_particle;
    public GameObject smoke_particle;

    [Header("Audio files")]
    public AudioSource myAudio;
    public AudioClip NewtonsThirdlawintro_audio;
    public AudioClip procedure_audio; 

    [Header("Gameplay Gameobject")] 
    public GameObject GameplayMain;
    public GameObject ExplanationMain;
    private GameObject JustInstantiatedNoPlayerCanvas;

    private string[] miniObjectives = { "Row towards Right", "Row towards Left", "Reach the Island" }; private int array_i = 1;

    [Header("Gameplay-canvas")]
    public GameObject Objective_canvas;
    public GameObject UI_assets;
    public TextMeshProUGUI Objective;
    public TextMeshProUGUI Steps;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;


    [Header("Gameplay Anims")]
    private Animator anim;
    public GameObject Island;
    public GameObject BoatRower;

    private int rowlogic = 0;
    private float step = -7.9f;
    private bool movement = false;

    private bool missioncomplete = true;


    private void Awake()
    {

        //==================== INSERT NO-PLAYER Menu  ====================//
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/NotDefaultPlayer_Menu", typeof(GameObject));  // Load No-Player Menu
        JustInstantiatedNoPlayerCanvas = (GameObject)GameObject.Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate No-Player Menu   {  Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);   }
        JustInstantiatedNoPlayerCanvas.SetActive(false);
        //================================================================//
    }
     void Update()
    {
        MoveIsland();
    }

    void Audio_intro_method()
    {
        Scene_CamExp1.SetActive(false);
        myAudio.clip= NewtonsThirdlawintro_audio;
        myAudio.Play();
    }

    void _procedureaudio()
    {
        myAudio.clip = procedure_audio;
        myAudio.Play();
    }

    void _Thrustandsmoke()
    {
        Thrust1_particle.SetActive(true);
        Thrust2_particle.SetActive(true);
        smoke_particle.SetActive(true);

    }

  


    //=======================================================//
    //                      GAMEPLAY                         //
    //======================================================//

    public void _ResetnInitialize()
    {
        JustInstantiatedNoPlayerCanvas.SetActive(true);
        GameplayMain.SetActive(true);
        ExplanationMain.SetActive(false);

        //=======stop ongoing explanatory audio=====//
        myAudio.Stop();
        //==========================================// 

        //============= POPULATE OBJECTIVE CANVAS TEXT FOR GAMEPLAY ============//
        //Show objective canvas
        Objective_canvas.SetActive(true);

        //Objective = GameObject.Find("Main_Objective").GetComponent<TextMeshProUGUI>();
        Objective.text = "Objective: Apply Newtons Third Law To Reach The Island.";

        //Steps = GameObject.Find("Steps").GetComponent<TextMeshProUGUI>();
        Steps.text = miniObjectives[0];
        //==========================================================//

    }

    public void _rowLeft()
    {
        if (rowlogic == 1)
        {
            Debug.Log("row left");
            anim = BoatRower.GetComponent<Animator>();
            anim.Play("BoatRow_left");
            rowlogic = 2;
            step += -7.9f;
            Debug.Log(step);
            if (array_i <= 2)
            {
                NextObjective();
            }
        }
    }
    public void _rowRight()
    {
        if(rowlogic == 0)
        {
            Debug.Log("row right");
            anim = BoatRower.GetComponent<Animator>();
            anim.Play("BoatRow_right");
            rowlogic = 3;
            step += -7.9f;
            Debug.Log(step);

            if (array_i <= 2)
            {
                NextObjective();
            }
            

        }
        
    }

     void MoveIsland()
    {
        if (step >= -95f)
        {
            Vector3 a = Island.transform.position;
            Vector3 b = new Vector3(0f, 0f, step);
            Island.transform.position = Vector3.Lerp(a, b, 0.5f*Time.deltaTime);    
            if(Vector3.Distance(a,b) < 0.7f) {
                if(rowlogic == 2) {rowlogic = 0;}
                else if(rowlogic == 3) {rowlogic = 1;}
            }
        }
        else {
            if (missioncomplete)
            {
                LastObjective();
            }
        }
        
    }

    void LastObjective()
    {
        missioncomplete = false;
        GreenCheckBox.SetActive(true);
        Steps.color = Color.green;
        Objective.color = Color.green;

        Invoke("missionPassed", 2f);

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

    void missionPassed()
    {
        Cursor.visible = true;
        GameObject missionCompletePrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/Mission Passed", typeof(GameObject));  // Load Player
        Instantiate(missionCompletePrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate mission-Complete canvas 
    }

}



