using UnityEngine;

public class playerMovement : MonoBehaviour
{

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float rotationSpeed = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Flymovement();
        RotatePlayer();
    }

    public void Flymovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    void RotatePlayer()
    {
        float angle = rb.linearVelocity.y * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}


