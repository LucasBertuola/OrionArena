﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AbilitysBomb : Ability
{
    public Transform firePoint;
    public Transform gundir;
    public GameObject bombprefab;
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
                bar.ResetAbility();
                UseAbility();

            }

            if (timeAt < timeForAbility)
            {
                timeAt += Time.deltaTime;

            }
        }
        
    }


    public virtual void UseAbility()
    {
        GameObject bomb = PhotonNetwork.Instantiate("Bomb", firePoint.position, firePoint.rotation);

        bomb.GetComponent<Bomb>().force = forceThrown;
        bomb.GetComponent<Bomb>().gundir = gundir;
        bomb.GetComponent<Bomb>().localPlayer = gameObject;
        AudioSource audioRPC = bomb.GetComponent<Bomb>().audioObj;
        audioRPC.clip = soundAbility;
        audioRPC.volume = volumeManager.sfx;
        audioRPC.Play();
    }
}
