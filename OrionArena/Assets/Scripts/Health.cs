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
    public Ability abilities;
    public GameObject killText;

    public PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        shooting = GetComponent<Shooting>();
        abilities = GetComponent<Ability>();
        healthPoints = healthMax;
        healthSlider.maxValue = healthMax;
        healthSlider.value = healthPoints;
    }

    public void CheckHealth()
    {
        if (photonView.IsMine && healthPoints <= 0)
        {
            GameManager.instance.EnableRespawn();
            playerController.disableInputs = true;
            photonView.RPC("Die", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void TakeDamage(float value)
    {
        healthSlider.value -= value;
        healthPoints = healthSlider.value;
        CheckHealth();
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
        abilities.enabled = false;
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
        healthSlider.value = healthPoints;
        shooting.enabled = true;
        abilities.enabled = true;
        playerController.fuelAmount = playerController.maxFuel;
        playerController.fuelSlider.value = playerController.maxFuel;
    }

    [PunRPC]
    void KilledBy(string name)
    {
        GameObject go = Instantiate(killText, new Vector2(0, 0), Quaternion.identity);
        go.transform.SetParent(GameManager.instance.killFeed.transform, false);
        go.GetComponent<Text>().text = "You got killed by : " + name;
        go.GetComponent<Text>().color = Color.red;
    }

    [PunRPC]
    void YouKilled(string name)
    {
        GameObject go = Instantiate(killText, new Vector2(0, 0), Quaternion.identity);
        go.transform.SetParent(GameManager.instance.killFeed.transform, false);
        go.GetComponent<Text>().text = "You killed : " + name;
        go.GetComponent<Text>().color = Color.green;
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
