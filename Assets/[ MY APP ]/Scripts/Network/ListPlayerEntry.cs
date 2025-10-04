using Photon.Pun.Demo.Asteroids;
using Photon.Pun.UtilityScripts;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListPlayerEntry : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI PlayerNameText;
    public Button PlayerReadyButton;
    public Image PlayerReadyImage;

    private int ownerId;
    private bool isPlayerReady;

    #region UNITY

    public void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber != ownerId)
        {
            PlayerReadyButton.gameObject.SetActive(false);
        }
        else
        {
            Hashtable initialProps = new Hashtable() { { AsteroidsGame.PLAYER_READY, isPlayerReady } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);

            PlayerReadyButton.onClick.AddListener(() =>
            {
                isPlayerReady = !isPlayerReady;
                SetPlayerReady(isPlayerReady);

                Hashtable props = new Hashtable() { { AsteroidsGame.PLAYER_READY, isPlayerReady } };
                PhotonNetwork.LocalPlayer.SetCustomProperties(props);

                if (PhotonNetwork.IsMasterClient)
                {
                    FindObjectOfType<NetworkManager>().LocalPlayerPropertiesUpdated();
                }
            });
        }
    }

    #endregion

    public void Initialize(int playerId, string playerName)
    {
        ownerId = playerId;
        PlayerNameText.text = playerName;
    }

    public void SetPlayerReady(bool playerReady)
    {
        PlayerReadyButton.GetComponentInChildren<TextMeshProUGUI>().text = playerReady ? "Ready!" : "Ready?";
        PlayerReadyImage.enabled = playerReady;
    }
}
