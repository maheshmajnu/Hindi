using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Step
{
    public string stepLine;
    public UnityEvent stepEvent;
}

[System.Serializable]
public class Objective
{
    public string objective;
    public List<Step> steps = new List<Step>();
}

public class ObjectiveController : MonoBehaviour
{
    [HideInInspector] public PlayerFunctionsController playerFunctionsController;
    public GameObject mainPlayer;

    [SerializeField] private GameObject missionFailedPanel;
    [SerializeField] private GameObject missionComplatedPanel;

    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI stepText;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;

    public List<Objective> objectives = new List<Objective>();

    public int currentStep;
    [SerializeField] private bool isLastStep;
    [SerializeField] private bool isLastObjective;
    public int currentObjective;

    public AudioSource objectSoundEffect;

    private void Start()
    {
        

        objectiveText.text = objectives[currentObjective].objective;
        stepText.text = objectives[currentObjective].steps[currentStep].stepLine;
    }

    private void Update()
    {
        
    }

    public void InvokeEvent()
    {
        if (objectives[currentObjective].steps[currentStep].stepEvent != null) objectives[currentObjective].steps[currentStep].stepEvent.Invoke();
    }

    [ContextMenu("STEP COMPLETED")]
    public void StepCompleted()
    {
        InventoryManager.Instance.gameObject.GetComponent<GamePlayManager>().HidePickUpPopUp();

        //mainPlayer = GameObject.Find("TPP_Player");
        ///* clear nearby objects List */
        //if (mainPlayer.activeInHierarchy && mainPlayer != null) // bug fix
        //{
        //    playerFunctionsController = mainPlayer.GetComponent<PlayerFunctionsController>();
        //    PlayerPickup playerpickup = playerFunctionsController.playerPickup;
        //    playerpickup.nearbyPickupObjects.Clear();
        //}
        ///* clear nearby objects List */

        if (currentObjective == objectives.Count - 1)
        {
            isLastObjective = true;
        }

        if(objectSoundEffect != null)
        {
            objectSoundEffect.Play();
        }

        if (isLastObjective)
        {
            if(currentStep == objectives[currentObjective].steps.Count - 1)
            {
                objectiveText.color = Color.green;
                Invoke("LastObjective", 1f);
            }
        }
        else if (currentStep == objectives[currentObjective].steps.Count - 1)
        {
            isLastStep = true;
            objectiveText.color = Color.green;
            Invoke("NextObjective", 1f);
        }

        EmptyCheckBox.SetActive(false);
        GreenCheckBox.SetActive(true);
        stepText.color = Color.green;

        Invoke("NextStep", 1f);

        
    }

    public void NextStep()
    {
        Debug.Log("NEXT STEP");
        if ((isLastStep))
        {
            currentStep = 0;
            isLastStep = false;
        }
        else
        {
            currentStep++;
        }

        stepText.text = objectives[currentObjective].steps[currentStep].stepLine;
        EmptyCheckBox.SetActive(true);
        GreenCheckBox.SetActive(false);
        stepText.color = Color.white;


    }

    public void NextObjective()
    {
        currentObjective++;
        objectiveText.color = new Color(255, 197, 0, 1);
        objectiveText.text = objectives[currentObjective].objective;

        if (currentObjective == objectives.Count - 1)
        {
            isLastObjective = true;
        }
    }

    void LastObjective()
    {
        objectiveText.color = Color.green;
        GreenCheckBox.SetActive(true);
        stepText.color = Color.green;
        objectiveText.color = Color.green;
        Invoke("missionPassed", 2f);
    }

    void missionPassed()
    {
        Cursor.visible = true;
        GameObject missionCompletePrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/Mission Passed", typeof(GameObject));  // Load Player
        Instantiate(missionCompletePrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate mission-Complete canvas 
    }


    public void missionFailed()
    {
        Cursor.visible = true;
        GameObject missionCompletePrefabObj = (GameObject)Resources.Load("Player/Menu_Canvas_Prefabs/Mission Failed", typeof(GameObject));  // Load Player
        Instantiate(missionCompletePrefabObj, new Vector3(0, 0, 0), Quaternion.identity);  // Instantiate mission-Complete canvas 
    }
}
