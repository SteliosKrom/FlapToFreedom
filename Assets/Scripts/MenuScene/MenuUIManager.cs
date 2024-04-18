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
    public GameObject creditsMenu;


    [Header("GAMEPLAY")]
    private readonly float quitButtonDelay = 0.5f;
    private readonly float settingsButtonDelay = 0.1f;
    private readonly float playButtonDelay = 0.1f;
    private readonly float creditsButtonDelay = 0.1f;


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
        creditsMenu.SetActive(false);
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
        Debug.Log("Best score is: " + bestScoreText.text);
    }

    private void LoadBestTime()
    {
        float savedBestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        int minutes = Mathf.FloorToInt(savedBestTime / 60);
        int seconds = Mathf.FloorToInt(savedBestTime % 60);
        bestTimeText.text = "Best Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        Debug.Log("Best time is: " + bestTimeText.text);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        StartCoroutine(QuitAfterDelay());
    }


    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(quitButtonDelay);
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
        Debug.Log("Settings are loaded after a delay");
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

    public void LoadCredits()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        StartCoroutine(LoadCreditsAfterDelay());
    }

    IEnumerator LoadCreditsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(creditsButtonDelay);
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }
}
