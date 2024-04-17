using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1, 1)]
    private float scrollSpeed = 0.5f;
    private float distance;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (RoundManager.Instance.currentState == GameState.Playing)
        {
            distance = distance + (Time.deltaTime * scrollSpeed) / 5f;
            mat.SetTextureOffset("_MainTex", new Vector2(distance, 0));
        }
    }
}
