using UnityEngine;

public class InactivatePowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("PowerUpIncreaseScoreBy2"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
