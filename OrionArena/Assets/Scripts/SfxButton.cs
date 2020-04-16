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
        PlaySound(2);
    }

    public void PlaySound(int i)
    {
        audio.clip = clips[i];
        audio.Play();
    }
}
