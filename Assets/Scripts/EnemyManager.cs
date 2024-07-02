using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 이동
    public float speed = 0.1f;
    private bool isMoving = true;
    private float stopDuration = 1.0f;

    // 탐지
    public Transform player;
    private float detectionRange = 1.7f;
    private bool isDetection = false;

    // 체력
    public int maxHealth = 100;
    public int currentHealth;

    // 공격
    public float attackRate = 1.0f;
    private float lastAttackTime = 0.0f;
    public int attackDamage = 10;

    public GameObject Player;

    // 이동 메소드
    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, speed);
    }

    // 플레이어와 충돌 시 잠시 멈추기
    private IEnumerator StopMoving(float duration)
    {
        isMoving = false;
        yield return new WaitForSeconds(duration);
        isMoving = true;
    }

    // 플레이어와의 거리 계산
    private void CheckPlayerDistance()
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
    }

    private bool CanAttack()
    {
        return (Time.time - lastAttackTime > 1f / attackRate) && isDetection;
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        // Attack logic here
        Debug.Log("Attack!");
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
            Invoke("CheckPlayerDistance", stopDuration);
            if (CanAttack())
            {
                Attack();
            }
        }
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsPlayer();
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }
}
