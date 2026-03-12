using UnityEngine;

public class BackgroundScroll3 : MonoBehaviour
{
    public float speed = 2f;

    private float spriteWidth;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;
    }
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        
        if (transform.position.x <= -spriteWidth)
        {
            transform.position += new Vector3(spriteWidth * 3, 0, 0); 
        }
    }
}