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
        UIManager.Instance.GetPlayerNameFromInputField();
        Debug.Log("Player name: " + UIManager.playerName);
        Debug.Log("Difficulty: " + UIManager.difficulty);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    
    public void OnMainMenuButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    
    public void OnPlayAgainButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
