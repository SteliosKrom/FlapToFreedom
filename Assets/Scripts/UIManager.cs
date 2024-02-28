using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//SPLIT MY UI ELEMENTS TO DIFFERENT CLASSES! (MAIN MENU UI ELEMENTS ON ANOTHER CLASS) & (MAIN GAME UI ELEMENTS ON ANOTHER CLASS)
public class UIManager : MonoBehaviour
{
    // Delay variables!
    private float loadDelay = 0.1f;
    private float goBackDelay = 0.1f;


    // Managers variables!
    public SettingsManager settingsManager;
    public AudioManager audioManager;
    public PlayerController playerController;
    public GameManager gameManager;


    // Button variables!
    public Button quitButton;
    public Button exitButton;
    public Button playButton;
    public Button optionsButton;
    public Button pauseButton;
    public Button continueButton;
    public Button homeMenuButton;
    public Button applyButton;
    public Button resetButton;
    public Button loadButton;

    // Start is called before the first frame update
    public void StartGame()
    {
        settingsManager = GameObject.Find("SettingsManager").GetComponent<SettingsManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();  
    }


    void Update()
    {
        InputForPauseMenuScreen();
    }

    public void QuitGame()
    {
        StartCoroutine(QuitAfterDelay());
    }


    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(loadDelay);

        PlayerPrefs.SetFloat("MasterVolume", 1.0f);
        PlayerPrefs.SetFloat("SoundsVolume", 1.0f);
        PlayerPrefs.SetFloat("MenuMusicVolume", 1.0f);

        PlayerPrefs.SetInt("QualityDropdownValue", 0);

        Application.Quit();
        Debug.Log("Game quits and editor play mode stops and our values reset to default!");
    }


    public void LoadOptionsScene()
    {
        StartCoroutine(LoadOptionsSceneAfterDelay());
        Debug.Log("Options scene is being loaded!");
    }



    IEnumerator LoadOptionsSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        SceneManager.LoadScene("OptionsScene");
        Debug.Log("Options Button is pressed after a delay!");
    }


    public void LoadMainGameScene()
    {
        StartCoroutine(LoadMainGameAfterDelay());
        Destroy(GameObject.FindWithTag("MainMenuBackgroundMusic"));
    }


    IEnumerator LoadMainGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        Debug.Log("Play Button is pressed after a delay!");
    }


    public void GoBackButton()
    {
        StartCoroutine(GoBackToMainMenuSceneAfterDelay());
    }

    IEnumerator GoBackToMainMenuSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(goBackDelay);
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("Left back button is pressed after a delay and back button is pressed!");
    }


    public void ContinueGameButton()
    {
        StartCoroutine(ContinueGameAfterDelay());
    }


    IEnumerator ContinueGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(loadDelay);       
       // gameManager.pauseMenuScreen.SetActive(false);
       // audioManager.mainGameMusicAudioSource.UnPause();
        Debug.Log("Continue button is pressed and pause menu screen gone!");
    }

    public void InputForPauseMenuScreen()
    {
        // Check the current game state!
        if (gameManager != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (true)
                {
                    gameManager.PauseGame();
                }

                else
                {
                    gameManager.ResumeGame();
                }      
            }
        } 
    }

    public void HomeBlackButton()
    {
        StartCoroutine(HomeBlackButtonAfterDelay());
    }

    IEnumerator HomeBlackButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("Main menu scene is loaded and home black button is pressed!");
    }

    public void RestartButton()
    {
        StartCoroutine(RestartButtonAfterDelay());
    }

    IEnumerator RestartButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        Debug.Log("Restart button pressed and loaded main game scene and game over menu scree gone!");
    }

    public void OnPointerEnter()
    {
       // audioManager.onPointerEnterAudioSource.PlayOneShot(audioManager.onPointerEnterAudioClip);
    }
}
