using UnityEngine;
using TMPro;
public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;

    public int currentLives = 2;
    public GameObject gameOverScreen;
    public TMP_Text livesText;

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


    public void LoseLife()
    {
        currentLives -= 1;
        livesText.text = currentLives.ToString();
        if (currentLives <= 0)
        {
            Time.timeScale = 0f;
            AudioManager.instance.StopMusicClip();
            AudioManager.instance.PlaySFXClip(4, 0.25f);
            gameOverScreen.SetActive(true);

            scoreManager.HighScoreUpdate();
        }
    }
}
