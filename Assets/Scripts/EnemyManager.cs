using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 이동
    public float speed = 0.005f;
    private bool isMoving = true;
    private float stopDuration = 1.5f;

    // 탐지
    public Transform player;
    public float detectionRange = 7f;
    private bool isDetection = false;

    // 체력
    public int maxHealth = 100;
    public int currentHealth;

    // 공격
    public float attackRange = 1.7f;
    public float attackRate = 1.0f;
    private float lastAttackTime = 0.0f;
    public int attackDamage = 10;

    public GameObject Player;
    private SpriteRenderer spriteRenderer;

    // 이동 메소드
    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, speed);
    }

    // 적의 방향을 업데이트
    private void UpdateDirection()
    {
        // 플레이어의 위치를 확인하고 적의 방향을 조정
        if (Player.transform.position.x < gameObject.transform.position.x)
        {
            // 플레이어가 적의 왼쪽에 있으면 스프라이트를 왼쪽으로 향하게 함
            spriteRenderer.flipX = true;
        }
        else
        {
            // 플레이어가 적의 오른쪽에 있으면 스프라이트를 오른쪽으로 향하게 함
            spriteRenderer.flipX = false;
        }
    }

    // 플레이어와 충돌 시 잠시 멈추기
    private IEnumerator StopMoving(float duration)
    {
        isMoving = false;
        yield return new WaitForSeconds(duration);
        isMoving = true;
    }

    // 플레이어와의 거리 계산 후 공격
    private void CheckPlayerDistanceAttack()
    {
        // 플레이어와 적 사이의 거리를 계산
        float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);

        // 거리가 감지 범위 이내인지 확인
        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("플레이어 감지됨!");
            isDetection = true;
        }
        else
        {
            Debug.Log("플레이어 감지되지 않음.");
            isDetection = false;
        }
        if(CanAttack())
            Attack();
    }

    private bool CanAttack()
    {
        return (Time.time - lastAttackTime > 1f / attackRate) && isDetection;
    }

    // 플레이어를 공격
    private void Attack()
    {
        lastAttackTime = Time.time;
        Debug.Log("Attack!");

        // 공격범위 안에 있는 지 확인
        float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if(distanceToPlayer <= attackRange)
        {
            Debug.Log("플레이어 공격 당함 hp-");
        }
        else
        {
            Debug.Log("플레이어 공격 회피 ");
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player") // 플레이어와 충돌 시
        {
            StartCoroutine(StopMoving(stopDuration));
            Invoke("CheckPlayerDistanceAttack", stopDuration);
        }
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsPlayer();
        }
        UpdateDirection();
    }

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
