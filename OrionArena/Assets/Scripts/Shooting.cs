using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField] PhotonView pv;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                pv.RPC("Shoot", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
