using UnityEngine;

public class DestroyOutOfBounds : BoundsBase
{
    // Object variables!
    public GameObject treeLogs;

    public void SetPlayerReference(PlayerController controller)
    {
        PlayerController = controller;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyLogsOutOfBoundaries();
    }

    public void DestroyLogsOutOfBoundaries()
    {
        if (gameObject.CompareTag("TreeLogs") && transform.position.x <= -XBounds)
        {
            PlayerController.CollectObject(gameObject);
            Debug.Log("Logs are destroyed!");
        }
    }
}
