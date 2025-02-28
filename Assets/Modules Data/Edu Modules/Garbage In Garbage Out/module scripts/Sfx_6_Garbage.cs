using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx_6_Garbage : MonoBehaviour
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


    // ON - OFF gameobjects
    [Header("Explanation Assets")]

    public GameObject Truck;
    public GameObject Dcar;
    public GameObject Twotypes;
    public GameObject Vegpeels;
    public GameObject DumpsterH1;
    public GameObject Vermicompost;
    public GameObject Earthworm;
    public GameObject Stirrer;
    public GameObject RunningSewage;
    public GameObject PlasticNo;
    public GameObject Reuse;
    public GameObject GarbageNo;
    public GameObject PaperBag;
    public GameObject jutebag;
    public GameObject Food;
    public GameObject No2;
    public GameObject No3;
    public GameObject No4;
    public GameObject Wooden;
    public GameObject RedwormShow;
    public GameObject Non;









    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("Camera animation", 0, targetNormalizedTime);
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




















    //Titles


    public GameObject T1;
    public GameObject T2;
    public GameObject T3;
    public GameObject T4;
    public GameObject T5;
    public GameObject T6;
    public GameObject T7;
    public GameObject T8;
    public GameObject D1;
    public GameObject D2;
    public GameObject D3;
    public GameObject D4;
    public GameObject D5;
    public GameObject D6;
    public GameObject D7;
    public GameObject D8;
    public GameObject D9;
    public GameObject D10;
    public GameObject D11;
    public GameObject D12;
    public GameObject D13;
    public GameObject D14;
    public GameObject D15;
    public GameObject D16;
    public GameObject DP1;
    public GameObject DP2;
    public GameObject DP3;
    public GameObject DP4;
    public GameObject DP5;
    public GameObject DP6;
    public GameObject DP7;
    public GameObject DP8;

    
















    // Exp - Animations

    private Animator anim;

    [Header("Explanation anims")]



    public GameObject truck_anim;
    public GameObject stir_anim;
    public GameObject sewage_anim;
    public GameObject wooden_anim;


























    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip a1;
    public AudioClip a2;
    public AudioClip a3;
    public AudioClip a4;
    public AudioClip a5;
    public AudioClip a6;
    public AudioClip a7;
    public AudioClip a8;
    public AudioClip a9;
    public AudioClip a10;
    public AudioClip a11;
    public AudioClip a12;
    public AudioClip a13;
    public AudioClip a14;
    public AudioClip a15;
    public AudioClip a16;
    public AudioClip a17;
    public AudioClip a18;
    public AudioClip a19;
    public AudioClip a20;
    public AudioClip a21;
    public AudioClip a22;
    public AudioClip a23;
    public AudioClip a24;
    public AudioClip a25;
    public AudioClip a26;
    public AudioClip a27;
    public AudioClip a28;
    public AudioClip a29;
    public AudioClip a30;
    public AudioClip a31;
    public AudioClip a32;
    public AudioClip a33;
    public AudioClip a34;
    public AudioClip a35;
    public AudioClip a36;
    public AudioClip a37;
    public AudioClip a38;
    public AudioClip a39;
    public AudioClip a40;
    public AudioClip a41;
    public AudioClip a42;
    public AudioClip a43;
    public AudioClip a44;
    public AudioClip a45;
    public AudioClip a46;
    public AudioClip a47;
    public AudioClip a48;
    public AudioClip a49;
    public AudioClip a50;
    public AudioClip a51;
    public AudioClip a52;
    public AudioClip a53;
    public AudioClip a54;
    public AudioClip a55;
    public AudioClip a56;
    public AudioClip a57;
    public AudioClip a58;
    public AudioClip a59;
    public AudioClip a60;
    public AudioClip a61;
    public AudioClip a62;
    public AudioClip a63;
    public AudioClip a64;
    public AudioClip a65;
    public AudioClip a66;



    void _Non_MethodON()
    {
        Non.SetActive(true);
    }
    void _Non_MethodoOFF()
    {
        Non.SetActive(false);
    }
    //
    void _RedwormShow_MethodON()
    {
        RedwormShow.SetActive(true);
    }
    void _RedwormShow_MethodoOFF()
    {
        RedwormShow.SetActive(false);
    }
    //
    void _Wooden_MethodON()
    {
        Wooden.SetActive(true);
    }
    void _Wooden_MethodoOFF()
    {
        Wooden.SetActive(false);
    }
    //
    void _No4_MethodON()
    {
        No4.SetActive(true);
    }
    void _No4_MethodoOFF()
    {
        No4.SetActive(false);
    }
    //
    void _No3_MethodON()
    {
        No3.SetActive(true);
    }
    void _No3_MethodoOFF()
    {
        No3.SetActive(false);
    }
    //
    void _No2_MethodON()
    {
        No2.SetActive(true);
    }
    void _No2_MethodoOFF()
    {
        No2.SetActive(false);
    }
    //
    void _Food_MethodON()
    {
        Food.SetActive(true);
    }
    void _Food_MethodoOFF()
    {
        Food.SetActive(false);
    }
    //
    void _jutebag_MethodON()
    {
        jutebag.SetActive(true);
    }
    void _jutebag_MethodoOFF()
    {
        jutebag.SetActive(false);
    }
    //
    void _PaperBag_MethodON()
    {
        PaperBag.SetActive(true);
    }
    void _PaperBag_MethodoOFF()
    {
        PaperBag.SetActive(false);
    }
    
    //
    void _Truck_MethodON()
    {
        Truck.SetActive(true);
    }
    void _Truck_MethodoOFF()
    {
        Truck.SetActive(false);
    }
    //
    void _Dcar_MethodON()
    {
        Dcar.SetActive(true);
    }
    void _Dcar_MethodoOFF()
    {
        Dcar.SetActive(false);
    }
    //
    void _2Types_MethodON()
    {
        Twotypes.SetActive(true);
    }
    void _2Types_MethodoOFF()
    {
        Twotypes.SetActive(false);
    }
    //
    void _Vegpeels_MethodON()
    {
        Vegpeels.SetActive(true);
    }
    void _Vegpeels_MethodoOFF()
    {
        Vegpeels.SetActive(false);
    }
    //
    void _DumpsterH1_MethodON()
    {
        DumpsterH1.SetActive(true);
    }
    void _DumpsterH1_MethodoOFF()
    {
        DumpsterH1.SetActive(false);
    }
    //
    void _Vermicompost_MethodON()
    {
        Vermicompost.SetActive(true);
    }
    void _Vermicompost_MethodoOFF()
    {
        Vermicompost.SetActive(false);
    }
    //
    void _Earthworm_MethodON()
    {
        Earthworm.SetActive(true);
    }
    void _Earthworm_MethodoOFF()
    {
        Earthworm.SetActive(false);
    }
    //
    void _Stirrer_MethodON()
    {
        Stirrer.SetActive(true);
    }
    void _Stirrer_MethodoOFF()
    {
        Stirrer.SetActive(false);
    }
    //
    void _RunningSewage_MethodON()
    {
        RunningSewage.SetActive(true);
    }
    void _RunningSewage_MethodoOFF()
    {
        RunningSewage.SetActive(false);
    }
    //
    void _PlasticNo_MethodON()
    {
        PlasticNo.SetActive(true);
    }
    void _PlasticNo_MethodoOFF()
    {
        PlasticNo.SetActive(false);
    }
    //
    void _Reuse_MethodON()
    {
        Reuse.SetActive(true);
    }
    void _Reuse_MethodoOFF()
    {
        Reuse.SetActive(false);
    }
    //
    void _GarbageNo_MethodON()
    {
        GarbageNo.SetActive(true);
    }
    void _GarbageNo_MethodoOFF()
    {
        GarbageNo.SetActive(false);
    }






































    //Animations





    void _Truck_animationAnimmethod()
    {

        anim = truck_anim.GetComponent<Animator>();
        anim.Play("Truck dump");
    }

    void _Stirrer_animationAnimmethod()
    {

        anim = stir_anim.GetComponent<Animator>();
        anim.Play("Stirrer aimation");
    }

    void _RunningSewage_animationAnimmethod()
    {

        anim = sewage_anim.GetComponent<Animator>();
        anim.Play("Running Sewage aniamation");
    }
    void _Wooden_animationAnimmethod()
    {

        anim = wooden_anim.GetComponent<Animator>();
        anim.Play("Sieve animation");
    }






































    //Titles







    //
    void _DP1_MethodON()
    {
        DP1.SetActive(true);
    }
    void _DP1_MethodoOFF()
    {
        DP1.SetActive(false);
    }
    //
    void _DP2_MethodON()
    {
        DP2.SetActive(true);
    }
    void _DP2_MethodoOFF()
    {
        DP2.SetActive(false);
    }
    //
    void _DP3_MethodON()
    {
        DP3.SetActive(true);
    }
    void _DP3_MethodoOFF()
    {
        DP3.SetActive(false);
    }
    //
    void _DP4_MethodON()
    {
        DP4.SetActive(true);
    }
    void _DP4_MethodoOFF()
    {
        DP4.SetActive(false);
    }
    //
    void _DP5_MethodON()
    {
        DP5.SetActive(true);
    }
    void _DP5_MethodoOFF()
    {
        DP5.SetActive(false);
    }
    //
    void _DP6_MethodON()
    {
        DP6.SetActive(true);
    }
    void _DP6_MethodoOFF()
    {
        DP6.SetActive(false);
    }
    //
    void _DP7_MethodON()
    {
        DP7.SetActive(true);
    }
    void _DP7_MethodoOFF()
    {
        DP7.SetActive(false);
    }
    //
    void _DP8_MethodON()
    {
        DP8.SetActive(true);
    }
    void _DP8_MethodoOFF()
    {
        DP8.SetActive(false);
    }



    //
    void _T1_MethodON()
    {
        T1.SetActive(true);
    }
    void _T1_MethodoOFF()
    {
        T1.SetActive(false);
    }
    //
    void _T2_MethodON()
    {
        T2.SetActive(true);
    }
    void _T2_MethodoOFF()
    {
        T2.SetActive(false);
    }
    //
    void _T3_MethodON()
    {
        T3.SetActive(true);
    }
    void _T3_MethodoOFF()
    {
        T3.SetActive(false);
    }
    //
    void _T4_MethodON()
    {
        T4.SetActive(true);
    }
    void _T4_MethodoOFF()
    {
        T4.SetActive(false);
    }
    //
    void _T5_MethodON()
    {
        T5.SetActive(true);
    }
    void _T5_MethodoOFF()
    {
        T5.SetActive(false);
    }
    //
    void _T6_MethodON()
    {
        T6.SetActive(true);
    }
    void _T6_MethodoOFF()
    {
        T6.SetActive(false);
    }
    //
    void _T7_MethodON()
    {
        T7.SetActive(true);
    }
    void _T7_MethodoOFF()
    {
        T7.SetActive(false);
    }
    //
    void _T8_MethodON()
    {
        T8.SetActive(true);
    }
    void _T8_MethodoOFF()
    {
        T8.SetActive(false);
    }
    







    //
    void _D1_MethodON()
    {
        D1.SetActive(true);
    }
    void _D1_MethodoOFF()
    {
        D1.SetActive(false);
    }
    //
    void _D2_MethodON()
    {
        D2.SetActive(true);
    }
    void _D2_MethodoOFF()
    {
        D2.SetActive(false);
    }
    //
    void _D3_MethodON()
    {
        D3.SetActive(true);
    }
    void _D3_MethodoOFF()
    {
        D3.SetActive(false);
    }
    //
    void _D4_MethodON()
    {
        D4.SetActive(true);
    }
    void _D4_MethodoOFF()
    {
        D4.SetActive(false);
    }
    //
    void _D5_MethodON()
    {
        D5.SetActive(true);
    }
    void _D5_MethodoOFF()
    {
        D5.SetActive(false);
    }
    //
    void _D6_MethodON()
    {
        D6.SetActive(true);
    }
    void _D6_MethodoOFF()
    {
        D6.SetActive(false);
    }
    //
    void _D7_MethodON()
    {
        D7.SetActive(true);
    }
    void _D7_MethodoOFF()
    {
        D7.SetActive(false);
    }
    //
    void _D8_MethodON()
    {
        D8.SetActive(true);
    }
    void _D8_MethodoOFF()
    {
        D8.SetActive(false);
    }
    //
    void _D9_MethodON()
    {
        D9.SetActive(true);
    }
    void _D9_MethodoOFF()
    {
        D9.SetActive(false);
    }
    //
    void _D10_MethodON()
    {
        D10.SetActive(true);
    }
    void _D10_MethodoOFF()
    {
        D10.SetActive(false);
    }
    //
    void _D11_MethodON()
    {
        D11.SetActive(true);
    }
    void _D11_MethodoOFF()
    {
        D11.SetActive(false);
    }
    //
    void _D12_MethodON()
    {
        D12.SetActive(true);
    }
    void _D12_MethodoOFF()
    {
        D12.SetActive(false);
    }
    //
    void _D13_MethodON()
    {
        D13.SetActive(true);
    }
    void _D13_MethodoOFF()
    {
        D13.SetActive(false);
    }
    //
    void _D14_MethodON()
    {
        D14.SetActive(true);
    }
    void _D14_MethodoOFF()
    {
        D14.SetActive(false);
    }
    //
    void _D15_MethodON()
    {
        D15.SetActive(true);
    }
    void _D15_MethodoOFF()
    {
        D15.SetActive(false);
    }
    //
    void _D16_MethodON()
    {
        D16.SetActive(true);
    }
    void _D16_MethodoOFF()
    {
        D16.SetActive(false);
    }


    
































































