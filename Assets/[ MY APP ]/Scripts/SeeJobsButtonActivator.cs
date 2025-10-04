using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeJobsButtonActivator : MonoBehaviour
{
    public GameObject seeJobsButtonCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seeJobsButtonCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            seeJobsButtonCanvas.SetActive(false);
        }
    }
}
