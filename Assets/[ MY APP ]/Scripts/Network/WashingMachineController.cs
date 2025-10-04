using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WashingMachineController : MonoBehaviour
{
    public List<GameObject> objBtn = new List<GameObject>();

    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            photonView.RPC("RPC_ActiveParentUI", RpcTarget.AllBuffered, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            photonView.RPC("RPC_ActiveParentUI", RpcTarget.AllBuffered, false);
        }
    }

    public void ActiveUI(GameObject obj)
    {
        photonView.RPC("RPC_ActiveUI", RpcTarget.AllBuffered, obj.name);
    }

    [PunRPC]
    void RPC_ActiveUI(string objName)
    {
        objBtn.ForEach((obj) => obj.SetActive(obj.name == objName));
    }


    [PunRPC]
    void RPC_ActiveParentUI(bool isActive)
    {
        objBtn[0].transform.parent.gameObject.SetActive(isActive);
    }
}
