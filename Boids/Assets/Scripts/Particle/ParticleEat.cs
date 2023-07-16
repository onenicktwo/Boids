using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEat : MonoBehaviour
{
    ParticleController pc;

    private void Awake()
    {
        pc = gameObject.GetComponentInParent<ParticleController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "food")
        {
            Eat(pc.foodNeighbors.IndexOf(collision.gameObject));
            Destroy(collision.gameObject);
        }
    }

    private void Eat(int foodIndex)
    {
        // Add energy based on food script
        pc.foodNeighbors.RemoveAt(foodIndex);
    }
}
