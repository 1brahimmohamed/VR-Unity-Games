using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverCanvas;
    
    
    private void Start()
    {
        gameOverCanvas.SetActive(false);
    }
    
    public void Update()
    {
        if (EnemyCollision.IsGameOver)
        {
            Debug.Log("Game Over");
            gameOverCanvas.SetActive(true);
        }
    }
    
    public void RestartGame()
    {
        Debug.Log("Restarting Game");
        Reset();
        SceneManager.LoadScene("GameScene");
    }
    
    public void BackToMainMenu()
    {
        Reset();
        SceneManager.LoadScene("MenuScene");
    }


    private void Reset()
    {
        gameOverCanvas.SetActive(false);
        Time.timeScale = 1;
        EnemyCollision.IsGameOver = false;
    }
}
