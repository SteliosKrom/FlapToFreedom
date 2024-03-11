using UnityEngine;

public class MoveBackgroundLeft : MonoBehaviour
{
    public float speed = 3f;

    // Update is called once per frame
    void Update()
    {
        if(RoundManager.Instance.currentState == GameState.Playing || RoundManager.Instance.currentState == GameState.Intro)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }       
    }
}
