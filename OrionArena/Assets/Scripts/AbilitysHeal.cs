using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AbilitysHeal : Ability
{
    [SerializeField] PhotonView pv;

    public Transform firePoint;
    public Transform gundir;
    public GameObject healprefab;
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
        
        GameObject shoot = PhotonNetwork.Instantiate("HealShoot", firePoint.position,firePoint.rotation);
        shoot.GetComponent<HealShoot>().gun = firePoint;
        shoot.GetComponent<HealShoot>().player = gameObject;
        AudioSource audioRPC = shoot.GetComponent<HealShoot>().audioObj;
        audioRPC.clip = soundAbility;
        audioRPC.volume = volumeManager.sfx;
        audioRPC.Play();

    }
}
