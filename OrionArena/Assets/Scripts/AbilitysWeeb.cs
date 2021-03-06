﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

public class AbilitysWeeb : Ability
{
    public Transform firePoint;
    public Transform gundir;
    public GameObject weebprefab;
    public float forceThrown;

    VolumeManager volumeManager;

    private void Start()
    {
        volumeManager = GameObject.FindGameObjectWithTag("Volume").GetComponent<VolumeManager>();

        pv = GetComponent<PhotonView>();
        timeAt = timeForAbility;

        if (pv.IsMine)
        {
            progressBar.SetActive(true);
        }
        else
        {
            progressBar.SetActive(false);
        }
    }

    [SerializeField] PhotonView pv;
    private void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetButton("Fire2") && pv.IsMine && timeAt >= timeForAbility)
            {
                timeAt = 0;
                UseAbility();
                bar.ResetAbility();

            }

            if (timeAt < timeForAbility)
            {
                timeAt += Time.deltaTime;
            }
        }

    }

    public virtual void UseAbility()
    {
        
            GameObject shoot = PhotonNetwork.Instantiate("NetShot", firePoint.position, firePoint.rotation);

            shoot.GetComponent<WeebShoot>().player = gameObject;
            AudioSource audioRPC = shoot.GetComponent<WeebShoot>().audioObj;
            audioRPC.clip = soundAbility;
        audioRPC.volume = volumeManager.sfx;

        audioRPC.Play();
        
    }

  

}
