using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeebShoot : MonoBehaviour
{
    public PhotonView pv;

    
    public float speed;
    public Transform gun;
    public float timeExplode;
    float timeAt;
    public GameObject particleWeeb;
    public GameObject player;
    public AudioSource audioObj;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
     
            // transform.rotation = gundir;
        Physics2D.IgnoreLayerCollision(10, 10);

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (timeAt > timeExplode)
        {
           pv.RPC("DestroyWeeb", RpcTarget.All);

        }

        timeAt += Time.deltaTime;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

           pv.RPC("DestroyHitWeeb", RpcTarget.All,collision.gameObject);
        }


    }

    [PunRPC]
    void DestroyWeeb()
    {
        Destroy(gameObject);
    }
    [PunRPC]
    void DestroyHitWeeb(GameObject playerHit)
    {
        PlayerController player = playerHit.GetComponent<PlayerController>();
        player.disableInputs = true;
        
        GameObject weebP = Instantiate(particleWeeb, playerHit.transform);
        weebP.GetComponent<WeebParticle>().playerHit = playerHit;
        Destroy(gameObject);

    }
}


