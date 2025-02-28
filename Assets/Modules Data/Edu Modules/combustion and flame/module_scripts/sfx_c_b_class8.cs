using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class sfx_c_b_class8 : MonoBehaviour
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
    public GameObject title_13;
    public GameObject title_14;
    public GameObject title_15;
    public GameObject des_1;
    public GameObject des_2;
    public GameObject des_3;
    public GameObject des_4;
    public GameObject des_5;
    public GameObject des_6;
    public GameObject des_7;
    public GameObject des_8;
    public GameObject des_9;
    public GameObject des_10;
    public GameObject des_11;
    public GameObject des_12;
    public GameObject des_13;
    public GameObject des_14;
    public GameObject des_15;
    public GameObject des_16;
    public GameObject des_17;
    public GameObject glass;
    public GameObject wood;
    public GameObject fireservice;
    public GameObject fireservice_2;

    public GameObject paperfire;
    public GameObject candlefire1;
    public GameObject explosion;
    public GameObject forest_fire;
    public GameObject stove_fire;
    public GameObject cooker;
    public GameObject petrol_drums;
    public GameObject toc_fire;





    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]

    public GameObject firing;
    public GameObject firing_1;
    public GameObject firing_2;
    public GameObject c_ic_anim;
    public GameObject toc_anim;
    public GameObject mag;
    public GameObject tong_with_scale;
    public GameObject tong_with_copper;
    public GameObject ice;
    public GameObject earth;



    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip in_1;
    public AudioClip in_2;
    public AudioClip comb_1;
    public AudioClip comb_2;
    public AudioClip c_ic_sub_1;
    public AudioClip c_ic_sub_2;
    public AudioClip c_ic_sub_3;
    public AudioClip cfb_1;
    public AudioClip cfb_2;
    public AudioClip i_temp_1;
    public AudioClip i_temp_2;
    public AudioClip i_temp_3;
    public AudioClip inflam_sub_1;
    public AudioClip inflam_sub_2;
    public AudioClip inflam_sub_3;
    public AudioClip inflam_sub_4;
    public AudioClip h_o_1;
    public AudioClip h_o_2;
    public AudioClip h_o_3;
    public AudioClip h_o_4;
    public AudioClip toc_1;
    public AudioClip toc_2;
    public AudioClip toc_3;
    public AudioClip toc_4;
    public AudioClip toc_5;
    public AudioClip toc_6;
    public AudioClip fl_1;
    public AudioClip fl_2;
    public AudioClip fl_3;
    public AudioClip fl_4;
    public AudioClip fl_5;
    public AudioClip fl_6;
    public AudioClip fl_7;
    public AudioClip fl_8;
    public AudioClip fl_9;
    public AudioClip fl_10;
    public AudioClip fl_11;
    public AudioClip fl_12;
    public AudioClip fl_13;
    public AudioClip fl_14;
    public AudioClip fl_15;








    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool collectedKey = false;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) || InventoryManager.Instance.player.InteractIspressed)
        {
            if (!collectedKey)
            {
                StartCoroutine(CheckForItems());
            }
        }
    }
    IEnumerator CheckForItems()
    {
        yield return new WaitForSeconds(1);
        if (InventoryManager.Instance.items.Count != 0)
        {
            foreach (Item itm in InventoryManager.Instance.items)
            {
                if (itm.item.itemName == "Key" && !collectedKey)
                {
                    Debug.Log("KEY");
                    InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                    collectedKey = true;
                    break;
                }
            }
        }
    }

    //
    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }
    //


    //
    void _title_1_MethodON()
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

    //
    void _title_11_MethodON()
    {
        title_11.SetActive(true);
    }
    //
    void title_12_MethodON()
    {
        title_12.SetActive(true);
    }
    //
    void title_13_MethodON()
    {
        title_13.SetActive(true);
    }
    //
    void title_14_MethodON()
    {
        title_14.SetActive(true);
    }
    //
    void title_15_MethodON()
    {
        title_15.SetActive(true);
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
    void des_6_MethodON()
    {
        des_6.SetActive(true);
    }
    //
    void des_7_MethodON()
    {
        des_7.SetActive(true);
    }
    //
    void des_8_MethodON()
    {
        des_8.SetActive(true);
    }
    //
    void des_9_MethodON()
    {
        des_9.SetActive(true);
    }
    //
    void des_10_MethodON()
    {
        des_10.SetActive(true);
    }
    //
    void des_11_MethodON()
    {
        des_11.SetActive(true);
    }
    //
    void des_12_MethodON()
    {
        des_12.SetActive(true);
    }
    //
    void des_13_MethodON()
    {
        des_13.SetActive(true);
    }
    //
    void des_14_MethodON()
    {
        des_14.SetActive(true);
    }
    //
    void des_15_MethodON()
    {
        des_15.SetActive(true);
    }
    //
    void des_16_MethodON()
    {
        des_16.SetActive(true);
    }
    //
    void des_17_MethodON()
    {
        des_17.SetActive(true);
    }
    //


    //
    void glass_MethodON()
    {
        glass.SetActive(true);
    }

    void glass_MethodOFF()
    {
        glass.SetActive(false);
    }
    //
    void wood_MethodON()
    {
        wood.SetActive(true);
    }

    void wood_MethodOFF()
    {
        wood.SetActive(false);
    }
    //
    void fireservice__MethodON()
    {
        fireservice.SetActive(true);

    }

    void fireservice__MethodOFF()
    {
        fireservice.SetActive(false);

    }
    //  
    void fireservice_2_MethodON()
    {
        fireservice_2.SetActive(true);
        
    }

    void fireservice_2_MethodOFF()
    {
        fireservice_2.SetActive(false);
       
    }
    //  
    void cic_MethodON()
    {
        c_ic_anim.SetActive(true);

    }

    void cic_MethodOFF()
    {
        c_ic_anim.SetActive(false);

    }
    //
    void toc_MethodON()
    {
        toc_anim.SetActive(true);

    }

    void toc_MethodOFF()
    {
        toc_anim.SetActive(false);

    }
    //
    void tong_with_scale_MethodON()
    {
        tong_with_scale.SetActive(true);

    }

    void tong_with_scale_MethodOFF()
    {
        tong_with_scale.SetActive(false);

    }
    //
    void tong_with_copper_MethodON()
    {
        tong_with_copper.SetActive(true);

    }

    void tong_withcopper_MethodOFF()
    {
        tong_with_copper.SetActive(false);

    }
    //
    void firing_2_MethodON()
    {
        firing_2.SetActive(true);

    }

    void firing_2_MethodOFF()
    {
        firing_2.SetActive(false);

    }
    //
    
    
    //
    void paperfire1_MethodON()
    {
        paperfire.SetActive(true);

    }

    void paperfire1_MethodOFF()
    {
        paperfire.SetActive(false);

    }
    //
    void paperfire2_MethodON()
    {
        paperfire.SetActive(true);

    }

    void paperfire2_MethodOFF()
    {
        paperfire.SetActive(false);

    }
    //
    void candlefire1_MethodON()
    {
        candlefire1.SetActive(true);

    }

    void candlefire1_MethodOFF()
    {
        candlefire1.SetActive(false);

    }
    //
    void candlefire2_MethodON()
    {
        candlefire1.SetActive(true);

    }

    void candlefire2_MethodOFF()
    {
        candlefire1.SetActive(false);

    }
    //
    void explosion_MethodON()
    {
        explosion.SetActive(true);

    }
    //
    void forest_fire_MethodON()
    {
        forest_fire.SetActive(true);

    }

    void forest_fire_MethodOFF()
    {
        forest_fire.SetActive(false);

    }
    //
    void stove_fire_MethodON()
    {
        stove_fire.SetActive(true);

    }

    void stove_fire_MethodOFF()
    {
        stove_fire.SetActive(false);

    }
    //
    void stove_fire_1_MethodON()
    {
        stove_fire.SetActive(true);

    }

    void stove_fire_1_MethodOFF()
    {
        stove_fire.SetActive(false);

    }
    //
    void cooker_MethodON()
    {
        cooker.SetActive(true);

    }

    void cooker_MethodOFF()
    {
        cooker.SetActive(false);

    }
    //
    void petrol_drums_MethodOFF()
    {
        petrol_drums.SetActive(false);

    }
    //
    //
    void toc_fire_MethodON()
    {
        toc_fire.SetActive(true);

    }

    void toc_fire_MethodOFF()
    {
        toc_fire.SetActive(false);

    }
    //









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
    void _firing_animationAnimmethod()
    {

        anim = firing.GetComponent<Animator>();
        anim.Play("firing_anim");
    }
    //
    void _firing_1_animationAnimmethod()
    {

        anim = firing_1.GetComponent<Animator>();
        anim.Play("firing_anim_2");
    }
    //
    void _firing_r_animationAnimmethod()
    {

        anim = firing.GetComponent<Animator>();
        anim.Play("firing_anim", -1, 0);
    }
    //
    void _firing_1r_animationAnimmethod()
    {

        anim = firing_1.GetComponent<Animator>();
        anim.Play("firing_anim_2", -1, 0 );
    }
    //
    void _firing_2_animationAnimmethod()
    {

        anim = firing_2.GetComponent<Animator>();
        anim.Play("firing_anim_3");
    }
    //
    void _c_ic_anim_animationAnimmethod()
       
    {

        anim = c_ic_anim.GetComponent<Animator>();
        anim.Play("c&ic_anim");
    }
    //
    void _toc_anim_animationAnimmethod()

    {

        anim = toc_anim.GetComponent<Animator>();
        anim.Play("toc_anim" );
    }
    //
    void _mag_animationAnimmethod()

    {

        anim = mag.GetComponent<Animator>();
        anim.Play("mag_anim");
    }
    //
    void _tws_animationAnimmethod()

    {

        anim = tong_with_scale.GetComponent<Animator>();
        anim.Play("tws_anim");
    }
    //
    void _twc_animationAnimmethod()

    {

        anim = tong_with_copper.GetComponent<Animator>();
        anim.Play("twc_anim");
    }
    //
    void _ice_animationAnimmethod()

    {

        anim = ice.GetComponent<Animator>();
        anim.Play("ice_anim");
    }
    //
    void _earth_animationAnimmethod()

    {

        anim = earth.GetComponent<Animator>();
        anim.Play("earth_anim");
    }
    //




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
    void _comb_1_audioMethod()

    {
        myAudio.clip = comb_1;
        myAudio.Play();
    }
    //
    void _comb_2_audioMethod()

    {
        myAudio.clip = comb_2;
        myAudio.Play();
    }
    //
    void _c_ic_sub_1_audioMethod()

    {
        myAudio.clip = c_ic_sub_1;
        myAudio.Play();
    }
    //
    void _c_ic_sub_2_audioMethod()

    {
        myAudio.clip = c_ic_sub_2;
        myAudio.Play();
    }
    //
    void _c_ic_sub_3_audioMethod()

    {
        myAudio.clip = c_ic_sub_3;
        myAudio.Play();
    }
    //
    void _cfb_1_audioMethod()

    {
        myAudio.clip = cfb_1;
        myAudio.Play();
    }
    //
    void _cfb_2_audioMethod()

    {
        myAudio.clip = cfb_2;
        myAudio.Play();
    }
    //
    void _i_temp_1_audioMethod()

    {
        myAudio.clip = i_temp_1;
        myAudio.Play();
    }
    //
    void _i_temp_2_audioMethod()

    {
        myAudio.clip = i_temp_2;
        myAudio.Play();
    }
    //
    void _i_temp_3_audioMethod()

    {
        myAudio.clip = i_temp_3;
        myAudio.Play();
    }
    //
    void _infam_1_audioMethod()

    {
        myAudio.clip = inflam_sub_1;
        myAudio.Play();
    }
    //
    void _infam_2_audioMethod()

    {
        myAudio.clip = inflam_sub_2;
        myAudio.Play();
    }
    //
    void _infam_3_audioMethod()

    {
        myAudio.clip = inflam_sub_3;
        myAudio.Play();
    }
    //
    void _infam_4_audioMethod()

    {
        myAudio.clip = inflam_sub_4;
        myAudio.Play();
    }
    //
    void _h_o_1_audioMethod()

    {
        myAudio.clip = h_o_1;
        myAudio.Play();
    }
    //
    void _h_o_2_audioMethod()

    {
        myAudio.clip = h_o_2;
        myAudio.Play();
    }
    //
    void _h_o_3_audioMethod()

    {
        myAudio.clip = h_o_3  ;
        myAudio.Play();
    }
    //
    void _h_o_4_audioMethod()

    {
        myAudio.clip = h_o_4;
        myAudio.Play();
    }
    //
    void _toc_1_audioMethod()

    {
        myAudio.clip = toc_1;
        myAudio.Play();
    }
    //
    void _toc_2_audioMethod()

    {
        myAudio.clip = toc_2;
        myAudio.Play();
    }
    //
    void _toc_3_audioMethod()

    {
        myAudio.clip = toc_3;
        myAudio.Play();
    }
    //
    void _toc_4_audioMethod()

    {
        myAudio.clip = toc_4;
        myAudio.Play();
    }
    //
    void _toc_5_audioMethod()

    {
        myAudio.clip = toc_5;
        myAudio.Play();
    }
    //
    void _toc_6_audioMethod()

    {
        myAudio.clip = toc_6;
        myAudio.Play();
    }
    //
    void _fl_1_audioMethod()

    {
        myAudio.clip = fl_1;
        myAudio.Play();
    }
    //
    void _fl_2_audioMethod()

    {
        myAudio.clip = fl_2;
        myAudio.Play();
    }
    //
    void _fl_3_audioMethod()

    {
        myAudio.clip = fl_3;
        myAudio.Play();
    }
    //
    void _fl_4_audioMethod()

    {
        myAudio.clip = fl_4;
        myAudio.Play();
    }
    //
    void _fl_5_audioMethod()

    {
        myAudio.clip = fl_5;
        myAudio.Play();
    }
    //
    void _fl_6_audioMethod()

    {
        myAudio.clip = fl_6;
        myAudio.Play();
    }
    //
    void _fl_7_audioMethod()

    {
        myAudio.clip = fl_7;
        myAudio.Play();
    }
    //
    void _fl_8_audioMethod()

    {
        myAudio.clip = fl_8;
        myAudio.Play();
    }
    //
    void _fl_9_audioMethod()

    {
        myAudio.clip = fl_9;
        myAudio.Play();
    }
    //
    void _fl_10_audioMethod()

    {
        myAudio.clip = fl_10;
        myAudio.Play();
    }
    //
    void _fl_11_audioMethod()

    {
        myAudio.clip = fl_11;
        myAudio.Play();
    }
    //
    void _fl_12_audioMethod()

    {
        myAudio.clip = fl_12;
        myAudio.Play();
    }
    //
    void _fl_13_audioMethod()

    {
        myAudio.clip = fl_13;
        myAudio.Play();
    }
    //
    void _fl_14_audioMethod()

    {
        myAudio.clip = fl_14;
        myAudio.Play();
    }
    //
    void _fl_15_audioMethod()

    {
        myAudio.clip = fl_15;
        myAudio.Play();
    }
    //

    public GameObject extinguisherParticle;
    bool usedExtinguisher = false;
    public void ExtinguisherTogggle()
    {
        if (!usedExtinguisher)
        {
            usedExtinguisher=true;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
        if (extinguisherParticle.activeInHierarchy)
        {
            extinguisherParticle.SetActive(false);
        }
        else
        {
            extinguisherParticle.SetActive(true);
            Destroy(extinguisherParticle.transform.parent.gameObject, 3f);
        }
    }

    public void ExtinguisherPlacement(GameObject obj)
    {
        obj.GetComponent<TargetController>().miniGameClickable = true;
        obj.transform.SetParent(InventoryManager.Instance.player.objectHolder);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
    }

    public void ExtinguisherDrop(GameObject obj)
    {
        obj.transform.SetParent(null);
    }

    public MeshRenderer buttonMesh;
    public Material buttonGreenMat;
    public GameObject gasObj;
    public Collider woodOptionColoider;
    public void TurningOnInertGas()
    {
        buttonMesh.material = buttonGreenMat;
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        //gasObj.SetActive(true);
        Destroy(buildingFire, 2f);
        firePar.gameObject.SetActive(false);
        woodOptionColoider.enabled = true;
       
    }

    public void MissionFail()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    public void StepCompleted()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public CanvasGroup fadeInAndOut;
    public GameObject buildingFire;
    public void FireCutScene()
    {
        fadeInAndOut.DOFade(1,1).OnComplete(() =>
            {
                fadeInAndOut.DOFade(0, 1);
                buildingFire.SetActive(true);
                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        });
    }

    public void UseWater(GameObject obj)
    {
        obj.SetActive(false);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void MeltKey(GameObject key)
    {
        key.transform.GetChild(0).gameObject.SetActive(false);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        //key.transform.position = new Vector3(key.transform.position.x,key.transform.position.y,key.transform.position.z + 2);
    }

    public GameObject matchbox;
    public ParticleSystem firePar;
    private bool fireLitUp;
    public TargetController meltFire;
    public GameObject finalDoor;
    public void LightUpFire()
    {
        firePar.gameObject.SetActive(true);
        firePar.Play();
        fireLitUp = true;
        //meltFire.enabled = true;
        finalDoor.tag = "Interactable";
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        woodOptionColoider.enabled = true;
    }

    public ParticleSystem exploding;
    public GameObject postExplosionObj;

    public void ExplodeWay(GameObject obj)
    {
        obj.SetActive(false);
        postExplosionObj.SetActive(true);
    }

    public void OpenDoor(Animator anim)
    {
        anim.SetTrigger("Trigger");
        matchbox.SetActive(true);
    }


}
