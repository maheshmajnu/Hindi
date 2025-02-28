using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx_Chemical_effects_of_electric_current_class_8 : MonoBehaviour
{

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

    public AudioSource gameSoundFxSource;
    public List<AudioClip> level1AudioClips;
    private int index;

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

    public void PlayAnim(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void TurnOnGODelay(GameObject obj)
    {
        StartCoroutine(GOTurnOnDelay(obj));
    }

    IEnumerator GOTurnOnDelay(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(true);
    }

    public Transform lv3CamHolder;
    public void FreezePlayer()
    {
        MiniGameStart();
        this.gameObject.transform.position = lv3CamHolder.position;
        this.gameObject.transform.rotation = lv3CamHolder.rotation;
    }

    public void DeFreezePlayer()
    {
        MiniGameEnd();
    }

    public void Lv1DropObj(TargetController lv1MiniGame)
    {
        index++;

        if (index == 4)
        {
            index = 0;
            lv1MiniGame.EndMiniGame();
            StepComp();
        }
    }

    public List<string> combo1;
    public List<string> combo2;
    public List<string> combo3;
    public List<string> choosedCombo;
    public GameObject button;

    public void CorrectAnswerLv3(string ans)
    {
        index++;
        choosedCombo.Add(ans);

        if (index == 3)
        {
            index = 0;
            button.tag = "Interactable";
            return;
        }
    }

    public void Lv3Button()
    {
        if(AreListsEqual(combo1,choosedCombo))
        {
            Level4Delay();
        }
        else if(AreListsEqual(combo2,choosedCombo))
        {
            Level4Delay();
        }
        else if (AreListsEqual(combo3, choosedCombo))
        {
            Level4Delay();
        }
        else
        {
            MissionFailed();
        }
    }

    bool AreListsEqual(List<string> listA, List<string> listB)
    {
        HashSet<string> setA = new HashSet<string>(listA);
        HashSet<string> setB = new HashSet<string>(listB);
        return setA.SetEquals(setB);
    }

    public Transform lv4SpawnPoint;
    public void Level4Delay()
    {
        StepComp();
        StartCoroutine(Lv4Delay());
    }

    IEnumerator Lv4Delay()
    {
        Fade();
        yield return new WaitForSeconds(1.2f);
        InventoryManager.Instance.player.ChangePosition(lv4SpawnPoint);
    }

    public void Fade()
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
    }

    public void DelayLevel4()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }

    public void StepComp()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }



    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;

    public AudioClip audio1;
    public AudioClip audio2;
    public AudioClip audio3;
    public AudioClip audio4;
    public AudioClip audio5;
    public AudioClip audio6;
    public AudioClip audio7;
    public AudioClip audio8;
    public AudioClip audio9;
    public AudioClip audio10;
    public AudioClip audio11;
    public AudioClip audio12;
    public AudioClip audio13;
    public AudioClip audio14;
    public AudioClip audio15;
    public AudioClip audio16;
    public AudioClip audio17;
    public AudioClip audio18;
    public AudioClip audio19;
    public AudioClip audio20;
    public AudioClip audio21;
    public AudioClip audio22;
    public AudioClip audio23;
    public AudioClip audio24;
    public AudioClip audio25;
    public AudioClip audio26;




    // Exp - Animations
    [Header("Explanation anims")]
    private Animator anim;

    public GameObject batteranim;





    // ON - OFF gameobjects
    [Header("Explanation Assets")]




    public GameObject tester;
    public GameObject gold_silver;
    public GameObject oxygen;
    public GameObject hydrogen;
    public GameObject steel_cathode;
    public GameObject silver_anode;
    public GameObject IntroductionT;
    public GameObject GoodT;
    public GameObject do_liqT;
    public GameObject CEET;
    public GameObject ElectroplatingT;
    public GameObject Use_of_chemicalT;
    public GameObject Chemical_effects;
    public GameObject electrolysisD;
    public GameObject electroplating_widelyD;
    public GameObject coating_of_zinc;
    public GameObject electrolysis;




















    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



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
            animator.Play("New Animation", 0, targetNormalizedTime);
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

    void audio1_method()
    {
        myAudio.clip = audio1;
        myAudio.Play();
    }

    //

    void audio2_method()
    {
        myAudio.clip = audio2;
        myAudio.Play();
    }

    //

    void audio3_method()
    {
        myAudio.clip = audio3;
        myAudio.Play();
    }

    //

    void audio4_method()
    {
        myAudio.clip = audio4;
        myAudio.Play();
    }

    //

    void audio5_method()
    {
        myAudio.clip = audio5;
        myAudio.Play();
    }

    //

    void audio6_method()
    {
        myAudio.clip = audio6;
        myAudio.Play();
    }

    //

    void audio7_method()
    {
        myAudio.clip = audio7;
        myAudio.Play();
    }

    //

    void audio8_method()
    {
        myAudio.clip = audio8;
        myAudio.Play();
    }

    //

    void audio9_method()
    {
        myAudio.clip = audio9;
        myAudio.Play();
    }

    //

    void audio10_method()
    {
        myAudio.clip = audio10;
        myAudio.Play();
    }

    //

    void audio11_method()
    {
        myAudio.clip = audio11;
        myAudio.Play();
    }

    //

    void audio12_method()
    {
        myAudio.clip = audio12;
        myAudio.Play();
    }

    //

    void audio13_method()
    {
        myAudio.clip = audio13;
        myAudio.Play();
    }

    //

    void audio14_method()
    {
        myAudio.clip = audio14;
        myAudio.Play();
    }

    //

    void audio15_method()
    {
        myAudio.clip = audio15;
        myAudio.Play();
    }

    //

    void audio16_method()
    {
        myAudio.clip = audio16;
        myAudio.Play();
    }

    //

    void audio17_method()
    {
        myAudio.clip = audio17;
        myAudio.Play();
    }

    //

    void audio18_method()
    {
        myAudio.clip = audio18;
        myAudio.Play();
    }

    //

    void audio19_method()
    {
        myAudio.clip = audio19;
        myAudio.Play();
    }

    //

    void audio20_method()
    {
        myAudio.clip = audio20;
        myAudio.Play();
    }

    //

    void audio21_method()
    {
        myAudio.clip = audio21;
        myAudio.Play();
    }

    //

    void audio22_method()
    {
        myAudio.clip = audio22;
        myAudio.Play();
    }

    //

    void audio23_method()
    {
        myAudio.clip = audio23;
        myAudio.Play();
    }

    //

    void audio24_method()
    {
        myAudio.clip = audio24;
        myAudio.Play();
    }

    //

    void audio25_method()
    {
        myAudio.clip = audio25;
        myAudio.Play();
    }

    //

    void audio26_method()
    {
        myAudio.clip = audio26;
        myAudio.Play();
    }






    //Animation Play

    void batteranim_anim_Method()
    {
        anim = batteranim.GetComponent<Animator>();
        anim.Play("BATTERY ANIM");
    }






    // ONOFF







    void _tester_MethodON()
    {
        tester.SetActive(true);
    }

    void _tester_MethodOFF()
    {
        tester.SetActive(false);
    }



    void _gold_silver_MethodON()
    {
        gold_silver.SetActive(true);
    }

    void _gold_silver_MethodOFF()
    {
        gold_silver.SetActive(false);
    }




    void _oxygen_MethodON()
    {
        oxygen.SetActive(true);
    }

    void _oxygen_MethodOFF()
    {
        oxygen.SetActive(false);
    }




    void _hydrogen_MethodON()
    {
        hydrogen.SetActive(true);
    }

    void _hydrogen_MethodOFF()
    {
        hydrogen.SetActive(false);
    }



    void _steel_cathode_MethodON()
    {
        steel_cathode.SetActive(true);
    }

    void _steel_cathode_MethodOFF()
    {
        steel_cathode.SetActive(false);
    }


    void _silver_anode_MethodON()
    {
        silver_anode.SetActive(true);
    }

    void _silver_anode_MethodOFF()
    {
        silver_anode.SetActive(false);
    }

    void _IntroductionT_MethodON()
    {
        IntroductionT.SetActive(true);
    }

    void _IntroductionT_MethodOFF()
    {
        IntroductionT.SetActive(false);
    }
    void _GoodT_MethodON()
    {
        GoodT.SetActive(true);
    }

    void _GoodT_MethodOFF()
    {
        GoodT.SetActive(false);
    }

    void _do_liqT_MethodON()
    {
        do_liqT.SetActive(true);
    }

    void _do_liqT_MethodOFF()
    {
        do_liqT.SetActive(false);
    }

    void _CEET_MethodON()
    {
        CEET.SetActive(true);
    }

    void _CEET_MethodOFF()
    {
        CEET.SetActive(false);
    }

    void _ElectroplatingT_MethodON()
    {
        ElectroplatingT.SetActive(true);
    }

    void _ElectroplatingT_MethodOFF()
    {
        ElectroplatingT.SetActive(false);
    }

    void _Use_of_chemicalT_MethodON()
    {
        Use_of_chemicalT.SetActive(true);
    }

    void _Use_of_chemicalT_MethodOFF()
    {
        Use_of_chemicalT.SetActive(false);
    }

    void _Chemical_effects_MethodON()
    {
        Chemical_effects.SetActive(true);
    }

    void _Chemical_effects_MethodOFF()
    {
        Chemical_effects.SetActive(false);
    }

    void _electrolysisD_MethodON()
    {
        electrolysisD.SetActive(true);
    }

    void _electrolysisD_MethodOFF()
    {
        electrolysisD.SetActive(false);
    }

    void _electroplating_widelyD_MethodON()
    {
        electroplating_widelyD.SetActive(true);
    }

    void _electroplating_widelyD_MethodOFF()
    {
        electroplating_widelyD.SetActive(false);
    }

    void _coating_of_zinc_MethodON()
    {
        coating_of_zinc.SetActive(true);
    }

    void _coating_of_zinc_MethodOFF()
    {
        coating_of_zinc.SetActive(false);
    }

    void _electrolysis_MethodON()
    {
        electrolysis.SetActive(true);
    }

    void _electrolysis_MethodOFF()
    {
        electrolysis.SetActive(false);
    }
































}
