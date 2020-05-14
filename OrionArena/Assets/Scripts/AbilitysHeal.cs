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
    public float timeForAbility = 5;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        timeAt = timeForAbility;
    }

    public float timeAt;
    private void Update()
    { 
        if (pv.IsMine)
        {

          if (Input.GetButton("Fire2") && pv.IsMine && timeAt >= timeForAbility)
          {
            timeAt = 0;
            pv.RPC("UseAbility", RpcTarget.AllBuffered);

         }

        if (timeAt < timeForAbility)
        {
            timeAt += Time.deltaTime;
        }
        }
    }

    [PunRPC]
    public virtual void UseAbility()
    {
        
        GameObject shoot = PhotonNetwork.Instantiate("HealShoot", firePoint.position,firePoint.rotation);
        shoot.GetComponent<HealShoot>().gun = firePoint;
        shoot.GetComponent<HealShoot>().player = gameObject;
        AudioSource audioRPC = shoot.GetComponent<HealShoot>().audioObj;
        audioRPC.clip = soundAbility;
        audioRPC.Play();

    }
}
