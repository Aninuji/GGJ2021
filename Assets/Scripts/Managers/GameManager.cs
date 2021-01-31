﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int level = 1;
    public int difficulty = 5;

    [HideInInspector]
    public GameObject _playerInstance;

    public GameObject gameOverPanel, winLevelPanel;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
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
    // Start is called before the first frame update


    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        level++;
        difficulty *= difficulty;
    }
    public void WinLevel()
    {
        winLevelPanel.SetActive(true);
        level++;

        difficulty *= difficulty;

    }


    public void ChangeScene(string name)
    {
        LevelLoader.Instance.LoadScene(name);

        winLevelPanel.SetActive(false);
        gameOverPanel.SetActive(false);


    }
}
