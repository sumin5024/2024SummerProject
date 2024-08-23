using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text coinText;
    public int coinCount;
    // Start is called before the first frame update
    void Start()
    {
        coinCount = gm1.instance.coin;
        UpdateCoinText();
    }

    // Update is called once per frame
    void Update()
    {
        if (coinCount != gm1.instance.coin) {
            coinCount = gm1.instance.coin;
            UpdateCoinText();
        }
        
    }
    void UpdateCoinText() {
        coinText.text = "Coins: " + coinCount;
    }
}

