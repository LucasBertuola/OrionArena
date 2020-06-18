using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxButton : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audio;
    VolumeManager volumeManager;
    void Start()
    {
        volumeManager = GameObject.FindGameObjectWithTag("Volume").GetComponent<VolumeManager>();

        audio = GetComponent<AudioSource>();
        PlaySound(2);
    }

    private void Update()
    {
        audio.volume = volumeManager.sfx;    
    }

    public void PlaySound(int i)
    {

        audio.clip = clips[i];
        audio.Play();
    }
}
