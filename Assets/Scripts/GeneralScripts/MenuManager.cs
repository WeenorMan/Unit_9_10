using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject highScoreScreen;
    public GameObject guideScreen;
    public TMP_Text text;

    private void Awake()
    {
        AudioManager.instance.PlayMusicClip(0, 0.25f);
    }


    public void Level1()
    {
        SceneManager.LoadScene("Level");
        Time.timeScale = 1f;
    }

    public void HighScores()
    {
        menuScreen.SetActive(false);
        highScoreScreen.SetActive(true);
        text.text = ScoreManager.instance.GetHighScores();
    }

    public void GuideScreen()
    {
        menuScreen.SetActive(false);
        guideScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        highScoreScreen.SetActive(false);
        guideScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

}
