using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_nutrition_in_animals_class_7 : MonoBehaviour
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





    // Exp - Audio
    [Header("Audio files")]

    public AudioSource myAudio;



    public AudioClip nutrition_in_animals;
    public AudioClip Various_modes_of_food_intake;
    public AudioClip Digestion_in_humans;
    public AudioClip canal_cavety;
    public AudioClip The_secretory_glands;
    public AudioClip The_mouth_and_buccal_cavity;
    public AudioClip food_pipe;
    public AudioClip smoll_intastiv;
    public AudioClip Livar;
    public AudioClip pancrias;
    public AudioClip Absorption_in_the_small_intestine;
    public AudioClip lorge_intestine;
    public AudioClip DIgestion_in_grass_eating_animals;











    // Titles



    public GameObject NUTRITION_IN_ANIMALS_T;
    public GameObject Various_modes_of_food_intakeT;
    public GameObject Digestion_in_humansT;
    public GameObject parts_of_canalT;

    public GameObject The_secretory_glands_that_secrete_digestiveT;
    public GameObject The_mouth_and_buccal_cavity_T;
    public GameObject Food_pipeT;
    public GameObject Stomach_T;
    public GameObject Small_intestine_T;
    public GameObject Liver_T;
    public GameObject PancreasT;
    public GameObject Absorption_in_the_small_intestineT;
    public GameObject Large_intestineT;
    public GameObject DIgestion_in_grass_eating_animals_T;
   
    
    
    
    
    
    
    
    
    
    public GameObject Taking_food_D;

    public GameObject The_mucus_D;

    public GameObject the_small_intestain_lenth_D;
    public GameObject the_undaigest_D;
    public GameObject lorge_intestive_D;









    // Titles_text_mesh_pro



    public GameObject food_pipee;
    public GameObject BK;
    public GameObject Stomach;
    public GameObject Small_intestine;
    public GameObject Large_intestine;
    public GameObject Anus;
    public GameObject liver;
    public GameObject Salivary_Glands;
    public GameObject Pancreas;





    [Header("Explanation anims")]
    private Animator anim;
    public GameObject eating_anim;










    private Animator animator;

    // Static variable to store the normalized time across scene reloads
    private static float targetNormalizedTime = -1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        // If there is a target time stored, jump to that animation keyframe
        if (targetNormalizedTime >= 0f)
        {
            animator.Play("nutrition in animals cam animation", 0, targetNormalizedTime);
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


















    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Audio
    void _nutrition_in_animals_audioMethod()

    {
        myAudio.clip = nutrition_in_animals;
        myAudio.Play();
    }







    


    void _Various_modes_of_food_intake_audioMethod()

    {
        myAudio.clip = Various_modes_of_food_intake;
        myAudio.Play();
    }









    


    void _Digestion_in_humans_audioMethod()

    {
        myAudio.clip = Digestion_in_humans;
        myAudio.Play();
    }






    


    void _canal_cavety_audioMethod()

    {
        myAudio.clip = canal_cavety;
        myAudio.Play();
    }







    


    void _The_secretory_glands_audioMethod()

    {
        myAudio.clip = The_secretory_glands;
        myAudio.Play();
    }






    void _The_mouth_and_buccal_cavity_audioMethod()

    {
        myAudio.clip = The_mouth_and_buccal_cavity;
        myAudio.Play();
    }







    void _food_pipe_audioMethod()

    {
        myAudio.clip = food_pipe;
        myAudio.Play();
    }






    void _smoll_intastiv_audioMethod()

    {
        myAudio.clip = smoll_intastiv;
        myAudio.Play();
    }





    void _Livar_audioMethod()

    {
        myAudio.clip = Livar;
        myAudio.Play();
    }






    void _pancrias_audioMethod()

    {
        myAudio.clip = pancrias;
        myAudio.Play();
    }








    void _Absorption_in_the_small_intestine_audioMethod()

    {
        myAudio.clip = Absorption_in_the_small_intestine;
        myAudio.Play();
    }






    void _lorge_intestine_audioMethod()

    {
        myAudio.clip = lorge_intestine;
        myAudio.Play();
    }








    void _DIgestion_in_grass_eating_animals_audioMethod()

    {
        myAudio.clip = DIgestion_in_grass_eating_animals;
        myAudio.Play();
    }











    // Titles


    void _NUTRITION_IN_ANIMALS_TMethodON()
    {
        NUTRITION_IN_ANIMALS_T.SetActive(true);
    }

    void _NUTRITION_IN_ANIMALS_TMethodOFF()
    {
        NUTRITION_IN_ANIMALS_T.SetActive(false);
    }





   


    void _Various_modes_of_food_intakeTMethodON()
    {
        Various_modes_of_food_intakeT.SetActive(true);
    }

    void _Various_modes_of_food_intakeT_TMethodOFF()
    {
        Various_modes_of_food_intakeT.SetActive(false);
    }











    void _Digestion_in_humansTMethodON()
    {
        Digestion_in_humansT.SetActive(true);
    }

    void _Digestion_in_humansTMethodOFF()
    {
        Digestion_in_humansT.SetActive(false);
    }










    void _parts_of_canalTMethodON()
    {
        parts_of_canalT.SetActive(true);
    }

    void _parts_of_canalTMethodOFF()
    {
        parts_of_canalT.SetActive(false);
    }

































    void _The_secretory_glands_that_secrete_digestiveTMethodON()
    {
        The_secretory_glands_that_secrete_digestiveT.SetActive(true);
    }

    void _The_secretory_glands_that_secrete_digestiveTMethodOFF()
    {
        The_secretory_glands_that_secrete_digestiveT.SetActive(false);
    }











    void _The_mouth_and_buccal_cavity_TMethodON()
    {
        The_mouth_and_buccal_cavity_T.SetActive(true);
    }

    void _The_mouth_and_buccal_cavity_TMethodOFF()
    {
        The_mouth_and_buccal_cavity_T.SetActive(false);
    }









    void _Food_pipeTMethodON()
    {
        Food_pipeT.SetActive(true);
    }

    void _Food_pipeTMethodOFF()
    {
        Food_pipeT.SetActive(false);
    }








    void _Stomach_TMethodON()
    {
        Stomach_T.SetActive(true);
    }

    void _Stomach_TMethodOFF()
    {
        Stomach_T.SetActive(false);
    }








    void _Small_intestine_TMethodON()
    {
        Small_intestine_T.SetActive(true);
    }

    void _Small_intestine_TMethodOFF()
    {
        Small_intestine_T.SetActive(false);
    }






    void _Liver_TMethodON()
    {
        Liver_T.SetActive(true);
    }

    void _Liver_TMethodOFF()
    {
        Liver_T.SetActive(false);
    }








    void _PancreasTMethodON()
    {
        PancreasT.SetActive(true);
    }

    void _PancreasTMethodOFF()
    {
        PancreasT.SetActive(false);
    }








    void _Absorption_in_the_small_intestineTMethodON()
    {
        Absorption_in_the_small_intestineT.SetActive(true);
    }

    void _Absorption_in_the_small_intestineTMethodOFF()
    {
        Absorption_in_the_small_intestineT.SetActive(false);
    }










    void _Large_intestineTMethodON()
    {
        Large_intestineT.SetActive(true);
    }

    void _Large_intestineTMethodOFF()
    {
        Large_intestineT.SetActive(false);
    }






    void _DIgestion_in_grass_eating_animals_TMethodON()
    {
        DIgestion_in_grass_eating_animals_T.SetActive(true);
    }

    void _DIgestion_in_grass_eating_animals_TMethodOFF()
    {
        DIgestion_in_grass_eating_animals_T.SetActive(false);
    }





    void _Taking_food_DMethodON()
    {
        Taking_food_D.SetActive(true);
    }

    void _Taking_food_DMethodOFF()
    {
        Taking_food_D.SetActive(false);
    }








    void _The_mucus_DMethodON()
    {
        The_mucus_D.SetActive(true);
    }

    void _The_mucus_DMethodOFF()
    {
        The_mucus_D.SetActive(false);
    }









    void _the_small_intestain_lenth_DMethodON()
    {
        the_small_intestain_lenth_D.SetActive(true);
    }

    void _the_small_intestain_lenth_DMethodOFF()
    {
        the_small_intestain_lenth_D.SetActive(false);
    }






    void _the_undaigest_DMethodON()
    {
        the_undaigest_D.SetActive(true);
    }

    void _the_undaigest_DMethodOFF()
    {
        the_undaigest_D.SetActive(false);
    }









    void _lorge_intestive_DMethodON()
    {
        lorge_intestive_D.SetActive(true);
    }

    void _lorge_intestive_DMethodOFF()
    {
        lorge_intestive_D.SetActive(false);
    }














    // Titles_text_mesh_pro










    void _food_pipeeMethodON()
    {
        food_pipee.SetActive(true);
    }

    void _food_pipeeMethodOFF()
    {
        food_pipee.SetActive(false);
    }






    void _BKMethodON()
    {
        BK.SetActive(true);
    }

    void _BKMethodOFF()
    {
        BK.SetActive(false);
    }







    void _StomachMethodON()
    {
        Stomach.SetActive(true);
    }

    void _StomachMethodOFF()
    {
        Stomach.SetActive(false);
    }






    void _Small_intestineMethodON()
    {
        Small_intestine.SetActive(true);
    }

    void _Small_intestineMethodOFF()
    {
        Small_intestine.SetActive(false);
    }







    void _Large_intestineMethodON()
    {
        Large_intestine.SetActive(true);
    }

    void _Large_intestineMethodOFF()
    {
        Large_intestine.SetActive(false);
    }









    void _AnusMethodON()
    {
        Anus.SetActive(true);
    }

    void _AnusMethodOFF()
    {
        Anus.SetActive(false);
    }







    void _liverMethodON()
    {
        liver.SetActive(true);
    }

    void _liverMethodOFF()
    {
        liver.SetActive(false);
    }







    void _Salivary_GlandsMethodON()
    {
        Salivary_Glands.SetActive(true);
    }

    void _Salivary_GlandsMethodOFF()
    {
        Salivary_Glands.SetActive(false);
    }









    void _PancreasMethodON()
    {
        Pancreas.SetActive(true);
    }

    void _PancreasMethodOFF()
    {
        Pancreas.SetActive(false);
    }
    //Animation Play
    void _eating_animMethod()
    {
        anim = eating_anim.GetComponent<Animator>();
        anim.Play("eating anim");
    }

    public AudioSource clickSound;
    private int currentIndex = 0;
    public List<string> humanOrgans = new List<string>();
    public List<string> cowOrgans = new List<string>();
    public Camera cam;
    public LayerMask layerMask;
    private bool humanMiniGameStarted = false;
    private bool cowMiniGameStarted = false;
    public TargetController humanBodyMiniGame;
    public TargetController cowMiniGame;
    public TargetController minigame2;
    private void Update()
    {
        if(humanMiniGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickSound.Play();
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
                {
                    if (raycastHit.collider != null)
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        if (raycastHit.collider.gameObject.name == humanOrgans[currentIndex])
                        {
                            currentIndex++;
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                            
                            if (currentIndex == humanOrgans.Count - 2)
                            {
                                minigame2.Output();
                            }
                            if (currentIndex == humanOrgans.Count)
                            {
                                humanMiniGameStarted = false;
                                
                                currentIndex = 0;
                            }
                        }
                        else
                        {
                            InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
                        }
                    }
                }
            }
        }

        if(cowMiniGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickSound.Play();
                Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
                {
                    if (raycastHit.collider != null)
                    {
                        Debug.Log(raycastHit.collider.gameObject.name);
                        if (raycastHit.collider.gameObject.name == cowOrgans[currentIndex])
                        {
                            currentIndex++;

                            InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().InvokeEvent();
                            if (currentIndex == cowOrgans.Count)
                            {
                                cowMiniGameStarted = false;
                                StartCoroutine(CowGameplayEndDelay());
                            }
                            else
                            {
                                InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
                            }
                        }
                        else
                        {
                            InventoryManager.Instance.GetComponent<ObjectiveController>().missionFailed();
                        }
                    }
                }
            }
        }
    }

    public void HumanMiniGameStart()
    {
        humanMiniGameStarted = true;
        InventoryManager.Instance.player.transform.root.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }

    public void MiniGameStart()
    {
        cowMiniGameStarted = true;
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

    public void FoodAnim1(Animator anim)
    {
        anim.SetTrigger("1");
    }

    public void FoodAnim2(Animator anim)
    {
        anim.SetTrigger("2");
        StartCoroutine(OrgansGameplayEndDelay());
    }

    public Animator mouthAnim;
    public void CowFoodAnim1(Animator anim)
    {
        anim.SetTrigger("1");       
    }

    public void CowFoodAnim2(Animator anim)
    {
        anim.SetTrigger("2");
        mouthAnim.SetTrigger("Trigger");
        //StartCoroutine(CowGameplayEndDelay());
    }

    public IEnumerator OrgansGameplayEndDelay()
    {
        yield return new WaitForSeconds(8);
        humanBodyMiniGame.EndMiniGame();
        cowMiniGame.Output();
    }
    public IEnumerator CowGameplayEndDelay()
    {
        yield return new WaitForSeconds(3);
        InventoryManager.Instance.gameObject.GetComponent<ObjectiveController>().StepCompleted();
    }


























}
