using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Configuration")]
    public float currentLevelTime;

    [Header("Player Data")]
    public Player player1;
    public Player player2;

    public static LevelManager Instance { get; private set; } // singleton pattern


    private void Awake() 
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update() 
    {
        Timer();
    }

    private void Timer()
    {
        currentLevelTime -= Time.deltaTime;

        if (currentLevelTime <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
