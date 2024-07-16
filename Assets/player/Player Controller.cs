using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fireRate = 1f; // 초당 발사 횟수
    private float nextFireTime = 0f; // 다음 발사 시간

    public GameObject bulletPrefab; // 총알 프리팹
    public Transform firePoint; // 총알 발사 위치

    private Rigidbody2D rb;
    private Vector2 movement;
    SpriteRenderer spriter;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 입력 처리
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 플레이어 이동 방향에 따라 firePoint 회전
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        // 플레이어 이동 방향에 따라 firePoint 회전
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        // 총 쏘기
        if (Input.GetKeyDown(KeyCode.Slash) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                Vector2 shootDirection = firePoint.up;
                bulletScript.SetDirection(shootDirection);
                Debug.Log("Bullet fired in direction: " + shootDirection);
            }
            else
            {
                Debug.LogError("Bullet script not found on the bullet prefab.");
            }
        }
        else
        {
            Debug.LogError("BulletPrefab or FirePoint is not assigned.");
        }
    }


    void FixedUpdate()
    {
        // 이동 처리
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", movement.magnitude);
        if (movement.x != 0)
        {
            spriter.flipX = movement.x < 0;
        }
    }
}
