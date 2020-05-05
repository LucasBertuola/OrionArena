﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShoot : MonoBehaviour
{
    public PhotonView pv;

    public float heal;
    public float speed;
    public Transform gun;
    public float timeExplode;
    float timeAt;
    public GameObject particleHeal;
    public GameObject player;
    public AudioSource audioObj;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }


    private void Update()
    {
        if(timeAt > 0.07f)
        {
            // transform.rotation = gundir;
            Physics2D.IgnoreLayerCollision(10, 10);

            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (timeAt > timeExplode)
            {
           pv.RPC("DestroyHeal", RpcTarget.AllBuffered);

            }

        }
        else
        {
            transform.position = gun.position;
        }

        timeAt += Time.deltaTime;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
           pv.RPC("DestroyHeal", RpcTarget.AllBuffered);
        }


    }

    [PunRPC]
    void DestroyHeal()
    {

        GameObject obj = Instantiate(particleHeal, transform.position, Quaternion.Euler(90, 0, 0));
        obj.GetComponent<healParticle>().heal = -heal;
        obj.GetComponent<healParticle>().player = player;

        Destroy(gameObject);
    }

    
}
