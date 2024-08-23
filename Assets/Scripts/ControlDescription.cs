using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDescription : MonoBehaviour
{
    public GameObject explanationPanel; // ¼³¸í Ã¢ Panel

    void Start()
    {
        explanationPanel.SetActive(false); // ¼³¸í Ã¢À» ±âº»ÀûÀ¸·Î ¼û±è
    }

    public void ShowExplanation()
    {
        explanationPanel.SetActive(true); // ¼³¸í Ã¢À» Ç¥½Ã
    }

    public void HideExplanation()
    {
        explanationPanel.SetActive(false); // ¼³¸í Ã¢À» ¼û±è
    }
}
