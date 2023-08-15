using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particlePrefab;

    private void Awake()
    {
        Debug.Log("GameManager instance: " + GameManager._instance);
        if(GameManager._instance != null)
            Debug.Log("GameManager flocks count: " + GameManager._instance.flocks.Count);

        // Loop through each flock in the GameManager's flocks list
        ICollection flockKeys = GameManager._instance.flocks.Keys;
        foreach (string flockKey in flockKeys)
        {
            SpawnParticlesForFlock((GameManager.Flock) GameManager._instance.flocks[flockKey]);
        }
    }

    private void SpawnParticlesForFlock(GameManager.Flock flock)
    {
        for (int i = 0; i < flock.particleCount; i++)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject newParticle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity, this.transform);
        ParticleController particleController = newParticle.GetComponent<ParticleController>();
        
        // Set the random followSpeed for the particle
        particleController.followSpeed = Random.Range(1f, 5f);

            // Set particle properties based on flock parameters
            particleController.flockID = flock.flockID;
            particleController.speed = flock.speed;
            particleController.initEnergy = flock.initEnergy;
            particleController.GetComponent<CircleCollider2D>().radius = flock.sightRadius;
            particleController.aliWeight = flock.alignmentWeight;
            particleController.cohWeight = flock.cohesionWeight;
            particleController.sepWeight = flock.separationWeight;

            particleController.hungryPercent = flock.hungryPercentage;
            particleController.reproducePercent = flock.reproducePercentage;

            // Very lazy way of choosing which particles call the reproduction script
            particleController.selected = GeneSelector.GetGeneBool();

            // Add the new particle to the GameManager's list
            GameManager._instance.AddParticle(newParticle);
            newParticle.name = "Particle " + GameManager._instance.particles.Count;
            particleController.spriteRenderer.color = flock.flockColor; // Set the color of the particle based on the flock's color
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float spawnX = Random.Range(-GameManager._instance.maxX, GameManager._instance.maxX);
        float spawnY = Random.Range(-GameManager._instance.maxY, GameManager._instance.maxY);
        return new Vector3(spawnX, spawnY, 0f);
    }
}
