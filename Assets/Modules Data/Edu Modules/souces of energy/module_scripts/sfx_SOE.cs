using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_SOE : MonoBehaviour
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

    public Light myLight;


    public GameObject title_1;
    public GameObject title_2;
    public GameObject title_3;
    public GameObject title_4;
    public GameObject title_5;
    public GameObject title_6;
    public GameObject title_7;
    public GameObject Description_n;
    public GameObject Description_b;
    public GameObject Description_o;
    public GameObject Description_t;
    public GameObject Description_oc;
    public GameObject Description_g;
    public GameObject Description_s;
    public GameObject Description_w;
    public GameObject Description_h;
    public GameObject Description_te;
    public GameObject Description_og;
    public GameObject Description_ng;
    public GameObject Description_we;
    public GameObject Description_1;
    public GameObject Description_2;
    public GameObject Description_3;
    public GameObject Description_4;
    public GameObject Description_5;
    public GameObject Description_6;
    public GameObject Description_7;
    public GameObject Description_8;
    public GameObject Description_9;
    public GameObject Description_10;
    public GameObject Description_11;
    public GameObject Description_12;
    public GameObject Description_13;
    public GameObject ui_button_1;
    public GameObject ui_button_2;
    public GameObject ui_button_3;
    public GameObject ui_button_4;
    public GameObject hc_;
    public GameObject nu_fu;
    public GameObject fo_fu;
    public GameObject methane_;
    public GameObject coshn;
    public GameObject con_electricity;
    public GameObject U_235;
    public GameObject U_45_text;
    public GameObject bb_1;
    public GameObject bb_2;
    public GameObject bb_3;
    public GameObject bb_4;
    public GameObject bulb_glow;
    public GameObject sun;


    // Exp - Animations
    private Animator anim;
    private AnimationClip clip;
    [Header("Explanation anims")]

    public GameObject nucleaar_fuels;
    public GameObject u_45;
    public GameObject methane;
    public GameObject hc;
    public GameObject se;
    public GameObject car;
    public GameObject s_anim_1;
    public GameObject s_anim_2;
    public GameObject bladehead_1;
    public GameObject bladehead_2;
    public GameObject bladehead_3;
    public GameObject bladehead_4;
    public GameObject bladehead_5;
    public GameObject bladehead_6;
    public GameObject bladehead_7;
    public GameObject bladehead_8;
    public GameObject bladehead_9;
    public GameObject bladehead_10;
    public GameObject bladehead_11;
    public GameObject bladehead_12;
    public GameObject bladehead_13;
    public GameObject bladehead_14;
    public GameObject fan;
    public GameObject shaft_1;
    public GameObject shaft_2;
    public GameObject shaft_3;
    public GameObject shaft_4;
    public GameObject shaft_5;
    public GameObject dam_water;
    public GameObject turbine_wheel;
    public GameObject tidal_turbine;
    public GameObject tree;



    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip in_1;
    public AudioClip in_2;
    public AudioClip in_3;
    public AudioClip in_4;
    public AudioClip in_5;
    public AudioClip in_6;
    public AudioClip ty_o_soe;
    public AudioClip nr_soe;
    public AudioClip r_soe;
    public AudioClip r_soe_in;
    public AudioClip fr_fu;
    public AudioClip co_1;
    public AudioClip co_2;
    public AudioClip co_3;
    public AudioClip cr_ol_1;
    public AudioClip cr_ol_2;
    public AudioClip cr_ol_3;
    public AudioClip cr_ol_4;
    public AudioClip na_g_1;
    public AudioClip na_g_2;
    public AudioClip na_g_3;
    public AudioClip na_g_4;
    public AudioClip th_en;
    public AudioClip nu_en_1;
    public AudioClip nu_en_2;
    public AudioClip nu_en_3;
    public AudioClip nu_en_4;
    public AudioClip nu_en_5;
    public AudioClip nu_en_6;
    public AudioClip nu_en_7;
    public AudioClip nu_en_8;
    public AudioClip coff_1;
    public AudioClip coff_2;
    public AudioClip coff_3;
    public AudioClip coff_4;
    public AudioClip coff_5;
    public AudioClip resoe_1;
    public AudioClip resoe_2;
    public AudioClip resoe_3;
    public AudioClip resoe_4;
    public AudioClip resoe_5;
    public AudioClip so_eg_1;
    public AudioClip so_eg_2;
    public AudioClip so_eg_3;
    public AudioClip so_eg_4;
    public AudioClip so_eg_5;
    public AudioClip so_eg_6;
    public AudioClip so_eg_7;
    public AudioClip so_eg_8;
    public AudioClip w_eg_1;
    public AudioClip w_eg_2;
    public AudioClip w_eg_3;
    public AudioClip h_eg_1;
    public AudioClip h_eg_2;
    public AudioClip h_eg_3;
    public AudioClip ge_eg_1;
    public AudioClip ge_eg_2;
    public AudioClip ge_eg_3;
    public AudioClip bm_eg_1;
    public AudioClip bm_eg_2;
    public AudioClip bm_eg_3;
    public AudioClip bm_eg_4;
    public AudioClip bm_eg_5;
    public AudioClip bm_eg_6;
    public AudioClip bm_eg_7;
    public AudioClip bm_eg_8;
    public AudioClip bm_eg_9;
    public AudioClip bm_eg_10;
    public AudioClip td_eg_1;
    public AudioClip td_eg_2;
    public AudioClip td_eg_3;
    public AudioClip we_eg_1;
    public AudioClip we_eg_2;
    public AudioClip ot_eg_1;
    public AudioClip ot_eg_2;
    public AudioClip ot_eg_3;
    public AudioClip ot_eg_4;
    public AudioClip ot_eg_5;
    public AudioClip ot_eg_6;


    //keyframes
    public int keyframe;
    public int keyframe_1;
    public int keyframe_2;
    public int keyframe_3;


    public GameObject mainbuttons;




    // Start is called before the first frame update
    void Start()
    {
        //  myLight = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }


    //spotlight

    void _light1_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(776.169983f, 62.6800003f, 461.970001f);
    }
    //
    void _light2_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(764.901001f, 64.3199997f, 459.549011f);
    }
    //
    void _light3_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(769.441467f, 62.5299988f, 466.637939f);
    }
    //
    void _light4_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(762.058472f, 65.2310028f, 463.007202f);
    }
    //
    void _light5_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(762.058472f, 61.9599991f, 462.408997f);
    }
    //
    void _light6_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(764.039978f, 64.8300018f, 465.790009f);
    }
    //
    void _light7_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(765.234009f, 62.2859993f, 466.234985f);
    }
    //
    void _light8_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(764.039978f, 64.8300018f, 465.790009f);
    }
    //
    void _light9_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(776.169983f, 62.6800003f, 461.970001f);
    }
    //



    //camera_explanatory play pause

    void _camExp_Pause()
    {
        gameObject.GetComponent<Animator>().speed = 0f;
    }
    //
    void _title_1_MethodON()
    {
        title_1.SetActive(true);
    }
    //
    void _title_2_MethodON()
    {
        title_2.SetActive(true);
    }
    //
    void _title_3_MethodON()
    {
        title_3.SetActive(true);
    }
    //
    void _title_4_MethodON()
    {
        title_4.SetActive(true);
    }
    //
    void _title_5_MethodON()
    {
        title_5.SetActive(true);
    }
    //
    void _title_6_MethodON()
    {
        title_6.SetActive(true);
    }
    //
    void _title_7_MethodON()
    {
        title_7.SetActive(true);
    }
    //
    void _Description_n_MethodON()
    {
        Description_n.SetActive(true);
    }
    //
    void _Description_b_MethodON()
    {
        Description_b.SetActive(true);
    }
    //
    void _Description_o_MethodON()
    {
        Description_o.SetActive(true);
    }
    //
    void _Description_t_MethodON()
    {
        Description_t.SetActive(true);
    }
    //
    void _Description_oc_MethodON()
    {
        Description_oc.SetActive(true);
    }
    //
    void _Description_g_MethodON()
    {
        Description_g.SetActive(true);
    }
    //
    void _Description_s_MethodON()
    {
        Description_s.SetActive(true);
    }
    //
    void _Description_w_MethodON()
    {
        Description_w.SetActive(true);
    }
    //
    void _Description_h_MethodON()
    {
        Description_h.SetActive(true);
    }
    //
    void _Description_te_MethodON()
    {
        Description_te.SetActive(true);
    }
    //
    void _Description_og_MethodON()
    {
        Description_og.SetActive(true);
    }
    //
    void _Description_ng_MethodON()
    {
        Description_ng.SetActive(true);
    }
    //
    void _Description_we_MethodON()
    {
        Description_we.SetActive(true);
    }
    //
    void _Description_1_MethodON()
    {
        Description_1.SetActive(true);
    }
    //
    void _Description_2_MethodON()
    {
        Description_2.SetActive(true);
    }
    //
    void _Description_3_MethodON()
    {
        Description_3.SetActive(true);
    }
    //
    void _Description_4_MethodON()
    {
        Description_4.SetActive(true);
    }
    //
    void _Description_5_MethodON()
    {
        Description_5.SetActive(true);
    }
    //
    void _Description_6_MethodON()
    {
        Description_6.SetActive(true);
    }
    //
    void _Description_7_MethodON()
    {
        Description_7.SetActive(true);
    }
    //
    void _Description_8_MethodON()
    {
        Description_8.SetActive(true);
    }
    //
    void _Description_9_MethodON()
    {
        Description_9.SetActive(true);
    }
    //
    void _Description_10_MethodON()
    {
        Description_10.SetActive(true);
    }
    //
    void _Description_11_MethodON()
    {
        Description_11.SetActive(true);
    }
    //
    void _Description_12_MethodON()
    {
        Description_12.SetActive(true);
    }
    //
    void _Description_13_MethodON()
    {
        Description_13.SetActive(true);
    }
    //
    void _ui_button_1_MethodON()
    {
        ui_button_1.SetActive(true);
    }
    //
    void _ui_button_2_MethodON()
    {
        ui_button_2.SetActive(true);
    }
    //
    void _ui_button_3_MethodON()
    {
        ui_button_3.SetActive(true);
    }
    //
    void _ui_button_4_MethodON()
    {
        ui_button_4.SetActive(true);
    }
    //
    void _hc_MethodON()
    {
        hc_.SetActive(true);
    }
    //
    void _nu_fu_MethodON()
    {
        nu_fu.SetActive(true);
    }
    //
    void _fo_fu_MethodON()
    {
        fo_fu.SetActive(true);
    }
    //
    void _methane_MethodON()
    {
        methane_.SetActive(true);
    }
    //
    void _coshn_MethodON()
    {
        coshn.SetActive(true);
    }
    //
    void _con_electricity_MethodON()
    {
        con_electricity.SetActive(true);

    }
   
    void _con_electricity_MethodOFF()
    {
        con_electricity.SetActive(false);
    }
    //
    void _U_235_MethodON()
    {
        U_235.SetActive(true);

    }

    void _U_235_MethodOFF()
    {
        U_235.SetActive(false);
    }
    //
    void _U_45_text_MethodON()
    {
        U_45_text.SetActive(true);

    }

    void _U_45_text_MethodOFF()
    {
        U_45_text.SetActive(false);
    }
    //
    void _bb_1_MethodON()
    {
        bb_1.SetActive(true);

    }

    void _bb_1_MethodOFF()
    {
        bb_1.SetActive(false);
    }
    //
    void _bb_2_MethodON()
    {
        bb_2.SetActive(true);

    }

    void _bb_2_MethodOFF()
    {
        bb_2.SetActive(false);
    }
    //
    void _bb_3_MethodON()
    {
        bb_3.SetActive(true);

    }

    void _bb_3_MethodOFF()
    {
        bb_3.SetActive(false);
    }
    //
    void _bb_4_MethodON()
    {
        bb_4.SetActive(true);

    }

    void _bb_4_MethodOFF()
    {
        bb_4.SetActive(false);
    }
    //
    void _bulb_glow_MethodON()
    {
        bulb_glow.SetActive(true);

    }
    //
    void _sun_MethodON()
    {
        sun.SetActive(true);

    }

    void _sun_MethodOFF()
    {
        sun.SetActive(false);
    }
    //
    void _nuclear_fuels_Animmethod()
    {

        anim = nucleaar_fuels.GetComponent<Animator>();
        anim.Play("nuclear fuels");
    }
    //
    void _u_45_Animmethod()
    {

        anim = u_45.GetComponent<Animator>();
        anim.Play("U-45");
    }
    //
    void _methane_Animmethod()
    {

        anim = methane.GetComponent<Animator>();
        anim.Play("methane_anim");
    }
    //
    void _hc_Animmethod()
    {

        anim = hc.GetComponent<Animator>();
        anim.Play("hc");
    }
    //
    void _se_Animmethod()
    {

        anim = se.GetComponent<Animator>();
        anim.Play("train_anim_soe");
    }
    //
    void _car_Animmethod()
    {

        anim = car.GetComponent<Animator>();
        anim.Play("car_anim_1");
    }
    //
    void _s_anim_1_Animmethod()
    {

        anim = s_anim_1.GetComponent<Animator>();
        anim.Play("s_anim_1");
    }
    //
    void _s_anim_2_Animmethod()
    {

        anim = s_anim_2.GetComponent<Animator>();
        anim.Play("s_anim_2");
    }
    //
    void _fan_Animmethod()
    {

        anim = fan.GetComponent<Animator>();
        anim.Play("fan_anim");
    }
    //
    void _bladehead_1_Animmethod()
    {

        anim = bladehead_1.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_2.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_3.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_4.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_5.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_6.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_7.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_8.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_9.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_10.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_11.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_12.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_13.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");
        anim = bladehead_14.GetComponent<Animator>();
        anim.Play("Armature_Explanation anim for camera 1");

    }
    //
    void _shaft_Animmethod()
    {

        anim = shaft_1.GetComponent<Animator>();
        anim.Play("shaft_anim_");
        anim = shaft_2.GetComponent<Animator>();
        anim.Play("shaft_anim_");
        anim = shaft_3.GetComponent<Animator>();
        anim.Play("shaft_anim_");
        anim = shaft_4.GetComponent<Animator>();
        anim.Play("shaft_anim_");
        anim = shaft_5.GetComponent<Animator>();
        anim.Play("shaft_anim_");
    }
    //
    void _hydro_Animmethod()
    {

        anim = dam_water.GetComponent<Animator>();
        anim.Play("Scene");
        anim = turbine_wheel.GetComponent<Animator>();
        anim.Play("turbine_wheel_anim");
  
    }
    //
    void _tidal_turbine_Animmethod()
    {

        anim = fan.GetComponent<Animator>();
        anim.Play("tidal_turbine");
    }
    //
    void _tree_Animmethod()
    {

        anim = tree.GetComponent<Animator>();
        anim.Play("tree_anim_2");
    }
    //






    void _in_1_audioMethod()
    {
        myAudio.clip = in_1;
        myAudio.Play();
    }
    //
    void _in_2_audioMethod()
    {
        myAudio.clip = in_2;
        myAudio.Play();
    }
    //
    void _in_3_audioMethod()
    {
        myAudio.clip = in_3;
        myAudio.Play();
    }
    //
    void _in_4_audioMethod()
    {
        myAudio.clip = in_4;
        myAudio.Play();
    }
    //
    void _in_5_audioMethod()
    {
        myAudio.clip = in_5;
        myAudio.Play();
    }
    //
    void _in_6_audioMethod()
    {
        myAudio.clip = in_6;
        myAudio.Play();
    }
    //
    void _ty_o_soe_audioMethod()
    {
        myAudio.clip = ty_o_soe;
        myAudio.Play();
    }
    //
    void _nr_soe_audioMethod()
    {
        myAudio.clip = nr_soe;
        myAudio.Play();
    }
    //
    void _r_soe_audioMethod()
    {
        myAudio.clip = r_soe;
        myAudio.Play();
    }
    //
    void _r_soe_in_audioMethod()
    {
        myAudio.clip = r_soe_in;
        myAudio.Play();
    }
    //
    void _fr_fu_audioMethod()
    {
        myAudio.clip = fr_fu;
        myAudio.Play();
    }
    //
    void _co_1_audioMethod()
    {
        myAudio.clip = co_1;
        myAudio.Play();
    }
    //
    void _co_2_audioMethod()
    {
        myAudio.clip = co_2;
        myAudio.Play();
    }
    //
    //
    void _co_3_audioMethod()
    {
        myAudio.clip = co_3;
        myAudio.Play();
    }
    //
    void _cr_ol_1_audioMethod()
    {
        myAudio.clip = cr_ol_1;
        myAudio.Play();
    }
    //
    void _cr_ol_2_audioMethod()
    {
        myAudio.clip = cr_ol_2;
        myAudio.Play();
    }
    //
    void _cr_ol_3_audioMethod()
    {
        myAudio.clip = cr_ol_3;
        myAudio.Play();
    }
    //
    void _cr_ol_4_audioMethod()
    {
        myAudio.clip = cr_ol_4;
        myAudio.Play();
    }
    //
    void _na_g_1_audioMethod()
    {
        myAudio.clip = na_g_1;
        myAudio.Play();
    }
    //
    void _na_g_2_audioMethod()
    {
        myAudio.clip = na_g_2;
        myAudio.Play();
    }
    //
    void _na_g_3_audioMethod()
    {
        myAudio.clip = na_g_3;
        myAudio.Play();
    }
    //
    void _na_g_4_audioMethod()
    {
        myAudio.clip = na_g_4;
        myAudio.Play();
    }
    //
    void _th_en_audioMethod()
    {
        myAudio.clip = th_en;
        myAudio.Play();
    }
    //
    void _nu_en_1_audioMethod()
    {
        myAudio.clip = nu_en_1;
        myAudio.Play();
    }
    //
    void _nu_en_2_audioMethod()
    {
        myAudio.clip = nu_en_2;
        myAudio.Play();
    }
    //
    void _nu_en_3_audioMethod()
    {
        myAudio.clip = nu_en_3;
        myAudio.Play();
    }
    //
    void _nu_en_4_audioMethod()
    {
        myAudio.clip = nu_en_4;
        myAudio.Play();
    }
    //
    void _nu_en_5_audioMethod()
    {
        myAudio.clip = nu_en_5;
        myAudio.Play();
    }
    //
    void _nu_en_6_audioMethod()
    {
        myAudio.clip = nu_en_6;
        myAudio.Play();
    }
    //
    void _nu_en_7_audioMethod()
    {
        myAudio.clip = nu_en_7;
        myAudio.Play();
    }
    //
    void _nu_en_8_audioMethod()
    {
        myAudio.clip = nu_en_8;
        myAudio.Play();
    }
    //
    void _coff_1_audioMethod()
    {
        myAudio.clip = coff_1;
        myAudio.Play();
    }
    //
    void _coff_2_audioMethod()
    {
        myAudio.clip = coff_2;
        myAudio.Play();
    }
    //
    void _coff_3_audioMethod()
    {
        myAudio.clip = coff_3;
        myAudio.Play();
    }
    //
    void _coff_4_audioMethod()
    {
        myAudio.clip = coff_4;
        myAudio.Play();
    }
    //
    void _coff_5_audioMethod()
    {
        myAudio.clip = coff_5;
        myAudio.Play();
    }
    //
    void _resoe_1_audioMethod()
    {
        myAudio.clip = resoe_1;
        myAudio.Play();
    }
    //
    void _resoe_2_audioMethod()
    {
        myAudio.clip = resoe_2;
        myAudio.Play();
    }
    //
    void _resoe_3_audioMethod()
    {
        myAudio.clip = resoe_3;
        myAudio.Play();
    }
    //
    void _resoe_4_audioMethod()
    {
        myAudio.clip = resoe_4;
        myAudio.Play();
    }
    //
    void _resoe_5_audioMethod()
    {
        myAudio.clip = resoe_5;
        myAudio.Play();
    }
    //
    void _so_eg_1_audioMethod()
    {
        myAudio.clip = so_eg_1;
        myAudio.Play();
    }
    //
    void _so_eg_2_audioMethod()
    {
        myAudio.clip = so_eg_2;
        myAudio.Play();
    }
    //
    void _so_eg_3_audioMethod()
    {
        myAudio.clip = so_eg_3;
        myAudio.Play();
    }
    //
    void _so_eg_4_audioMethod()
    {
        myAudio.clip = so_eg_4;
        myAudio.Play();
    }
    //
    void _so_eg_5_audioMethod()
    {
        myAudio.clip = so_eg_5;
        myAudio.Play();
    }
    //
    void _so_eg_6_audioMethod()
    {
        myAudio.clip = so_eg_6;
        myAudio.Play();
    }
    //
    void _so_eg_7_audioMethod()
    {
        myAudio.clip = so_eg_7;
        myAudio.Play();
    }
    //
    void _so_eg_8_audioMethod()
    {
        myAudio.clip = so_eg_8;
        myAudio.Play();
    }
    //
    void _w_eg_1_audioMethod()
    {
        myAudio.clip = w_eg_1;
        myAudio.Play();
    }
    //
    void _w_eg_2_audioMethod()
    {
        myAudio.clip = w_eg_2;
        myAudio.Play();
    }
    //
    void _w_eg_3_audioMethod()
    {
        myAudio.clip = w_eg_3;
        myAudio.Play();
    }
    //
    void _h_eg_1_audioMethod()
    {
        myAudio.clip = h_eg_1;
        myAudio.Play();
    }
    //
    void _h_eg_2_audioMethod()
    {
        myAudio.clip = h_eg_2;
        myAudio.Play();
    }
    //
    void _h_eg_3_audioMethod()
    {
        myAudio.clip = h_eg_3;
        myAudio.Play();
    }
    //
    void _ge_eg_1_audioMethod()
    {
        myAudio.clip = ge_eg_1;
        myAudio.Play();
    }
    //
    void _ge_eg_2_audioMethod()
    {
        myAudio.clip = ge_eg_2;
        myAudio.Play();
    }
    //
    void _ge_eg_3_audioMethod()
    {
        myAudio.clip = ge_eg_3;
        myAudio.Play();
    }
    //
    void _bm_eg_1_audioMethod()
    {
        myAudio.clip = bm_eg_1;
        myAudio.Play();
    }
    //
    void _bm_eg_2_audioMethod()
    {
        myAudio.clip = bm_eg_2;
        myAudio.Play();
    }
    //
    void _bm_eg_3_audioMethod()
    {
        myAudio.clip = bm_eg_3;
        myAudio.Play();
    }
    //
    void _bm_eg_4_audioMethod()
    {
        myAudio.clip = bm_eg_4;
        myAudio.Play();
    }
    //
    void _bm_eg_5_audioMethod()
    {
        myAudio.clip = bm_eg_5;
        myAudio.Play();
    }
    //
    void _bm_eg_6_audioMethod()
    {
        myAudio.clip = bm_eg_6;
        myAudio.Play();
    }
    //
    void _bm_eg_7_audioMethod()
    {
        myAudio.clip = bm_eg_7;
        myAudio.Play();
    }
    //
    void _bm_eg_8_audioMethod()
    {
        myAudio.clip = bm_eg_8;
        myAudio.Play();
    }
    //
    void _bm_eg_9_audioMethod()
    {
        myAudio.clip = bm_eg_9;
        myAudio.Play();
    }
    //
    void _bm_eg_10_audioMethod()
    {
        myAudio.clip = bm_eg_10;
        myAudio.Play();
    }
    //
    void _td_eg_1_audioMethod()
    {
        myAudio.clip = td_eg_1;
        myAudio.Play();
    }
    //
    void _td_eg_2_audioMethod()
    {
        myAudio.clip = td_eg_2;
        myAudio.Play();
    }
    //
    void _td_eg_3_audioMethod()
    {
        myAudio.clip = td_eg_3;
        myAudio.Play();
    }
    //
    void _we_eg_1_audioMethod()
    {
        myAudio.clip = we_eg_1;
        myAudio.Play();
    }
    //
    void _we_eg_2_audioMethod()
    {
        myAudio.clip = we_eg_2;
        myAudio.Play();
    }
    //
    void _ot_eg_1_audioMethod()
    {
        myAudio.clip = ot_eg_1;
        myAudio.Play();
    }
    //
    void _ot_eg_2_audioMethod()
    {
        myAudio.clip = ot_eg_2;
        myAudio.Play();
    }
    //
    void _ot_eg_3_audioMethod()
    {
        myAudio.clip = ot_eg_3;
        myAudio.Play();
    }
    //
    void _ot_eg_4_audioMethod()
    {
        myAudio.clip = ot_eg_4;
        myAudio.Play();
    }
    //
    void _ot_eg_5_audioMethod()
    {
        myAudio.clip = ot_eg_5;
        myAudio.Play();
    }
    //
    void _ot_eg_6_audioMethod()
    {
        myAudio.clip = ot_eg_6;
        myAudio.Play();
    }
    //















    public void a_1()
    {
        anim = gameObject.GetComponent<Animator>();
        clip = anim.GetCurrentAnimatorClipInfo(0)[0].clip;
        float time = (float)keyframe / clip.frameRate;
        float normalizeTime = time / clip.length;
        anim.Play("camera", 0, normalizeTime);
        gameObject.GetComponent<Animator>().speed = 1f;
    }




    public void a_2()
    {
        anim = gameObject.GetComponent<Animator>();
        clip = anim.GetCurrentAnimatorClipInfo(0)[0].clip;
        float time = (float)keyframe_1 / clip.frameRate;
        float normalizeTime = time / clip.length;
        anim.Play("camera", 0, normalizeTime);
        gameObject.GetComponent<Animator>().speed = 1f;
    }




    public void a_3()
    {
        anim = gameObject.GetComponent<Animator>();
        clip = anim.GetCurrentAnimatorClipInfo(0)[0].clip;
        float time = (float)keyframe_2 / clip.frameRate;
        float normalizeTime = time / clip.length;
        anim.Play("camera", 0, normalizeTime);
        gameObject.GetComponent<Animator>().speed = 1f;
    }


    public void a_4()
    {
        anim = gameObject.GetComponent<Animator>();
        clip = anim.GetCurrentAnimatorClipInfo(0)[0].clip;
        float time = (float)keyframe_3 / clip.frameRate;
        float normalizeTime = time / clip.length;
        anim.Play("camera", 0, normalizeTime);
        gameObject.GetComponent<Animator>().speed = 1f;

    }




    void camerastop_buttonshow()
    {
        gameObject.GetComponent<Animator>().speed = 0f;

        mainbuttons.SetActive(true);

    }











}
