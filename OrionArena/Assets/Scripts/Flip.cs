using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    [SerializeField] PhotonView pv;
    public GameObject player;

    private void Start()
    {
        pv = player.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            FlipBody();
        }    
    }

    public void FlipBody()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        Vector3 bodyLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            bodyLocalScale.x = -1f;
        }
        else
        {
            bodyLocalScale.x = +1f;
        }
        transform.localScale = bodyLocalScale;
    }
}
