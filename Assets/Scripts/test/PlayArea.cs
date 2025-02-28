using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private GameObject missionFailedPanel;
    [SerializeField] private GameObject missionComplatedPanel;
    [SerializeField] private TestSceneManager testSceneManager;
    [SerializeField] private float maxMissionTime;


    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI stepText;
    public GameObject GreenCheckBox;
    public GameObject EmptyCheckBox;
    [SerializeField] private TextMeshProUGUI timerText;

    public List<string> steps = new List<string>();
    public int currentStep;

    private void Start()
    {
        objectiveText.text = "Pick up the Sphere!";
        stepText.text = steps[currentStep];
    }

    private void Update()
    {
        if (!testSceneManager.isMissionCompleted)
            maxMissionTime -= Time.deltaTime;
        timerText.text = maxMissionTime.ToString("0");

        if (maxMissionTime <= 0)
        {
            missionFailedPanel?.SetActive(true);
            testSceneManager.isMissionCompleted = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //check if the mission is completed;
            if (testSceneManager.isMissionCompleted && maxMissionTime > 0)
            {
                //missionComplatedPanel?.SetActive(true);
                LastObjective();
            }
            else
            {
                missionFailedPanel?.SetActive(true);
            }
        }
    }

    public void NextObjective()
    {
        EmptyCheckBox.SetActive(false);
        GreenCheckBox.SetActive(true);
        stepText.color = Color.green;
        
        Invoke("StepCompleted", 1f);
    }

    public void StepCompleted()
    {
        currentStep++;
        stepText.text = steps[currentStep];
        EmptyCheckBox.SetActive(true);
        GreenCheckBox.SetActive(false);
        stepText.color = Color.white;
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
}
