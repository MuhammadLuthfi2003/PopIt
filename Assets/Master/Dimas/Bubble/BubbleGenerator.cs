using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    [Range(1, 5)]
    public int countSpawn;
    public float spawnTimerDelay;

    public GameObject[] BubblesGameobjects;
    public BoxCollider2D BubblesSpawnCollider;

    private void Start() 
    {
        StartCoroutine(BubbleSpawning());
    }

    private IEnumerator BubbleSpawning()
    {
        CreateInstance();
        yield return new WaitForSeconds(spawnTimerDelay);
        
        StartCoroutine(BubbleSpawning());
    }

    private void CreateInstance()
    {
        int spawnIndex = UnityEngine.Random.Range(1, countSpawn);

        for (int i = 0; i < spawnIndex; i++)
        {
            GameObject bubblePrefabs = BubblesGameobjects[UnityEngine.Random.Range(0, BubblesGameobjects.Length)];
            GameObject bubblessIns = Instantiate(bubblePrefabs);
            bubblessIns.SetActive(true);

            bubblessIns.transform.position = GetRandomPositionInsideBox();
            bubblessIns.transform.localScale = Vector2.one * GetRandomBubbleScale();

            Bubble bubble = bubblessIns.GetComponent<Bubble>();

            bubble.defaultHP = UnityEngine.Random.Range(1, 4);
        }
    }

    public float GetRandomBubbleScale()
    {
        float randomIndex = UnityEngine.Random.Range(0.6202f, 1f);
        
        return randomIndex;
    }
    public Vector2 GetRandomPositionInsideBox()
    {
        if (BubblesSpawnCollider == null)
        {
            Debug.LogError("BoxCollider2D is not assigned!");
            return Vector2.zero;
        }

        // Get the bounds of the BoxCollider2D
        Bounds bounds = BubblesSpawnCollider.bounds;

        // Generate a random position inside the bounds
        float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float randomY = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }
}
