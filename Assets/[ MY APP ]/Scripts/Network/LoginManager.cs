using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerNameInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("RoomScene");
    }

    public void OnLoginButtonClicked()
    {
        string playerName = PlayerNameInput.text;
        playerName = (string.IsNullOrWhiteSpace(playerName)) ? "Associate #" + Random.Range(1000, 10000) : playerName;

        PhotonNetwork.LocalPlayer.NickName = playerName;
        PhotonNetwork.ConnectUsingSettings(PhotonNetwork.PhotonServerSettings.AppSettings, Application.internetReachability == NetworkReachability.NotReachable);
    }
}
