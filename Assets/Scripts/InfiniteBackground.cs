using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject[] backgrounds;
    public float speed = 2f;

    [Header("Seam Covers")]
    public GameObject monsterPrefab;
    public GameObject deathStarPrefab;

    private GameObject[] coverPrefabs;
    private int nextCoverIndex = 0;

    void Start()
    {
        coverPrefabs = new GameObject[] { monsterPrefab, deathStarPrefab };

        // Sort backgrounds by X position to ensure we know which one is leftmost
        System.Array.Sort(backgrounds, (a, b) => a.transform.position.x.CompareTo(b.transform.position.x));

        // Set Death Star as the first to be spawned (index 1)
        nextCoverIndex = 1;

        // Position the first cover at the initial seam between the first two backgrounds
        if (backgrounds.Length >= 2)
        {
            SpriteRenderer sr0 = backgrounds[0].GetComponent<SpriteRenderer>();
            float initialSeamX = backgrounds[0].transform.position.x + (sr0.bounds.size.x / 2);
            PositionCoverAtSeam(initialSeamX);
        }
    }

    void Update()
    {
        float dt = Time.deltaTime;
        
        // Move backgrounds
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position += Vector3.left * speed * dt;
        }

        // Covers move themselves via SeamCover component

        for (int i = 0; i < backgrounds.Length; i++)
        {
            SpriteRenderer sr = backgrounds[i].GetComponent<SpriteRenderer>();
            float rightEdge = backgrounds[i].transform.position.x + (sr.bounds.size.x / 2);

            // Check if the background piece has moved off-screen to the left
            if (rightEdge < Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect))
            {
                // Find current rightmost piece
                GameObject rightmost = backgrounds[0];
                float maxX = -float.MaxValue;

                for (int j = 0; j < backgrounds.Length; j++)
                {
                    float x = backgrounds[j].transform.position.x + (backgrounds[j].GetComponent<SpriteRenderer>().bounds.size.x / 2);
                    if (x > maxX)
                    {
                        maxX = x;
                        rightmost = backgrounds[j];
                    }
                }

                SpriteRenderer srRight = rightmost.GetComponent<SpriteRenderer>();

                // Move this piece to the right of the current rightmost piece
                float newX = rightmost.transform.position.x + (srRight.bounds.size.x / 2) + (sr.bounds.size.x / 2);
                backgrounds[i].transform.position = new Vector3(newX, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);

                // Place a cover exactly on the seam
                float seamX = rightmost.transform.position.x + (srRight.bounds.size.x / 2);
                PositionCoverAtSeam(seamX);
            }
        }
    }

    void PositionCoverAtSeam(float xPos)
    {
        GameObject prefab = coverPrefabs[nextCoverIndex];
        if (prefab == null) return;

        float randomY = Random.Range(-1f, 1f);
        GameObject cover = Instantiate(prefab, new Vector3(xPos, randomY, -1f), Quaternion.identity);

        SpriteRenderer coverSR = cover.GetComponent<SpriteRenderer>();
        if (coverSR != null)
            coverSR.sortingOrder = 10;

        cover.AddComponent<SeamCover>().speed = speed;

        nextCoverIndex = (nextCoverIndex + 1) % coverPrefabs.Length;
    }
}