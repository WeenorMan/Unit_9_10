using UnityEngine;
using TMPro;
public class LivesManager : MonoBehaviour
{
    public int currentLives = 2;
    public GameObject gameOverScreen;
    public TMP_Text livesText;

    public ScoreManager scoreManager;

    public void LoseLife()
    {
        currentLives -= 1;
        livesText.text = currentLives.ToString();
    }
}
