using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectManager : MonoBehaviour
{
    public GameObject rotateObject;
    public float speed = 2.0f;
    public Vector3 rotateAmount = new Vector3();

    Animator animator;
    PhotonView photonView;
    bool isRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotate)
        {
            rotateObject.transform.Rotate(rotateAmount * Time.deltaTime * speed);
        }
    }

    public void rotateActive(bool _isRotate)
    {
        photonView.RPC("RPC_rotateActive", RpcTarget.AllBuffered, _isRotate);
    }

    [PunRPC]
    void RPC_rotateActive(bool _isRotate)
    {
        isRotate = _isRotate;

        if (animator)
            animator.SetBool("isClick", _isRotate);
    }
}
