using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Conn : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject loginPanel, roomPanel;
    [SerializeField] private InputField playerName, roomName;
    [SerializeField] private Text txtNick;
    [SerializeField] private GameObject[] player;
    [SerializeField] private int id;

    void Start()
    {

    }

    public void Login()
    {
        PhotonNetwork.NickName = playerName.text;
        PhotonNetwork.ConnectUsingSettings();
        loginPanel.SetActive(false);
        roomPanel.SetActive(true);
    }

    public void CreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(roomName.text,new RoomOptions(), TypedLobby.Default);
    }

    public void SetID(int Id)
    {
        id = Id;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join failed");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        print(PhotonNetwork.CurrentRoom.Name);
        print(PhotonNetwork.CurrentRoom.PlayerCount);
        print(PhotonNetwork.NickName);
        txtNick.text = "Username: " + PhotonNetwork.NickName;

        roomPanel.SetActive(false);
        PhotonNetwork.Instantiate(player[id].name, new Vector2(Random.Range(-14, 14), 0), Quaternion.identity, 0);
    }
}
