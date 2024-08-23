using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class refreshmain : MonoBehaviour
{
        public void RestartGame()
    {
       // SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 후 초기화하도록 콜백 등록
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
