using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCena : MonoBehaviour
{
    public float moveSpeed = 50;
    public float destroyTime = 1f;

    float timeAt;
 

    private void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        timeAt += Time.deltaTime;
        if (timeAt > destroyTime)
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
