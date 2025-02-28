using System.Collections;
using UnityEngine;

public class sfx_magnet : MonoBehaviour
{

    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject bar_magnet_2;
    public GameObject bar_magnet_3;
    public GameObject bar_magnet_4;
    public GameObject bar_magnet_5;
    public GameObject bar_magnet_6;
    public GameObject bolt;
    public GameObject gold_bar;
    public GameObject hammer_hitting;
    public GameObject horse_shoe_2;
    public GameObject horse_shoe_3;
    public GameObject human_hand1;
    public GameObject wool_ball;
    public GameObject key_1;
    public GameObject shpered_stick;
    public GameObject shpered_stick_bottom;
    public GameObject iron_bar;
    public GameObject BunsenBurner2;
    public GameObject Small_rock;
    public GameObject line_1;
    public GameObject bm_text_;
    public GameObject cm_text_;
    public GameObject um_text_;
    public GameObject rm_text_;
    public GameObject hsm_text_;
    public GameObject lab;

    // Exp - Animations
    private Animator anim;
    [Header("Explanation anims")]


    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camera", 0, targetNormalizedTime);
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




    public GameObject bar_magnet_1;
    public GameObject cylinder_magnet;
    public GameObject u_magnet;
    public GameObject horse_shoe_1;
    public GameObject ring_magnet;
    public GameObject hand;
    public GameObject hand_2;
    public GameObject nuts_anim;
    public GameObject nut_5;
    public GameObject bar_magnet_7;
    public GameObject bar_magnet_8;
    public GameObject bar_magnet_9;
    public GameObject bar_magnet_10;
    public GameObject bar_magnet_11;
    public GameObject bar_magnet_12;
    public GameObject bar_magnet_13;

    // Exp - Audio
    [Header("Audio files")]
    public AudioSource myAudio;

    public AudioClip h_m;
    public AudioClip o_m;
    public AudioClip m;
    public AudioClip nm_m;
    public AudioClip p_m;
    public AudioClip p_1;
    public AudioClip p_2;
    public AudioClip p_3;
    public AudioClip cgm_1;
    public AudioClip cgm_2;
    public AudioClip cm_1;
    public AudioClip cm_2;
    public AudioClip cm_3;
    public AudioClip cm_4;
    public AudioClip cm_5;

    void _lab_MethodON()
    {
        lab.SetActive(true);
    }

    void _lab_MethodOFF()
    {
        lab.SetActive(false);
    }

    void _small_rock_1_MethodON()
    {
        Small_rock.SetActive(true);
    }

    void _small_rock_1_MethodOFF()
    {
        Small_rock.SetActive(false);
    }

    void _line_1_MethodON()
    {
        line_1.SetActive(true);
    }

    void _L_M_MethodOFF()
    {
        line_1.SetActive(false);

    }

    void _bm_text_MethodON()
    {
        bm_text_.SetActive(true);
    }

    void _bm_text_MethodOFF()
    {
        bm_text_.SetActive(false);
    }

    void _cm_text_MethodON()
    {
        cm_text_.SetActive(true);
    }

    void _cm_text_MethodOFF()
    {
        cm_text_.SetActive(false);
    }

    void _um_text_MethodON()
    {
        um_text_.SetActive(true);
    }

    void _um_text_MethodOFF()
    {
        um_text_.SetActive(false);
    }

    void _rm_text_MethodON()
    {
        rm_text_.SetActive(true);
    }

    void _rm_text_MethodOFF()
    {
        rm_text_.SetActive(false);
    }

    void _hsm_text_MethodON()
    {
        hsm_text_.SetActive(true);
    }

    void _hsm_text_MethodOFF()
    {
        hsm_text_.SetActive(false);
    }


    void _s1_MethodON()
    {
        bar_magnet_1.SetActive(true);
        cylinder_magnet.SetActive(true);
        ring_magnet.SetActive(true);
        u_magnet.SetActive(true);
        horse_shoe_1.SetActive(true);

    }

    void _s1_MethodOFF()
    {
        bar_magnet_1.SetActive(false);
        cylinder_magnet.SetActive(false);
        ring_magnet.SetActive(false);
        u_magnet.SetActive(false);
        horse_shoe_1.SetActive(false);
    }

    void _s2_MethodON()
    {
        horse_shoe_2.SetActive(true);
        nuts_anim.SetActive(true);
        bolt.SetActive(true);


    }

    void _s2_MethodOFF()
    {
        horse_shoe_2.SetActive(false);
        nuts_anim.SetActive(false);
        bolt.SetActive(false);


    }


    void _s3_MethodON()
    {
        hand.SetActive(true);
        shpered_stick.SetActive(true);
        shpered_stick_bottom.SetActive(true);
        wool_ball.SetActive(true);
        gold_bar.SetActive(true);
        bar_magnet_2.SetActive(true);

    }

