using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public PlayerController pc;

    public static ScoreManager instance;
    public int score;

    [Header("UI References")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text finalScoreText;
    public TMP_Text goHighScoreText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    
    public void AddScore(int points)
    {
        score += points;

        UpdateScoreText();
        UpdateHighScoreTextLive();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
        UpdateHighScoreTextLive();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "SCORE\n " + score.ToString();
    }

    
    private void UpdateHighScoreTextLive()
    {
        int savedHighScore = PlayerPrefs.GetInt("SavedHighScore", 0);

        int displayHighScore = Mathf.Max(score, savedHighScore);

        if (highScoreText != null)
            highScoreText.text = "HIGHSCORE\n " + displayHighScore.ToString();
    }

    
    public void HighScoreUpdate()
    {
        int savedHighScore = PlayerPrefs.GetInt("SavedHighScore", 0);

        if (score > savedHighScore)
        {
            PlayerPrefs.SetInt("SavedHighScore", score);
        }

        int finalHighScore = PlayerPrefs.GetInt("SavedHighScore", 0);

        if (finalScoreText != null)
        {
            finalScoreText.text = "FINALSCORE " + score.ToString();
        }

        if (highScoreText != null)
        {
            highScoreText.text = "HIGHSCORE\n " + finalHighScore.ToString();
        }

        if (goHighScoreText != null)
        {
            goHighScoreText.text = "HIGHSCORE " + finalHighScore.ToString();
        }
    }

    
    public string GetHighScores()
    {
        int savedHighScore = PlayerPrefs.GetInt("SavedHighScore", 0);
        return "HIGHSCORE: " + savedHighScore.ToString();
    }
}
