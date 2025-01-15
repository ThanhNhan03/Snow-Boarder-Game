using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float resetTime = 0.5f;
    [SerializeField]
    ParticleSystem DeadEffect;
    bool hasCrash = false; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && !hasCrash)
        {
            hasCrash = true;
            FindAnyObjectByType<PlayerController>().OnDisable();
            DeadEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("Reset", resetTime);
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
