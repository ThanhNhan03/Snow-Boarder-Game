using UnityEngine;

public class SnowParticleScaler : MonoBehaviour
{
    public Camera mainCamera;
    public ParticleSystem snowParticleSystem;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; 
        }

        if (snowParticleSystem == null)
        {
            snowParticleSystem = GetComponent<ParticleSystem>(); 
        }

        AdjustParticleArea();
    }

    void AdjustParticleArea()
    {
        var shape = snowParticleSystem.shape;
        float cameraHeight = 2f * mainCamera.orthographicSize; 
        float cameraWidth = cameraHeight * mainCamera.aspect;  

        shape.scale = new Vector3(cameraWidth + 2, 1, cameraHeight + 2); 
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + (cameraHeight / 2), 0);
    }

    void Update()
    {
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + (2 * mainCamera.orthographicSize) / 2, 0);
    }
}
