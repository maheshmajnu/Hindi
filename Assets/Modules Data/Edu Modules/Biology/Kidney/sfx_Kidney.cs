using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sfx_Kidney : MonoBehaviour
{
        [Header("Exp Assets")]
    public GameObject Skeleton;
    public GameObject Kidney_Top;
    public GameObject Kidney_Bottom;
    public GameObject Kidney_parts;
    public GameObject Capsue;
    public GameObject UShape;

    public GameObject RC;
    public GameObject RT_PCT;
    public GameObject RT_Loop;
    public GameObject RT_Dct;

        [Header("cutscene mp3")]
    public AudioSource myAudio;

    public AudioClip title;
    public AudioClip Exp;
    
    public AudioClip UrinarySys_title;
    public AudioClip UrinarySys_exp1;
    public AudioClip UrinarySys_parts;

    public AudioClip kidneys_title;
    public AudioClip kidneys_exp1;
    public AudioClip kidneys_exp2;
    public AudioClip kidneys_exp3;
    public AudioClip kidneys_exp4;
    public AudioClip kidneys_exp5; 

    public AudioClip WOK_title;
    public AudioClip WOK_parts;
    public AudioClip WOK_exp1;

    public AudioClip RC_title;
    public AudioClip RC_exp;

    public AudioClip Glow_title;
    public AudioClip Glow_exp; 
     
    public AudioClip BC_title; 
    public AudioClip BC_exp;

    public AudioClip RT_title;
    public AudioClip RT_exp;

    public AudioClip PCT_title;
    public AudioClip PCT_exp;
    
    public AudioClip Loop_title;
    public AudioClip Loop_exp;

    public AudioClip DCT_title;
    public AudioClip DCT_exp;

    public AudioClip CD_title;
    public AudioClip CD_exp;   

    public AudioClip Uretus_title;
    public AudioClip Uretus_exp;

    public AudioClip Unrinary_title; 
    public AudioClip Unrinary_exp; 

    public AudioClip Uretra_title;
    public AudioClip Uretra_exp;


    [Header("Lines & Texts")]
    public GameObject Line_Urinary; 
    public GameObject Line_Uretra; 
    public GameObject Line_Uretes; 
    public GameObject Dimensions;
    public GameObject Line_Nephron;

    public GameObject Line_Renal_C;
    public GameObject Line_Renal_T;
    public GameObject Line_Glom;
    public GameObject Line_BC;
    public GameObject Line_BSpace;
    public GameObject Line_PCT;
    public GameObject Line_Loop;
    public GameObject Line_DCT;
    public GameObject Line_CollectingDuct; 

     // Other voice delayed line methods
        void Line_BC_ON(){
            Line_BC.SetActive(true);
            Line_BSpace.SetActive(true);
        }
        void Line_BC_OFF(){
            Line_BC.SetActive(false);
            Line_BSpace.SetActive(false);
        }

        void Line_CollectingDuct_ON(){
            Line_CollectingDuct.SetActive(true);
        }
        void Line_CollectingDuct_OFF(){
            Line_CollectingDuct.SetActive(false);
        }

        void Line_DCT_ON(){
            Line_DCT.SetActive(true);
            RT_Dct.SetActive(true);
        }
        void Line_DCT_OFF(){
            Line_DCT.SetActive(false);
            RT_Dct.SetActive(false);
        }


        void Line_Loop_ON(){
            Line_Loop.SetActive(true);
            RT_Loop.SetActive(true);
        }
        void Line_Loop_OFF(){
            Line_Loop.SetActive(false);
            RT_Loop.SetActive(false);
        }

        void Line_PCT_ON(){
            Line_PCT.SetActive(true);
            RT_PCT.SetActive(true);
        }
        void Line_PCT_OFF(){
            Line_PCT.SetActive(false);
            RT_PCT.SetActive(false);
        }

        void Line_Urinary_ON(){
            Line_Urinary.SetActive(true);
        }

        void Line_Uretes_OFF(){
            Line_Uretes.SetActive(false);
        }

        void Line_Nephron_ON(){
            Line_Nephron.SetActive(true);
        }
        void Line_Nephron_OFF(){
            Line_Nephron.SetActive(false);
        }
        void Line_WOK_Parts_ON(){
            Line_Renal_C.SetActive(true); RC.SetActive(true); 
            Line_Renal_T.SetActive(true);
        }
        void Line_WOK_Parts_OFF(){
            Line_Renal_C.SetActive(false); RC.SetActive(false); 
            Line_Renal_T.SetActive(false); 
        }
        void Line_Glom_ON(){
            Line_Glom.SetActive(true);
        }void Line_Glom_OFF(){
            Line_Glom.SetActive(false);
        }
 
    //=============== TOGGLES ===============//

     void Skeleton_ON(){
        Skeleton.SetActive(true);
    }
    
    void Skeleton_OFF(){
        Skeleton.SetActive(false);
    }
     void Kidney_Top_ON(){
        Kidney_Top.SetActive(true);
    }
    
    void Kidney_Top_OFF(){
        Kidney_Top.SetActive(false);
    }
     void Kidney_Bottom_ON(){
        Kidney_Bottom.SetActive(true);
    }
    
    void Kidney_Bottom_OFF(){
        Kidney_Bottom.SetActive(false);
    }
 
     void UShape_ON(){
        Kidney_Top.SetActive(true);
    }
    
    void UShape_OFF(){
        Kidney_Top.SetActive(false);
    }

     void Capsue_ON(){
        Capsue.SetActive(true);
    }
    
    void Capsue_OFF(){
        Capsue.SetActive(false);
    }

    void Kidney_parts_ON(){
        Kidney_Top.SetActive(true);
    }
    
    void Kidney_parts_OFF(){
        Kidney_Top.SetActive(false);
    }

    void KidneyTotal_ON(){
        Kidney_parts_ON();
        Kidney_Top_ON();
        Kidney_Bottom_ON();
    }
    void KidneyTotal_OFF(){
        Kidney_parts_OFF();
        Kidney_Top_OFF();
        Kidney_Bottom_OFF();        
    }

    //========================  VOICES =====================//


        void title_Method(){
        myAudio.clip = title;
        myAudio.Play();
    }
        void Exp_Method(){
         myAudio.clip = Exp;
        myAudio.Play();       
    }
        void UrinarySys_title_Method(){
        myAudio.clip = UrinarySys_title;
        myAudio.Play();
    }
        void UrinarySys_exp1_Method(){
        myAudio.clip = UrinarySys_exp1;
        myAudio.Play();
    }
        void UrinarySys_parts_Method(){
        myAudio.clip = UrinarySys_parts;
        myAudio.Play();
    }
        void kidneys_title_Method(){
        myAudio.clip = kidneys_title;
        myAudio.Play();
    }
        void kidneys_exp1_Method(){
        myAudio.clip = kidneys_exp1;
        myAudio.Play();
    }
        void kidneys_exp2_Method(){
        myAudio.clip = kidneys_exp2;
        myAudio.Play();
        //line
        Dimensions.SetActive(true);
    }
        void kidneys_exp3_Method(){
        myAudio.clip = kidneys_exp3;
        myAudio.Play();
    }
        void kidneys_exp4_Method(){
        myAudio.clip = kidneys_exp4;
        myAudio.Play();
        //line
        Dimensions.SetActive(false);
    }
        void kidneys_exp5_Method(){
        myAudio.clip = kidneys_exp5;
        myAudio.Play();
    }
        void WOK_title_Method(){
        myAudio.clip = WOK_title;
        myAudio.Play();
    }
        void WOK_parts_Method(){
        myAudio.clip = WOK_parts;
        myAudio.Play();
    }    
        void WOK_exp1_Method(){
        myAudio.clip = WOK_exp1;
        myAudio.Play();
    }
        void RC_title_Method(){
        myAudio.clip = RC_title;
        myAudio.Play();
    }
        void RC_exp_Method(){
        myAudio.clip = RC_exp;
        myAudio.Play();
    }
        void Glow_title_Method(){
        myAudio.clip = Glow_title;
        myAudio.Play();
    }
        void Glow_exp_Method(){
        myAudio.clip = Glow_exp;
        myAudio.Play();
    }
        void BC_title_Method(){
        myAudio.clip = BC_title;
        myAudio.Play();
    }
        void BC_exp_Method(){
        myAudio.clip = BC_exp;
        myAudio.Play();
    }
        void RT_title_Method(){
        myAudio.clip = RT_title;
        myAudio.Play();
    }
        void RT_exp_Method(){
        myAudio.clip = RT_exp;
        myAudio.Play();
    }
        void PCT_title_Method(){
        myAudio.clip = PCT_title;
        myAudio.Play();
    }
        void PCT_exp_Method(){
        myAudio.clip = PCT_exp;
        myAudio.Play();
    }
        void Loop_title_Method(){
        myAudio.clip = Loop_title;
        myAudio.Play();
    }
        void Loop_exp_Method(){
        myAudio.clip = Loop_exp;
        myAudio.Play();
    }
        void DCT_title_Method(){
        myAudio.clip = DCT_title;
        myAudio.Play();
    }
        void DCT_exp_Method(){
        myAudio.clip = DCT_exp;
        myAudio.Play();
    }
        void CD_title_Method(){
        myAudio.clip = CD_title;
        myAudio.Play();
    }
        void CD_exp_Method(){
        myAudio.clip = CD_exp;
        myAudio.Play();
    }
        void Uretus_title_Method(){
        myAudio.clip = Uretus_title;
        myAudio.Play();
        //line
        Line_Uretes.SetActive(true);
    }
        void Uretus_exp_Method(){
        myAudio.clip = Uretus_exp;
        myAudio.Play();
    }
        void Unrinary_title_Method(){
        myAudio.clip = Unrinary_title;
        myAudio.Play();
    }
        void Unrinary_exp_Method(){
        myAudio.clip = Unrinary_exp;
        myAudio.Play();
    }
        void Uretra_title_Method(){
        myAudio.clip = Uretra_title;
        myAudio.Play();
        //line
        Line_Uretra.SetActive(true);
        Line_Urinary.SetActive(false);
    }
        void Uretra_exp_Method(){
        myAudio.clip = Uretra_exp;
        myAudio.Play();
    }




        //GoTo GamePlay
        public void GoToGamePlay(){
            //Load objective into static variable
            StaticVariables.Human_Objective = "Kidney";
        //open game scene
        GameObject loader = GameObject.Find("Sceneloader Canvas");
        loader.GetComponent<SceneLoader>().LoadScene(4);
        //SceneManager.LoadScene("HumanBody"); 
    }



}
