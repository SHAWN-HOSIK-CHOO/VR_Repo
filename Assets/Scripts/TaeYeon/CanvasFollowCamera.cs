using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    public Camera mainCamera; // Inspector에서 메인 카메라를 연결
    public float distance = 1.0f; // 캔버스와 카메라 사이의 거리

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not assigned.");
            return;
        }

        // 캔버스의 위치를 카메라의 위치 + Forward 벡터 * 거리로 설정
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;

        // 캔버스의 회전을 카메라의 회전으로 설정 (필요한 경우)
        transform.rotation = mainCamera.transform.rotation;
    }
}