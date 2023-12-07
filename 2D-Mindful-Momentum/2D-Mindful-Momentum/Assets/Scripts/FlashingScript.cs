using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FlashingScript : MonoBehaviour
{
    public TMP_Text digit;
    public GameObject circle;
    public TMP_Text score;
    
    public float digTime = 0.8f; // duration for digit
    public float cirTime = 0.8f; // duration for circle
    
    public int playerScore = 0;
    
    private int _currentDigit;
    public int difficulty = 1;
    bool _isGameOver = false; // flag to stop objects from appearing
    
    public GameObject gameOverPanel;
    public TMP_Text BetterLuckText;

    void Start()
    {
        StartPlaying();
    }

    IEnumerator SwitchObjects()
    {
        while (!_isGameOver)
        {   
             // show digit
            _currentDigit = Random.Range(1, 10); // Generate a random digit between 1 and 9
            digit.text = _currentDigit.ToString(); // Display the digit
            digit.enabled = true;
            // hide circle
            circle.SetActive(false);

            yield return new WaitForSeconds(cirTime);
            
            
            // hide digit
            digit.enabled = false;
            // show circle
            circle.SetActive(true);

            yield return new WaitForSeconds(digTime);           
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            // if keypress during circle end game
            if(circle.activeSelf == true){
                EndGame();
            }

            // if keypress during digit, check digit
            if(digit.enabled == true){
                if(digit.text == "3"){
                    playerScore -= difficulty;
                    score.text = "SCORE: " + playerScore;
                }else {
                    playerScore += difficulty;
                    score.text = "SCORE: " + playerScore;
                }
            }
        }
    }
    
    public void StartPlaying() {
        _isGameOver = false;
        Debug.Log(UIManager.difficulty);
        switch (UIManager.difficulty)
        {
            case "Easy":
                difficulty = 1;
                digTime = 0.8f;
                cirTime = 0.8f;
                break;
            case "Challenging":
                difficulty = 2;
                digTime = 0.4f;
                cirTime = 0.4f;
                break;
            case "Legend":
                difficulty = 3;
                digTime = 0.2f;
                cirTime = 0.2f;
                break;
            default:
                difficulty = 1;
                break;
        }
        
        Time.timeScale = 1;
        score.text = "SCORE: " + UIManager.Instance.GetScore();
        StartCoroutine(SwitchObjects());
    }
    
    void EndGame() {
        Time.timeScale = 0;
        circle.SetActive(false);
        digit.text = "";
        gameOverPanel.SetActive(true);
        BetterLuckText.text = "Better luck next time, " + UIManager.playerName + " !";
        _isGameOver = true;
        UIManager.Instance.ResetScore();    
    }
}

