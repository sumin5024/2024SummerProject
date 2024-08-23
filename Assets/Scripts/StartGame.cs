using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public void StartButton()
    {
        // ������ ���� ���� �ε�
        AudioManager.instance.PlayBgm(false);
        SceneManager.LoadScene("MainScene"); 
    }
    void Start()
    {
        AudioManager.instance.PlayBgm(true);
    }
}
