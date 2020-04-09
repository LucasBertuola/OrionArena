using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxButton : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audio.clip = clips[Random.Range(0, clips.Length)];
        audio.Play();
    }
}
