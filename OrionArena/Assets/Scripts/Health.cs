using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Health : MonoBehaviourPun
{
    public Slider healthSlider;

    public float healthMax = 100f;
    public float healthPoints;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D boxCollider;
    public GameObject playerCanvas;

    public PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        healthPoints = healthMax;
        healthSlider.maxValue = healthMax;
        healthSlider.value = healthPoints;
    }

    public void TakeDamage(float value)
    {
        healthPoints -= value;
        healthSlider.value = healthPoints;


        if (photonView.IsMine && healthPoints <= 0)
        {
            GameManager.instance.EnableRespawn();
            playerController.disableInputs = true;
            photonView.RPC("Die", RpcTarget.AllBuffered);
        }

    }

    [PunRPC]
    public void Die()
    {
        rb.gravityScale = 0;
        boxCollider.enabled = false;
        sr.enabled = false;
        playerCanvas.SetActive(false);
    }

    [PunRPC]
    public void Revive()
    {
        rb.gravityScale = 5;
        boxCollider.enabled = true;
        sr.enabled = true;
        playerCanvas.SetActive(true);
        healthPoints = healthMax;
        healthSlider.value = healthPoints;
    }

    public void EnableInputs()
    {
        playerController.disableInputs = false;
    }

}
