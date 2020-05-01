﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public PhotonView pv;
    public float force;
    Rigidbody2D rb;
    public float damage;
    public Transform gundir;
    public float timeExplode;
    public float timeThrow = 1f;
    float timeAt;
    public GameObject particleExplosion;

    public string killerName;
    public GameObject localPlayer;
    public GameObject shooter;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        killerName = localPlayer.GetComponent<PlayerController>().myName;
        rb = GetComponent<Rigidbody2D>();
        Invoke("Colider", 0.2f);
    }

    private void Update()
    {
        if(timeAt < timeThrow)
        { 
            transform.rotation = gundir.transform.rotation;
            Throw();
        }
        timeAt += Time.deltaTime;

        if (timeAt > timeExplode)
        {
           pv.RPC("DestroyBomb", RpcTarget.AllBuffered);
        }

    }
  
    [PunRPC]
    void DestroyBomb()
    {
        rb.bodyType = RigidbodyType2D.Static;

        GameObject obj = Instantiate(particleExplosion, transform.position, transform.rotation);
        obj.GetComponent<CFX_AutoDestructShuriken>().damage = damage;
        Destroy(gameObject,0.2f);
    }

    void Throw()
    {
        rb.AddForce(transform.right * force * Time.deltaTime, ForceMode2D.Impulse);
    }

    void Colider()
    {
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
