using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsService : MonoBehaviour
{
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    private void Awake()
    {
        InitSliders();
        
        _sfxVolumeSlider.onValueChanged.AddListener(OnSfxSliderValueChanged);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
    }

    private void InitSliders()
    {
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVol", 1f);
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVol", 1f);
        
        SoundManager.Instance.ChangeSfxVolume(_sfxVolumeSlider.value);
        SoundManager.Instance.ChangeMusicVolume(_musicVolumeSlider.value);
    }

    private void OnSfxSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat("sfxVol", _sfxVolumeSlider.value);
        SoundManager.Instance.ChangeSfxVolume(_sfxVolumeSlider.value);
    }

    private void OnMusicSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat("musicVol", _musicVolumeSlider.value);
        SoundManager.Instance.ChangeMusicVolume(_musicVolumeSlider.value);
    }
}