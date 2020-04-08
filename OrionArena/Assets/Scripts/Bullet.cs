using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    public GameObject hitGroundEffect;
    public GameObject hitPlayerEffect;
    public float damage = 50;
    public float moveSpeed = 50;
    public float destroyTime = 1f;

    //private PhotonView pv;

    private void Start()
    {
        //pv = GetComponent<PhotonView>();

    }

    private void Update()
    {
        Physics2D.IgnoreLayerCollision(10, 10);
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        Destroy(gameObject, destroyTime);
        /*if (pv.IsMine)
        {
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0)
            {
                pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (!photonView.IsMine)
        {
            return;
        }*/

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        /*if (target != null && (!target.IsMine || target.IsSceneView))
        {*/
        if (gameObject.CompareTag("RedBullet"))
        {
            if (collision.gameObject.CompareTag("BluePlayer") || collision.gameObject.CompareTag("Player"))
            {
                //pv.RPC("SpawnEffect", RpcTarget.AllBuffered);
                Instantiate(hitGroundEffect, transform.position, transform.rotation);
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                //target.RPC("TakeDamage", RpcTarget.AllBuffered, damage);

                //Debug.Log("Red" + collision.gameObject.GetComponent<Health>().healthPoints + collision.gameObject.name);

                //pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
                DestroyBullet();
            }
            //pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
            DestroyBullet();
        }

        else if (gameObject.CompareTag("BlueBullet"))
        {
            if (collision.gameObject.CompareTag("RedPlayer") || collision.gameObject.CompareTag("Player"))
            {
                //pv.RPC("SpawnEffect", RpcTarget.AllBuffered);
                Instantiate(hitGroundEffect, transform.position, transform.rotation);
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                //target.RPC("TakeDamage", RpcTarget.AllBuffered, damage);

                //Debug.Log("Blue" + collision.gameObject.GetComponent<Health>().healthPoints + collision.gameObject.name);

                //pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
                DestroyBullet();
            }
            //pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
            DestroyBullet();
        }

        else if (gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.CompareTag("BluePlayer") || collision.gameObject.CompareTag("RedPlayer")
                || collision.gameObject.CompareTag("Player"))
            {
                //pv.RPC("SpawnEffect", RpcTarget.AllBuffered);
                Instantiate(hitGroundEffect, transform.position, transform.rotation);
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                //target.RPC("TakeDamage", RpcTarget.AllBuffered, damage);

                //Debug.Log("Bullet" + collision.gameObject.GetComponent<Health>().healthPoints + collision.gameObject.gameObject.name);

                //pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
                DestroyBullet();
            }
            //pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
            DestroyBullet();
        }


        //}


        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Instantiate(hitGroundEffect, transform.position, transform.rotation);
            DestroyBullet();
            //pv.RPC("SpawnEffect", RpcTarget.AllBuffered);
            //pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
        }



    }

    //[PunRPC]
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    //[PunRPC]
    void SpawnEffect()
    {
        Instantiate(hitGroundEffect, transform.position, transform.rotation);
    }

    /*IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(destroyTime);
        pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
    }*/

}