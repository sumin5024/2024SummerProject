using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위해 추가

public class gm1 : MonoBehaviour
{
    public static gm1 instance;

    [Header("# Game Control")]
    public bool isLive = true; // 게임이 시작될 때 true로 설정
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int playerId;
    public float health1;
    public float maxHealth1 = 100;
    public float health2;
    public float maxHealth2 = 100;
    public int level;
    public int kill;
    public int exp;
    public int coin = 0;

    [Header("# Game Object")]
    public Transform uiJoy;
    public GameObject enemyCleaner;

    private bool isRecoveryCooldown = false;
    private bool isGamePaused = false; // 게임 일시 중지 상태 확인을 위한 변수

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        health1 = maxHealth1;
        level = EnemySpawner.Instance.currentWaveIndex + 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !isRecoveryCooldown && coin >= 20)
        {
            UseRecoveryItem();
            coin -= 20;
        }

        // 게임이 종료되었을 때에도 Enter 키로 재시작 가능하게 설정
        if (!isLive && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }

        // 스페이스바로 게임 일시 중지/재개
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePauseGame();
        }
    }

    void UseRecoveryItem()
    {
        health1 = maxHealth1;
        StartCoroutine(RecoveryItemCooldown());
    }

    IEnumerator RecoveryItemCooldown()
    {
        isRecoveryCooldown = true;
        yield return new WaitForSeconds(30f);
        isRecoveryCooldown = false;
    }

    public void EndGame()
    {
        isLive = false; // 게임 종료 상태로 설정
        Time.timeScale = 0f;

        Debug.Log("Game Over");
        MonoBehaviour[] allBehaviours = FindObjectsOfType<MonoBehaviour>();
        foreach (var behaviour in allBehaviours)
        {
            behaviour.enabled = false;
        }
    }

    public void EndGameWithDelay()
    {
        Invoke("EndGame", 1f); // 1초 후에 EndGame 메서드 호출
    }

    // 게임 재시작 함수
    void RestartGame()
    {
        // 현재 씬을 다시 로드하여 게임을 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // 게임 시간 다시 정상화
        isLive = true; // 게임을 재시작할 때 isLive를 true로 설정
    }

    // 게임 일시 중지/재개 함수
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;

        // 모든 EnemyManager 인스턴스를 찾아서 멈추거나 재개
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        foreach (var enemy in enemies)
        {
            enemy.SetPaused(isGamePaused);
        }
    }
}
