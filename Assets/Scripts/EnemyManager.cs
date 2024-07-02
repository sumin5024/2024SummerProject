using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // �̵�
    public float speed = 0.1f;
    private bool isMoving = true;
    private float stopDuration = 1.0f;

    // Ž��
    public Transform player;
    private float detectionRange = 1.7f;
    private bool isDetection = false;

    // ü��
    public int maxHealth = 100;
    public int currentHealth;

    // ����
    public float attackRate = 1.0f;
    private float lastAttackTime = 0.0f;
    public int attackDamage = 10;

    public GameObject Player;

    // �̵� �޼ҵ�
    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, speed);
    }

    // �÷��̾�� �浹 �� ��� ���߱�
    private IEnumerator StopMoving(float duration)
    {
        isMoving = false;
        yield return new WaitForSeconds(duration);
        isMoving = true;
    }

    // �÷��̾���� �Ÿ� ���
    private void CheckPlayerDistance()
    {
        // �÷��̾�� �� ������ �Ÿ��� ���
        float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);

        // �Ÿ��� ���� ���� �̳����� Ȯ��
        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("�÷��̾� ������!");
            isDetection = true;
        }
        else
        {
            Debug.Log("�÷��̾� �������� ����.");
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
        if (collision.collider.tag == "Player") // �÷��̾�� �浹 ��
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
