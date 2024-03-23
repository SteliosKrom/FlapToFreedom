using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGoBackTransitions : MonoBehaviour
{
    [Header("GAME OBJECTS")]
    [SerializeField] private List<GameObject> treeLogsList;
    [SerializeField] private List<GameObject> gemParticleList;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject backgroundLayers;
    [SerializeField] private GameObject groundLayers;

    [Header("GAMEPLAY")]
    public float goBackDelay = 0.1f;

    [Header("AUDIO SOURCES")]
    [SerializeField] private AudioSource pressButtonSoundAudioSource;


    [Header("AUDIO CLIPS")]
    [SerializeField] private AudioClip pressButtonSoundAudioClip;

    public void GoBackToMenuButton()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(GoBackToMainMenuSceneAfterDelay());
    }

    IEnumerator GoBackToMainMenuSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(goBackDelay);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    } 
}
