using UnityEngine;

public class MovingUpDownLogs : MonoBehaviour
{
    [Header("TREE LOGS")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private bool movingUp = true;

    private void Update()
    {
        if (movingUp)
        {
            transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
            if (transform.position.y >= maxY)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);
            if (transform.position.y <= minY)
            {
                movingUp = true;
            }
        }
    }
}
