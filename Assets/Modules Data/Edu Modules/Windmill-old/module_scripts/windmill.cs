using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class windmill : MonoBehaviour
{
    [Header("Gameplay-canvas")]
    public GameObject Objective_canvas; 
    public TextMeshProUGUI Objective;
    public TextMeshProUGUI Steps;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;

    [Header("Gameplay-Objects")]
    public Camera MainCam;
    public Camera RayCam;
    private Camera TempCam;

    public GameObject CMvcam1;
    //public GameObject MobileControlsUI;

    public GameObject rotor_lable;
    public GameObject gearbox_lable;
    public GameObject shaft_lable;
    public GameObject anemometer_lable;
    public GameObject generator_lable;
    public GameObject nacelle_lable;
    public GameObject controller_lable;


    public Material newMaterialRef;
    public Material redMaterialRef;
    public Material defaultMaterial;
    public Material error_RedMaterial;
    /* public GameObject one;
    public GameObject two;
    public GameObject three;
    */

    private Animator anim;  
    GameObject hitObj;
    public GameObject lights;

    int logic=0;

    void Start()
    {
        //store TempCam in MainCam
        TempCam = MainCam;
    }
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;

        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //on mouse click 
        if (Input.GetMouseButtonDown(0))
        {

            //check if mouse click 
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "switch1")
                {
                    CMvcam1.SetActive(true);
                    //MobileControlsUI.SetActive(false); 
                    //Turn raycam ON after delay 2 sec
                    StartCoroutine(RayCamOn());
                }
                else if (hit.collider.gameObject.name == "switch")
                {
                    PlayerOnScreenControlUI_on();
                    RayCam.gameObject.SetActive(false);
                    CMvcam1.SetActive(false);
                    //MobileControlsUI.SetActive(true);
                    //switch back Raycam with Maincam
                    MainCam = TempCam;
                }

                //Arrangement logic
                else if (hit.collider != null && hit.collider.CompareTag("one"))
                {
                    if (logic == 0) 
                    {
                        rotor_lable.SetActive(false);

                        Debug.Log(hit.collider.gameObject.name);
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = newMaterialRef;

                        //animation Play
                        anim = hitObj.transform.parent.gameObject.GetComponent<Animator>();
                        anim.Play("Low Shaft Rod_low speed shaft");
                        //make it unclickable
                        hitObj.GetComponent<Collider>().enabled = false;

                        //delay
                        StartCoroutine(DefaultMat());
                        logic += 1;
                }
                }
                else if (hit.collider != null && hit.collider.CompareTag("two") ) {
                    if (logic == 1)
                    {
                        gearbox_lable.SetActive(false);

                        Debug.Log(hit.collider.gameObject.name);
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = newMaterialRef;

                        //animation Play
                        anim = hitObj.transform.parent.gameObject.GetComponent<Animator>();
                        anim.Play("Gear Box_gear box");
                        //make it unclickable
                        hitObj.GetComponent<Collider>().enabled = false;

                        //delay
                        StartCoroutine(DefaultMat());
                        logic += 1;
                    }
                    else {
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = redMaterialRef;
                        //delay
                        StartCoroutine(RedDefaultMat());
                    }
                }
                else if (hit.collider != null && hit.collider.CompareTag("three") )
                {
                    if (logic == 2)
                    {
                        shaft_lable.SetActive(false);
                        Debug.Log(hit.collider.gameObject.name);
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = newMaterialRef;

                        //animation Play
                        anim = hitObj.transform.parent.gameObject.GetComponent<Animator>();
                        anim.Play("High Speed Shaft_high spped shaft");
                        //make it unclickable
                        hitObj.GetComponent<Collider>().enabled = false;

                        //delay
                        StartCoroutine(DefaultMat());
                        logic += 1;
                    }
                    else
                    {
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = redMaterialRef;
                        //delay
                        StartCoroutine(RedDefaultMat());
                    }
                }
                else if (hit.collider != null && hit.collider.CompareTag("four"))
                {
                    if (logic == 3)
                    {
                        generator_lable.SetActive(false);

                        Debug.Log(hit.collider.gameObject.name);
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = newMaterialRef;

                        //animation Play
                        anim = hitObj.transform.parent.gameObject.GetComponent<Animator>();
                        anim.Play("Generator_generator");
                        //make it unclickable
                        hitObj.GetComponent<Collider>().enabled = false;

                        //delay
                        StartCoroutine(DefaultMat());
                        logic += 1;
                    }
                    else
                    {
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = redMaterialRef;
                        //delay
                        StartCoroutine(RedDefaultMat());
                    }
                }
                else if (hit.collider != null && hit.collider.CompareTag("five"))
                {
                    if (logic == 4)
                    {
                        controller_lable.SetActive(false);

                        Debug.Log(hit.collider.gameObject.name);
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = newMaterialRef;

                        //animation Play
                        anim = hitObj.transform.parent.gameObject.GetComponent<Animator>();
                        anim.Play("Controllor_controller");
                        //make it unclickable
                        hitObj.GetComponent<Collider>().enabled = false;

                        //delay
                        StartCoroutine(DefaultMat());
                        logic += 1;
                    }
                    else
                    {
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = redMaterialRef;
                        //delay
                        StartCoroutine(RedDefaultMat());
                    }
                }
                else if (hit.collider != null && hit.collider.CompareTag("six"))
                {
                    if (logic == 5)
                    {
                        nacelle_lable.SetActive(false);

                        Debug.Log(hit.collider.gameObject.name);
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = newMaterialRef;

                        //animation Play
                        anim = hitObj.transform.parent.gameObject.GetComponent<Animator>();
                        anim.Play("Nacelle_nacelle");
                        //make it unclickable
                        hitObj.GetComponent<Collider>().enabled = false;

                        //delay
                        StartCoroutine(DefaultMat());
                        logic += 1;
                    }
                    else
                    {
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = redMaterialRef;
                        //delay
                        StartCoroutine(RedDefaultMat());
                    }
                }
                else if (hit.collider != null && hit.collider.CompareTag("seven"))
                {
                    if (logic == 6)
                    {
                        anemometer_lable.SetActive(false);

                        Debug.Log(hit.collider.gameObject.name);
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = newMaterialRef;

                        //animation Play
                        anim = hitObj.transform.parent.gameObject.GetComponent<Animator>();
                        anim.Play("anemometer_anemometer");
                        //make it unclickable
                        hitObj.GetComponent<Collider>().enabled = false;

                        //delay
                        StartCoroutine(DefaultMat());
                        //rotate fan, turn on lights
                        GameObject.Find("Rotor").GetComponent<Animator>().Play("RotorAction");
                        lights.SetActive(true);

                        //finish
                        LastObjective();

                    }
                    else
                    {
                        hitObj = hit.collider.gameObject;
                        hitObj.GetComponent<Renderer>().material = redMaterialRef;
                        //delay
                        StartCoroutine(RedDefaultMat());
                    }
                }
            }
        }
    }

    

        IEnumerator RayCamOn()
    {
        yield return new WaitForSecondsRealtime(2f);
        //raycamon
        RayCam.transform.gameObject.SetActive(true);
        //assign raycam to maincam
        MainCam = RayCam;

    }
        IEnumerator DefaultMat()
    {
        yield return new WaitForSecondsRealtime(0.3f);

        hitObj.GetComponent<Renderer>().material = defaultMaterial;
    }
    IEnumerator RedDefaultMat()
    {
        yield return new WaitForSecondsRealtime(0.3f);

        hitObj.GetComponent<Renderer>().material = error_RedMaterial;
    }


     
    void LastObjective()
    {
        GreenCheckBox.SetActive(true);
        Steps.color = Color.green;
        Objective.color = Color.green;

        Invoke("missionPassed", 2f);

    }


    void PlayerOnScreenControlUI_on()
    {
        GameObject.Find("Crosshair-Canvas").GetComponent<Canvas>().enabled = true;
        var item = GameObject.Find("Mobile controls UI Canvas");
        if (item != null)
        {
            item.GetComponent<Canvas>().enabled = true;
        }
    }

    void missionPassed()
    {
        Cursor.visible = true;
        GameObject missionCompletePrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/Mission Passed", typeof(GameObject));  // Load Player
        Instantiate(missionCompletePrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate mission-Complete canvas 
    }




}
