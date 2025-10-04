using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRSocketNetworkInteractor : MonoBehaviour
{
    public GameObject objSocket;
    [SerializeField] bool isGrab;

    PhotonView photonView;
    XRGrabInteractable xRGrabInteractable;
    List<MeshRenderer> meshRenderers;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        xRGrabInteractable = GetComponent<XRGrabInteractable>();

        xRGrabInteractable.selectEntered.AddListener(obj => GrabActive(true));
        xRGrabInteractable.selectExited.AddListener(obj => GrabActive(false));

        meshRenderers = new List<MeshRenderer>(objSocket.GetComponentsInChildren<MeshRenderer>());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == objSocket && !isGrab)
        {
            meshRenderers.ForEach(renderer => renderer.enabled = false );
            transform.position = objSocket.transform.position;
            transform.rotation = objSocket.transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objSocket)
        {
            meshRenderers.ForEach(renderer => renderer.enabled = true);
        }
    }

    public void GrabActive(bool _isGrab)
    {
        photonView.RPC("RPC_GrabActive", RpcTarget.AllBuffered, _isGrab);
    }

    [PunRPC]
    void RPC_GrabActive(bool _isGrab)
    {
        isGrab = _isGrab;
    }
}
