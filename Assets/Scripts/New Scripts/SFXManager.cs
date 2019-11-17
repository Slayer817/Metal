using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip cockingClip1;
    public AudioClip cockingClip2;
    public AudioClip aimingSound;

    public AudioSource mainAudioSource;

    public bool aimSoundHasPlayed = false;

    private void Start()
    {
        mainAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }
}
