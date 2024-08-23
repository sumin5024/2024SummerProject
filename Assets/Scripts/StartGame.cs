using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartButton()
    {
        // 게임의 메인 씬을 로드
        SceneManager.LoadScene("MainScene"); 
    }
}
