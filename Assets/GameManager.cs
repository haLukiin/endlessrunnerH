using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverCanvas; // Full Canvas
    public TMP_Text quitText;         // "Press Escape to Quit"

    private bool isGameOver = false;

    void Start()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(false);

        if (quitText != null)
            quitText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);

        if (quitText != null)
            quitText.gameObject.SetActive(true);
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