using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // �Ѿ� �ӵ�
    private Vector2 direction;

    // ���� ���� �Լ�
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        // �Ѿ��� ������ ���Ŀ� �����̵��� �մϴ�.
        Destroy(gameObject, 5f); // 5�� �Ŀ� �Ѿ��� �ڵ����� �����˴ϴ�.
    }

    void Update()
    {
        // �Ѿ� �̵�
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �Ѿ˰� �ٸ� ������Ʈ�� �浹���� �� �Ѿ� ����
        Destroy(gameObject);
    }
}
