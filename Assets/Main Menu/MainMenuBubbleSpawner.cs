using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBubbleSpawner : MonoBehaviour
{
    public GameObject BubblePrefab;

    public Vector2 oscillationRange;
    public Vector2 oscillationSpeed;
    public Vector2 bubbleSizeRange;

    private float minOscillation;
    private float maxOscillation;
    private float minOscillationSpeed;
    private float maxOscillationSpeed;
    private float minBubbleSize;
    private float maxBubbleSize;

    public int maxBubbleCount;
    public float currentBubbleCount = 0;
    private Collider2D collider2d;

    List<GameObject> bubbles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
        minOscillation = oscillationRange.x;
        maxOscillation = oscillationRange.y;
        minOscillationSpeed = oscillationSpeed.x;
        maxOscillationSpeed = oscillationSpeed.y;
        minBubbleSize = bubbleSizeRange.x;
        maxBubbleSize = bubbleSizeRange.y;

        StartCoroutine(SpawnBubbles());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnBubbles()
    {
        for (int i = 0; i < maxBubbleCount; i++)
        {
            if (currentBubbleCount < maxBubbleCount)
            {
                Vector3 randomPoint = RandomPointInBounds();
                GameObject bubble = Instantiate(BubblePrefab, randomPoint, Quaternion.identity);
                float randomsize = Random.Range(minBubbleSize, maxBubbleSize);
                bubble.transform.localScale = new Vector3(randomsize, randomsize, randomsize);
                SFXPlayer.instance.PlayBubbleSpawnSFX();
                bubble.GetComponent<BubbleMainMenu>().speed = Random.Range(minOscillationSpeed, maxOscillationSpeed);
                bubble.GetComponent<BubbleMainMenu>().offset = Random.Range(minOscillation, maxOscillation);
                currentBubbleCount++;
                bubbles.Add(bubble);
            }
            yield return new WaitForSeconds(0.5f);
        }

        StartCoroutine(BreakAllBubbles());
    }

    IEnumerator BreakAllBubbles()
    {
        print("Breaking all bubbles");
        foreach (GameObject bubble in bubbles)
        {
            SFXPlayer.instance.PlayBubblePopSFX();
            Destroy(bubble);
            currentBubbleCount--;
            float randomtime = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(randomtime);
        }

        bubbles.Clear();
        yield return new WaitForSeconds(3f);
        StartCoroutine(SpawnBubbles());
    }

    Vector3 RandomPointInBounds()
    {
        Vector3 randomvector = new Vector3(Random.Range(collider2d.bounds.min.x, collider2d.bounds.max.x), Random.Range(collider2d.bounds.min.y, collider2d.bounds.max.y), 0);
        return randomvector;
    }
}