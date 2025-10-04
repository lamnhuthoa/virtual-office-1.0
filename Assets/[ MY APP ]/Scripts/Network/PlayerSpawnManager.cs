using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;

public class PlayerSpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] playerPrefab;
    [SerializeField] private GameObject[] roomMasterPrefab;
    [SerializeField] private List<Transform> spawnPosition = new List<Transform>();
    private GameObject localPlayer;

    void Start()
    {
        int playerNumber = Random.Range(0, playerPrefab.Length);

        object value;
        PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("pNr", out value);
        int playerNumberPos = int.Parse(value.ToString());
        
        if (PhotonNetwork.IsMasterClient)
        {
            localPlayer = PhotonNetwork.Instantiate(roomMasterPrefab[0].name, spawnPosition[0].position, spawnPosition[0].rotation);
        } else
        {
            localPlayer = PhotonNetwork.Instantiate(playerPrefab[playerNumber].name, spawnPosition[playerNumberPos].position, spawnPosition[playerNumberPos].rotation);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("RoomScene");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void EnableCanvas ()
    {
        if (localPlayer != null)
        {
            localPlayer.GetComponent<EnableWebviewCanvas>().canvas.SetActive(true);
        }
    }
}
