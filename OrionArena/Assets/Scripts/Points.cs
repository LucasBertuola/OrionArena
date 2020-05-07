using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public int points = 0;
    public Text pointsText;
    private PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    [PunRPC]
    void GainPoint()
    {
        points += 1;
        pointsText.text = "POINTS: " + points.ToString();

        if (points >= 5)
        {
            pv.RPC("ShowWinScreen", RpcTarget.AllBuffered, this.gameObject);
        }
    }
}
