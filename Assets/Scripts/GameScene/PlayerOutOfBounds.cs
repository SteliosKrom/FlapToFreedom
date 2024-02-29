using UnityEngine;

//REMOVE THIS CLASS AND HANDLE IT VIA PLAYER CONTROLLER CLASS
[RequireComponent(typeof(PlayerController))]
public class PlayerOutOfBounds : BoundsBase 
{
    // Managers variables!
    private AudioManager audioManager;

    private void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        DestroyPlayerOutOfBoundaries();
    }
    public void DestroyPlayerOutOfBoundaries()
    {
        if (transform.position.y <= LowerYRange)
        {
            PlayerController.player.SetActive(false);
            RoundManager.Instance.GameOver();
            //  audioManager.GameOverSound();
        }

        else if (transform.position.y >= UpperYRange)
        {
            PlayerController.player.SetActive(false);
            RoundManager.Instance.GameOver();
           // audioManager.GameOverSound();
        }
    }
}
