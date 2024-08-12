using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int pl2health = 100;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMovementDirection;
    SpriteRenderer spriter;
    Animator anim;
    //스피드 부스트 영역
    public float SpeedBoostMul = 2f;
    public float SpeedBoostDuration = 8f;
    public float SpeedBoostCooldownDuration = 10f;
    private bool isSpeedBoostActive = false;
    private bool isSpeedBoostCooldown = false;
    //스피드 부스트 아이템 영역 끝

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 입력 처리
        movement.x = Input.GetAxisRaw("Horizontal2");
        movement.y = Input.GetAxisRaw("Vertical2");

        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement.normalized;
        }

        if (Input.GetKeyDown(KeyCode.O)) // O 키
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.U) && !isSpeedBoostActive && !isSpeedBoostCooldown) {
            StartCoroutine(ActivateSpeedBoost());
        }
    }

    void FixedUpdate()
    {
        // 이동 처리
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        anim.SetFloat("Speed", movement.magnitude);
        if (movement.x != 0)
        {
            spriter.flipX = movement.x < 0;
        }
    }

    void Shoot()
    {
        Debug.Log("Bullet fired by Player 2");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = lastMovementDirection * 20f; // 총알의 속도 설정
    }
    IEnumerator ActivateSpeedBoost() {
        isSpeedBoostActive = true;
        moveSpeed *= SpeedBoostMul;

        yield return new WaitForSeconds(SpeedBoostDuration);

        moveSpeed /=SpeedBoostMul;
        isSpeedBoostActive = !isSpeedBoostActive;

        StartCoroutine(SpeedBoostCooldown());

    }

    IEnumerator SpeedBoostCooldown() {
        isSpeedBoostCooldown = true;

        yield return new WaitForSeconds(SpeedBoostCooldownDuration);

         isSpeedBoostCooldown = false;

    }
}
