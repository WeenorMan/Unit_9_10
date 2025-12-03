using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public PlayerController pc;

    public static ScoreManager instance;
    public int score;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text finalScoreText;
    public TMP_Text goHighScoreText;

    public void Awake()
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
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE\n " + score.ToString();
        }
    }

    

    public void HighScoreUpdate()
    {
        if (PlayerPrefs.HasKey("SavedHighScore"))
        {
            if (score > PlayerPrefs.GetInt("SavedHighScore"))
            {
                PlayerPrefs.SetInt("SavedHighScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("SavedHighScore", score);
        }

        finalScoreText.text = "FINALSCORE " + score.ToString();
        highScoreText.text = "HIGHSCORE\n " + PlayerPrefs.GetInt("SavedHighScore").ToString();
        goHighScoreText.text = "HIGHSCORE " + PlayerPrefs.GetInt("SavedHighScore").ToString();
    }
}