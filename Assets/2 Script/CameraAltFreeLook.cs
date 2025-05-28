using UnityEngine;
using Cinemachine;

public class CameraAltFreeLook : MonoBehaviour
{
    public CinemachineFreeLook freeLook;
    public Transform player;
    public float restoreSpeed = 5f; // 복귀 속도

    private bool wasAltHeld = false;
    private float originalCameraX;
    private float originalCameraY;
    private bool isRestoring = false;

    void Update()
    {
        bool isAltHeld = Input.GetKey(KeyCode.LeftAlt);

        if (freeLook == null) return;

        // ALT 누른 순간: 현재 시점 저장
        if (isAltHeld && !wasAltHeld)
        {
            originalCameraX = freeLook.m_XAxis.Value;
            originalCameraY = freeLook.m_YAxis.Value;
            isRestoring = false;
        }

        // ALT 뗀 순간: 복귀 시작
        if (!isAltHeld && wasAltHeld)
        {
            isRestoring = true;
        }

        // 복귀 처리
        if (isRestoring)
        {
            // Lerp로 부드럽게 시점 복구
            freeLook.m_XAxis.Value = Mathf.LerpAngle(freeLook.m_XAxis.Value, originalCameraX, Time.deltaTime * restoreSpeed);
            freeLook.m_YAxis.Value = Mathf.Lerp(freeLook.m_YAxis.Value, originalCameraY, Time.deltaTime * restoreSpeed);

            // 거의 복귀했으면 종료
            if (Mathf.Abs(freeLook.m_XAxis.Value - originalCameraX) < 0.001f &&
                Mathf.Abs(freeLook.m_YAxis.Value - originalCameraY) < 0.001f)
            {
                isRestoring = false;
            }
        }

        wasAltHeld = isAltHeld;
    }
}
