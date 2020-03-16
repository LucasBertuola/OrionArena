using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitGroundEffect;
    public GameObject hitPlayerEffect;
    public int damage = 10;

    private void Update()
    {
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("RedBullet"))
        {
            if (collision.collider.gameObject.CompareTag("BluePlayer") || collision.collider.gameObject.CompareTag("Player"))
            {
                Instantiate(hitPlayerEffect, transform.position, transform.rotation);
                collision.collider.GetComponent<Health>().TakeDamage(damage);

                //Debug.Log("Red" + collision.collider.gameObject.GetComponent<Health>().HealthPoints);

                Destroy(gameObject);
            }
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("BlueBullet"))
        {
            if (collision.collider.gameObject.CompareTag("RedPlayer") || collision.collider.gameObject.CompareTag("Player"))
            {
                Instantiate(hitPlayerEffect, transform.position, transform.rotation);
                collision.collider.GetComponent<Health>().TakeDamage(damage);

                //Debug.Log("Blue" + collision.collider.gameObject.GetComponent<Health>().HealthPoints);
              
            }
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Bullet"))
        {        
            if (collision.collider.gameObject.CompareTag("BluePlayer") || collision.collider.gameObject.CompareTag("RedPlayer") 
                || collision.collider.gameObject.CompareTag("Player"))
            {
                Instantiate(hitPlayerEffect, transform.position, transform.rotation);
                collision.collider.GetComponent<Health>().TakeDamage(damage);

                //Debug.Log("Bullet" + collision.collider.gameObject.GetComponent<Health>().HealthPoints);

                Destroy(gameObject);
            }
            Destroy(gameObject);
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Instantiate(hitGroundEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
