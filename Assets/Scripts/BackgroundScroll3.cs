using UnityEngine;

public class BackgroundScroll3 : MonoBehaviour
{
    public float speed = 2f;
    public float resetX = 20f;
    public float leftLimit = -20f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftLimit)
        {
            transform.position = new Vector3(resetX, transform.position.y, transform.position.z);
        }
        if (transform.position.x < leftLimit)
        {
            Destroy(gameObject);
        }
    }

}