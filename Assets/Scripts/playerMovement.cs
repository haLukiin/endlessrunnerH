using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float rotationSpeed = 5f;

    private Rigidbody2D rb;
    private bool isDead = false;

    [Header("References")]
    public GameManager gameManager; // assign in Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDead)
        {
            Flymovement();
            RotatePlayer();
        }
    }

    void Flymovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            rb.linearVelocity = Vector2.up * jumpForce;
    }

    void RotatePlayer()
    {
        float angle = rb.linearVelocity.y * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
            Die();
    }

    void Die()
    {
        isDead = true;

        // Hide the player
        gameObject.SetActive(false);

        // Tell GameManager to show Game Over
        if (gameManager != null)
            gameManager.GameOver();
    }
}