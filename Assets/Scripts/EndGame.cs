using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void Exit()
    {
        // ������ ���¿����� ������ ������� �����Ƿ� Ȯ�ο� �α׸� ����մϴ�.
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ����
    #else
        Application.Quit();  // ���� ����� ���� ����
    #endif
    }
}