    void _s3_MethodOFF()
    {
        hand.SetActive(false);
        shpered_stick.SetActive(false);
        shpered_stick_bottom.SetActive(false);
        wool_ball.SetActive(false);
        gold_bar.SetActive(false);
        bar_magnet_2.SetActive(false);

    }


    void _s4_MethodON()
    {
        key_1.SetActive(true);
        nut_5.SetActive(true);
        bar_magnet_3.SetActive(true);

    }

    void _s4_MethodOFF()
    {
        key_1.SetActive(false);
        nut_5.SetActive(false);
        bar_magnet_3.SetActive(false);

    }

    void _s5_MethodON()
    {
        bar_magnet_12.SetActive(true);

    }

    void _s5_MethodOFF()
    {
        bar_magnet_12.SetActive(false);

    }

    void _s6_MethodON()
    {
        bar_magnet_7.SetActive(true);
        bar_magnet_8.SetActive(true);
        bar_magnet_9.SetActive(true);
        bar_magnet_10.SetActive(true);

    }

    void _s6_MethodOFF()
    {
        bar_magnet_7.SetActive(false);
        bar_magnet_8.SetActive(false);
        bar_magnet_9.SetActive(false);
        bar_magnet_10.SetActive(false);

    }

    void _s7_MethodON()
    {
        bar_magnet_4.SetActive(true);
        bar_magnet_6.SetActive(true);


    }

    void _s7_MethodOFF()
    {
        bar_magnet_4.SetActive(false);
        bar_magnet_6.SetActive(false);


    }


    void _s8_MethodON()
    {
        horse_shoe_3.SetActive(true);
        iron_bar.SetActive(true);


    }

    void _s8_MethodOFF()
    {
        horse_shoe_3.SetActive(false);
        iron_bar.SetActive(false);


    }


    void _s9_MethodON()
    {
        BunsenBurner2.SetActive(true);
        bar_magnet_11.SetActive(true);


    }

    void _s9_MethodOFF()
    {
        BunsenBurner2.SetActive(false);
        bar_magnet_11.SetActive(false);


    }


    void _s10_MethodON()
    {
        hammer_hitting.SetActive(true);
        bar_magnet_5.SetActive(true);


    }

    void _s10_MethodOFF()
    {
        hammer_hitting.SetActive(false);
        bar_magnet_5.SetActive(false);


    }


    void _s11_MethodON()
    {
        hand_2.SetActive(true);
        bar_magnet_13.SetActive(true);


    }


    void _s11_MethodOFF()
    {
        hand_2.SetActive(false);
        bar_magnet_13.SetActive(false);


    }





    void _bar_magnet_1_Animmethod()
    {

        anim = bar_magnet_1.GetComponent<Animator>();
        anim.Play("b_m_");
    }

    void _cylinder_magnet_Animmethod()
    {

        anim = cylinder_magnet.GetComponent<Animator>();
        anim.Play("c_m_");
    }

    void _u_magnet_Animmethod()
    {

        anim = u_magnet.GetComponent<Animator>();
        anim.Play("u_m_");
    }

    void _ring_magnet_Animmethod()
    {

        anim = ring_magnet.GetComponent<Animator>();
        anim.Play("r_m_");
    }

    void _horse_shoe_1_Animmethod()
    {

        anim = horse_shoe_1.GetComponent<Animator>();
        anim.Play("h_s_m_");
    }


    void _nuts_anim_Animmethod()
    {

        anim = nuts_anim.GetComponent<Animator>();
        anim.Play("nuts_anim", -1, 0);
    }

    void _iron_bar_Animmethod()
    {

        anim = iron_bar.GetComponent<Animator>();
        anim.Play("iron_bar_anim");


    }

    void _hammer_hitting_Animmethod()
    {

        anim = hammer_hitting.GetComponent<Animator>();
        anim.Play("hammer_hitting");

    }


    void _hand_Animmethod()
    {

        anim = hand.GetComponent<Animator>();
        anim.Play("hand_anim");
    }


    void _nut_5_Animmethod()
    {

        anim = nut_5.GetComponent<Animator>();
        anim.Play("nut_5_anim");
    }


    void _nut_anim_Animmethod()
    {

        anim = nuts_anim.GetComponent<Animator>();
        anim.Play("nuts_anim");
    }



    void _key_1_Animmethod()
    {

        anim = key_1.GetComponent<Animator>();
        anim.Play("key_anim");

    }


    void _bar_magnet_8_Animmethod()
    {

        anim = bar_magnet_8.GetComponent<Animator>();
        anim.Play("bar_8");


    }


    void _bar_magnet_10_Animmethod()
    {

        anim = bar_magnet_10.GetComponent<Animator>();
        anim.Play("n_not_2");


    }


