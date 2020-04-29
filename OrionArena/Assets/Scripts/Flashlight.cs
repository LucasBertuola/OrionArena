using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Flashlight : MonoBehaviour
{
    [SerializeField] PhotonView pv;

    public GameObject player;

    public float offset = -90f;

    void Start()
    {
        pv = player.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            Aiming();
        }
    }

    private void Aiming()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);
    }
}
