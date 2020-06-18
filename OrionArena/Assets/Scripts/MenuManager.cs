using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
//using WebSocketSharp;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject usernameScreen, connectScreen;
    [SerializeField] private GameObject createUser;
    [SerializeField] private InputField userNameInput, createRoomInput, joinRoomInput;
    [SerializeField] private GameObject connectingText;
    [SerializeField] private GameObject volumePanel;
    [SerializeField] private GameObject menu;

    public SfxButton sfx;

    const string playerNamePrefKey = "PlayerName";

    private void Awake()
    {
        PhotonNetwork.Disconnect();
        //PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Play()
    {
        sfx.PlaySound(3);
        PhotonNetwork.ConnectUsingSettings();
        connectingText.SetActive(true);
        menu.SetActive(false);
    }

    public void OpenVolume()
    {
        sfx.PlaySound(3);
        
        volumePanel.SetActive(true);
    }

    public void ExitVolume()
    {
        sfx.PlaySound(3);
        
        volumePanel.SetActive(false);
    }

    public void Exit()
    {
        sfx.PlaySound(5);

        Application.Quit();
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
        if (createRoomInput.text != "")
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 10;
            PhotonNetwork.JoinOrCreateRoom(joinRoomInput.text, roomOptions, TypedLobby.Default);
            sfx.PlaySound(1);

        }
        else
        {
            sfx.PlaySound(4);

        }
    }

    public void OnCreateRoom()
    {
        if (createRoomInput.text != "")
        {
            PhotonNetwork.CreateRoom(createRoomInput.text, new RoomOptions { MaxPlayers = 10 }, null);
            sfx.PlaySound(1);

        }
        else
        {
            
            sfx.PlaySound(4);
        }
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
