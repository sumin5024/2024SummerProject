using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDescription : MonoBehaviour
{
    public GameObject explanationPanel;
     // ���� â Panel

    void Start()
    {
        explanationPanel.SetActive(false); // ���� â�� �⺻������ ����
    }

    public void ShowExplanation()
    {
        explanationPanel.SetActive(true); // ���� â�� ǥ��
    }

    public void HideExplanation()
    {
        explanationPanel.SetActive(false); // ���� â�� ����
    }
}
