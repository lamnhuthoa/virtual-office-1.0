using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    // Reference to the image prefab
    public GameObject imagePrefab;
    public Transform targetPosition;

    public void SpawnImage(int imageIndex)
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("RPC_SpawnImage", RpcTarget.AllBuffered, imageIndex);
    }

    // Method to move an image to a new position
    public void MoveImage(GameObject image, Vector3 newPosition)
    {
        image.transform.position = newPosition;
    }

    [PunRPC]
    public void RPC_SpawnImage (int imageIndex)
    {
        ImagePickerManager imagePickerManager = GameObject.FindObjectOfType<ImagePickerManager>();
        GameObject imageSpawn = Instantiate(imagePrefab, imagePrefab.transform.position, Quaternion.identity);
        imageSpawn.GetComponentInChildren<Image>().sprite = imagePickerManager.imageList[imageIndex];

        if(!PhotonNetwork.IsMasterClient) { 
            imageSpawn.GetComponent<XRGrabNetworkInteractable>().enabled = false;
        }
    }
}
