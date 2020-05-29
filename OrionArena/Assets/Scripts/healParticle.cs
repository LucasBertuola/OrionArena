using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healParticle : MonoBehaviour
{
    PhotonView pv;

    float destroyTime = 5;
    public float heal;
    float timeAt;
    float timeAt2;
    public float timeHeal;
    public GameObject healthParticle;
    public GameObject player;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (pv.IsMine)
        {
            timeAt += Time.deltaTime;
            timeAt2 += Time.deltaTime;

            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(5, 10), 0, -Vector2.up);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Player" && timeAt > timeHeal)
                    {
                        PhotonView target = hit.collider.gameObject.GetComponent<PhotonView>();

                        if (target == player.GetComponent<PhotonView>()) {
                            target.RPC("Heal", RpcTarget.AllBuffered, heal);
                            pv.RPC("HealParticle", RpcTarget.AllBuffered, hit.collider.gameObject.transform.position);
                            timeAt = 0;
                        }
                    }
                }
            }

            if(timeAt2 > 5)
            {
                PhotonNetwork.Destroy(gameObject);
            }

        }
    }

    [PunRPC]
    void HealParticle(Vector3 pos)
    {
        Instantiate(healthParticle, new Vector3(pos.x, pos.y + 2), Quaternion.Euler(0, 90, 0));
    }
}
