using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void Exit()
    {
        // 에디터 상태에서는 게임이 종료되지 않으므로 확인용 로그를 출력합니다.
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서 게임 종료
    #else
        Application.Quit();  // 실제 빌드된 게임 종료
    #endif
    }
}
