using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player2Controller : MonoBehaviour
{
    public static Player2Controller instance;
    public static Player1Controller player1;
    public float moveSpeed = 5f;
    public Weapon[] weapons; // 무기 배열
    public Transform firePoint;

    private int currentWeaponIndex = 0; // 현재 무기 인덱스
    public int shotgunFireCount = 0; // 샷건 발사 횟수 카운트
    private bool isShotgunCooldown = false; // 샷건 발사 제한 여부

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMovementDirection = Vector2.right; // 초기 방향을 오른쪽으로 설정


    private SpriteRenderer spriter;
    public Animator anim;

    //스피드 부스트 영역
    public float SpeedBoostMul = 2f;
    public float SpeedBoostDuration = 8f;
    public float SpeedBoostCooldownDuration = 10f;
    private bool isSpeedBoostActive = false;
    private bool isSpeedBoostCooldown = false;
    //스피드 부스트 아이템 영역 끝

    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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

        if (Input.GetKeyDown(KeyCode.M)) // O 키
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.J) && !isSpeedBoostActive && !isSpeedBoostCooldown && gm1.instance.coin>=10)
        {
            StartCoroutine(ActivateSpeedBoost());
            gm1.instance.coin -=10;
        }

        if (Input.GetKeyDown(KeyCode.Comma)) //  키
        {
            OtherWeapon();
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

        if (currentWeapon.weaponType == WeaponType.Shotgun)
        {
            if (isShotgunCooldown)
            {
                Debug.Log("Shotgun is cooling down!");
                return;
            }

            float spreadAngle = 10f; // 총알 퍼지는 각도
            int bulletCount = 3; // 총알 수
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Shot);
            for (int i = 0; i < bulletCount; i++)
            {
                float angleOffset = (i - (bulletCount - 1) / 2f) * spreadAngle;

                float angle = Mathf.Atan2(lastMovementDirection.y, lastMovementDirection.x) * Mathf.Rad2Deg + angleOffset;

                GameObject shotBullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, Quaternion.identity);
                Rigidbody2D shotBulletRb = shotBullet.GetComponent<Rigidbody2D>();

                Vector2 bulletDirection = Quaternion.Euler(0, 0, angleOffset) * lastMovementDirection;
                shotBulletRb.velocity = bulletDirection * 15f;
                

                shotBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            }

            shotgunFireCount++;

            if (shotgunFireCount >= 10)
            {
                StartCoroutine(ShotgunCooldown());
            }
        }
        else if (currentWeapon.weaponType == WeaponType.Pistol)
        {
            GameObject bullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = lastMovementDirection * 20f;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Shot);

            // 발사 방향에 맞게 이미지 회전
            float angle = Mathf.Atan2(lastMovementDirection.y, lastMovementDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }

    IEnumerator ShotgunCooldown()
    {
        isShotgunCooldown = true;
        Debug.Log("Shotgun is on cooldown for 5 seconds.");
        yield return new WaitForSeconds(5f); // 5초 동안 샷건 발사 제한
        shotgunFireCount = 0;
        isShotgunCooldown = false;
        Debug.Log("Shotgun cooldown is over.");
    }

    void OtherWeapon()
    {
        if (weapons.Length == 0) return;

        currentWeaponIndex--;
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        Debug.Log("Player 2 weapon changed to: " + weapons[currentWeaponIndex].weaponType);
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

    public void HandlePlayerDeath1()
    {
        if (gm1.instance.health1 <= 0)
        {
            anim.SetTrigger("dead");
             moveSpeed=0;
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            if (player1 != null)
            {
                player1.HandlePlayerDeath2();
            }
        }
    }

    public void HandlePlayerDeath2()
    {
        anim.SetTrigger("dead");
        gm1.instance.EndGameWithDelay();
    }
    private void Start()
    {
        if (player1 == null)
        {
            player1 = FindObjectOfType<Player1Controller>();
        }
    }
}
