﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCoins : Singleton<TotalCoins>
{

   // private int minCoins = 0;
    private int totalCoins;
    public Text coinsText; 
    void Awake(){
        DontDestroyOnLoad(gameObject);
       
    }

    public void addTotalCoins(int coins){
        totalCoins = totalCoins + coins;
        if(coinsText != null){
            setText();
        }
    }

    public void diffTotalCoins(int coins){
        totalCoins = totalCoins - coins;
        if(coinsText != null){
            setText();
        }
    }

    private void setText(){
        coinsText.text = totalCoins.ToString();
    }
}