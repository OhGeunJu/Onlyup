using UnityEngine;
using Cinemachine;

public class CameraAltFreeLook : MonoBehaviour
{
    public CinemachineFreeLook freeLook;
    public Transform player;
    public float restoreSpeed = 5f; // ���� �ӵ�

    private bool wasAltHeld = false;
    private float originalCameraX;
    private float originalCameraY;
    private bool isRestoring = false;

    void Update()
    {
        bool isAltHeld = Input.GetKey(KeyCode.LeftAlt);

        if (freeLook == null) return;

        // ALT ���� ����: ���� ���� ����
        if (isAltHeld && !wasAltHeld)
        {
            originalCameraX = freeLook.m_XAxis.Value;
            originalCameraY = freeLook.m_YAxis.Value;
            isRestoring = false;
        }

        // ALT �� ����: ���� ����
        if (!isAltHeld && wasAltHeld)
        {
            isRestoring = true;
        }

        // ���� ó��
        if (isRestoring)
        {
            // Lerp�� �ε巴�� ���� ����
            freeLook.m_XAxis.Value = Mathf.LerpAngle(freeLook.m_XAxis.Value, originalCameraX, Time.deltaTime * restoreSpeed);
            freeLook.m_YAxis.Value = Mathf.Lerp(freeLook.m_YAxis.Value, originalCameraY, Time.deltaTime * restoreSpeed);

            // ���� ���������� ����
            if (Mathf.Abs(freeLook.m_XAxis.Value - originalCameraX) < 0.001f &&
                Mathf.Abs(freeLook.m_YAxis.Value - originalCameraY) < 0.001f)
            {
                isRestoring = false;
            }
        }

        wasAltHeld = isAltHeld;
    }
}
