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

    VolumeManager volumeManager;
    private void Start()
    {
        volumeManager = GameObject.FindGameObjectWithTag("Volume").GetComponent<VolumeManager>();

        killerName = localPlayer.GetComponent<PlayerController>().myName;
        audioObj.volume = volumeManager.sfx;

    }

    private void Update()
    {
        timeAt = Time.deltaTime;
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
                if (target != localPlayer.GetComponent<PhotonView>())
                {
                    target.RPC("TakeDamage", RpcTarget.AllBuffered, damage);
               
                    if (target.GetComponent<Health>().healthPoints <= 0 && !target.GetComponent<Health>().isDead)
                    {
                        target.RPC("SetIsDead", RpcTarget.AllBuffered, true);
                        Player gotKilled = target.Owner;
                        target.RPC("KilledBy", gotKilled, killerName);
                        target.RPC("YouKilled", localPlayer.GetComponent<PhotonView>().Owner, target.Owner.NickName);

                        GameManager.instance.PlaySoundVoice(1);

                        localPlayer.GetComponent<Points>().AddPoints();
                    }
                }
            }
        }

    }
}
