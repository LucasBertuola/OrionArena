using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Health : MonoBehaviourPun, IPunObservable
{
    public Slider healthSlider;
    public Image healthFill;

    public float healthMax = 100f;
    public float healthPoints;
    public bool isDissolving = false;
    public float fade = 1f;
    public bool isDead = false;

    public float shieldPoints = 0f;
    public bool shield = false;

    public Rigidbody2D rb;
    public SpriteRenderer[] sr;
    public BoxCollider2D boxCollider;
    public GameObject playerCanvas;
    public Shooting shooting;
    public Ability abilities;
    public GameObject killText;
    //public Material[] material;

    public PlayerController playerController;

    public Light2D shieldLight;

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

    private void Update()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("Dissolve", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void TakeDamage(float value)
    {
        if (shieldPoints <= 0)
        {
            healthPoints -= value;
            healthSlider.value = healthPoints;
            CheckHealth();

            if (shield == true)
            {
                foreach (SpriteRenderer mat in sr)
                {
                    mat.material.SetInt("_Shield", 0);
                }
                healthSlider.GetComponentInChildren<Image>().color = Color.red;
                healthFill.color = Color.green;
                shield = false;
                shieldLight.enabled = false;
            }            
        }
        else
        {
            shieldPoints -= value;
            healthSlider.value = shieldPoints;
            healthSlider.GetComponentInChildren<Image>().color = Color.green;
            healthFill.color = Color.blue;
        }

    }

    [PunRPC]
    public void Heal(float value)
    {
        if (healthPoints > healthMax)
        {
            healthPoints = healthMax;
        }
        else
        {
            healthPoints += value;
            healthSlider.value = healthPoints;
        }
    }

    [PunRPC]
    public void GainShield(float value)
    {
        shieldPoints += value;
        shieldLight.enabled = true;
        healthSlider.GetComponentInChildren<Image>().color = Color.green;
        healthFill.color = Color.blue;
        shield = true;

        foreach (SpriteRenderer mat in sr)
        {
            mat.material.SetInt("_Shield", 1);
        }


        if (shieldPoints > healthMax)
        {
            shieldPoints = healthMax;
        }
    }

    [PunRPC]
    public void Die()
    {
        isDissolving = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0,0);
        boxCollider.enabled = false;
        /*foreach (SpriteRenderer sprite in sr)
        {
            sprite.enabled = false;
        }*/
        playerCanvas.SetActive(false);
        if (abilities != null)
        {
            abilities.enabled = false;
        }
        playerController.flashlight.enabled = false;
        shooting.enabled = false;
    }

    [PunRPC]
    public void Revive()
    {
        rb.gravityScale = 5;
        boxCollider.enabled = true;
        /*foreach (SpriteRenderer sprite in sr)
        {
            sprite.enabled = true;
        }*/
        playerCanvas.SetActive(true);
        healthPoints = healthMax;
        healthSlider.value = healthPoints;
        shooting.enabled = true;
        if (abilities != null)
        {
            abilities.enabled = true;
        }
        playerController.fuelAmount = playerController.maxFuel;
        playerController.fuelSlider.value = playerController.maxFuel;
        playerController.flashlight.enabled = true;
        shieldPoints = 0;
        isDead = false;
    }

    [PunRPC]
    void KilledBy(string name)
    {
        GameObject go = Instantiate(killText, new Vector2(0, 0), Quaternion.identity);
        go.transform.SetParent(GameManager.instance.killFeed.transform, false);
        go.GetComponent<Text>().text = "You got killed by : " + name;
        go.GetComponent<Text>().color = Color.red;
        Destroy(go, 7);
    }

    [PunRPC]
    void YouKilled(string name)
    {
        GameObject go = Instantiate(killText, new Vector2(0, 0), Quaternion.identity);
        go.transform.SetParent(GameManager.instance.killFeed.transform, false);
        go.GetComponent<Text>().text = "You killed : " + name;
        go.GetComponent<Text>().color = Color.green;
        Destroy(go, 7);
    }

    [PunRPC]
    void SetIsDead(bool value)
    {
        isDead = value;
    }

    [PunRPC]
    void Dissolve()
    {
        if (healthPoints <= 0 && isDissolving)
        {
            foreach (SpriteRenderer mat in sr)
            {
                fade -= Time.deltaTime;

                if (fade <= 0f)
                {
                    fade = 0f;
                    isDissolving = false;
                }

                mat.material.SetFloat("_Fade", fade);
            }
        }
        else if (healthPoints > 0 && !isDissolving)
        {
            foreach (SpriteRenderer mat in sr)
            {
                fade += Time.deltaTime;

                if (fade >= 1f)
                {
                    fade = 1f;
                }

                mat.material.SetFloat("_Fade", fade);
            }
        }
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
