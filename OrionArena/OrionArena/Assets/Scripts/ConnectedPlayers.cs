using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectedPlayers : MonoBehaviour
{
    public GameObject currentPlayersGRID;
    public GameObject currentPlayersPrefab;

    public void AddLocalPlayer()
    {
        GameObject obj = Instantiate(currentPlayersPrefab, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(currentPlayersGRID.transform, false);
        obj.GetComponentInChildren<Text>().text = PhotonNetwork.NickName;
        obj.GetComponentInChildren<Text>().color = Color.green;
    }

    [PunRPC]
    public void UpdatePlayerList(string name)
    {
        GameObject obj = Instantiate(currentPlayersPrefab, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(currentPlayersGRID.transform, false);
        obj.GetComponentInChildren<Text>().text = name;
    }

    public void RemovePlayerList(string name)
    {
        foreach (Text playerName in currentPlayersGRID.GetComponentsInChildren<Text>())
        {
            if (name == playerName.text)
            {
                Destroy(playerName.transform.parent.gameObject);
            }
        }
    }
}
