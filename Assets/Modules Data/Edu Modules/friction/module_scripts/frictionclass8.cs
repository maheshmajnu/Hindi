using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class frictionclass8 : MonoBehaviour
{

    public Transform wayPoint1;
    public MissionWaypoint waypoint;
    public GameObject waypointCanvas;

    public void SetWayPoint(Transform target)
    {
        waypoint.player = InventoryManager.Instance.player.transform;
        waypoint.target = target;
        waypointCanvas.SetActive(true);
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

        SetWayPoint(wayPoint1);
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

    }

    public void EndMiniGame(TargetController miniGame)
    {
        miniGame.EndMiniGame();
        StepComp();
    }

    public void EndMiniGameDelay(TargetController miniGame)
    {
        StartCoroutine(DelayMiniGameEnd(miniGame));
    }

    IEnumerator DelayMiniGameEnd(TargetController miniGame)
    {
        yield return new WaitForSeconds(3);
        miniGame.EndMiniGame();
        StepComp();
    }

    public void TurnOnObjWithDelay(GameObject obj)
    {
        StartCoroutine(DelayGOOff(obj));
    }

    IEnumerator DelayGOOff(GameObject obj)
    {
        yield return new WaitForSeconds(3);
        obj.SetActive(true);
    }

    public void MiniGameStartDelay(TargetController miniGame)
    {
        Debug.Log("Mini Game Start");
        miniGame.Output();
    }

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
        StepComp();
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    public void StepComp()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void FinalLevelCheck()
    {
        if(!shoesCollected)
        {
            MissionFailed();
        }
    }

    bool shoesCollected = false;
    public void CollectedShoes()
    {
        shoesCollected = true;
    }


    // ON - OFF gameobjects
    [Header("Explanation Assets")]
    public GameObject kidkickingball;
    public GameObject walkingkid;
    public GameObject movingcar;
    public GameObject hammer;
    public GameObject slowpushkid;
    public GameObject hardpushkid;
    public GameObject boxA;
    public GameObject boxB;
    public GameObject boxC;
    public GameObject boxD;
    public GameObject trolley;
    public GameObject boardA;
    public GameObject boardB;
    public GameObject slowkick;
    public GameObject slowball;
    public GameObject hands;
    public GameObject kidfoot;
    public GameObject cartwo;
    public GameObject tin;
    public GameObject arrows;
    public GameObject arrowsB;
    public GameObject arrowsC;
    public GameObject arrowsD;
    public GameObject arrowsE;
    public GameObject bus;

    public GameObject frictionS;
    public GameObject frictionpro;
    public GameObject frictiontypes;
    public GameObject slidingfriction;
    public GameObject rollingfriction;
    public GameObject friction;
    public GameObject frictioneffs;
    public GameObject frictionadvantages;
    public GameObject frictiondisadvantages;
    public GameObject methodsofincreasing;
    public GameObject waysofreducing;
    public GameObject increasingreducing;
    public GameObject frictiondown;
    public GameObject appliedfrictional;
    public GameObject applied;
    public GameObject frictional;
    public GameObject staticfriction;
    public GameObject one;
    public GameObject two;
    public GameObject rollingdown;
    public GameObject ballbearings;
    public GameObject solid;
    public GameObject nofriction;
    public GameObject wearntear;
    public GameObject tyresofvehicles;
    public GameObject shoesoles;
    public GameObject lubricants;
    public GameObject ballbearingsD;
    public GameObject fishshape;
    public GameObject grooves;
    public GameObject powder;
    public GameObject matchstickflame;


    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]

    

    public GameObject kkball;
    public GameObject football;
    public GameObject pullingkid;
    public GameObject Pull_Heavy_Object;
    public GameObject roller_stand;
    public GameObject book;
    public GameObject hand_writing;
    public GameObject walk;
    public GameObject walking;
    public GameObject push;
    public GameObject pushing;
    public GameObject carmoving;
    public GameObject hammeranim;
    public GameObject slowpushkidanim;
    public GameObject hardpushkidanim;
    public GameObject boxbanim;
    public GameObject boxcanim;
    public GameObject boxdanim;
    public GameObject trolleyanim;
    public GameObject slowkickanim;
    public GameObject slowballanim;
    public GameObject lefthand;
    public GameObject righthand;
    public GameObject matchstick;
    public GameObject kidtrip;
    public GameObject tinanim;


    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip audio_1;
    public AudioClip audio_2;
    public AudioClip audio_3;
    public AudioClip audio_4;
    public AudioClip audio_5;
    public AudioClip audio_6;
    public AudioClip audio_7;
    public AudioClip audio_8;
    public AudioClip audio_9;
    public AudioClip audio_10;
    public AudioClip audio_11;
    public AudioClip audio_12;
    public AudioClip audio_13;
    public AudioClip audio_14;
    public AudioClip audio_15;
    public AudioClip audio_16;
    public AudioClip audio_17;
    public AudioClip audio_18;
    public AudioClip audio_19;
    public AudioClip audio_20;
    public AudioClip audio_21;
    public AudioClip audio_22;
    public AudioClip audio_23;
    public AudioClip audio_24;
    public AudioClip audio_25;
    public AudioClip audio_26;
    public AudioClip audio_27;
    public AudioClip audio_28;
    public AudioClip audio_29;
    public AudioClip audio_30;
    public AudioClip audio_31;
    public AudioClip audio_32;
    public AudioClip audio_33;
    public AudioClip audio_34;
    public AudioClip audio_35;





    public AudioClip audio_1b;
    public AudioClip audio_2b;
    public AudioClip audio_3b;
    public AudioClip audio_4b;
    public AudioClip audio_5b;
    public AudioClip audio_6b;
    public AudioClip audio_7b;
    public AudioClip audio_8b;
    public AudioClip audio_9b;
    public AudioClip audio_10b;
    public AudioClip audio_11b;
    public AudioClip audio_12b;
    public AudioClip audio_13b;
    public AudioClip audio_14b;
    public AudioClip audio_15b;
    public AudioClip audio_16b;
    public AudioClip audio_17b;
    public AudioClip audio_18b;
    public AudioClip audio_19b;
    public AudioClip audio_20b;
    public AudioClip audio_21b;
    public AudioClip audio_22b;
    public AudioClip audio_23b;
    public AudioClip audio_24b;
    public AudioClip audio_25b;
    public AudioClip audio_26b;
    public AudioClip audio_27b;
    public AudioClip audio_28b;
    public AudioClip audio_29b;
    public AudioClip audio_30b;
    public AudioClip audio_31b;
    public AudioClip audio_32b;
    public AudioClip audio_33b;
    public AudioClip audio_34b;
    public AudioClip audio_35b;
    public AudioClip audio_36b;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    void _matchstickflameMethodON()
    {
        matchstickflame.SetActive(true);
    }
    void _matchstickflameMethodoOFF()
    {
        matchstickflame.SetActive(false);
    }
    //

    void _BusMethodON()
    {
        bus.SetActive(true);
    }
    void _BusMethodoOFF()
    {
        bus.SetActive(false);
    }
    //
    void _ArrowsMethodON()
    {
        arrows.SetActive(true);
    }
    void _ArrowsMethodoOFF()
    {
        arrows.SetActive(false);
    }
    //
    void _ArrowsBMethodON()
    {
        arrowsB.SetActive(true);
    }
    void _ArrowsBMethodoOFF()
    {
        arrowsB.SetActive(false);
    }
    //
    void _ArrowsCMethodON()
    {
        arrowsC.SetActive(true);
    }
    void _ArrowsCMethodoOFF()
    {
        arrowsC.SetActive(false);
    }
    //
    void _ArrowsDMethodON()
    {
        arrowsD.SetActive(true);
    }
    void _ArrowsDMethodoOFF()
    {
        arrowsD.SetActive(false);
    }
    //
    void _ArrowsEMethodON()
    {
        arrowsE.SetActive(true);
    }
    void _ArrowsEMethodoOFF()
    {
        arrowsE.SetActive(false);
    }
    //



    void _WearNTearMethodON()
    {
        wearntear.SetActive(true);
    }
    void _WearNTearMethodoOFF()
    {
        wearntear.SetActive(false);
    }
    //
    void _TyresofvehiclesMethodON()
    {
        tyresofvehicles.SetActive(true);
    }
    void _TyresofvehiclesMethodoOFF()
    {
        tyresofvehicles.SetActive(false);
    }
    //
    void _ShoesolesMethodON()
    {
        shoesoles.SetActive(true);
    }
    void _ShoesolesMethodoOFF()
    {
        shoesoles.SetActive(false);
    }
    //
    void _LubricantsMethodON()
    {
        lubricants.SetActive(true);
    }
    void _LubricantsMethodoOFF()
    {
        lubricants.SetActive(false);
    }
    //
    void _BallBearingsDMethodON()
    {
        ballbearingsD.SetActive(true);
    }
    void _BallBearingsDMethodoOFF()
    {
        ballbearingsD.SetActive(false);
    }
    //
    void _FishShapeMethodON()
    {
        fishshape.SetActive(true);
    }
    void _FishShapeMethodoOFF()
    {
        fishshape.SetActive(false);
    }
    //
    void _GroovesMethodON()
    {
        grooves.SetActive(true);
    }
    void _GroovesMethodoOFF()
    {
        grooves.SetActive(false);
    }
    //
    void _PowderMethodON()
    {
        powder.SetActive(true);
    }
    void _PowderMethodoOFF()
    {
        powder.SetActive(false);
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
            animator.Play("cameraanimation", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }
    }

    private bool isMuted = false;

    public void ToggleAudio()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
        Debug.Log("Audio Muted: " + isMuted);
    }

    public void _Jump_To1(float value)
    {
        ToggleAudio();
        RestartSceneWithKeyframe(value);
    }

    private void RestartSceneWithKeyframe(float normalizedTime)
    {
        targetNormalizedTime = normalizedTime; // Store the keyframe to jump to
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(1);
    }







































    void _FrictionSMethodON()
    {
        frictionS.SetActive(true);
    }
    void _FrictionSMethodoOFF()
    {
        frictionS.SetActive(false);
    }
    //
    void _FrictionProMethodON()
    {
        frictionpro.SetActive(true);
    }
    void _FrictionProMethodoOFF()
    {
        frictionpro.SetActive(false);
    }
    //
    void _FrictionTypesMethodON()
    {
        frictiontypes.SetActive(true);
    }
    void _FrictionTypesMethodoOFF()
    {
        frictiontypes.SetActive(false);
    }
    //
    void _SlidingFrictionMethodON()
    {
        slidingfriction.SetActive(true);
    }
    void _SlidingFrictionMethodoOFF()
    {
        slidingfriction.SetActive(false);
    }
    //
    void _RollingFrictionMethodON()
    {
        rollingfriction.SetActive(true);
    }
    void _RollingFrictionMethodoOFF()
    {
        rollingfriction.SetActive(false);
    }
    //
    void _FrictionMethodON()
    {
        friction.SetActive(true);
    }
    void _FrictionMethodoOFF()
    {
        friction.SetActive(false);
    }
    //
    void _FrictionEffsMethodON()
    {
        frictioneffs.SetActive(true);
    }
    void _FrictionEffsMethodoOFF()
    {
        frictioneffs.SetActive(false);
    }
    //
    void _FrictionAdvMethodON()
    {
        frictionadvantages.SetActive(true);
    }
    void _FrictionAdvMethodoOFF()
    {
        frictionadvantages.SetActive(false);
    }
    //
    void _FrictionDisadvMethodON()
    {
        frictiondisadvantages.SetActive(true);
    }
    void _FrictionDisadvMethodoOFF()
    {
        frictiondisadvantages.SetActive(false);
    }
    //
    void _IncreasingMethodON()
    {
        methodsofincreasing.SetActive(true);
    }
    void _IncreasingMethodoOFF()
    {
        methodsofincreasing.SetActive(false);
    }
    //
    void _ReducingMethodON()
    {
        waysofreducing.SetActive(true);
    }
    void _ReducingMethodoOFF()
    {
        waysofreducing.SetActive(false);
    }
    //
    void _IncreasingReducingMethodON()
    {
        increasingreducing.SetActive(true);
    }
    void _IncreasingReducingMethodoOFF()
    {
        increasingreducing.SetActive(false);
    }
    //
    void _FrictionDownMethodON()
    {
        frictiondown.SetActive(true);
    }
    void _FrictionDownMethodoOFF()
    {
        frictiondown.SetActive(false);
    }
    //
    void _AppliedFrictionalMethodON()
    {
        appliedfrictional.SetActive(true);
    }
    void _AppliedFrictionalMethodoOFF()
    {
        appliedfrictional.SetActive(false);
    }
    //
    void _AppliedMethodON()
    {
        applied.SetActive(true);
    }
    void _AppliedMethodoOFF()
    {
        applied.SetActive(false);
    }
    //
    void _FrictionalMethodON()
    {
        frictional.SetActive(true);
    }
    void _FrictionalMethodoOFF()
    {
        frictional.SetActive(false);
    }
    //
    void _StaticFrictionMethodON()
    {
        frictional.SetActive(true);
    }
    void _StaticFrictionMethodoOFF()
    {
        frictional.SetActive(false);
    }
    //
    void _OneMethodON()
    {
        one.SetActive(true);
    }
    void _OneMethodoOFF()
    {
        one.SetActive(false);
    }
    //
    void _TwoMethodON()
    {
        two.SetActive(true);
    }
    void _TwoMethodoOFF()
    {
        two.SetActive(false);
    }
    //
    void _RollingDownMethodON()
    {
        rollingdown.SetActive(true);
    }
    void _RollingDownMethodoOFF()
    {
        rollingdown.SetActive(false);
    }
    //
    void _BallBearingsMethodON()
    {
        ballbearings.SetActive(true);
    }
    void _BallBearingsMethodoOFF()
    {
        ballbearings.SetActive(false);
    }
    //
    void _SolidMethodON()
    {
        solid.SetActive(true);
    }
    void _SolidMethodoOFF()
    {
        solid.SetActive(false);
    }
    //
    void _NoFrictionMethodON()
    {
        nofriction
        .SetActive(true);
    }
    void _NoFrictionMethodoOFF()
    {
        nofriction.SetActive(false);
    }
    //
    






















    void _HandsMethodON()
    {
        hands.SetActive(true);
    }
    void _HandsMethodoOFF()
    {
        hands.SetActive(false);
    }
    //
    void _KidFootMethodON()
    {
        kidfoot.SetActive(true);
    }
    void _KidFootMethodoOFF()
    {
        kidfoot.SetActive(false);
    }
    //
    void _TinMethodON()
    {
        tin.SetActive(true);
    }
    void _TinMethodoOFF()
    {
        tin.SetActive(false);
    }
    //
    void _CarTwoMethodON()
    {
        cartwo.SetActive(true);
    }
    void _CarTwoMethodoOFF()
    {
        cartwo.SetActive(false);
    }
    //
    void _SlowkickMethodON()
    {
        slowkick.SetActive(true);
    }
    void _SlowkickMethodoOFF()
    {
        slowkick.SetActive(false);
    }
    //
    void _SlowballMethodON()
    {
        slowball.SetActive(true);
    }
    void _SlowballMethodoOFF()
    {
        slowball.SetActive(false);
    }
    //

    void _BoardAMethodON()
    {
        boardA.SetActive(true);
    }
    void _BoardAMethodoOFF()
    {
        boardA.SetActive(false);
    }
    //
    void _BoardBMethodON()
    {
        boardB.SetActive(true);
    }
    void _BoardBMethodoOFF()
    {
        boardB.SetActive(false);
    }
    //

    void _TrolleyMethodON()
    {
        trolleyanim.SetActive(true);
    }
    void _TrolleyMethodoOFF()
    {
        trolleyanim.SetActive(false);
    }
    //

    void _BoxDMethodON()
    {
        boxD.SetActive(true);
    }
    void _BoxDMethodoOFF()
    {
        boxD.SetActive(false);
    }
    //

    void _BoxCMethodON()
    {
        boxC.SetActive(true);
    }
    void _BoxCMethodoOFF()
    {
        boxC.SetActive(false);
    }
    //

    void _BoxBMethodON()
    {
        boxB.SetActive(true);
    }
    void _BoxBMethodoOFF()
    {
        boxB.SetActive(false);
    }
    //

    void _BoxAMethodON()
    {
        boxA.SetActive(true);
    }
    void _BoxAMethodoOFF()
    {
        boxA.SetActive(false);
    }
    //

    void _HardpushkidMethodON()
    {
        hardpushkid.SetActive(true);
    }
    void _HardpushkidMethodoOFF()
    {
        hardpushkid.SetActive(false);
    }
    //

    void _SlowpushkidMethodON()
    {
        slowpushkid.SetActive(true);
    }
    void _SlowpushkidMethodoOFF()
    {
        slowpushkid.SetActive(false);
    }
    //

    void _HammerMethodON()
    {
        hammer.SetActive(true);
    }
    void _HammerMethodoOFF()
    {
        hammer.SetActive(false);
    }
    //

    void _WalkingMethodON()
    {
        walking.SetActive(true);
    }
    void _WalkingMethodoOFF()
    {
        walking.SetActive(false);
    }
    //
    void _PushMethodON()
    {
        push.SetActive(true);
    }
    void _PushMethodoOFF()
    {
        push.SetActive(false);
    }
    //
    void _PushingMethodON()
    {
        pushing.SetActive(true);
    }
    void _PushingMethodoOFF()
    {
        pushing.SetActive(false);
    }
    //
    void _MovingCarMethodON()
    {
        movingcar.SetActive(true);
    }
    void _MovingCarMethodoOFF()
    {
        movingcar.SetActive(false);
    }
    //









    void _bookMethodON()
    {
        book.SetActive(true);
    }
    void _bookMethodoOFF()
    {
        book.SetActive(false);
    }




    void _kidkickingballMethodON()
    {
        kidkickingball.SetActive(true);
    }
    void _kidkickingballMethodoOFF()
    {
        kidkickingball.SetActive(false);
    }
    void _footballMethodON()
    {
        football.SetActive(true);
    }
    void _footballMethodoOFF()
    {
        football.SetActive(false);
    }
    void _pullingkidMethodON()
    {
        pullingkid.SetActive(true);
    }
    void _pullingkidMethodoOFF()
    {
        pullingkid.SetActive(false);
    }
    void _roller_standMethodON()
    {
        roller_stand.SetActive(true);
    }
    void _roller_standMethodoOFF()
    {
        roller_stand.SetActive(false);
    }




    void _kkballAnimmethod()
    {

        anim = kkball.GetComponent<Animator>();
        anim.Play("kickingballani");
    }

    void _footballAnimmethod()
    {

        anim = football.GetComponent<Animator>();
        anim.Play("football ani");
    }
    void _pullingkidAnimmethod()
    {

        anim = pullingkid.GetComponent<Animator>();
        anim.Play("Pull positin");
    }
    void _Pull_Heavy_ObjectAnimmethod()
    {

        anim = Pull_Heavy_Object.GetComponent<Animator>();
        anim.Play("pullingani");
    }
    void _roller_standAnimmethod()
    {

        anim = roller_stand.GetComponent<Animator>();
        anim.Play("pullingstand");
    }
    void _bookAnimmethod()
    {

        anim = book.GetComponent<Animator>();
        anim.Play("book ani");
    }
    void _hand_writingAnimmethod()
    {

        anim = hand_writing.GetComponent<Animator>();
        anim.Play("writingani");
    }
    void _walkingAnimmethod()
    {

        anim = walking.GetComponent<Animator>();
        anim.Play("Walking");
    }
    void _carmovingAnimmethod()
    {

        anim = carmoving.GetComponent<Animator>();
        anim.Play("Car animation");
    }
    void _HammerAnimmethod()
    {

        anim = hammeranim.GetComponent<Animator>();
        anim.Play("Hammer animation", -1, 0 );
    }
    void _SlowpushkidAnimmethod()
    {

        anim = slowpushkidanim.GetComponent<Animator>();
        anim.Play("Push");
    }
    void _HardpushkidAnimmethod()
    {

        anim = hardpushkidanim.GetComponent<Animator>();
        anim.Play("Pushing");
    }
    void _BoxBAnimmethod()
    {

        anim = boxbanim.GetComponent<Animator>();
        anim.Play("Box b animation");
    }
    void _BoxCAnimmethod()
    {

        anim = boxcanim.GetComponent<Animator>();
        anim.Play("Box c anim");
    }
    void _BoxDAnimmethod()
    {

        anim = boxdanim.GetComponent<Animator>();
        anim.Play("Box d anim");
    }
    void _TrolleyAnimmethod()
    {

        anim = trolleyanim.GetComponent<Animator>();
        anim.Play("Trolley");
    }
    void _SlowkickAnimmethod()
    {

        anim = slowkickanim.GetComponent<Animator>();
        anim.Play("Slow kick");
    }
    void _SlowballAnimmethod()
    {

        anim = slowballanim.GetComponent<Animator>();
        anim.Play("Slowball anim");
    }
    void _LeftHandAnimmethod()
    {

        anim = lefthand.GetComponent<Animator>();
        anim.Play("Left hand anim", -1, 0 );
    }
    void _RightHandAnimmethod()
    {

        anim = righthand.GetComponent<Animator>();
        anim.Play("Right hand anim", -1, 0 );
    }
    void _MatchstickAnimmethod()
    {

        anim = matchstick.GetComponent<Animator>();
        anim.Play("Matchstick anim");
    }
    void _KidTripAnimmethod()
    {

        anim = kidtrip.GetComponent<Animator>();
        anim.Play("Tripping");
    }
    void _TinAnimmethod()
    {

        anim = tinanim.GetComponent<Animator>();
        anim.Play("Tin anim");
    }






    





        void _audio_1b_audioMethod()
    {
        myAudio.clip = audio_1b;
        myAudio.Play();
    }
    void _audio_2b_audioMethod()
    {
        myAudio.clip = audio_2b;
        myAudio.Play();
    }
    void _audio_3b_audioMethod()
    {
        myAudio.clip = audio_3b;
        myAudio.Play();
    }
    void _audio_4b_audioMethod()
    {
        myAudio.clip = audio_4b;
        myAudio.Play();
    }
    void _audio_5b_audioMethod()
    {
        myAudio.clip = audio_5b;
        myAudio.Play();
    }
    void _audio_6b_audioMethod()
    {
        myAudio.clip = audio_6b;
        myAudio.Play();
    }
    void _audio_7b_audioMethod()
    {
        myAudio.clip = audio_7b;
        myAudio.Play();
    }
    void _audio_8b_audioMethod()
    {
        myAudio.clip = audio_8b;
        myAudio.Play();
    }
    void _audio_9b_audioMethod()
    {
        myAudio.clip = audio_9b;
        myAudio.Play();
    }
    void _audio_10b_audioMethod()
    {
        myAudio.clip = audio_10b;
        myAudio.Play();
    }
    void _audio_11b_audioMethod()
    {
        myAudio.clip = audio_11b;
        myAudio.Play();
    }
    void _audio_12b_audioMethod()
    {
        myAudio.clip = audio_12b;
        myAudio.Play();
    }
    void _audio_13b_audioMethod()
    {
        myAudio.clip = audio_13b;
        myAudio.Play();
    }
    void _audio_14b_audioMethod()
    {
        myAudio.clip = audio_14b;
        myAudio.Play();
    }
    void _audio_15b_audioMethod()
    {
        myAudio.clip = audio_15b;
        myAudio.Play();
    }
    void _audio_16b_audioMethod()
    {
        myAudio.clip = audio_16b;
        myAudio.Play();
    }
    void _audio_17b_audioMethod()
    {
        myAudio.clip = audio_17b;
        myAudio.Play();
    }
    void _audio_18b_audioMethod()
    {
        myAudio.clip = audio_18b;
        myAudio.Play();
    }
    void _audio_19b_audioMethod()
    {
        myAudio.clip = audio_19b;
        myAudio.Play();
    }
    void _audio_20b_audioMethod()
    {
        myAudio.clip = audio_20b;
        myAudio.Play();
    }
    void _audio_21b_audioMethod()
    {
        myAudio.clip = audio_21b;
        myAudio.Play();
    }
    void _audio_22b_audioMethod()
    {
        myAudio.clip = audio_22b;
        myAudio.Play();
    }
    void _audio_23b_audioMethod()
    {
        myAudio.clip = audio_23b;
        myAudio.Play();
    }
    void _audio_24b_audioMethod()
    {
        myAudio.clip = audio_24b;
        myAudio.Play();
    }
    void _audio_25b_audioMethod()
    {
        myAudio.clip = audio_25b;
        myAudio.Play();
    }
    void _audio_26b_audioMethod()
    {
        myAudio.clip = audio_26b;
        myAudio.Play();
    }
    void _audio_27b_audioMethod()
    {
        myAudio.clip = audio_27b;
        myAudio.Play();
    }
    void _audio_28b_audioMethod()
    {
        myAudio.clip = audio_28b;
        myAudio.Play();
    }
    void _audio_29b_audioMethod()
    {
        myAudio.clip = audio_29b;
        myAudio.Play();
    }
    void _audio_30b_audioMethod()
    {
        myAudio.clip = audio_30b;
        myAudio.Play();
    }
    void _audio_31b_audioMethod()
    {
        myAudio.clip = audio_31b;
        myAudio.Play();
    }
    void _audio_32b_audioMethod()
    {
        myAudio.clip = audio_32b;
        myAudio.Play();
    }
    void _audio_33b_audioMethod()
    {
        myAudio.clip = audio_33b;
        myAudio.Play();
    }
    void _audio_34b_audioMethod()
    {
        myAudio.clip = audio_34b;
        myAudio.Play();
    }
    void _audio_35b_audioMethod()
    {
        myAudio.clip = audio_35b;
        myAudio.Play();
    }
    void _audio_36b_audioMethod()
    {
        myAudio.clip = audio_36b;
        myAudio.Play();
    }








    void _audio_1_audioMethod()
    {
        myAudio.clip = audio_1;
        myAudio.Play();
    }
    void _audio_2_audioMethod()
    {
        myAudio.clip = audio_2;
        myAudio.Play();
    }
    void _audio_3_audioMethod()
    {
        myAudio.clip = audio_3;
        myAudio.Play();
    }
    void _audio_4_audioMethod()
    {
        myAudio.clip = audio_4;
        myAudio.Play();
    }
    void _audio_5_audioMethod()
    {
        myAudio.clip = audio_5;
        myAudio.Play();
    }
    void _audio_6_audioMethod()
    {
        myAudio.clip = audio_6;
        myAudio.Play();
    }
    void _audio_7_audioMethod()
    {
        myAudio.clip = audio_7;
        myAudio.Play();
    }
    void _audio_8_audioMethod()
    {
        myAudio.clip = audio_8;
        myAudio.Play();
    }
    void _audio_9_audioMethod()
    {
        myAudio.clip = audio_9;
        myAudio.Play();
    }
    void _audio_10_audioMethod()
    {
        myAudio.clip = audio_10;
        myAudio.Play();
    }

    void _audio_11_audioMethod()
    {
        myAudio.clip = audio_11;
        myAudio.Play();
    }
    void _audio_12_audioMethod()
    {
        myAudio.clip = audio_12;
        myAudio.Play();
    }
    void _audio_13_audioMethod()
    {
        myAudio.clip = audio_13;
        myAudio.Play();
    }
    void _audio_14_audioMethod()
    {
        myAudio.clip = audio_14;
        myAudio.Play();
    }
    void _audio_15_audioMethod()
    {
        myAudio.clip = audio_15;
        myAudio.Play();
    }
    void _audio_16_audioMethod()
    {
        myAudio.clip = audio_16;
        myAudio.Play();
    }
    void _audio_17_audioMethod()
    {
        myAudio.clip = audio_17;
        myAudio.Play();
    }
    void _audio_18_audioMethod()
    {
        myAudio.clip = audio_18;
        myAudio.Play();
    }
    void _audio_19_audioMethod()
    {
        myAudio.clip = audio_19;
        myAudio.Play();
    }
    void _audio_20_audioMethod()
    {
        myAudio.clip = audio_20;
        myAudio.Play();
    }
    void _audio_21_audioMethod()
    {
        myAudio.clip = audio_21;
        myAudio.Play();
    }
    void _audio_22_audioMethod()
    {
        myAudio.clip = audio_22;
        myAudio.Play();
    }
    void _audio_23_audioMethod()
    {
        myAudio.clip = audio_23;
        myAudio.Play();
    }
    void _audio_24_audioMethod()
    {
        myAudio.clip = audio_24;
        myAudio.Play();
    }
    void _audio_25_audioMethod()
    {
        myAudio.clip = audio_25;
        myAudio.Play();
    }
    void _audio_26_audioMethod()
    {
        myAudio.clip = audio_26;
        myAudio.Play();
    }
    void _audio_27_audioMethod()
    {
        myAudio.clip = audio_27;
        myAudio.Play();
    }
    void _audio_28_audioMethod()
    {
        myAudio.clip = audio_28;
        myAudio.Play();
    }
    void _audio_29_audioMethod()
    {
        myAudio.clip = audio_29;
        myAudio.Play();
    }
    void _audio_30_audioMethod()
    {
        myAudio.clip = audio_30;
        myAudio.Play();
    }
    void _audio_31_audioMethod()
    {
        myAudio.clip = audio_31;
        myAudio.Play();
    }
    void _audio_32_audioMethod()
    {
        myAudio.clip = audio_32;
        myAudio.Play();
    }
    void _audio_33_audioMethod()
    {
        myAudio.clip = audio_33;
        myAudio.Play();
    }
    void _audio_34_audioMethod()
    {
        myAudio.clip = audio_34;
        myAudio.Play();
    }
    void _audio_35_audioMethod()
    {
        myAudio.clip = audio_35;
        myAudio.Play();
    }




}
