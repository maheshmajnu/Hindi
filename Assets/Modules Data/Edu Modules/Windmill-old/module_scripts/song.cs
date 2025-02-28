using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class song : MonoBehaviour
{

    // public AudioClip otherClip;


    [Header("Lines & Texts")]
    public GameObject lineNacelle;
    public GameObject lineController;
    public GameObject lineGenerator;
    public GameObject lineShaftrod;
    public GameObject lineGearbox;
    public GameObject lineBlade;
    public GameObject lineInverter;
    public GameObject lineAnemometer;
   

    [Header("TurnOn - after cutscene")]
    public GameObject _CMvcam1;
    public GameObject _MainCamera;
    public GameObject _all;
    public GameObject _Switch;
    public GameObject _Switch1;

    

    [Header("TurnOff - after cutscene")]
    public GameObject _BladeHead; 

    [Header("cutscene mp3")]
    public AudioSource myAudio;
    public AudioClip game_bgm;

    public AudioClip anemometer;
    public AudioClip blades;
    public AudioClip gearbox;
    public AudioClip generator;
    public AudioClip grid;
    public AudioClip inverter;
    public AudioClip nacelle;
    public AudioClip rotor;
    public AudioClip shaftrod;
    public AudioClip thecontroller;
    public AudioClip wind_intro;
    public AudioClip wind_intro_two;

    public GameObject lights;

    [Header("Gameplay Objects")]
    public Camera ExpCam;
    private GameObject mainPlayer; 
    public string Interactable_ColliderName;


    public Camera RayCam;
    private Camera TempCam;
    public Camera MainCam;
    public GameObject CMvcam1; 

    [Header("Gameplay Anims")] 
    private Animator anim;

    // [Header("Lines&texts")]
    private string[] miniObjectives = { "Align the components of Windmill in correct Order." }; private int array_i = 1;

    [Header("Gameplay-canvas")]
    public GameObject Objective_canvas;
    public GameObject UI_assets;
    public TextMeshProUGUI Objective;
    public TextMeshProUGUI Steps;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;

    /*
    public GameObject anemometer_ui;
    public GameObject blades_ui;
    public GameObject gearbox_ui;
    public GameObject generator_ui;
    public GameObject grid_ui;
    public GameObject inverter_ui;
    public GameObject nacelle_ui;
    public GameObject rotor_ui;
    public GameObject shaftrod_ui;
    public GameObject thecontroller_ui;
    public GameObject wind_intro_ui;
    public GameObject wind_intro_two_iu;*/

    
    public GameObject MobileControlsUI;



    // Start is called before the first frame update
    void Start()
    {

    


    }

 

 



    // Update is called once per frame
    void Update()
    {
        //===================================// 
        /* Get interactable Collider Name */
        HoverdColliderName();
        //===================================//
    }

    void Anemometer() {
        myAudio.clip = anemometer; 
        myAudio.Play();
         
    }

    void Blades()
    {
        myAudio.clip = blades; 
        myAudio.Play();
    }
    void Gearbox()
    {
        myAudio.clip = gearbox;
        myAudio.Play();
    }
    void Generator()
    {
        myAudio.clip = generator;
        myAudio.Play();
    }
    void Grid()
    {
        myAudio.clip = grid;
        myAudio.Play();
         
    }

    void Inverter()
    {
        myAudio.clip = inverter;
        myAudio.Play();
    }
    void Nacelle()
    {
        myAudio.clip = nacelle;
        myAudio.Play();
         
    }
    void Rotor()
    {
        myAudio.clip = rotor;
        myAudio.Play();
    }
    void Shaftrod()
    {
        myAudio.clip = shaftrod;
        myAudio.Play();
    }
    void Thecontroller()
    {
        myAudio.clip = thecontroller;
        myAudio.Play();
    }
    void Wind_intro()
    {
        myAudio.clip = wind_intro;
        myAudio.Play();
    }
    void Wind_intro_two()
    {
        myAudio.clip = wind_intro_two;
        myAudio.Play();
    }

    void CutsceneEnd() {

        StartCoroutine(Waitdelay()); 
    }

    void LightsOn()
    {
        //lights on
        lights.SetActive(true);
    }

IEnumerator Waitdelay()
{
    yield return new WaitForSecondsRealtime(8f);
        Debug.Log("cutscene compete");

        _ResetnInitialize(); 
    }


    void GotoGameplay()
    { 
        _Switch.SetActive(true);
        _Switch1.SetActive(true);
        _MainCamera.SetActive(true);
        _all.SetActive(true);
        //_SwitchtoClass.SetActive(true);
        //turnoff bladehead, camera
        _BladeHead.SetActive(false);
        lights.SetActive(false);
        UI_assets.SetActive(false);
        //_Camera.SetActive(false);
        //_gameplay.SetActive(false); 
    }

    public void _ResetnInitialize()
    {


        //=======stop ongoing explanatory audio=====//
        //myAudio.Stop();
        myAudio.clip = game_bgm;
        myAudio.Play(); myAudio.loop = true;
        //==========================================//
        anim = ExpCam.GetComponent<Animator>();
        anim.Play("empty");

        GotoGameplay();

        //==================== INSERT PLAYER  ====================//
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/PlayerPrefabs/Main/__Player", typeof(GameObject));  // Load Player
        Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate Player 
        mainPlayer = GameObject.Find("TPP_Player");  //Get TPP_Player
        GameObject spawnpoint = mainPlayer.GetComponent<Teleporting>().FindClosestSpawnPoint();  //find spawnpoint
        //teleport Player
        mainPlayer.GetComponent<CharacterController>().enabled = false;
        mainPlayer.GetComponent<CharacterController>().transform.position = spawnpoint.transform.position;
        mainPlayer.GetComponent<CharacterController>().enabled = true;
        //Diable Player Camera Auido Listerner
        GameObject.Find("MainCamera").GetComponent<AudioListener>().enabled = false;
        //========================================================//


        //============= POPULATE OBJECTIVE CANVAS TEXT FOR GAMEPLAY ============//
        //Show objective canvas
        Objective_canvas.SetActive(true);


        //Objective = GameObject.Find("Main_Objective").GetComponent<TextMeshProUGUI>();
        Objective.text = "Objective: Light Up The Building.";

        //Steps = GameObject.Find("Steps").GetComponent<TextMeshProUGUI>();
        Steps.text = miniObjectives[0];
        //==========================================================//
        
    }



    //get Collider name that player Hovered
    void HoverdColliderName()
    {
        
        mainPlayer = GameObject.Find("TPP_Player");  //Get TPP_Player
        if (mainPlayer != null)
        {
            GameObject colliderObj = mainPlayer.GetComponent<PlayerMove>().CrosshairHitObj;
            if (colliderObj != null)
            {
                Interactable_ColliderName = mainPlayer.GetComponent<PlayerMove>().CrosshairHitObj.name;
                if (Interactable_ColliderName != null)
                {
                    Debug.Log("collider:" + Interactable_ColliderName );
                    if (Input.GetMouseButtonDown(0) || mainPlayer.GetComponent<PlayerMove>().InteractIspressed)
                    {
                        mainPlayer.GetComponent<PlayerMove>().InteractIspressed = false;
                        DoColliderAction();
                    }
                }
            }
        }
        
    }

    //call respective collider actions based on collider names
    void DoColliderAction()
    {
        Debug.Log("collider:" + Interactable_ColliderName + "clicked do something");
        if (Interactable_ColliderName == "switch1")
        {
            PlayerOnScreenControlUI_off();
            CMvcam1.SetActive(true);
            //------------------------------------ MobileControlsUI.SetActive(false);
            //Turn raycam ON after delay 2 sec
            StartCoroutine(RayCamOn());
            
        }
        // this below case doesnt work because crosshair aim hit is not possible
        else if (Interactable_ColliderName == "switch")  // this case is re-written in windmill.cs
        {
            PlayerOnScreenControlUI_on(); 
            RayCam.gameObject.SetActive(false);
            CMvcam1.SetActive(false);
          //------------------------------------  MobileControlsUI.SetActive(true);
            //switch back Raycam with Maincam
            MainCam = TempCam;
            
        }
    }


    void PlayerOnScreenControlUI_off()
    {
        GameObject.Find("Crosshair-Canvas").GetComponent<Canvas>().enabled = false;
        var item = GameObject.Find("Mobile controls UI Canvas");
        if (item != null)
        {
            item.GetComponent<Canvas>().enabled = false;
        }
    }
    void PlayerOnScreenControlUI_on()
    {
        GameObject.Find("Crosshair-Canvas").GetComponent<Canvas>().enabled = true;
        var item = GameObject.Find("Mobile controls UI Canvas");
        if (item != null)
        {
            item.GetComponent<Canvas>().enabled = true;
        }
    }


    IEnumerator RayCamOn()
    {
        yield return new WaitForSecondsRealtime(2f);
        //raycamon
        RayCam.transform.gameObject.SetActive(true);
        //assign raycam to maincam
        MainCam = RayCam;

    }

    void _lineNacellemethodon()
    {
        lineNacelle.SetActive(true);
    }

    void _lineNacellemethodoff()
    {
        lineNacelle.SetActive(false);
    }

    void _lineControllermethodon()
    {
        lineController.SetActive(true);
    }

    void _lineControllermethodoff()
    {
        lineController.SetActive(false);
    }

    void _lineGeneratormethodon()
    {
        lineGenerator.SetActive(true);
    }

    void _lineGeneratormethodoff()
    {
        lineGenerator.SetActive(false);
    }

    void _lineShaftrodrmethodon()
    {
        lineShaftrod.SetActive(true);
    }

    void _lineShaftrodmethodoff()
    {
        lineShaftrod.SetActive(false);
    }

    void _lineGearboxmethodon()
    {
        lineGearbox.SetActive(true);
    }

    void _lineGearboxmethodoff()
    {
        lineGearbox.SetActive(false);
    }

    void _lineBlademethodon()
    {
        lineBlade.SetActive(true);
    }

    void _lineBlademethodoff()
    {
        lineBlade.SetActive(false);
    }

    void _lineInvertermethodon()
    {
        lineInverter.SetActive(true);
    }

    void _lineInvertermethodoff()
    {
        lineInverter.SetActive(false);
    }

    void _lineAnemometermethodon()
    {
        lineAnemometer.SetActive(true);
    }

    void _lineAnemometermethodoff()
    {
        lineAnemometer.SetActive(false);
    }

    void _lineRotormethodon(){

    }

    void _lineRotormethodoff(){

    }

    
}
