using UnityEngine;

public class BirdObstacle : MonoBehaviour
{
    [Header("Bird Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float destroyOffsetX = -15f; 
    
    private Camera mainCamera;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        
        // Convert the bird's position to viewport coordinates
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
        float spriteWidth = spriteRenderer.bounds.size.x;
        float leftEdgeViewportX = mainCamera.WorldToViewportPoint(transform.position - Vector3.right * (spriteWidth/2)).x;
        
        // Destroy only when the entire sprite is off screen to the left
        if (leftEdgeViewportX < 0)
        {
            Destroy(gameObject);
        }
    }
}