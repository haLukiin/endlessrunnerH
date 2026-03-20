using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float minSpeedX = 2f;
    public float maxSpeedX = 5f;

    public float minSpeedY = 1f;
    public float maxSpeedY = 3f;

    private float speedX;
    private float speedY;


    void Start()
    {
        // Random speeds for variation
        speedX = Random.Range(minSpeedX, maxSpeedX);
        speedY = Random.Range(minSpeedY, maxSpeedY);
    }

    void Update()
    {
        // Move left AND down
        transform.position += new Vector3(-speedX, -speedY, 0) * Time.deltaTime;
    }
}