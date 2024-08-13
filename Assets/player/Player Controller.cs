using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int pl1health = 100;
    public Weapon[] weapons; // 무기 배열
    public Transform firePoint;

    //스피드 부스트 영역
    public float SpeedBoostMul = 2f;
    public float SpeedBoostDuration = 8f;
    public float SpeedBoostCooldownDuration = 10f;
    private bool isSpeedBoostActive = false;
    private bool isSpeedBoostCooldown = false;
    //스피드 부스트 아이템 영역 끝
    private int currentWeaponIndex = 0; // 현재 무기 인덱스

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMovementDirection;
    private SpriteRenderer spriter;
    private Animator anim;

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

        if (movement != Vector2.zero)
        {
            lastMovementDirection = movement.normalized;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // 숫자 1 키
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.E) && !isSpeedBoostActive && !isSpeedBoostCooldown)
        {
            StartCoroutine(ActivateSpeedBoost());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // 숫자 2 키
        {
            PreviousWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // 숫자 3 키
        {
            NextWeapon();
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
        if (weapons.Length == 0)
        {
            Debug.LogError("No weapons assigned!");
            return;
        }

        if (currentWeaponIndex < 0 || currentWeaponIndex >= weapons.Length)
        {
            Debug.LogError("Invalid weapon index!");
            return;
        }

        Weapon currentWeapon = weapons[currentWeaponIndex];

        if (currentWeapon.weaponType == WeaponType.Pistol)
        {
            GameObject bullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = lastMovementDirection * 20f;
        }
        else if (currentWeapon.weaponType == WeaponType.Shotgun)
        {
            float spreadAngle = 10f; // 총알 퍼지는 각도
            int bulletCount = 3; // 총알 수

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = (i - (bulletCount - 1) / 2f) * spreadAngle;

                // `lastMovementDirection`을 기준으로 총알 발사 방향 설정
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lastMovementDirection) * Quaternion.Euler(0, 0, angle);

                GameObject shotBullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, Quaternion.identity);
                Rigidbody2D shotBulletRb = shotBullet.GetComponent<Rigidbody2D>();

                // 총알의 방향을 `rotation * Vector2.up`으로 설정
                Vector2 bulletDirection = rotation * Vector2.up;

                // 디버깅 로그
                Debug.Log($"Bullet Angle: {angle}, Bullet Direction: {bulletDirection}, Rotation: {rotation.eulerAngles}");

                shotBulletRb.velocity = bulletDirection * 15f;
            }
        }
    }

    void PreviousWeapon()
    {
        if (weapons.Length == 0) return;

        currentWeaponIndex--;
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        Debug.Log("Player 1 weapon changed to: " + weapons[currentWeaponIndex].weaponType);
    }

    void NextWeapon()
    {
        if (weapons.Length == 0) return;

        currentWeaponIndex++;
        if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }
        Debug.Log("Player 1 weapon changed to: " + weapons[currentWeaponIndex].weaponType);
    }

    IEnumerator ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        moveSpeed *= SpeedBoostMul;

        yield return new WaitForSeconds(SpeedBoostDuration);

        moveSpeed /= SpeedBoostMul;
        isSpeedBoostActive = false;

        StartCoroutine(SpeedBoostCooldown());
    }

    IEnumerator SpeedBoostCooldown()
    {
        isSpeedBoostCooldown = true;

        yield return new WaitForSeconds(SpeedBoostCooldownDuration);

        isSpeedBoostCooldown = false;
    }
}
