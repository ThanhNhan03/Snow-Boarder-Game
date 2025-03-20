using UnityEngine;

public class SlideEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem SlideEffectParticle;
    [SerializeField] AudioSource slideAudioSource; 
    [SerializeField] AudioClip slideSound; 
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            SlideEffectParticle.Play();
            if (slideAudioSource != null && slideSound != null)
            {
                slideAudioSource.clip = slideSound;
                slideAudioSource.Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            SlideEffectParticle.Stop();
            if (slideAudioSource != null)
            {
                slideAudioSource.Stop();
            }
        }
    }
}
