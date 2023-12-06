using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // load the game scene
        Debug.Log("Loading game scene...");
        SceneManager.LoadScene("GameScene");
    }
    
    public void QuitGame()
    {
        // quit the game
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
