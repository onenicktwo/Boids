using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    private int initialFoodCount;

    private bool spawn = false;
    private float foodPerSec;
    private float secPerFood;
    private float timer;

    private void Awake()
    {
        initialFoodCount = GameManager._instance.initialFood;
        foodPerSec = (float)GameManager._instance.foodPerSec;
        SpawnFood(initialFoodCount);
    }

    private void Update()
    {
        if (spawn)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = secPerFood;
                GameObject food = Instantiate(foodPrefab, GetRandomSpawnPosition(), Quaternion.identity, this.transform);
                GameManager._instance.AddFood();
            }
        }
    }

    public void SpawnFood(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject food = Instantiate(foodPrefab, spawnPosition, Quaternion.identity, this.transform);
            GameManager._instance.AddFood();
        }
        secPerFood = 1 / foodPerSec;
        timer = secPerFood;
        spawn = true;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnX = Random.Range(-GameManager._instance.maxX, GameManager._instance.maxX);
        float spawnY = Random.Range(-GameManager._instance.maxY, GameManager._instance.maxY);
        return new Vector3(spawnX, spawnY, 0f);
    }
}
