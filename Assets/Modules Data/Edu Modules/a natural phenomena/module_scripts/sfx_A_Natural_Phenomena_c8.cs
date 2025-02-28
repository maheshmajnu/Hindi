using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_A_Natural_Phenomena_c8 : MonoBehaviour
{
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;

   
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypointCanvas.SetActive(true);
        waypoint.player = InventoryManager.Instance.player.transform;
        waypoint.target = target;
    }


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
    }

    public Camera cam;
    public LayerMask layerMask;
    private bool canChoose = true;
    public TargetController level1MiniGame;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canChoose)
        {
            Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
            {
                if (raycastHit.collider != null)
                {
                    Debug.Log(raycastHit.collider.gameObject.name);
                    if (raycastHit.collider.gameObject.name == "Correct")
                    {
                        level1MiniGame.defaultEvent?.Invoke();
                        StepComplete();
                        level1MiniGame.EndMiniGame();
                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }
    }

    public void CanChooseTrue()
    {
        canChoose = true;
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    public void MiniGameStart()
    {
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

        canChoose = false;
    }

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void PlayerPosChange(Transform pos)
    {
        StartCoroutine(DelayPlayerPosChange(pos));
    }

    IEnumerator DelayPlayerPosChange(Transform pos)
    {
       yield return new WaitForSeconds(1f);
        InventoryManager.Instance.player.ChangePosition(pos);
    }

    public void PlayerPosChangeLv3(Transform pos)
    {
        InventoryManager.Instance.player.ChangePosition(pos);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    public GameObject title_1;
    public GameObject title_2;
    public GameObject title_3;
    public GameObject title_4;
    public GameObject title_5;
    public GameObject title_6;
    public GameObject title_7;
    public GameObject title_8;
    public GameObject title_9;
    public GameObject title_10;
    public GameObject title_11;
    public GameObject title_12;
    public GameObject des_1;
    public GameObject des_2;
    public GameObject des_3;
    public GameObject des_4;
    public GameObject des_5;

    public GameObject lightening_conductor;
    public GameObject building_fence;
    public GameObject cone_of_protection;
    public GameObject ebonite_rod_rubbed;
    public GameObject glass_rod_rubbed;


    //praticles on-off

    public GameObject sparks;
    public GameObject spark;
    public GameObject lightening;
    public GameObject thunder;
    public GameObject earthquake;
    public GameObject rain;
    public GameObject meteor;
    public GameObject tornado;
    public GameObject lightening_4;
    public GameObject wind;

    // Exp - animation

    private Animator anim;

    [Header("Explanation anims")]

    public GameObject spacemeteoranim; 
    public GameObject skymeteoranim;
    public GameObject plug;
    public GameObject combanim1;
    public GameObject combanim2;
    public GameObject rubbinganim;
    public GameObject attractanim;
    public GameObject repelanim;
    public GameObject electroscopeanim;



    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip introduction;
    public AudioClip Lightning;
    public AudioClip ChargingbyRubbing;
    public AudioClip TypesofChargesandTheirInteraction;
    public AudioClip TheElectroscope;
    public AudioClip UsesofanElectroscope;
    public AudioClip Lightningstrike;
    public AudioClip LightningConductor;
    public AudioClip Earthing;
    public AudioClip Coneofprotection;
    public AudioClip Earthquakes;
    public AudioClip Precautionstotakeincaseofanearthquake;






    // Start is called before the first frame updat

    //
    void title_1_MethodON()
    {
        title_1.SetActive(true);
    }
    //
    void title_2_MethodON()
    {
        title_2.SetActive(true);
    }
    //
    void title_3_MethodON()
    {
        title_3.SetActive(true);
    }
    //
    void title_4_MethodON()
    {
        title_4.SetActive(true);
    }
    //
    void title_5_MethodON()
    {
        title_5.SetActive(true);
    }
    //
    void title_6_MethodON()
    {
        title_6.SetActive(true);
    }
    //
    void title_7_MethodON()
    {
        title_7.SetActive(true);
    }
    //
    void title_8_MethodON()
    {
        title_8.SetActive(true);
    }
    //
    //
    void title_9_MethodON()
    {
        title_9.SetActive(true);
    }
    //
    void title_10_MethodON()
    {
        title_10.SetActive(true);
    }
    //
    void title_11_MethodON()
    {
        title_11.SetActive(true);
    }
    //
    void title_12_MethodON()
    {
        title_12.SetActive(true);
    }
    //
    void des_1_MethodON()
    {
        des_1.SetActive(true);
    }
    //
    void des_2_MethodON()
    {
        des_2.SetActive(true);
    }
    //
    void des_3_MethodON()
    {
        des_3.SetActive(true);
    }
    //
    void des_4_MethodON()
    {
        des_4.SetActive(true);
    }
    //
    void des_5_MethodON()
    {
        des_5.SetActive(true);
    }
    //






    // Jump to point buttons

    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camera_anim", 0, targetNormalizedTime);
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
















    //
    void lighteningconductor_MethodON()
    {
        lightening_conductor.SetActive(true);
        
    }
    //
    void llighteningconductor_MethodOFF()
    {
        lightening_conductor.SetActive(false);
        
    }
    //
    void building_fence_MethodON()
    {
        building_fence.SetActive(true);
       
    }
    //
    void building_fence_MethodOFF()
    {
        building_fence.SetActive(false);
        
    }
    //
    void cone_of_protection_MethodON()
    {
        cone_of_protection.SetActive(true);

    }
    //
    void cone_of_protection_MethodOFF()
    {
        cone_of_protection.SetActive(false);

    }
    //



    //praticles on-off
    //
    void lighteningandthunder_MethodON()
    {
        lightening.SetActive(true);
        thunder.SetActive(true);
    }
    //
    void lighteningandthunder_MethodOFF()
    {
        lightening.SetActive(false);
        thunder.SetActive(false);
    }
    //
    void lighteningandthunder2_MethodON()
    {
        lightening.SetActive(true);
        thunder.SetActive(true);
    }
    //
    void lighteningandthunder2_MethodOFF()
    {
        lightening.SetActive(false);
        thunder.SetActive(false);
    }
    //
    void lighteningandthunder3_MethodON()
    {
        lightening.SetActive(true);
        thunder.SetActive(true);
    }
    //
    void lighteningandthunder3_MethodOFF()
    {
        lightening.SetActive(false);
        thunder.SetActive(false);
    }
    //
    void lighteningandthunder4_MethodON()
    {
        lightening_4.SetActive(true);
        
    }
    //
    void lighteningandthunder4_MethodOFF()
    {
        lightening_4.SetActive(false);
        
    }
    //
    void sparks_MethodON()
    {
        sparks.SetActive(true);
    }
    //
    void wiresparks_MethodON()
    {
        spark.SetActive(true);
    }
    void wiresparks_MethodOFF()
    {
        spark.SetActive(false);
    }
    //
    void rain_MethodON()
    {
        rain.SetActive(true);
    }
    void rain_MethodOFF()
    {
        rain.SetActive(false);
    }
    //
    void rain2_MethodON()
    {
        rain.SetActive(true);
    }
    void rain2_MethodOFF()
    {
        rain.SetActive(false);
    }
    //
    void meteor_MethodON()
    {
        meteor.SetActive(true);

    }
    void meteor_MethodOFF()
    {
        meteor.SetActive(false);

    }
    //
    void tornado_MethodON()
    {
        tornado.SetActive(true);
    }
    void tornado_MethodOFF()
    {
        tornado.SetActive(false);
    }
    //
    void tornado2_MethodON()
    {
        tornado.SetActive(true);
    }
    void tornado2_MethodOFF()
    {
        tornado.SetActive(false);
    }
    //
    void spark_MethodON()
    {
        spark.SetActive(true);
    }
    void spark_MethodOFF()
    {
        spark.SetActive(false);
    }
    //
    void wind1_MethodON()
    {
        wind.SetActive(true);
    }
    void wind1_MethodOFF()
    {
        wind.SetActive(false);
    }
    //
    void wind2_MethodON()
    {
        wind.SetActive(true);
    }
    void wind2_MethodOFF()
    {
        wind.SetActive(false);
    }
    //

    //animations

    //
    void spacemeteoranim_Animmethod()
    {

        anim = spacemeteoranim.GetComponent<Animator>();
        anim.Play("skymeteoranim");
    }
    //
    void skymeteoranim_Animmethod()
    {

        anim = skymeteoranim.GetComponent<Animator>();
        anim.Play("skymeteoranim");
    }
    //
    void torndo_Animmethod()
    {

        anim = tornado.GetComponent<Animator>();
        anim.Play("tornadoanim");
    }
    //
    void plug_Animmethod()
    {

        anim = plug.GetComponent<Animator>();
        anim.Play("plug anim");
    }
    //
    void combanim1_Animmethod()
    {

        anim = combanim1.GetComponent<Animator>();
        anim.Play("comb anim 1"); 
    }
    //
    void rubbinganim_Animmethod()
    {

        anim = rubbinganim.GetComponent<Animator>();
        anim.Play("rubbing_anim");
    }
    //
    void combanim2_Animmethod()
    {

        anim = combanim2.GetComponent<Animator>();
        anim.Play("comb anim 2");
    }
    //
    void attractanim_Animmethod()
    {

        anim = attractanim.GetComponent<Animator>();
        anim.Play("attract anim");
    }
    //
    void attractanim2_Animmethod()
    {

        anim = attractanim.GetComponent<Animator>();
        anim.Play("attract anim", -1, 0);
    }
    //
    void attractanim3_Animmethod()
    {

        anim = attractanim.GetComponent<Animator>();
        anim.Play("attract anim", -1, 0);
    }
    //
    void repelanim_Animmethod()
    {

        anim = repelanim.GetComponent<Animator>();
        anim.Play("repel anim");
    }
    //
    void repelanim2_Animmethod()
    {

        anim = repelanim.GetComponent<Animator>();
        anim.Play("repel anim", -1, 0);
    }
    //
    void repelanim3_Animmethod()
    {

        anim = repelanim.GetComponent<Animator>();
        anim.Play("repel anim", -1, 0);
    }
    //
    void repelanim4_Animmethod()
    {

        anim = repelanim.GetComponent<Animator>();
        anim.Play("repel anim", -1, 0);
    }
    //
    void electroscopeanim_Animmethod()
    {

        anim = electroscopeanim.GetComponent<Animator>();
        anim.Play("electroscope anim");
    }
    //
    void earthquakeanim_Animmethod()
    {

        anim = earthquake.GetComponent<Animator>();
        anim.Play("earthquakeanim");
    }
    //
    void earthquakeanim2_Animmethod()
    {

        anim = earthquake.GetComponent<Animator>();
        anim.Play("earthquakeanim", -1, 0);
    }
    //
    void earthquakeanim3_Animmethod()
    {

        anim = earthquake.GetComponent<Animator>();
        anim.Play("earthquakeanim", -1, 0);
    }
    //
    void rodsrubbing_Animmethod()
    {

        anim = ebonite_rod_rubbed.GetComponent<Animator>();
        anim.Play("ebonite_rod_rubbed");

        anim = glass_rod_rubbed.GetComponent<Animator>();
        anim.Play("glass rod rubbed");
    }
    //
   




    //audios




    //
    void introduction_audioMethod()

    {
        myAudio.clip = introduction;
        myAudio.Play();
    }
    //
    void Lightning_audioMethod()

    {
        myAudio.clip = Lightning;
        myAudio.Play();
    }
    //
    void ChargingbyRubbing_audioMethod()

    {
        myAudio.clip = ChargingbyRubbing;
        myAudio.Play();
    }
    //
    void TypesofChargesandTheirInteraction_audioMethod()

    {
        myAudio.clip = TypesofChargesandTheirInteraction;
        myAudio.Play();
    }
    //
    void TheElectroscope_audioMethod()

    {
        myAudio.clip = TheElectroscope;
        myAudio.Play();
    }
    //
    void UsesofanElectroscope_audioMethod()

    {
        myAudio.clip = UsesofanElectroscope;
        myAudio.Play();
    }
    //
    void Lightningstrike_audioMethod()

    {
        myAudio.clip = Lightningstrike ;
        myAudio.Play();
    }
    //
    void LightningConductor_audioMethod()

    {
        myAudio.clip = LightningConductor;
        myAudio.Play();
    }
    //
    void Earthing_audioMethod()

    {
        myAudio.clip = Earthing;
        myAudio.Play();
    }
    //
    void Coneofprotection_audioMethod()

    {
        myAudio.clip = Coneofprotection;
        myAudio.Play();
    }
    //
    void Earthquakes_audioMethod()

    {
        myAudio.clip = Earthquakes;
        myAudio.Play();
    }
    //
    void Precautionstotakeincaseofanearthquake_audioMethod()

    {
        myAudio.clip = Precautionstotakeincaseofanearthquake;
        myAudio.Play();
    }
    //


}