// Audio


    void _P2A1_audioMethod()

    {
        myAudio.clip = a1;
        myAudio.Play();
    }
    void _P2A2_audioMethod()

    {
        myAudio.clip = a2;
        myAudio.Play();
    }
    void _P2A3_audioMethod()

    {
        myAudio.clip = a3;
        myAudio.Play();
    }
    void _P2A4_audioMethod()

    {
        myAudio.clip = a4;
        myAudio.Play();
    }
    void _P2A5_audioMethod()

    {
        myAudio.clip = a5;
        myAudio.Play();
    }
    void _P2A6_audioMethod()

    {
        myAudio.clip = a6;
        myAudio.Play();
    }
    void _P2A7_audioMethod()

    {
        myAudio.clip = a7;
        myAudio.Play();
    }
    void _P2A8_audioMethod()

    {
        myAudio.clip = a8;
        myAudio.Play();
    }
    void _P2A9_audioMethod()

    {
        myAudio.clip = a9;
        myAudio.Play();
    }
    void _P2A10_audioMethod()

    {
        myAudio.clip = a10;
        myAudio.Play();
    }
    void _P2A11_audioMethod()

    {
        myAudio.clip = a11;
        myAudio.Play();
    }
    void _P2A12_audioMethod()

    {
        myAudio.clip = a12;
        myAudio.Play();
    }
    void _P2A13_audioMethod()

    {
        myAudio.clip = a13;
        myAudio.Play();
    }
    void _P2A14_audioMethod()

    {
        myAudio.clip = a14;
        myAudio.Play();
    }
    void _P2A15_audioMethod()

    {
        myAudio.clip = a15;
        myAudio.Play();
    }
    void _P2A16_audioMethod()

    {
        myAudio.clip = a16;
        myAudio.Play();
    }
    void _P2A17_audioMethod()

    {
        myAudio.clip = a17;
        myAudio.Play();
    }
    void _P2A18_audioMethod()

    {
        myAudio.clip = a18;
        myAudio.Play();
    }
    void _P2A19_audioMethod()

    {
        myAudio.clip = a19;
        myAudio.Play();
    }
    void _P2A20_audioMethod()

    {
        myAudio.clip = a20;
        myAudio.Play();
    }
    void _P2A21_audioMethod()

    {
        myAudio.clip = a21;
        myAudio.Play();
    }
    void _P2A22_audioMethod()

    {
        myAudio.clip = a22;
        myAudio.Play();
    }
    void _P2A23_audioMethod()

    {
        myAudio.clip = a23;
        myAudio.Play();
    }
    void _P2A24_audioMethod()

    {
        myAudio.clip = a24;
        myAudio.Play();
    }
    void _P2A25_audioMethod()

    {
        myAudio.clip = a25;
        myAudio.Play();
    }
    void _P2A26_audioMethod()

    {
        myAudio.clip = a26;
        myAudio.Play();
    }
    void _P2A27_audioMethod()

    {
        myAudio.clip = a27;
        myAudio.Play();
    }
    void _P2A28_audioMethod()

    {
        myAudio.clip = a28;
        myAudio.Play();
    }
    void _P2A29_audioMethod()

    {
        myAudio.clip = a29;
        myAudio.Play();
    }
    void _P2A30_audioMethod()

    {
        myAudio.clip = a30;
        myAudio.Play();
    }
    void _P2A31_audioMethod()

    {
        myAudio.clip = a31;
        myAudio.Play();
    }
    void _P2A32_audioMethod()

    {
        myAudio.clip = a32;
        myAudio.Play();
    }
    void _P2A33_audioMethod()

    {
        myAudio.clip = a33;
        myAudio.Play();
    }
    void _P2A34_audioMethod()

    {
        myAudio.clip = a34;
        myAudio.Play();
    }
    void _P2A35_audioMethod()

    {
        myAudio.clip = a35;
        myAudio.Play();
    }
    void _P2A36_audioMethod()

    {
        myAudio.clip = a36;
        myAudio.Play();
    }
    void _P2A37_audioMethod()

    {
        myAudio.clip = a37;
        myAudio.Play();
    }
    void _P2A38_audioMethod()

    {
        myAudio.clip = a38;
        myAudio.Play();
    }
    void _P2A39_audioMethod()

    {
        myAudio.clip = a39;
        myAudio.Play();
    }
    void _P2A40_audioMethod()

    {
        myAudio.clip = a40;
        myAudio.Play();
    }
    void _P2A41_audioMethod()

    {
        myAudio.clip = a41;
        myAudio.Play();
    }
    void _P2A42_audioMethod()

    {
        myAudio.clip = a42;
        myAudio.Play();
    }
    void _P2A43_audioMethod()

    {
        myAudio.clip = a43;
        myAudio.Play();
    }
    void _P2A44_audioMethod()

    {
        myAudio.clip = a44;
        myAudio.Play();
    }
    void _P2A45_audioMethod()

    {
        myAudio.clip = a45;
        myAudio.Play();
    }
    void _P2A46_audioMethod()

    {
        myAudio.clip = a46;
        myAudio.Play();
    }
    void _P2A47_audioMethod()

    {
        myAudio.clip = a47;
        myAudio.Play();
    }
    void _P2A48_audioMethod()

    {
        myAudio.clip = a48;
        myAudio.Play();
    }
    void _P2A49_audioMethod()

    {
        myAudio.clip = a49;
        myAudio.Play();
    }
    void _P2A50_audioMethod()

    {
        myAudio.clip = a50;
        myAudio.Play();
    }
    void _P2A51_audioMethod()

    {
        myAudio.clip = a51;
        myAudio.Play();
    }
    void _P2A52_audioMethod()

    {
        myAudio.clip = a52;
        myAudio.Play();
    }
    void _P2A53_audioMethod()

    {
        myAudio.clip = a53;
        myAudio.Play();
    }
    void _P2A54_audioMethod()

    {
        myAudio.clip = a54;
        myAudio.Play();
    }
    void _P2A55_audioMethod()

    {
        myAudio.clip = a55;
        myAudio.Play();
    }
    void _P2A56_audioMethod()

    {
        myAudio.clip = a56;
        myAudio.Play();
    }
    void _P2A57_audioMethod()

    {
        myAudio.clip = a57;
        myAudio.Play();
    }
    void _P2A58_audioMethod()

    {
        myAudio.clip = a58;
        myAudio.Play();
    }
    void _P2A59_audioMethod()

    {
        myAudio.clip = a59;
        myAudio.Play();
    }
    void _P2A60_audioMethod()

    {
        myAudio.clip = a60;
        myAudio.Play();
    }
    void _P2A61_audioMethod()

    {
        myAudio.clip = a61;
        myAudio.Play();
    }
    void _P2A62_audioMethod()

    {
        myAudio.clip = a62;
        myAudio.Play();
    }
    void _P2A63_audioMethod()

    {
        myAudio.clip = a63;
        myAudio.Play();
    }
    void _P2A64_audioMethod()

    {
        myAudio.clip = a64;
        myAudio.Play();
    }
    void _P2A65_audioMethod()

    {
        myAudio.clip = a65;
        myAudio.Play();
    }
    void _P2A66_audioMethod()

    {
        myAudio.clip = a66;
        myAudio.Play();
    }




    void _Goto_menuMethodON()
    {
        //open initial city scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(0);
        //SceneManager.LoadScene("Miniworld"); 
    }

    public void StepComplete()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }

    private int nonBioDeg;
    public TargetController level1MiniGame;
    public GameObject dragStage;
    public GameObject pickupStage;
    public void DropOnDustbin(GameObject obj)
    {
        nonBioDeg++;
        Destroy(obj);
        if(nonBioDeg == 4)
        {
            level1MiniGame.EndMiniGame();
            dragStage.SetActive(false);
            pickupStage.SetActive(true);
            nonBioDeg = 0;
        }
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

    public void Degrading()
    {
        nonBioDeg++;
        if (nonBioDeg == 4)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void level3game()
    {
        currentIndex++;
        if (currentIndex == 3)
        {
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
            level3MiniGame.EndMiniGame();
            level3Minigame = false;
        }
    }

    public Camera cam;
    private bool level3Minigame;
    public LayerMask layerMask;
    public int currentIndex;
    public TargetController level3MiniGame;
    private bool collectedBioDegradable;
    private int bioDeg;

    public void LevelMiniGame()
    {
        level3Minigame = true;
    }

    private void Update()
    {
        if (level3Minigame)
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

                            if (currentIndex == 3)
                            {
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                                level3MiniGame.EndMiniGame();
                                level3Minigame = false;
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
    }

    private int ind = 0;
    public TargetController lv1MiniGame;
    public void Lv1MiniGame(GameObject colloider)
    {
        colloider.tag = "Finish";
        ind++;

        if(ind == 3)
        {

            lv1MiniGame.EndMiniGame();
            StepComplete();
        }
    }

    public void MissionFailed()
    {
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().missionFailed();
    }


































































































































}
