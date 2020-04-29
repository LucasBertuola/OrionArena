using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField] PhotonView pv;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform[] firePoints;

    public GameObject audioObj;
    public AudioClip gunSound;


    [Header("Shoot")]
    public float spread = 5;
    public float fireRate = 1f;
    public float fireCD = 0f;
    public float damage = 10f;
    public bool isShotgun = false;
    public float travelTime = 1.5f;
    public float bulletSpeed = 50;

    //public float bulletForce = 20f;
    public GameObject fireparticle;
    public Transform fireMark;
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
                pv.RPC("SetFire", RpcTarget.All);
                pv.RPC("GunShot", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void SetFire()
    {
        Instantiate(fireparticle, fireMark);
    }

    [PunRPC]
    void Shoot()
    {
        if (isShotgun)
        {
            for (int i = 0; i < firePoints.Length; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
                bullet.GetComponent<Bullet>().damage = damage;
                bullet.GetComponent<Bullet>().destroyTime = travelTime;
                bullet.GetComponent<Bullet>().moveSpeed = bulletSpeed;
                bullet.GetComponent<Bullet>().localPlayer = this.gameObject;
            }
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().damage = damage;
            bullet.GetComponent<Bullet>().destroyTime = travelTime;
            bullet.GetComponent<Bullet>().moveSpeed = bulletSpeed;
            bullet.GetComponent<Bullet>().localPlayer = this.gameObject;
        }

        //bullet.transform.Rotate(0, 0, Random.Range(-spread, spread));
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }



    [PunRPC]
    public void GunShot()
    {
        GameObject audioAtual = Instantiate(audioObj, transform.position, Quaternion.identity);
        AudioSource audioRPC = audioAtual.GetComponent<AudioSource>();
        audioRPC.clip = gunSound;
        audioRPC.Play();
    }
}
