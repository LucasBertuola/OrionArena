using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class Bullet : MonoBehaviour
{
    public GameObject hitGroundEffect;
    public GameObject hitPlayerEffect;
    public float damage = 50;
    public float moveSpeed = 50;
    public float destroyTime = 1f;

    public string killerName;
    public GameObject localPlayer;
    public GameObject shooter;
    float timeAt;
    private void Start()
    {
        killerName = localPlayer.GetComponent<PlayerController>().myName;
    }

    private void Update()
    {
        Physics2D.IgnoreLayerCollision(10, 10);
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        timeAt += Time.deltaTime;
        if(timeAt > destroyTime)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        /*if (gameObject.CompareTag("RedBullet"))
        {
            if (collision.gameObject.CompareTag("BluePlayer") || collision.gameObject.CompareTag("Player"))
            {
                Instantiate(hitPlayerEffect, transform.position, transform.rotation);
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);

                //Debug.Log("Red" + collision.gameObject.GetComponent<Health>().healthPoints + collision.gameObject.name);

                DestroyBullet();
            }
            DestroyBullet();
        }

        else if (gameObject.CompareTag("BlueBullet"))
        {
            if (collision.gameObject.CompareTag("RedPlayer") || collision.gameObject.CompareTag("Player"))
            {
                Instantiate(hitPlayerEffect, transform.position, transform.rotation);
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);

                //Debug.Log("Blue" + collision.gameObject.GetComponent<Health>().healthPoints + collision.gameObject.name);

                DestroyBullet();
            }
            DestroyBullet();
        }

        else if (gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.CompareTag("BluePlayer") || collision.gameObject.CompareTag("RedPlayer")
                || collision.gameObject.CompareTag("Player"))
            {
                Instantiate(hitPlayerEffect, transform.position, transform.rotation);
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);

                //Debug.Log("Bullet" + collision.gameObject.GetComponent<Health>().healthPoints + collision.gameObject.gameObject.name);

                DestroyBullet();
            }
            DestroyBullet();
        }*/

        if (target != null && (!target.IsMine || target.IsSceneView))
        {
            if (target.tag == "Player")
            {
                target.RPC("TakeDamage", RpcTarget.AllBuffered, damage);
                Instantiate(hitPlayerEffect, transform.position, transform.rotation);

                if (target.GetComponent<Health>().healthPoints <= 0 && !target.GetComponent<Health>().isDead)
                {
                    target.RPC("SetIsDead", RpcTarget.AllBuffered, true);
                    Player gotKilled = target.Owner;
                    target.RPC("KilledBy", gotKilled, killerName);
                    target.RPC("YouKilled", localPlayer.GetComponent<PhotonView>().Owner, target.Owner.NickName);

                    target.RPC("PlaySoundVoice", RpcTarget.AllBuffered);
                        localPlayer.GetComponent<Points>().AddPoints();
                }
            }
            
            DestroyBullet();
        }


        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Instantiate(hitGroundEffect, transform.position, transform.rotation);
            DestroyBullet();
        }



    }

    void DestroyBullet()
    {
       // PhotonNetwork.Destroy(gameObject);
        Destroy(gameObject);
    }

    void SpawnEffect()
    {
        Instantiate(hitGroundEffect, transform.position, transform.rotation);
    }

}