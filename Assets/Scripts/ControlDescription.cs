using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDescription : MonoBehaviour
{
    public GameObject explanationPanel; // 설명 창 Panel

    void Start()
    {
        explanationPanel.SetActive(false); // 설명 창을 기본적으로 숨김
    }

    public void ShowExplanation()
    {
        explanationPanel.SetActive(true); // 설명 창을 표시
    }

    public void HideExplanation()
    {
        explanationPanel.SetActive(false); // 설명 창을 숨김
    }
}
