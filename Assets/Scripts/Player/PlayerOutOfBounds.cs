using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerOutOfBounds : BoundsBase
{
    // Managers variables!
    private AudioManager audioManager;
    private GameManager gameManager;

    private void Start()
    {
        PlayerController = GetComponent<PlayerController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        DestroyPlayerOutOfBoundaries();
    }
    public void DestroyPlayerOutOfBoundaries()
    {
        if (transform.position.y <= LowerYRange)
        {
            Debug.Log("Destroying object due to lower bound");
            PlayerController.player.SetActive(false);
            gameManager.GameOver();
            audioManager.GameOverSound();
            Debug.Log("Player game object is destroyed!");
        }

        else if (transform.position.y >= UpperYRange)
        {
            Debug.Log("Destroying object due to upper bound");
            PlayerController.player.SetActive(false);
            gameManager.GameOver();
            audioManager.GameOverSound();
            Debug.Log("Player game object is destroyed!");
        }
    }
}
