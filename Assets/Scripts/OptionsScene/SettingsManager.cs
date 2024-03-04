using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer myAudioMixer;
    const string menuMusicVol = "MenuMusicVolume";
    const string soundEffectsVol = "SoundsVolume";
    const string masterVol = "MasterVolume";

    [Header("UI")]
    [SerializeField] private Slider menuMusicVolumeSlider;
    [SerializeField] private TextMeshProUGUI menuMusicVolumeSliderText;

    [SerializeField] private Slider soundsVolumeSlider;
    [SerializeField] private TextMeshProUGUI soundsVolumeSliderText;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private TextMeshProUGUI masterVolumeSliderText;

    [Header("DISPLAY")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    Resolution[] resolutions;

    [Header("AUDIO SOURCES")]
    public AudioSource pressButtonAudioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip pressButtonAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        ResolutionSettings();
        CheckQualityDropdownNullReference();
        LoadSettings();
    }

    public void CheckQualityDropdownNullReference()
    {
        if (qualityDropdown != null)
        {
            qualityDropdown.value = 0;
            QualitySettings.SetQualityLevel(qualityDropdown.value);
            Debug.Log("Quality settings are: " + qualityDropdown.value);
        }
    }

    public void SaveSettings()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        //Here we save all of our settings!
        float menuMusicVolumeValue = menuMusicVolumeSlider.value;
        float soundsVolumeValue = soundsVolumeSlider.value;
        float masterVolumeValue = masterVolumeSlider.value;
        int qualityDropdownValue = qualityDropdown.value;

        PlayerPrefs.SetFloat("MenuMusicVolume", menuMusicVolumeValue);
        PlayerPrefs.SetFloat("SoundsVolume", soundsVolumeValue);
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeValue);
        PlayerPrefs.SetInt("QualityDropdown", qualityDropdownValue);

        myAudioMixer.SetFloat(soundEffectsVol, Mathf.Log10(soundsVolumeValue) * 20);
        myAudioMixer.SetFloat(menuMusicVol, Mathf.Log10(menuMusicVolumeValue) * 20);
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolumeValue) * 20);
    }

    public void LoadSettings()
    {
        //Here we load all of our saved settings!
        float menuMusicVolumeValue = PlayerPrefs.GetFloat("MenuMusicVolume");
        float soundsVolumeValue = PlayerPrefs.GetFloat("SoundsVolume");
        float masterVolumeValue = PlayerPrefs.GetFloat("MasterVolume");
        int qualityDropdownValue = PlayerPrefs.GetInt("QualityDropdown");
        if (menuMusicVolumeSlider != null)
        {
            menuMusicVolumeSlider.value = menuMusicVolumeValue;
        }
        if (soundsVolumeSlider != null)
        {
            soundsVolumeSlider.value = soundsVolumeValue;
        }
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = masterVolumeValue;
        }
        if (qualityDropdown != null)
        {
            qualityDropdown.value = qualityDropdownValue;
        }
    }

    public void MenuMusicVolumeSlider()
    {
        float menuVolume = menuMusicVolumeSlider.value;
        menuMusicVolumeSliderText.text = menuVolume.ToString("0.0");
    }

    public void SoundsVolumeSlider()
    {
        float soundsVolume = soundsVolumeSlider.value;
        soundsVolumeSliderText.text = soundsVolume.ToString("0.0");
    }

    public void MasterVolumeSlider()
    {
        float masterVolume = masterVolumeSlider.value;
        masterVolumeSliderText.text = masterVolume.ToString("0.0");
    }

    public void ResetSettings()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        if (menuMusicVolumeSlider != null)
        {
            menuMusicVolumeSlider.value = 1.0f;
            myAudioMixer.SetFloat(menuMusicVol, Mathf.Log10(menuMusicVolumeSlider.value) * 20);
            PlayerPrefs.SetFloat("MenuMusicVolume", menuMusicVolumeSlider.value);
        }
        if (soundsVolumeSlider != null)
        {
            soundsVolumeSlider.value = 1.0f;
            myAudioMixer.SetFloat(soundEffectsVol, Mathf.Log10(soundsVolumeSlider.value) * 20);
            PlayerPrefs.SetFloat("SoundsVolume", soundsVolumeSlider.value);
        }
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = 1.0f;
            myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolumeSlider.value) * 20);
            PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        }
        if (qualityDropdown != null)
        {
            qualityDropdown.value = 0;
            QualitySettings.SetQualityLevel(qualityDropdown.value);
            PlayerPrefs.SetInt("QualityDropdown", qualityDropdown.value);
            Debug.Log("Quality settings are: " + qualityDropdown.value);
        }

        Debug.Log("Reset settings to default!");
    }

    public void GraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("Current quality level: " + QualitySettings.GetQualityLevel());
    }

    public void FullScreenMode(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("Fullscreen mode is: " + isFullScreen);
    }

    public void ResolutionSettings()
    {
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions;

        if (resolutionDropdown != null)
        {
            resolutionDropdown.ClearOptions();
        }
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        if (resolutionDropdown != null)
        {
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }      
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}