    void _bar_magnet_9_Animmethod()
    {

        anim = bar_magnet_9.GetComponent<Animator>();
        anim.Play("bar_9");


    }


    void _bar_magnet_6_Animmethod()
    {

        anim = bar_magnet_6.GetComponent<Animator>();
        anim.Play("bar_6");


    }

    void _bar_magnet_13_Animmethod()
    {

        anim = bar_magnet_13.GetComponent<Animator>();
        anim.Play("bar_13");


    }

    void _hand_2_Animmethod()
    {

        anim = hand_2.GetComponent<Animator>();
        anim.Play("hand_2");

    }


    void _h_m_audioMethod()
    {
        myAudio.clip = h_m;
        myAudio.Play();
    }

    void _o_m_audioMethod()
    {
        myAudio.clip = o_m;
        myAudio.Play();
    }

    void _m_audioMethod()
    {
        myAudio.clip = m;
        myAudio.Play();
    }

    void _nm_m_audioMethod()
    {
        myAudio.clip = nm_m;
        myAudio.Play();
    }

    void _p_m_audioMethod()
    {
        myAudio.clip = p_m;
        myAudio.Play();
    }

    void _p_1_audioMethod()
    {
        myAudio.clip = p_1;
        myAudio.Play();
    }

    void _p_2_audioMethod()
    {
        myAudio.clip = p_2;
        myAudio.Play();
    }

    void _p_3_audioMethod()
    {
        myAudio.clip = p_3;
        myAudio.Play();
    }

    void _cgm_1_audioMethod()
    {
        myAudio.clip = cgm_1;
        myAudio.Play();
    }

    void _cgm_2_audioMethod()
    {
        myAudio.clip = cgm_2;
        myAudio.Play();
    }

    void _cm_1_audioMethod()
    {
        myAudio.clip = cm_1;
        myAudio.Play();
    }

    void _cm_2_audioMethod()
    {
        myAudio.clip = cm_2;
        myAudio.Play();
    }

    void _cm_3_audioMethod()
    {
        myAudio.clip = cm_3;
        myAudio.Play();
    }

    void _cm_4_audioMethod()
    {
        myAudio.clip = cm_4;
        myAudio.Play();
    }

    void _cm_5_audioMethod()
    {
        myAudio.clip = cm_5;
        myAudio.Play();
    }


    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }


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

    

    public Animator door1;
    public Animator door2;
    public void RotateMagnetBar(Animator anim)
    {
        StartCoroutine(OpenDoor(anim));
    }

    IEnumerator OpenDoor(Animator anim)
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Trigger");
        yield return new WaitForSeconds(1);
        door1.SetTrigger("Trigger");
    }

    public void RotateMagnetBar2(Animator anim)
    {
        StartCoroutine(OpenDoor2(anim));
    }

    IEnumerator OpenDoor2(Animator anim)
    {
        yield return new WaitForSeconds(1);
        anim.SetTrigger("Trigger");
        yield return new WaitForSeconds(1);
        door2.SetTrigger("Trigger");
    }
    public void MiniGameStart()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
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

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    public void PlayAnimTrigg(Animator anim)
    {
        anim.SetTrigger("Trigger"); // Replace "TriggerName" with the actual name of your trigger
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetBool("Bool", true);
    }

    public void EndAnim(Animator anim)
    {
        anim.SetBool("Bool", false);
    }

    private bool correctMagnet;
    private TargetController correctMagnetMiniGame;
    public LayerMask layerMask;
    public Camera cam;
    private int currentIndex;
    private bool collectedCompass = false;
    private bool collectedMagnet = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || InventoryManager.Instance.player.InteractIspressed)
        {
            if (!collectedCompass || !collectedMagnet)
            {
                StartCoroutine(CheckForItems());
            }
        }

        if (correctMagnet)
        {
            if (Input.GetMouseButtonDown(0))
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
                            currentIndex++;
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                            if (currentIndex == 5)
                            {
                                correctMagnetMiniGame.EndMiniGame();
                                correctMagnet = false;
                            }
                        }
                        else
                        {
                            Debug.Log("Mission Failed");
                        }
                    }
                }
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
                if (itm.item.itemName == "Compass" && !collectedCompass)
                {
                    collectedCompass = true;
                    InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                    break;
                }
                else if(itm.item.itemName == "Magnet" && !collectedMagnet)
                {
                    collectedMagnet = true;
                    InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                    break;
                }
            }
        }
    }

    private bool collectedKey = false;
    public void CollectedKey()
    {
        collectedKey = true;
    }

    public int index = 0;
    public TargetController minigame2;
    public void Lv2Game()
    {
        index++;
        if (index == 5)
        {
            StepComplete();
            minigame2.EndMiniGame();
        }
    }

    public void OpenDoor3(Animator anim)
    {
        if(collectedKey)
        {
            anim.SetTrigger("Trigger");
        }
    }
}

