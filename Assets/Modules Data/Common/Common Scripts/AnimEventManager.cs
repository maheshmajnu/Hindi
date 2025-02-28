using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEventManager : MonoBehaviour
{
    //public class stringEvents : UnityEvent<string> { } 

    public UnityEvent UnityEvent1;
    public UnityEvent UnityEvent2;
    public UnityEvent UnityEvent3;
    public UnityEvent UnityEvent4;
    public UnityEvent UnityEvent5;
    public UnityEvent UnityEvent6;
    public UnityEvent UnityEvent7;
    public UnityEvent UnityEvent8;
    public UnityEvent UnityEvent9; 

    void AnimationEvent1()
    {
        UnityEvent1.Invoke();
    }
    void AnimationEvent2()
    {
        UnityEvent2.Invoke();
    }
    void AnimationEvent3()
    {
        UnityEvent3.Invoke();
    }

    void AnimationEvent4()
    {
        UnityEvent4.Invoke();
    }
    void AnimationEvent5()
    {
        UnityEvent5.Invoke();
    }
    void AnimationEvent6()
    {
        UnityEvent6.Invoke();
    }
    void AnimationEvent7()
    {
        UnityEvent7.Invoke();
    }
    void AnimationEvent8()
    {
        UnityEvent8.Invoke();
    }
    void AnimationEvent9()
    {
        UnityEvent9.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
