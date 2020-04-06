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
    public Shooting shooting;

    public PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        shooting = GetComponent<Shooting>();
        healthPoints = healthMax;
        healthSlider.maxValue = healthMax;
        photonView.RPC("UpdateHealth", RpcTarget.AllBuffered);
    }

    public void TakeDamage(float value)
    {
        healthPoints -= value;
        photonView.RPC("UpdateHealth", RpcTarget.AllBuffered);

        if (photonView.IsMine && healthPoints <= 0)
        {
            GameManager.instance.EnableRespawn();
            playerController.disableInputs = true;
            photonView.RPC("Die", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void UpdateHealth()
    {
        healthSlider.value = healthPoints;
    }

    [PunRPC]
    public void Die()
    {
        rb.gravityScale = 0;
        boxCollider.enabled = false;
        sr.enabled = false;
        playerCanvas.SetActive(false);
        shooting.enabled = false;
    }

    [PunRPC]
    public void Revive()
    {
        rb.gravityScale = 5;
        boxCollider.enabled = true;
        sr.enabled = true;
        playerCanvas.SetActive(true);
        healthPoints = healthMax;
        photonView.RPC("UpdateHealth", RpcTarget.AllBuffered);
        shooting.enabled = true;
    }

    public void EnableInputs()
    {
        playerController.disableInputs = false;
    }

}
