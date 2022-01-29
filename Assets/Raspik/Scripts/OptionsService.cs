using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsService : MonoBehaviour
{
    public void LoadOptions()
    {
        float sfxVol = PlayerPrefs.GetFloat("sfxVol", 1f);
        float musicVol = PlayerPrefs.GetFloat("musicVol", 1f);

        Slider[] sliders = gameObject.GetComponentsInChildren<Slider>();
        foreach (var slider in sliders)
        {
            switch (slider.name)
            {
                case "SFXSliderObj":
                    slider.value = sfxVol;
                    break;
                case "MusSliderObj":
                    slider.value = musicVol;
                    break;
                default: break;
            }
        }
    }

    public void SaveOptions()
    {
        Slider[] sliders = gameObject.GetComponentsInChildren<Slider>();
        foreach (var slider in sliders)
        {
            switch (slider.name)
            {
                case "SFXSliderObj":
                    PlayerPrefs.SetFloat("sfxVol",slider.value);
                    break;
                case "MusSliderObj":
                    PlayerPrefs.SetFloat("musicVol",slider.value);
                    break;
                default: break;
            }
        }

        EventManager.OptionsChanged();
    }
}