using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
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
    public TMP_Text highscoreText;    // Text to display highscore on game over
    public TMP_Text topScoresText;    // Text to display top 5 scores
    public TMP_Text countdownText;    // Large text for 3, 2, 1, GO!

    [Header("Score Settings")]
    public float scoreMultiplier = 10f; // How fast the score increases

    private float currentScore = 0f;
    private bool isGameOver = false;
    public bool IsCountingDown => isCountingDown;
    private bool isCountingDown = true;
    private const string TopScoresKey = "TopScores";

    void Start()
    {
        // Reset time scale in case it was left at 0 or something else
        Time.timeScale = 1f;

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);

        if (quitText != null)
            quitText.gameObject.SetActive(false);
            
        if (restartText != null)
            restartText.gameObject.SetActive(false);
            
        if (highscoreText != null)
            highscoreText.gameObject.SetActive(false);

        if (topScoresText != null)
            topScoresText.gameObject.SetActive(false);

        UpdateScoreText();

        // Start the countdown
        if (countdownText != null)
        {
            StartCoroutine(StartCountdown());
        }
        else
        {
            isCountingDown = false;
        }
    }

    IEnumerator StartCountdown()
    {
        isCountingDown = true;
        Time.timeScale = 0f; // Pause movement and gameplay
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(0.5f);

        countdownText.gameObject.SetActive(false);
        isCountingDown = false;
        Time.timeScale = 1f; // Resume gameplay
    }

    void Update()
    {
        if (!isGameOver && !isCountingDown)
        {
            // Increase score based on time
            currentScore += Time.deltaTime * scoreMultiplier;
            UpdateScoreText();
        }
        else if (isGameOver)
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
        int finalScore = Mathf.FloorToInt(currentScore);

        HandleTopScores(finalScore);

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        if (quitText != null)
            quitText.gameObject.SetActive(true);
            
        if (restartText != null)
            restartText.gameObject.SetActive(true);
    }

    void HandleTopScores(int newScore)
    {
        // 1. Load existing scores
        string scoresString = PlayerPrefs.GetString(TopScoresKey, "");
        List<int> highScores = new List<int>();

        if (!string.IsNullOrEmpty(scoresString))
        {
            highScores = scoresString.Split(',').Select(int.Parse).ToList();
        }

        // 2. Add new score, sort and keep top 5
        highScores.Add(newScore);
        highScores = highScores.OrderByDescending(s => s).Take(5).ToList();

        // 3. Save back to PlayerPrefs
        string newScoresString = string.Join(",", highScores);
        PlayerPrefs.SetString(TopScoresKey, newScoresString);
        PlayerPrefs.Save();

        // 4. Update UI
        if (highscoreText != null)
        {
            highscoreText.text = "Current Best: " + highScores[0].ToString();
            highscoreText.gameObject.SetActive(true);
        }

        if (topScoresText != null)
        {
            string display = "TOP 5 SCORES\n";
            for (int i = 0; i < highScores.Count; i++)
            {
                display += (i + 1) + ". " + highScores[i] + "\n";
            }
            topScoresText.text = display;
            topScoresText.gameObject.SetActive(true);
        }
    }

    void RestartGame()
    {
        // Ensure time is moving before loading new scene
        Time.timeScale = 1f;
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