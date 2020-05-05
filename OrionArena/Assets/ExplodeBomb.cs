using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBomb : MonoBehaviour
{
	public float damage;
	public AudioSource audioObj;
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
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<Health>().TakeDamage(damage);

		}

	}
}
