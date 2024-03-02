using UnityEngine;

public class TreeLogsController : MonoBehaviour
{
    [Header("GAMEPLAY")]
    [SerializeField] private GameObject treeLogs;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(""))
        {
            treeLogs.gameObject.SetActive(false);
        }
    }
}
