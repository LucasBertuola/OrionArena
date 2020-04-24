using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffLimits : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("RedPlayer") || collision.gameObject.CompareTag("BluePlayer"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(5000);
        }
    }
}
