using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    [SerializeField] float cameraMoveSpeed = 5f;
    [SerializeField] float zoomAmount = 2f;
    [SerializeField] float zoomSpeed = 2f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera = null;
    [SerializeField] float minOrthgraphicSize = 5f;
    [SerializeField] float maxOrthgraphicSize = 30f;
    float y;
    float x;

    private float orthgraphicSize;
    private float targetOrthgraphicSize;

    private void Start()
    {
        orthgraphicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthgraphicSize = orthgraphicSize;
    }

    private void LateUpdate()
    {
        CameraMove();
        CameraZoom();
    }

    private void CameraMove()
    {
        
       // if (Input.GetKey(KeyCode.W) != false || Input.GetKey(KeyCode.S) != false)
       // {
            y = Input.GetAxisRaw("Vertical");
       // }
       // if (Input.GetKeyDown(KeyCode.A) != false || Input.GetKeyDown(KeyCode.D) != false)
        //{
            x = Input.GetAxisRaw("Horizontal");
       // }

        Vector3 moveDirection = new Vector3(x, y).normalized;

        transform.position += moveDirection * cameraMoveSpeed * Time.deltaTime;
    }

    private void CameraZoom()
    {
        targetOrthgraphicSize += -Input.mouseScrollDelta.y * zoomAmount;
        targetOrthgraphicSize = Mathf.Clamp(targetOrthgraphicSize, minOrthgraphicSize, maxOrthgraphicSize);

        orthgraphicSize = Mathf.Lerp(orthgraphicSize, targetOrthgraphicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthgraphicSize;
    }
}
