using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip brown;
    public AudioClip cherry;
    public AudioClip grape;
    public AudioClip victory;
    public AudioClip fail;
    public AudioClip popup;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }




    
}
