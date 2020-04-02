using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField] PhotonView pv;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float spread = 0;
    public float fireRate = 1f;
    public float fireCD = 0f;

    //public float bulletForce = 20f;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (fireCD > 0)
            {
                fireCD -= Time.deltaTime;
            }

            if (Input.GetButton("Fire1") && fireCD <= 0)
            {
                pv.RPC("Shoot", RpcTarget.AllBuffered);
                //Shoot();
                fireCD = fireRate;
            }
        }
    }

    [PunRPC]
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //bullet.transform.Rotate(0, 0, Random.Range(-spread, spread));
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
