using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagePickerManager : MonoBehaviour
{
    public ImageManager imageManager;
    public GameObject UIControllerParent; // Reference to the ImagePickerUI GameObject
    public GameObject ImagePickerUI;
    public Transform imageListParent; // Parent object where the image buttons will be instantiated
    public GameObject imageButtonPrefab; // Prefab of the button to display images

    // List of images to display in the ImagePickerUI
    public Sprite[] imageList;


    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            UIControllerParent.SetActive(false);
        } else
        {
            PopulateImagePickerUI();
        }
    }

    public void ToggleImagePicker ()
    {
        ImagePickerUI.SetActive(!ImagePickerUI.activeInHierarchy);
    }

    void PopulateImagePickerUI()
    {
        // Destroy existing images
        foreach (Transform child in imageListParent)
        {
            Destroy(child.gameObject);
        }

        // Create new image buttons
        foreach (Sprite image in imageList)
        {
            GameObject imageButton = Instantiate(imageButtonPrefab, imageListParent);
            if (imageButton != null)
            {
                Image imageComponent = imageButton.GetComponentInChildren<Image>();

                if (imageComponent != null)
                {
                    Debug.Log(image);
                    imageComponent.sprite = image;

                    // Add click listener to the button to select the image
                    imageButton.GetComponent<Button>().onClick.AddListener(() => SetPickedImage(image));
                }
                else
                {
                    Debug.LogError("Image component not found on the imageButton GameObject.");
                }
            }
            else
            {
                Debug.LogError("imageButton is not initialized.");
            }
        }
    }

    // Method to set the picked image
    public void SetPickedImage(Sprite imageSprite)
    {
        List<Sprite> sprites = new List<Sprite>(imageList);
        int imageIndex = sprites.IndexOf(imageSprite);
        imageManager.SpawnImage(imageIndex);

        ImagePickerUI.SetActive(false);
    }
}