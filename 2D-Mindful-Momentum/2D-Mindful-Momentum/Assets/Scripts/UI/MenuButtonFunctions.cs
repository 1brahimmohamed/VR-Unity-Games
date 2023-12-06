using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonFunctions : MonoBehaviour
{
    public void OnPlayButtonClicked()
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
        Debug.Log("Quit button clicked");
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
    
    public void OnEasyButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    
    public void OnMediumButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    
    public void OnHardButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
