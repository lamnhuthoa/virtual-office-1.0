using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabNetworkInteractable : XRGrabInteractable
{
    PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        pv.RequestOwnership();
        base.OnSelectEntering(args);
    }
}
