using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyAttack : EnemyManager
{
    private Animator animator2;

    public float detectionRange = 7f;
    private bool isDetection = false;

    public float attackRange = 1.7f;
    public float attackRate = 2.0f;
    private float lastAttackTime = 0.0f;
    public int attackDamage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("스켈레톤 충돌남");
            StartCoroutine(StopMoving(stopDuration));
            Invoke("CheckPlayerDistanceAttack", stopDuration);
            gm1.instance.health1-=attackDamage;
        }
    }

    public override IEnumerator StopMoving(float duration)
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
            Debug.Log("�÷��̾� ������!");
            isDetection = true;
        }
        else
        {
            Debug.Log("�÷��̾� �������� ����.");
            isDetection = false;
        }
        if (CanAttack())
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
        animator2.SetTrigger("Attack");

        float distanceToPlayer = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (distanceToPlayer <= attackRange)
        {
            Debug.Log("�÷��̾�� ���ظ� ����.");

        }
        else
        {
            Debug.Log("�÷��̾ ���� ���� �ۿ� ����.");
        }
    }
    void Start()
    {
        animator2 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
