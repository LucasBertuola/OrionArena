using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioClip[] clips;
    private void Start()
    {
        GetComponent<AudioSource>().clip = clips[ Random.Range(0, clips.Length)];
        GetComponent<AudioSource>().Play();

    }
}
