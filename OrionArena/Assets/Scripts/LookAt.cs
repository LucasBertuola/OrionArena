using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] PhotonView pv;

    public GameObject player;

    public float offset;

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
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = offset;
        Vector3 difference = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            //transform.rotation = Quaternion.Euler(0f, 0f, -angle);
            transform.localScale = -aimLocalScale;
            aimLocalScale.y = 1f;
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0f, 0f, angle);
            transform.localScale = aimLocalScale;
            aimLocalScale.y = -1f;
        }
        //transform.localScale = aimLocalScale;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
