using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Zoom : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float zoomSpeed = 2f;
    bool isZoomed = false;
    float minFov = 20f;
    float maxFov = 60f;
    [SerializeField] CinemachineVirtualCamera virtualCam;

    private void OnDisable()
    {
        virtualCam.m_Lens.FieldOfView = maxFov;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isZoomed = !isZoomed;
            if (isZoomed)
            {
                Debug.Log("Zoomed In");
                virtualCam.m_Lens.FieldOfView = minFov;
            }
            else
            {
                Debug.Log("Zoomed Out");
                virtualCam.m_Lens.FieldOfView = maxFov;
            }
        }
    }
}
