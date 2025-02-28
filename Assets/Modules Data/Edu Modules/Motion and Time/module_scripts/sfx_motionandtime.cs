using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_motionandtime : MonoBehaviour
{
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public TargetController miniGame1;

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

    public void TurnOnGOWithDelay(GameObject obj)
    {
        StartCoroutine(ObjectTurnOnDelay(obj));
    }

    IEnumerator ObjectTurnOnDelay(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ParentCamera(Transform holder)
    {
        this.transform.SetParent(holder);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
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

    public void StepCompleted()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public Transform camHolder2Lv5;
    public void PlayAnimation(Animator anim)
    {
        anim.SetTrigger("Trigger");
        if (StaticVariables.gamemode == 1) //1=PC
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (StaticVariables.gamemode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        MiniGameStart();
        StartCoroutine(DelayAnimEnd());
    }

    IEnumerator DelayAnimEnd()
    {
        yield return new WaitForSeconds(5);
        MiniGameEnd();
        StepCompleted();
    }

    public void PlayAnimationLoose(Animator anim)
    {
        anim.SetTrigger("Trigger");
        MiniGameStart();
        StartCoroutine(DelayAnimEndLoose());
    }

    IEnumerator DelayAnimEndLoose()
    {
        yield return new WaitForSeconds(5);
        MissionFailed();
    }

    public void PlayerPosChangeDelay(Transform pos)
    {
        StartCoroutine(ChangePlayerPos(pos));
    }

    IEnumerator ChangePlayerPos(Transform pos)
    {
        yield return new WaitForSeconds(5);
        InventoryManager.Instance.player.ChangePosition(pos);
    }

    public Camera cam;
    public LayerMask layerMask;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
            {
                if (raycastHit.collider != null)
                {
                    if (raycastHit.collider.gameObject.name == "Correct")
                    {
                        StepCompleted();
                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject Taxi;
    public GameObject cycle;
    public GameObject Train;
    public GameObject Tracks;
    public GameObject Go;
    public GameObject Redcar_end;
    public GameObject Yellowcar_end;
    public GameObject Kidrunning;
    public GameObject Run_track;
    public GameObject carinterior_setup;
    public GameObject speedometer;
    public GameObject Uni;
    public GameObject earth;
    public GameObject sun;
    public GameObject timeunits;
    public GameObject timeseps;
    public GameObject speedunits;
    public GameObject lastmet;
    public GameObject redracecar;
    public GameObject yellowracecar;
    public GameObject redcarspeed;
    public GameObject yellowcarspeed;
    public GameObject arrows;
    public GameObject kid_final;
    public GameObject runtrackdist;
    public GameObject static_panel;
    public GameObject nonstatic_panel;
    public GameObject fastandslowcanvas;
    public GameObject fastnslowobj;
    public GameObject distance_travelled;
    public GameObject speed_panel;
    public GameObject distance_covered;
    public GameObject measuringspeed_panel;
    public GameObject speedo_n_odo;
    public GameObject speedometer_txt;
    public GameObject odometer_txt;
    public GameObject measuretime_panel;
    public GameObject sunrise_txt;
    public GameObject moon_txt;
    public GameObject sun_earth;
    public GameObject second;
    public GameObject s;
    public GameObject min_hrs;
    public GameObject one_min;
    public GameObject one_hr;
    public GameObject mps;
    public GameObject mkm;
    public GameObject bus;









    //Title//




    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject T6;
    public GameObject T7;


























    // Exp - Animations

    private Animator anim;
    [Header("Explanation anims")]
    public GameObject Train_anim;
    public GameObject Taxi_anim;
    public GameObject Go_anim;
    public GameObject YellowcarA;
    public GameObject RedcarA;
    public GameObject RedcarAendanim;
    public GameObject YellowcarAendanim;
    public GameObject Kidparentrunning_anim;
    public GameObject needle_animation;
    public GameObject dail_aimation;
    public GameObject direction_light;
    public GameObject Earth_rev;
    public GameObject sun_rising;
    public GameObject redspeedanim;
    public GameObject yellowspeedanim;
    public GameObject kid_final_anim;













    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip Allobjects;
    public AudioClip staticobjects;
    public AudioClip movingobjects;
    public AudioClip doctorappointment;
    public AudioClip hospitaldistance;
    public AudioClip cycleortaxi;
    public AudioClip choosingtaxi;
    public AudioClip taxispeednslow;
    public AudioClip fastnslowintro;
    public AudioClip race_track;
    public AudioClip race_begins;
    public AudioClip race_end;
    public AudioClip fastnslow_definition;
    public AudioClip speed_intro;
    public AudioClip faster_object;
    public AudioClip speed_definition;
    public AudioClip Two_cars;
    public AudioClip speedOf_cars;
    public AudioClip speed_conclusion;
    public AudioClip measuringspeed_definition;
    public AudioClip applying_formula;
    public AudioClip activity;
    public AudioClip startnendpoints;
    public AudioClip measuring_points;
    public AudioClip friend_running;
    public AudioClip noting_distance;
    public AudioClip dividing_time;
    public AudioClip speedometer_intro;
    public AudioClip two_meters;
    public AudioClip speedometer_definition;
    public AudioClip speedometer_motion;
    public AudioClip odometer_definition;
    public AudioClip odometer_motion;
    public AudioClip elder_shadow;
    public AudioClip time_intervals;
    public AudioClip sunrise;
    public AudioClip moon;
    public AudioClip earth_revo;
    public AudioClip watches;
    public AudioClip unit_of_time;
    public AudioClip min_hr;
    public AudioClip seconds;
    public AudioClip m_per_sec;
    public AudioClip lastone;











    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("Cam animation", 0, targetNormalizedTime);
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















    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }


    //Audio

    void _1All_objectsaudioMethod()
    {
        myAudio.clip = Allobjects;
        myAudio.Play();
    }

    //

    void _2Static_objectsaudioMethod()
    {
        myAudio.clip = staticobjects;
        myAudio.Play();
    }

    //

    void _3Moving_objectsaudioMethod()
    {
        myAudio.clip = movingobjects;
        myAudio.Play();
    }

    //

    void _4Doctor_appointmentaudioMethod()
    {
        myAudio.clip = doctorappointment;
        myAudio.Play();
    }

    //

    void _5Hospital_distanceaudioMethod()
    {
        myAudio.clip = hospitaldistance;
        myAudio.Play();
    }

    //

    void _6Cycle_or_TaxiaudioMethod()
    {
        myAudio.clip = cycleortaxi;
        myAudio.Play();
    }

    //

    void _7ChoosingTaxiaudioMethod()
    {
        myAudio.clip = choosingtaxi;
        myAudio.Play();
    }

    //

    void _8Taxi_sppednSlowaudioMethod()
    {
        myAudio.clip = taxispeednslow;
        myAudio.Play();
    }

    //

    void _9Fastnslow_introaudioMethod()
    {
        myAudio.clip = fastnslowintro;
        myAudio.Play();
    }

    //

    void _10Race_trackaudioMethod()
    {
        myAudio.clip = race_track;
        myAudio.Play();
    }

    //

    void _11Race_beginsaudioMethod()
    {
        myAudio.clip = race_begins;
        myAudio.Play();
    }

    //

    void _12Race_endaudioMethod()
    {
        myAudio.clip = race_end;
        myAudio.Play();
    }

    //

    void _13Fastnslow_end_endaudioMethod()
    {
        myAudio.clip = fastnslow_definition;
        myAudio.Play();
    }

    //

    void _14Speed_introaudioMethod()
    {
        myAudio.clip = speed_intro;
        myAudio.Play();
    }

    //

    void _15Faster_objectaudioMethod()
    {
        myAudio.clip = faster_object;
        myAudio.Play();
    }

    //

    void _16Speed_definitionaudioMethod()
    {
        myAudio.clip = speed_definition;
        myAudio.Play();
    }

    //

    void _17Two_carsaudioMethod()
    {
        myAudio.clip = Two_cars;
        myAudio.Play();
    }

    //

    void _18Speed_of_carsaudioMethod()
    {
        myAudio.clip = speedOf_cars;
        myAudio.Play();
    }

    //

    void _19Speed_conclusionaudioMethod()
    {
        myAudio.clip = speed_conclusion;
        myAudio.Play();
    }

    //

    void _20Measuring_speed_definitionaudioMethod()
    {
        myAudio.clip = measuringspeed_definition;
        myAudio.Play();
    }

    //

    void _21Applying_formulaaudioMethod()
    {
        myAudio.clip = applying_formula;
        myAudio.Play();
    }

    //

    void _22ActivityaudioMethod()
    {
        myAudio.clip = activity;
        myAudio.Play();
    }

    //

    void _23Startandend_pointsaudioMethod()
    {
        myAudio.clip = startnendpoints;
        myAudio.Play();
    }

    //

    void _24MeasuringpointsaudioMethod()
    {
        myAudio.clip = measuring_points;
        myAudio.Play();
    }

    //

    void _25Friend_runningaudioMethod()
    {
        myAudio.clip = friend_running;
        myAudio.Play();
    }

    //

    void _26Noting_distanceaudioMethod()
    {
        myAudio.clip = noting_distance;
        myAudio.Play();
    }

    //

    void _27Dividing_timeaudioMethod()
    {
        myAudio.clip = dividing_time;
        myAudio.Play();
    }

    //

    void _28Speedometer_introaudioMethod()
    {
        myAudio.clip = speedometer_intro;
        myAudio.Play();
    }

    //

    void _29Two_metersaudioMethod()
    {
        myAudio.clip = two_meters;
        myAudio.Play();
    }

    //

    void _30Speedometer_definitionaudioMethod()
    {
        myAudio.clip = speedometer_definition;
        myAudio.Play();
    }

    //

    void _31Speedometer_motionaudioMethod()
    {
        myAudio.clip = speedometer_motion;
        myAudio.Play();
    }

    //

    void _32Odometer_definitionaudioMethod()
    {
        myAudio.clip = odometer_definition;
        myAudio.Play();
    }

    //

    void _33Odometer_motionaudioMethod()
    {
        myAudio.clip = odometer_motion;
        myAudio.Play();
    }
    
    //

    void _34Elders_shasowsaudioMethod()
    {
        myAudio.clip = elder_shadow;
        myAudio.Play();
    }

    //

    void _35Time_intervalsaudioMethod()
    {
        myAudio.clip = time_intervals;
        myAudio.Play();
    }

    //

    void _36SunrideaudioMethod()
    {
        myAudio.clip = sunrise;
        myAudio.Play();
    }

    //

    void _37MoonaudioMethod()
    {
        myAudio.clip = moon;
        myAudio.Play();
    }

    //

    void _38Earth_revaudioMethod()
    {
        myAudio.clip = earth_revo;
        myAudio.Play();
    }

    //

    void _39WatchesaudioMethod()
    {
        myAudio.clip = watches;
        myAudio.Play();
    }

    //

    void _40Time_unitsaudioMethod()
    {
        myAudio.clip = unit_of_time;
        myAudio.Play();
    }

    //

    void _41Min_hrMethod()
    {
        myAudio.clip = min_hr;
        myAudio.Play();
    }

    //

    void _42SecondsaudioMethod()
    {
        myAudio.clip = seconds;
        myAudio.Play();
    }

    //

    void _43MperSaudioMethod()
    {
        myAudio.clip = m_per_sec;
        myAudio.Play();
    }

    //

    void _44LastaudioMethod()
    {
        myAudio.clip = lastone;
        myAudio.Play();
    }










    //Title//





    void _T1MethodON()
    {
        T1.SetActive(true);
    }

    void _T1MethodOFF()
    {
        T1.SetActive(false);
    }






    void _T2MethodON()
    {
        T2.SetActive(true);
    }

    void _T2MethodOFF()
    {
        T2.SetActive(false);
    }





    void _T3MethodON()
    {
        T3.SetActive(true);
    }

    void _T3MethodOFF()
    {
        T3.SetActive(false);
    }





    void _T4MethodON()
    {
        T4.SetActive(true);
    }

    void _T4MethodOFF()
    {
        T4.SetActive(false);
    }





    void _T5MethodON()
    {
        T5.SetActive(true);
    }

    void _T5MethodOFF()
    {
        T5.SetActive(false);
    }





    void _T6MethodON()
    {
        T6.SetActive(true);
    }

    void _T6MethodOFF()
    {
        T6.SetActive(false);
    }





    void _T7MethodON()
    {
        T7.SetActive(true);
    }

    void _T7MethodOFF()
    {
        T7.SetActive(false);
    }




























    void _BusMethodON()
    {
        bus.SetActive(true);
    }

    void _BusMethodOFF()
    {
        bus.SetActive(false);
    }
    //

    void _Mkm_txtMethodON()
    {
        mkm.SetActive(true);
    }

    void _Mkm_txtMethodOFF()
    {
        mkm.SetActive(false);
    }
    //

    void _Mps_txtMethodON()
    {
        mps.SetActive(true);
    }

    void _Mps_txtMethodOFF()
    {
        mps.SetActive(false);
    }
    //

    void _One_hr_txtMethodON()
    {
        one_hr.SetActive(true);
    }

    void _One_hr_txtMethodOFF()
    {
        one_hr.SetActive(false);
    }
    //

    void _One_min_txtMethodON()
    {
        one_min.SetActive(true);
    }

    void _One_min_txtMethodOFF()
    {
        one_min.SetActive(false);
    }
    //

    void _Min_hr_txtMethodON()
    {
        min_hrs.SetActive(true);
    }

    void _Min_hr_txtMethodOFF()
    {
        min_hrs.SetActive(false);
    }
    //

    void _S_txtMethodON()
    {
        s.SetActive(true);
    }

    void _S_txtMethodOFF()
    {
        s.SetActive(false);
    }
    //

    void _Second_txtMethodON()
    {
        second.SetActive(true);
    }

    void _Second_txtMethodOFF()
    {
        second.SetActive(false);
    }
    //

    void _Sun_earth_txtMethodON()
    {
        sun_earth.SetActive(true);
    }

    void _Sun_earth_txtMethodOFF()
    {
        sun_earth.SetActive(false);
    }
    //

    void _Moon_txtMethodON()
    {
        moon_txt.SetActive(true);
    }

    void _Moon_txtMethodOFF()
    {
        moon_txt.SetActive(false);
    }
    //

    void _Sunrise_txtMethodON()
    {
        sunrise_txt.SetActive(true);
    }

    void _Sunrise_txtMethodOFF()
    {
        sunrise_txt.SetActive(false);
    }
    //

    void _Measuretime_panellMethodON()
    {
        measuretime_panel.SetActive(true);
    }

    void _Measuretime_panelMethodOFF()
    {
        measuretime_panel.SetActive(false);
    }
    //

    void _Speedometer_txtlMethodON()
    {
        speedometer_txt.SetActive(true);
    }

    void _Speedometer_txtMethodOFF()
    {
        speedometer_txt.SetActive(false);
    }
    //

    void _Odometer_txtMethodON()
    {
        odometer_txt.SetActive(true);
    }

    void _Odometer_txtMethodOFF()
    {
        odometer_txt.SetActive(false);
    }
    //

    void _Speedonodo_panelMethodON()
    {
        speedo_n_odo.SetActive(true);
    }

    void _Speedonodo_panelMethodOFF()
    {
        speedo_n_odo.SetActive(false);
    }
    //

    void _Measuringspeed_panelMethodON()
    {
        measuringspeed_panel.SetActive(true);
    }

    void _Measuringspeed_panelMethodOFF()
    {
        measuringspeed_panel.SetActive(false);
    }
    //

    void _Distance_coveredMethodON()
    {
        distance_covered.SetActive(true);
    }

    void _Distance_coveredMethodOFF()
    {
        distance_covered.SetActive(false);
    }
    //

    void _Speed_panelMethodON()
    {
        speed_panel.SetActive(true);
    }

    void _Speed_panelMethodOFF()
    {
        speed_panel.SetActive(false);
    }
    //

    void _Distance_travelledMethodON()
    {
        distance_travelled.SetActive(true);
    }

    void _Distance_TravelledMethodOFF()
    {
        distance_travelled.SetActive(false);
    }
    //

    void _FastnslowobjMethodON()
    {
        fastnslowobj.SetActive(true);
    }

    void _FastnslowobjMethodOFF()
    {
        fastnslowobj.SetActive(false);
    }
    //

    void _FastnslowcanvasMethodON()
    {
        fastandslowcanvas.SetActive(true);
    }

    void _FastnslowcanvasMethodOFF()
    {
        fastandslowcanvas.SetActive(false);
    }
    //

    void _NonStaticaPanelMethodON()
    {
        nonstatic_panel.SetActive(true);
    }

    void _NonStaticPanelMethodOFF()
    {
        nonstatic_panel.SetActive(false);
    }
    //

    void _StaticaPanelMethodON()
    {
        static_panel.SetActive(true);
    }

    void _StaticPanelMethodOFF()
    {
        static_panel.SetActive(false);
    }
    //

    void _Runtrack_distanceMethodON()
    {
        runtrackdist.SetActive(true);
    }

    void _Runtrack_distanceMethodOFF()
    {
        runtrackdist.SetActive(false);
    }
    //

    void _Kid_FinalMethodON()
    {
        kid_final.SetActive(true);
    }

    void _Kid_finalMethodOFF()
    {
        kid_final.SetActive(false);
    }
    //

    void _ArrowMethodON()
    {
        arrows.SetActive(true);
    }

    void _ArrowMethodOFF()
    {
        arrows.SetActive(false);
    }
    //

    void _RedracecarSpeedMethodON()
    {
        redcarspeed.SetActive(true);
    }

    void _RedracecarSpeedMethodOFF()
    {
        redcarspeed.SetActive(false);
    }
    //

    void _YellowracecarSpeedMethodON()
    {
        yellowcarspeed.SetActive(true);
    }

    void _YellowraceSpeedcarMethodOFF()
    {
        yellowcarspeed.SetActive(false);
    }
    //

    void _RedracecarMethodON()
    {
        redracecar.SetActive(true);
    }

    void _RedracecarMethodOFF()
    {
        redracecar.SetActive(false);
    }
    //


    void _YellowracecarMethodON()
    {
        yellowracecar.SetActive(true);
    }

    void _YellowracecarMethodOFF()
    {
        yellowracecar.SetActive(false);
    }
    //

    void _TaxiMethodON()
    {
        Taxi.SetActive(true);
    }

    void _TaxiMethodOFF()
    {
        Taxi.SetActive(false);
    }
    //

    void _CycleMethodON()
    {
        cycle.SetActive(true);
    }

    void _CycleMethodOFF()
    {
        cycle.SetActive(false);
    }
    //

    void _TrainMethodON()
    {
        Train.SetActive(true);
    }

    void _TrainMethodOFF()
    {
        Train.SetActive(false);
    }
    //

    void _TracksMethodON()
    {
        Tracks.SetActive(true);
    }

    void _TracksMethodOFF()
    {
        Tracks.SetActive(false);
    }
    //

    void _GoMethodON()
    {
        Go.SetActive(true);
    }

    void _GoMethodOFF()
    {
        Go.SetActive(false);
    }
    //

    void _RedcarendMethodON()
    {
        Redcar_end.SetActive(true);
    }

    void _RedcarendMethodOFF()
    {
        Redcar_end.SetActive(false);
    }
    //

    void _YellowcarendMethodON()
    {
        Yellowcar_end.SetActive(true);
    }

    void _YellowcarendMethodOFF()
    {
        Yellowcar_end.SetActive(false);
    }
    //

    

    void _Run_trackMethodON()
    {
        Run_track.SetActive(true);
    }

    void _Run_trackMethodOFF()
    {
        Run_track.SetActive(false);
    }
    //

    void _Car_interiorMethodON()
    {
        carinterior_setup.SetActive(true);
    }

    void _Car_interiorMethodOFF()
    {
        carinterior_setup.SetActive(false);
    }
    //

    void _SpeedometerMethodON()
    {
        speedometer.SetActive(true);
    }

    void _SpeedometerMethodOFF()
    {
        speedometer.SetActive(false);
    }
    //

    void _UniMethodON()
    {
        Uni.SetActive(true);
    }

    void _UniMethodOFF()
    {
        Uni.SetActive(false);
    }
    //

    void _EarthMethodON()
    {
        earth.SetActive(true);
    }

    void _EarthMethodOFF()
    {
        earth.SetActive(false);
    }
    //

    void _SunMethodON()
    {
        sun.SetActive(true);
    }

    void _SunMethodOFF()
    {
        sun.SetActive(false);
    }
    //

    void _TimeunitsMethodON()
    {
        timeunits.SetActive(true);
    }

    void _TimeunitsMethodOFF()
    {
        timeunits.SetActive(false);
    }
    //

    void _TimesepsMethodON()
    {
        timeseps.SetActive(true);
    }

    void _TimesepsMethodOFF()
    {
        timeseps.SetActive(false);
    }
    //

    void _SpeedunitsMethodON()
    {
        speedunits.SetActive(true);
    }

    void _SpeedunitsMethodOFF()
    {
        speedunits.SetActive(false);
    }
    //

    void _LastMethodON()
    {
        lastmet.SetActive(true);
    }

    void _LastMethodOFF()
    {
        lastmet.SetActive(false);
    }
    //
































    // Animations

    void _TrainanimationAnimmethod()
    {

        anim = Train_anim.GetComponent<Animator>();
        anim.Play("Train animation");
    }

    //

    void _TaxianimationAnimmethod()
    {

        anim = Taxi_anim.GetComponent<Animator>();
        anim.Play("Taxi animation");
    }

    //

    void _Go_animationAnimmethod()
    {

        anim = Go_anim.GetComponent<Animator>();
        anim.Play("Go animation");
    }

    //

    void _Yellowcar_A_animationAnimmethod()
    {

        anim = YellowcarA.GetComponent<Animator>();
        anim.Play("Yellow car 1st anim");
    }

    //

    void _Redcar_A_animationAnimmethod()
    {

        anim = RedcarA.GetComponent<Animator>();
        anim.Play("Red car 1st anim");
    }

    //

    void _Redcar_A_end_animationAnimmethod()
    {

        anim = RedcarAendanim.GetComponent<Animator>();
        anim.Play("Red car race complete 1");
    }

    //

    void _Yellowcar_A_end_animationAnimmethod()
    {

        anim = YellowcarAendanim.GetComponent<Animator>();
        anim.Play("Yellow car race complete 1");
    }

    //

    void _Kid_parent_running_animationAnimmethod()
    {

        anim = Kidparentrunning_anim.GetComponent<Animator>();
        anim.Play("Kid group running");
    }

    //

    void _Needle_animationAnimmethod()
    {

        anim = needle_animation.GetComponent<Animator>();
        anim.Play("Needle animation");
    }

    //

    void _Dail_animationAnimmethod()
    {

        anim = dail_aimation.GetComponent<Animator>();
        anim.Play("Dail animation");
    }

    //

    void _Direction_light_animationAnimmethod()
    {

        anim = direction_light.GetComponent<Animator>();
        anim.Play("Direction light animation");
    }

    void _Earth_rev_animationAnimmethod()
    {

        anim = Earth_rev.GetComponent<Animator>();
        anim.Play("Earth rotation animation");
    }

    void _Sunrise_animationAnimmethod()
    {

        anim = sun_rising.GetComponent<Animator>();
        anim.Play("Sun rise anim");
    }

    void _Redcarspeed_animationAnimmethod()
    {

        anim = redspeedanim.GetComponent<Animator>();
        anim.Play("redcarspeed");
    }

    void _Yellowcarspeed_animationAnimmethod()
    {

        anim = yellowspeedanim.GetComponent<Animator>();
        anim.Play("Yellowcarspeed");
    }

    void _Kid_run_finalanimationAnimmethod()
    {

        anim = kid_final_anim.GetComponent<Animator>();
        anim.Play("Kid running F");
    }






















}
