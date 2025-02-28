using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_microorganisms_friend_or_foe_8class : MonoBehaviour
{


    public GameObject Scene_Gameplay;
    public GameObject Scene_Explantion;
    private Animator anim;

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

    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip interduction;
    public AudioClip microorganisam_and_thair_habitat;
    public AudioClip Microorganisms_and_their_habitat_2;
    public AudioClip catagires_of_micro_organisam;
    public AudioClip friendly_microorganisam;
    public AudioClip commercial_use;
    public AudioClip other_benifits_provide;
    public AudioClip biogas_synthasis;
   
 
    



    public AudioClip hormfull_microorganigam;
    public AudioClip types;
    public AudioClip Non_communicable_diseases;
    public AudioClip Communicable_diseases;
    public AudioClip Disease_causing;
    public AudioClip Housefly;
    public AudioClip Mesquito;
    public AudioClip mascito_p2;






    public AudioClip Common_Human_disease;
    public AudioClip Disease_causingg;
    public AudioClip Foot_and_mouth_disease;
    public AudioClip Disease_tabul;
    public AudioClip Food_poisoning;
    public AudioClip Food_preservation;
    public AudioClip Pasteurization;









    public AudioClip Chemical_Method_pa;
    public AudioClip Chemical_Method_pb;
    public AudioClip Preservation_by_Sugar;
    public AudioClip Preservation_by_Common_Salt;
    public AudioClip Preservation_by_Oil_and_Vinegar;
    public AudioClip Nitrogen_fixation;
    public AudioClip Nitrogen_fixation_pa;
    public AudioClip Nitrogen_cycle;
    public AudioClip Nitrogen_cycle_pb;
    public AudioClip Nitrogen_cycle_pc;
    public AudioClip nitrogen_cycle_pd;
   











































    // Titles



    public GameObject IntroductionT;
    public GameObject MicroorganismsT;
    public GameObject Categorie_T;
    public GameObject Friendly_microorganisam_T;
    public GameObject Commercial_use_of_microorganismsT;
    public GameObject Other_benifits_T;
    public GameObject Biogas_synthesis_T;



    public GameObject Harmful_microorganismsT;
    public GameObject Non_communicable_diseasesT;
    public GameObject Communicable_diseasesT;
    public GameObject Disease_causingT;
    public GameObject House_flyT;
    public GameObject MosquitoT;
    public GameObject Common_HumanT;










    public GameObject microorganisamD;
    public GameObject classifidemicD;
    public GameObject Nitrogen_fixationD;
    public GameObject AnaerobicD;
  
    
    public GameObject The_microorganismsD;
    public GameObject Example_diabetesD;
    public GameObject SneezingD;
    public GameObject HouseflyD;




    public GameObject AnthraxD;
    public GameObject pasteurizationD;






    public GameObject SodiumD;
    public GameObject SugarD;
    public GameObject SaltingD;
    public GameObject RhizobiumD;
    public GameObject atmosphereD;















    public GameObject Common_HumanaT;
    public GameObject Disease_causinggT;
    public GameObject Foot_and_mouth_diseaseT;
    public GameObject Disease_causingaT;
    public GameObject Food_poisoningT;
    public GameObject Food_preservationT;
    public GameObject PasteurizationT;






    public GameObject Chemical_MethodT;
    public GameObject Preservation_by_SugarT;
    public GameObject Preservation_by_Common_SaltT;
    public GameObject Preservation_by_Oil_and_VinegarT;
    public GameObject Nitrogen_fixationT;
    public GameObject Nitrogen_cycleT;
    

























































    // models_ON/OFF










    public GameObject biogasmodel;




    public GameObject masquto_sprey;

    public GameObject masquto;
    public GameObject masquto_cream;







































    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("microorganisam", 0, targetNormalizedTime);
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


















    // Audio


    void _interduction_audioMethod()

    {
        myAudio.clip = interduction;
        myAudio.Play();
    }






    void _microorganisam_and_thair_habitat_audioMethod()

    {
        myAudio.clip = microorganisam_and_thair_habitat;
        myAudio.Play();
    }








    void _Microorganisms_and_their_habitat_2_audioMethod()

    {
        myAudio.clip = Microorganisms_and_their_habitat_2;
        myAudio.Play();
    }






    void _catagires_of_micro_organisam_audioMethod()

    {
        myAudio.clip = catagires_of_micro_organisam;
        myAudio.Play();
    }









    void _friendly_microorganisam_audioMethod()

    {
        myAudio.clip = friendly_microorganisam;
        myAudio.Play();
    }









    void _commercial_use_audioMethod()

    {
        myAudio.clip = commercial_use;
        myAudio.Play();
    }






    void _other_benifits_provide_audioMethod()

    {
        myAudio.clip = other_benifits_provide;
        myAudio.Play();
    }








    void _biogas_synthasis_audioMethod()

    {
        myAudio.clip = biogas_synthasis;
        myAudio.Play();
    }









    void _hormfull_microorganigam_audioMethod()

    {
        myAudio.clip = hormfull_microorganigam;
        myAudio.Play();
    }








    void _types_audioMethod()

    {
        myAudio.clip = types;
        myAudio.Play();
    }









    void _Non_communicable_diseases_audioMethod()

    {
        myAudio.clip = Non_communicable_diseases;
        myAudio.Play();
    }










    void _Communicable_diseases_audioMethod()

    {
        myAudio.clip = Communicable_diseases;
        myAudio.Play();
    }










    void _Disease_causing_audioMethod()

    {
        myAudio.clip = Disease_causing;
        myAudio.Play();
    }









    void _Housefly_audioMethod()

    {
        myAudio.clip = Housefly;
        myAudio.Play();
    }







    void _Mesquito_audioMethod()

    {
        myAudio.clip = Mesquito;
        myAudio.Play();
    }









    void _mascito_p2_audioMethod()

    {
        myAudio.clip = mascito_p2;
        myAudio.Play();
    }






































    void _Common_Human_disease_audioMethod()

    {
        myAudio.clip = Common_Human_disease;
        myAudio.Play();
    }












    void _Disease_causingg_audioMethod()

    {
        myAudio.clip = Disease_causingg;
        myAudio.Play();
    }












    void _Foot_and_mouth_disease_audioMethod()

    {
        myAudio.clip = Foot_and_mouth_disease;
        myAudio.Play();
    }









    void _Disease_tabul_audioMethod()

    {
        myAudio.clip = Disease_tabul;
        myAudio.Play();
    }












    void _Food_poisoning_audioMethod()

    {
        myAudio.clip = Food_poisoning;
        myAudio.Play();
    }











    void _Food_preservation_audioMethod()

    {
        myAudio.clip = Food_preservation;
        myAudio.Play();
    }









    void _Pasteurization_audioMethod()

    {
        myAudio.clip = Pasteurization;
        myAudio.Play();
    }

























    void _Chemical_Method_pa_audioMethod()

    {
        myAudio.clip = Chemical_Method_pa;
        myAudio.Play();
    }







    void _Chemical_Method_pb_audioMethod()

    {
        myAudio.clip = Chemical_Method_pb;
        myAudio.Play();
    }







    void _Preservation_by_Sugar_audioMethod()

    {
        myAudio.clip = Preservation_by_Sugar;
        myAudio.Play();
    }







    void _Preservation_by_Common_Salt_audioMethod()

    {
        myAudio.clip = Preservation_by_Common_Salt;
        myAudio.Play();
    }






    void _Preservation_by_Oil_and_Vinegar_audioMethod()

    {
        myAudio.clip = Preservation_by_Oil_and_Vinegar;
        myAudio.Play();
    }





    void _Nitrogen_fixation_audioMethod()

    {
        myAudio.clip = Nitrogen_fixation;
        myAudio.Play();
    }





    void _Nitrogen_fixation_pa_audioMethod()

    {
        myAudio.clip = Nitrogen_fixation_pa;
        myAudio.Play();
    }





    void _Nitrogen_cycle_audioMethod()

    {
        myAudio.clip = Nitrogen_cycle;
        myAudio.Play();
    }




    
    void _Nitrogen_cycle_pb_audioMethod()

    {
        myAudio.clip = Nitrogen_cycle_pb;
        myAudio.Play();
    }





    void _Nitrogen_cycle_pc_audioMethod()

    {
        myAudio.clip = Nitrogen_cycle_pc;
        myAudio.Play();
    }





    void _nitrogen_cycle_pd_audioMethod()

    {
        myAudio.clip = nitrogen_cycle_pd;
        myAudio.Play();
    }
























    // Titles


    void _IntroductionTMethodON()
    {
        IntroductionT.SetActive(true);
    }

    void _IntroductionTMethodOFF()
    {
        IntroductionT.SetActive(false);
    }











   


    void _MicroorganismsTMethodON()
    {
        MicroorganismsT.SetActive(true);
    }

    void _MicroorganismsTMethodOFF()
    {
        MicroorganismsT.SetActive(false);
    }











    void _Categorie_TMethodON()
    {
        Categorie_T.SetActive(true);
    }

    void _Categorie_TMethodOFF()
    {
        Categorie_T.SetActive(false);
    }











    void _Friendly_microorganisam_TMethodON()
    {
        Friendly_microorganisam_T.SetActive(true);
    }

    void _Friendly_microorganisam_TMethodOFF()
    {
        Friendly_microorganisam_T.SetActive(false);
    }












    void _Commercial_use_of_microorganismsTMethodON()
    {
        Commercial_use_of_microorganismsT.SetActive(true);
    }

    void _Commercial_use_of_microorganismsTMethodOFF()
    {
        Commercial_use_of_microorganismsT.SetActive(false);
    }









    void _Other_benifits_TMethodON()
    {
        Other_benifits_T.SetActive(true);
    }

    void _Other_benifits_TMethodOFF()
    {
        Other_benifits_T.SetActive(false);
    }









    void _Biogas_synthesis_TMethodON()
    {
        Biogas_synthesis_T.SetActive(true);
    }

    void _Biogas_synthesis_TMethodOFF()
    {
        Biogas_synthesis_T.SetActive(false);
    }











    void _Harmful_microorganismsTMethodON()
    {
        Harmful_microorganismsT.SetActive(true);
    }

    void _Harmful_microorganismsTMethodOFF()
    {
        Harmful_microorganismsT.SetActive(false);
    }












    void _Non_communicable_diseasesTMethodON()
    {
        Non_communicable_diseasesT.SetActive(true);
    }

    void _Non_communicable_diseasesTMethodOFF()
    {
        Non_communicable_diseasesT.SetActive(false);
    }











    void _Communicable_diseasesTMethodON()
    {
        Communicable_diseasesT.SetActive(true);
    }

    void _Communicable_diseasesTMethodOFF()
    {
        Communicable_diseasesT.SetActive(false);
    }











    void _Disease_causingTMethodON()
    {
        Disease_causingT.SetActive(true);
    }

    void _Disease_causingTMethodOFF()
    {
        Disease_causingT.SetActive(false);
    }









    void _House_flyTMethodON()
    {
        House_flyT.SetActive(true);
    }

    void _House_flyTMethodOFF()
    {
        House_flyT.SetActive(false);
    }









    void _MosquitoTMethodON()
    {
        MosquitoT.SetActive(true);
    }

    void _MosquitoTMethodOFF()
    {
        MosquitoT.SetActive(false);
    }










    void _Common_HumanTMethodON()
    {
        Common_HumanT.SetActive(true);
    }

    void _Common_HumanTMethodOFF()
    {
        Common_HumanT.SetActive(false);
    }





































    void _microorganisamD_TMethodON()
    {
        microorganisamD.SetActive(true);
    }

    void _microorganisamDMethodOFF()
    {
        microorganisamD.SetActive(false);
    }










    void _classifidemicD_TMethodON()
    {
        classifidemicD.SetActive(true);
    }

    void _classifidemicDMethodOFF()
    {
        classifidemicD.SetActive(false);
    }









    void _Nitrogen_fixationD_TMethodON()
    {
        Nitrogen_fixationD.SetActive(true);
    }

    void _Nitrogen_fixationDMethodOFF()
    {
        Nitrogen_fixationD.SetActive(false);
    }









    void _AnaerobicD_TMethodON()
    {
        AnaerobicD.SetActive(true);
    }

    void _AnaerobicDMethodOFF()
    {
        AnaerobicD.SetActive(false);
    }



































    void _The_microorganismsDMethodON()
    {
        The_microorganismsD.SetActive(true);
    }

    void _The_microorganismsDMethodOFF()
    {
        The_microorganismsD.SetActive(false);
    }











    void _Example_diabetesDMethodON()
    {
        Example_diabetesD.SetActive(true);
    }

    void _Example_diabetesDMethodOFF()
    {
        Example_diabetesD.SetActive(false);
    }











    void _SneezingDMethodON()
    {
        SneezingD.SetActive(true);
    }

    void _SneezingDMethodOFF()
    {
        SneezingD.SetActive(false);
    }













    void _HouseflyDMethodON()
    {
        HouseflyD.SetActive(true);
    }

    void _HouseflyDMethodOFF()
    {
        HouseflyD.SetActive(false);
    }













    void _AnthraxDMethodON()
    {
        AnthraxD.SetActive(true);
    }

    void _AnthraxDMethodOFF()
    {
        AnthraxD.SetActive(false);
    }














    void _pasteurizationDMethodON()
    {
        pasteurizationD.SetActive(true);
    }

    void _pasteurizationDMethodOFF()
    {
        pasteurizationD.SetActive(false);
    }






    void _SodiumDMethodON()
    {
        SodiumD.SetActive(true);
    }

    void _SodiumDMethodOFF()
    {
        SodiumD.SetActive(false);
    }





    void _SugarDMethodON()
    {
        SugarD.SetActive(true);
    }

    void _SugarDMethodOFF()
    {
        SugarD.SetActive(false);
    }





    void _SaltingDMethodON()
    {
        SaltingD.SetActive(true);
    }

    void _SaltingDMethodOFF()
    {
        SaltingD.SetActive(false);
    }







    void _RhizobiumDMethodON()
    {
        RhizobiumD.SetActive(true);
    }

    void _RhizobiumDMethodOFF()
    {
        RhizobiumD.SetActive(false);
    }






    void _atmosphereDMethodON()
    {
        atmosphereD.SetActive(true);
    }

    void _atmosphereDMethodOFF()
    {
        atmosphereD.SetActive(false);
    }




















































    void _Common_HumanaTMethodON()
    {
        Common_HumanaT.SetActive(true);
    }

    void _Common_HumanaTMethodOFF()
    {
        Common_HumanaT.SetActive(false);
    }















    void _Disease_causinggTMethodON()
    {
        Disease_causinggT.SetActive(true);
    }

    void _Disease_causinggTMethodOFF()
    {
        Disease_causinggT.SetActive(false);
    }











    void _Foot_and_mouth_diseaseTMethodON()
    {
        Foot_and_mouth_diseaseT.SetActive(true);
    }

    void _Foot_and_mouth_diseaseTMethodOFF()
    {
        Foot_and_mouth_diseaseT.SetActive(false);
    }













    void _Disease_causingaTMethodON()
    {
        Disease_causingaT.SetActive(true);
    }

    void _Disease_causingaTMethodOFF()
    {
        Disease_causingaT.SetActive(false);
    }









    void _Food_poisoningTMethodON()
    {
        Food_poisoningT.SetActive(true);
    }

    void _Food_poisoningTMethodOFF()
    {
        Food_poisoningT.SetActive(false);
    }










    void _Food_preservationTMethodON()
    {
        Food_preservationT.SetActive(true);
    }

    void _Food_preservationTMethodOFF()
    {
        Food_preservationT.SetActive(false);
    }










    void _PasteurizationTMethodON()
    {
        PasteurizationT.SetActive(true);
    }

    void _PasteurizationTMethodOFF()
    {
        PasteurizationT.SetActive(false);
    }





































    void _Chemical_MethodTMethodON()
    {
        Chemical_MethodT.SetActive(true);
    }

    void _Chemical_MethodTMethodOFF()
    {
        Chemical_MethodT.SetActive(false);
    }








    void _Preservation_by_SugarTMethodON()
    {
        Preservation_by_SugarT.SetActive(true);
    }

    void _Preservation_by_SugarTMethodOFF()
    {
        Preservation_by_SugarT.SetActive(false);
    }






    void _Preservation_by_Common_SaltTMethodON()
    {
        Preservation_by_Common_SaltT.SetActive(true);
    }

    void _Preservation_by_Common_SaltTMethodOFF()
    {
        Preservation_by_Common_SaltT.SetActive(false);
    }







    void _Preservation_by_Oil_and_VinegarTMethodON()
    {
        Preservation_by_Oil_and_VinegarT.SetActive(true);
    }

    void _Preservation_by_Oil_and_VinegarTMethodOFF()
    {
        Preservation_by_Oil_and_VinegarT.SetActive(false);
    }







    void _Nitrogen_fixationTMethodON()
    {
        Nitrogen_fixationT.SetActive(true);
    }

    void _Nitrogen_fixationTMethodOFF()
    {
        Nitrogen_fixationT.SetActive(false);
    }






    void _Nitrogen_cycleTMethodON()
    {
        Nitrogen_cycleT.SetActive(true);
    }

    void _Nitrogen_cycleTMethodOFF()
    {
        Nitrogen_cycleT.SetActive(false);
    }


















































    // Models













    void _biogasmodel_TMethodON()
    {
        biogasmodel.SetActive(true);
    }

    void _biogasmodelMethodOFF()
    {
        biogasmodel.SetActive(false);
    }












    void _masquto_spreyMethodON()
    {
        masquto_sprey.SetActive(true);
    }

    void _masquto_spreyMethodOFF()
    {
        masquto_sprey.SetActive(false);
    }








    void _masqutoMethodON()
    {
        masquto.SetActive(true);
    }

    void _masqutoMethodOFF()
    {
        masquto.SetActive(false);
    }













    void _masquto_creamMethodON()
    {
        masquto_cream.SetActive(true);
    }

    void _masquto_creamMethodOFF()
    {
        masquto_cream.SetActive(false);
    }




    public Transform targetPos;
    public Transform lakeTargetPos;
    private int killCount;
    private bool onLake = false;
    public void SetTargetTransform(Transform backt)
    {
        int randomVal = Random.Range(-3, 4);
        Vector3 lakePos = new Vector3(lakeTargetPos.position.x + randomVal, lakeTargetPos.position.y, lakeTargetPos.position.z + randomVal);
        Vector3 target = new Vector3(targetPos.position.x + randomVal, targetPos.position.y, targetPos.position.z + randomVal);
        backt.DOMove(target, 20f).OnComplete(() =>
        {
            backt.DOMove(lakePos, 1f).OnComplete(() =>
            {
                backt.gameObject.tag = "Untagged";
                backt.gameObject.GetComponent<Animator>().enabled = false;

            });
        });
    }

    public IEnumerator CalculateBacteria()
    {
        yield return new WaitForSeconds(22f);
        if (killCount == 3)
        {
            onLake = true;
            InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
        else
        {
            InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
        }
    }

    public void MiniGameStart()
    {
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
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

    private int collectedIndex;
    public TargetController collectMiniGame;
    public void CollectBact()
    {

        collectedIndex++;
        Debug.Log("COLLECTED BACTERIA - " + collectedIndex);
        if (collectedIndex == 3)
        {
            collectedIndex = 0;
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void MissionFail()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
    }

    public void NextStep()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().InvokeEvent();
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    private int badBactCount;
    public void DropBadBacteria(ItemObject obj)
    {
        if (obj.item.itemName == "B") badBactCount++;
        else InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();

        if (badBactCount == 3)
        {
            InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
        }
    }

    public void HeatMilk(GameObject stove)
    {
        stove.GetComponent<BoxCollider>().enabled = false;
        stove.transform.GetChild(0).gameObject.SetActive(true);
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }
    public void CoolMilk()
    {
        InventoryManager.Instance.GetComponent<ObjectiveController>().StepCompleted();
    }

    public void CanChips(Animator animator)
    {
        animator.SetTrigger("Trigger");
    }







































}
