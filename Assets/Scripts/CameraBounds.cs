using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class CameraBounds : MonoBehaviour
{
    private Camera mainCamera;
    private EdgeCollider2D edgeCollider;

    void Start()
    {
        mainCamera = Camera.main;
        edgeCollider = GetComponent<EdgeCollider2D>();
        UpdateCollider();
    }

    void Update()
    {
        UpdateCollider();
    }

    void UpdateCollider()
    {
        Vector2[] edges = new Vector2[5];

        // 카메라의 네 모서리 좌표를 구해서 EdgeCollider에 할당
        edges[0] = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)); // Bottom-left
        edges[1] = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.nearClipPlane)); // Top-left
        edges[2] = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane)); // Top-right
        edges[3] = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)); // Bottom-right
        edges[4] = edges[0]; // 다시 Bottom-left로 돌아와 폐쇄된 루프 생성

        edgeCollider.points = edges;
    }
}
