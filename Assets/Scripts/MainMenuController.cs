using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip buttonAudioClip;

    public void PlayAgain()
    {
        Time.timeScale = 1;
        AudioSource.PlayClipAtPoint(buttonAudioClip, transform.position);
        Invoke("LoadGameScene", 0.3f);

    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

}
