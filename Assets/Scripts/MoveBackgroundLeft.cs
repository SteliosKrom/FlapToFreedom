using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundLeft : MonoBehaviour
{
    public float speed = 3f;

    private PlayerController playerController;
    private UIManager uiManager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (playerController != null)
        {
            playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        }

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.currentState == GameManager.GameState.Playing)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }     
    }
}
