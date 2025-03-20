using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameOverUI gameOverUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rb;
    [Header("Movement Settings")]
    [SerializeField] private float torqueAmout = 1f;
    [SerializeField] private float boostAmount = 30f;
    [SerializeField] private float baseSpeed = 20f;

    [Header("Distance Score Settings")]
    [SerializeField] private float distanceTraveled = 0f;
    [SerializeField] private float score = 0f;
    [SerializeField] private float scoreMultiplier = 0.1f;

    [Header("Trick Score Settings")]
    [SerializeField] private float rotationThreshold = 360f;
    [SerializeField] private float trickScore = 100f;

    [Header("Air Time Score Settings")]
    [SerializeField] private float airTimeScore = 10f;
    [SerializeField] private float airTimeThreshold = 1f;

    [Header("Game State")]
    [SerializeField] private bool isDead = false;
    [SerializeField] private float restartDelay = 1f;

    // Private variables that don't need to show in inspector
    private Vector2 lastPosition;
    private float currentRotation = 0f;
    private float lastRotation = 0f;
    private bool isInAir = false;
    private float airTime = 0f;
    private bool hasAwardedAirTime = false;
    private bool canMove = true;

    SurfaceEffector2D surfaceEffector2D;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (canMove && !isDead)
        {
            RotatePlayer();
            RespondBoost();
            UpdateTrickScore();
            UpdateAirTimeScore();
        }
    }

    void UpdateAirTimeScore()
    {
        if (isInAir)
        {
            airTime += Time.deltaTime;

            // Award points for every second in air after threshold
            if (airTime >= airTimeThreshold && !hasAwardedAirTime)
            {
                score += airTimeScore;
                hasAwardedAirTime = true;
                airTimeThreshold += 1f; // Increase threshold for next award
            }
        }
    }

    void UpdateTrickScore()
    {
        currentRotation = transform.rotation.eulerAngles.z;

        float rotationDelta = Mathf.Abs(currentRotation - lastRotation);

        if (rotationDelta > 180f)
        {
            rotationDelta = 360f - rotationDelta;
        }

        if (isInAir)
        {
            distanceTraveled += rotationDelta;

            if (distanceTraveled >= rotationThreshold)
            {
                score += trickScore;
                distanceTraveled = 0f;
            }
        }

        lastRotation = currentRotation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isInAir = false;
            distanceTraveled = 0f;
            // Reset air time variables
            airTime = 0f;
            airTimeThreshold = 1f;
            hasAwardedAirTime = false;
        }

        // Check for collision with bird
        if (collision.gameObject.CompareTag("Bird") || collision.gameObject.CompareTag("Catus"))
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Alternative check for trigger colliders
        if (collision.CompareTag("Bird") || collision.gameObject.CompareTag("Catus"))
        {
            Die();
        }
    }

    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            canMove = false;
            
            // Delay game over screen
            StartCoroutine(ShowGameOverAfterDelay());
        }
    }

    private IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        
        // Pause the game
        Time.timeScale = 0f;
        
        // Show game over UI
        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOver(score);
        }
    }

    // void RestartLevel()
    // {
    //     // Reload the current scene
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isInAir = true;
        }
    }

    // Add method to get current score
    public float GetScore()
    {
        return score;
    }

    public void OnDisable()
    {
        canMove = false;
    }

    void RespondBoost()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            surfaceEffector2D.speed = boostAmount;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(torqueAmout);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-torqueAmout);
        }
    }
}