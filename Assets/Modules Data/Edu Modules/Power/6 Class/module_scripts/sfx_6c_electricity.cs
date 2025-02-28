using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sfx_6c_electricity : MonoBehaviour
{
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

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
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject Circuit_board;
    public GameObject Wires;
    public GameObject Hand_pose1;
    public GameObject Hand_pose2;
    public GameObject closed_circuit_bulb;
    public GameObject circuitboardsetA;
    public GameObject circuitboardsetB;
    public GameObject switchbase;
    public GameObject bulb_txt;
    public GameObject terminal_txt;
    public GameObject filament_txt;
    public GameObject thin_wires_txt;
    public GameObject insulated_wires_txt;
    public GameObject two_terminals_txt;
    public GameObject switchexp_txt;
    public GameObject tubelight_glow;
    public GameObject bulbglow;
    public GameObject bulballtexts;
    public GameObject bulbexpglow;
    


    // Exp - Animations

    private Animator anim;
    [Header("Explanation anims")]
    public GameObject fan;
    public GameObject Bulb;
    public GameObject torchlight;
    public GameObject handswitchwall;
    public GameObject rightwire;
    public GameObject leftwire;
    public GameObject downhand;
    public GameObject switchon;
  



    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip a_1;
    public AudioClip a_2;
    public AudioClip a_3;
    public AudioClip a_4;
    public AudioClip a_5;
    public AudioClip a_6;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    void _bulbexpglowMethodON()
    {
        bulbexpglow.SetActive(true);
    }

    void _bulbexpglowMethodOFF()
    {
        bulbexpglow.SetActive(false);
    }


    //


    void _bulballtxtsMethodON()
    {
        bulballtexts.SetActive(true);
    }

    void _bulballtxtsMethodOFF()
    {
        bulballtexts.SetActive(false);
    }


    //


    void _bulbglowMethodON()
    {
        bulbglow.SetActive(true);
    }

    void _bulbglowMethodOFF()
    {
        bulbglow    .SetActive(false);
    }


    //

    void _tubeglowMethodON()
    {
        tubelight_glow.SetActive(true);
    }

    void _tubeglowMethodOFF()
    {
        tubelight_glow.SetActive(false);
    }


    //



    void _Switchexp_txtMethodON()
    {
        switchexp_txt.SetActive(true);
    }

    void _Switchexp_txtMethodOFF()
    {
        switchexp_txt.SetActive(false);
    }


    //



    void _Bulb_txtMethodON()
    {
        bulb_txt.SetActive(true);
    }

    void _Bulb_txtMethodOFF()
    {
        bulb_txt.SetActive(false);
    }


    //

    void _Terminal_txtMethodON()
    {
        terminal_txt.SetActive(true);
    }

    void _Terminal_txtMethodOFF()
    {
        terminal_txt.SetActive(false);
    }


    //

    void _Filament_txtMethodON()
    {
        filament_txt.SetActive(true);
    }

    void _Filament_txtMethodOFF()
    {
        filament_txt.SetActive(false);
    }


    //

    void _Thinwires_txtMethodON()
    {
        thin_wires_txt.SetActive(true);
    }

    void _Thinwires_txtWiresMethodOFF()
    {
        thin_wires_txt.SetActive(false);
    }


    //

    void _Insulatedwires_txtMethodON()
    {
        insulated_wires_txt.SetActive(true);
    }

    void _Insulatedtwires_txtMethodOFF()
    {
        insulated_wires_txt.SetActive(false);
    }


    //

    void _Twoterminals_txtMethodON()
    {
        two_terminals_txt.SetActive(true);
    }

    void _Twoterminals_txtMethodOFF()
    {
        two_terminals_txt.SetActive(false);
    }


    //







    void _BulbB_animationAnimmethod()
    {

        anim = Bulb.GetComponent<Animator>();
        anim.Play("Bulb animation 2");
    }


    void _Circuit_boardMethodON()
    {
        Circuit_board.SetActive(true);
    }

    void _Circuit_boardMethodOFF()
    {
        Circuit_board.SetActive(false);
    }

    //

    void _WiresMethodON()
    {
        Wires.SetActive(true);
    }

    void _WiresMethodOFF()
    {
        Wires.SetActive(false);
    }


    //

    void _Closed_circuit_bulbMethodON()
    {
        closed_circuit_bulb.SetActive(true);
        
    }

    void _Closed_circuit_bulbMethodOFF()
    {
        closed_circuit_bulb.SetActive(false);
    }



    //


    void _Hand_switch_pose_AMethodON()
    {
        Hand_pose1.SetActive(true);
    }

    void _Hand_switch_pose_AMethodOFF()
    {
        Hand_pose1.SetActive(false);
    }


    //


    void _Hand_wall_switch_pose_BMethodON()
    {
        Hand_pose2.SetActive(true);
    }

    void _Hand_wall_switch_pose_BMethodOFF()
    {
        Hand_pose2.SetActive(false);
    }

    //


    void _CircuitsetAMethodON()
    {
        circuitboardsetA.SetActive(true);
    }

    void _CircuitsetAMethodOFF()
    {
        circuitboardsetA.SetActive(false);
    }

  //


    void _CircuitsetBMethodON()
    {
        circuitboardsetB.SetActive(true);
    }

    void _CircuitsetBMethodOFF()
    {
        circuitboardsetB.SetActive(false);
    }

    //

    void _switchbaseMethodON()
    {
        switchbase.SetActive(true);
    }

    void _switchbaseMethodOFF()
    {
        switchbase.SetActive(false);
    }

     

    void _Goto_menuMethod()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }




    // anims


    void _Fan_animationAnimmethod()
    {

        anim = fan.GetComponent<Animator>();
        anim.Play("Fan animation");
    }


    //



    void _Bulb_animationAnimmethod()
    {

        anim = Bulb.GetComponent<Animator>();
        anim.Play("Bulb animation");
    }

    //



    void _torchlight_animationAnimmethod()
    {

        anim = torchlight.GetComponent<Animator>();
        anim.Play("Torch light turntable");
    }

    //

    void _handswitchwall_animationAnimmethod()
    {

        anim = handswitchwall.GetComponent<Animator>();
        anim.Play("Wall switch hand");
    }

    //

    void _rightwire_animationAnimmethod()
    {

        anim = rightwire.GetComponent<Animator>();
        anim.Play("right wire animation");
    }

    //

    void _lefttwire_animationAnimmethod()
    {

        anim = leftwire.GetComponent<Animator>();
        anim.Play("left wire animation");
    }

    //

    void _downhand_animationAnimmethod()
    {

        anim = downhand.GetComponent<Animator>();
        anim.Play("Closed circuit switch hand animation");
    }


    //
    void _Switch_onAnimmethod()
    {

        anim = switchon.GetComponent<Animator>();
        anim.Play("switchon");
    }


    void _a_1_audioMethod()
    {
        myAudio.clip = a_1;
        myAudio.Play();
    }


    void _a_2_audioMethod()
    {
        myAudio.clip = a_2;
        myAudio.Play();
    }


    void _a_3_audioMethod()
    {
        myAudio.clip = a_3;
        myAudio.Play();
    }


    void _a_4_audioMethod()
    {
        myAudio.clip = a_4;
        myAudio.Play();
    }


    void _a_5_audioMethod()
    {
        myAudio.clip = a_5;
        myAudio.Play();
    }


    void _a_6_audioMethod()
    {
        myAudio.clip = a_6;
        myAudio.Play();
    }

    public void MiniGameStart()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MiniGameEnd()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(true);
        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private int connectedCount = 0;
    public void CheckCircuit()
    {
        connectedCount++;
        if(connectedCount == 2)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }



}
