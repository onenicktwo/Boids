using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehave : MonoBehaviour
{
    public int energyValue = 5;
    public Color foodColor = Color.green;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Particle"))
        {
            ParticleEnergy particle = other.GetComponent<ParticleEnergy>();

            if (particle != null)
            {
                // Increase the particle's energy
                particle.IncreaseEnergy(energyValue);
            }

            // Destroy the food object after it has been consumed
            Destroy(gameObject);
        }
    }
}

