using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public float lifetime = 0.7f; // 총알이 사라지기까지의 시간 (초)

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 총알이 중력 영향을 받지 않도록 설정
    }

    void Start()
    {
        Debug.Log("Destroy will be called after " + lifetime + " seconds.");
        Destroy(gameObject, lifetime); // 지정된 시간이 지나면 총알 삭제
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
            Debug.Log("총알이 " + hitInfo.name + "에 맞음"); // 적에게 닿았을 때 메시지 출력
            enemy.TakeDamage(damage);
            Destroy(gameObject); // 적에게 맞았을 때 총알 파괴
        }
        else
        {
            if(hitInfo.tag == "object")
            {
                Destroy(gameObject);
            }
            Debug.Log("총알이 적이 아닌 객체에 맞음: " + hitInfo.name); // 다른 객체에 닿았을 때 메시지 출력
        }
    }
}
