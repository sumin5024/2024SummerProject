using UnityEngine;

public class PlayerBoundaryCorrection : MonoBehaviour
{
    public Transform player1;       // 1P 플레이어의 Transform
    public Transform player2;       // 2P 플레이어의 Transform
    public Camera mainCamera;       // 메인 카메라 (Cinemachine으로 제어되는 경우 실제 렌더링 카메라)
    public float boundaryMargin = 0.1f; // 경계 여유 공간

    void Update()
    {
        // 1P 플레이어의 화면 범위 내에서 제한하기 위해 뷰포트 좌표 계산
        Vector3 player1ViewportPos = mainCamera.WorldToViewportPoint(player1.position);
        Vector3 player2ViewportPos = mainCamera.WorldToViewportPoint(player2.position);

        // 2P 플레이어가 뷰포트를 벗어나지 않도록 제한
        if (player2ViewportPos.x < boundaryMargin)
            player2ViewportPos.x = boundaryMargin;
        if (player2ViewportPos.x > 1.0f - boundaryMargin)
            player2ViewportPos.x = 1.0f - boundaryMargin;
        if (player2ViewportPos.y < boundaryMargin)
            player2ViewportPos.y = boundaryMargin;
        if (player2ViewportPos.y > 1.0f - boundaryMargin)
            player2ViewportPos.y = 1.0f - boundaryMargin;

        // 수정된 뷰포트 좌표를 다시 월드 좌표로 변환하여 2P의 위치를 조정
        player2.position = mainCamera.ViewportToWorldPoint(player2ViewportPos);
    }
}
