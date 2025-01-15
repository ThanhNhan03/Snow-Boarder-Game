using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float resetTime = 1f;
    [SerializeField]
    ParticleSystem FinishEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindAnyObjectByType<PlayerController>().OnDisable();
            FinishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke("Reset", resetTime);
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
