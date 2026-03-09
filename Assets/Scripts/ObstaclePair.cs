using UnityEngine;

/// <summary>
/// Moves the obstacle pair leftward and destroys it when it's off-screen.
/// </summary>
public class ObstaclePair : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float scrollSpeed = 4f;
    [SerializeField] private float destroyAtX = -10f;

    private void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x < destroyAtX)
        {
            Destroy(gameObject);
        }
    }
}
