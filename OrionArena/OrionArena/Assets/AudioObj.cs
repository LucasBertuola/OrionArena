using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class AudioObj : MonoBehaviour
{
    [PunRPC]
    void Start()
    {
        Destroy(gameObject, 1);
    }


}
