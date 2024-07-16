using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // 총알 속도
    private Vector2 direction;

    // 방향 설정 함수
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        // 총알이 생성된 직후에 움직이도록 합니다.
        Destroy(gameObject, 5f); // 5초 후에 총알이 자동으로 삭제됩니다.
    }

    void Update()
    {
        // 총알 이동
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 총알과 다른 오브젝트가 충돌했을 때 총알 삭제
        Destroy(gameObject);
    }
}
