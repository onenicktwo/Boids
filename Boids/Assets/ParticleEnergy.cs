using System.Collections;
using UnityEngine;

public class ParticleEnergy : MonoBehaviour
{
    public int energy; // this will store the particle's energy
    public int maxEnergy = 100; // maximum energy a particle can have
    public int energyDecreaseRate = 1; // how much energy decreases per second

    private void Start()
    {
        // Initialize energy to its maximum value
        energy = maxEnergy;
        StartCoroutine(DecreaseEnergyOverTime());
    }

    public void IncreaseEnergy(int amount)
    {
        energy += amount;

        // Make sure energy doesn't exceed maxEnergy
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        Debug.Log("Increased energy by " + amount + ". Current energy: " + energy);
    }

    private IEnumerator DecreaseEnergyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // wait for 1 second
            energy -= energyDecreaseRate;

            if (energy <= 0)
            {
                Debug.Log("Particle ran out of energy!");
                Destroy(gameObject);
                break; // this will exit the Coroutine
            }
        }
    }
}