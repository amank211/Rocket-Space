using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public int Score = 0;

    public int HighScore;
    [SerializeField]
    TextMeshProUGUI ScoreText, textHighScore;
   

    private void Start() {
        HighScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
        textHighScore.text = "Best: " +  HighScore.ToString();
    }


    public void IncrementScore(int score) { 
        Score += score;
        ScoreText.text = "Score: 0" + Score;
        checkForHighScore();
    }

    public void checkForHighScore() {
        if (Score > HighScore) {
            PlayerPrefs.SetInt("HIGH_SCORE", Score);
            textHighScore.text = HighScore.ToString();
            textHighScore.text = "Best: " + Score.ToString();

        }
    }
}
