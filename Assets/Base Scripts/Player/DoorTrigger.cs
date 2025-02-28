using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DoorTrigger : MonoBehaviour
{
    private Animator anim;

    public List<TargetController> equations;

    public UnityEvent balancedEvent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void CheckForDoorOpen(TargetController rotatable)
    {
        rotatable.rotationCount--;

        if(rotatable.rotationCount == 0)
        {
            rotatable.hasRotated = true;
            rotatable.gameObject.tag = "Untagged";
        }

        foreach (var controller in equations)
        {
            if(!controller.hasRotated)
            {
                return;
            }
        }

        if(anim != null) anim.SetTrigger("Trigger");
        if(balancedEvent != null) { balancedEvent.Invoke(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "BigMagnetBar")
        {
            Debug.Log("OpenDoor");
        }
    }
}
