using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D cc;

    public float speed = 0.005f;
    protected bool isMoving = true;
    protected float stopDuration = 1.5f;


    public Transform player1;
    public Transform player2;
    private Transform targetPlayer;


    public int maxHealth = 100;
    public int currentHealth;

    public int collisionDamage = 10;

    public GameObject Player;
    private SpriteRenderer spriteRenderer;


    private Attract_Item attractItem;



    private void MoveTowardsTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed);
    }

     private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPlayer.transform.position, speed);
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

    public virtual IEnumerator StopMoving(float duration)
    {
        isMoving = false;
        yield return new WaitForSeconds(duration);
        isMoving = true;
    }


    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        if (currentHealth <= 0)
        {
            Collider2D Ecollider = GetComponent<Collider2D>();
            if(Ecollider != null)
            {
                Ecollider.enabled = false;
            }
            Die();
            gm1.instance.coin += 1; // 자석 = 5, 빨라지기 = 10, 체력회복 = 20
        }
    }

    private void Die()
    {
        animator.SetBool("Dead", true);
        StartCoroutine(StopMoving(1f));
        cc.isTrigger = true;
        Debug.Log("Enemy Died");
        Destroy(gameObject, 1f);
        EnemySpawner.Instance.enemiesRemaining--;
        
    }




    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        attractItem = FindObjectOfType<Attract_Item>();
        animator = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
     
    }


    void Update()
    {
        float distanceToPlayer1 = Vector2.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, player2.position);

        if (distanceToPlayer1 < distanceToPlayer2)
        {
            targetPlayer = player1;
        }
        else
        {
            targetPlayer = player2;
        }


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
            UpdateDirection(targetPlayer.transform.position);
        }
        if (EnemySpawner.Instance.enemiesRemaining <= 0)
        {
            EnemySpawner.Instance.NextWave();
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Player1Controller player1 = collision.gameObject.GetComponent<Player1Controller>();
            Player2Controller player2 = collision.gameObject.GetComponent<Player2Controller>();
            Debug.Log("기본 적 충돌남");
            StartCoroutine(StopMoving(stopDuration));
            //Invoke("CheckPlayerDistanceAttack", stopDuration);
            gm1.instance.health1 -= collisionDamage;
            if(player1 != null)
            {
                player1.HandlePlayerDeath1();
            }
            if(player2 != null)
            {
                player2.HandlePlayerDeath1();
            }

        }
    }
}
