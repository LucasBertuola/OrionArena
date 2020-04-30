using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShoot : MonoBehaviour
{
    public float heal;
    public float speed;
    Quaternion gundir;
    public float timeExplode;
    float timeAt;
    public GameObject particleHeal;
    public GameObject player;


    private void Update()
    {
       // transform.rotation = gundir;
        Physics2D.IgnoreLayerCollision(10, 10);

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (timeAt > timeExplode)
        {
            DestroyHeal();
        }

        timeAt += Time.deltaTime;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("BluePlayer") || collision.gameObject.CompareTag("RedPlayer")
               || collision.gameObject.CompareTag("Player"))
        {

            DestroyHeal();
        }


    }
    void DestroyHeal()
    {

        GameObject obj = Instantiate(particleHeal, transform.position, Quaternion.Euler(100, 0, 0));
        obj.GetComponent<healParticle>().heal = -heal;
        obj.GetComponent<healParticle>().player = player;

        Destroy(gameObject);
    }

    public void defineDir(Quaternion qua)
    {
        gundir = qua;
    } 

}
