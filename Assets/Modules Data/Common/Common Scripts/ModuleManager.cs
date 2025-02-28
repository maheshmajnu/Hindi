using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayAnimation(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void PlayAnimationWithDelay(Animator anim)
    {
        StartCoroutine(AnimationDelay(anim));
    }

    IEnumerator AnimationDelay(Animator anim)
    {
        yield return new WaitForSeconds(2);
        anim.SetTrigger("Trigger");
    }

    public void TurnOnGameObject(GameObject go)
    {
        go.SetActive(true);
    }

    public void TurnOnGoDelay(GameObject go)
    {
        StartCoroutine(GoTurnOnDelay(go));
    }

    IEnumerator GoTurnOnDelay(GameObject go)
    {
        yield return new WaitForSeconds(2);
        go.SetActive(true);
    }

    public void TurnOffGameObject(GameObject go)
    {
        go.SetActive(false);
    }

    public void TurnOffGoDelay(GameObject go)
    {
        StartCoroutine(GoTurnOffDelay(go));
    }

    IEnumerator GoTurnOffDelay(GameObject go)
    {
        yield return new WaitForSeconds(2);
        go.SetActive(false);
    }
}
