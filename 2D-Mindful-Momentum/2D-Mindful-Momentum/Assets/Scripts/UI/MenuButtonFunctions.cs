using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuButtonFunctions : MonoBehaviour
{
    
    public void OnNewGameButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Difficulty");
    }
    
    public void OnLeaderboardButtonClicked()
    {
        Debug.Log("Leaderboard button clicked");
    }
    
    public void OnCreditsButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }
    
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
    
    public void OnBackButtonClicked()
    {
        // check if we are in the difficulty scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Difficulty")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        
        // check if we are in the credits scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Credits")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        
        // check if we are in the leaderboard scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Leaderboard")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
    
    public void OnPlayButtonClicked()
    {
        Debug.Log("Player name: " + UIManager.Instance.GetPlayerName());
        Debug.Log("Difficulty: " + UIManager.Instance.difficulty);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
