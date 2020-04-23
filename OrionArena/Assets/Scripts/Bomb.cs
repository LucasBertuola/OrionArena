using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float force;
    Rigidbody2D rb;
    public float damage;
    public Transform gundir;
    public float timeExplode;
    public float timeThrow = 1f;
    float timeAt;
    public GameObject particleExplosion;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("Colider", 0.2f);
    }

    private void Update()
    {
        if(timeAt < timeThrow)
        { 
            transform.rotation = gundir.transform.rotation;
            Throw();
        }
        timeAt += Time.deltaTime;

        if (timeAt > timeExplode)
        {
            DestroyBomb();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("BluePlayer") || collision.gameObject.CompareTag("RedPlayer")
               || collision.gameObject.CompareTag("Player"))
        {
            //pv.RPC("SpawnEffect", RpcTarget.AllBuffered);
            
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            //target.RPC("TakeDamage", RpcTarget.AllBuffered, damage);

            //Debug.Log("Bullet" + collision.gameObject.GetComponent<Health>().healthPoints + collision.gameObject.gameObject.name);

            //pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
            DestroyBomb();
        }
        //pv.RPC("DestroyBullet", RpcTarget.AllBuffered);
     
    }
    void DestroyBomb()
    {
        rb.bodyType = RigidbodyType2D.Static;

        GameObject obj = Instantiate(particleExplosion, transform.position, transform.rotation);
        obj.GetComponent<CFX_AutoDestructShuriken>().damage = damage;
        Destroy(gameObject,0.2f);
    }

    void Throw()
    {
        
        rb.AddForce(transform.right * force * Time.deltaTime, ForceMode2D.Impulse);
    }

    void Colider()
    {
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
