using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeebParticle : MonoBehaviour
{
    float timeAt;
    public float timeStun;
    public GameObject player;
    public GameObject playerHit;
    private void Update()
    {

        timeAt += Time.deltaTime;
        if(timeAt > timeStun)
        {
            playerHit.GetComponent<PlayerController>().disableInputs = false;
        }
    }

    
}
