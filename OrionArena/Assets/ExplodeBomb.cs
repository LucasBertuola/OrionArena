using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ExplodeBomb : MonoBehaviour
{
	public float damage;
	public AudioSource audioObj;

    public string killerName;
    public GameObject localPlayer;
    public GameObject shooter;

    private void Start()
    {
        killerName = localPlayer.GetComponent<PlayerController>().myName;
    }

    void OnEnable()
	{
		Invoke("Hit", 0.2f);
	}

	private void Hit()
	{
		GetComponent<CircleCollider2D>().enabled = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();

        /*if (collision.gameObject.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<Health>().TakeDamage(damage);

		}*/

        if (target != null)
        {
            if (target.tag == "Player")
            {
                target.RPC("TakeDamage", RpcTarget.AllBuffered, damage);

                if (target.GetComponent<Health>().healthPoints <= 0 && !target.GetComponent<Health>().isDead)
                {
                    target.RPC("SetIsDead", RpcTarget.AllBuffered, true);
                    Player gotKilled = target.Owner;
                    target.RPC("KilledBy", gotKilled, killerName);
                    target.RPC("YouKilled", localPlayer.GetComponent<PhotonView>().Owner, target.Owner.NickName);
                    localPlayer.GetComponent<Points>().AddPoints();
                }
            }
        }

    }
}
