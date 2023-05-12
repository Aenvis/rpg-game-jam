using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;

    private void Start()
    {
        if (freeLookCamera == null)
        {
            Debug.LogError("Free Look Camera is not attached.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = 2500; // Replace with your value
        }
        else
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = 0; // No rotation
        }
    }
}
