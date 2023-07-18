using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovement : MonoBehaviour
{
    private ParticleController pc;

    private void Awake()
    {
        pc = this.GetComponent<ParticleController>();
    }
    public Vector2 alignment()
    {
        Vector2 v = Vector2.zero;
        if (pc.particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in pc.particleNeighbors)
            {
                v += (Vector2) (n.rb2d.velocity);
            }
            v /= pc.particleNeighbors.Count;
        }
        return v;
    }

    public Vector2 cohesion()
    {
        Vector2 v = Vector2.zero;
        if (pc.particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in pc.particleNeighbors)
            {
                v += (Vector2)(n.gameObject.transform.position);
            }
            v /= pc.particleNeighbors.Count;
            v -= (Vector2) (pc.gameObject.transform.position);  
        }
        return v;
    }

    public Vector2 seperation()
    {
        Vector2 v = Vector2.zero;
        int nAvoid = 0;
        if (pc.particleNeighbors.Count != 0)
        {
            foreach (ParticleController n in pc.particleNeighbors)
            {
                if (Vector2.SqrMagnitude(n.gameObject.transform.position - pc.gameObject.transform.position) < 4f)
                {
                    nAvoid++;
                    v += (Vector2)(pc.gameObject.transform.position - n.gameObject.transform.position);
                }
            }
            if(nAvoid > 0)
                v /= nAvoid;
        }
        return v;
        /*
         * // If no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        // Add all points together and average.
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
         */
    }

    public Vector2 nearestFood()
    {
        Vector2 v = Vector2.zero;
        if (pc.foodNeighbors.Count == 0)
            return Vector2.zero;
        for(int i = 0; i < pc.foodNeighbors.Count; i++)
        {
            if (pc.foodNeighbors[i] != null)
            {       
                if (v == Vector2.zero)
                    v = pc.foodNeighbors[i].transform.position - pc.gameObject.transform.position;
                else if ((pc.foodNeighbors[i].transform.position - pc.gameObject.transform.position).magnitude < v.magnitude)
                {
                    v = pc.foodNeighbors[i].transform.position - pc.gameObject.transform.position;
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
