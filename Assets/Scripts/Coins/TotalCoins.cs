using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalCoins : Singleton<TotalCoins>
{

   // private int minCoins = 0;
    private int totalCoins;
    
    [TextArea]
    public string Notes = "Comment Here.";
    public TextMesh coinsMesh; 

    void Awake(){
        DontDestroyOnLoad(gameObject);
       
    }

    public void addTotalCoins(int coins){
        totalCoins = totalCoins + coins;
        if(coinsMesh != null){
            setText();
        }
    }

    public void diffTotalCoins(int coins){
        totalCoins = totalCoins - coins;
        if(coinsMesh != null){
            setText();
        }
    }

    private void setText(){
        coinsMesh.text = "Coins: " + totalCoins;
    }
}
