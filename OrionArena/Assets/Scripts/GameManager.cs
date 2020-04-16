using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    //public GameObject playerPrefab;
    public GameObject[] playerPrefab;
    public GameObject spawnPanel;
    public GameObject sceneCam;

    public Text spawnTimer;
    public Text pingRate;
    public GameObject respawnUI;
    public GameObject menu;

    private float timeAmount = 5f;
    private bool startRespawn;

    public GameObject localPlayer;
    public GameObject feedBox;
    public GameObject feedTextPrefab;

    public static GameManager instance = null;

    private void Awake()
    {
        instance = this;
        spawnPanel.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        if (startRespawn)
        {
            StartRespawn();
        }

        pingRate.text = "Ping: " + PhotonNetwork.GetPing();
    }

    public void StartRespawn()
    {
        timeAmount -= Time.deltaTime;
        spawnTimer.text = "Respawn in " + timeAmount.ToString("F0");

        if (timeAmount <= 0)
        {
            respawnUI.SetActive(false);
            startRespawn = false;
            PlayerRelocation();
            localPlayer.GetComponent<Health>().EnableInputs();
            localPlayer.GetComponent<PhotonView>().RPC("Revive", RpcTarget.AllBuffered);
        }
    }

    public void ToggleMenu()
    {
        if(menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject go = Instantiate(feedTextPrefab, new Vector2(0f, 0f), Quaternion.identity);
        go.transform.SetParent(feedBox.transform);
        go.GetComponent<Text>().text = newPlayer.NickName + " has joined the game.";
        Destroy(go, 3);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GameObject go = Instantiate(feedTextPrefab, new Vector2(0f, 0f), Quaternion.identity);
        go.transform.SetParent(feedBox.transform);
        go.GetComponent<Text>().text = otherPlayer.NickName + " has left the game.";
        Destroy(go, 3);
    }

    public void PlayerRelocation()
    {
        float randomValueX = Random.Range(-70, 70);
        float randomValueY = Random.Range(-5, 25);
        localPlayer.transform.localPosition = new Vector2(randomValueX, randomValueY);
    }

    public void EnableRespawn()
    {
        timeAmount = 5f;
        startRespawn = true;
        respawnUI.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    public void SpawnShotgunPlayer()
    {
        float randomValueX = Random.Range(-70, 70);
        float randomValueY = Random.Range(-5, 25);
        PhotonNetwork.Instantiate(playerPrefab[0].name, new Vector2(playerPrefab[0].transform.position.x + randomValueX, playerPrefab[0].transform.position.y + randomValueY)
            , Quaternion.identity, 0);
        spawnPanel.SetActive(false);
        sceneCam.SetActive(false);
    }

    public void SpawnRiflePlayer()
    {
        float randomValueX = Random.Range(-70, 70);
        float randomValueY = Random.Range(-5, 25);
        PhotonNetwork.Instantiate(playerPrefab[1].name, new Vector2(playerPrefab[1].transform.position.x + randomValueX, playerPrefab[1].transform.position.y + randomValueY)
            , Quaternion.identity, 0);
        spawnPanel.SetActive(false);
        sceneCam.SetActive(false);
    }

    public void SpawnPistolPlayer()
    {
        float randomValueX = Random.Range(-70, 70);
        float randomValueY = Random.Range(-5, 25);
        PhotonNetwork.Instantiate(playerPrefab[2].name, new Vector2(playerPrefab[2].transform.position.x + randomValueX, playerPrefab[2].transform.position.y + randomValueY)
            , Quaternion.identity, 0);
        spawnPanel.SetActive(false);
        sceneCam.SetActive(false);
    }
}
