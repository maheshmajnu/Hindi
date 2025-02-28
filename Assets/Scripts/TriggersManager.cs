using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggersManager : MonoBehaviour
{
    private TestSceneManager testSceneManager;

    public Animator doorAnimator;
    [SerializeField] private string triggerName;
    public List<ItemObject> items = new List<ItemObject>();

    public int maxCount;
    public int currentCount;
    public bool hasFilled;

    public ObjectiveController objectiveController;
    public int objectiveIndex;
    public int stepIndex;

    public UnityEvent checkEvent;
    public UnityEvent defaultEvent;
    public UnityEvent wrongEvent;

    private void Start()
    {
        testSceneManager = FindObjectOfType<TestSceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            ItemObject itmObj = other.gameObject.GetComponent<ItemObject>();
            if (itmObj != null)
            {
                if (defaultEvent != null) defaultEvent.Invoke();
                items.Add(itmObj);
                if (itmObj.item?.itemName == triggerName)
                {
                    currentCount++;
                    if (currentCount == maxCount)
                    {
                        foreach(ItemObject itm in items)
                        {
                            if (itm.item.itemName != triggerName) return;
                        }

                        hasFilled = true;
                        if(checkEvent != null) checkEvent.Invoke();
                    }
                }
                else
                {
                    hasFilled = false;
                    if(wrongEvent != null) wrongEvent.Invoke();
                }
            }
        }
        else if(other.gameObject.tag == "Player")
        {
            if(objectiveIndex == objectiveController.currentObjective && stepIndex == objectiveController.currentStep)
            {
                gameObject.SetActive(false);
                objectiveController.InvokeEvent();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            CheckItems();
        }
    }

    public void CheckItems()
    {
        if(items != null)
        {
            foreach (ItemObject itmObj in items)
            {
                if (itmObj.item.itemName != triggerName) return;

                Debug.Log("Checking");
                if (currentCount >= maxCount) hasFilled = true;

                if (checkEvent != null) checkEvent.Invoke();
            }
        }
    }
}
