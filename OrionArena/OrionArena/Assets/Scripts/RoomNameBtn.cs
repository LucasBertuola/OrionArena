using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class RoomNameBtn : MonoBehaviour
{
    public Text roomName;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() => JoinRoomByName(roomName.text));
    }

    private void JoinRoomByName(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
