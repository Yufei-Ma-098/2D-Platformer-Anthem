using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public PolygonCollider2D CameraBounds;

    public Transform player;
    public Transform elevator;
    public float cameraSizeOnElevator = 15f;
    public float cameraSizeNormal = 7f;
    public float zoomSpeed = 5f;

    private bool isFollowingElevator = false;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineConfiner confiner;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();

        confiner.m_BoundingShape2D = CameraBounds;

        virtualCamera.Follow = player;
        virtualCamera.LookAt = player;
    }

    private void Update()
    {
        float targetSize;

        if (!isFollowingElevator)
        {
            targetSize = cameraSizeNormal;
        }
        else
        {
            targetSize = cameraSizeOnElevator;
        }

        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetSize, zoomSpeed * Time.deltaTime);
    }

    public void StartFollowingElevator()
    {
        isFollowingElevator = true;
        virtualCamera.Follow = elevator;
        virtualCamera.LookAt = elevator;
    }

    public void StopFollowingElevator()
    {
        isFollowingElevator = false;
        virtualCamera.Follow = player;
        virtualCamera.LookAt = player;
    }
}
