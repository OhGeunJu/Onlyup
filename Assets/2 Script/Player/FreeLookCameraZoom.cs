using UnityEngine;
using Cinemachine;

[System.Serializable]
public struct RigSettings
{
    public float height;
    public float radius;
}

public class FreeLookCameraZoom : MonoBehaviour
{
    public CinemachineFreeLook freeLook;
    public Transform player;

    public RigSettings[] defaultRigs = new RigSettings[3];
    public float zoomOutRadius = 7.5f;   // 걷기 시 줌아웃
    public float runZoomOutRadius = 9f;  // 달리기 시 더 멀어짐
    public float zoomSpeed = 2f;

    void Start()
    {
        if (freeLook == null) return;

        // 초기값 저장
        for (int i = 0; i < 3; i++)
        {
            defaultRigs[i].height = freeLook.m_Orbits[i].m_Height;
            defaultRigs[i].radius = freeLook.m_Orbits[i].m_Radius;
        }
    }

    void Update()
    {
        if (player == null || freeLook == null) return;

        // 방향키 입력 확인 (WASD)
        bool h = Input.GetAxisRaw("Horizontal") != 0f;
        bool v = Input.GetAxisRaw("Vertical") != 0f;
        bool isMoving = h || v;

        // Shift 키를 누르고 있는지 확인
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        // 목표 반지름 결정
        float targetRadius = isRunning ? runZoomOutRadius :
                              isMoving ? zoomOutRadius :
                              0f; // → 아래에서 defaultRadius로 대체됨

        for (int i = 0; i < 3; i++)
        {
            float defaultHeight = defaultRigs[i].height;
            float defaultRadius = defaultRigs[i].radius;

            float radiusGoal = isMoving ? targetRadius : defaultRadius;

            freeLook.m_Orbits[i].m_Radius = Mathf.Lerp(freeLook.m_Orbits[i].m_Radius, radiusGoal, Time.deltaTime * zoomSpeed);
            freeLook.m_Orbits[i].m_Height = Mathf.Lerp(freeLook.m_Orbits[i].m_Height, defaultHeight, Time.deltaTime * zoomSpeed);
        }

    }

    
}
