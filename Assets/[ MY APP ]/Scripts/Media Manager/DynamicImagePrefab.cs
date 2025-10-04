using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicImagePrefab : MonoBehaviour
{
    public Image imageComponent;

    // This method sets the selected image to the prefab and adjusts its dimensions
    public void SetSelectedImage(Sprite sprite)
    {
        if (sprite != null)
        {
            // Set the sprite to the Image component
            imageComponent.sprite = sprite;

            // Adjust the dimensions of the prefab based on image resolution
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
        }
        else
        {
            Debug.LogError("Selected image sprite is null.");
        }
    }
}
