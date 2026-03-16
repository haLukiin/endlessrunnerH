using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject[] backgrounds;
    public float speed = 2f;

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += Vector3.left * speed * Time.deltaTime;
        }

        
        for (int i = 0; i < backgrounds.Length; i++)
        {
            SpriteRenderer sr = backgrounds[i].GetComponent<SpriteRenderer>();
            float rightEdge = backgrounds[i].transform.position.x + sr.bounds.size.x / 2;

            if (rightEdge < Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect)
            {
                
                GameObject rightmost = backgrounds[0];
                float maxX = rightmost.transform.position.x + rightmost.GetComponent<SpriteRenderer>().bounds.size.x / 2;

                for (int j = 1; j < backgrounds.Length; j++)
                {
                    float x = backgrounds[j].transform.position.x + backgrounds[j].GetComponent<SpriteRenderer>().bounds.size.x / 2;
                    if (x > maxX)
                    {
                        maxX = x;
                        rightmost = backgrounds[j];
                    }
                }

                SpriteRenderer srRight = rightmost.GetComponent<SpriteRenderer>();

                float newX =
                rightmost.transform.position.x +
                (srRight.bounds.size.x / 2) +
                (sr.bounds.size.x / 2);

                backgrounds[i].transform.position =
                new Vector3(newX, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            }
        }
    }
}