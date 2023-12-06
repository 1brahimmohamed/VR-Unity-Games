using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TMP_Text _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<TMP_Text>();
    }
    
    private void Update()
    {
        if (!EnemyCollision.IsGameOver)
        {
             _scoreText.text = "Score: " + EnemyCollision.EnemyCount;
        }
        else
        {
            EnemyCollision.EnemyCount = 0;
        }
    }
}
