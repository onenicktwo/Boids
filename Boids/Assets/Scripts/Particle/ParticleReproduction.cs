using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleReproduction : MonoBehaviour
{
    /*
     * Will hold all the future genes
     */
    public float radius = .1f;
    private ParticleController pc;

    private float busyTime;
    private float busyTimeFactor = 20f;
    private float cooldown;
    private float cooldownFactor = 10f;
    private float matureCooldown;
    private float matureCooldownFactor = 4f;
    [HideInInspector]
    public bool onCooldown = false;

    public GameObject childPrefab;

    public float energyUsage = 1f;

    private void Awake()
    {
        pc = GetComponent<ParticleController>();

        // Energy is already a timer, so this shouldn't cause issues
        busyTimeFactor = pc.busyTimeFactor;
        cooldownFactor = pc.reproduceCooldownFactor;
        matureCooldownFactor = pc.matureCooldownFactor;

        busyTime = pc.initEnergy * busyTimeFactor;
        cooldown = pc.initEnergy * cooldownFactor;
        matureCooldown = pc.initEnergy * matureCooldownFactor;
    }

    public void Check()
    {
        if (pc.selected)
        {
            foreach (ParticleController n in pc.particleNeighbors)
            {
                if (n != null &&
                    Vector2.Distance(n.gameObject.transform.position, this.gameObject.transform.position) < radius && 
                    !n.isBusy && 
                    !onCooldown && 
                    !n.reproduction.onCooldown)
                {
                    pc.isBusy = true;
                    pc.particleNeighbors[pc.particleNeighbors.IndexOf(n)].isBusy = true;
                    pc.rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
                    pc.particleNeighbors[pc.particleNeighbors.IndexOf(n)].rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
                    StartCoroutine(Reproduce(n));
                }
            }
        }
    }

    private IEnumerator Reproduce(ParticleController otherParent)
    {
        yield return new WaitForSecondsRealtime(busyTime);

        // Interesting error, since the prefab is technically itself you have to reset its constraints first or the child is frozen
        // I don't know the immediate fix for this since making a seperate prefab would still need a prefab to spawn in
        int indexOfOtherPC = pc.particleNeighbors.IndexOf(otherParent);
        StartCoroutine(Cooldown());
        StartCoroutine(pc.particleNeighbors[indexOfOtherPC].reproduction.Cooldown());

        // Child making
        GameObject particle = Instantiate(childPrefab, this.transform.position, Quaternion.identity);
        GameManager._instance.AddParticle(particle);
        particle.name = "Particle " + GameManager._instance.particles.Count;
        if (Random.Range(0, 2) == 1)
            particle.GetComponent<ParticleController>().selected = true;
        else
            particle.GetComponent<ParticleController>().selected = false;
        StartCoroutine(particle.GetComponent<ParticleController>().reproduction.MatureCooldown());

        pc.currEnergy -= energyUsage;
    }

    // Uses the already made onCooldown variable (making another variable would just overcomplicate things)
    public IEnumerator MatureCooldown()
    {
        onCooldown = true;
        yield return new WaitForSecondsRealtime(matureCooldown);
        onCooldown = false;
    }

    public IEnumerator Cooldown()
    {
        pc.isBusy = false;
        // Error grabbing the rb2d (saying null, meaning it isn't set by the time it reaches here?)
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        onCooldown = true;
        yield return new WaitForSecondsRealtime(cooldown);
        onCooldown = false;
    }
}
