using System;
using UnityEngine;
using UnityEngine.Rendering;



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
        AudioClip clip = null;

        for (int i = 0; i < musicClips.Length; i++)
        {
            if (i == index)
            {
                if (musicClips[i] == null) return;

                clip = musicClips[i];
                break;
            }
        }

        musicSource.PlayOneShot(clip,volume);
    }
    public void StopMusicClip()
    {
        
    }



}


