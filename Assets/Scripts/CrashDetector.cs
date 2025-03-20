using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private float delayBeforeGameOver = 1f;
    [SerializeField] ParticleSystem DeadEffect;
    bool hasCrash = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && !hasCrash)
        {
            hasCrash = true;
            var player = FindAnyObjectByType<PlayerController>();
            player.OnDisable();
            
            DeadEffect.Play();
            GetComponent<AudioSource>().Play();
            
            // Delay the game over screen and pause
            StartCoroutine(ShowGameOverAfterDelay(player.GetScore()));
        }
    }

    private IEnumerator ShowGameOverAfterDelay(float score)
    {
        yield return new WaitForSeconds(delayBeforeGameOver);
        Time.timeScale = 0f;
        
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver(score);
        }
    }
}
