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

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(4, 4), 0, -Vector2.up);
        

            if (hit.collider != null)
            {


            PhotonView target = hit.collider.gameObject.GetComponent<PhotonView>();


            if (target != null && (!target.IsMine || target.IsSceneView))
            {
                if (target.tag == "Player")
                {
                    target.RPC("Disable", RpcTarget.AllBuffered, true);

                   
                    pv.RPC("DestroyHitWeeb", RpcTarget.All, target.gameObject);

                }
            }
        }
            
        


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();


        if (target != null && (!target.IsMine || target.IsSceneView))
        {
            if (target.tag == "Player")
            {
                Debug.Log("Atingiu");
                pv.RPC("DestroyHitWeeb", RpcTarget.All, target.gameObject);

            }
        }


    }

    [PunRPC]
    void DestroyWeeb()
    {
        Destroy(gameObject);
    }

    [PunRPC]
    void DestroyHitWeeb(GameObject playerHit, PhotonView target)
    {
        
        GameObject weebP = Instantiate(particleWeeb, playerHit.transform);
        weebP.GetComponent<WeebParticle>().playerHit = target;
        Destroy(gameObject);

    }
}


