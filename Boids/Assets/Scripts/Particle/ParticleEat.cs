using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEat : MonoBehaviour
{
    public ParticleController pc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "food")
        {
            pc.currEnergy += collision.GetComponent<FoodBehave>().energyValue;
            pc.foodNeighbors.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
