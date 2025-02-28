
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CheckpointData
{
    public int checkpointValue;
    public int currentStep;
    public int currentObjective;
}

public class CheckpointManager : MonoBehaviour
{
    //public GameObject gameManager;
    public ObjectiveController objectiveController;
    public static CheckpointManager Instance { get; private set; }
    public CheckpointData currentCheckpointData = new CheckpointData();
    private string currentSceneName;

    private void Awake()
    {
        

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void DestroyCheckpointManager()
    {
        if (Instance == this)
        {
            Instance = null; // Clear the static reference
            Destroy(gameObject); // Destroy the GameObject
            Debug.Log("CheckpointManager destroyed.");
        }
    }



    public void SaveCheckpoint(int checkpointValue, int stepValue, int objectiveValue)
    {
        currentCheckpointData.checkpointValue = checkpointValue;
        currentCheckpointData.currentStep = stepValue;
        currentCheckpointData.currentObjective = objectiveValue;

        Debug.Log($"Checkpoint saved: {checkpointValue}, Step: {stepValue}, Objective: {objectiveValue}");
    }

    public (int checkpoint, int currentStep, int currentObjective) LoadCheckpoint()
    {
        
        Debug.Log($"Checkpoint loaded: {currentCheckpointData.checkpointValue}, Step: {currentCheckpointData.currentStep}, Objective: {currentCheckpointData.currentObjective}");
        return (currentCheckpointData.checkpointValue, currentCheckpointData.currentStep, currentCheckpointData.currentObjective);
        
    }

    public void ResetCheckpoints()
    {
        currentCheckpointData = new CheckpointData();
        Debug.Log("Checkpoint data reset in memory.");
    }

    public void RestoreCheckpoint()
    {
        var (checkpointValue, stepValue, objectiveValue) = LoadCheckpoint();

        if (objectiveController != null)
        {
            objectiveController.currentStep = stepValue;
            objectiveController.currentObjective = objectiveValue;

            Debug.Log("Checkpoint restored successfully.");
        }
        else
        {
            Debug.LogError("ObjectiveController not found! Ensure GameManager exists in the hierarchy.");
        }
    }
}
