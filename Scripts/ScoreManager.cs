using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public int Score = 0;

    [SerializeField]
    TextMeshProUGUI ScoreText;


    public void IncrementScore() { 
        Score++;
        ScoreText.text = "Score: 000000000" + Score;
    }
}
