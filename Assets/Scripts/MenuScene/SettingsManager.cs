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
    const string gameMusicVol = "GameMusicVolume";

    const float defaultMenuVolume = 1.0f;
    const float defaultGameVolume = 1.0f;
    const float defaultSoundsVolume = 1.0f;
    const float defaultMasterVolume = 1.0f;

    [Header("UI")]
    [SerializeField] private Slider gameVolumeSlider;
    [SerializeField] private TextMeshProUGUI gameVolumeSliderText;

    [SerializeField] private Slider menuMusicVolumeSlider;
    [SerializeField] private TextMeshProUGUI menuMusicVolumeSliderText;

    [SerializeField] private Slider soundsVolumeSlider;
    [SerializeField] private TextMeshProUGUI soundsVolumeSliderText;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private TextMeshProUGUI masterVolumeSliderText;

    [Header("DISPLAY")]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private Toggle antiAliasToggle;

    [Header("AUDIO SOURCES")]
    public AudioSource onPointerEnterAudioSource;
    public AudioSource onPointerClickAudioSource;
    public AudioSource pressButtonSoundAudioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip onPointerEnterAudioClip;
    public AudioClip onPointerClickAudioClip;
    public AudioClip pressButtonSoundAudioClip;

    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        VSyncInitial();
        AntiAliasInitial();

        soundsVolumeSlider.value = defaultSoundsVolume;
        menuMusicVolumeSlider.value = defaultMenuVolume;
        masterVolumeSlider.value = defaultMasterVolume;
        gameVolumeSlider.value = defaultGameVolume;

        myAudioMixer.SetFloat(soundEffectsVol, Mathf.Log10(soundsVolumeSlider.value) * 20);
        myAudioMixer.SetFloat(menuMusicVol, Mathf.Log10(menuMusicVolumeSlider.value) * 20);
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolumeSlider.value) * 20);
        myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(gameVolumeSlider.value) * 20);

        LoadSettings();
        onPointerClickAudioSource.Stop();
    }

    private void AntiAliasInitial()
    {
        if (QualitySettings.antiAliasing == 0)
        {
            antiAliasToggle.isOn = false;
            Debug.Log($"Start game anti alias value is {QualitySettings.antiAliasing} and {antiAliasToggle.isOn}");
        }
        else
        {
            antiAliasToggle.isOn = true;
            Debug.Log($"Start game anti alias value is {QualitySettings.antiAliasing} and {antiAliasToggle.isOn}");
        }
    }

    private void VSyncInitial()
    {
        if (QualitySettings.vSyncCount == 0)
        {
            vSyncToggle.isOn = false;
            Debug.Log($"Start game vSyncToggle value is {QualitySettings.vSyncCount} and {vSyncToggle.isOn}");
        }
        else
        {
            vSyncToggle.isOn = true;
            Debug.Log($"Start game vSyncToggle value is {QualitySettings.vSyncCount} and {vSyncToggle.isOn}");
        }
    }

    public void SaveAntiAliasingAndVSyncOptions()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            Debug.Log($"vSyncCount value is {QualitySettings.vSyncCount} and vSyncToggle is {vSyncToggle.isOn}");
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Debug.Log($"vSyncCount value is {QualitySettings.vSyncCount} and vSyncToggle is {vSyncToggle.isOn}");
        }

        if (antiAliasToggle.isOn)
        {
            QualitySettings.antiAliasing = 2;
            Debug.Log($"Anti aliasing value is{QualitySettings.antiAliasing} and {antiAliasToggle.isOn}");
        }
        else
        {
            QualitySettings.antiAliasing = 0;
            Debug.Log($"Anti aliasing value is{QualitySettings.antiAliasing} and {antiAliasToggle.isOn}");
        }
    }

    public void SaveFullScreenMode()
    {
        if (fullscreenToggle.isOn)
        {
            int screenWidth = Screen.currentResolution.width;
            int screenHeight = Screen.currentResolution.height;
            Screen.fullScreen = fullscreenToggle.isOn;
            Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.ExclusiveFullScreen);
        }
        else
        {
            int screenWidth = 1920;
            int screenHeight = 1080;
            Screen.fullScreen = !fullscreenToggle.isOn;
            Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.Windowed);
        }
    }

    public void SaveSettings()
    {
        SaveFullScreenMode();
        SaveAntiAliasingAndVSyncOptions();

        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);

        float menuMusicVolumeValue = menuMusicVolumeSlider.value;
        float soundsVolumeValue = soundsVolumeSlider.value;
        float masterVolumeValue = masterVolumeSlider.value;
        float gameVolumeValue = gameVolumeSlider.value;

        PlayerPrefs.SetFloat("MenuVolume", menuMusicVolumeValue);
        PlayerPrefs.SetFloat("SoundsVolume", soundsVolumeValue);
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeValue);
        PlayerPrefs.SetFloat("GameVolume", gameVolumeValue);

        myAudioMixer.SetFloat(soundEffectsVol, Mathf.Log10(soundsVolumeValue) * 20);
        myAudioMixer.SetFloat(menuMusicVol, Mathf.Log10(menuMusicVolumeValue) * 20);
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolumeValue) * 20);
        myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(gameVolumeValue) * 20);
    }

    public void LoadSettings()
    {
        float menuMusicVolumeValue = PlayerPrefs.GetFloat("MenuVolume");
        float soundsVolumeValue = PlayerPrefs.GetFloat("SoundsVolume");
        float masterVolumeValue = PlayerPrefs.GetFloat("MasterVolume");
        float gameVolumeValue = PlayerPrefs.GetFloat("GameVolume");

        menuMusicVolumeSlider.value = menuMusicVolumeValue;
        soundsVolumeSlider.value = soundsVolumeValue;
        masterVolumeSlider.value = masterVolumeValue;
        gameVolumeSlider.value = gameVolumeValue;

        myAudioMixer.SetFloat(soundEffectsVol, Mathf.Log10(soundsVolumeSlider.value) * 20);
        myAudioMixer.SetFloat(menuMusicVol, Mathf.Log10(menuMusicVolumeSlider.value) * 20);
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolumeSlider.value) * 20);
        myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(gameVolumeSlider.value) * 20);
    }

    public void MenuMusicVolumeSlider()
    {
        float menuVolume = menuMusicVolumeSlider.value;
        menuMusicVolumeSliderText.text = menuVolume.ToString("0.0");
        myAudioMixer.SetFloat(menuMusicVol, Mathf.Log10(menuVolume) * 20);
    }

    public void GameVolumeSlider()
    {
        float gameVolume = gameVolumeSlider.value;
        gameVolumeSliderText.text = gameVolume.ToString("0.0");
        myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(gameVolume) * 20);
    }

    public void SoundsVolumeSlider()
    {
        float soundsVolume = soundsVolumeSlider.value;
        soundsVolumeSliderText.text = soundsVolume.ToString("0.0");
        myAudioMixer.SetFloat(soundEffectsVol, Mathf.Log10(soundsVolume) * 20);
    }

    public void MasterVolumeSlider()
    {
        float masterVolume = masterVolumeSlider.value;
        masterVolumeSliderText.text = masterVolume.ToString("0.0");
        myAudioMixer.SetFloat(masterVol, Mathf.Log10(masterVolume) * 20);
    }

    public void ResetSettings()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);

        if (menuMusicVolumeSlider != null)
        {
            menuMusicVolumeSlider.value = 1.0f;
            myAudioMixer.SetFloat(menuMusicVol, Mathf.Log10(menuMusicVolumeSlider.value) * 20);
            PlayerPrefs.SetFloat("MenuVolume", menuMusicVolumeSlider.value);
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
        if (gameVolumeSlider != null)
        {
            gameVolumeSlider.value = 1.0f;
            myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(gameVolumeSlider.value) * 20);
            PlayerPrefs.SetFloat("GameVolume", gameVolumeSlider.value);
        }
        Debug.Log("Reset settings to default!");
    }


    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }

    public void OnPointerClick()
    {
        AudioManager.Instance.PlaySound(onPointerClickAudioSource, onPointerClickAudioClip);
    }
}

