using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsUIManager : MonoBehaviour
{
    public AudioMixer myAudioMixer;
    const string gameMusicVol = "GameMusicVolume";
    private float goBackDelay = 0.1f;

    [Header("UI")]
    [SerializeField] private Slider gameVolumeSlider;
    [SerializeField] private TextMeshProUGUI gameVolumeSliderText;

    [Header("GAMEOBJECTS")]
    public GameObject mainMenu;
    public GameObject optionsMenu;

    [Header("AUDIO SOURCES")]
    public AudioSource onPointerEnterAudioSource;
    public AudioSource pressButtonSoundAudioSource;
    public AudioSource onPointerClickAudioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip onPointerEnterAudioClip;
    public AudioClip pressButtonSoundAudioClip;
    public AudioClip onPointerClickAudioClip;

    

    private void Start()
    {
        gameVolumeSlider.value = 1.0f;
    }

    public void GameVolumeSlider()
    {
        float gameVolume = gameVolumeSlider.value;
        gameVolumeSliderText.text = gameVolume.ToString("0.0");
        myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(gameVolumeSlider.value) * 20);
    }

    public void GoBackButton()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(GoBackToMainMenuSceneAfterDelay());
    }

    IEnumerator GoBackToMainMenuSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(goBackDelay);
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
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
