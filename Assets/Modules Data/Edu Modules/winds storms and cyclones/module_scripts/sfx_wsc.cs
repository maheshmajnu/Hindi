using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_wsc : MonoBehaviour
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


    public GameObject cube;
    public GameObject rain;
    public GameObject clouds;
    public GameObject bb;
    public GameObject carshed_tin;
    public GameObject tin;
    public GameObject carshed_paper;
    public GameObject paper;
    public GameObject tornado;
    public GameObject map;
    public GameObject sun;
    public GameObject of_1;
    public GameObject of_2;
    public GameObject title_1;
    public GameObject title_2;
    public GameObject title_3;
    public GameObject title_4;
    public GameObject title_5;
    public GameObject title_6;
    public GameObject title_7;
    public GameObject title_8;
    public GameObject title_9;
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
    public GameObject Afterburner;
    public GameObject boy;
    public GameObject line_1;
    public GameObject line_2;
    public GameObject line_3;
    public GameObject line_4;
    public GameObject line_5;
    public GameObject line_6;
    public GameObject line_7;
    public GameObject text_1;
    public GameObject text_2;
    public GameObject text_3;
    public GameObject text_4;
    public GameObject text_5;
    public GameObject text_6;
    public GameObject text_7;
    public GameObject text_8;
    public GameObject text_9;
    public GameObject text_10;
    public GameObject text_11;
    public GameObject text_12;
    public GameObject text_13;
    public GameObject water;
    public GameObject sphere_2;
    public GameObject sphere_3;
    public GameObject ui_button;
    public GameObject steam;
    public GameObject burner_exp;
    public GameObject sun_light;
    public GameObject tv;
    public GameObject wb;
    public GameObject thunderstrom;
    public GameObject phone;
    public GameObject lungs;
    public GameObject remy_boy;

    // Exp - Animations
    private Animator anim;
    [Header("Explanation anims")]

    public GameObject aa_1;
    public GameObject aa_2;
    public GameObject aa_3;
    public GameObject aa_3_1;
    public GameObject wc_anim_2;
    public GameObject house_anim;
    public GameObject red_arrow;
    public GameObject mw_anim;
    public GameObject wc_anim_3;
    public GameObject wc_map_anim_;
    public GameObject kite;
    public GameObject ba_anim;
    public GameObject wa;
    public GameObject ca;
    public GameObject human_blue_arrows_;

    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip in_1;
    public AudioClip in_2;
    public AudioClip in_3;
    public AudioClip in_4;
    public AudioClip wn_1;
    public AudioClip wn_2;
    public AudioClip wn_3;
    public AudioClip wn_4;
    public AudioClip aep_1;
    public AudioClip aep_2;
    public AudioClip aep_3;
    public AudioClip aep_4;
    public AudioClip aep_5;
    public AudioClip aep_6;
    public AudioClip aep_7;
    public AudioClip aep_8;
    public AudioClip aep_9;
    public AudioClip aep_10;
    public AudioClip wap_1;
    public AudioClip wap_2;
    public AudioClip wap_3;
    public AudioClip wap_4;
    public AudioClip wap_5;
    public AudioClip wc_1;
    public AudioClip wc_2;
    public AudioClip wc_3;
    public AudioClip wc_4;
    public AudioClip wc_5;
    public AudioClip wc_6;
    public AudioClip mw_1;
    public AudioClip mw_2;
    public AudioClip mw_3;
    public AudioClip mw_4;
    public AudioClip mw_5;
    public AudioClip ts_1;
    public AudioClip ts_2;
    public AudioClip ts_3;
    public AudioClip ts_4;
    public AudioClip ts_5;
    public AudioClip cyc_1;
    public AudioClip cyc_2;
    public AudioClip cyc_3;
    public AudioClip cyc_4;
    public AudioClip cyc_5;
    public AudioClip cyc_6;
    public AudioClip cyc_7;
    public AudioClip cyc_8;
    public AudioClip cyc_9;
    public AudioClip cyc_10;
    public AudioClip td;
    public AudioClip esm_1;
    public AudioClip esm_2;
    public AudioClip esm_3;
    public AudioClip esm_4;
    public AudioClip esm_5;
    public AudioClip esm_6;
    public AudioClip esm_7;
    public AudioClip esm_8;





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

    void _earthlight_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(357.290009f, 13.7399998f, 128.549942f);
    }
    //
    void _earthlight2_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(357.290009f, 13.7399998f, 128.549942f);
    }
    //
    void _earthlight3_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(357.290009f, 13.7399998f, 128.549942f);
    }
    //
    void _burnerlight_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(353.640015f, 13.7089996f, 131.910004f);
    }
    //
    void _lungslight_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(359.190002f, 13.7399998f, 128.549942f);
    }
    //
    void _boylight_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(356.959991f, 13.7399998f, 134.039993f);
    }
    //
    void _boylight2_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(356.959991f, 13.7399998f, 134.039993f);
    }
    //
    void _burnerlight2_methodON()
    {
        myLight.intensity = 5f;
        myLight.transform.position = new Vector3(353.640015f, 13.7089996f, 131.910004f);
    }
    //




    //camera_explanatory play pause

    void _camExp_Pause()
    {
        gameObject.GetComponent<Animator>().speed = 0f;
    }


    //
    void _ui_button_MethodON()
    {
        ui_button.SetActive(true);
    }

    //
    void _lines_1_MethodON()
    {
        line_1.SetActive(true);
        line_2.SetActive(true);
        line_3.SetActive(true);
    }
    //
    void _lines_1_MethodOFF()
    {
        line_1.SetActive(false);
        line_2.SetActive(false);
        line_3.SetActive(false);
    }
    //
    void _lines_2_MethodON()
    {
        line_5.SetActive(true);
        line_6.SetActive(true);
        line_7.SetActive(true);
    }
    //
    void _lines_2_MethodOFF()
    {
        line_5.SetActive(false);
        line_6.SetActive(false);
        line_7.SetActive(false);
    }
    //
    void _lines_3_MethodON()
    {
        text_9.SetActive(true);
        text_10.SetActive(true);
        text_11.SetActive(true);
    }
    //
    void _lines_3_MethodOFF()
    {
        text_9.SetActive(false);
        text_10.SetActive(false);
        text_11.SetActive(false);
    }
    //
    void _text_1_MethodON()
    {
        text_1.SetActive(true);
        text_2.SetActive(true);
    }
    //
    void _text_1_MethodOFF()
    {
        text_1.SetActive(false);
        text_2.SetActive(false);
    }
    //
    void _text_3_MethodON()
    {
        text_3.SetActive(true);
       
    }
    //
    void _text_3_MethodOFF()
    {
        text_3.SetActive(false);
        
    }
    //
    void _text_4_MethodON()
    {
        text_4.SetActive(true);
    }
    //
    void _text_5_MethodON()
    {
        text_5.SetActive(true);
    }
    //
    void _text_6_MethodON()
    {
        text_6.SetActive(true);
    }
    //
    void _text_7_MethodON()
    {
        text_7.SetActive(true);
    }
    //
    void _text_8_MethodON()
    {
        text_8.SetActive(true);
    }
    //
    void _text_12_MethodON()
    {
        text_12.SetActive(true);
    }
    //
    void _text_13_MethodON()
    {
        text_13.SetActive(true);
    }
    //
    void _line_4_MethodON()
    {
        line_4.SetActive(true);

    }
    //
    void _line_4_MethodOFF()
    {
        line_4.SetActive(false);

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
    void _title_8_MethodON()
    {
        title_8.SetActive(true);
    }
    //
    void _title_9_MethodON()
    {
        title_9.SetActive(true);
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
    void _bb_MethodON()
    {
        bb.SetActive(true);

    }

    void _bb_MethodOFF()
    {
        bb.SetActive(false);
    }
    //
    void _water_MethodON()
    {
        water.SetActive(true);

    }

    void _water_MethodOFF()
    {
        water.SetActive(false);
    }
    //
    void _cube_MethodON()
    {
        cube.SetActive(true);

    }

    void _cube_MethodOFF()
    {
        cube.SetActive(false);
    }
    //
    void _house_MethodON()
    {
        house_anim.SetActive(true);

    }

    void _house_MethodOFF()
    {
        house_anim.SetActive(false);
    }
    //
    void _red_arrow_MethodON()
    {
        red_arrow.SetActive(true);
        
    }

    void _red_arrow_MethodOFF()
    {
        red_arrow.SetActive(false);
        
    }
    //
    void _wc_anim_2_MethodON()
    {
     
        wc_anim_2.SetActive(true);
    }

    void _wc_anim_2_MethodOFF()
    {
        wc_anim_2.SetActive(false);
    }
    //
    void _mw_anim_MethodON()
    {
        mw_anim.SetActive(true);

    }

    void _mw_anim_MethodOFF()
    {
        mw_anim.SetActive(false);
    }
    //
    void _Tornado_MethodON()
    {
        tornado.SetActive(true);

    }

    void _Tornado_MethodOFF()
    {
        tornado.SetActive(false);
    }
    //
    void _rain_MethodON()
    {
        rain.SetActive(true);

    }

    void _rain_MethodOFF()
    {
        rain.SetActive(false);
    }
    //
    void _tin_MethodON()
    {
        tin.SetActive(true);

    }

    void _tin_MethodOFF()
    {
        tin.SetActive(false);
    }
    //
    void _carshedtin_MethodON()
    {
        carshed_tin.SetActive(true);

    }

    void _carshedtin_MethodOFF()
    {
        carshed_tin.SetActive(false);
    }
    //
    void _paper_MethodON()
    {
        paper.SetActive(true);

    }

    void _paper_MethodOFF()
    {
        paper.SetActive(false);
    }
    //
    void _carshed_paper_MethodON()
    {
        carshed_paper.SetActive(true);

    }

    void _carshed_paper_MethodOFF()
    {
        carshed_paper.SetActive(false);
    }
    //
    void _map_MethodON()
    {
        map.SetActive(true);

    }

    void _map_MethodOFF()
    {
        map.SetActive(false);
    }
    //
    void _of_1_MethodON()
    {
        of_1.SetActive(true);

    }

    void _of_1_MethodOFF()
    {
        of_1.SetActive(false);
    }
    //
    void _of_2_MethodON()
    {
        of_2.SetActive(true);

    }

    void _of_2_MethodOFF()
    {
        of_2.SetActive(false);
    }
    //
    void _aa_1_MethodON()
    {
        aa_1.SetActive(true);

    }

    void _aa_1_MethodOFF()
    {
        aa_1.SetActive(false);
    }
    //
    void _aa_2_MethodON()
    {
        aa_2.SetActive(true);

    }

    void _aa_2_MethodOFF()
    {
        aa_2.SetActive(false);
    }
    //
    void _aa_3_MethodON()
    {
        aa_3.SetActive(true);
        aa_3_1.SetActive(true);

    }

    void _aa_3_MethodOFF()
    {
        aa_3.SetActive(false);
        aa_3_1.SetActive(false);
    }
    //
    void _house_anim_2_MethodON()
    {
        house_anim.SetActive(true);

    }

    void _house_anim_2_MethodOFF()
    {
        house_anim.SetActive(false);
    }
    //
    void _clouds_MethodON()
    {
        clouds.SetActive(true);

    }

    void _clouds_MethodOFF()
    {
        clouds.SetActive(false);
    }
    //
    void _afterburner_MethodON()
    {
        Afterburner.SetActive(true);

    }

    void _afterburner_MethodOFF()
    {
        Afterburner.SetActive(false);
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
    void mw_anim_MethodON()
    {
        mw_anim.SetActive(true);

    }

    void mw_anim_MethodOFF()
    {
        mw_anim.SetActive(false);
    }
    //
    void _wc_anim_3_MethodON()
   {
        wc_anim_3.SetActive(true);

    }

    void _wc_anim_3_MethodOFF()
    {
        wc_anim_3.SetActive(false);

    }
    //
    void _kite_MethodON()
    {

        kite.SetActive(true);
        boy.SetActive(true);

    }

    void _kite_MethodOFF()
    {
        kite.SetActive(false);
        boy.SetActive(false);

    }
    //
    void _sphere_2_MethodON()
    {
        sphere_3.SetActive(true);
    }
    
    void _sphere_2_MethodOFF()
    {
        sphere_2.SetActive(false);
    }
    //
    void _sphere_3_MethodON()
    {
        sphere_3.SetActive(true);
    }
   
    void _sphere_3_MethodOFF()
    {
        sphere_3.SetActive(false);
    }
    //
    void _wa_MethodON()
    {
        wa.SetActive(true);
        ca.SetActive(true);
    }

    void _wa_MethodOFF()
    {
        wa.SetActive(false);
        ca.SetActive(false);
    }
    //
    void _steam_MethodON()
    {
        steam.SetActive(true);
    }
  
    void _steam_MethodOFF()
    {
        steam.SetActive(false);
    }
    //
    void _burner_exp_MethodON()
    {
        burner_exp.SetActive(true);

    }

    void _burner_exp_MethodOFF()
    {
        burner_exp.SetActive(false);
    }
    //
    void _sun_light_MethodON()
    {
        sun_light.SetActive(true);

    }

    void _sun_light_MethodOFF()
    {
        sun_light.SetActive(false);
    }
    //
    void _tv_MethodON()
    {
        tv.SetActive(true);

    }

    void _tv_MethodOFF()
    {
        tv.SetActive(false);
    }
    //
    void _wb_MethodON()
    {
        wb.SetActive(true);

    }

    void _wb_MethodOFF()
    {
        wb.SetActive(false);
    }
    //
    void _ts_MethodON()
    {
        thunderstrom.SetActive(true);

    }

    void _ts_MethodOFF()
    {
        thunderstrom.SetActive(false);
    }
    //
    void _phone_MethodON()
    {
        phone.SetActive(true);

    }

    void _phone_MethodOFF()
    {
        phone.SetActive(false);
    }
    //
    void _lungs_MethodON()
    {
        lungs.SetActive(true);

    }

    void _lungs_MethodOFF()
    {
        lungs.SetActive(false);
    }
    //
    void _remy_MethodON()
    {
        remy_boy.SetActive(true);

    }

    void _remy_MethodOFF()
    {
        remy_boy.SetActive(false);

    }
    //
    void _hba_MethodON()
    {
        human_blue_arrows_.SetActive(true);

    }

    void _hba_MethodOFF()
    {
        human_blue_arrows_.SetActive(false);

    }
    //











    //
    void _aa_1_Animmethod()
    {

        anim = aa_1.GetComponent<Animator>();
        anim.Play("aa_1");
    }
    //
    void _aa_2_Animmethod()
    {

        anim = aa_2.GetComponent<Animator>();
        anim.Play("aa_2");
    }
    //
    void _aa_3_Animmethod()
    {

        anim = aa_3.GetComponent<Animator>();
        anim.Play("aa_3");
        anim = aa_3_1.GetComponent<Animator>();
        anim.Play("aa_3");

    }
    //
    void _house_anim_Animmethod()
    {

        anim = house_anim.GetComponent<Animator>();
        anim.Play("house");
    }
    //
    void _wc_anim_2_Animmethod()
    {

        anim = wc_anim_2.GetComponent<Animator>();
        anim.Play("wc_anim_2");
    }
    //
    void _wc_anim_3_Animmethod()
    {

        anim = wc_anim_3.GetComponent<Animator>();
        anim.Play("wc_anim_3");
    }
    //
    void _red_arrow_anim_Animmethod()
    {
        anim = red_arrow.GetComponent<Animator>();
        anim.Play("red_arrow");
    }
    //
    void _mw_anim_Animmethod()
    {

        anim = mw_anim.GetComponent<Animator>();
        anim.Play("mw_anim");
    }
    //
    void _wc_map_anim_Animmethod()
    {

        anim = wc_map_anim_.GetComponent<Animator>();
        anim.Play("wc_earth_anim");
    }
    //
    void _kite_anim_Animmethod()
    {

        anim = kite.GetComponent<Animator>();
        anim.Play("kite_anim");
    }
    //
    void _kite2_anim_Animmethod()
    {

        anim = kite.GetComponent<Animator>();
        anim.Play("kite_anim_2");
    }
    //
    void _ba_anim_Animmethod()
    {

        anim = ba_anim.GetComponent<Animator>();
        anim.Play("bottle_arrow_anim");
    }
    //
    void _red_arrow_2_anim_Animmethod()
    {

        anim = red_arrow.GetComponent<Animator>();
        anim.Play("red_2");
    }
    //
    void _red_arrow_3_anim_Animmethod()
    {

        anim = red_arrow.GetComponent<Animator>();
        anim.Play("red_arrow");
    }
    //
    void _wa_anim_Animmethod()
    {

        anim = wa.GetComponent<Animator>();
        anim.Play("wa_anim");
    }
    //
    void _ca_anim_Animmethod()
    {

        anim = ca.GetComponent<Animator>();
        anim.Play("ca_anim");
    }
    //
    void _hba_anim_Animmethod()
    {

        anim = human_blue_arrows_.GetComponent<Animator>();
        anim.Play("human_arrow_anim");
    }
    //
    void _tornado_anim_Animmethod()
    {

        anim = tornado.GetComponent<Animator>();
        anim.Play("tornado");
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
    void _wn_1_audioMethod()
    {
        myAudio.clip = wn_1;
        myAudio.Play();
    }
    //
    void _wn_2_audioMethod()
    {
        myAudio.clip = wn_2;
        myAudio.Play();
    }
    //
    void _wn_3_audioMethod()
    {
        myAudio.clip = wn_3;
        myAudio.Play();
    }
    //
    void _wn_4_audioMethod()
    {
        myAudio.clip = wn_4;
        myAudio.Play();
    }
    //
    void _aep_1_audioMethod()
    {
        myAudio.clip = aep_1;
        myAudio.Play();
    }
    //
    void _aep_2_audioMethod()
    {
        myAudio.clip = aep_2;
        myAudio.Play();
    }
    //
    void _aep_3_audioMethod()
    {
        myAudio.clip = aep_3;
        myAudio.Play();
    }
    //
    void _aep_4_audioMethod()
    {
        myAudio.clip = aep_4;
        myAudio.Play();
    }
    //
    void _aep_5_audioMethod()
    {
        myAudio.clip = aep_5;
        myAudio.Play();
    }
    //
    void _aep_6_audioMethod()
    {
        myAudio.clip = aep_6;

        myAudio.Play();
    }
    //
    void _aep_7_audioMethod()
    {
        myAudio.clip = aep_7;
        myAudio.Play();
    }
    //
    void _aep_8_audioMethod()
    {
        myAudio.clip = aep_8;
        myAudio.Play();
    }
    //
    void _aep_9_audioMethod()
    {
        myAudio.clip = aep_9;
        myAudio.Play();
    }
    //
    void _aep_10_audioMethod()
    {
        myAudio.clip = aep_10;
        myAudio.Play();
    }
    //
    void _wap_1_audioMethod()
    {
        myAudio.clip = wap_1;
        myAudio.Play();
    }
    //
    void _wap_2_audioMethod()
    {
        myAudio.clip = wap_2;
        myAudio.Play();
    }
    //
    void _wap_3_audioMethod()
    {
        myAudio.clip = wap_3;
        myAudio.Play();
    }
    //
    void _wap_4_audioMethod()
    {
        myAudio.clip = wap_4;
        myAudio.Play();
    }
    //
    void _wap_5_audioMethod()
    {
        myAudio.clip = wap_5;
        myAudio.Play();
    }
    //
    void _wc_1_audioMethod()
    {
        myAudio.clip = wc_1;
        myAudio.Play();
    }
    //
    void _wc_2_audioMethod()
    {
        myAudio.clip = wc_2;
        myAudio.Play();
    }
    //
    void _wc_3_audioMethod()
    {
        myAudio.clip = wc_3;
        myAudio.Play();
    }
    //
    void _wc_4_audioMethod()
    {
        myAudio.clip = wc_4;
        myAudio.Play();
    }
    //
    void _wc_5_audioMethod()
    {
        myAudio.clip = wc_5;
        myAudio.Play();
    }
    //
    void _wc_6_audioMethod()
    {
        myAudio.clip = wc_6;
        myAudio.Play();
    }
    //
    void _mw_1_audioMethod()
    {
        myAudio.clip = mw_1;
        myAudio.Play();
    }
    //
    void _mw_2_audioMethod()
    {
        myAudio.clip = mw_2;
        myAudio.Play();
    }
    //
    void _mw_3_audioMethod()
    {
        myAudio.clip = mw_3;
        myAudio.Play();
    }
    //
    void _mw_4_audioMethod()
    {
        myAudio.clip = mw_4;
        myAudio.Play();
    }
    //
    void _mw_5_audioMethod()
    {
        myAudio.clip = mw_5;
        myAudio.Play();
    }
    //
    void _ts_1_audioMethod()
    {
        myAudio.clip = ts_1;
        myAudio.Play();
    }
    //
    void _ts_2_audioMethod()
    {
        myAudio.clip = ts_2;
        myAudio.Play();
    }
    //
    void _ts_3_audioMethod()
    {
        myAudio.clip = ts_3;
        myAudio.Play();
    }
    //
    void _ts_4_audioMethod()
    {
        myAudio.clip = ts_4;
        myAudio.Play();
    }
    //
    void _ts_5_audioMethod()
    {
        myAudio.clip = ts_5;
        myAudio.Play();
    }
    //
    void _cyc_1_audioMethod()
    {
        myAudio.clip = cyc_1;
        myAudio.Play();
    }
    //
    void _cyc_2_audioMethod()
    {
        myAudio.clip = cyc_2;
        myAudio.Play();
    }
    //
    void _cyc_3_audioMethod()
    {
        myAudio.clip = cyc_3;
        myAudio.Play();
    }
    //
    void _cyc_4_audioMethod()
    {
        myAudio.clip = cyc_4;
        myAudio.Play();
    }
    //
    void _cyc_5_audioMethod()
    {
        myAudio.clip = cyc_5;
        myAudio.Play();
    }
    //
    void _cyc_6_audioMethod()
    {
        myAudio.clip = cyc_6;
        myAudio.Play();
    }
    //
    void _cyc_7_audioMethod()
    {
        myAudio.clip = cyc_7;
        myAudio.Play();
    }
    //
    void _cyc_8_audioMethod()
    {
        myAudio.clip = cyc_8;
        myAudio.Play();
    }
    //
    void _cyc_9_audioMethod()
    {
        myAudio.clip = cyc_9;
        myAudio.Play();
    }
    //
    void _cyc_10_audioMethod()
    {
        myAudio.clip = cyc_10;
        myAudio.Play();
    }
    //
    void _td_audioMethod()
    {
        myAudio.clip = td;
        myAudio.Play();
    }
    //
    void _esm_1_audioMethod()
    {
        myAudio.clip = esm_1;
        myAudio.Play();
    }
    //
    void _esm_2_audioMethod()
    {
        myAudio.clip = esm_2;
        myAudio.Play();
    }
    //
    void _esm_3_audioMethod()
    {
        myAudio.clip = esm_3;
        myAudio.Play();
    }
    //
    void _esm_4_audioMethod()
    {
        myAudio.clip = esm_4;
        myAudio.Play();
    }
    //
    void _esm_5_audioMethod()
    {
        myAudio.clip = esm_5;
        myAudio.Play();
    }
    //
    void _esm_6_audioMethod()
    {
        myAudio.clip = esm_6;
        myAudio.Play();
    }
    //
    void _esm_7_audioMethod()
    {
        myAudio.clip = esm_7;
        myAudio.Play();
    }
    //
    void _esm_8_audioMethod()
    {
        myAudio.clip = esm_8;
        myAudio.Play();
    }
    //



















}
