using Cinemachine;
using UnityEngine;
using Watermelon;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera startCamera;

    [SerializeField]
    private CinemachineVirtualCamera mainCamera;

    [SerializeField]
    private Joystick joystick;

    public void SwitchToStartCamera() 
    {
        startCamera.enabled = true;
        mainCamera.enabled = false;
        joystick.DisableControl();
    }

    public void SwitchToMainCamera()
    {
        startCamera.enabled = false;
        mainCamera.enabled = true;
        joystick.EnableControl();
    }
}
