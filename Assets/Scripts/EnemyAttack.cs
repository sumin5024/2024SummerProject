using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRate = 1.0f;
    private float lastAttackTime = 0.0f;
    public int attackDamage = 10;
    private GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (Time.time - lastAttackTime > 1f / attackRate)
            {
                lastAttackTime = Time.time;
                Attack();
            }
        }
    }

    private void Update()
    {

    }

    private void Attack()
    {
        // Attack logic here
        Debug.Log("Attack!");
    }
}
