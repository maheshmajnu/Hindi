using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationStart : MonoBehaviour
{
    public Animator animator; // Assign your Animator in the Inspector

    [Header("Animation Events")]
    public UnityEvent onAnimationEventTriggered;

    public void PlayAnimTrigg()
    {
        animator.SetTrigger("Trigger"); // Replace "Trigger" with the actual name of your trigger
    }

    public void PlayBoolTrue()
    {
        animator.SetBool("Bool", true);
    }

    public void PlayBoolFalse()
    {
        animator.SetBool("Bool", false);
    }

    // Method called by the Animation Event
    public void AnimationEventTrigger()
    {
        Debug.Log("Animation event triggered!");
        onAnimationEventTriggered?.Invoke();
    }
}
