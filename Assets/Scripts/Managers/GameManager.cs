using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int difficulty = 5;

    [HideInInspector]
    public GameObject _playerInstance;

    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void StartGame()
    {
        WorldGenerator.Instance.SetupScene(5);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void Start()
    {
        StartGame();
    }

    public void ChangeScene(string name)
    {
        LevelLoader.Instance.LoadScene(name);

    }
}
