using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float minHeight = 3f;
    [SerializeField] private float maxHeight = 6f;
    [SerializeField] private float spawnOffsetX = 15f; // Khoảng cách spawn so với camera
    
    private Camera mainCamera;
    private float timer;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnInterval)
        {
            SpawnBird();
            timer = 0f;
        }
    }
    
    void SpawnBird()
    {
        // Lấy vị trí camera
        Vector3 cameraPosition = mainCamera.transform.position;
        
        // Tính toán vị trí spawn bên phải camera
        float spawnX = cameraPosition.x + spawnOffsetX;
        float randomY = Random.Range(minHeight, maxHeight) + cameraPosition.y;
        Vector2 spawnPosition = new Vector2(spawnX, randomY);
        
        // Spawn bird
        GameObject bird = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
    }
}