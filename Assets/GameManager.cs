using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverCanvas; // Full Canvas
    public TMP_Text quitText;         // "Press Escape to Quit"
    public TMP_Text restartText;      // "Press Space to Restart"
    public TMP_Text scoreText;        // Text to display current score

    [Header("Score Settings")]
    public float scoreMultiplier = 10f; // How fast the score increases

    private float currentScore = 0f;
    private bool isGameOver = false;

    void Start()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);

        if (quitText != null)
            quitText.gameObject.SetActive(false);
            
        if (restartText != null)
            restartText.gameObject.SetActive(false);

        UpdateScoreText();
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Increase score based on time
            currentScore += Time.deltaTime * scoreMultiplier;
            UpdateScoreText();
        }
        else 
        {
            // Check for Restart
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
            
            // Check for Quit
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            // Display score as a whole number
            scoreText.text = "Score: " + Mathf.FloorToInt(currentScore).ToString();
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        if (quitText != null)
            quitText.gameObject.SetActive(true);
            
        if (restartText != null)
            restartText.gameObject.SetActive(true);
    }

    void RestartGame()
    {
        // Reload the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}