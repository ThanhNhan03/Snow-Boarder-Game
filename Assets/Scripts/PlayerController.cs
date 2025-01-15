using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rb;
    [SerializeField] private float torqueAmout = 1f;
    [SerializeField] private float boostAmount = 30f;
    [SerializeField] private float baseSpeed = 20f;
    bool canMove = true;

    SurfaceEffector2D surfaceEffector2D;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            RotatePlayer();
            RespondBoost();
        }  
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
