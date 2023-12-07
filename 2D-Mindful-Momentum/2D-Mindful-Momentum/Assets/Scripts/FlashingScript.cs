using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FlashingScript : MonoBehaviour
{
    public Text digit;
    public GameObject circle;
    public TextMeshProUGUI tmp;
    public float digTime = 0.8f; // duration for digit
    public float cirTime = 0.8f; // duration for circle
    public int playerScore = 0;
    public int maxScore = 15;
    private int currentDigit;
    public int difficulty = 1;
    bool isGameOver = false; // flag to stop objects from appearing

    void Start()
    {
        StartCoroutine(SwitchObjects());
    }

    IEnumerator SwitchObjects()
    {
       
        while (isGameOver == false)
        {   
             // show digit
            currentDigit = Random.Range(1, 10); // Generate a random digit between 1 and 9
            digit.text = currentDigit.ToString(); // Display the digit
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
                endGame();
            }

            // if keypress during digit, check digit
            if(digit.enabled == true){
                if(digit.text == "3"){
                    endGame();
                }else {
                    playerScore+= difficulty;
                    tmp.text = "SCORE: " + playerScore;
                }
            }
        }

        if(playerScore == maxScore){
            win();
        }

    }

    void endGame() {

        circle.SetActive(false);
        digit.text = "Game Over!";
        digit.enabled = true;
        isGameOver = true;
        // code for saving score and going back to main menu here
    }

    void win() {
        circle.SetActive(false);
        digit.text = "You Won!";
        digit.enabled = true;
        isGameOver = true;
    }

}

