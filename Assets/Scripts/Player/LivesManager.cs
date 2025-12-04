using System;
using UnityEngine;
using TMPro;
public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;

    public int currentLives = 3;
    public GameObject gameOverScreen;
    public TMP_Text livesText;
    private int extraLivesMilestones = 0;

    public ScoreManager scoreManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateLivesUI();
    }

    private void Update()
    {
        CheckExtraLife();
    }

    private void CheckExtraLife()
    {
        if (scoreManager == null) return;

        int milestonesReached = scoreManager.score / 5000;

        // Award a life for each new milestone
        while (milestonesReached > extraLivesMilestones)
        {
            currentLives += 1;
            extraLivesMilestones += 1;
            UpdateLivesUI();
            AudioManager.instance.PlaySFXClip(1, 0.25f);
        }
    }

    public void LoseLife()
    {
        currentLives -= 1;
        UpdateLivesUI();
        if (currentLives <= 0)
        {
            Time.timeScale = 0f;
            AudioManager.instance.StopMusicClip();
            AudioManager.instance.PlaySFXClip(4, 0.25f);
            gameOverScreen.SetActive(true);

            scoreManager.HighScoreUpdate();
        }
    }
    public void InstaDeath()
    {
        currentLives = 0;
        UpdateLivesUI();
        if (currentLives <= 0)
        {
            Time.timeScale = 0f;
            AudioManager.instance.StopMusicClip();
            AudioManager.instance.PlaySFXClip(4, 0.25f);
            gameOverScreen.SetActive(true);

            scoreManager.HighScoreUpdate();
        }
    }


    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = currentLives.ToString();
        }
    }

}
