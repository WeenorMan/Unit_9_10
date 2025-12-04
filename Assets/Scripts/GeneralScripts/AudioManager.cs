using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // ---------------------------------------------------------
    // Singleton
    // ---------------------------------------------------------

    public static AudioManager instance;

    // ---------------------------------------------------------
    // Inspector Fields
    // ---------------------------------------------------------

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] SFXClips;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    // ---------------------------------------------------------
    // Unity Lifecycle
    // ---------------------------------------------------------

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // ---------------------------------------------------------
    // SFX Playback
    // ---------------------------------------------------------

    public void PlaySFXClip(int index, float volume)
    {
        if (index < 0 || index >= SFXClips.Length) return;

        AudioClip clip = SFXClips[index];
        if (clip == null) return;

        SFXSource.PlayOneShot(clip, volume);
    }

    // ---------------------------------------------------------
    // Music Playback
    // ---------------------------------------------------------

    public void PlayMusicClip(int index, float volume)
    {
        if (index < 0 || index >= musicClips.Length) return;

        AudioClip clip = musicClips[index];
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.loop = true;
        musicSource.playOnAwake = false;

        musicSource.Play();
    }

    public void PauseMusicClip()
    {
        if (musicSource.isPlaying)
            musicSource.Pause();
    }

    public void UnPauseMusicClip()
    {
        if (!musicSource.isPlaying)
            musicSource.UnPause();
    }

    public void StopMusicClip()
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
    }
}
