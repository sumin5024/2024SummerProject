using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager�� ����ϱ� ���� �߰�

public class gm1 : MonoBehaviour
{
    public static gm1 instance;

    [Header("# Game Control")]
    public bool isLive = true; // ������ ���۵� �� true�� ����
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
    private bool isGamePaused = false; // ���� �Ͻ� ���� ���� Ȯ���� ���� ����

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

        // ������ ����Ǿ��� ������ Enter Ű�� ����� �����ϰ� ����
        if (!isLive && Input.GetKeyDown(KeyCode.Return))
        {
            RestartGame();
        }

        // �����̽��ٷ� ���� �Ͻ� ����/�簳
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
        isLive = false; // ���� ���� ���·� ����
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
        Invoke("EndGame", 1f); // 1�� �Ŀ� EndGame �޼��� ȣ��
    }

    // ���� ����� �Լ�
    void RestartGame()
    {
        // ���� ���� �ٽ� �ε��Ͽ� ������ �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // ���� �ð� �ٽ� ����ȭ
        isLive = true; // ������ ������� �� isLive�� true�� ����
    }

    // ���� �Ͻ� ����/�簳 �Լ�
    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;

        // ��� EnemyManager �ν��Ͻ��� ã�Ƽ� ���߰ų� �簳
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        foreach (var enemy in enemies)
        {
            enemy.SetPaused(isGamePaused);
        }
    }
}
