using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AvatarData
{
    public Vector3 position;
    public Quaternion rotation;
    // Add any other avatar data you want to save
}

[System.Serializable]
public class SceneData
{
    public List<AvatarData> avatars = new List<AvatarData>();
    // Add any other scene data you want to save
}

public class SceneController : MonoBehaviour
{
    public GameObject avatarPrefab;
    public Transform avatarsParent;

    private SceneData sceneData;

    public void SaveScene()
    {
        sceneData = new SceneData();

        // Save avatar positions and rotations
        foreach (Transform avatarTransform in avatarsParent)
        {
            AvatarData avatarData = new AvatarData();
            avatarData.position = avatarTransform.position;
            avatarData.rotation = avatarTransform.rotation;
            // Add any other avatar data you want to save

            sceneData.avatars.Add(avatarData);
        }

        // Save sceneData to disk or wherever you store your scene data
        // (You need to implement saving the sceneData)
    }

    public void LoadScene()
    {
        // Load sceneData from disk or wherever you stored it

        // Instantiate avatars
        foreach (AvatarData avatarData in sceneData.avatars)
        {
            GameObject avatarObj = Instantiate(avatarPrefab, avatarData.position, avatarData.rotation, avatarsParent);
            // You might need to add logic here to set up the FinalIK components based on the saved data
        }
    }
}
