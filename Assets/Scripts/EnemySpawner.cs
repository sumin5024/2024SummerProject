using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.FilePathAttribute;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    //[SerializeField] int maxYLocation = 5;
    //[SerializeField] int minYLocation = -5;
    [SerializeField] int YLocation;
    [SerializeField] float minRespawnTime;

    //[SerializeField] int spawnYLocation = 3;
    [SerializeField] static GameObject Enemy1;
    [SerializeField] static GameObject Enemy2;
    [SerializeField] static GameObject Enemy3;
    [SerializeField] static GameObject Enemy4;
    [SerializeField] static GameObject Enemy5;

    public int enemiesRemaining = 0;
    public Wave[] waves = new Wave[10];
    public float spawnTime = 50.0f; // �����ֱ�
    private bool isSpawning = false;  // ���� ����
    public int currentWaveIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    [System.Serializable]
    public class Wave
    {
        
        
        public int[] enemyCount = new int[5]; // �� ����
       
        public GameObject[] enemyPrefab = new GameObject[5] { Enemy1, Enemy2, Enemy3, Enemy4, Enemy5 };
    }


    IEnumerator StartWave()
    {
        Wave currentWave = waves[currentWaveIndex];
        //yield return new WaitForSeconds(spawnTime);
        // YLocation = Random.Range(minYLocation, maxYLocation);
        enemiesRemaining = 0;
        isSpawning = true;

        for (int i = 0; i < currentWave.enemyPrefab.Length; i++)
        {
            for (int j = 0; j < currentWave.enemyCount[i]; j++)
            {
                
                SpawnEnemy(currentWave.enemyPrefab[i]);
                enemiesRemaining++;
                yield return new WaitForSeconds(1);
            }
        }

        isSpawning = false;

    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, new Vector3(-49, Random.Range(-12.0f, 26.0f)), Quaternion.identity);
    }

    public void NextWave()
    {
        if(enemiesRemaining <= 0 && !isSpawning)
        {
            currentWaveIndex++;
            gm1.instance.level++;
            if(currentWaveIndex < waves.Length)
            {
                StartCoroutine(StartWave());
            }
            else
            {
                AudioManager.instance.PlayBgm(false);
                gm1.instance.RestartGame();
                SceneManager.LoadScene("EndScene");
            }
        }
    }

    IEnumerator Start()
    {

        yield return StartCoroutine(StartWave());

    }

    void Update()
    {
        /*if(currentWaveIndex >= waves.Length)
        {
            SceneManager.LoadScene("EndScene");
        }*/
    }
}
