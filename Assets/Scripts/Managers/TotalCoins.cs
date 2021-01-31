using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCoins : MonoBehaviour
{
    public static TotalCoins Instance;

    public delegate void OnCoinChange(int newValue);
    public event OnCoinChange coinChangeEvent;

    // private int minCoins = 0;
    public int _totalCoins;
    public int totalCoins
    {
        get
        {
            return _totalCoins;
        }
        set
        {
            _totalCoins = value;
            setText();
            if (coinChangeEvent != null) coinChangeEvent(value);

        }
    }

    public Text coinsText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void addTotalCoins(int coins)
    {
        totalCoins = totalCoins + coins;
        if (coinsText != null)
        {
            setText();
        }
    }

    public void diffTotalCoins(int coins)
    {
        totalCoins = totalCoins - coins;
        if (coinsText != null)
        {
            setText();
        }
    }

    private void setText()
    {
        coinsText.text = totalCoins.ToString();
    }
}
