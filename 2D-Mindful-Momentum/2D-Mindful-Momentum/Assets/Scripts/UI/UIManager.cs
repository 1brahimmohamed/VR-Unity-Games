using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   public static UIManager Instance; // Singleton instance
   
   public static string playerName = "Player";
   public static string difficulty = "Challenging";
   public int score = 0;
   
   public TMP_InputField nameInputField;
   
   private void Awake()
   {
       // Singleton pattern to ensure only one instance exists
       if (Instance == null)
       {
           Instance = this;
       }
       else
       {
           Destroy(gameObject);
       }
   }
   
    public void SetPlayerName(string name)
    {
         playerName = name;
    }

    public void  GetPlayerNameFromInputField()
    {
        if (nameInputField != null)
        {
            if (nameInputField.text == "")
            {
                playerName = "Player";
            }
            
            else playerName = nameInputField.text;
        }
    }

    public string GetPlayerName()
    {
        return playerName;
    }
    
    public void SetDifficulty(string diff)
    {
        difficulty = diff;
    }
    
    public string GetDifficulty()
    {
        return difficulty;
    }
    
    public void SetScore(int newScore)
    {
        score = newScore;
    }
    
    public int GetScore()
    {
        return score;
    }
    
    public void ResetScore()
    {
        score = 0;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
    

    
}
