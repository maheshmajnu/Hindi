using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_aau_class6 : MonoBehaviour
{
    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public TargetController miniGame1;

    private GameObject JustInstantiatedNoPlayerCanvas;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("camera_anim", 0, targetNormalizedTime);
            targetNormalizedTime = -1f; // Reset after use
        }
        animator = GetComponent<Animator>();

        //==================== INSERT NO-PLAYER Menu  ====================//
        GameObject SelectedPrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/NotDefaultPlayer_Menu", typeof(GameObject));  // Load No-Player Menu
        JustInstantiatedNoPlayerCanvas = (GameObject)GameObject.Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate No-Player Menu   {  Instantiate(SelectedPrefabObj, new Vector3(0, 0, 0), Quaternion.identity);   }
        JustInstantiatedNoPlayerCanvas.SetActive(false);
        //================================================================//
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
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        JustInstantiatedNoPlayerCanvas.SetActive(true);

        miniGame1.Output();
    }

    public List<GameObject> questions = new List<GameObject>();

    public void TurnOnGOWithDelay(GameObject obj)
    {
        StartCoroutine(ObjectTurnOnDelay(obj));
    }

    IEnumerator ObjectTurnOnDelay(GameObject obj)
    {
        foreach (GameObject objec in questions)
        {
            objec.SetActive(false);
        }
        yield return new WaitForSeconds(2);
        obj.SetActive(true);
    }

    public void ChangeCamHolder(Transform camHolder)
    {
        Debug.Log("Changing cam holder");
        this.transform.position = camHolder.position;
        this.transform.rotation = camHolder.rotation;
    }

    public void ChangeCamHolderWithDelay(Transform camHolder)
    {
        StartCoroutine(ChangeCamHolderDelay(camHolder));
    }

    IEnumerator ChangeCamHolderDelay(Transform camHolder)
    {
        yield return new WaitForSeconds(0.5f);
        this.transform.position = camHolder.position;
        this.transform.rotation = camHolder.rotation;
    }

    private int index = 0;
    public void MultiSelectAnswer(int count)
    {
        index++;

        if (index == count)
        {
            StepComplete();
        }
    }

    public void PlayAnimTrigg(Animator anim)
    {
        anim.SetTrigger("Trigger"); // Replace "TriggerName" with the actual name of your trigger
    }

    private int currentIndex = 0;
    public LayerMask layerMask;
    public Camera cam;
    public List<string> answers = new List<string>();
    public bool bubbleLevel;

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
                    Debug.Log(raycastHit.collider.gameObject.name);
                    if (raycastHit.collider.gameObject.name == "Correct")
                    {
                        currentIndex++;
                        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();

                        if(bubbleLevel)
                        {
                            ind++;

                            if(ind == 3)
                            {
                                ind = 0;
                                StepComplete();
                                ChangeCamHolder(camHolderLv3);
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                            }
                        }

                    }
                    else
                    {
                        MissionFailed();
                    }
                }
            }
        }
    }

    public void StartBubble()
    {
        bubbleLevel = true;
    }

    public int ind = 0;
    public void Lv2Bubble()
    {
        ind++;
        Debug.Log("Bubble - " + ind);
        if (ind == 3)
        {
           
        }
    }

    public Transform camHolderLv3;
    public Transform camHolderLv4;
    public Animator bubbleAnim;
    public void PieChart()
    {
        ind++;

        if (ind == 4)
        {
            ind = 0;
            ChangeCamHolder(camHolderLv4);
            PlayAnimTrigg(bubbleAnim);
            StepComplete();
        }
    }

    public bool fireStarted = false;
    public void Co2Pickup()
    {
        if (fireStarted)
        {
            StepComplete();
        }
        else
        {
            MissionFailed();
        }
    }

    public void Lv5PickUp()
    {
        ind++;

        if (ind == 5)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            fireStarted = true;
        }
    }

    public void Lv5FireStart(GameObject fire)
    {
        StartCoroutine(Lv5FireStartDelay(fire));
    }

    IEnumerator Lv5FireStartDelay(GameObject fire)
    {
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeInAndOut();
        yield return new WaitForSeconds(1);
        fire.SetActive(true);
    }

    public void Co2PickUp(GameObject fire)
    {
        StepComplete();
        fire.SetActive(false);
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

    public void StepComplete()
    {
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
    public GameObject title_13;
    public GameObject jar;
    public GameObject p1;
    public GameObject p2;
    public GameObject candle_fire;
    public GameObject rain;
    public GameObject fire;
    public GameObject fog;
    public GameObject dust;


    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    

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










    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]


    public GameObject candle_exp;

    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip a_1;
    public AudioClip a_2;
    public AudioClip a_3;
    public AudioClip a_4;
    public AudioClip a_5;
    public AudioClip a_6;
    public AudioClip a_7;
    public AudioClip a_8;
    public AudioClip a_9;
    public AudioClip a_10;
    public AudioClip a_11;
    public AudioClip a_12;
    public AudioClip a_13;
    public AudioClip a_14;
    public AudioClip a_15;
    public AudioClip a_16;
    public AudioClip a_17;
    public AudioClip a_18;
    public AudioClip a_19;
    public AudioClip a_20;
    public AudioClip a_21;
    public AudioClip a_22;
    public AudioClip a_23;
    public AudioClip a_24;
    public AudioClip a_25;
    public AudioClip a_26;
    public AudioClip a_27;

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
    void title_13_MethodON()
    {
        title_13.SetActive(true);
    }
    //

    void dust_MethodON()
    {
        dust.SetActive(true);
    }

    void dust_MethodOFF()
    {
        dust.SetActive(false);
    }



    void jar_MethodON()
    {
        jar.SetActive(true);
    }

    void jar_MethodOFF()
    {
        jar.SetActive(false);
    }
    //

    void p1_MethodOFF()
    {
        p1.SetActive(false);
    }
    //
    void p2_MethodON()
    {
        p2.SetActive(true);
    }

    void p2_MethodOFF()
    {
        p2.SetActive(false);
    }
    //
    void candle_exp_MethodON()
    {
        candle_exp.SetActive(true);
    }

    void candle_exp_MethodOFF()
    {
        candle_exp.SetActive(false);
    }
    //
    void candle_fire_MethodON()
    {
        candle_fire.SetActive(true);
    }

    void candle_fire_MethodOFF()
    {
        candle_fire.SetActive(false);
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
    void fire_MethodON()
    {
        fire.SetActive(true);
    }

    void fire_MethodOFF()
    {
        fire.SetActive(false);
    }
    //
    void fog_MethodON()
    {
        fog.SetActive(true);
    }

    void fog_MethodOFF()
    {
        fog.SetActive(false);
    }
    //




    //
    void _candle_exp_Animmethod()
    {

        anim = candle_exp.GetComponent<Animator>();
        anim.Play("candle_exp_anim");
    }
    //




    //
    void _a_1_audioMethod()

    {
        myAudio.clip = a_1;
        myAudio.Play();
    }
    //
    void _a_2_audioMethod()

    {
        myAudio.clip = a_2;
        myAudio.Play();
    }
    //
    void _a_3_audioMethod()

    {
        myAudio.clip = a_3;
        myAudio.Play();
    }
    //
    void _a_4_audioMethod()

    {
        myAudio.clip = a_4;
        myAudio.Play();
    }
    //
    void _a_5_audioMethod()

    {
        myAudio.clip = a_5;
        myAudio.Play();
    }
    //
    void _a_6_audioMethod()

    {
        myAudio.clip = a_6;
        myAudio.Play();
    }
    //
    void _a_7_audioMethod()

    {
        myAudio.clip = a_7;
        myAudio.Play();
    }
    //
    void _a_8_audioMethod()

    {
        myAudio.clip = a_8;
        myAudio.Play();
    }
    //
    void _a_9_audioMethod()

    {
        myAudio.clip = a_9;
        myAudio.Play();
    }
    //
    void _a_10_audioMethod()

    {
        myAudio.clip = a_10;
        myAudio.Play();
    }
    //
    void _a_11_audioMethod()

    {
        myAudio.clip = a_11;
        myAudio.Play();
    }
    //
    void _a_12_audioMethod()

    {
        myAudio.clip = a_12;
        myAudio.Play();
    }
    //
    void _a_13_audioMethod()

    {
        myAudio.clip = a_13;
        myAudio.Play();
    }
    //
    void _a_14_audioMethod()

    {
        myAudio.clip = a_14;
        myAudio.Play();
    }
    //
    void _a_15_audioMethod()

    {
        myAudio.clip = a_15;
        myAudio.Play();
    }
    //
    void _a_16_audioMethod()

    {
        myAudio.clip = a_16;
        myAudio.Play();
    }
    //
    void _a_17_audioMethod()

    {
        myAudio.clip = a_17;
        myAudio.Play();
    }
    //
    void _a_18_audioMethod()

    {
        myAudio.clip = a_18;
        myAudio.Play();
    }
    //
    void _a_19_audioMethod()

    {
        myAudio.clip = a_19;
        myAudio.Play();
    }
    //
    void _a_20_audioMethod()

    {
        myAudio.clip = a_20;
        myAudio.Play();
    }
    //
    void _a_21_audioMethod()

    {
        myAudio.clip = a_21;
        myAudio.Play();
    }
    //
    void _a_22_audioMethod()

    {
        myAudio.clip = a_22;
        myAudio.Play();
    }
    //
    void _a_23_audioMethod()

    {
        myAudio.clip = a_23;
        myAudio.Play();
    }
    //
    void _a_24_audioMethod()

    {
        myAudio.clip = a_24;
        myAudio.Play();
    }
    //
    void _a_25_audioMethod()

    {
        myAudio.clip = a_25;
        myAudio.Play();
    }
    //
    void _a_26_audioMethod()

    {
        myAudio.clip = a_26;
        myAudio.Play();
    }
    //
    void _a_27_audioMethod()

    {
        myAudio.clip = a_27;
        myAudio.Play();
    }
    //




}
