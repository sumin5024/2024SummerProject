using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // �̵�
    public float speed = 0.005f;
    private bool isMoving = true;
    private float stopDuration = 1.5f;

    // Ž��
    public Transform player;
    public float detectionRange = 7f;
    private bool isDetection = false;

    // ü��
    public int maxHealth = 100;
    public int currentHealth;

    // ����
    public float attackRange = 1.7f;
    public float attackRate = 1.0f;
    private float lastAttackTime = 0.0f;
    public int attackDamage = 10;

    public GameObject Player;
    private SpriteRenderer spriteRenderer;

    // �̵� �޼ҵ�
    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, speed);
    }

    // ���� ������ ������Ʈ
    private void UpdateDirection()
    {
        // �÷��̾��� ��ġ�� Ȯ���ϰ� ���� ������ ����
        if (Player.transform.position.x < gameObject.transform.position.x)
        {
            // �÷��̾ ���� ���ʿ� ������ ��������Ʈ�� �������� ���ϰ� ��
            spriteRenderer.flipX = true;
        }
        else
        {
            // �÷��̾ ���� �����ʿ� ������ ��������Ʈ�� ���������� ���ϰ� ��
            spriteRenderer.flipX = false;
        }
    }

    // �÷��̾�� �浹 �� ��� ���߱�
    private IEnumerator StopMoving(float duration)
    {
        isMoving = false;
        yield return new WaitForSeconds(duration);
        isMoving = true;
    }

    // �÷��̾���� �Ÿ� ��� �� ����
    private void CheckPlayerDistanceAttack()
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
        if(CanAttack())
            Attack();
    }

    private bool CanAttack()
    {
        return (Time.time - lastAttackTime > 1f / attackRate) && isDetection;
    }

    // �÷��̾ ����
    private void Attack()
    {
        lastAttackTime = Time.time;
        Debug.Log("Attack!");

        // ���ݹ��� �ȿ� �ִ� �� Ȯ��
        float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if(distanceToPlayer <= attackRange)
        {
            Debug.Log("�÷��̾� ���� ���� hp-");
        }
        else
        {
            Debug.Log("�÷��̾� ���� ȸ�� ");
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
        if (collision.collider.tag == "Player") // �÷��̾�� �浹 ��
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
