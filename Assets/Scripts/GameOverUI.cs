using UnityEngine;
using TMPro; // Add TextMeshPro namespace
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text scoreText;     
    [SerializeField] private TMP_Text highScoreText; 
    private const string HIGH_SCORE_KEY = "HighScore";

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver(float currentScore)
    {
        gameOverPanel.SetActive(true);
        
        // Update high score
        float highScore = PlayerPrefs.GetFloat(HIGH_SCORE_KEY, 0);
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat(HIGH_SCORE_KEY, highScore);
            PlayerPrefs.Save();
        }

        // Display scores
        scoreText.text = $"Score: {Mathf.Round(currentScore)}";
        highScoreText.text = $"High Score: {Mathf.Round(highScore)}";
    }

    public void OnRetryButton()
    {
        // Resume normal time scale before loading new scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}