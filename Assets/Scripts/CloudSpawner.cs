using UnityEngine;
using System.Collections.Generic;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs; // Các Prefab mây có thể spawn
    public Camera mainCamera;
    public float spawnOffsetX = 10f; // Khoảng cách spawn trước Camera
    public float minY = 2f, maxY = 6f; // Phạm vi chiều cao mây
    public float minScale = 0.5f, maxScale = 1.5f; // Kích thước ngẫu nhiên của mây
    public float cloudSpeed = 1f; // Tốc độ trôi của mây
    public int maxCloudsOnScreen = 10; // Số lượng mây tối đa trên màn hình

    private float lastXPosition;
    private List<GameObject> clouds = new List<GameObject>();

    void Start()
    {
        lastXPosition = mainCamera.transform.position.x;
    }

    void Update()
    {
        // Nếu camera di chuyển xa hơn vị trí trước đó + spawnOffsetX, tạo mây mới
        if (mainCamera.transform.position.x > lastXPosition + spawnOffsetX)
        {
            lastXPosition += spawnOffsetX;
            SpawnCloud();
        }

        // Xóa mây cũ nếu chúng ra khỏi màn hình
        RemoveOldClouds();
    }

    void SpawnCloud()
    {
        // Chọn ngẫu nhiên một Prefab mây
        GameObject cloudPrefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];

        // Random vị trí xuất hiện
        float spawnX = mainCamera.transform.position.x + spawnOffsetX;
        float spawnY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        // Tạo mây
        GameObject cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);

        // Random kích thước mây
        float scale = Random.Range(minScale, maxScale);
        cloud.transform.localScale = new Vector3(scale, scale, 1);

        // Cho mây di chuyển về bên trái
        Rigidbody2D rb = cloud.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(-cloudSpeed, 0);
        }

        // Thêm vào danh sách mây đang tồn tại
        clouds.Add(cloud);
    }

    void RemoveOldClouds()
    {
        // Duyệt qua danh sách mây và xóa nếu nó ra khỏi màn hình
        for (int i = clouds.Count - 1; i >= 0; i--)
        {
            if (clouds[i].transform.position.x < mainCamera.transform.position.x - 20f)
            {
                Destroy(clouds[i]);
                clouds.RemoveAt(i);
            }
        }
    }
}
