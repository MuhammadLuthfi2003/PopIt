using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    private LevelManager levelManager;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = TimeSpan.FromSeconds(levelManager.currentLevelTime).ToString(@"m\:ss");

    }
}
