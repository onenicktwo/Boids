using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particlePrefab;

    private void Awake()
    {
        /*
        // Assuming GameManager has already been instantiated and initialized
        if (GameManager._instance.flocks.Count > 0)
        {
            foreach (GameManager.Flock flock in GameManager._instance.flocks)
            {
                SpawnParticles(flock);
            }
        }
        */
        for (int i = 0; i < GameManager._instance.initialParticles; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject newParticle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
            ParticleController particleController = newParticle.GetComponent<ParticleController>();

            // Very lazy way of choosing which particles call the reproduction script
            if (Random.Range(0, 2) == 1)
                particleController.selected = true;
            else
                particleController.selected = false;

            // Add the new particle to the GameManager's list
            GameManager._instance.AddParticle(newParticle);
            newParticle.name = "Particle " + GameManager._instance.particles.Count;
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
            
            // Very lazy way of choosing which particles call the reproduction script
            if (Random.Range(0, 2) == 1)
                particleController.selected = true;
            else
                particleController.selected = false;

            // Add the new particle to the GameManager's list
            GameManager._instance.AddParticle(newParticle);
            newParticle.name = "Particle " + GameManager._instance.particles.Count;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnX = Random.Range(-GameManager._instance.maxX, GameManager._instance.maxX);
        float spawnY = Random.Range(-GameManager._instance.maxY, GameManager._instance.maxY);
        return new Vector3(spawnX, spawnY, 0f);
    }
}
