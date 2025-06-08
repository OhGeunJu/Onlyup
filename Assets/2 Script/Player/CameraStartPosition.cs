using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraStartPosition : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public Transform player;

    void Start()
    {
        // ĳ���Ͱ� �ٶ󺸴� ����(���� ����)�� �������� X�� ���� ����
        float angleY = player.eulerAngles.y;
        freeLookCamera.m_XAxis.Value = angleY;

        // ī�޶� ������ �����ٺ��� ���� ���� (0~1 ����)
        freeLookCamera.m_YAxis.Value = 0.6f;
    }
}
