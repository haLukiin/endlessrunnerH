using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages game state: playing and dead. Auto-starts on Play, restarts on Space after death.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private ObstacleSpawner obstacleSpawner;

    private int score;
    private bool isDead;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        obstacleSpawner.StartSpawning();
        Debug.Log("Game started — obstacles spawning.");
    }

    private void Update()
    {
        if (isDead && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    /// <summary>
    /// Called by the player when it dies.
    /// </summary>
    public void OnPlayerDied()
    {
        if (isDead) return;
        isDead = true;
        obstacleSpawner.StopSpawning();
        foreach (ObstaclePair pair in FindObjectsByType<ObstaclePair>(FindObjectsSortMode.None))
            pair.enabled = false;
        Debug.Log("Game Over — Score: " + score + " — Press Space to restart.");
    }

    /// <summary>
    /// Called by score triggers to increment the score.
    /// </summary>
    public void AddScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }
}
