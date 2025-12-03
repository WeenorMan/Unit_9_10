using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.instance.PlayMusicClip(0, 0.25f);
    }


    public void Level1()
    {
        SceneManager.LoadScene("Level");
        Time.timeScale = 1f;
    }

}
