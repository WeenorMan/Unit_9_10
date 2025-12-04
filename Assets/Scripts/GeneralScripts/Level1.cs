using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    PlayerControls playerControls;
    public GameObject pauseMenu;

    private void Awake()
    {
        AudioManager.instance.PlayMusicClip(1, 0.25f);
    }


    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        AudioManager.instance.PauseMusicClip();
        pauseMenu.SetActive(true);
    }
    public void ResumeButton()
    {
        Time.timeScale = 1f;
        AudioManager.instance.UnPauseMusicClip();
        pauseMenu.SetActive(false);
    }

   
}
