using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particlePrefab;
    public int initialParticleCount;

    private void Awake()
    {
        initialParticleCount = GameManager._instance.particleCount;
        SpawnParticles(initialParticleCount);
    }

    public void SpawnParticles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject particle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
            GameManager._instance.AddParticle(particle);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnX = Random.Range(-GameManager._instance.maxX, GameManager._instance.maxX);
        float spawnY = Random.Range(-GameManager._instance.maxY, GameManager._instance.maxY);
        return new Vector3(spawnX, spawnY, 0f);
    }
}
