using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SFXSource;
    public AudioSource MusicSource;

    public static SoundManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            EventManager.onOptionsChange += ChangeVolume;
            ChangeVolume();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeVolume()
    {
        SFXSource.volume = PlayerPrefs.GetFloat("sfxVol", 1f);
        MusicSource.volume = PlayerPrefs.GetFloat("musicVol", 1f);
        Debug.Log($"Volume Changed: SFX - {SFXSource.volume.ToString()}, Music - {MusicSource.volume.ToString()}");
    }
    
    public void Play(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
    }
    
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }
    
}