using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    private ParticleController pc;
    public float radius = 1f;
    private void Awake()
    {
        pc = this.GetComponent<ParticleController>();
    }
    public Vector2 alignment(List<ParticleController> particleNeighbors)
    {
        Vector2 v = Vector2.zero;
        if (particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in particleNeighbors)
            {
                v += (Vector2) (n.rb2d.velocity);
            }
            v /= particleNeighbors.Count;
        }
        return v;
    }

    public Vector2 cohesion(List<ParticleController> particleNeighbors)
    {
        Vector2 v = Vector2.zero;
        if (particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in particleNeighbors)
            {
                v += (Vector2)(n.gameObject.transform.position);
            }
            v /= particleNeighbors.Count;
            v -= (Vector2) (pc.gameObject.transform.position);  
        }
        return v;
    }

    public Vector2 seperation(List<ParticleController> particleNeighbors)
    {
        Vector2 v = Vector2.zero;
        int nAvoid = 0;
        if (particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in particleNeighbors)
            {
                if (Vector2.Distance(n.gameObject.transform.position, this.gameObject.transform.position) < radius)
                {
                    nAvoid++;
                    v += (Vector2)(this.gameObject.transform.position - n.gameObject.transform.position);
                }
            }
            if(nAvoid > 0)
                v /= nAvoid;
        }
        return v;
    }

    public Vector2 nearestFood(List<GameObject> foodNeighbors)
    {
        Vector2 v = Vector2.zero;
        if (foodNeighbors.Count == 0)
            return Vector2.zero;
        for(int i = 0; i < foodNeighbors.Count; i++)
        {
            if (foodNeighbors[i] != null)
            {       
                if (v == Vector2.zero)
                    v = foodNeighbors[i].transform.position - this.gameObject.transform.position;
                else if ((foodNeighbors[i].transform.position - this.gameObject.transform.position).magnitude < v.magnitude)
                {
                    v = foodNeighbors[i].transform.position - this.gameObject.transform.position;
                }
            }
            else
            {
                pc.foodNeighbors.Remove(pc.foodNeighbors[i]);
                i--;
            }
        }
        v.Normalize();
        return v;
    }
}
