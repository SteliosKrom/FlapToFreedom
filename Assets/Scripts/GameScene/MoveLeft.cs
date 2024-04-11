using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float currentSpeed;

    void Update()
    {
        if (RoundManager.Instance.currentState == GameState.Playing)
        {
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        }
    }
}
