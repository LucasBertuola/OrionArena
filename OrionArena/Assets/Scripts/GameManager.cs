using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject spawnButton;
    public GameObject sceneCam;

    public Text spawnTimer;
    public Text pingRate;
    public GameObject respawnUI;

    private float timeAmount = 5f;
    private bool startRespawn;

    public GameObject localPlayer;
    public static GameManager instance = null;

    private void Awake()
    {
        instance = this;
        spawnButton.SetActive(true);
    }

    private void Update()
    {
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

    public void SpawnPlayer()
    {
        float randomValueX = Random.Range(-70, 70);
        float randomValueY = Random.Range(-5, 25);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(playerPrefab.transform.position.x + randomValueX, playerPrefab.transform.position.y + randomValueY)
            , Quaternion.identity, 0);
        spawnButton.SetActive(false);
        sceneCam.SetActive(false);
    }
}
