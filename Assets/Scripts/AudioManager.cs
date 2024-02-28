using UnityEngine;


public class AudioManager : MonoBehaviour
{
    //Variables initialization
    public AudioSource clickButtonAudioSource;
    public AudioSource clickToggleAudioSource;
    public AudioSource jumpSoundAudioSource;
    public AudioSource mainGameMusicAudioSource;
    public AudioSource gameOverSoundAudioSource;
    public AudioSource onPointerEnterAudioSource;

    public AudioClip toggleAudioClip;
    public AudioClip clickButtonSoundClip;
    public AudioClip jumpSoundClip;
    public AudioClip gameOverSoundClip;
    public AudioClip onPointerEnterAudioClip;


    public void PressButtonSound()
    {
        if (clickButtonSoundClip != null)
        {
            clickButtonAudioSource.PlayOneShot(clickButtonSoundClip);
          //  Debug.Log("Press button sound is active!");
        }
    }

    public void PressOptionsSound()
    {
        if (toggleAudioClip != null)
        {
            clickToggleAudioSource.PlayOneShot(toggleAudioClip);
        //    Debug.Log("Press toggle button sound is active!");
        }
    }

    public void PlayerJumpSound()
    {
        jumpSoundAudioSource.PlayOneShot(jumpSoundClip);
     //   Debug.Log("Jump sound clip works!");
    }

    public void GameOverSound()
    {
        if (gameOverSoundClip != null)
        {
            gameOverSoundAudioSource.PlayOneShot(gameOverSoundClip);
         //   Debug.Log("Game over sound is enabled!");
        }
    }
}

