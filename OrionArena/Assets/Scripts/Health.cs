using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HealthPoints = 100;
        
    public void TakeDamage(int value)
    {
        HealthPoints -= value;
        if(HealthPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
