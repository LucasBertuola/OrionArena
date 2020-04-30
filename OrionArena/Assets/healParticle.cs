using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healParticle : MonoBehaviour
{
    public float heal;
    private void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(5, 7), 0, -Vector2.up);
        if(hit.collider != null)
        {
            if(hit.collider.tag == "Player")
                 hit.collider.GetComponent<Health>().TakeDamage(heal);
        }
    }
}
