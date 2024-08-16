using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gm1 : MonoBehaviour
{
    private Animator animator;
    private Collider2D coll;
    
    public static gm1 instance;
        [Header("# Game Control")]
        public bool isLive = true;
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
    
        [Header("# Game Object")]
        //public Result uiResult;
        public Transform uiJoy;
        public GameObject enemyCleaner;


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
        health1=maxHealth1;
        level = EnemySpawner.Instance.currentWaveIndex + 1;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

    
    }
    void Update()
    {
        

    }
    

    
}
