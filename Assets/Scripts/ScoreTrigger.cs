using UnityEngine;

/// <summary>
/// Invisible trigger placed between pipe gaps. Awards a point when the player passes through.
/// </summary>
public class ScoreTrigger : MonoBehaviour
{
    private bool scored;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (scored) return;
        if (other.CompareTag("Player"))
        {
            scored = true;
            GameManager.Instance.AddScore();
        }
    }
}
