using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayUI : MonoBehaviour
{
    private LevelManager levelManager;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    [Header("Victory Panel")]
    public GameObject victoryPanel;
    public TextMeshProUGUI victoryPlayer;
    public TextMeshProUGUI victoryScore;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        levelManager = LevelManager.Instance;
        victoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = TimeSpan.FromSeconds(levelManager.currentLevelTime).ToString(@"m\:ss");
        UpdatePlayerScore();

        if (levelManager.currentLevelTime <= 0)
        {
            DecideWinner();
            victoryPanel.SetActive(true);
            Time.timeScale = 0;
        }

    }

    void UpdatePlayerScore()
    {
        scoreText.text = $"P1: {levelManager.player1.playerData.point} - P2: {levelManager.player2.playerData.point}";
    }

    void DecideWinner()
    {
        if (levelManager.player1.playerData.point > levelManager.player2.playerData.point)
        {
            victoryPlayer.text = "P1 WON!";
            victoryScore.text = "Score: " +levelManager.player1.playerData.point.ToString();
        }
        else if (levelManager.player1.playerData.point < levelManager.player2.playerData.point)
        {
            victoryPlayer.text = "P2 WON!";
            victoryScore.text = "Score: " + levelManager.player2.playerData.point.ToString();
        }
        else
        {
            victoryPlayer.text = "Draw";
            victoryScore.text = levelManager.player1.playerData.point.ToString();
        }
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
}
