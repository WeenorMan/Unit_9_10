using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip[] musicClips, SFXClips;
    [SerializeField] private AudioSource musicSource, SFXSource; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {

    }

    public void PlaySFXClip(int index, float volume)
    {
        AudioClip clip = null; 

        for(int i = 0; i < SFXClips.Length; i++)
        {
            if (i == index)
            {
                if (SFXClips[i] == null) return;

                clip = SFXClips[i];
                break;
            }
        }

        SFXSource.PlayOneShot(clip, volume);
    }


    public void PlayMusicClip(int index, float volume)
    {
        if (index < 0 || index >= musicClips.Length) return;
        if (musicClips[index] == null) return;

        AudioClip clip = musicClips[index];

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = true;      
        musicSource.playOnAwake = false;

        musicSource.Play();           
    }

    public void PauseMusicClip()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void UnPauseMusicClip()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    public void StopMusicClip()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }



}


