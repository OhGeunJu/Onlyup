using UnityEngine;
using Cinemachine;

public class CameraAltFreeLook : MonoBehaviour
{
    public CinemachineFreeLook freeLook;
    public Transform player;

    private bool wasAltHeld = false;
    private float originalCameraY;

    void Update()
    {
        bool isAltHeld = Input.GetKey(KeyCode.LeftAlt);

        // ALT 누른 순간: 현재 카메라 Y축 값 저장
        if (isAltHeld && !wasAltHeld)
        {
            originalCameraY = freeLook.m_XAxis.Value;
        }

        // ALT 뗀 순간: Y축 회전값 복귀
        if (!isAltHeld && wasAltHeld)
        {
            freeLook.m_XAxis.Value = originalCameraY;
        }

        wasAltHeld = isAltHeld;
    }
}
