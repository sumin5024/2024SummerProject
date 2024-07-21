using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D bc;

    public float speed = 0.005f;
    private bool isMoving = true;
    private float stopDuration = 1.5f;


    public Transform player;
    public float detectionRange = 7f;
    private bool isDetection = false;


    public int maxHealth = 100;
    public int currentHealth;

    


    public float attackRange = 1.7f;
    public float attackRate = 1.0f;
    private float lastAttackTime = 0.0f;
    public int attackDamage = 10;

    public GameObject Player;
    private SpriteRenderer spriteRenderer;


    private Attract_Item attractItem;


    private void MoveTowardsTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed);
    }


    private void UpdateDirection(Vector3 target)
    {
        if (target.x < gameObject.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private IEnumerator StopMoving(float duration)
    {
        isMoving = false;
        yield return new WaitForSeconds(duration);
        isMoving = true;
    }

    private void CheckPlayerDistanceAttack()
    {
        float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
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

    private void Attack()
    {
        lastAttackTime = Time.time;
        Debug.Log("Attack!");

        float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if(distanceToPlayer <= attackRange)
        {
            Debug.Log("플레이어에게 피해를 입힘.");
           
        }
        else
        {
            Debug.Log("플레이어가 공격 범위 밖에 있음.");
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
        animator.SetBool("Dead", true);
        StartCoroutine(StopMoving(1f));
        bc.isTrigger = true;
        Debug.Log("Enemy Died");
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            StartCoroutine(StopMoving(stopDuration));
            Invoke("CheckPlayerDistanceAttack", stopDuration);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        attractItem = FindObjectOfType<Attract_Item>();
        animator = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (attractItem != null && attractItem.IsAttractActive())
        {
            MoveTowardsTarget(attractItem.GetTargetPosition());
            UpdateDirection(attractItem.GetTargetPosition());
        }
        else
        {
            if (isMoving)
            {
                MoveTowardsPlayer();
            }
            UpdateDirection(Player.transform.position);
        }
    }


    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, speed);
    }


    private void UpdateDirection()
    {
        if (Player.transform.position.x < gameObject.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
