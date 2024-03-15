using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestTimeText;


    [Header("GAMEOBJECTS")]
    public GameObject mainMenu;
    public GameObject optionsMenu;


    [Header("GAMEPLAY")]
    private readonly float quitButtonDelay = 0.2f;
    private readonly float settingsButtonDelay = 0.1f;
    private readonly float playButtonDelay = 0.1f;


    [Header("AUDIO SOURCES")]
    public AudioSource onPointerEnterAudioSource;
    public AudioSource pressButtonAudioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip onPointerEnterAudioClip;
    public AudioClip pressButtonAudioClip;


    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    private void OnEnable()
    {
        LoadBestScore();
        LoadBestTime();
    }

    private void LoadBestScore()
    {
        int savedBestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + savedBestScore;
    }

    private void LoadBestTime()
    {
        float savedBestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        int minutes = Mathf.FloorToInt(savedBestTime / 60);
        int seconds = Mathf.FloorToInt(savedBestTime % 60);
        bestTimeText.text = "Best Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        StartCoroutine(QuitAfterDelay());
    }


    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(quitButtonDelay);

        PlayerPrefs.SetFloat("MasterVolume", 1.0f);
        PlayerPrefs.SetFloat("SoundsVolume", 1.0f);
        PlayerPrefs.SetFloat("MenuMusicVolume", 1.0f);
        PlayerPrefs.SetInt("QualityDropdownValue", 0);

        Application.Quit();
        Debug.Log("Game quits and editor play mode stops and our values reset to default!");
    }

    public void LoadSettingsScene()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        StartCoroutine(LoadSettingsAfterDelay());
    }

    IEnumerator LoadSettingsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(settingsButtonDelay);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void LoadMainGameScene()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        StartCoroutine(LoadMainGameAfterDelay());
    }

    IEnumerator LoadMainGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(playButtonDelay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }
}
