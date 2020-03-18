using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resDropdown;
    public Dropdown qualDropdown;
    public Slider sensSlider;
    public Slider volSlider;
    public Toggle toggle;
    public int ResReset;
    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && 
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResReset = currentResolutionIndex;
        resDropdown.AddOptions(options);
        resDropdown.value = currentResolutionIndex;
        resDropdown.RefreshShownValue();
    }

    public void OnVolumeChange(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ChangeSensetivity(float value)
    {
        SharedSavedStuff.Sensetivity = value;
    }

    public void Reset()
    {
        SetFullscreen(true);
        qualDropdown.SetValueWithoutNotify(3);
        resDropdown.SetValueWithoutNotify(ResReset);
        OnVolumeChange(0);
        ChangeSensetivity(100);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, Screen.fullScreen);
        SetQuality(3);
        qualDropdown.RefreshShownValue();
        resDropdown.RefreshShownValue();
        volSlider.value = 0;
        sensSlider.value = 100;
        toggle.isOn = true;
    }
}