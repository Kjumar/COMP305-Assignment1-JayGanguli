using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    [SerializeField] private List<GameObject> virtualCameras;

    void Start()
    {
        
    }

    public void TransitionTo(GameObject targetCamera)
    {
        foreach (GameObject g in virtualCameras)
        {
            if (g == targetCamera)
            {
                g.GetComponent<CinemachineVirtualCamera>().Priority = 10;
            }
            else
            {
                g.GetComponent<CinemachineVirtualCamera>().Priority = 5;
            }
        }
    }
}
