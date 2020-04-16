using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Health : MonoBehaviourPun, IPunObservable
{
    public Slider healthSlider;

    public float healthMax = 100f;
    public float healthPoints;

    public Rigidbody2D rb;
    public SpriteRenderer[] sr;
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
        UpdateHealth();
    }

    public void TakeDamage(float value)
    {
        healthPoints -= value;
        UpdateHealth();

        if (photonView.IsMine && healthPoints <= 0)
        {
            GameManager.instance.EnableRespawn();
            playerController.disableInputs = true;
            photonView.RPC("Die", RpcTarget.AllBuffered);
        }
    }

    public void UpdateHealth()
    {
        healthSlider.value = healthPoints;
    }

    [PunRPC]
    public void Die()
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0,0);
        boxCollider.enabled = false;
        foreach (SpriteRenderer sprite in sr)
        {
            sprite.enabled = false;
        }
        playerCanvas.SetActive(false);
        shooting.enabled = false;
    }

    [PunRPC]
    public void Revive()
    {
        rb.gravityScale = 5;
        boxCollider.enabled = true;
        foreach (SpriteRenderer sprite in sr)
        {
            sprite.enabled = true;
        }
        playerCanvas.SetActive(true);
        healthPoints = healthMax;
        UpdateHealth();
        shooting.enabled = true;
        playerController.fuelAmount = playerController.maxFuel;
        playerController.fuelSlider.value = playerController.maxFuel;
    }

    public void EnableInputs()
    {
        playerController.disableInputs = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(healthPoints);
        }
        else
        {
            this.healthPoints = (float)stream.ReceiveNext();
        }
    }
}
