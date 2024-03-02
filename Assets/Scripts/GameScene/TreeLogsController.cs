using UnityEngine;

public class TreeLogsController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Logs"))
        {
            other.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
