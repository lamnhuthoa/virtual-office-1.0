using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.PUN;
using UnityEngine.UI;

public class NetworkedPlayerVoiceChatManager : MonoBehaviour
{
    [SerializeField]
    private Image speakerImage;

    [SerializeField]
    private PhotonVoiceView photonVoiceView;

    private PunVoiceClient punVoiceNetwork;

    private void Awake()
    {
        this.punVoiceNetwork = PunVoiceClient.Instance;
        this.speakerImage.enabled = false;
    }

    private void Update()
    {
        if (this.punVoiceNetwork == null)
        {
            this.punVoiceNetwork = PunVoiceClient.Instance;
        }
        this.speakerImage.enabled = this.photonVoiceView.IsSpeaking;
    }
}
