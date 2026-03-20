using UnityEngine;

public class BackgroundScroll2 : MonoBehaviour
{
    public float speed = 2f;
    void Update() { transform.position += Vector3.left * speed * Time.deltaTime; }
}