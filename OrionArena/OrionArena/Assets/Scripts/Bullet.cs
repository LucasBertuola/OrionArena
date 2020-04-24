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

    public string killerName;
    public GameObject localPlayer;

    private void Update()
    {
        Physics2D.IgnoreLayerCollision(10, 10);
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (gameObject.CompareTag("RedBullet"))
        {
            if (collision.gameObject.CompareTag("BluePlayer") || collision.gameObject.CompareTag("Player"))
            {
                Instantiate(hitGroundEffect, transform.position, transform.rotation);
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
                Instantiate(hitGroundEffect, transform.position, transform.rotation);
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
                Instantiate(hitGroundEffect, transform.position, transform.rotation);
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);

                //Debug.Log("Bullet" + collision.gameObject.GetComponent<Health>().healthPoints + collision.gameObject.gameObject.name);

                DestroyBullet();
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
        Destroy(gameObject);
    }

    void SpawnEffect()
    {
        Instantiate(hitGroundEffect, transform.position, transform.rotation);
    }

}