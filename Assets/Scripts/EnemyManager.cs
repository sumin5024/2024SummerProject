using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D bc;

    public float speed = 0.005f;
    protected bool isMoving = true;
    protected float stopDuration = 1.5f;


    public Transform player;


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
        if (currentHealth <= 0)
        {
            Collider2D Ecollider = GetComponent<Collider2D>();
            if(Ecollider != null)
            {
                Ecollider.enabled = false;
            }
            Die();
        }
    }

    private void Die()
    {
        animator.SetBool("Dead", true);
        StartCoroutine(StopMoving(1f));
        //bc.isTrigger = true;
        Debug.Log("Enemy Died");
        Destroy(gameObject, 1f);
        EnemySpawner.Instance.enemiesRemaining--;
        if (EnemySpawner.Instance.enemiesRemaining == 0)
        {
            EnemySpawner.Instance.NextWave();
        }
    }




    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        attractItem = FindObjectOfType<Attract_Item>();
        animator = GetComponent<Animator>();
        //bc = GetComponent<BoxCollider2D>();
        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("기본 적 충돌남");
            StartCoroutine(StopMoving(stopDuration));
            Invoke("CheckPlayerDistanceAttack", stopDuration);
            gm1.instance.health1 -= collisionDamage;
            
        }
    }
}
