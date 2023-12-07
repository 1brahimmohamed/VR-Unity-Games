using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   public static UIManager Instance; // Singleton instance
   
   public string playerName = "Player";
   public string difficulty = "Challenging";
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
    
    public void SetDifficulty(string diff)
    {
        difficulty = diff;
    }
    
    public void SetScore(int newScore)
    {
        score = newScore;
    }
    
    public void ResetScore()
    {
        score = 0;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
    
    public string GetPlayerName()
    {

        if (nameInputField != null)
        {
            if (nameInputField.text == "")
            {
                playerName = "Player";
            }
            
            else playerName = nameInputField.text;
        }
        
        return playerName;
    }
    
}
