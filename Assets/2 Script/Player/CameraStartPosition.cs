using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraStartPosition : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public Transform player;

    void Start()
    {
        // 캐릭터가 바라보는 방향(월드 방향)을 기준으로 X축 값을 설정
        float angleY = player.eulerAngles.y;
        freeLookCamera.m_XAxis.Value = angleY;

        // 카메라 위에서 내려다보는 각도 설정 (0~1 범위)
        freeLookCamera.m_YAxis.Value = 0.6f;
    }
}
