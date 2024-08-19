using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gm1 : MonoBehaviour
{
    public static gm1 instance;
        [Header("# Game Control")]
        public bool isLive;
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
        //public Result uiResult;
        public Transform uiJoy;
        public GameObject enemyCleaner;

        private bool isRecoveryCooldown = false;


    void Awake() {
    if (instance == null) {
        instance = this;
    } else {
        Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
}
    void UseRecoveryItem() {
        health1 = maxHealth1;
        StartCoroutine(RecoveryItemCooldown());
    }

    IEnumerator RecoveryItemCooldown() {
        isRecoveryCooldown = true;
        yield return new WaitForSeconds(30f);
        isRecoveryCooldown = false;

    }

    public void EndGame()
    {
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




    void Start(){
        health1=maxHealth1;
        level = EnemySpawner.Instance.currentWaveIndex + 1;
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Y) && !isRecoveryCooldown && coin>=20)
        {
            UseRecoveryItem();
            coin -= 20;
        }

    }
}
