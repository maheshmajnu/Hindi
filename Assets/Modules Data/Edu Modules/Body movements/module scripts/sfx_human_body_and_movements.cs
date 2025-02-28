using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_human_body_and_movements : MonoBehaviour
{
    public Transform waypoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypoint.player = InventoryManager.Instance.player.transform;
        waypointCanvas.SetActive(true);
        waypoint.target = target;
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
        SetWayPoint(waypoint1);
    }


    //on and off
    [Header("Explanation Assets")]


    public GameObject baby;
    public GameObject brain;
    public GameObject sketeon;
    public GameObject rock1;
    public GameObject rock;
    public GameObject skull;
    public GameObject sholder;
    public GameObject rib_bone;
    public GameObject incubator;
    public GameObject muscle;
    public GameObject bobymovementsT;
    public GameObject anewbornbaby;
    public GameObject ourbodies;
    public GameObject theribs;
    public GameObject theribscage;
    public GameObject shoulderbone;
    public GameObject theskull;
    public GameObject theskeleton;
    public GameObject musclesd;
    public GameObject differenttypesofjoints;
    public GameObject ballandsoketsjointsd;
    public GameObject pivotaljointsd;
    public GameObject hingejointsd;
    public GameObject fixedjointsd;
    public GameObject whatisgaitofanimals;
    public GameObject earthwormt;
    public GameObject snailt;
    public GameObject cockroacht;
    public GameObject box;
    public GameObject pivotal;
    public GameObject gait;
    




    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject sketeonanim;
    public GameObject earthwormanim;
    public GameObject snail;
    public GameObject crockrach;


    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("cam_anim", 0, targetNormalizedTime);
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






    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip introau;
    public AudioClip babyau;
    public AudioClip skeletonau;
    public AudioClip ribandchestau;
    public AudioClip ribcageau;
    public AudioClip sholderau;
    public AudioClip skullau;
    public AudioClip sketeonbendau;
    public AudioClip musclesau;
    public AudioClip pivotalau;
    public AudioClip hingeau;
    public AudioClip fixedau;
    public AudioClip ballau;
    public AudioClip gaitintroau;
    public AudioClip eartwormau;
    public AudioClip snailau;
    public AudioClip cockroachau;


    // Line on/off

    //
    void baby_methodON()
    {
        baby.SetActive(true);
    }


    void baby_methodOFF()
    {
        baby.SetActive(false);
    }
    //
    void brain_methodON()
    {
        brain.SetActive(true);
    }


    void brain_methodOFF()
    {
        brain.SetActive(false);
    }



    //
    void corckrach_methodON()
    {
        crockrach.SetActive(true);
    }


    void crockrach_methodOFF()
    {
        crockrach.SetActive(false);
    }
    //
    void rock1_methodON()
    {
        rock1.SetActive(true);
        rock.SetActive(true);
    }


    void rock1_methodOFF()
    {
        rock1.SetActive(false);
        rock.SetActive(false);
    }
    //
    void sketeon_methodON()
    {
        sketeon.SetActive(true);
    }


    void sketeon_methodOFF()
    {
        sketeon.SetActive(false);
    }
    //

    void sketeon1_methodON()
    {
        sketeon.SetActive(true);
    }


    void sketeon1_methodOFF()
    {
        sketeon.SetActive(false);
    }
    //

    void sketeon3_methodON()
    {
        sketeon.SetActive(true);
    }


    void sketeon3_methodOFF()
    {
        sketeon.SetActive(false);
    }
    //

    void snail_methodON()
    {
        snail.SetActive(true);
    }


    void snail_methodOFF()
    {
        snail.SetActive(false);
    }
    //
    void skull_methodON()
    {
        skull.SetActive(true);
    }


    void skull_methodOFF()
    {
        skull.SetActive(false);
    }
    //

    void skull2_methodON()
    {
        skull.SetActive(true);
        sketeon.SetActive(true);
    }


    void skull2_methodOFF()
    {
        skull.SetActive(false);
        sketeon.SetActive(false);
    }

    //

    void sholder_methodON()
    {
        sholder.SetActive(true);
    }


    void sholder_methodOFF()
    {
        sholder.SetActive(false);
    }
    //
    void rib_bone_methodON()
    {
        rib_bone.SetActive(true);
    }


    void rib_bone_methodOFF()
    {
        rib_bone.SetActive(false);
    }
    //
    void incubator_methodON()
    {
        incubator.SetActive(true);
    }


    void incubator_methodOFF()
    {
        incubator.SetActive(false);
    }

    //
    void earthworm_methodON()
    {
        earthwormanim.SetActive(true);
    }


    void earthworm_methodOFF()
    {
        earthwormanim.SetActive(false);
    }

    //
    void box_methodON()
    {
        box.SetActive(true);
    }


    void box_methodOFF()
    {
        box.SetActive(false);
    }

    //
    void muscle_methodON()
    {
        muscle.SetActive(true);
    }


    void muscle_methodOFF()
    {
        muscle.SetActive(false);
    }
    //

    void gait_methodON()
    {
        gait.SetActive(true);
    }


    void gait_methodOFF()
    {
        gait.SetActive(false);
    }
    //


    void pivotal_methodON()
    {
        pivotal.SetActive(true);
    }


    void pivotal_methodOFF()
    {
        pivotal.SetActive(false);
    }


    //
    void bobymovementsT_methodON()
    {
        bobymovementsT.SetActive(true);
    }

    //

    void anewbornbabayd_methodON()
    {
        anewbornbaby.SetActive(true);
    }

    //

    void ourbodiesd_methodON()
    {
        ourbodies.SetActive(true);
    }


    //
    void theribsd_methodON()
    {
        theribs.SetActive(true);
    }

    //
    void theribcaged_methodON()
    {
        theribscage.SetActive(true);
    }


    //
    void shoulderbonesd_methodON()
    {
        shoulderbone.SetActive(true);
    }


    //
    void theskulld_methodON()
    {
        theskull.SetActive(true);
    }


    //
    void theskeletond_methodON()
    {
        theskeleton.SetActive(true);
    }


    //
    void musclesdd_methodON()
    {
        musclesd.SetActive(true);
    }


    //
    void differenttypesofjointst_methodON()
    {
        differenttypesofjoints.SetActive(true);
    }

    //
    void ballandsockeetjointsd_methodON()
    {
        ballandsoketsjointsd.SetActive(true);
    }

    //
    void pivotaljontesd_methodON()
    {
        pivotaljointsd.SetActive(true);
    }


    //
    void hingejointsd_methodON()
    {
        hingejointsd.SetActive(true);
    }

    //
    void fixedjoitsjontesdd_methodON()
    {
        fixedjointsd.SetActive(true);
    }

    //
    void whatisgaitoganimal_methodON()
    {
        whatisgaitofanimals.SetActive(true);
    }

    //
    void earthwormt_methodON()
    {
        earthwormt.SetActive(true);
    }

    //
    void snailt_methodON()
    {
        snailt.SetActive(true);
    }

    //
    void cockroacht_methodON()
    {
        cockroacht.SetActive(true);
    }


    //













    //Audio play

    void introau_method()
    {
        myAudio.clip = introau;
        myAudio.Play();
    }

    //

    void babyau_method()
    {
        myAudio.clip = babyau;
        myAudio.Play();
    }
    //

    void skeletonau_method()
    {
        myAudio.clip = skeletonau;
        myAudio.Play();
    }
    //

    void ribcageau_method()
    {
        myAudio.clip = ribcageau;
        myAudio.Play();
    }
    //

    void ribandchestau_method()
    {
        myAudio.clip = ribandchestau;
        myAudio.Play();
    }
    //

    void sholderau_method()
    {
        myAudio.clip = sholderau;
        myAudio.Play();
    }
    //

   
    void sketonbendau_method()
    {
        myAudio.clip = sketeonbendau;
        myAudio.Play();
    }


    //
    void skullau_method()
    {
        myAudio.clip = skullau;
        myAudio.Play();
    }


    //

    void musclesau_method()
    {
        myAudio.clip = musclesau;
        myAudio.Play();
    }

    //

    void ballau_method()
    {
        myAudio.clip = ballau;
        myAudio.Play();
    }

    //

    void pivotalau_method()
    {
        myAudio.clip = pivotalau;
        myAudio.Play();
    }

    //

    void hingeau_method()
    {
        myAudio.clip = hingeau;
        myAudio.Play();
    }

    //

    void fixedau_method()
    {
        myAudio.clip = fixedau;
        myAudio.Play();
    }

    //

    void gaitintroau_method()
    {
        myAudio.clip = gaitintroau;
        myAudio.Play();
    }
    //

    void earthwormau_method()
    {
        myAudio.clip = eartwormau;
        myAudio.Play();
    }
    //

    void snailau_method()
    {
        myAudio.clip = snailau;
        myAudio.Play();
    }
    //

    void cockroachau_method()
    {
        myAudio.clip = cockroachau;
        myAudio.Play();
    }








    //Animation Play

    void _sketeon_anim_Method()
    {
        anim = sketeon.GetComponent<Animator>();
        anim.Play("gate sketeon sliderAction");
    }


    // anim

    void _earthworm_anim_Method()
    {
        anim = earthwormanim.GetComponent<Animator>();
        anim.Play("gate sketeon sliderAction");
    }


    // 

    void _snail_anim_Method()
    {
        anim = snail.GetComponent<Animator>();
        anim.Play("gate snail sliderAction");
    }


    // anim

    void _cockroach_anim_Method()
    {
        anim = cockroacht.GetComponent<Animator>();
        anim.Play("gate ockroach sliderAction");
    }



    private bool ribsMiniGameSatarted = false;
    private bool skullMiniGameSatarted = false;
    public TargetController ribsMiniGame;
    public TargetController skullMiniGame;
    private int index;
    public Camera cam;
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        if (ribsMiniGameSatarted)
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
                        if (raycastHit.collider.gameObject.name == "Rib")
                        {
                            index++;
                            TargetController controller = raycastHit.collider.gameObject.GetComponent<TargetController>();
                            controller.defaultEvent.Invoke();

                            if (index == 7)
                            {
                                index = 0;
                                ribsMiniGame.EndMiniGame();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                                ribsMiniGameSatarted = false;
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

        if (skullMiniGameSatarted)
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
                        if (raycastHit.collider.gameObject.name == "Skull")
                        {
                            index++;
                            TargetController controller = raycastHit.collider.gameObject.GetComponent<TargetController>();
                            controller.defaultEvent.Invoke();

                            if (index == 6)
                            {
                                index = 0;
                                skullMiniGame.EndMiniGame();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                                skullMiniGameSatarted = false;
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

    public void RibsMiniGame()
    {
        ribsMiniGameSatarted = true;
    }

    public void SkullMiniGame()
    {
        skullMiniGameSatarted = true;
    }

    public void MiniGameStart()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        waypointCanvas.SetActive(false);
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

        waypointCanvas.SetActive(true);
    }

    public TargetController movementsMiniGame;
    public void DropJoints()
    {
        index++;
        Debug.Log("DROPPED");

        if (index == 5)
        {
            index = 0;
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            movementsMiniGame.EndMiniGame();
        }
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

































}
