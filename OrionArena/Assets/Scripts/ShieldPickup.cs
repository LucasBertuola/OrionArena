using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShieldPickup : MonoBehaviour
{
    public float shieldRegen = 50f;
    public bool isRespawning = false;
    public float spawnTimer = 30f;

    private SpriteRenderer sr;
    private Collider2D pickupCollider;
    VolumeManager volumeManager;
    private void Start()
    {
        volumeManager = GameObject.FindGameObjectWithTag("Volume").GetComponent<VolumeManager>();

        sr = GetComponent<SpriteRenderer>();
        pickupCollider = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (target != null)
        {
            if (target.tag == "Player")
            {
                target.RPC("GainShield", RpcTarget.AllBuffered, shieldRegen);
                DeactivatePickup();
                Invoke("ActivatePickup", spawnTimer);
            }           
        }
    }

    void DeactivatePickup()
    {
        GetComponent<AudioSource>().volume = volumeManager.sfx;

        GetComponent<AudioSource>().Play();
        sr.enabled = false;
        pickupCollider.enabled = false;
    }

    void ActivatePickup()
    {
        sr.enabled = true;
        pickupCollider.enabled = true;
    }
}
