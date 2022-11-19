using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject mainMenuScreen;
    [SerializeField] TMP_Text finalScore;

    public static UIManager Instance;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    public void UpdateUIScore(int newScore) { 
        scoreText.text = newScore.ToString();
        finalScoreText.text = newScore.ToString();
    }

    public void UpdateUIHealth(int newHealth) {
        healthText.text = newHealth.ToString();
    }

    public void UpdateUITime(int newTime)
    {
        timeText.text = newTime.ToString();
    }

    public void ShowGameOverScreen() {
        Time.timeScale = 0; // Freeze game 
        gameOverScreen.SetActive(true);
        finalScore.text = ""+GameManager.Instance.Score;
    }

    public void ShowMainMenuScreen()
    {
        Time.timeScale = 0; // Freeze game 
        mainMenuScreen.SetActive(true);
    }

}
