using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggers : MonoBehaviour
{
    [SerializeField] private GameObject cameraIn;
    [SerializeField] private GameObject cameraOut;

    public VirtualCameraController vCamController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            vCamController.TransitionTo(cameraIn);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            vCamController.TransitionTo(cameraOut);
        }
    }
}
