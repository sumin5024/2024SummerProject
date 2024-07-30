using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 총알의 중력 영향을 받지 않도록 설정
    }

    public void SetDirection(Vector2 direction)
    {
        rb.velocity = direction * speed; // 총알이 발사되는 방향 설정
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyManager enemy = hitInfo.GetComponent<EnemyManager>();
        if (enemy != null)
        {
            Debug.Log("Bullet hit " + hitInfo.name); // 적에게 닿았을 때 메시지 출력
            enemy.TakeDamage(damage);
            Destroy(gameObject); // 적에게 맞았을 때 총알 파괴
        }
        else
        {
            Debug.Log("Bullet hit non-enemy object: " + hitInfo.name); // 다른 객체에 닿았을 때 메시지 출력
        }
    }
}
