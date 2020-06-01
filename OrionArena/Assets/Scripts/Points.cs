using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class Points : MonoBehaviour
{ 
    public Text playerPoints;

    PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        playerPoints.text = "KILLS: " + PhotonNetwork.LocalPlayer.GetScore().ToString();
    }

    public void AddPoints()
    {
        PhotonNetwork.LocalPlayer.AddScore(1);
        //UpdateText();

        if (PhotonNetwork.LocalPlayer.GetScore() >= 10)
        {
            pv.RPC("WinScreen", RpcTarget.AllBuffered);
        }
    }

    public void UpdateText()
    {
        playerPoints.text = "KILLS: " + PhotonNetwork.LocalPlayer.GetScore().ToString();
    }

    [PunRPC]
    void WinScreen()
    {
        GameManager.instance.ShowWinScreen(this.gameObject.GetComponent<PhotonView>().Owner);
    }
}
