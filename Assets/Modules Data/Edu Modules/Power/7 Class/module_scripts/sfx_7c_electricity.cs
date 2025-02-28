using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sfx_7c_electricity : MonoBehaviour
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

        //SetWayPoint(wayPoint1); 

    }

    public Camera cam;

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject cells;
    public GameObject carbattery;
    public GameObject torchlight;
    public GameObject remote;
    public GameObject switches;
    public GameObject bulbD;
    public GameObject batteryD;
    public GameObject lightfuseD;
    public GameObject closedcircuitbulb;
    public GameObject circuitdiagram;
    public GameObject electricalcircuitdiagram;
    public GameObject celldiagram;
    public GameObject bulbdiagram;
    public GameObject fusediagram;
    public GameObject switchdiagram;
    public GameObject batterydiagram;
    public GameObject wirediagram;
    public GameObject waterheater;
    public GameObject oven;
    public GameObject nichromewire;
    public GameObject mcb;
    public GameObject bothcells;
    public GameObject singlebulb;
    public GameObject wire;
    public GameObject carbatteryexp;
    public GameObject nailwoodplate;
    public GameObject mainfuse;
    public GameObject greendot;
    public GameObject explanationwires;
    public GameObject hangingiron;
    public GameObject hangingem;
    



    // Exp - Animations

    private Animator anim;
    [Header("Explanation anims")]
    public GameObject cellA;
    public GameObject cellB;
    public GameObject compdeflplugkey;
    public GameObject magneteffplugkey;
    public GameObject magneteffbarmagnet;
    public GameObject compassneedle;
    public GameObject potatocompassneedle;
    public GameObject hand;
    public GameObject nichromewirem;
    public GameObject nailplugkey;
    public GameObject wallcompass;
    public GameObject hangingmagnet;
    public GameObject hitter;
    public GameObject hitterplugkey;
    public GameObject hitterexp;
    public GameObject compasspointer;



    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip electric_current;
    public AudioClip aud1_elec_circ;
    public AudioClip aud2_source;
    public AudioClip aud3_batterytitle;
    public AudioClip aud3_batterymain;
    public AudioClip aud3_batteryexmp;
    public AudioClip aud4_bulb;
    public AudioClip aud5_connectingwire;
    public AudioClip aud6_plugkeytitle;
    public AudioClip aud6_plugkey;
    public AudioClip aud7_cell;
    public AudioClip aud8_battery;
    public AudioClip aud9_fusetitle;
    public AudioClip aud9_fuse;
    public AudioClip aud10_gapcd;
    public AudioClip aud11_heatefftitle;
    public AudioClip aud11_heateffmain;
    public AudioClip f_exp;
    public AudioClip hg_ef_cr_tl;
    public AudioClip hg_ef_cr_exp;
    public AudioClip na_s_u_tl;
    public AudioClip na_s_u_exp;
    public AudioClip sa_dv_f;
    public AudioClip mcb_tl;
    public AudioClip mcb_exp;
    public AudioClip so_of_el_cr_in;
    public AudioClip sw_tl;
    public AudioClip sw_exp;
    public AudioClip sy_of_em_el_cr_in;
    public AudioClip c_d_in;
    public AudioClip ma_in;
    public AudioClip ema1_in;
    public AudioClip ema2_in;
    public AudioClip eb1_in;
    public AudioClip eb2_in;
    public AudioClip po;














    //Titles




    public GameObject title1;


    public GameObject title2;

    public GameObject title3;

    public GameObject title4;


    public GameObject title5;



    public GameObject title6;

    public GameObject title7;

    public GameObject title8;













    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("Camera animation", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }
    }

    public void _Jump_To1(float value)
    {
        RestartSceneWithKeyframe(value);
    }

    private void RestartSceneWithKeyframe(float normalizedTime)
    {
        targetNormalizedTime = normalizedTime; // Store the keyframe to jump to
                                               //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(1);
    }

















































    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


















    void _title1_MethodON()
    {
        title1.SetActive(true);
    }

    void _title1_MethodOFF()
    {
        title1.SetActive(false);
    }




    void _title2_MethodON()
    {
        title2.SetActive(true);
    }

    void _title2_MethodOFF()
    {
        title2.SetActive(false);
    }






    void _title3_MethodON()
    {
        title3.SetActive(true);
    }

    void _title3_MethodOFF()
    {
        title3.SetActive(false);
    }






    void _title4_MethodON()
    {
        title4.SetActive(true);
    }

    void _title4_MethodOFF()
    {
        title4.SetActive(false);
    }





    void _title5_MethodON()
    {
        title5.SetActive(true);
    }

    void _title5_MethodOFF()
    {
        title5.SetActive(false);
    }










    void _title6_MethodON()
    {
        title6.SetActive(true);
    }

    void _title6_MethodOFF()
    {
        title6.SetActive(false);
    }







    void _title7_MethodON()
    {
        title7.SetActive(true);
    }

    void _title7_MethodOFF()
    {
        title7.SetActive(false);
    }









    void _title8_MethodON()
    {
        title8.SetActive(true);
    }

    void _title8_MethodOFF()
    {
        title8.SetActive(false);
    }













    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }






























 void _electric_current_Method()
    {
        myAudio.clip = electric_current;
        myAudio.Play();
    }


    void _aud1_elec_circ_Method()
    {
        myAudio.clip = aud1_elec_circ;
        myAudio.Play();
    }

    void _aud2_source_Method()
    {
        myAudio.clip = aud2_source;
        myAudio.Play();
    }




    void aud3_batterytitle_Method()
    {
        myAudio.clip = aud3_batterytitle;
        myAudio.Play();
    }
    void _aud3_batterymain_Method()
    {
        myAudio.clip = aud3_batterymain;
        myAudio.Play();
    }
    void aud3_batteryexmp_Method()
    {
        myAudio.clip = aud3_batteryexmp;
        myAudio.Play();
    }



    void _aud4_bulb_Method()
    {
        myAudio.clip = aud4_bulb;
        myAudio.Play();
    }


    void _aud5_connectingwire_Method()
    {
        myAudio.clip = aud5_connectingwire;
        myAudio.Play();
    }

