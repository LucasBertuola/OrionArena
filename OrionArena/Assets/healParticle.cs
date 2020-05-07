using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healParticle : MonoBehaviour
{
    public float heal;
    float timeAt;
    public float timeHeal;
    public GameObject healthParticle;
    public GameObject player;

    private void Update()
    {

        timeAt += Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(5, 10), 0, -Vector2.up);
        foreach (RaycastHit2D hit in hits)
        {

       
        if(hit.collider != null)
        {
           

            if (hit.collider.tag == "Player" && player == hit.collider.gameObject && timeAt > timeHeal)
            {
                PhotonView target = hit.collider.gameObject.GetComponent<PhotonView>();

                target.RPC("Heal", RpcTarget.AllBuffered, heal); 
                Instantiate(healthParticle,new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 2,-3), Quaternion.Euler(0,90,0));
                timeAt = 0;
            }
        }
        }

    }
}
