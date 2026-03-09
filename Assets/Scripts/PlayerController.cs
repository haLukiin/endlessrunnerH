using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the player's flappy-bird-style physics: falls with gravity, flaps upward on Space press.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Flap Settings")]
    [SerializeField] private float flapForce = 7f;
    [SerializeField] private float maxFallSpeed = -15f;

    private Rigidbody2D rigidBody;
    private bool isDead;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead) return;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Flap();
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        // Clamp fall speed
        Vector2 velocity = rigidBody.linearVelocity;
        velocity.y = Mathf.Max(velocity.y, maxFallSpeed);
        rigidBody.linearVelocity = velocity;
    }

    /// <summary>
    /// Applies an upward impulse to simulate a flap.
    /// </summary>
    private void Flap()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, flapForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    /// <summary>
    /// Kills the player and notifies the GameManager.
    /// </summary>
    private void Die()
    {
        if (isDead) return;
        isDead = true;
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.bodyType = RigidbodyType2D.Static;
        GameManager.Instance.OnPlayerDied();
    }
}
