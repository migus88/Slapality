using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _mainMusic;
    

    //TODO: Use injection instead
    public static SoundManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            if (_mainMusic != null)
            {
                Instance.StopMusic();
                Instance.PlayMusic(_mainMusic);
            }

            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeSfxVolume(float value)
    {
        value = Mathf.Clamp(value, 0, 1f);
        _sfxSource.volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        value = Mathf.Clamp(value, 0, 1f);
        _musicSource.volume = value;
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
    
    public void PlaySfx(AudioClip clip)
    {
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }
    
    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public async UniTaskVoid PlaySfxDelayed(AudioClip clip, float delay)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay));
        PlaySfx(clip);
    }
    
}