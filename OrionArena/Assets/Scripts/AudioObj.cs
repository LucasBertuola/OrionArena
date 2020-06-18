using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class AudioObj : MonoBehaviour
{
    VolumeManager volumeManager;

    [PunRPC]
    void Start()
    {
        volumeManager = GameObject.FindGameObjectWithTag("Volume").GetComponent<VolumeManager>();

        GetComponent<AudioSource>().volume = volumeManager.sfx;
        Destroy(gameObject, 1);
    }


}
