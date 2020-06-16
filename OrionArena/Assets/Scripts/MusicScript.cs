using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip[] clips;
    AudioSource audio;
    VolumeManager volumeManager;
    private void Start()
    {
        volumeManager = GameObject.FindGameObjectWithTag("Volume").GetComponent<VolumeManager>();

        audio = GetComponent<AudioSource>();
        audio.clip = clips[ Random.Range(0, clips.Length)];
        audio.Play();

    }
    private void Update()
    {
       audio.volume = volumeManager.music / 2;
        Debug.Log("Volume " + VolumeManager.volume.music); 
    }
}
