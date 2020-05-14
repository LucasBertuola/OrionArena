using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthPickup : MonoBehaviour
{
    public float healthRegen = 50f;
    public bool isRespawning = false;
    public float spawnTimer = 30f;

    private SpriteRenderer sr;
    private Collider2D pickupCollider;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        pickupCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (target != null)
        {
            if (target.tag == "Player")
            {
                target.RPC("Heal", RpcTarget.AllBuffered, healthRegen);
                DeactivatePickup();
                Invoke("ActivatePickup", spawnTimer);
            }
        }
    }

    void DeactivatePickup()
    {
        sr.enabled = false;
        pickupCollider.enabled = false;
    }

    void ActivatePickup()
    {
        sr.enabled = true;
        pickupCollider.enabled = true;
    }
}
