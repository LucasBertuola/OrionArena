using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AbilitysWeeb: Ability
{
    public Transform firePoint;
    public Transform gundir;
    public GameObject weebprefab;
    public float forceThrown;
    public float timeForAbility = 5;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        timeAt = timeForAbility;
    }

    public float timeAt;
    [SerializeField] PhotonView pv;
    private void Update()
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

    [PunRPC]
    public virtual void UseAbility()
    {
        
        GameObject shoot = Instantiate(weebprefab, firePoint.position,firePoint.rotation);
    
        shoot.GetComponent<WeebShoot>().player = gameObject;
        AudioSource audioRPC = shoot.GetComponent<WeebShoot>().audioObj;
        audioRPC.clip = soundAbility;
        audioRPC.Play();

    }
}
