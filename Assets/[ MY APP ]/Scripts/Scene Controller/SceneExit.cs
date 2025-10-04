using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class SceneExit : MonoBehaviourPunCallbacks
{
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

}
