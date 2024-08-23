using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gm1 : MonoBehaviour
{
    public static gm1 instance;

    [Header("# Game Control")]
    public bool isLive = true; // 게임 상태 초기화
    public float gameTime;
    public float maxGameTime = 20f;

    [Header("# Player Info")]
    public int playerId;
    public float health1;
    public float maxHealth1 = 100;
    public int level;
    public int kill;
    public int exp;
    public int coin = 0;

    [Header("# Game Object")]
    public Transform uiJoy;
    public GameObject enemyCleaner;

    private bool isRecoveryCooldown = false;
    private bool isGamePaused = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 게임 매니저를 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
            return; // 이 객체를 파괴한 후 더 이상 진행하지 않음
        }
    }

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !isRecoveryCooldown && coin >= 20)
        {
            UseRecoveryItem();
            coin -= 20;
        }

        // 엔터 키로 게임 재시작
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }

        // 스페이스바로 게임 일시 중지/재개
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePauseGame();
        }
    }

    void InitializeGame()
    {
        isLive = true;
        health1 = maxHealth1; // 플레이어 체력 초기화
        level = 1;
        kill = 0;
        exp = 0;
        coin = 0;
        Time.timeScale = 1f; // 시간 스케일 초기화
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
    isLive = false;
    Time.timeScale = 0f;

    Debug.Log("Game Over");
    MonoBehaviour[] allBehaviours = FindObjectsOfType<MonoBehaviour>();
    foreach (var behaviour in allBehaviours)
    {
        // gm1 인스턴스 자신은 비활성화하지 않음
        if (behaviour != this)
        {
            behaviour.enabled = false;
        }
    }
}


    public void EndGameWithDelay()
    {
        Invoke("EndGame", 1f); // 1초 후에 EndGame 메서드 호출
    }

    // 게임 재시작 함수
    void RestartGame()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 후 초기화하도록 콜백 등록
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 씬이 로드된 후 호출되는 콜백
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeGame(); // 씬이 로드될 때 게임 상태 초기화
        SceneManager.sceneLoaded -= OnSceneLoaded; // 콜백 제거
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 후 초기화하도록 콜백 등록
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 씬 로드 후 초기화를 위한 콜백 제거
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
