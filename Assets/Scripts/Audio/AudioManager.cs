using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("MANAGERS")]
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        } 
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
         source.PlayOneShot(clip);  
    }
}

