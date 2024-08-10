using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    [SerializeField] int maxYLocation = 5;
    [SerializeField] int minYLocation = -5;
    [SerializeField] int YLocation;
    [SerializeField] float minRespawnTime;

    [SerializeField] int spawnYLocation = 3;
    [SerializeField] static GameObject Enemy1;
    [SerializeField] static GameObject Enemy2;
    [SerializeField] static GameObject Enemy3;
    [SerializeField] static GameObject Enemy4;
    [SerializeField] static GameObject Enemy5;

    public int enemiesRemaining = 0;
    public Wave[] waves = new Wave[10];
    public float spawnTime = 50.0f; // 생성주기
    private bool isSpawning = false;  // 스폰 상태
    public int currentWaveIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    [System.Serializable]
    public class Wave
    {
        
        
        public int[] enemyCount = new int[5]; // 적 숫자
       
        public GameObject[] enemyPrefab = new GameObject[5] { Enemy1, Enemy2, Enemy3, Enemy4, Enemy5 };
    }


    IEnumerator StartWave()
    {
        Wave currentWave = waves[currentWaveIndex];
        //yield return new WaitForSeconds(spawnTime);
        // YLocation = Random.Range(minYLocation, maxYLocation);
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
        Instantiate(enemyPrefab, new Vector3(10, YLocation), Quaternion.identity);
    }

    public void NextWave()
    {
        if(enemiesRemaining == 0 && !isSpawning)
        {
            currentWaveIndex++;
            if(currentWaveIndex < waves.Length)
            {
                StartCoroutine(StartWave());
            }
            else
            {
                Debug.Log("모든 웨이브 끝");
            }
        }
    }

    IEnumerator Start()
    {

        yield return StartCoroutine(StartWave());

    }

    void Update()
    {
 
    }
}
