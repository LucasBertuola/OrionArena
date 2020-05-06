using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeebParticle : MonoBehaviour
{
    float timeAt;
    public float timeStun;
    public GameObject player;
    public PhotonView playerHit;
    private void Update()
    {

        timeAt += Time.deltaTime;
        if(timeAt > timeStun)
        {
             playerHit.RPC("Disable", RpcTarget.All,false);
            Destroy(gameObject);
        }
    }

    
}
