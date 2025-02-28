using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventAudioPlay : MonoBehaviour
{
    [SerializeField]
    private AudioSource myAudio;
    [SerializeField]
    private AudioClip Audiofile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playClip()
    {
        myAudio.clip = Audiofile;
        myAudio.Play();
    }
}
