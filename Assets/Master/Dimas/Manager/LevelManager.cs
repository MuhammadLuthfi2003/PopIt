using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Configuration")]
    public float LevelTime;
    
    private float currentLevelTime;

    private void Awake() 
    {
        currentLevelTime = LevelTime;
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
