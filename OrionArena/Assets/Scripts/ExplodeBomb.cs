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

    float timeAt;
    private void Start()
    {
        killerName = localPlayer.GetComponent<PlayerController>().myName;
    }

    private void Update()
    {
        timeAt = Time.deltaTime;

        if (timeAt > 0.2 && timeAt < 0.4)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 10, Vector2.up);
            foreach (RaycastHit2D hit in hits)
            {

                if (hit.collider != null)
                {
                    PhotonView target = hit.collider.gameObject.GetComponent<PhotonView>();

                    if (target.tag == "Player")
                    {
                        target.RPC("TakeDamage", RpcTarget.AllBuffered, damage);

                        if (target.GetComponent<Health>().healthPoints <= 0 && !target.GetComponent<Health>().isDead)
                        {
                            target.RPC("SetIsDead", RpcTarget.AllBuffered, true);
                            Player gotKilled = target.Owner;
                            target.RPC("KilledBy", gotKilled, killerName);
                            target.RPC("YouKilled", localPlayer.GetComponent<PhotonView>().Owner, target.Owner.NickName);

                            target.RPC("PlaySoundVoice", RpcTarget.AllBuffered);

                            localPlayer.GetComponent<Points>().AddPoints();
                        }
                    }

                }
            }
        }
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
