using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int difficulty = 1;
    public int time = 30;
    public bool gameOver = false;
    [SerializeField] int score;
    [SerializeField] AudioClip buttonAudioClip;

    public int Score {
        get => score;
        set {
            score = value;
            UIManager.Instance.UpdateUIScore(score);
            if(score % 1000 == 0) difficulty++;
        }
    }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) { 
            Instance= this;
        }
    }

    public void Start()
    {
        Score = 0;
        StartCoroutine(CountDownRoutine());
    }

    IEnumerator CountDownRoutine()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            UIManager.Instance.UpdateUITime(time);
        }

        GameManager.Instance.gameOver = true;
        UIManager.Instance.ShowGameOverScreen();

    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        StartCoroutine(PlayButtonAudio());
        
    }

    public void MainMenu()
    {
        AudioSource.PlayClipAtPoint(buttonAudioClip, transform.position);
        UIManager.Instance.ShowMainMenuScreen();
    }

    IEnumerator PlayButtonAudio() {
        AudioSource.PlayClipAtPoint(buttonAudioClip, transform.position);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Game");
    }


}
