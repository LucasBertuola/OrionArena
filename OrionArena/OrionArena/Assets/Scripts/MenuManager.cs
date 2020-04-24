using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject usernameScreen, connectScreen;
    [SerializeField] private GameObject createUser;
    [SerializeField] private InputField userNameInput, createRoomInput, joinRoomInput;
    [SerializeField] private GameObject connectingText;

    const string playerNamePrefKey = "PlayerName";

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master!");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby!");
        usernameScreen.SetActive(true);
        connectingText.SetActive(false);

        string defaultName = string.Empty;
        if (PlayerPrefs.HasKey(playerNamePrefKey))
        {
            defaultName = PlayerPrefs.GetString(playerNamePrefKey);
            userNameInput.text = defaultName;
        }
    }

    public override void OnJoinedRoom()
    {
        //Play game scene
        PhotonNetwork.LoadLevel(1);
    }

    #region UIMethods

    public void OnCreateName()
    {
        PhotonNetwork.NickName = userNameInput.text;
        PlayerPrefs.SetString(playerNamePrefKey, userNameInput.text);
        usernameScreen.SetActive(false);
        connectScreen.SetActive(true);
    }

    public void OnJoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom(joinRoomInput.text, roomOptions, TypedLobby.Default);
    }

    public void OnCreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomInput.text, new RoomOptions { MaxPlayers = 10 }, null);
    }

    public void OnNameFieldChanged()
    {
        if (userNameInput.text.Length >= 2)
        {
            createUser.SetActive(true);
        }
        else if (userNameInput.text.Length < 2)
        {
            createUser.SetActive(false);
        }
    }
    #endregion
}
