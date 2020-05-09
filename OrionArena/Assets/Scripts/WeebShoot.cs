
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WeebShoot : MonoBehaviour
{
    PhotonView pv;
    public float speed;
        public Transform gun;
      public float timeExplode;
       float timeAt;
        public GameObject particleWeeb;
        public GameObject player;
        public AudioSource audioObj;

    private void Update()
    {
        pv = GetComponent<PhotonView>();
        Physics2D.IgnoreLayerCollision(10, 10);
        transform.Translate(Vector2.right * speed * Time.deltaTime);


        timeAt += Time.deltaTime;
        if (timeAt > timeExplode)
        {
            pv.RPC("DestroyNet", RpcTarget.AllBuffered);
        }

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(5, 10), 0, -Vector2.up);
     
            if (hit.collider != null)
            {
            PhotonView target = hit.collider.gameObject.GetComponent<PhotonView>();

            if (target != null && (!target.IsMine || target.IsSceneView))
            {
                if (target.tag == "Player")
                {
                    target.RPC("Disable", RpcTarget.AllBuffered, true);
                    GameObject weebP = Instantiate(particleWeeb, target.gameObject.transform);
                    weebP.GetComponent<WeebParticle>().playerHit = target;
                }

              pv.RPC("DestroyNet", RpcTarget.AllBuffered);

            }
        }
        
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        if (target != null && (!target.IsMine || target.IsSceneView))
        {
            if (target.tag == "Player")
            {
                target.RPC("Disable", RpcTarget.AllBuffered, true);
                GameObject weebP =  Instantiate(particleWeeb, target.gameObject.transform);
                weebP.GetComponent<WeebParticle>().playerHit = target;
            }

            DestroyNet();
        }
    }

    [PunRPC]
    void DestroyNet()
    {
        PhotonNetwork.Destroy(gameObject);
    }



}
