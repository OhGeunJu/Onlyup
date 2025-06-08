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
    public float zoomOutRadius = 7.5f;   // �ȱ� �� �ܾƿ�
    public float runZoomOutRadius = 9f;  // �޸��� �� �� �־���
    public float zoomSpeed = 2f;

    void Start()
    {
        if (freeLook == null) return;

        // �ʱⰪ ����
        for (int i = 0; i < 3; i++)
        {
            defaultRigs[i].height = freeLook.m_Orbits[i].m_Height;
            defaultRigs[i].radius = freeLook.m_Orbits[i].m_Radius;
        }
    }

    void Update()
    {
        if (player == null || freeLook == null) return;

        // ����Ű �Է� Ȯ�� (WASD)
        bool h = Input.GetAxisRaw("Horizontal") != 0f;
        bool v = Input.GetAxisRaw("Vertical") != 0f;
        bool isMoving = h || v;

        // Shift Ű�� ������ �ִ��� Ȯ��
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        // ��ǥ ������ ����
        float targetRadius = isRunning ? runZoomOutRadius :
                              isMoving ? zoomOutRadius :
                              0f; // �� �Ʒ����� defaultRadius�� ��ü��

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
