using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public float foodPerSec;
    private float secPerFood;
    private float timer;

    private void Awake()
    {
        secPerFood = 1 / foodPerSec;
        timer = secPerFood;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = secPerFood;
            Instantiate(foodPrefab, new Vector3(Random.Range(-11.4f, 11.4f), Random.Range(-5f, 5f), 0), Quaternion.identity);
        }
    }
}
