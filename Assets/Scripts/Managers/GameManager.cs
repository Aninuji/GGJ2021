using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int difficulty = 5;

    [HideInInspector]
    public GameObject _playerInstance;

    // Start is called before the first frame update
    void StartGame()
    {
        WorldGenerator.Instance.SetupScene(5);
    }

    public void GameOver()
    {

    }

    public void Start(){
        StartGame();
    }
}
