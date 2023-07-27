using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particlePrefab;

    private void Start()
    {
        // Assuming GameManager has already been instantiated and initialized
        if (GameManager._instance.flocks.Count > 0)
        {
            Debug.Log("GameManager is initialized. Flocks count: " + GameManager._instance.flocks.Count);
            foreach (GameManager.Flock flock in GameManager._instance.flocks)
            {
                SpawnParticles(flock);
            }
        }
        else
        {
            Debug.Log("GameManager is not initialized or there are no flocks.");
        }
    }

    public void SpawnParticles(GameManager.Flock flock)
    {
        for (int i = 0; i < flock.particleCount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject newParticle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
            ParticleController particleController = newParticle.GetComponent<ParticleController>();

            // Set particle properties based on flock parameters
            particleController.aliWeight = flock.alignmentWeight;
            particleController.cohWeight = flock.cohesionWeight;
            particleController.sepWeight = flock.separationWeight;
            particleController.initEnergy = flock.initEnergy;

            // Add the new particle to the GameManager's list
            GameManager._instance.AddParticle(newParticle);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnX = Random.Range(-GameManager._instance.maxX, GameManager._instance.maxX);
        float spawnY = Random.Range(-GameManager._instance.maxY, GameManager._instance.maxY);
        return new Vector3(spawnX, spawnY, 0f);
    }
}