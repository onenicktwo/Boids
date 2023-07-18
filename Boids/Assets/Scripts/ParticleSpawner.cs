using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particlePrefab;
    public int initialParticleCount;

    private void Start()
    {
        SpawnParticles(initialParticleCount);
    }

    public void SpawnParticles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnX = Random.Range(-10f, 10f);
        float spawnY = Random.Range(-10f, 10f);
        return new Vector3(spawnX, spawnY, 0f);
    }
}
