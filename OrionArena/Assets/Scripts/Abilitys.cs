using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Abilitys : MonoBehaviour
{
    public Transform firePoint;
    public Transform gundir;
    public GameObject bombprefab;
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
        if (Input.GetButton("Fire2") && timeAt >= timeForAbility)
        {
            timeAt = 0;
            pv.RPC("UseAbility", RpcTarget.AllBuffered);
       
        }

        if(timeAt < timeForAbility)
        {
            timeAt += Time.deltaTime;
        }
        
    }

    [PunRPC]
    public virtual void UseAbility()
    {
        GameObject bomb = Instantiate(bombprefab, firePoint.position, firePoint.rotation);
        bomb.GetComponent<Bomb>().force = forceThrown;
        bomb.GetComponent<Bomb>().gundir = gundir;
        bomb.GetComponent<Bomb>().localPlayer = this.gameObject;

    }
}
