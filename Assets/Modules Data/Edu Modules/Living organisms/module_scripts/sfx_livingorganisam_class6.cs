using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_livingorganisam_class6 : MonoBehaviour
{


    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    public Animator anim;

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

    // Titles



    public GameObject habitatT;
    public GameObject habitatTypesT;
    public GameObject terrestrialHabitatT;
    public GameObject aquaticHabitatT;
    public GameObject adaptationT;
    public GameObject exploringdesertT;
    public GameObject descoveringmountainsT;
    public GameObject exploringaquatichabitataT;
    public GameObject adaptationsinfrogT;











    public GameObject habitatD;
    public GameObject terrestrialD;
    public GameObject AquatichabitatD;

    public GameObject adapationD;



    private Animator animator;

// Static variable to store the normalized time across scene reloads
private static float targetNormalizedTime = -1f;

private void Awake()
{
    animator = GetComponent<Animator>();

    // If there is a target time stored, jump to that animation keyframe
    if (targetNormalizedTime >= 0f)
    {
        animator.Play("camera animation", 0, targetNormalizedTime);
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












    // ON - OFF gameobjects
    [Header("Explanation Assets")]


    



    public GameObject yak;
    public GameObject porcupine;
    public GameObject plain;
    public GameObject frog;
    public GameObject mountaingoat;
    public GameObject plain1;






































    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip habitat;
    public AudioClip types;
    public AudioClip terrestrial;
    public AudioClip aquatic;
    public AudioClip adaptation;
    public AudioClip exploringdesert;
    public AudioClip para1;
    public AudioClip para2;
    

    public AudioClip discoveringmountains;
    public AudioClip paradm2;
    public AudioClip paradm3;
    public AudioClip paradm4;
    public AudioClip paradm5;
    public AudioClip paradm6;





    public AudioClip exploringaquatichabitat;
    public AudioClip adaptationinfrog;











    





















    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    // Titles


    void _HabitatTMethodON()
    {
        habitatT.SetActive(true);
    }

    void _HabitatTMethodOFF()
    {
        habitatT.SetActive(false);
    }



    void _habitatTypesTMethodON()
    {
        habitatTypesT.SetActive(true);
    }

    void _HabitatTypesTMethodOFF()
    {
        habitatTypesT.SetActive(false);
    }




    void _terrestrialHabitatTMethodON()
    {
        terrestrialHabitatT.SetActive(true);
    }

    void _terrestrialHabitatTMethodOFF()
    {
        terrestrialHabitatT.SetActive(false);
    }




    void _aquaticHabitatTMethodON()
    {
        aquaticHabitatT.SetActive(true);
    }

    void _aquaticHabitatTMethodOFF()
    {
        aquaticHabitatT.SetActive(false);
    }





    void _adaptationTMethodON()
    {
        adaptationT.SetActive(true);
    }

    void _adaptationTMethodOFF()
    {
        adaptationT.SetActive(false);
    }




    void _exploringdesertTMethodON()
    {
        exploringdesertT.SetActive(true);
    }

    void _exploringdesertTMethodOFF()
    {
        exploringdesertT.SetActive(false);
    }




    void _descoveringmountainsTMethodON()
    {
        descoveringmountainsT.SetActive(true);
    }

    void _descoveringmountainsTMethodOFF()
    {
        descoveringmountainsT.SetActive(false);
    }






    void _exploringaquatichabitataTMethodON()
    {
        exploringaquatichabitataT.SetActive(true);
    }

    void _exploringaquatichabitataTMethodOFF()
    {
        exploringaquatichabitataT.SetActive(false);
    }



    void _adaptationsinfrogTMethodON()
    {
        adaptationsinfrogT.SetActive(true);
    }

    void _adaptationsinfrogTMethodOFF()
    {
        adaptationsinfrogT.SetActive(false);
    }





    void _habitatDMethodON()
    {
        habitatD.SetActive(true);
    }

    void _habitatDMethodOFF()
    {
        habitatD.SetActive(false);
    }






    void _terrestrialDMethodON()
    {
        terrestrialD.SetActive(true);
    }

    void _terrestrialDMethodOFF()
    {
        terrestrialD.SetActive(false);
    }







    void _AquatichabitatDMethodON()
    {
        AquatichabitatD.SetActive(true);
    }

    void _AquatichabitatDMethodOFF()
    {
        AquatichabitatD.SetActive(false);
    }





    void _adapationDMethodON()
    {
        adapationD.SetActive(true);
    }

    void _adapationDMethodOFF()
    {
        adapationD.SetActive(false);
    }



















    // Objects

    void _yakMethodON()
    {
        yak.SetActive(true);
    }

    void _yakMethodOFF()
    {
        yak.SetActive(false);
    }



    void _porcupianMethodON()
    {
        porcupine.SetActive(true);
    }

    void _porcupianMethodOFF()
    {
        porcupine.SetActive(false);
    }




    void _plainMethodON()
    {
        plain.SetActive(true);
    }

    void _plainMethodOFF()
    {
        plain.SetActive(false);
    }




    void _frogMethodON()
    {
        frog.SetActive(true);
    }

    void _frogMethodOFF()
    {
        frog.SetActive(false);
    }




    void _mountaingoat_MethodON()
    {
        mountaingoat.SetActive(true);
    }

    void _mountaingoat_MethodOFF()
    {
        mountaingoat.SetActive(false);
    }



    void _plain1MethodON()
    {
        plain1.SetActive(true);
    }

    void _plain1MethodOFF()
    {
        plain1.SetActive(false);
    }



































    // Audio


    void _habitat_audioMethod()

    {
        myAudio.clip = habitat;
        myAudio.Play();
    }



    void _types_audioMethod()

    {
        myAudio.clip = types;
        myAudio.Play();
    }

    void _terrestrial_audioMethod()

    {
        myAudio.clip = terrestrial;
        myAudio.Play();
    }


    void _aquatic_audioMethod()

    {
        myAudio.clip = aquatic;
        myAudio.Play();
    }


    void _adaptation_audioMethod()

    {
        myAudio.clip = adaptation;
        myAudio.Play();
    }




    void _exploringdesert_audioMethod()

    {
        myAudio.clip = exploringdesert;
        myAudio.Play();
    }



    void _para1_audioMethod()

    {
        myAudio.clip = para1;
        myAudio.Play();
    }


    void _para2_audioMethod()

    {
        myAudio.clip = para2;
        myAudio.Play();
    }

    


















    void _discoveringmountains_audioMethod()

    {
        myAudio.clip = discoveringmountains;
        myAudio.Play();
    }



    void _paradm2_audioMethod()

    {
        myAudio.clip = paradm2;
        myAudio.Play();
    }



    void _paradm3_audioMethod()

    {
        myAudio.clip = paradm3;
        myAudio.Play();
    }


    void _paradm4_audioMethod()

    {
        myAudio.clip = paradm4;
        myAudio.Play();
    }


    void _paradm5_audioMethod()

    {
        myAudio.clip = paradm5;
        myAudio.Play();
    }



    void _paradm6_audioMethod()

    {
        myAudio.clip = paradm6;
        myAudio.Play();
    }

























    void _exploringaquatichabitat_audioMethod()

    {
        myAudio.clip = exploringaquatichabitat;
        myAudio.Play();
    }





    void _adaptationinfrog_audioMethod()

    {
        myAudio.clip = adaptationinfrog;
        myAudio.Play();
    }


    private int index = 0;
    IEnumerator ChangeToNextLevel(Transform spawnPoint)
    {
        yield return new WaitForSeconds(1);
        InventoryManager.Instance.GetComponent<GamePlayManager>().FadeOut();
        InventoryManager.Instance.player.ChangePosition(spawnPoint);
    }

    public void ChooseCorrect(Transform spawnPoint)
    {
        index++;
        InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();

        if (index == 3)
        {
            index = 0;
            StartCoroutine(ChangeToNextLevel(spawnPoint));
        }
    }

    public void ChooseCorrectLastLevel()
    {
        index++;
        InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void ChooseWrongOne()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }
}
