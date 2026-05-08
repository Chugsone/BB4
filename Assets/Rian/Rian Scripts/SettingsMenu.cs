using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering;
public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        // resolution
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        volumeSlider.value = SaveDataController.Instance.current.Settings.volume;
        qualityDropdown.value = SaveDataController.Instance.current.Settings.quality;  
        fullscreenToggle.isOn = SaveDataController.Instance.current.Settings.isFullscreen;

        audioMixer.SetFloat("volume", SaveDataController.Instance.current.Settings.volume);
        QualitySettings.SetQualityLevel(SaveDataController.Instance.current.Settings.quality);
        Screen.fullScreen = SaveDataController.Instance.current.Settings.isFullscreen;
        Screen.SetResolution(SaveDataController.Instance.current.Settings.resolution.x, SaveDataController.Instance.current.Settings.resolution.y, Screen.fullScreen);
    }

    public void setvolume(float volume)
    {
        Debug.Log("Volume set to: " + volume);
        audioMixer.SetFloat("volume", volume);
        SaveDataController.Instance.current.Settings.volume = volume; 
        SaveDataController.Instance.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); //call this with save data on start
        SaveDataController.Instance.current.Settings.quality = qualityIndex;
        SaveDataController.Instance.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SaveDataController.Instance.current.Settings.isFullscreen = isFullscreen; 
        SaveDataController.Instance.Save();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        SaveDataController.Instance.current.Settings.resolution = new(resolution.width, resolution.height);
        SaveDataController.Instance.Save();
    }
}
