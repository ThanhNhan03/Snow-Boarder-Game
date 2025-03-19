using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TerrainGenerator : MonoBehaviour
{
    public Camera mainCamera;
    public Transform player; 
    public SpriteShapeController spriteShapeController;
    [SerializeField] int segmentLength = 10; // Độ dài mỗi đoạn địa hình
    [SerializeField] int visibleChunks = 5; // Số đoạn hiển thị trước camera
    [SerializeField] float amplitude = 3f; // Biên độ
    [SerializeField] float frequency = 0.2f; // Tần số

    private List<Vector3> terrainPoints = new List<Vector3>();
    private float lastXPosition;
    private float terrainWidth = 0f;

    void Start()
    {
        lastXPosition = player.position.x;
        terrainWidth = player.position.x;

        GenerateInitialTerrain();
    }

    void Update()
    {
        float cameraX = mainCamera.transform.position.x;
        if (cameraX > lastXPosition - segmentLength)
        {
            GenerateNewSegment();
            lastXPosition += segmentLength;
        }

        RemoveOldSegments(cameraX);
    }

    void GenerateInitialTerrain()
    {
        for (int i = 0; i < visibleChunks; i++)
        {
            GenerateNewSegment();
        }
    }

    void GenerateNewSegment()
    {
        float startX = terrainWidth;

        for (int i = 0; i < segmentLength; i++)
        {
            float x = startX + i;
            float y = Mathf.PerlinNoise(x * frequency, 0) * amplitude;
            terrainPoints.Add(new Vector3(x, y, 0));
        }

        terrainWidth += segmentLength;
        UpdateTerrainShape();
    }

    void RemoveOldSegments(float cameraX)
    {
        float minX = cameraX - (visibleChunks * segmentLength);

        while (terrainPoints.Count > 0 && terrainPoints[0].x < minX)
        {
            terrainPoints.RemoveAt(0);
        }

        UpdateTerrainShape();
    }

    void UpdateTerrainShape()
    {
        var spline = spriteShapeController.spline;
        spline.Clear();

        for (int i = 0; i < terrainPoints.Count; i++)
        {
            spline.InsertPointAt(i, terrainPoints[i]);
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
        }

        float minY = -10f;
        if (terrainPoints.Count > 1)
        {
            spline.InsertPointAt(terrainPoints.Count, new Vector3(terrainPoints[^1].x, minY, 0));
            spline.InsertPointAt(terrainPoints.Count + 1, new Vector3(terrainPoints[0].x, minY, 0));
        }

        spriteShapeController.BakeMesh();
    }
}
