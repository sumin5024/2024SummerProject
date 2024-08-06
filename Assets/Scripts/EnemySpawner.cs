using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int maxYLocation = 5;
    [SerializeField] int minYLocation = -5;
    [SerializeField] int YLocation;
    [SerializeField] float minRespawnTime;

    [SerializeField] int spawnYLocation = 3;
    [SerializeField] GameObject Enemy1;
    [SerializeField] GameObject Enemy2;
    [SerializeField] GameObject Enemy3;


    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(1.0f, 4.0f));
        YLocation = Random.Range(minYLocation, maxYLocation);
        Instantiate(Enemy1, new Vector3(10, YLocation), Quaternion.identity);
        Instantiate(Enemy2, new Vector3(10, YLocation), Quaternion.identity);
        Instantiate(Enemy3, new Vector3(10, YLocation), Quaternion.identity);
    }
    
    IEnumerator Start()
    {
        while(true)
        {
            yield return StartCoroutine(SpawnEnemy());
        }
        
    }

    void Update()
    {
        
    }
}