void _aud6_plugkey_title_Method()
    {
        myAudio.clip = aud6_plugkeytitle;
        myAudio.Play();
    }
  

    void _aud6_plugkey_Method()
    {
        myAudio.clip = aud6_plugkey;
        myAudio.Play();
    }


    void _aud7_cell_Method()
    {
        myAudio.clip = aud7_cell;
        myAudio.Play();
    }

    void _aud8_battery_Method()
    {
        myAudio.clip = aud8_battery;
        myAudio.Play();
    }
    void _aud9_fusetitle_Method()
    {
        myAudio.clip = aud9_fusetitle;
        myAudio.Play();
    }

    void _aud9_fuse_Method()
    {
        myAudio.clip = aud9_fuse;
        myAudio.Play();
    }
    void _aud10_gapcd_Method()
    {
        myAudio.clip = aud10_gapcd;
        myAudio.Play();
    }
    void _aud11_heatefftitle_Method()
    {
        myAudio.clip = aud11_heatefftitle;
        myAudio.Play();
    }
    void _aud11_heateffmain_Method()
    {
        myAudio.clip = aud11_heateffmain;
        myAudio.Play();
    }



    void _f_exp_Method()
    {
        myAudio.clip = f_exp;
        myAudio.Play();
    }

    void _hg_ef_cr_tl_Method()
    {
        myAudio.clip = hg_ef_cr_tl;
        myAudio.Play();
    }

    void _hg_ef_cr_exp_Method()
    {
        myAudio.clip = hg_ef_cr_exp;
        myAudio.Play();
    }

    void _na_s_u_tl_Method()
    {
        myAudio.clip = na_s_u_tl;
        myAudio.Play();
    }

    void _na_s_u_exp_Method()
    {
        myAudio.clip = na_s_u_exp;
        myAudio.Play();
    }

    void _sa_dv_f_Method()
    {
        myAudio.clip = sa_dv_f;
        myAudio.Play();
    }

    void _mcb_tl_Method()
    {
        myAudio.clip = mcb_tl;
        myAudio.Play();
    }

    void _mcb_exp_Method()
    {
        myAudio.clip = mcb_exp;
        myAudio.Play();
    }

    void _so_of_el_cr_in_Method()
    {
        myAudio.clip = so_of_el_cr_in;
        myAudio.Play();
    }

    void _sw_tl_Method()
    {
        myAudio.clip = sw_tl;
        myAudio.Play();
    }

    void _sw_exp_Method()
    {
        myAudio.clip = sw_exp;
        myAudio.Play();
    }

    void _sy_of_em_el_cr_in_Method()
    {
        myAudio.clip = sy_of_em_el_cr_in;
        myAudio.Play();
    }

    void _c_d_in_Method()
    {
        myAudio.clip = c_d_in;
        myAudio.Play();
    }

    void _ma_in_Method()
    {
        myAudio.clip = ma_in;
        myAudio.Play();
    }

    void _ema1_in_Method()
    {
        myAudio.clip = ema1_in;
        myAudio.Play();
    }

    void _ema2_in_Method()
    {
        myAudio.clip = ema2_in;
        myAudio.Play();
    }

    void _eb1_in_Method()
    {
        myAudio.clip = eb1_in;
        myAudio.Play();
    }

    void _eb2_in_Method()
    {
        myAudio.clip = eb2_in;
        myAudio.Play();
    }

    void _po_Method()
    {
        myAudio.clip = po;
        myAudio.Play();
    }















    void _CellsMethodON()
    {
        cells.SetActive(true);
    }

    void _CellsMethodOFF()
    {
        cells.SetActive(false);
    }
    //


    void _HangingEMMethodON()
    {
        hangingem.SetActive(true);
    }

    void _HangingEMMethodOFF()
    {
        hangingem.SetActive(false);
    }

    void _Both_cellsMethodON()
    {
        bothcells.SetActive(true);
    }

    void _Both_cellsMethodOFF()
    {
        bothcells.SetActive(false);
    }
    //
    void _Single_bulbMethodON()
    {
        singlebulb.SetActive(true);
    }

    void _Single_bulbMethodOFF()
    {
        singlebulb.SetActive(false);
    }
    //
    void _WireMethodON()
    {
        wire.SetActive(true);
    }

    void _WireMethodOFF()
    {
        wire.SetActive(false);
    }
    //
    void _Car_battery_expMethodON()
    {
        carbatteryexp.SetActive(true);
    }

    void _Car_battery_expMethodOFF()
    {
        carbatteryexp.SetActive(false);
    }
    //
    void _E_circuit_diagramMethodON()
    {
        electricalcircuitdiagram.SetActive(true);
    }

    void _E_circuit_diagramMethodOFF()
    {
        electricalcircuitdiagram.SetActive(false);
    }
    //
    void _Nail_wood_plateMethodON()
    {
        nailwoodplate.SetActive(true);
    }

    void _Nail_wood_plateMethodOFF()
    {
        nailwoodplate.SetActive(false);
    }
    //
    void _Hanging_ironMethodON()
    {
        hangingiron.SetActive(true);
    }

    void _Hanging_ironMethodOFF()
    {
        hangingiron.SetActive(false);
    }

    void _Main_fuseMethodON()
    {
        mainfuse.SetActive(true);
    }

    void _Main_fuseMethodOFF()
    {
        mainfuse.SetActive(false);
    }
    //
    void _Green_dotMethodON()
    {
        greendot.SetActive(true);
    }

    void _Green_dotMethodOFF()
    {
        greendot.SetActive(false);
    }
    //
    void _Explanation_wiresMethodON()
    {
        explanationwires.SetActive(true);
    }

    void _Explanation_wiresMethodOFF()
    {
        explanationwires.SetActive(false);
    }
    //
    void _Car_batteryMethodON()
    {
        carbattery.SetActive(true);
    }

    void _Car_batteryMethodOFF()
    {
        carbattery.SetActive(false);
    }
    //
    void _TorchlightMethodON()
    {
        torchlight.SetActive(true);
    }

    void _TorchlightMethodOFF()
    {
        torchlight.SetActive(false);
    }
    //
    void _TV_remoteMethodON()
    {
        remote.SetActive(true);
    }

    void _TV_remoteMethodOFF()
    {
        remote.SetActive(false);
    }
    //
    void _SwitchesMethodON()
    {
        switches.SetActive(true);
    }

    void _SwitchesMethodOFF()
    {
        switches.SetActive(false);
    }
    //
    void _Exp_bulbMethodON()
    {
        bulbD.SetActive(true);
    }

    void _Exp_bulbMethodOFF()
    {
        bulbD.SetActive(false);
    }
    //
    void _Exp_batteryMethodON()
    {
        batteryD.SetActive(true);
    }

    void _Exp_batteryMethodOFF()
    {
        batteryD.SetActive(false);
    }
    //
    void _Exp_fuseMethodON()
    {
        lightfuseD.SetActive(true);
    }

    void _Exp_fuseMethodOFF()
    {
        lightfuseD.SetActive(false);
    }
    //
    void _Closed_circuit_bulbMethodON()
    {
        closedcircuitbulb.SetActive(true);
    }

    void _Closed_circuit_bulbMethodOFF()
    {
        closedcircuitbulb.SetActive(false);
    }
    //
    void _Circuit_diagramMethodON()
    {
        circuitdiagram.SetActive(true);
    }

    void _Circuit_diagramMethodOFF()
    {
        circuitdiagram.SetActive(false);
    }
    //
    void _Cell_diagramMethodON()
    {
        celldiagram.SetActive(true);
    }

    void _Cell_diagramMethodOFF()
    {
        celldiagram.SetActive(false);
    }
    //
    void _Bulb_diagramMethodON()
    {
        bulbdiagram.SetActive(true);
    }

    void _Bulb_diagramMethodOFF()
    {
        bulbdiagram.SetActive(false);
    }
    //
    void _Fuse_diagramMethodON()
    {
        fusediagram.SetActive(true);
    }

    void _Fuse_diagramMethodOFF()
    {
        fusediagram.SetActive(false);
    }
    //
    void _Switch_diagramMethodON()
    {
        switchdiagram.SetActive(true);
    }

    void _Switch_diagramMethodOFF()
    {
        switchdiagram.SetActive(false);
    }
    //
    void _Battery_diagramMethodON()
    {
        batterydiagram.SetActive(true);
    }

    void _Battery_diagramMethodOFF()
    {
        batterydiagram.SetActive(false);
    }
    //
    void _Wire_diagramMethodON()
    {
        wirediagram.SetActive(true);
    }

    void _Wire_diagramMethodOFF()
    {
        wirediagram.SetActive(false);
    }
    //
    void _Water_heater_diagramMethodON()
    {
        waterheater.SetActive(true);
    }

    void _Water_heaterMethodOFF()
    {
        waterheater.SetActive(false);
    }
    //
    void _OvenMethodON()
    {
        oven.SetActive(true);
    }

    void _OvenMethodOFF()
    {
        oven.SetActive(false);
    }
    //
    void _Nichrome_wireMethodON()
    {
        nichromewire.SetActive(true);
    }

    void _Nichrome_wireMethodOFF()
    {
        nichromewire.SetActive(false);
    }
    //
    void _M_C_BMethodON()
    {
        mcb.SetActive(true);
    }

    void _M_C_BMethodOFF()
    {
        mcb.SetActive(false);
    }

    void _CellA_animationAnimmethod()
    {

        anim = cellA.GetComponent<Animator>();
        anim.Play("Cell 2 animation");
    }
    //
    void _CellB_animationAnimmethod()
    {

        anim = cellB.GetComponent<Animator>();
        anim.Play("Cell 3 animation");
    }
    //
    void _Compass_deflection_plugkey_animationAnimmethod()
    {

        anim = compdeflplugkey.GetComponent<Animator>();
        anim.Play("Compass deflection plug key animation");
    }
    //
    void _Magnetic_effect_plugkey_animationAnimmethod()
    {

        anim = magneteffplugkey.GetComponent<Animator>();
        anim.Play("Deflection of compass plug key");
    }
    //
    void _Magnetic_effect_barmagnet_animationAnimmethod()
    {

        anim = magneteffbarmagnet.GetComponent<Animator>();
        anim.Play("New magnet animation");
    }
    //
    void _Compass_needle_animationAnimmethod()
    {

        anim = compassneedle.GetComponent<Animator>();
        anim.Play("Needles compass animation");
    }
    //
    void _Potato_compass_needle_animationAnimmethod()
    {

        anim = potatocompassneedle.GetComponent<Animator>();
        anim.Play("Potato needle animation");
    }
    //
    void _Hand_animationAnimmethod()
    {

        anim = hand.GetComponent<Animator>();
        anim.Play("Hand animation");
    }
    //
    void _Nichrome_wire_animationAnimmethod()
    {

        anim = nichromewirem.GetComponent<Animator>();
        anim.Play("nichrome wire animation");
    }
    //
    void _Nailwoodplate_plugkeyAnimmethod()
    {

        anim = nailplugkey.GetComponent<Animator>();
        anim.Play("Nail wood plug key animation");
    }
    //
    void _Wall_compassAnimmethod()
    {

        anim = wallcompass.GetComponent<Animator>();
        anim.Play("Compass animation");
    }
    //
    void _Hanging_magnet_newAnimmethod()
    {

        anim = hangingmagnet.GetComponent<Animator>();
        anim.Play("Bar magnet animation",-1,0);
    }
    //
    void _Hitter_animationAnimmethod()
    {

        anim = hitter.GetComponent<Animator>();
        anim.Play("Hitter animation");
    }
    //
    void _Hitter_plugkeyanimationAnimmethod()
    {

        anim = hitterplugkey.GetComponent<Animator>();
        anim.Play("Hitter plug key animation");
    }
    //
    void _Hitter_expanimationAnimmethod()
    {

        anim = hitterexp.GetComponent<Animator>();
        anim.Play("Hitter explanation animation");
    }
    //
    void _Compass_pointers_animationAnimmethod()
    {

        anim = compasspointer.GetComponent<Animator>();
        anim.Play("wheelpointer");
    }

    public void MiniGameStart()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void MiniGameEnd()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(true);
    }

    private int droppedObj;
    public TargetController circuitminigame;
    public void DropCircuitObjects()
    {
        droppedObj++;
        if(droppedObj == 5)
        {
            circuitminigame.EndMiniGame();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public MeshRenderer door;
    public Material clearDoor;
    public void turnOnSwitch(Animator anim)
    {
        anim.SetTrigger("Trigger");
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        StartCoroutine(ClearDoor());
    }
    public void turnOnMagnetSwitch(Animator anim)
    {
        anim.SetTrigger("Trigger");
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        StartCoroutine(AttractNails());
    }
    public Animator nailsAnim;
    IEnumerator AttractNails()
    {
        yield return new WaitForSeconds(2);
        nailsAnim.SetTrigger("Trigger");
    }

    IEnumerator ClearDoor()
    {
        yield return new WaitForSeconds(2);
        door.material = clearDoor;
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void StepComp()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    private bool collectedTresure;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || InventoryManager.Instance.player.InteractIspressed)
        {
            if (!collectedTresure)
            {
                StartCoroutine(CheckForItems());
            }
        }
    }

    private int index = 0;
    public GameObject lv2Switch;
    public void Lv1TurnOnMeshRend(MeshRenderer mesh)
    {
        index++;
        mesh.enabled = true;

        if (index == 6)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            circuitminigame.EndMiniGame();
            lv2Switch.SetActive(true);
            InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        }
    }
    IEnumerator CheckForItems()
    {
        yield return new WaitForSeconds(1);
        if (InventoryManager.Instance.items.Count != 0)
        {
            foreach (Item itm in InventoryManager.Instance.items)
            {
                if (itm.item.itemName == "Tresure" && !collectedTresure)
                {
                    Debug.Log("KEY");
                    InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                    collectedTresure = true;
                    InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                    break;
                }
            }
        }
    }

}
