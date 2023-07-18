using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab; // Variable to hold the food prefab
    public int initialFoodCount;

    private void Start()
    {
        SpawnFood(initialFoodCount);
    }

    public void SpawnFood(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnX = Random.Range(-10f, 10f);
        float spawnY = Random.Range(-10f, 10f);
        return new Vector3(spawnX, spawnY, 0f);
    }
}
