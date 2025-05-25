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

        // ALT ���� ����: ���� ī�޶� Y�� �� ����
        if (isAltHeld && !wasAltHeld)
        {
            originalCameraY = freeLook.m_XAxis.Value;
        }

        // ALT �� ����: Y�� ȸ���� ����
        if (!isAltHeld && wasAltHeld)
        {
            freeLook.m_XAxis.Value = originalCameraY;
        }

        wasAltHeld = isAltHeld;
    }
}
